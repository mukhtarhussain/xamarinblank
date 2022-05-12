using Com.Pvr.Tobservice;
using Com.Pvr.Tobservice.Enums;
using Com.Pvr.Tobservice.Interfaces;
using System;
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
                ToBServiceHelper.Instance.ServiceBinder.PbsControlSetDeviceAction(PBS_DeviceControlEnum.DeviceControlReboot, new RebootDeviceCallback());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }

    public class RebootDeviceCallback : IntCallbackStub
    {
        public override void Callback(int result)
        {
        }
    }
}