using Com.Pvr.Tobservice;
using Com.Pvr.Tobservice.Enums;
using Com.Pvr.Tobservice.Interfaces;
using System;
using System.IO;
using Xamarin.Forms;
using XamarinBlank.Droid;

[assembly: Dependency(typeof(InstallPackage))]
namespace XamarinBlank.Droid
{
    public class InstallPackage : IInstallPackage
    {
        void IInstallPackage.Install(string filePath) //byte[] installPackageBytes)
        {
            try
            {
                //var installPackagePath = Path.Combine(Android.OS.Environment.DataDirectory.AbsolutePath, DateTime.Now.ToString("yyyyMMddHHmmss.apk"));
                ToBServiceHelper.Instance.ServiceBinder.PbsControlAPPManger(PBS_PackageControlEnum.PackageSilenceInstall, filePath, 0, new InstallPackageCallback());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }

    public class InstallPackageCallback : IntCallbackStub
    {
        public override void Callback(int result)
        {
            System.Diagnostics.Debug.WriteLine($"Result: {result}");
        }
    }
}