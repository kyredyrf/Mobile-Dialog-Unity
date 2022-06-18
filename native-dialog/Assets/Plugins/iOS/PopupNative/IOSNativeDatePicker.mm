//  Created by PingAK9

#import "IOSNativeDatePicker.h"

static NSString *_gameObjectName = nil;

@implementation IOSNativeDatePicker

+ (CGFloat) GetW {
    
    UIViewController *vc =  UnityGetGLViewController();
    
    bool IsLandscape = true;
    
    
    UIInterfaceOrientation orientation = [UIApplication sharedApplication].statusBarOrientation;
    if(orientation == UIInterfaceOrientationLandscapeLeft || orientation == UIInterfaceOrientationLandscapeRight) {
        IsLandscape = true;
    } else {
        IsLandscape = false;
    }
    
    CGFloat w;
    if(IsLandscape) {
        w = vc.view.frame.size.height;
    } else {
        w = vc.view.frame.size.width;
    }
    
    
    NSArray *vComp = [[UIDevice currentDevice].systemVersion componentsSeparatedByString:@"."];
    if ([[vComp objectAtIndex:0] intValue] >= 8) {
        w = vc.view.frame.size.width;
    }
    
    
    return w;
}

+ (void)DP_changeDate:(UIDatePicker *)sender {
    
    NSDateFormatter *dateFormatter = [[NSDateFormatter alloc] init];

    
    [dateFormatter setDateFormat: @"yyyy-MM-dd HH:mm:ss"];
    NSString *dateString = [dateFormatter stringFromDate:sender.date];
    
    NSLog(@"DateChangedEvent: %@", dateString);

    char *gameObjectName = (char *) [_gameObjectName UTF8String];
    UnitySendMessage(gameObjectName, "DateChangedEvent", [DataConvertor NSStringToChar:dateString]);
}

+(void) DP_PickerClosed:(UIDatePicker *)sender {
    NSDateFormatter *dateFormatter = [[NSDateFormatter alloc] init];

    [dateFormatter setDateFormat: @"yyyy-MM-dd HH:mm:ss"];
    NSString *dateString = [dateFormatter stringFromDate:sender.date];
    
    NSLog(@"DateChangedEvent: %@", dateString);
    
    char *gameObjectName = (char *) [_gameObjectName UTF8String];
    UnitySendMessage(gameObjectName, "PickerClosedEvent", [DataConvertor NSStringToChar:dateString]);
    
}

UIDatePicker *datePicker;

+ (void) DP_showTimePicker:(double)firstUnixTime {
    [self DP_show:1 firstUnixTime:firstUnixTime minimumUnixTime:0 maximumUnixTime:0];
}

+ (void) DP_showDatePicker:(double)firstUnixTime minimumUnixTime:(double)minimumUnixTime maximumUnixTime:(double)maximumUnixTime {
    [self DP_show:2 firstUnixTime:firstUnixTime minimumUnixTime:minimumUnixTime maximumUnixTime:maximumUnixTime];
}

+ (void) DP_show:(int)mode firstUnixTime:(double)firstUnixTime minimumUnixTime:(double)minimumUnixTime maximumUnixTime:(double)maximumUnixTime {
    UIViewController *vc =  UnityGetGLViewController();
    
    
    
    CGRect toolbarTargetFrame = CGRectMake(0, vc.view.bounds.size.height-216-44, [self GetW], 44);
    CGRect datePickerTargetFrame = CGRectMake(0, vc.view.bounds.size.height-216, [self GetW], 216);
    CGRect darkViewTargetFrame = CGRectMake(0, vc.view.bounds.size.height-216-44, [self GetW], 260);
    CGRect whiteViewTargetFrame = vc.view.bounds;
    
    UIView *darkView = [[UIView alloc] initWithFrame:CGRectMake(0, vc.view.bounds.size.height, [self GetW], 260)];
    darkView.alpha = 1;

    if (@available(iOS 13.0, *)) {
        darkView.backgroundColor = [UIColor systemGray5Color];
    } else {
        if (@available(iOS 12.0, *)) {
            if (darkView.traitCollection.userInterfaceStyle == UIUserInterfaceStyleDark) {
                darkView.backgroundColor = [UIColor blackColor];
            } else {
                darkView.backgroundColor = [UIColor whiteColor];
            }
        } else {
            darkView.backgroundColor = [UIColor whiteColor];
        }
    }

    darkView.tag = 9;
    
    UIView *whiteView = [[UIView alloc] initWithFrame:vc.view.bounds];
    whiteView.alpha = 1;
    whiteView.backgroundColor = [[UIColor alloc] initWithRed:1 green:1 blue:1 alpha:0];
    whiteView.tag = 8;
    
    UITapGestureRecognizer *tapGesture = [[UITapGestureRecognizer alloc] initWithTarget:self action:@selector(DP_dismissDatePicker:)];
    [darkView addGestureRecognizer:tapGesture];
    [vc.view addSubview:darkView];
    [whiteView addGestureRecognizer:tapGesture];
    [vc.view addSubview:whiteView];
    
    
    datePicker = [[UIDatePicker alloc] initWithFrame:CGRectMake(0, vc.view.bounds.size.height+44, [self GetW], 216)];
    datePicker.tag = 10;
        
    
    [datePicker addTarget:self action:@selector(DP_changeDate:) forControlEvents:UIControlEventValueChanged];
    switch (mode) {
        case 1:
            datePicker.datePickerMode = UIDatePickerModeTime;
            break;
            
        case 2:
            datePicker.datePickerMode = UIDatePickerModeDate;
            break;
    }
    
    if (@available(iOS 13.4, *)) {
        datePicker.preferredDatePickerStyle = UIDatePickerStyleWheels;
    }
    if (minimumUnixTime != 0.0) {
        datePicker.minimumDate = [NSDate dateWithTimeIntervalSince1970 :minimumUnixTime];
    }
    if (maximumUnixTime != 0.0) {
        datePicker.maximumDate = [NSDate dateWithTimeIntervalSince1970 :maximumUnixTime];
    }
    NSDate *dateTraded = [NSDate dateWithTimeIntervalSince1970 :firstUnixTime];
    [datePicker setDate:dateTraded];
    
    // NSLog(@"dtp mode: %ld", (long)datePicker.datePickerMode);
    
    
    [vc.view addSubview:datePicker];
    
    UIToolbar *toolBar = [[UIToolbar alloc] initWithFrame:CGRectMake(0, vc.view.bounds.size.height, [self GetW], 44)];
    
    toolBar.tag = 11;
    toolBar.barStyle = UIBarStyleDefault;
    UIBarButtonItem *spacer = [[UIBarButtonItem alloc] initWithBarButtonSystemItem:UIBarButtonSystemItemFlexibleSpace target:nil action:nil];
    UIBarButtonItem *doneButton = [[UIBarButtonItem alloc] initWithBarButtonSystemItem:UIBarButtonSystemItemDone target:self action:@selector(DP_dismissDatePicker:)];

    
    [toolBar setItems:[NSArray arrayWithObjects:spacer, doneButton, nil]];
    [vc.view addSubview:toolBar];
    
    [UIView beginAnimations:@"MoveIn" context:nil];
    toolBar.frame = toolbarTargetFrame;
    datePicker.frame = datePickerTargetFrame;
    darkView.frame = darkViewTargetFrame;
    whiteView.frame = whiteViewTargetFrame;
    
    [UIView commitAnimations];
    
}

+ (void)DP_removeViews:(id)object {
    UIViewController *vc =  UnityGetGLViewController();
    
    [[vc.view viewWithTag:8] removeFromSuperview];
    [[vc.view viewWithTag:9] removeFromSuperview];
    [[vc.view viewWithTag:10] removeFromSuperview];
    [[vc.view viewWithTag:11] removeFromSuperview];
}

+ (void)DP_dismissDatePicker:(id)sender {
    UIViewController *vc =  UnityGetGLViewController();
    
    [self DP_PickerClosed:datePicker];
    
    CGRect toolbarTargetFrame = CGRectMake(0, vc.view.bounds.size.height, [self GetW], 44);
    CGRect datePickerTargetFrame = CGRectMake(0, vc.view.bounds.size.height+44, [self GetW], 216);
    CGRect darkViewTargetFrame = CGRectMake(0, vc.view.bounds.size.height, [self GetW], 260);
    
    
    [UIView beginAnimations:@"MoveOut" context:nil];
    [vc.view viewWithTag:9].frame = darkViewTargetFrame;
    [vc.view viewWithTag:10].frame = datePickerTargetFrame;
    [vc.view viewWithTag:11].frame = toolbarTargetFrame;
    [UIView setAnimationDelegate:self];
    [UIView setAnimationDidStopSelector:@selector(DP_removeViews:)];
    [UIView commitAnimations];
}


extern "C" {
    
    //--------------------------------------
    //  Unity Call Date Time Picker
    //--------------------------------------
    
    void _TAG_ShowTimePicker(char* gameObjectName, double unix) {
        _gameObjectName = [DataConvertor charToNSString:gameObjectName];
        [IOSNativeDatePicker DP_showTimePicker:unix];
    }
    
    void _TAG_ShowDatePicker(char* gameObjectName, double unix) {
        _gameObjectName = [DataConvertor charToNSString:gameObjectName];
        [IOSNativeDatePicker DP_showDatePicker:unix minimumUnixTime:0 maximumUnixTime:0];
    }

    void _TAG_ShowDatePickerWithRange(char* gameObjectName, double firstUnixTime, double minimumUnixTime, double maximumUnixTime) {
        _gameObjectName = [DataConvertor charToNSString:gameObjectName];
        [IOSNativeDatePicker DP_showDatePicker:firstUnixTime minimumUnixTime:minimumUnixTime maximumUnixTime:maximumUnixTime];
    }
}

@end
