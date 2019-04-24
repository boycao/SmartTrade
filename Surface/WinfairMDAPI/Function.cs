using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static Surface.WinfairMDAPI.Callback;
using static Surface.WinfairMDAPI.Delegate;
using static Surface.WinfairMDAPI.DllImport;
using static Surface.WinfairMDAPI.Struct;

namespace Surface.WinfairMDAPI
{
    class Function
    {
        public static void MDCreate()
        {
            onquote = new lpRtn(OnQuote);
            onhistquote = new lpRtnEx(OnInstrument);
            oninstrument = new lpRtnEx(OnInstrument);
            CreateFtdcMdApi();
            SetMarketApiCallback(IntPtr.Zero, null, null, null, null, null, null, onquote, null, null, onhistquote, oninstrument);
        }

        public static void MDSubscribe()
        {
            if ((int)ReqUserLogin(IntPtr.Zero, 0) == 0)
            {
                string allTemp = "ALL";
                string endTemp = "END";
                IntPtr pallTemp = Marshal.StringToHGlobalAnsi(allTemp);
                IntPtr pendTemp = Marshal.StringToHGlobalAnsi(endTemp);
                IntPtr ppallTemp = Marshal.StringToHGlobalAnsi(allTemp);
                Marshal.StructureToPtr(pallTemp, ppallTemp, true);
                //SubscribeMarketData(ppallTemp, 1, pallTemp);
                //SubscribeMarketData(ppallTemp, 1, pendTemp);

                FileStream file = new FileStream(@"C:\\users\\11712\\Desktop\\hs300.txt", FileMode.Open);
                StreamReader sr = new StreamReader(file);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] output = line.Split('\t');
                    string ExID;
                    string InID = output[1];
                    int length = InID.Length;
                    InID = InID.PadLeft(6, '0');
                    if (InID.Substring(0, 1) == "6")
                    {
                        ExID = "SSE";
                    }
                    else
                    {
                        ExID = "SZE";
                    }
                    IntPtr pexID = Marshal.StringToHGlobalAnsi(ExID);
                    IntPtr pinID = Marshal.StringToHGlobalAnsi(InID);
                    IntPtr ppinID = Marshal.StringToHGlobalAnsi(InID);
                    Marshal.StructureToPtr(pinID, ppinID, true);
                    try
                    {
                        SubscribeMarketData(ppinID, 1, pexID);

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                SubscribeMarketData(IntPtr.Zero, 0, pendTemp);
            }
        }

        public static void MDQryHistQuote()
        {
            if ((int)ReqUserLogin(IntPtr.Zero, 0) == 0)
            {
                string szInstrumentID = "510300";
                string szExchangeID = "SSE";
                IntPtr pszInstrumentID = Marshal.StringToHGlobalAnsi(szInstrumentID);
                IntPtr pszExchangeID = Marshal.StringToHGlobalAnsi(szExchangeID);
                ReqQryHistQuote(pszInstrumentID, pszExchangeID, 20160426, 92500000, 20160427, 100000000);
            }
        }

        public static void MDQryInst()
        {
            if ((int)ReqUserLogin(IntPtr.Zero, 0) == 0)
            {
                CSecurityMyQryInstrumentField stQryInst = new CSecurityMyQryInstrumentField();
                IntPtr pstQryInst = new IntPtr();
                Marshal.StructureToPtr(stQryInst, pstQryInst, true);
                ReqQryInstrument(pstQryInst, 0);
            }
        }
    }
}
