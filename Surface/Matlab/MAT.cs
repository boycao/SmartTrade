using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using static Surface.MyTimer.Data;

namespace Surface.Matlab
{
    class MAT
    {
        private static int strategyToBuy = 0;

        public static int StrategyToBuy { get => strategyToBuy; set => strategyToBuy = value; }

        public static void getTarget()
        {

            string fileName = "..//..//Matlab//csv//" + Surface.Form1.chosenStrategy+".csv";
            FileStream fsRead = new FileStream(@fileName, FileMode.Open);
            StreamReader sr = new StreamReader(fsRead);
            string str = "";
            while ((str = sr.ReadLine()) != null)
            {
                string[] sArray = str.Split(',');
                String id = sArray[0].Substring(0,6);
                double pro = Convert.ToDouble(sArray[1]) * 100;
                pro = Double.Parse(pro.ToString("F2"));
                String id1 = sArray[2].Substring(0, 6);
                String id2 = sArray[3].Substring(0, 6);
                String id3 = sArray[4].Substring(0, 6);
                string exid = (id[0] == '6') ? "SSE" : "SZE";
                TargetDict[id] = new StockInfo() { StockID = id, Proportion = pro, ExchangeID = exid, OptionStockID1 = id1, OptionStockID2 = id2, OptionStockID3 = id3 };
            }
            fsRead.Close();
        }
    }
}
