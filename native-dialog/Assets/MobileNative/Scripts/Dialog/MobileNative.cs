
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace pingak9
{

    public class MobileNative
    {

#if UNITY_IPHONE
        [DllImport("__Internal")]
        private static extern void _TAG_ShowDialogNeutral(string title, string message, string accept, string neutral, string decline);

        [DllImport("__Internal")]
        private static extern void _TAG_ShowDialogConfirm(string title, string message, string yes, string no);

        [DllImport("__Internal")]
        private static extern void _TAG_ShowDialogInfo(string title, string message, string ok);

        [DllImport("__Internal")]
        private static extern void _TAG_DismissCurrentAlert();
	
        [DllImport ("__Internal")]
        private static extern void _TAG_ShowTimePicker(double unix);
	
        [DllImport ("__Internal")]
        private static extern void _TAG_ShowDatePicker(double unix);
	
        [DllImport ("__Internal")]
        private static extern void _TAG_ShowDatePickerWithRange(double firstUnixTime, double minimumUnixTime, double maximumUnixTime);

#endif

        public static void showDialogNeutral(string gameObjectName, string title, string message, string accept, string neutral, string decline)
        {
#if UNITY_EDITOR
#elif UNITY_IPHONE
            _TAG_ShowDialogNeutral(title, message, accept, neutral, decline);
#elif UNITY_ANDROID            
            AndroidJavaClass javaUnityClass = new AndroidJavaClass("com.pingak9.nativepopup.Bridge");
            javaUnityClass.CallStatic("ShowDialogNeutral", gameObjectName, title, message, accept, neutral, decline);
#endif
        }

        /// <summary>
        /// Calls a Native Confirm Dialog on iOS and Android
        /// </summary>
        /// <param name="title">Dialog title text</param>
        /// <param name="message">Dialog message text</param>
        /// <param name="yes">Accept Button text</param>
        /// <param name="no">Cancel Button text</param>
        /// <param name="cancelable">Android only. Allows setting the cancelable property of the dialog</param>
        public static void showDialogConfirm(string gameObjectName, string title, string message, string yes, string no, bool cancelable = true)
        {
#if UNITY_EDITOR
#elif UNITY_IPHONE
            _TAG_ShowDialogConfirm(title, message, yes, no);
#elif UNITY_ANDROID            
            AndroidJavaClass javaUnityClass = new AndroidJavaClass("com.pingak9.nativepopup.Bridge");
            javaUnityClass.CallStatic("ShowDialogConfirm", gameObjectName, title, message, yes, no, cancelable);
#endif
        }

        public static void showInfoPopup(string gameObjectName, string title, string message, string ok)
        {
#if UNITY_EDITOR
#elif UNITY_IPHONE
            _TAG_ShowDialogInfo(title, message, ok);
#elif UNITY_ANDROID            
            AndroidJavaClass javaUnityClass = new AndroidJavaClass("com.pingak9.nativepopup.Bridge");
            javaUnityClass.CallStatic("ShowDialogInfo", gameObjectName, title, message, ok);
#endif
        }

        public static void DismissCurrentAlert()
        {
#if UNITY_EDITOR
#elif UNITY_IPHONE
            _TAG_DismissCurrentAlert();
#elif UNITY_ANDROID
            AndroidJavaClass javaUnityClass = new AndroidJavaClass("com.pingak9.nativepopup.Bridge");
            javaUnityClass.CallStatic("DismissCurrentAlert");
#endif
        }

        public static void showDatePicker(string gameObjectName, int year, int month, int day)
        {
#if UNITY_EDITOR
#elif UNITY_IPHONE
            DateTimeOffset dateTime = new DateTimeOffset(year, month, day);
            double unix = (TimeZoneInfo.ConvertTimeToUtc(dateTime) - new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds; 
            _TAG_ShowDatePicker(unix);
#elif UNITY_ANDROID
            AndroidJavaClass javaUnityClass = new AndroidJavaClass("com.pingak9.nativepopup.Bridge");
            javaUnityClass.CallStatic("ShowDatePicker", gameObjectName, year, month, day);
#endif
        }

        public static void showDatePicker(string gameObjectName, int year, int month, int day, int firstDayOfWeek, int minYear, int minMonth, int minDay, int maxYear, int maxMonth, int maxDay, bool calendarViewShown, bool spinnerShown)
        {
#if UNITY_EDITOR
#elif UNITY_IPHONE
            var firstDateTime = new DateTimeOffset(year, month, day);
            var minDateTime = new DateTimeOffset(minYear, minMonth, minDay);
            var maxDateTime = new DateTimeOffset(maxYear, maxMonth, maxDay);
            var baseTime = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            var firstUnixTime = (TimeZoneInfo.ConvertTimeToUtc(firstDateTime) - baseTime).TotalSeconds; 
            var minUnixTime = (TimeZoneInfo.ConvertTimeToUtc(minDateTime) - baseTime).TotalSeconds; 
            var maxUnixTime = (TimeZoneInfo.ConvertTimeToUtc(maxDateTime) - baseTime).TotalSeconds; 
            _TAG_ShowDatePickerWithRange(firstUnixTime, minUnixTime, maxUnixTime);
#elif UNITY_ANDROID
            AndroidJavaClass javaUnityClass = new AndroidJavaClass("com.pingak9.nativepopup.Bridge");
            javaUnityClass.CallStatic("ShowDatePicker", gameObjectName, year, month, day, firstDayOfWeek, minYear, minMonth, minDay, maxYear, maxMonth, maxDay, calendarViewShown, spinnerShown);
#endif
        }

        public static void showTimePicker(string gameObjectName, int hour, int minute)
        {
#if UNITY_EDITOR
#elif UNITY_IPHONE
            _TAG_ShowTimePicker(0);
#elif UNITY_ANDROID
            AndroidJavaClass javaUnityClass = new AndroidJavaClass("com.pingak9.nativepopup.Bridge");
            javaUnityClass.CallStatic("ShowTimePicker", gameObjectName, hour, minute);
#endif
        }
    }
}