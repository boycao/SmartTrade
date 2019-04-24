using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Surface.Form1;
using static Surface.MyLogic.Struct;
using static Surface.MyTimer.Data;
using static Surface.WinfairMDAPI.Struct;
using static Surface.WinfairTDAPI.Function;
using static Surface.WinfairTDAPI.Struct;
using Surface.WinfairTDAPI;
using System.Collections.Concurrent;

namespace Surface.MyLogic
{
    class Logic
    {

        private static int buyWaitTime, buyTry, overTimeExchange, errorExchange;
        private static int sellWaitTime, sellTry;
        private static double buyAdditionalPrice;
        private static double sellAdditionalPrice;

        public static ConcurrentDictionary<string, string> optionStock = new ConcurrentDictionary<string, string>();
        public static ConcurrentDictionary<string, double> stockPrice = new ConcurrentDictionary<string, double>();
        public static ConcurrentDictionary<string, double> stockRate = new ConcurrentDictionary<string, double>();
        public static ConcurrentDictionary<string, string> orderStatus = new ConcurrentDictionary<string, string>();

        public static int BuyWaitTime { get => buyWaitTime; set => buyWaitTime = value; }
        public static int BuyTry { get => buyTry; set => buyTry = value; }
        public static int OverTimeExchange { get => overTimeExchange; set => overTimeExchange = value; }
        public static int ErrorExchange { get => errorExchange; set => errorExchange = value; }
        public static int SellWaitTime { get => sellWaitTime; set => sellWaitTime = value; }
        public static int SellTry { get => sellTry; set => sellTry = value; }
        public static double BuyAdditionalPrice { get => buyAdditionalPrice; set => buyAdditionalPrice = value; }
        public static double SellAdditionalPrice { get => sellAdditionalPrice; set => sellAdditionalPrice = value; }
        public static double SpreadToTransfer { get; internal set; }
        public static int AllWaitingTime { get; internal set; }
        public static int BuyPrice { get => buyPrice; set => buyPrice = value; }
        public static int SellPrice { get => sellPrice; set => sellPrice = value; }

        private static int buyPrice = 0;
        private static int sellPrice = 0;

        public enum PRICE_NAME { 涨停价, 卖五价, 卖四价, 卖三价, 卖二价, 卖一价, 当前价 = 0, 买一价, 买二价, 买三价, 买四价, 买五价, 跌停价};
        public enum TD_NAME { UpperLimitPrice, AskPrice5, AskPrice4, AskPrice3, AskPrice2, AskPrice1, LastPrice, BidPrice1, BidPrice2, BidPrice3, BidPrice4, BidPrice5, LowerLimitPrice}



        public static void buyStock(object obj)
        {
            BuyInfo bi = (BuyInfo)obj;
            bool changeFlag = true;
            bool overTimeFlag = true;
            bool errorFlag = true;
            int count;
            double price;
            ORDER_DIRECTION eDirection = ORDER_DIRECTION.BID;
            ORDER_OFFSET eOffset = ORDER_OFFSET.OPEN;
            countAndPrice cp = calculateCount(bi.Si.StockID, bi.Si.Proportion, HoldingValue, "Buy");
            count = cp.Count;
            price = cp.Price;
            string localHandleID = "";
            do
            {
                localHandleID = TDOrderInsert(bi.Si.ExchangeID, bi.Si.StockID, price, count, eDirection, eOffset);
            } while (localHandleID == "");
            Str_log = Str_log + "\n" + DateTime.Now.ToString() + ": 尝试以" + price + "元买入股票" + bi.StockID + count + "手";
            Thread.Sleep(BuyWaitTime);
            TDQryOrdersToday();
            Thread.Sleep(1000);
            if (orderStatus[localHandleID] == "ALLTRADED")
            {
                changeFlag = false;
                bool flag = HoldingDict.TryAdd(bi.StockID, bi.Si);
                if (flag)
                {
                    Str_log = Str_log + "\n" + DateTime.Now.ToString() + ": 成功以" + price + "元买入股票" + bi.StockID + count + "手";
                }
                return;
            }
            if (changeFlag)
            {
                if (overTimeFlag)
                {
                    dealWithOverTime(bi.StockID, bi.Si.Proportion, HoldingValue, bi.Order + 1);
                    return;
                }
                else if (errorFlag)
                {
                    dealWithError(bi.StockID, bi.Si.Proportion, HoldingValue, bi.Order + 1);
                    return;
                }
                else
                {
                    TDOrderAction(localHandleID);
                    Str_log = Str_log + "\n" + DateTime.Now.ToString() + ": 撤回单号" + localHandleID + "订单";
                    for (int i = 1; i < BuyTry; i++)
                    {
                        cp = calculateCount(bi.Si.StockID, bi.Si.Proportion, HoldingValue, "Buy");
                        count = cp.Count;
                        price = cp.Price;
                        do
                        {
                            localHandleID = TDOrderInsert(bi.Si.ExchangeID, bi.Si.StockID, price, count, eDirection, eOffset);
                        } while (localHandleID == "");
                        Str_log = Str_log + "\n" + DateTime.Now.ToString() + ": 尝试以" + price + "元买入股票" + bi.StockID + count + "手";
                        Thread.Sleep(BuyWaitTime);
                        TDQryOrdersToday();
                        Thread.Sleep(1000);
                        if (orderStatus[localHandleID] == "ALLTRADED")
                        {
                            changeFlag = false;
                            bool flag = HoldingDict.TryAdd(bi.StockID, bi.Si);
                            if (flag)
                            {
                                Str_log = Str_log + "\n" + DateTime.Now.ToString() + ": 成功以" + price + "元买入股票" + bi.StockID + count + "手";
                            }
                            return;
                        }
                        if (changeFlag)
                        {
                            if (overTimeFlag)
                            {
                                dealWithOverTime(bi.StockID, bi.Si.Proportion, Surface.MyTimer.Data.HoldingValue, bi.Order + 1);
                                return;
                            }
                            else if (errorFlag)
                            {
                                dealWithError(bi.StockID, bi.Si.Proportion, Surface.MyTimer.Data.HoldingValue, bi.Order + 1);
                                return;
                            }
                            else
                            {
                                TDOrderAction(localHandleID);
                                Str_log = Str_log + "\n" + DateTime.Now.ToString() + ": 撤回单号" + localHandleID + "订单";
                                if (i == SellTry - 1)
                                {
                                    cp = calculateCount(bi.Si.StockID, bi.Si.Proportion, Surface.MyTimer.Data.HoldingValue, "BuyLimit");
                                    count = cp.Count;
                                    price = cp.Price;
                                    do
                                    {
                                        localHandleID = TDOrderInsert(bi.Si.ExchangeID, bi.Si.StockID, price, count, eDirection, eOffset);
                                    } while (localHandleID == "");
                                    Str_log = Str_log + "\n" + DateTime.Now.ToString() + ": 尝试以" + price + "元买入股票" + bi.StockID + count + "手";
                                    Thread.Sleep(BuyWaitTime);
                                    TDQryOrdersToday();
                                    Thread.Sleep(1000);
                                    if (orderStatus[localHandleID] == "ALLTRADED")
                                    {
                                        changeFlag = false;
                                        bool flag = HoldingDict.TryAdd(bi.StockID, bi.Si);
                                        if (flag)
                                        {
                                            Str_log = Str_log + "\n" + DateTime.Now.ToString() + ": 成功以" + price + "元买入股票" + bi.StockID + count + "手";
                                        }
                                        return;
                                    }
                                    else
                                    {
                                        Str_log = Str_log + "\n" + DateTime.Now.ToString() + ": 买入失败";
                                        Er_log = Er_log + "\n" + DateTime.Now.ToString() + ": 买入失败";
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    return;
                }
            }
        }

        public static void sellStock(object obj)
        {
            StockInfo si = (StockInfo)obj;
            bool changeFlag = true;
            int count;
            double price;
            ORDER_DIRECTION eDirection = ORDER_DIRECTION.ASK;
            ORDER_OFFSET eOffset = ORDER_OFFSET.CLOSE;
            countAndPrice cp = calculateCount(si.StockID, si.Proportion, HoldingValue, "Sell");
            count = cp.Count;
            string localHandleID;
            price = cp.Price;
            do
            {
                localHandleID = TDOrderInsert(si.ExchangeID, si.StockID, price, count, eDirection, eOffset);
            } while (localHandleID == "");
            Str_log = Str_log + "\n" + DateTime.Now.ToString() + ": 尝试以" + price + "元卖出股票" + si.StockID + count + "手";
            Thread.Sleep(SellWaitTime);
            TDQryOrdersToday();
            Thread.Sleep(1000);
            if (orderStatus[localHandleID] == "ALLTRADED")
            {
                changeFlag = false;
                StockInfo value;
                bool flag = HoldingDict.TryRemove(si.StockID, out value);
                if (flag)
                {
                    Str_log = Str_log + "\n" + DateTime.Now.ToString() + ": 成功以" + price + "元卖出股票" + si.StockID + count + "手";
                }
                return;
            }

            if (changeFlag)
            {
                TDOrderAction(localHandleID);
                Str_log = Str_log + "\n" + DateTime.Now.ToString() + ": 撤回单号" + localHandleID + "订单";
                for (int i = 1; i < SellTry; i++)
                {
                    cp = calculateCount(si.StockID, si.Proportion, HoldingValue, "Sell");
                    count = cp.Count;
                    price = cp.Price;
                    do
                    {
                        localHandleID = TDOrderInsert(si.ExchangeID, si.StockID, price, count, eDirection, eOffset);
                    } while (localHandleID == "");
                    Str_log = Str_log + "\n" + DateTime.Now.ToString() + ": 尝试以" + price + "元卖出股票" + si.StockID + count + "手";
                    Thread.Sleep(SellWaitTime);
                    TDQryOrdersToday();
                    Thread.Sleep(1000);
                    if (orderStatus[localHandleID] == "ALLTRADED")
                    {
                        changeFlag = false;
                        StockInfo value;
                        bool flag = HoldingDict.TryRemove(si.StockID, out value);
                        if (flag)
                        {
                            Str_log = Str_log + "\n" + DateTime.Now.ToString() + ": 成功以" + price + "元卖出股票" + si.StockID + count + "手";
                        }
                        return;
                    }
                    if (changeFlag)
                    {
                        TDOrderAction(localHandleID);
                        Str_log = Str_log + "\n" + DateTime.Now.ToString() + ": 撤回单号" + localHandleID + "订单";
                        if (i == SellTry - 1)
                        {
                            cp = calculateCount(si.StockID, si.Proportion, Surface.MyTimer.Data.HoldingValue, "SellLimit");
                            count = cp.Count;
                            price = cp.Price;
                            do
                            {
                                localHandleID = TDOrderInsert(si.ExchangeID, si.StockID, price, count, eDirection, eOffset);
                            } while (localHandleID == "");
                            Str_log = Str_log + "\n" + DateTime.Now.ToString() + ": 尝试以" + price + "元卖出股票" + si.StockID + count + "手";
                            Thread.Sleep(SellWaitTime);
                            TDQryOrdersToday();
                            Thread.Sleep(1000);
                            if (orderStatus[localHandleID] == "ALLTRADED")
                            {
                                changeFlag = false;
                                StockInfo value;
                                bool flag = HoldingDict.TryRemove(si.StockID, out value);
                                if (flag)
                                {
                                    Str_log = Str_log + "\n" + DateTime.Now.ToString() + ": 成功以" + price + "元卖出股票" + si.StockID + count + "手";
                                }
                                return;
                            }
                            else
                            {
                                Str_log = Str_log + "\n" + DateTime.Now.ToString() + ": 卖出失败";
                                Er_log = Er_log + "\n" + DateTime.Now.ToString() + ": 卖出失败";
                                return;
                            }
                        }
                    }
                }
                return;
            }
        }

        private static void dealWithOverTime(string stockID, double changingProportion, double value, int order)
        {
            switch (OverTimeExchange)
            {
                // overTimeExchange == 0 means cashExchange is chosen
                case 0: cashExchange(); break;
                // overTimeExchange == 1 means ETFExchange is chosen
                case 1: ETFExchange(); break;
                // overTimeExchange == 2 means stockExchange is chosen
                case 2: stockExchange(stockID, changingProportion, value, order); break;
            }
        }

        private static void dealWithError(string stockID, double changingProportion, double value, int order)
        {
            switch (ErrorExchange)
            {
                // errorExchange == 0 means cashExchange is chosen
                case 0: cashExchange(); break;
                // errorExchange == 1 means ETFExchange is chosen
                case 1: ETFExchange(); break;
                // errorExchange == 2 means stockExchange is chosen
                case 2: stockExchange(stockID, changingProportion, value, order); break;
            }
        }

        private static void cashExchange()
        {
            // do nothing
        }

        private static void ETFExchange()
        {
            // do not have this function
            throw new NotImplementedException();
        }

        private static void stockExchange(string stockID, double changingProportion, double value, int order)
        {
            if (MDDict.ContainsKey(stockID))
            {
                string optionStockID = "";
                if (order == 1)
                {
                    optionStockID = OperationDict[stockID].OptionStockID1;
                    BuyInfo bi = new BuyInfo(OperationDict[stockID], stockID, 1);
                    Surface.MyTimer.Data.StockInfo si = bi.Si;
                    si = new Surface.MyTimer.Data.StockInfo();
                    si.StockID = optionStockID;
                    si.Proportion = changingProportion;
                    buyStock(bi);
                }
                else if (order == 2)
                {
                    optionStockID = Surface.MyTimer.Data.OperationDict[stockID].OptionStockID2;
                    BuyInfo bi = new BuyInfo(Surface.MyTimer.Data.OperationDict[stockID], stockID, 2);
                    Surface.MyTimer.Data.StockInfo si = bi.Si;
                    si = new Surface.MyTimer.Data.StockInfo();
                    si.StockID = optionStockID;
                    si.Proportion = changingProportion;
                    buyStock(bi);
                }
                else if (order == 3)
                {
                    optionStockID = Surface.MyTimer.Data.OperationDict[stockID].OptionStockID3;
                    BuyInfo bi = new BuyInfo(Surface.MyTimer.Data.OperationDict[stockID], stockID, 3);
                    Surface.MyTimer.Data.StockInfo si = bi.Si;
                    si = new Surface.MyTimer.Data.StockInfo();
                    si.StockID = optionStockID;
                    si.Proportion = changingProportion;
                    buyStock(bi);
                }
                else
                {
                    Console.WriteLine("Buying option stock failed.");
                }
            }
            else
            {
                Console.WriteLine("Target list add error!");
            }
        }

        public static countAndPrice calculateCount(string stockID, double changingProportion, double value, string status)
        {
            int num = MDDict.Count;
            // 如果是尝试买入
            // 使用当前价格自定义加档买入
            if (status == "Buy")
            {
                double price = 0;
                switch (BuyPrice)
                {
                    case -6: price = MDDict[stockID].UpperLimitPrice; break;
                    case -5: price = MDDict[stockID].AskPrice5; break;
                    case -4: price = MDDict[stockID].AskPrice4; break;
                    case -3: price = MDDict[stockID].AskPrice3; break;
                    case -2: price = MDDict[stockID].AskPrice2; break;
                    case -1: price = MDDict[stockID].AskPrice1; break;
                    case 0: price = MDDict[stockID].LastPrice; break;
                    case 1: price = MDDict[stockID].BidPrice1; break;
                    case 2: price = MDDict[stockID].BidPrice2; break;
                    case 3: price = MDDict[stockID].BidPrice3; break;
                    case 4: price = MDDict[stockID].BidPrice4; break;
                    case 5: price = MDDict[stockID].BidPrice5; break;
                    case 6: price = MDDict[stockID].LowerLimitPrice; break;
                }
                double buyPrice = price + BuyAdditionalPrice;
                int ct = (int)(changingProportion * value / buyPrice / 100);
                countAndPrice cp = new countAndPrice();
                cp.Count = ct;
                cp.Price = buyPrice;
                return cp;
            }
            // 如果是尝试卖出
            // 使用当前价格自定义减档卖出
            else if (status == "Sell")
            {
                double price = 0;
                switch (BuyPrice)
                {
                    case -6: price = MDDict[stockID].UpperLimitPrice; break;
                    case -5: price = MDDict[stockID].AskPrice5; break;
                    case -4: price = MDDict[stockID].AskPrice4; break;
                    case -3: price = MDDict[stockID].AskPrice3; break;
                    case -2: price = MDDict[stockID].AskPrice2; break;
                    case -1: price = MDDict[stockID].AskPrice1; break;
                    case 0: price = MDDict[stockID].LastPrice; break;
                    case 1: price = MDDict[stockID].BidPrice1; break;
                    case 2: price = MDDict[stockID].BidPrice2; break;
                    case 3: price = MDDict[stockID].BidPrice3; break;
                    case 4: price = MDDict[stockID].BidPrice4; break;
                    case 5: price = MDDict[stockID].BidPrice5; break;
                    case 6: price = MDDict[stockID].LowerLimitPrice; break;
                }
                double sellPrice = price - SellAdditionalPrice;
                int ct = (int)(changingProportion * value / sellPrice / 100);
                countAndPrice cp = new countAndPrice();
                cp.Count = ct;
                cp.Price = sellPrice;
                return cp;
            }
            // 如果是尝试无论如何也要买入
            // 使用涨停价买入
            else if (status == "BuyLimit")
            {
                double upperLimitPrice = MDDict[stockID].UpperLimitPrice;
                int ct = (int)(changingProportion * value / upperLimitPrice / 100);
                countAndPrice cp = new countAndPrice();
                cp.Count = ct;
                cp.Price = upperLimitPrice;
                return cp;
            }
            // 如果是尝试无论如何也要卖出
            // 使用跌停价卖出
            else
            {
                // status == "SellLimit"
                double lowerLimitPrice = MDDict[stockID].LowerLimitPrice;
                int ct = (int)(changingProportion * value / lowerLimitPrice / 100);
                countAndPrice cp = new countAndPrice();
                cp.Count = ct;
                cp.Price = lowerLimitPrice;
                return cp;
            }
        }

        private void queryOrderStatus()
        {
            Task keepQryStatus = new Task(updateOrderStatus);
            keepQryStatus.Start();
        }

        private void updateOrderStatus()
        {
            while (true)
            {
                TDQryOrdersToday();
                Thread.Sleep(300);
            }
        }
    }
}
