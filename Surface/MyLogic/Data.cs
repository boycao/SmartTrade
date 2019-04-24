using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Surface.WinfairTDAPI.Struct;

namespace Surface.MyLogic
{
    class Data
    {
        private static int buyWaitTime, buyTry, overTimeExchange, errorExchange;
        private static int sellWaitTime, sellTry;
        private static double buyAdditionalPrice;
        private static double sellAdditionalPrice;

        public static ConcurrentDictionary<string, string> optionStock = new ConcurrentDictionary<string, string>();
        public static ConcurrentDictionary<string, double> stockPrice = new ConcurrentDictionary<string, double>();
        public static ConcurrentDictionary<string, double> stockRate = new ConcurrentDictionary<string, double>();
        public static ConcurrentDictionary<string, string> orderStatus = new ConcurrentDictionary<string, tagSTRUCT_ORDER_FIXED>(); 

        public static int BuyWaitTime { get => buyWaitTime; set => buyWaitTime = value; }
        public static int BuyTry { get => buyTry; set => buyTry = value; }
        public static int OverTimeExchange { get => overTimeExchange; set => overTimeExchange = value; }
        public static int ErrorExchange { get => errorExchange; set => errorExchange = value; }
        public static int SellWaitTime { get => sellWaitTime; set => sellWaitTime = value; }
        public static int SellTry { get => sellTry; set => sellTry = value; }
        public static double BuyAdditionalPrice { get => buyAdditionalPrice; set => buyAdditionalPrice = value; }
        public static double SellAdditionalPrice { get => sellAdditionalPrice; set => sellAdditionalPrice = value; }
    }
}
