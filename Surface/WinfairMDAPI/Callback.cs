using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static Surface.Form1;
using static Surface.WinfairMDAPI.Struct;
using static Surface.MyTimer.Data;

namespace Surface.WinfairMDAPI
{
    class Callback
    {
        public static void OnQuote(IntPtr AContex, IntPtr pRtn)
        {
            int num = MDDict.Count;
            tagCThostMyDepthMarketDataFieldEx Data = new tagCThostMyDepthMarketDataFieldEx();
            Data = (tagCThostMyDepthMarketDataFieldEx)Marshal.PtrToStructure(pRtn, typeof(tagCThostMyDepthMarketDataFieldEx));
            //Marshal.PtrToStructure(pRtn, Data);
            if (pRtn != IntPtr.Zero)
            {
                //Console.WriteLine(Data.ExchangeInstID + " " + Data.TradingDay + " " + Data.UpdateTime + " " + Data.ExchangeID + " " + Data.InstrumentID + " " + Data.InstrumentName + " " + Data.LastPrice.ToString() + "(" + Data.Volume.ToString() + ")");
                //MDStruct ms = new MDStruct();
                //ms.InstrumentID = Data.InstrumentID;
                //ms.LastPrice = Data.LastPrice;
                //MDList.Add(ms);
                /*string str = Data.InstrumentID;
                if (MDList.ContainsKey(str))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (str == target[i])
                        {
                            presentValue -= 1000 * MDList[str].LastPrice;
                            MDList[str] = Data;
                            presentValue += 1000 * Data.LastPrice;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (str == target[i])
                        {
                            presentValue -= 1000 * MDList[str].LastPrice;
                            try
                            {
                                MDList.Add(str, Data);
                            }
                            catch { }
                            presentValue += 1000 * Data.LastPrice;
                            break;
                        }
                    }
                }*/

                if (!MDDict.TryAdd(Data.InstrumentID, Data))
                {
                    tagCThostMyDepthMarketDataFieldEx temp = MDDict[Data.InstrumentID];
                    MDDict.TryUpdate(Data.InstrumentID, Data, temp);
                    //HoldingValue -= 
                }
            }
        }

        public static void OnHistQuote(IntPtr AContex, IntPtr pRtn, int nCount)
        {
            tagCThostMyDepthMarketDataFieldEx[] Data = new tagCThostMyDepthMarketDataFieldEx[nCount];
            if (pRtn == IntPtr.Zero)
            {
                Console.WriteLine("Error");
            }
            if (nCount == 0)
            {
                Console.WriteLine("error");
            }
            if (pRtn != IntPtr.Zero && nCount > 0)
            {
                for (int i = 0; i < nCount; i++)
                {
                    IntPtr pdata = (IntPtr)((UInt32)pRtn + i * Marshal.SizeOf(typeof(tagCThostMyDepthMarketDataFieldEx)));
                    Data[i] = (tagCThostMyDepthMarketDataFieldEx)Marshal.PtrToStructure(pdata, typeof(tagCThostMyDepthMarketDataFieldEx));
                    Console.WriteLine(Data[i].TradingDay + " " + Data[i].UpdateTime + " " + Data[i].ExchangeID + "s." + Data[i].InstrumentID + "s " + Data[i].InstrumentName + "s " + Data[i].LastPrice.ToString() + "(" + Data[i].Volume.ToString() + ")");
                }
            }
        }

        public static void OnInstrument(IntPtr AContex, IntPtr pRtn, int nCount)
        {
            tagSTRUCT_INSTRUMENT[] Data = new tagSTRUCT_INSTRUMENT[nCount];
            if (pRtn != IntPtr.Zero && nCount > 0)
            {
                string path = @"D:\\instrument.txt";
                using (StreamWriter sw = File.CreateText(path))
                {
                    for (int i = 0; i < nCount; i++)
                    {
                        IntPtr pdata = (IntPtr)((UInt32)pRtn + i * Marshal.SizeOf(typeof(tagSTRUCT_INSTRUMENT)));
                        Data[i] = (tagSTRUCT_INSTRUMENT)Marshal.PtrToStructure(pRtn, typeof(tagSTRUCT_INSTRUMENT));
                        sw.WriteLine(Data[i].ExchangeID + "." + Data[i].InstrumentID + " " + Data[i].InstrumentName + " " + Data[i].MinBuyVolume + " " + Data[i].PriceTick);
                    }
                }
            }
        }
    }
}
