//  Created by PingAK9

#import "IOSNativePopUpsManager.h"

@implementation IOSNativePopUpsManager

static UIAlertController* _currentAllert =  nil;
static NSString *_gameObjectName = nil;

+ (void) unregisterAllertView {
    if(_currentAllert != nil) {
        _currentAllert = nil;
    }
}

+(void) dismissCurrentAlert {
    if(_currentAllert != nil) {
        [_currentAllert dismissViewControllerAnimated:NO completion:nil];
        _currentAllert = nil;
    }
}

+(void) ShowDialogNeutral: (NSString *) title message: (NSString*) msg acceptTitle: (NSString*) b1 neutralTitle: (NSString*) b2 declineTitle: (NSString*) b3 {

    UIAlertController *alertController = [UIAlertController alertControllerWithTitle:title message:msg preferredStyle:UIAlertControllerStyleAlert];
    
    UIAlertAction *rateAction = [UIAlertAction actionWithTitle:b1 style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [IOSNativePopUpsManager unregisterAllertView];
        char *gameObjectName = (char *) [_gameObjectName UTF8String];
        UnitySendMessage(gameObjectName, "OnAcceptCallBack",  [DataConvertor NSIntToChar:0]);
    }];
    
    UIAlertAction *laterAction = [UIAlertAction actionWithTitle:b2 style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [IOSNativePopUpsManager unregisterAllertView];
        char *gameObjectName = (char *) [_gameObjectName UTF8String];
        UnitySendMessage(gameObjectName, "OnNeutralCallBack",  [DataConvertor NSIntToChar:1]);
    }];

    UIAlertAction *declineAction = [UIAlertAction actionWithTitle:b3 style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [IOSNativePopUpsManager unregisterAllertView];
        char *gameObjectName = (char *) [_gameObjectName UTF8String];
        UnitySendMessage(gameObjectName, "OnDeclineCallBack",  [DataConvertor NSIntToChar:2]);
    }];

    [alertController addAction:rateAction];
    [alertController addAction:laterAction];
    [alertController addAction:declineAction];
    
    [[[[UIApplication sharedApplication] keyWindow] rootViewController] presentViewController:alertController animated:YES completion:nil];
    _currentAllert = alertController;
}

+ (void) ShowDialogConfirm: (NSString *) title message: (NSString*) msg yesTitle:(NSString*) b1 noTitle: (NSString*) b2{

    UIAlertController *alertController = [UIAlertController alertControllerWithTitle:title message:msg preferredStyle:UIAlertControllerStyleAlert];
    
    UIAlertAction *yesAction = [UIAlertAction actionWithTitle:b1 style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [IOSNativePopUpsManager unregisterAllertView];
        char *gameObjectName = (char *) [_gameObjectName UTF8String];
        UnitySendMessage(gameObjectName, "OnYesCallBack",  [DataConvertor NSIntToChar:0]);
    }];
    
    UIAlertAction *noAction = [UIAlertAction actionWithTitle:b2 style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [IOSNativePopUpsManager unregisterAllertView];
        char *gameObjectName = (char *) [_gameObjectName UTF8String];
        UnitySendMessage(gameObjectName, "OnNoCallBack",  [DataConvertor NSIntToChar:1]);
    }];
    
    [alertController addAction:yesAction];
    [alertController addAction:noAction];
    
    
    [[[[UIApplication sharedApplication] keyWindow] rootViewController] presentViewController:alertController animated:YES completion:nil];
    _currentAllert = alertController;
}

+(void)ShowDialogInfo: (NSString *) title message: (NSString*) msg okTitle:(NSString*) b1 {
    
    UIAlertController *alertController = [UIAlertController alertControllerWithTitle:title message:msg preferredStyle:UIAlertControllerStyleAlert];
    
    UIAlertAction *okAction = [UIAlertAction actionWithTitle:b1 style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [IOSNativePopUpsManager unregisterAllertView];
        char *gameObjectName = (char *) [_gameObjectName UTF8String];
        UnitySendMessage(gameObjectName, "OnOkCallBack",  [DataConvertor NSIntToChar:0]);
    }];
    [alertController addAction:okAction];
    
    
    [[[[UIApplication sharedApplication] keyWindow] rootViewController] presentViewController:alertController animated:YES completion:nil];
    _currentAllert = alertController;
}

extern "C" {
    // Unity Call
    
    void _TAG_ShowDialogNeutral(char* gameObjectName, char* title, char* message, char* accept, char* neutral, char* decline) {
        _gameObjectName = [DataConvertor charToNSString:gameObjectName];
        [IOSNativePopUpsManager ShowDialogNeutral:[DataConvertor charToNSString:title] message:[DataConvertor charToNSString:message] acceptTitle:[DataConvertor charToNSString:accept] neutralTitle:[DataConvertor charToNSString:neutral] declineTitle:[DataConvertor charToNSString:decline]];
    }
    
    void _TAG_ShowDialogConfirm(char* gameObjectName, char* title, char* message, char* yes, char* no) {
        _gameObjectName = [DataConvertor charToNSString:gameObjectName];
        [IOSNativePopUpsManager ShowDialogConfirm:[DataConvertor charToNSString:title] message:[DataConvertor charToNSString:message] yesTitle:[DataConvertor charToNSString:yes] noTitle:[DataConvertor charToNSString:no]];
    }
    
    void _TAG_ShowDialogInfo(char* gameObjectName, char* title, char* message, char* ok) {
        _gameObjectName = [DataConvertor charToNSString:gameObjectName];
        [IOSNativePopUpsManager ShowDialogInfo:[DataConvertor charToNSString:title] message:[DataConvertor charToNSString:message] okTitle:[DataConvertor charToNSString:ok]];
    }
    
    void _TAG_DismissCurrentAlert() {
        [IOSNativePopUpsManager dismissCurrentAlert];
    }
}

@end
