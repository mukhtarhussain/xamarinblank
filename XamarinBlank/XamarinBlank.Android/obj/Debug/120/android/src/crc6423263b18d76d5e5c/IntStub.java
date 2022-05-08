package crc6423263b18d76d5e5c;


public class IntStub
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
		mono.android.Runtime.register ("XamarinBlank.Droid.IntStub, XamarinBlank.Android", IntStub.class, __md_methods);
	}


	public IntStub ()
	{
		super ();
		if (getClass () == IntStub.class)
			mono.android.TypeManager.Activate ("XamarinBlank.Droid.IntStub, XamarinBlank.Android", "", this, new java.lang.Object[] {  });
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
