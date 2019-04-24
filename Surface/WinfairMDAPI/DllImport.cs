using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static Surface.WinfairMDAPI.Delegate;

namespace Surface.WinfairMDAPI
{
    class DllImport
    {
        [DllImport(@"C:\Users\11712\Desktop\CitiCup\WinfairMDAPIDemo\Release\WinfairMDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void CreateFtdcMdApi();

        [DllImport(@"C:\Users\11712\Desktop\CitiCup\WinfairMDAPIDemo\Release\WinfairMDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetMarketApiCallback(IntPtr AContex, lpNoParam AOnFrontConnected, lpIntParam AOnFrontDisconnected, lpIntParam AOnHeartBeatWarning, lpRspError AOnRspError, lpRsp AOnRspUserLogin, lpRsp AOnRspUserLogout, lpRtn AOnRtnDepthMarketData, lpRsp AOnRspSubMarketData, lpRsp AOnRspUnSubMarketData, lpRtnEx AOnRspQryHistQuote, lpRtnEx AOnRspQryInstrument);

        [DllImport(@"C:\Users\11712\Desktop\CitiCup\WinfairMDAPIDemo\Release\WinfairMDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static long ReqUserLogin(IntPtr pReqUserLoginField, int nRequestID);

        [DllImport(@"C:\Users\11712\Desktop\CitiCup\WinfairMDAPIDemo\Release\WinfairMDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static int ReqQryHistQuote(IntPtr pszInstrumentID, IntPtr pszExchangeID, int nStartDate, int nStartTime, int nEndDate, int nEndTime);

        [DllImport(@"C:\Users\11712\Desktop\CitiCup\WinfairMDAPIDemo\Release\WinfairMDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static int ReqQryInstrument(IntPtr pQryInstrument, int nRequestID);

        [DllImport(@"C:\Users\11712\Desktop\CitiCup\WinfairMDAPIDemo\Release\WinfairMDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static long SubscribeMarketData(IntPtr ppInstrumentID, int nCount, IntPtr pExchageID);
    }
}
