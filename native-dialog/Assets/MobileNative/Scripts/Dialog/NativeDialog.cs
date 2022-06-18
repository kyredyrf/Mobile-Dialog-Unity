using UnityEngine;
using System.Collections;
using System;

namespace pingak9
{
    public class NativeDialog
    {
        public NativeDialog() { }

        public static void OpenDialog(string title, string message, string ok = "Ok", Action okAction = null)
        {
            MobileDialogInfo.Create(title, message, ok, okAction);
        }
        public static void OpenDialog(string title, string message, string yes, string no, Action yesAction = null, Action noAction = null)
        {
            MobileDialogConfirm.Create(title, message, yes, no, yesAction, noAction);
        }
        public static void OpenDialog(string title, string message, string accept, string neutral, string decline, Action acceptAction = null, Action neutralAction = null, Action declineAction = null)
        {
            MobileDialogNeutral.Create(title, message, accept, neutral, decline, acceptAction, neutralAction, declineAction);
        }
        public static void OpenDatePicker(DateTimeOffset firstDate, DateTimeOffset minDate, DateTimeOffset maxDate, Action<DateTimeOffset> onChange = null, Action<DateTimeOffset> onClose = null)
        {
            MobileDateTimePicker.CreateDate(firstDate, minDate, maxDate, onChange, onClose);
        }
        public static void OpenTimePicker(DateTimeOffset firstTime, Action<DateTimeOffset> onChange = null, Action<DateTimeOffset> onClose = null)
        {
            MobileDateTimePicker.CreateTime(firstTime, onChange, onClose);
        }
    }
}