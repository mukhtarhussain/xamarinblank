using System;

namespace XamarinBlank
{
    public class InstallPackageEventArgs : EventArgs
    {
        public bool PackageInstalled { get; }

        public InstallPackageEventArgs(bool packageInstalled)
        {
            PackageInstalled = packageInstalled;
        }
    }
}
