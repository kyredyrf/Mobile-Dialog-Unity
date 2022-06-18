package com.pingak9.nativepopup;

import android.app.AlertDialog;
import android.app.DatePickerDialog;
import android.app.TimePickerDialog;
import android.content.DialogInterface;
import android.widget.DatePicker;
import android.widget.TimePicker;

import com.unity3d.player.UnityPlayer;

import java.sql.Time;
import java.util.Calendar;
import java.util.Date;

/**
 * Created by PingAK9
 */
public class Bridge {

    static AlertDialog alertDialog;
    public static void ShowDialogNeutral(String title, String message, String accept, String neutral, String decline) {
        DismissCurrentAlert();
        alertDialog = new AlertDialog.Builder(UnityPlayer.currentActivity).create(); //Read Update
        alertDialog.setTitle(title);
        alertDialog.setMessage(message);

        alertDialog.setButton(accept, new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
                UnityPlayer.UnitySendMessage("MobileDialogNeutral", "OnAcceptCallBack", "0");
            }
        });
        alertDialog.setButton2(neutral, new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
                UnityPlayer.UnitySendMessage("MobileDialogNeutral", "OnNeutralCallBack", "1");
            }
        });
        alertDialog.setButton3(decline, new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
                UnityPlayer.UnitySendMessage("MobileDialogNeutral", "OnDeclineCallBack", "2");
            }
        });
        alertDialog.show();
    }

    private static AlertDialog CreateDialogConfirm(String title, String message, String yes, String no) {
        AlertDialog dialog = new AlertDialog.Builder(UnityPlayer.currentActivity).create(); //Read Update
        dialog.setTitle(title);
        dialog.setMessage(message);

        dialog.setButton(yes, new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
                UnityPlayer.UnitySendMessage("MobileDialogConfirm", "OnYesCallBack", "0");
            }
        });
        dialog.setButton2(no, new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
                UnityPlayer.UnitySendMessage("MobileDialogConfirm", "OnNoCallBack", "1");
            }
        });

        return dialog;
    }

    public static void ShowDialogConfirm(String title, String message, String yes, String no) {
        DismissCurrentAlert();

        alertDialog = CreateDialogConfirm(title, message, yes, no);

        alertDialog.show();
    }

    // Method overload for supporting non cancelable dialog
    public static void ShowDialogConfirm(String title, String message, String yes, String no, boolean cancelable) {
        DismissCurrentAlert();

        alertDialog = CreateDialogConfirm(title, message, yes, no);
        alertDialog.setCancelable((cancelable));

        alertDialog.show();
    }

    public static void ShowDialogInfo(String title, String message, String ok) {
        DismissCurrentAlert();
        alertDialog = new AlertDialog.Builder(UnityPlayer.currentActivity).create(); //Read Update
        alertDialog.setTitle(title);
        alertDialog.setMessage(message);

        alertDialog.setButton(ok, new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
                UnityPlayer.UnitySendMessage("MobileDialogInfo", "OnOkCallBack", "0");
            }
        });


        alertDialog.show();
    }

    public static void DismissCurrentAlert() {
        if (alertDialog != null)
            alertDialog.hide();
    }
    public static void ShowDatePicker(int year, int month, int day) {
        DatePickerDialog datePickerDialog = new DatePickerDialog(UnityPlayer.currentActivity,
                new DatePickerDialog.OnDateSetListener() {

                    @Override
                    public void onDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth) {

                        String s = String.format("%d-%d-%d %d:%d:%d", year, monthOfYear+1, dayOfMonth,0,0,0);
                        UnityPlayer.UnitySendMessage("MobileDateTimePicker", "PickerClosedEvent", s);
                    }
                },
                year, month - 1, day);
        datePickerDialog.show();
    }

    public static void ShowDatePicker(int year, int month, int day, int firstDayOfWeek, int minYear, int minMonth, int minDay, int maxYear, int maxMonth, int maxDay, boolean calendarViewShown, boolean spinnerShown) {
        DatePickerDialog datePickerDialog = new DatePickerDialog(UnityPlayer.currentActivity,
                new DatePickerDialog.OnDateSetListener() {

                    @Override
                    public void onDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth) {

                        String s = String.format("%d-%d-%d %d:%d:%d", year, monthOfYear+1, dayOfMonth,0,0,0);
                        UnityPlayer.UnitySendMessage("MobileDateTimePicker", "PickerClosedEvent", s);
                    }
                },
                year, month - 1, day);

        Calendar minCalendar = Calendar.getInstance();
        minCalendar.set(minYear, minMonth - 1, minDay);
        minCalendar.set(Calendar.HOUR_OF_DAY, minCalendar.getMinimum(Calendar.HOUR_OF_DAY));
        minCalendar.set(Calendar.MINUTE, minCalendar.getMinimum(Calendar.MINUTE));
        minCalendar.set(Calendar.SECOND, minCalendar.getMinimum(Calendar.SECOND));
        minCalendar.set(Calendar.MILLISECOND, minCalendar.getMinimum(Calendar.MILLISECOND));

        Calendar maxCalendar = Calendar.getInstance();
        maxCalendar.set(maxYear, maxMonth - 1, maxDay);
        maxCalendar.set(Calendar.HOUR_OF_DAY, maxCalendar.getMaximum(Calendar.HOUR_OF_DAY));
        maxCalendar.set(Calendar.MINUTE, maxCalendar.getMaximum(Calendar.MINUTE));
        maxCalendar.set(Calendar.SECOND, maxCalendar.getMaximum(Calendar.SECOND));
        maxCalendar.set(Calendar.MILLISECOND, maxCalendar.getMaximum(Calendar.MILLISECOND));

        DatePicker datePicker = datePickerDialog.getDatePicker();
        datePicker.setFirstDayOfWeek(firstDayOfWeek);
        datePicker.setMinDate(minCalendar.getTimeInMillis());
        datePicker.setMaxDate(maxCalendar.getTimeInMillis());
        datePicker.setCalendarViewShown(calendarViewShown);
        datePicker.setSpinnersShown(spinnerShown);

        datePickerDialog.show();
    }

    public static void ShowTimePicker(int hour, int minute) {
        final Calendar c = Calendar.getInstance();

        // Create a new instance of TimePickerDialog and return it
        TimePickerDialog timePickerDialog = new TimePickerDialog(UnityPlayer.currentActivity,
                new TimePickerDialog.OnTimeSetListener() {
                    @Override
                    public void onTimeSet(TimePicker view, int hourOfDay, int minute) {
                        int yeah = c.get(Calendar.YEAR);
                        int day = c.get(Calendar.DAY_OF_MONTH);
                        int month = c.get(Calendar.MONTH);
                        Calendar c = Calendar.getInstance();
                        String s = String.format("%d-%d-%d %d:%d:%d",yeah,month +1,day, hourOfDay, minute, 0);
                        UnityPlayer.UnitySendMessage("MobileDateTimePicker", "PickerClosedEvent", s);
                    }
                }, hour, minute, true);
        timePickerDialog.show();

    }
}
