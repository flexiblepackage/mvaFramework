using System.Runtime.InteropServices;
using System;


public class waitSingleObject
{
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
}
