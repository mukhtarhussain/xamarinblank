package crc6423263b18d76d5e5c;


public class InstallPackageCallback
	extends com.pvr.tobservice.interfaces.IIntCallback.Stub
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_callback:(I)V:GetCallback_IHandler\n" +
			"";
		mono.android.Runtime.register ("XamarinBlank.Droid.InstallPackageCallback, XamarinBlank.Android", InstallPackageCallback.class, __md_methods);
	}


	public InstallPackageCallback ()
	{
		super ();
		if (getClass () == InstallPackageCallback.class)
			mono.android.TypeManager.Activate ("XamarinBlank.Droid.InstallPackageCallback, XamarinBlank.Android", "", this, new java.lang.Object[] {  });
	}


	public void callback (int p0)
	{
		n_callback (p0);
	}

	private native void n_callback (int p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
