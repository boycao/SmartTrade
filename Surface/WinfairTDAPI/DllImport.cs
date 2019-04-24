using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static Surface.WinfairTDAPI.Delegate;
using static Surface.WinfairTDAPI.Struct;

namespace Surface.WinfairTDAPI
{
    class DllImport
    {
        ///新建Api
        [DllImport(@"C:\Users\11712\Desktop\WinfairAPILib\x86\Release\WinfairTDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr WinfairTdApi_CreateWinfairTdApi(IntPtr pszClientTag);

        ///删除Api
        [DllImport(@"C:\Users\11712\Desktop\WinfairAPILib\x86\Release\WinfairTDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void WinfairTdApi_DeleteWinfairTdApi(IntPtr hClient);

        ///设置回调函数
        [DllImport(@"C:\Users\11712\Desktop\WinfairAPILib\x86\Release\WinfairTDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void WinfairTdApi_SetTdApiCallback(IntPtr hClient, IntPtr pContex, lpOnConnectedParam pfnConnected,
                lpOnDisconnectedParam pfnDisconnected, lpOnHeartBeatWarningParam pfnHeartBeatWarning, lpOnInterruptedParam pfnInterrupted,
                lpOnRspUserLoginParam pfnRspUserLogin, lpOnRspUserLogoutParam pfnRspUserLogout, lpOnRspQryInstrumentParam pfnRspQryInstrument,
                lpOnRspQryHoldingParam pfnRspQryHolding, lpOnRspQryCashParam pfnRspQryCash, lpOnRspQryOrdersTodayParam pfnRspQryOrdersToday,
                lpOnRspQryDealsTodayParam pfnRspQryDealsToday, lpOnRtnOrderParam pfnRtnOrder, lpOnRtnDealParam pfnRtnDeal,
                lpOnRspQryHoldingParam pfnRtnHolding, lpOnRspQryCashParam pfnRtnCash, lpOnRspErrorParam pfnRspError);

        ///连接
        [DllImport(@"C:\Users\11712\Desktop\WinfairAPILib\x86\Release\WinfairTDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool WinfairTdApi_Connect(IntPtr hClient, IntPtr pszErrorMsg);

        ///断开
        [DllImport(@"C:\Users\11712\Desktop\WinfairAPILib\x86\Release\WinfairTDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool WinfairTdApi_Disconnect(IntPtr hClient, IntPtr pszErrorMsg);

        ///查询连接状态
        [DllImport(@"C:\Users\11712\Desktop\WinfairAPILib\x86\Release\WinfairTDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool WinfairTdApi_IsConnected(IntPtr hClient);

        ///请求登录
        [DllImport(@"C:\Users\11712\Desktop\WinfairAPILib\x86\Release\WinfairTDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool WinfairTdApi_ReqUserLogin(IntPtr hClient, IntPtr pszErrorMsg);

        ///请求登出
        [DllImport(@"C:\Users\11712\Desktop\WinfairAPILib\x86\Release\WinfairTDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool WinfairTdApi_ReqUserLogout(IntPtr hClient, IntPtr pszErrorMsg);

        ///请求订阅私有流
        [DllImport(@"C:\Users\11712\Desktop\WinfairAPILib\x86\Release\WinfairTDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool WinfairTdApi_ReqRegRspRtn(IntPtr hClient, int nMode, IntPtr pszErrorMsg);

        ///请求查询交易日
        [DllImport(@"C:\Users\11712\Desktop\WinfairAPILib\x86\Release\WinfairTDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr WinfairTdApi_ReqQryTradingDay(IntPtr hClient);

        ///请求查询合约
        [DllImport(@"C:\Users\11712\Desktop\WinfairAPILib\x86\Release\WinfairTDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool WinfairTdApi_ReqQryInstrument(IntPtr hClient, IntPtr pszErrorMsg, int nQueryType, IntPtr pszExchangeInstrument);

        ///请求报单录入
        [DllImport(@"C:\Users\11712\Desktop\WinfairAPILib\x86\Release\WinfairTDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool WinfairTdApi_ReqOrderInsert(IntPtr hClient, IntPtr pszExchangeID, IntPtr pszInstrumentID, double dOrderPrice, int nOrderVolume, ORDER_DIRECTION eDirection, ORDER_OFFSET eOffset, IntPtr pszAdapterTag, IntPtr pszStockPoolTag, IntPtr pszFundID, IntPtr pszCombiID, IntPtr pszIdentity, IntPtr pszLocalIntPtrID, IntPtr pszErrorMsg);

        ///请求撤单录入
        [DllImport(@"C:\Users\11712\Desktop\WinfairAPILib\x86\Release\WinfairTDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool WinfairTdApi_ReqOrderAction(IntPtr hClient, uint dwUniqueID, IntPtr pszIdentity, IntPtr pszLocalIntPtrID, IntPtr pszExchangeID, IntPtr pszOrderSysID, IntPtr pszErrorMsg);

        ///请求查询订单
        [DllImport(@"C:\Users\11712\Desktop\WinfairAPILib\x86\Release\WinfairTDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool WinfairTdApi_ReqQryOrder(IntPtr hClient, IntPtr pstOrder, IntPtr pszErrorMsg);

        ///请求查询持仓
        [DllImport(@"C:\Users\11712\Desktop\WinfairAPILib\x86\Release\WinfairTDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool WinfairTdApi_ReqQryHolding(IntPtr hClient, IntPtr pszErrorMsg);

        ///请求查询资金
        [DllImport(@"C:\Users\11712\Desktop\WinfairAPILib\x86\Release\WinfairTDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool WinfairTdApi_ReqQryCash(IntPtr hClient, IntPtr pszErrorMsg);

        ///请求查询当日委托
        [DllImport(@"C:\Users\11712\Desktop\WinfairAPILib\x86\Release\WinfairTDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool WinfairTdApi_ReqQryOrdersToday(IntPtr hClient, IntPtr pszErrorMsg);

        ///请求查询当日成交
        [DllImport(@"C:\Users\11712\Desktop\WinfairAPILib\x86\Release\WinfairTDAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool WinfairTdApi_ReqQryDealsToday(IntPtr hClient, IntPtr pszErrorMsg);
    }
}
