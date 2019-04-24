using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Surface.WinfairTDAPI
{
    class Delegate
    {
        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void lpOnConnectedParam(IntPtr pContex, bool bSuccess, IntPtr pszUsername, IntPtr pszIdentity, IntPtr pszErrorMsg);
        public static lpOnConnectedParam onconnected;

        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void lpOnDisconnectedParam(IntPtr pContex, IntPtr pszUsername, IntPtr pszIdentity, IntPtr pszErrorMsg);
        public static lpOnDisconnectedParam ondisconnected;

        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void lpOnHeartBeatWarningParam(IntPtr pContex, IntPtr pszCmdAddress, IntPtr pszBrdAddress, IntPtr pszIdentity);
        public static lpOnHeartBeatWarningParam onheartbeatwarning;

        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void lpOnInterruptedParam(IntPtr pContex, IntPtr pszUsername, IntPtr pszIdentity);
        public static lpOnInterruptedParam oninterrupted;

        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void lpOnRspUserLoginParam(IntPtr pContex, bool bSuccess, IntPtr pszTraingDay, IntPtr pszUsername, IntPtr pszIdentity, IntPtr pszErrorMsg);
        public static lpOnRspUserLoginParam onrspuserlogin;

        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void lpOnRspUserLogoutParam(IntPtr pContex, IntPtr pszUsername, IntPtr pszIdentity, IntPtr pszErrorMsg);
        public static lpOnRspUserLogoutParam onrspuserlogout;

        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void lpOnRspQryInstrumentParam(IntPtr pContex, bool bSuccess, IntPtr pInstruments, int nCount, IntPtr pszErrorMsg);
        public static lpOnRspQryInstrumentParam onrspqryinstrument;

        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void lpOnRspQryHoldingParam(IntPtr pContex, bool bSuccess, IntPtr pHoldings, int nCount, int nOperationSeq, IntPtr pszErrorMsg);
        public static lpOnRspQryHoldingParam onrspqryholding;
        public static lpOnRspQryHoldingParam onrtnholding;

        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void lpOnRspQryCashParam(IntPtr pContex, bool bSuccess, IntPtr pCashes, int nCount, int nOperationSeq, IntPtr pszErrorMsg);
        public static lpOnRspQryCashParam onrspqrycash;
        public static lpOnRspQryCashParam onrtncash;

        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void lpOnRspQryOrdersTodayParam(IntPtr pContex, bool bSuccess, IntPtr pOrders, int nCount, int nOperationSeq, IntPtr pszErrorMsg);
        public static lpOnRspQryOrdersTodayParam onrspqryorderstoday;

        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void lpOnRspQryDealsTodayParam(IntPtr pContex, bool bSuccess, IntPtr pDeals, int nCount, int nOperationSeq, IntPtr pszErrorMsg);
        public static lpOnRspQryDealsTodayParam onrspqrydealstoday;

        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void lpOnRtnOrderParam(IntPtr pContex, bool bSuccess, IntPtr pOrder, bool bCurrentConnection, IntPtr pszErrorMsg);
        public static lpOnRtnOrderParam onrtnorder;

        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void lpOnRtnDealParam(IntPtr pContex, bool bSuccess, IntPtr pDeal, bool bCurrentConnection, IntPtr pszErrorMsg);
        public static lpOnRtnDealParam onrtndeal;

        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void lpOnRspErrorParam(IntPtr pContex, uint dwUniqueID, uint nErrorClass, uint nErrorID, IntPtr pszErrorInfo, bool bCurrentConnection);
        public static lpOnRspErrorParam onrsperror;
    }
}
