using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static Surface.WinfairMDAPI.Struct;

namespace Surface.WinfairMDAPI
{
    class Delegate
    {
        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void lpNoParam(IntPtr AContex);

        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void lpIntParam(IntPtr AContex, int n);

        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void lpRspError(IntPtr AContex, IntPtr pRspInfo, int nRequestID, bool bIsLast);

        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void lpRsp(IntPtr AContex, IntPtr pRsp, IntPtr pRspInfo, int nRequestID, bool bIsLast);

        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void lpRtn(IntPtr AContex, IntPtr pRtn);
        public static lpRtn onquote;

        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void lpRtnEx(IntPtr AContex, IntPtr pRtn, int nCount);
        public static lpRtnEx onhistquote;
        public static lpRtnEx oninstrument;
    }
}
