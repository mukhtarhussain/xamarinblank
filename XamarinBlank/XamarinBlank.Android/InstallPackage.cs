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
        void IInstallPackage.Install(string packageName, byte[] packageBytes)
        {
            try
            {
                var installPackagePath = Path.Combine(Android.App.Application.Context.GetExternalFilesDir("").AbsolutePath, packageName);
                File.WriteAllBytes(installPackagePath, packageBytes);
                ToBServiceHelper.Instance.ServiceBinder.PbsControlAPPManger(PBS_PackageControlEnum.PackageSilenceInstall, packageName, 0, new InstallPackageCallback());
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