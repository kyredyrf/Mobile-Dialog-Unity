
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace pingak9
{

    public class MobileNative
    {
#if UNITY_IOS
        [DllImport("__Internal")]
        private static extern void _TAG_ShowDialogNeutral(string gameObjectName, string title, string message, string accept, string neutral, string decline);

        [DllImport("__Internal")]
        private static extern void _TAG_ShowDialogConfirm(string gameObjectName, string title, string message, string yes, string no);

        [DllImport("__Internal")]
        private static extern void _TAG_ShowDialogInfo(string gameObjectName, string title, string message, string ok);

        [DllImport("__Internal")]
        private static extern void _TAG_DismissCurrentAlert();
	
        [DllImport ("__Internal")]
        private static extern void _TAG_ShowTimePicker(string gameObjectName, double unix);
	
        [DllImport ("__Internal")]
        private static extern void _TAG_ShowDatePicker(string gameObjectName, double unix);
	
        [DllImport ("__Internal")]
        private static extern void _TAG_ShowDatePickerWithRange(string gameObjectName, double firstUnixTime, double minimumUnixTime, double maximumUnixTime);
#elif UNITY_ANDROID
        static AndroidJavaClass JavaUnityClass => new AndroidJavaClass("com.pingak9.nativepopup.Bridge");
#endif

        public static void showDialogNeutral(string gameObjectName, string title, string message, string accept, string neutral, string decline)
        {
#if UNITY_EDITOR
            Debug.LogWarning("unsupported");
#elif UNITY_IOS
            _TAG_ShowDialogNeutral(gameObjectName, title, message, accept, neutral, decline);
#elif UNITY_ANDROID
            JavaUnityClass.CallStatic("ShowDialogNeutral", gameObjectName, title, message, accept, neutral, decline);
#else
            Debug.LogWarning("unsupported");
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
            Debug.LogWarning("unsupported");
#elif UNITY_IOS
            _TAG_ShowDialogConfirm(gameObjectName, title, message, yes, no);
#elif UNITY_ANDROID
            JavaUnityClass.CallStatic("ShowDialogConfirm", gameObjectName, title, message, yes, no, cancelable);
#else
            Debug.LogWarning("unsupported");
#endif
        }

        public static void showInfoPopup(string gameObjectName, string title, string message, string ok)
        {
#if UNITY_EDITOR
            Debug.LogWarning("unsupported");
#elif UNITY_IOS
            _TAG_ShowDialogInfo(gameObjectName, title, message, ok);
#elif UNITY_ANDROID
            JavaUnityClass.CallStatic("ShowDialogInfo", gameObjectName, title, message, ok);
#else
            Debug.LogWarning("unsupported");
#endif
        }

        public static void DismissCurrentAlert()
        {
#if UNITY_EDITOR
            Debug.LogWarning("unsupported");
#elif UNITY_IOS
            _TAG_DismissCurrentAlert();
#elif UNITY_ANDROID
            JavaUnityClass.CallStatic("DismissCurrentAlert");
#else
            Debug.LogWarning("unsupported");
#endif
        }

        public static void showDatePicker(string gameObjectName, DateTimeOffset firstDate)
        {
#if UNITY_EDITOR
            Debug.LogWarning("unsupported");
#elif UNITY_IOS
            var baseTime = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var firstUnixTime = (firstDate.ToUniversalTime() - baseTime).TotalSeconds; 
            _TAG_ShowDatePicker(gameObjectName, firstUnixTime);
#elif UNITY_ANDROID
            JavaUnityClass.CallStatic("ShowDatePicker", gameObjectName, firstDate.Year, firstDate.Month, firstDate.Day);
#else
            Debug.LogWarning("unsupported");
#endif
        }

        public static void showDatePicker(string gameObjectName, DateTimeOffset firstDate, DateTimeOffset minDate, DateTimeOffset maxDate)
        {
#if UNITY_EDITOR
            Debug.LogWarning("unsupported");
#elif UNITY_IOS
            var baseTime = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var firstUnixTime = (firstDate.ToUniversalTime() - baseTime).TotalSeconds; 
            var minUnixTime = (minDate.ToUniversalTime() - baseTime).TotalSeconds; 
            var maxUnixTime = (maxDate.ToUniversalTime() - baseTime).TotalSeconds; 
            _TAG_ShowDatePickerWithRange(gameObjectName, firstUnixTime, minUnixTime, maxUnixTime);
#elif UNITY_ANDROID
            JavaUnityClass.CallStatic("ShowDatePicker", gameObjectName,
                firstDate.Year, firstDate.Month, firstDate.Day,
                minDate.Year, minDate.Month, minDate.Day,
                maxDate.Year, maxDate.Month, maxDate.Day);
#else
            Debug.LogWarning("unsupported");
#endif
        }

        public static void showDatePicker(string gameObjectName, DateTimeOffset firstDate, DateTimeOffset minDate, DateTimeOffset maxDate, int firstDayOfWeek, bool calendarViewShown, bool spinnerShown)
        {
#if UNITY_EDITOR
            Debug.LogWarning("unsupported");
#elif UNITY_IOS
            Debug.LogWarning("unsupported");
#elif UNITY_ANDROID
            JavaUnityClass.CallStatic("ShowDatePicker", gameObjectName,
                firstDate.Year, firstDate.Month, firstDate.Day,
                minDate.Year, minDate.Month, minDate.Day,
                maxDate.Year, maxDate.Month, maxDate.Day,
                firstDayOfWeek, calendarViewShown, spinnerShown);
#else
            Debug.LogWarning("unsupported");
#endif
        }

        public static void showTimePicker(string gameObjectName, DateTimeOffset firstDate)
        {
#if UNITY_EDITOR
            Debug.LogWarning("unsupported");
#elif UNITY_IOS
            var baseTime = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var firstUnixTime = (firstDate.ToUniversalTime() - baseTime).TotalSeconds; 
            _TAG_ShowTimePicker(gameObjectName, firstUnixTime);
#elif UNITY_ANDROID
            JavaUnityClass.CallStatic("ShowTimePicker", gameObjectName, dateTime.Hour, dateTime.Minute);
#else
            Debug.LogWarning("unsupported");
#endif
        }
    }
}