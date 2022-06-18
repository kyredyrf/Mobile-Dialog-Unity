using UnityEngine;
using System.Collections;
using System;

namespace pingak9
{
    public class MobileDialogConfirm : MonoBehaviour
    {
        #region PUBLIC_VARIABLES

        public Action yesAction;
        public Action noAction;
        public string title;
        public string message;
        public string yes;
        public string no;
        public string urlString;

        #endregion

        #region PUBLIC_FUNCTIONS

        // Constructor
        public static MobileDialogConfirm Create(string title, string message, string yes, string no, Action yesAction, Action noAction)
        {
            var gameObjectName = "MobileDialogConfirm";
            var dialog = new GameObject(gameObjectName).AddComponent<MobileDialogConfirm>();
            dialog.title = title;
            dialog.message = message;
            dialog.yes = yes;
            dialog.no = no;
            dialog.yesAction = yesAction;
            dialog.noAction = noAction;
            dialog.init(gameObjectName);
            return dialog;
        }

        public void init(string gameObjectName)
        {
            MobileNative.showDialogConfirm(gameObjectName, title, message, yes, no);
        }

        #endregion

        #region IOS_EVENT_LISTENER

        public void OnYesCallBack(string message)
        {
            if (yesAction != null)
            {
                yesAction();
            }
            Destroy(gameObject);
        }
        public void OnNoCallBack(string message)
        {
            if (noAction != null)
            {
                noAction();
            }
            Destroy(gameObject);
        }
        #endregion
    }
}