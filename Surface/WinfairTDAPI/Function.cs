using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static Surface.Form1;
using static Surface.WinfairTDAPI.Callback;
using static Surface.WinfairTDAPI.Delegate;
using static Surface.WinfairTDAPI.DllImport;
using static Surface.WinfairTDAPI.Struct;

namespace Surface.WinfairTDAPI
{
    class Function
    {
        public static void TDCreate()
        {
            if (g_hTDClient == IntPtr.Zero)
            {
                onconnected = new lpOnConnectedParam(OnConnected);
                ondisconnected = new lpOnDisconnectedParam(OnDisconnected);
                onheartbeatwarning = new lpOnHeartBeatWarningParam(OnHeartBeatWarning);
                oninterrupted = new lpOnInterruptedParam(OnInterrupted);
                onrspuserlogin = new lpOnRspUserLoginParam(OnRspUserLogin);
                onrspuserlogout = new lpOnRspUserLogoutParam(OnRspUserLogout);
                onrspqryinstrument = new lpOnRspQryInstrumentParam(OnRspQryInstrument);
                onrspqryholding = new lpOnRspQryHoldingParam(OnRspQryHolding);
                onrspqrycash = new lpOnRspQryCashParam(OnRspQryCash);
                onrspqryorderstoday = new lpOnRspQryOrdersTodayParam(OnRspQryOrdersToday);
                onrspqrydealstoday = new lpOnRspQryDealsTodayParam(OnRspQryDealsToday);
                onrtnorder = new lpOnRtnOrderParam(OnRtnOrder);
                onrtndeal = new lpOnRtnDealParam(OnRtnDeal);
                onrtnholding = new lpOnRspQryHoldingParam(OnRtnHolding);
                onrtncash = new lpOnRspQryCashParam(OnRtnCash);
                onrsperror = new lpOnRspErrorParam(OnRspError);

                g_hTDClient = WinfairTdApi_CreateWinfairTdApi(Marshal.StringToHGlobalAnsi("MyClient"));
                WinfairTdApi_SetTdApiCallback(g_hTDClient, IntPtr.Zero, onconnected, ondisconnected, onheartbeatwarning, oninterrupted, onrspuserlogin, onrspuserlogout, onrspqryinstrument, onrspqryholding, onrspqrycash, onrspqryorderstoday, onrspqrydealstoday, onrtnorder, onrtndeal, onrtnholding, onrtncash, onrsperror);
                if (g_hTDClient != IntPtr.Zero)
                {
                    Str_log = Str_log + "\nAPI Created";
                }
            }
            else
            {
               // Console.WriteLine("API Already Created");
            }
        }

        public static void TDConnect()
        {
            IntPtr pszErrMsg = Marshal.AllocHGlobal(256);
            if (g_hTDClient != IntPtr.Zero)
            {
                WinfairTdApi_Connect(g_hTDClient, pszErrMsg);
             //   Console.WriteLine(Marshal.PtrToStringAnsi(pszErrMsg));
            }
        }

        public static void TDDisconnect()
        {
            IntPtr pszErrMsg = Marshal.AllocHGlobal(256);
            if (g_hTDClient != IntPtr.Zero)
            {
                WinfairTdApi_Disconnect(g_hTDClient, pszErrMsg);
                Console.WriteLine(Marshal.PtrToStringAnsi(pszErrMsg));
            }
        }

        public static void TDQryInst()
        {
            IntPtr pszErrMsg = Marshal.AllocHGlobal(256);
            if (g_hTDClient != IntPtr.Zero)
            {
                WinfairTdApi_ReqQryInstrument(g_hTDClient, pszErrMsg, 1, Marshal.StringToHGlobalAnsi("SSE"));
                WinfairTdApi_ReqQryInstrument(g_hTDClient, pszErrMsg, 1, Marshal.StringToHGlobalAnsi("SZE"));
                Console.WriteLine(Marshal.PtrToStringAnsi(pszErrMsg));
            }
        }

        public static void TDQryCash()
        {
            IntPtr pszErrMsg = Marshal.AllocHGlobal(256);
            if (g_hTDClient != IntPtr.Zero)
            {
                WinfairTdApi_ReqQryCash(g_hTDClient, pszErrMsg);
                Console.WriteLine(Marshal.PtrToStringAnsi(pszErrMsg));
            }
        }

        public static void TDQryHolding()
        {
            IntPtr pszErrMsg = Marshal.AllocHGlobal(256);
            if (g_hTDClient != IntPtr.Zero)
            {
                WinfairTdApi_ReqQryHolding(g_hTDClient, pszErrMsg);
                Console.WriteLine(Marshal.PtrToStringAnsi(pszErrMsg));
            }
        }

        public static string TDOrderInsert(string csExchange, string csCode, double dPrice, int nVolume, ORDER_DIRECTION eDirection, ORDER_OFFSET eOffset)
        {
            nVolume *= 100;
            IntPtr szErrMsg = Marshal.AllocHGlobal(2560);
            IntPtr szIdentity = Marshal.AllocHGlobal(256);
            IntPtr szLocalHandleID = Marshal.AllocHGlobal(256);
            if (g_hTDClient != IntPtr.Zero)
            {
                if (csExchange != "" && csCode != "" && eDirection != ORDER_DIRECTION.UNKNOWNDIRECTION && eOffset != ORDER_OFFSET.UNKNOWNOFFSET && nVolume > 0)
                {
                    if (WinfairTdApi_ReqOrderInsert(g_hTDClient, Marshal.StringToHGlobalAnsi(csExchange), Marshal.StringToHGlobalAnsi(csCode), dPrice, nVolume, eDirection, eOffset, Marshal.StringToHGlobalAnsi(""), Marshal.StringToHGlobalAnsi(""), Marshal.StringToHGlobalAnsi(""), Marshal.StringToHGlobalAnsi(""), szIdentity, szLocalHandleID, szErrMsg))
                    {
                        Console.WriteLine(Marshal.PtrToStringAnsi(szIdentity));
                        Console.WriteLine(Marshal.PtrToStringAnsi(szLocalHandleID));
                        Console.WriteLine(Marshal.PtrToStringAnsi(szErrMsg));
                        string sszLocalHandleID = Marshal.PtrToStringAnsi(szLocalHandleID);
                        return sszLocalHandleID;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Params");
                }
            }
            return "";
        }

        public static void TDOrderAction(string csLocalHandleID)
        {
            string csIdentity = "";
            uint dwUniqueID = 0;
            IntPtr pszErrMsg = Marshal.AllocHGlobal(256);
            if (g_hTDClient != IntPtr.Zero && (dwUniqueID > 0 || csIdentity != "" && csLocalHandleID != ""))
            {
                WinfairTdApi_ReqOrderAction(g_hTDClient, dwUniqueID, Marshal.StringToHGlobalAnsi(csIdentity), Marshal.StringToHGlobalAnsi(csLocalHandleID), IntPtr.Zero, IntPtr.Zero, pszErrMsg);
                Console.WriteLine(Marshal.PtrToStringAnsi(pszErrMsg));
            }
        }

        public static void TDQryOrder()
        {
            string csIdentity = "";
            string csLocalHandleID = "";
            uint dwUniqueID = 0;
            IntPtr pszErrMsg = Marshal.AllocHGlobal(256);
            if (g_hTDClient != IntPtr.Zero && (dwUniqueID > 0 || csIdentity != "" && csLocalHandleID != ""))
            {
                tagSTRUCT_ORDER_FIXED stOrder = new tagSTRUCT_ORDER_FIXED();
                stOrder.dwUniqueID = dwUniqueID;
                stOrder.szIdentity = csIdentity;
                stOrder.szLocalHandleID = csLocalHandleID;
                IntPtr pstOrder = Marshal.AllocHGlobal(256);
                Marshal.StructureToPtr(stOrder, pstOrder, true);
                WinfairTdApi_ReqQryOrder(g_hTDClient, pstOrder, pszErrMsg);
                Console.WriteLine(Marshal.PtrToStringAnsi(pszErrMsg));
            }
        }

        public static void TDQryOrdersToday()
        {
            IntPtr pszErrMsg = Marshal.AllocHGlobal(256);
            if (g_hTDClient != IntPtr.Zero)
            {
                WinfairTdApi_ReqQryOrdersToday(g_hTDClient, pszErrMsg);
                Console.WriteLine(Marshal.PtrToStringAnsi(pszErrMsg));
            }
        }

        public static void TDQryDealsToday()
        {
            IntPtr pszErrMsg = Marshal.AllocHGlobal(256);
            if (g_hTDClient != IntPtr.Zero)
            {
                WinfairTdApi_ReqQryDealsToday(g_hTDClient, pszErrMsg);
                Console.WriteLine(Marshal.PtrToStringAnsi(pszErrMsg));
            }
        }
    }
}
