using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Surface.MyLogic
{
    class Struct
    {
        public struct countAndPrice
        {
            private int count;
            private double price;

            public int Count { get => count; set => count = value; }
            public double Price { get => price; set => price = value; }
        }

        public class BuyInfo
        {
            private Surface.MyTimer.Data.StockInfo si;
            private string _stockID;
            private int order;

            internal Surface.MyTimer.Data.StockInfo Si { get => si; set => si = value; }
            public string StockID { get => _stockID; set => _stockID = value; }
            public int Order { get => order; set => order = value; }

            public BuyInfo(Surface.MyTimer.Data.StockInfo si, string _stock, int order = 0)
            {
                this.Si = si;
                this.StockID = _stock;
                this.Order = order;
            }
        }
    }
}
