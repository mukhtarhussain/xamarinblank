using Android.Content;
using Android.OS;
using Com.Pvr.Tobservice;
using Com.Pvr.Tobservice.Enums;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinBlank.Droid;

[assembly: Dependency(typeof(RebootDevice))]
namespace XamarinBlank.Droid
{
    public class RebootDevice : IRebootDevice
    {
        void IRebootDevice.Reboot()
        {
            try
            {
                //Intent i = new Intent(Intent.ActionShutdown);
                //i.AddFlags(ActivityFlags.NewTask);
                //i.PutExtra("android.intent.extra.KEY_CONFIRM", true);
                //i.PutExtra(Intent.ExtraShutdownUserspaceOnly, true);
                //Android.App.Application.Context.StartActivity(i);
                //var permission = ContextCompat.CheckSelfPermission(Android.App.Application.Context, Intent.ActionReboot);
                //if (permission.HasFlag(Permission.Denied))
                //{
                //    ActivityCompat.RequestPermissions(Android.App.Application.Context, new string[] { Intent.ActionReboot }, RequestedPermission.Required);
                //    System.Diagnostics.Debug.WriteLine("Request permission");
                //}
                //else
                //{
                //PowerManager pm = (PowerManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.PowerService);
                //pm.Reboot("Reboot");
                //}
                //Java.Lang.Runtime.GetRuntime().Exec(new String[] { "/system/xbin/su", "-c", "reboot -p" });

                ToBServiceHelper.Instance.ServiceBinder.PbsControlSetDeviceAction(PBS_DeviceControlEnum.DeviceControlReboot, new IntStub());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            //Java.Lang.Runtime.GetRuntime().Exec(new string[] { "/system/xbin/su", "-c", "reboot -p" });
        }
    }
}