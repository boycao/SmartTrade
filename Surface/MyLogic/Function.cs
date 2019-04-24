using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Surface.MyLogic.Data;
using static Surface.MyLogic.Struct;
using Surface.WinfairTDAPI;
namespace Surface.MyLogic
{
    class Function
    {
        private void buyStock(object obj)
        {
            BuyInfo bi = (BuyInfo)obj;
            bool changeFlag = false;
            bool overTimeFlag = false;
            bool errorFlag = false;
            bool buyFlag = false;
            int count;
            double price;
            countAndPrice cp = calculateCount(bi.Si.StockID, bi.Si.Proportion, Surface.MyTimer.Data.HoldingValue, "Buy");
            count = cp.Count;
            price = cp.Price;
            //Surface.WinfairTDAPI.Function.OrderInsert(bi.Si.StockID, price, count);
            Thread.Sleep(BuyWaitTime);

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
            }
            else
            {
                //Surface.WinfairTDAPI.Function.OrderAction();
                for (int i = 1; i < BuyTry; i++)
                {
                    cp = calculateCount(bi.Si.StockID, bi.Si.Proportion, Surface.MyTimer.Data.HoldingValue, "Buy");
                    count = cp.Count;
                    price = cp.Price;
                    //Surface.WinfairTDAPI.Function.OrderInsert(bi.Si.StockID, price, count);
                    Thread.Sleep(BuyWaitTime);
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
                            //Surface.WinfairTDAPI.Function.OrderAction();
                            if (i == SellTry - 1)
                            {
                                cp = calculateCount(bi.Si.StockID, bi.Si.Proportion, Surface.MyTimer.Data.HoldingValue, "BuyLimit");
                                count = cp.Count;
                                price = cp.Price;
                                //Surface.WinfairTDAPI.Function.OrderInsert(bi.Si.StockID, price, count);
                                Thread.Sleep(BuyWaitTime);
                                if (buyFlag)
                                {
                                    Console.WriteLine("Buying failed.");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void sellStock(object obj)
        {
            Surface.MyTimer.Data.StockInfo si = (Surface.MyTimer.Data.StockInfo)obj;
            bool changeFlag = false;
            bool sellFlag = false;
            int count;
            double price;
            countAndPrice cp = calculateCount(si.StockID, si.Proportion, Surface.MyTimer.Data.HoldingValue, "Sell");
            count = cp.Count;
            price = cp.Price;
            //Surface.WinfairTDAPI.Function.OrderInsert(si.StockID, price, count);
            string localHandleID = Surface.WinfairTDAPI.Function.TDOrderInsert();          
            Thread.Sleep(SellWaitTime);
            if (orderStatus[localHandleID] == "ALLTRADED")
            {
                changeFlag = true;
            }
            if (changeFlag)
            {
                //Surface.WinfairTDAPI.Function.OrderAction();
                for (int i = 1; i < SellTry; i++)
                {
                    cp = calculateCount(si.StockID, si.Proportion, Surface.MyTimer.Data.HoldingValue, "Sell");
                    count = cp.Count;
                    price = cp.Price;
                    //Surface.WinfairTDAPI.Function.OrderInsert(si.StockID, price, count);
                    localHandleID = Surface.WinfairTDAPI.Function.TDOrderInsert();
                    Thread.Sleep(SellWaitTime);
                    if (orderStatus[localHandleID] == "ALLTRADED")
                    {
                        changeFlag = true;
                    }
                    if (changeFlag)
                    {
                        //Surface.WinfairTDAPI.Function.OrderAction();
                        if (i == SellTry - 1)
                        {
                            cp = calculateCount(si.StockID, si.Proportion, Surface.MyTimer.Data.HoldingValue, "SellLimit");
                            count = cp.Count;
                            price = cp.Price;
                            //Surface.WinfairTDAPI.Function.OrderInsert(si.StockID, price, count);
                            localHandleID = Surface.WinfairTDAPI.Function.TDOrderInsert();
                            Thread.Sleep(SellWaitTime);
                            if (orderStatus[localHandleID] == "ALLTRADED")
                            {
                                changeFlag = true;
                            }
                            if (sellFlag)
                            {
                                Console.WriteLine("Selling failed.");
                            }
                        }
                    }
                }
            }
        }

        private void dealWithOverTime(string stockID, double changingProportion, double value, int order)
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

        private void dealWithError(string stockID, double changingProportion, double value, int order)
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

        private void cashExchange()
        {
            // do nothing
        }

        private void ETFExchange()
        {
            // do not have this function
            throw new NotImplementedException();
        }

        private void stockExchange(string stockID, double changingProportion, double value, int order)
        {
            if (Surface.MyTimer.Data.MDDict.ContainsKey(stockID))
            {
                string optionStockID = "";
                if (order == 1)
                {
                    optionStockID = Surface.MyTimer.Data.OperationDict[stockID].OptionStockID1;
                    BuyInfo bi = new BuyInfo(Surface.MyTimer.Data.OperationDict[stockID], stockID, 1);
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

        private countAndPrice calculateCount(string stockID, double changingProportion, double value, string status)
        {
            // 如果是尝试买入
            // 使用当前价格自定义加档买入
            if (status == "Buy")
            {
                double lastPrice = Surface.MyTimer.Data.MDDict[stockID].LastPrice;
                double buyPrice = lastPrice + BuyAdditionalPrice;
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
                double lastPrice = Surface.MyTimer.Data.MDDict[stockID].LastPrice;
                double sellPrice = lastPrice - SellAdditionalPrice;
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
                double upperLimitPrice = Surface.MyTimer.Data.MDDict[stockID].UpperLimitPrice;
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
                double lowerLimitPrice = Surface.MyTimer.Data.MDDict[stockID].LowerLimitPrice;
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
                Surface.WinfairTDAPI.Function.TDQryOrdersToday();
                Thread.Sleep(300);
            }
        }
    }
}
