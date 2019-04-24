using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Surface.WinfairMDAPI.Struct;

namespace Surface.MyTimer
{
    class Data
    {
        private static ConcurrentDictionary<string, tagCThostMyDepthMarketDataFieldEx> mDDict = new ConcurrentDictionary<string, tagCThostMyDepthMarketDataFieldEx>();

        internal static ConcurrentDictionary<string, tagCThostMyDepthMarketDataFieldEx> MDDict { get => mDDict; set => mDDict = value; }
        private static double targetValue = 0;
        private static double holdingValue = 100000000;
        private static double indexValue = 0;
        private static double minusValue = 0;
        private static ConcurrentDictionary<string, StockInfo> targetDict = new ConcurrentDictionary<string, StockInfo>();
        private static ConcurrentDictionary<string, StockInfo> formerDict = new ConcurrentDictionary<string, StockInfo>();
        private static ConcurrentDictionary<string, StockInfo> holdingDict = new ConcurrentDictionary<string, StockInfo>();
        private static ConcurrentDictionary<string, StockInfo> operationDict = new ConcurrentDictionary<string, StockInfo>();

        public static double TargetValue { get => targetValue; set => targetValue = value; }
        public static double HoldingValue { get => holdingValue; set => holdingValue = value; }
        public static double IndexValue { get => indexValue; set => indexValue = value; }
        public static double MinusValue { get => minusValue; set => minusValue = value; }
        internal static ConcurrentDictionary<string, StockInfo> TargetDict { get => targetDict; set => targetDict = value; }
        internal static ConcurrentDictionary<string, StockInfo> FormerDict { get => formerDict; set => formerDict = value; }
        internal static ConcurrentDictionary<string, StockInfo> HoldingDict { get => holdingDict; set => holdingDict = value; }
        internal static ConcurrentDictionary<string, StockInfo> OperationDict { get => operationDict; set => operationDict = value; }

        public struct StockInfo
        {
            private string stockID;
            private string exchangeID;
            private double proportion;
            private string name;
            private string optionStockID1;
            private string optionStockID2;
            private string optionStockID3;

            public string StockID { get => stockID; set => stockID = value; }
            public double Proportion { get => proportion; set => proportion = value; }
            public string ExchangeID { get => exchangeID; set => exchangeID = value; }
            public string OptionStockID1 { get => optionStockID1; set => optionStockID1 = value; }
            public string OptionStockID2 { get => optionStockID2; set => optionStockID2 = value; }
            public string OptionStockID3 { get => optionStockID3; set => optionStockID3 = value; }
            public string Name { get => name; set => name = value; }
        }

        public struct BriefInfo
        {
            private string stockID;
            private string exchangeID;
            private string name;
            private double proportion;
            

            public string StockID { get => stockID; set => stockID = value; }
            public double Proportion { get => proportion; set => proportion = value; }
            public string ExchangeID { get => exchangeID; set => exchangeID = value; }
            public string Name { get => name; set => name = value; }
        }

        public struct OptionInfo
        {

            private string stockID;
            private string optionStockID1;
            private string optionStockID2;
            private string optionStockID3;

            public string StockID { get => stockID; set => stockID = value; }
            public string OptionStockID1 { get => optionStockID1; set => optionStockID1 = value; }
            public string OptionStockID2 { get => optionStockID2; set => optionStockID2 = value; }
            public string OptionStockID3 { get => optionStockID3; set => optionStockID3 = value; }
        }
    }
}
