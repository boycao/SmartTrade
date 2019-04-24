using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static Surface.Form1;
using static Surface.WinfairTDAPI.DllImport;
using static Surface.WinfairTDAPI.Struct;
using static Surface.MyLogic.Logic;

namespace Surface.WinfairTDAPI
{
    class Callback
    {
        public static void OnConnected(IntPtr pContex, bool bSuccess, IntPtr pszUsername, IntPtr pszIdentity, IntPtr pszErrorMsg)
        {
            //Console.WriteLine("OnConnected: " + Marshal.PtrToStringAnsi(pszErrorMsg));
            Sys_log = Sys_log + "\n" + DateTime.Now.ToString() + ": 正在连接：" + Marshal.PtrToStringAnsi(pszErrorMsg);
        }

        public static void OnDisconnected(IntPtr pContex, IntPtr pszUsername, IntPtr pszIdentity, IntPtr pszErrorMsg)
        {
            //Console.WriteLine("OnDisconnected: " + Marshal.PtrToStringAnsi(pszErrorMsg));
            Sys_log = Sys_log + "\n" + DateTime.Now.ToString() + ": 正在断开连接：" + Marshal.PtrToStringAnsi(pszErrorMsg);
        }

        public static void OnHeartBeatWarning(IntPtr pContex, IntPtr pszCmdAddress, IntPtr pszBrdAddress, IntPtr pszIdentity)
        {
            //Console.WriteLine("OnHeartBeatWarning");
        }

        public static void OnInterrupted(IntPtr pContex, IntPtr pszUsername, IntPtr pszIdentity)
        {
            //Console.WriteLine("OnInterrupted");
        }

        public static void OnRspUserLogin(IntPtr pContex, bool bSuccess, IntPtr pszTraingDay, IntPtr pszUsername, IntPtr pszIdentity, IntPtr pszErrorMsg)
        {
            //Console.WriteLine("OnRspUserLogin: " + Marshal.PtrToStringAnsi(pszErrorMsg));
            Sys_log = Sys_log + "\n" + DateTime.Now.ToString() + ": 正在登录：" + Marshal.PtrToStringAnsi(pszErrorMsg);
            IntPtr szErrMsg = Marshal.AllocHGlobal(256); ;
            WinfairTdApi_ReqRegRspRtn(g_hTDClient, 1, szErrMsg);
            //Console.WriteLine("ReqRegRspRtn: " + Marshal.PtrToStringAnsi(szErrMsg));
            Sys_log = Sys_log + "\n" + DateTime.Now.ToString() + ": 登录状态：" + Marshal.PtrToStringAnsi(pszErrorMsg);
        }

        public static void OnRspUserLogout(IntPtr pContex, IntPtr pszUsername, IntPtr pszIdentity, IntPtr pszErrorMsg)
        {
            //Console.WriteLine("OnRspUserLogout: " + Marshal.PtrToStringAnsi(pszErrorMsg));
            Sys_log = Sys_log + "\n" + DateTime.Now.ToString() + ": 正在登出：" + Marshal.PtrToStringAnsi(pszErrorMsg);
        }

        public static void OnRspQryInstrument(IntPtr pContex, bool bSuccess, IntPtr pInstruments, int nCount, IntPtr pszErrorMsg)
        {
            //Console.WriteLine("OnRspQryInstrument: " + Marshal.PtrToStringAnsi(pszErrorMsg));
            Sys_log = Sys_log + "\n" + DateTime.Now.ToString() + ": 正在查询股票：" + Marshal.PtrToStringAnsi(pszErrorMsg);
            tagSTRUCT_INSTRUMENT[] Instruments = new tagSTRUCT_INSTRUMENT[nCount];
            for (int i = 0; i < nCount; i++)
            {
                IntPtr pinstruments = (IntPtr)((UInt32)pInstruments + i * Marshal.SizeOf(typeof(tagSTRUCT_INSTRUMENT)));
                Instruments[i] = (tagSTRUCT_INSTRUMENT)Marshal.PtrToStructure(pinstruments, typeof(tagSTRUCT_INSTRUMENT));
                //Console.WriteLine((i + 1).ToString() + "\t" + Instruments[i].ExchangeID + "." + Instruments[i].InstrumentID + "\t" + Instruments[i].InstrumentName);
            }
        }

        public static void OnRspQryHolding(IntPtr pContex, bool bSuccess, IntPtr pHoldings, int nCount, int nOperationSeq, IntPtr pszErrorMsg)
        {
            //Console.WriteLine("OnRspQryHolding: " + Marshal.PtrToStringAnsi(pszErrorMsg));
            Sys_log = Sys_log + "\n" + DateTime.Now.ToString() + ": 正在查询持仓：" + Marshal.PtrToStringAnsi(pszErrorMsg);
            tagSTRUCT_HOLDING_FIXED[] Holdings = new tagSTRUCT_HOLDING_FIXED[nCount];
            for (int i = 0; i < nCount; i++)
            {
                IntPtr pholdings = (IntPtr)((UInt32)pHoldings + i * Marshal.SizeOf(typeof(tagSTRUCT_HOLDING_FIXED)));
                Holdings[i] = (tagSTRUCT_HOLDING_FIXED)Marshal.PtrToStructure(pholdings, typeof(tagSTRUCT_HOLDING_FIXED));
                //Console.WriteLine((i + 1).ToString() + "\t" + Holdings[i].szExchangeID + "." + Holdings[i].szCode + "\t" + Holdings[i].szName + "\tTotal Vol: " + Holdings[i].nTotalVolume.ToString() + "\tFrozen Vol: " + Holdings[i].nSellFrozenVol.ToString() + "\tAvailable Vol: " + Holdings[i].nAvailableVolume.ToString());
            }
        }

        public static void OnRspQryCash(IntPtr pContex, bool bSuccess, IntPtr pCashes, int nCount, int nOperationSeq, IntPtr pszErrorMsg)
        {
            //Console.WriteLine("OnRspQryCash: " + Marshal.PtrToStringAnsi(pszErrorMsg));
            Sys_log = Sys_log + "\n" + DateTime.Now.ToString() + ": 正在查询现金：" + Marshal.PtrToStringAnsi(pszErrorMsg);
            tagSTRUCT_CASH_FIXED[] Cashes = new tagSTRUCT_CASH_FIXED[nCount];
            for (int i = 0; i < nCount; i++)
            {
                IntPtr pcashes = (IntPtr)((UInt32)pCashes + i * Marshal.SizeOf(typeof(tagSTRUCT_CASH_FIXED)));
                Cashes[i] = (tagSTRUCT_CASH_FIXED)Marshal.PtrToStructure(pcashes, typeof(tagSTRUCT_CASH_FIXED));
                //Console.WriteLine((i + 1).ToString() + "\tCurrency:" + Cashes[i].nCurrencyType.ToString() + "\tTotal: " + Cashes[i].dTotalAmount.ToString() + "\tFrozen: " + Cashes[i].dFrozenAmount.ToString() + "\tAvailable: " + Cashes[i].dAvaliableAmount.ToString());
            }
        }

        public static void OnRspQryOrdersToday(IntPtr pContex, bool bSuccess, IntPtr pOrders, int nCount, int nOperationSeq, IntPtr pszErrorMsg)
        {
            //Console.WriteLine("OnRspQryOrdersToday: " + Marshal.PtrToStringAnsi(pszErrorMsg));
            Sys_log = Sys_log + "\n" + DateTime.Now.ToString() + ": 正在查询今日下单：" + Marshal.PtrToStringAnsi(pszErrorMsg);
            if (bSuccess && pOrders != IntPtr.Zero && nCount > 0)
            {
                tagSTRUCT_ORDER_FIXED[] Orders = new tagSTRUCT_ORDER_FIXED[nCount];
                for (int i = 0; i < nCount; i++)
                {
                    IntPtr porders = (IntPtr)((UInt32)pOrders + i * Marshal.SizeOf(typeof(tagSTRUCT_ORDER_FIXED)));
                    Orders[i] = (tagSTRUCT_ORDER_FIXED)Marshal.PtrToStructure(porders, typeof(tagSTRUCT_ORDER_FIXED));
                    //Console.WriteLine((i + 1).ToString() + "\t" + ORDER_STATUS_NAME[(int)Orders[i].ordStatus].ToString() + "\t" + Orders[i].nOrderDate.ToString() + "\t" + Orders[i].nOrderTime.ToString() + "\t" + Orders[i].szOrderSysID + "\t" + Orders[i].szExchangeID + "\t" +
                    //Orders[i].szInstrumentID + "\t" + ORDER_OFFSET_NAME[(int)Orders[i].ordOffset] + "\t" + ORDER_DIRECTION_NAME[(int)Orders[i].ordDirection] + "\t" + Orders[i].nOrderVolume.ToString() + "\t" + Orders[i].dOrderPrice.ToString() + "\t" +
                    //Orders[i].nDealVolume.ToString() + "\t" + Orders[i].dAveragePrice.ToString() + "\t" + Orders[i].dTotalAmount.ToString() + "\t" + Orders[i].nCanceledVolume.ToString());

                    //update orderStatus 
                    if (orderStatus.ContainsKey(Orders[i].szLocalHandleID.ToString()))
                    {
                        orderStatus[Orders[i].szLocalHandleID.ToString()] = Orders[i].ordStatus.ToString();
                    }
                    else
                        orderStatus.TryAdd(Orders[i].szLocalHandleID.ToString(), Orders[i].ToString());
                }
            }
        }

        public static void OnRspQryDealsToday(IntPtr pContex, bool bSuccess, IntPtr pDeals, int nCount, int nOperationSeq, IntPtr pszErrorMsg)
        {
            //Console.WriteLine("OnRspQryDealsToday: " + Marshal.PtrToStringAnsi(pszErrorMsg));
            Sys_log = Sys_log + "\n" + DateTime.Now.ToString() + ": 正在查询今日成交：" + Marshal.PtrToStringAnsi(pszErrorMsg);
            if (bSuccess && pDeals != IntPtr.Zero && nCount > 0)
            {
                tagSTRUCT_DEAL_FIXED[] Deals = new tagSTRUCT_DEAL_FIXED[nCount];
                for (int i = 0; i < nCount; i++)
                {
                    IntPtr pdeals = (IntPtr)((UInt32)pDeals + i * Marshal.SizeOf(typeof(tagSTRUCT_DEAL_FIXED)));
                    Deals[i] = (tagSTRUCT_DEAL_FIXED)Marshal.PtrToStructure(pdeals, typeof(tagSTRUCT_DEAL_FIXED));
                    //Console.WriteLine(Deals[i].nTradeDate.ToString() + "\t" + Deals[i].nTradeTime.ToString() + "\t" + Deals[i].szTradeID + "\t" + Deals[i].szOrderSysID + "\t" + Deals[i].szExchangeID + "\t" +
                    //Deals[i].szInstrumentID + "\t" + ORDER_OFFSET_NAME[(int)Deals[i].ordOffset] + "\t" + ORDER_DIRECTION_NAME[(int)Deals[i].ordDirection] + "\t" + Deals[i].nDealVolume.ToString() + "\t" + Deals[i].dAveragePrice.ToString() + "\t" + Deals[i].dTotalAmount.ToString() + "\t");
                }
            }
        }

        public static void OnRtnOrder(IntPtr pContex, bool bSuccess, IntPtr pOrder, bool bCurrentConnection, IntPtr pszErrorMsg)
        {
            //Console.WriteLine("OnRtnOrder: " + Marshal.PtrToStringAnsi(pszErrorMsg));
            Sys_log = Sys_log + "\n" + DateTime.Now.ToString() + ": 正在查询所有下单：" + Marshal.PtrToStringAnsi(pszErrorMsg);
            if (bSuccess)
            {
                if (pOrder != IntPtr.Zero)
                {
                    tagSTRUCT_ORDER_FIXED Order = new tagSTRUCT_ORDER_FIXED();
                    Marshal.PtrToStructure(pOrder, typeof(tagSTRUCT_ORDER_FIXED));
                    //Console.WriteLine(1.ToString(), "\t" + ORDER_STATUS_NAME[(int)Order.ordStatus] + "\t" + Order.nOrderDate.ToString() + "\t" + Order.nOrderTime.ToString() + "\t" + Order.szOrderSysID + "\t" + Order.szExchangeID + "\t" +
                    //Order.szInstrumentID + "\t" + ORDER_OFFSET_NAME[(int)Order.ordOffset] + "\t" + ORDER_DIRECTION_NAME[(int)Order.ordDirection] + "\t" + Order.nOrderVolume.ToString() + "\t" + Order.dOrderPrice.ToString() + "\t" +
                    //Order.nDealVolume.ToString() + "\t" + Order.dAveragePrice.ToString() + "\t" + Order.dTotalAmount.ToString() + "\t" + Order.nCanceledVolume.ToString());
                }
            }
        }

        public static void OnRtnDeal(IntPtr pContex, bool bSuccess, IntPtr pDeal, bool bCurrentConnection, IntPtr pszErrorMsg)
        {/*
            //Console.WriteLine("OnRtnDeal: " + Marshal.PtrToStringAnsi(pszErrorMsg));
            Sys_log = Sys_log + "\n" + DateTime.Now.ToString() + ": 正在查询所有成交：" + Marshal.PtrToStringAnsi(pszErrorMsg);
            if (bSuccess && pDeal != IntPtr.Zero)
            {
                tagSTRUCT_DEAL_FIXED Deal = new tagSTRUCT_DEAL_FIXED();
                Marshal.PtrToStructure(pDeal, Deal);
                //Console.WriteLine(1 + "\t" + Deal.nTradeDate.ToString() + "\t" + Deal.nTradeTime.ToString() + "\t" + Deal.szTradeID + "\t" + Deal.szOrderSysID + "\t" + Deal.szExchangeID + "\t" +
                //Deal.szInstrumentID + "\t" + ORDER_OFFSET_NAME[(int)Deal.ordOffset] + "\t" + ORDER_DIRECTION_NAME[(int)Deal.ordDirection] + "\t" + Deal.nDealVolume.ToString() + "\t" + Deal.dAveragePrice.ToString() + "\t" + Deal.dTotalAmount.ToString());
            }
            */
        }

        public static void OnRtnHolding(IntPtr pContex, bool bSuccess, IntPtr pHoldings, int nCount, int nOperationSeq, IntPtr pszErrorMsg)
        {
            //Console.WriteLine("OnRtnHolding: " + Marshal.PtrToStringAnsi(pszErrorMsg));
            Sys_log = Sys_log + "\n" + DateTime.Now.ToString() + ": 正在查询所有持仓：" + Marshal.PtrToStringAnsi(pszErrorMsg);
            if (bSuccess && pHoldings != IntPtr.Zero && nCount > 0)
            {
                tagSTRUCT_HOLDING_FIXED[] Holdings = new tagSTRUCT_HOLDING_FIXED[nCount];
                for (int i = 0; i < nCount; i++)
                {
                    IntPtr pholdings = (IntPtr)((UInt32)pHoldings + i * Marshal.SizeOf(typeof(tagSTRUCT_HOLDING_FIXED)));
                    Holdings[i] = (tagSTRUCT_HOLDING_FIXED)Marshal.PtrToStructure(pholdings, typeof(tagSTRUCT_HOLDING_FIXED));
                    //Console.WriteLine((i + 1).ToString() + "\t" + Holdings[i].szExchangeID + "\t" + Holdings[i].szCode + "\t" + Holdings[i].szName + "\t" + Holdings[i].nSecuType.ToString() + "\t" + ORDER_DIRECTION_NAME[(int)Holdings[i].ordDirection] + "\t" +
                    //Holdings[i].nTotalVolume.ToString() + "\t" + Holdings[i].nAvailableVolume.ToString() + "\t" + Holdings[i].nBuyFrozenVol.ToString() + "\t" + Holdings[i].nSellFrozenVol.ToString() + "\t" + Holdings[i].dMarketValue.ToString() + "\t" + Holdings[i].dCurrentPrice.ToString() + "\t" +
                    //sHoldings[i].dCostPrice.ToString() + "\t" + Holdings[i].dFloatingPL.ToString() + "\t" + Holdings[i].dReturn.ToString() + "\t" + Holdings[i].nBuyVolToday.ToString() + "\t" + Holdings[i].nSellVolToday.ToString());
                }
            }
        }

        public static void OnRtnCash(IntPtr pContex, bool bSuccess, IntPtr pCashes, int nCount, int nOperationSeq, IntPtr pszErrorMsg)
        {
            //Console.WriteLine("OnRtnCash: " + Marshal.PtrToStringAnsi(pszErrorMsg));
            Sys_log = Sys_log + "\n" + DateTime.Now.ToString() + ": 正在查询所有现金：" + Marshal.PtrToStringAnsi(pszErrorMsg);
            if (bSuccess && pCashes != IntPtr.Zero && nCount > 0)
            {
                tagSTRUCT_CASH_FIXED[] Cashes = new tagSTRUCT_CASH_FIXED[nCount];
                for (int i = 0; i < nCount; i++)
                {
                    IntPtr pcashes = (IntPtr)((UInt32)pCashes + i * Marshal.SizeOf(typeof(tagSTRUCT_CASH_FIXED)));
                    Cashes[i] = (tagSTRUCT_CASH_FIXED)Marshal.PtrToStructure(pcashes, typeof(tagSTRUCT_CASH_FIXED));
                    //Console.WriteLine((i + 1).ToString() + "\tCurrency:" + Cashes[i].nCurrencyType.ToString() + " \tTotal: " + Cashes[i].dTotalAmount.ToString() + "\tFrozen: " + Cashes[i].dFrozenAmount.ToString() + "\tAvailable: " + Cashes[i].dAvaliableAmount.ToString());
                }
            }
        }

        public static void OnRspError(IntPtr pContex, uint dwUniqueID, uint nErrorClass, uint nErrorID, IntPtr pszErrorInfo, bool bCurrentConnection)
        {
            //Console.WriteLine("OnRspError: o" + Marshal.PtrToStringAnsi(pszErrorInfo));
            Sys_log = Sys_log + "\n" + DateTime.Now.ToString() + ": 错误：" + Marshal.PtrToStringAnsi(pszErrorInfo);
        }
    }
}
