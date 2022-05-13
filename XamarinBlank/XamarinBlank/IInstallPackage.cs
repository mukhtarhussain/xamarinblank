namespace XamarinBlank
{
    public interface IInstallPackage
    {
        void Install(string packageName, byte[] packageBytes);
    }
}
