using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Concurrent;
using Surface.MyTimer;
using System.Threading.Tasks;
using System.IO;
using Surface.MyLogic;
using static Surface.Matlab.MAT;
using static Surface.MyLogic.Logic;
using static Surface.MyTimer.Data;
using static Surface.MyLogic.Struct;
using static Surface.WinfairMDAPI.Function;
using static Surface.WinfairTDAPI.Function;

namespace Surface
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public static Dictionary<string, string[]> dict = new Dictionary<string, string[]>();
        public static IntPtr g_hTDClient = new IntPtr();

        public Form1()
        {
            InitializeComponent();
            //HoldingDict.TryAdd("601611", new StockInfo { StockID = "601611", Proportion = 4.19, ExchangeID = "SSE", OptionStockID1 = "601718", OptionStockID2 = "600827", OptionStockID3 = "600820" });

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            show();
        }

        private const int freq = 1000;

        private delegate void toShowNoParam();
        private delegate void toShowBriefDict(List<BriefInfo> list);
        private delegate void toShowOptionDict(List<OptionInfo> list);

        public static string chosenStrategy;
        public static string alpha;
        private static int col_width = 100;
        private static int col_width2 = 245;
        private static int col_width3 = 183;




        private void showValue()
        {
            while (true)
            {
                toShowValue();
                Thread.Sleep(freq);
            }
        }

        private void toShowValue()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new toShowNoParam(toShowValue));
            }
            else
            {
                //label_OV.Text = Data.TargetValue.ToString();
                label_HV.Text = Data.HoldingValue.ToString();
                //label_IV.Text = Data.IndexValue.ToString();
                label_MV.Text = Data.MinusValue.ToString();
            }
        }

        public void showTarget()
        {
            while (true)
            {
                List<StockInfo> DictionaryToList = TargetDict.Values.ToList();
                List<BriefInfo> briefList = new List<BriefInfo>(); 
                foreach (StockInfo si in DictionaryToList)
                {
                    BriefInfo bi = new BriefInfo();
                    bi.StockID = si.StockID;
                    bi.ExchangeID = si.ExchangeID;
                    bi.Proportion = si.Proportion;
                    briefList.Add(bi);
                }
                showTarget(briefList);
                Thread.Sleep(freq);
            }
        }

        private void showTarget(List<BriefInfo> list)
        {
            
            if (this.InvokeRequired)
            {
                this.Invoke(new toShowBriefDict(showTarget), new object[] { list });
            }
            else
            {
                dataGridViewSingle.DataSource = list;
                dataGridViewSingle.Columns[0].HeaderText = "股票代码";
                dataGridViewSingle.Columns[0].Width = col_width;
                dataGridViewSingle.Columns[1].HeaderText = "比例（%）";
                dataGridViewSingle.Columns[1].Width = col_width;
                dataGridViewSingle.Columns[2].HeaderText = "交易所";
                dataGridViewSingle.Columns[2].Width = col_width;

                dataGridViewMulti.DataSource = list;
                dataGridViewMulti.Columns[0].HeaderText = "股票代码";
                dataGridViewMulti.Columns[0].Width = col_width;
                dataGridViewMulti.Columns[1].HeaderText = "比例（%）";
                dataGridViewMulti.Columns[1].Width = col_width;
                dataGridViewMulti.Columns[2].HeaderText = "交易所";
                dataGridViewMulti.Columns[2].Width = col_width;

                dataGridViewTarget.DataSource = list;
                dataGridViewTarget.Columns[0].HeaderText = "股票代码";
                dataGridViewTarget.Columns[0].Width = col_width2;
                dataGridViewTarget.Columns[1].HeaderText = "比例（%）";
                dataGridViewTarget.Columns[1].Width = col_width2;
                dataGridViewTarget.Columns[2].HeaderText = "交易所";
                dataGridViewTarget.Columns[2].Width = col_width2;
                dataGridViewTarget.ColumnHeadersHeight = 60;
                dataGridViewTarget.Columns[3].Visible = false;




                //dataGridViewTarget.Rows[0].MinimumHeight = 50;

            }
        }

        public void showTargetOption()
        {
            while (true)
            {
                List<StockInfo> DictionaryToList = TargetDict.Values.ToList();
                List<OptionInfo> OptionList = new List<OptionInfo>();
                foreach (StockInfo si in DictionaryToList)
                {
                    OptionInfo oi = new OptionInfo();
                    oi.StockID = si.StockID;
                    oi.OptionStockID1 = si.OptionStockID1;
                    oi.OptionStockID2 = si.OptionStockID2;
                    oi.OptionStockID3 = si.OptionStockID3;
                    OptionList.Add(oi);
                }
                showTargetOption(OptionList);
                Thread.Sleep(freq);
            }
        }

        private void showTargetOption(List<OptionInfo> list)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new toShowOptionDict(showTargetOption), new object[] { list });
            }
            else
            {

                dataGridViewOptionTarget.DataSource = list;
                dataGridViewOptionTarget.Columns[0].HeaderText = "股票代码";
                dataGridViewOptionTarget.Columns[0].Width = col_width3;
                dataGridViewOptionTarget.Columns[1].HeaderText = "替代股1";
                dataGridViewOptionTarget.Columns[1].Width = col_width3;
                dataGridViewOptionTarget.Columns[2].HeaderText = "替代股2";
                dataGridViewOptionTarget.Columns[2].Width = col_width3;
                dataGridViewOptionTarget.Columns[3].HeaderText = "替代股3";
                dataGridViewOptionTarget.Columns[3].Width = col_width3;
                dataGridViewOptionTarget.ColumnHeadersHeight = 60;






            }
        }

        public void showHolding()
        {
            while (true)
            {
                List<StockInfo> DictionaryToList = HoldingDict.Values.ToList();
                List<BriefInfo> briefList = new List<BriefInfo>();
                foreach (StockInfo si in DictionaryToList)
                {
                    BriefInfo bi = new BriefInfo();
                    bi.StockID = si.StockID;
                    bi.ExchangeID = si.ExchangeID;
                    bi.Proportion = si.Proportion;
                    briefList.Add(bi);
                }
                showHolding(briefList);
                Thread.Sleep(freq);
            }
        }

        private void showHolding(List<BriefInfo> list)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new toShowBriefDict(showHolding), new object[] { list });
            }
            else
            {
                dataGridViewHolding.DataSource = list;
                dataGridViewHolding.Columns[0].HeaderText = "股票代码";
                dataGridViewHolding.Columns[0].Width = col_width2;
                dataGridViewHolding.Columns[1].HeaderText = "比例（%）";
                dataGridViewHolding.Columns[1].Width = col_width2;
                dataGridViewHolding.Columns[2].HeaderText = "交易所";
                dataGridViewHolding.Columns[2].Width = col_width2;
                dataGridViewHolding.Columns[3].Visible = false;
                dataGridViewHolding.ColumnHeadersHeight = 60;

                dataGridViewWatch.DataSource = list;
                dataGridViewWatch.Columns[0].HeaderText = "股票代码";
                dataGridViewWatch.Columns[0].Width = col_width;
                dataGridViewWatch.Columns[1].HeaderText = "比例（%）";
                dataGridViewWatch.Columns[1].Width = col_width;
                dataGridViewWatch.Columns[2].HeaderText = "交易所";
                dataGridViewWatch.Columns[2].Width = col_width;
                dataGridViewWatch.Columns[3].Visible = false;
                dataGridViewWatch.ColumnHeadersHeight = 60;
            }
        }

        public void showHoldingOption()
        {
            while (true)
            {
                List<StockInfo> DictionaryToList = HoldingDict.Values.ToList();
                List<OptionInfo> OptionList = new List<OptionInfo>();
                foreach (StockInfo si in DictionaryToList)
                {
                    OptionInfo oi = new OptionInfo();
                    oi.StockID = si.StockID;
                    oi.OptionStockID1 = si.OptionStockID1;
                    oi.OptionStockID2 = si.OptionStockID2;
                    oi.OptionStockID3 = si.OptionStockID3;
                    OptionList.Add(oi);
                }
                showHoldingOption(OptionList);
                Thread.Sleep(freq);
            }
        }

        private void showHoldingOption(List<OptionInfo> list)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new toShowOptionDict(showHoldingOption), new object[] { list });
            }
            else
            {

                dataGridViewHoldingOption.DataSource = list;
                dataGridViewHoldingOption.Columns[0].HeaderText = "股票代码";
                dataGridViewHoldingOption.Columns[0].Width = col_width3;
                dataGridViewHoldingOption.Columns[1].HeaderText = "替代股1";
                dataGridViewHoldingOption.Columns[1].Width = col_width3;
                dataGridViewHoldingOption.Columns[2].HeaderText = "替代股2";
                dataGridViewHoldingOption.Columns[2].Width = col_width3;
                dataGridViewHoldingOption.Columns[3].HeaderText = "替代股3";
                dataGridViewHoldingOption.Columns[3].Width = col_width3;
                dataGridViewHoldingOption.ColumnHeadersHeight = 60;
            }
        }

        private void show()
        {
            Task pre = new Task(showValue);
            pre.Start();
            Task target = new Task(showTarget);
            target.Start();
            Task targetOption = new Task(showTargetOption);
            targetOption.Start();
            Task holding = new Task(showHolding);
            holding.Start();
            Task holdingOption = new Task(showHoldingOption);
            holdingOption.Start();
            Task system = new Task(showSystem);
            system.Start();
        }

        private void showSystem()
        {
            while (true)
            {
                toShowSystem();
                Thread.Sleep(freq);
            }
        }

        private void toShowSystem()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new toShowNoParam(toShowSystem));
            }
            else
            {
                log_Asyn_update();
                log_Syn_update();
            }
        }

        private void log_Asyn_update()
        {
            richTextBoxSystem.Text = Sys_log;
            richTextBoxError.Text = Er_log;
        }

        public void log_Syn_update()
        {
            richTextBoxStrategy.Text = Str_log;
        }

        public void value_update() // 市值等Value更新
        {
            label_HV.Text = g_HV.ToString();
            //label_OV.Text = g_OV.ToString();
            //label_IV.Text = g_IV.ToString();
            label_MV.Text = g_MV.ToString();
        }

        public static double g_HV, g_OV, g_IV, g_MV;  // Value
        public static int chosen_strategy;            // 目前选中策略
        public static String Sys_log = "", Str_log, Er_log;       // 日志

        private void btnStrategy_Click(object sender, EventArgs e)
        {
            Task t = new Task(startStrategy);
            if (btnStrategy.BackColor == Color.FromArgb(224, 224, 224)) // 未执行策略，要开始执行策略
            {
                btnStrategy.BackColor = Color.FromArgb(255, 128, 0);
                t.Wait();
            }
            else // 暂停策略
            {
                btnStrategy.BackColor = Color.FromArgb(255, 128, 0);
                t.Start();
            }
        }

        private void startStrategy()
        {
            //MDCreate();
            //MDSubscribe();

            SpreadToTransfer = Convert.ToDouble(textSpreadToTransfer.Text);
            AllWaitingTime = Convert.ToInt32(textWaitingTime.Text);
            BuyWaitTime = Convert.ToInt32(textBuyWaitingTime.Text);
            SellWaitTime = Convert.ToInt32(textSellWaitingTime.Text);
            switch (textBuyStrategy.Text)
            {
                case "WithCash":
                    StrategyToBuy = 0;
                    break;
                case "WithETF":
                    StrategyToBuy = 1;
                    break;
                case "WithOptionStock":
                    StrategyToBuy = 2;
                    break;
            }
            BuyPrice = (int)((PRICE_NAME)Enum.Parse(typeof(PRICE_NAME), basisSell.Text));
            SellPrice =(int)((PRICE_NAME)Enum.Parse(typeof(PRICE_NAME), basisSell.Text));
            BuyTry = Convert.ToInt32(textBuyTry.Text);
            SellTry = Convert.ToInt32(textSellTry.Text);
            BuyAdditionalPrice = Convert.ToDouble(textAddtionalBuy.Text) / 100;
            SellAdditionalPrice = Convert.ToDouble(textAdditionalSell.Text) / 100;

            //TargetDict
            getTarget();

            //OperationDict
            //var intersect = FormerDict.Keys.Intersect(TargetDict.Keys);
            //foreach (var item in intersect)
            //{
            //    StockInfo si = new StockInfo();
            //    si.StockID = item;
            //    si.Name = TargetDict[item].Name;
            //    si.Proportion = TargetDict[item].Proportion - FormerDict[item].Proportion;
            //    si.OptionStockID1 = TargetDict[item].OptionStockID1;
            //    si.OptionStockID2 = TargetDict[item].OptionStockID2;
            //    si.OptionStockID3 = TargetDict[item].OptionStockID3;
            //    OperationDict[si.StockID] = si;
            //}

            //var formerExceptTarget = FormerDict.Keys.Except(TargetDict.Keys);
            //foreach (var item in formerExceptTarget)
            //{
            //    StockInfo si = new StockInfo();
            //    si.StockID = item;
            //    si.Name = FormerDict[item].Name;
            //    si.Proportion = -FormerDict[item].Proportion;
            //    si.OptionStockID1 = FormerDict[item].OptionStockID1;
            //    si.OptionStockID2 = FormerDict[item].OptionStockID2;
            //    si.OptionStockID3 = FormerDict[item].OptionStockID3;
            //    OperationDict[si.StockID] = si;
            //}

            //var targetExceptFormer = TargetDict.Keys.Except(FormerDict.Keys);
            //foreach (var item in targetExceptFormer)
            //{
            //    StockInfo si = new StockInfo();
            //    si.StockID = item;
            //    si.Name = TargetDict[item].Name;
            //    si.Proportion = TargetDict[item].Proportion;
            //    si.OptionStockID1 = TargetDict[item].OptionStockID1;
            //    si.OptionStockID2 = TargetDict[item].OptionStockID2;
            //    si.OptionStockID3 = TargetDict[item].OptionStockID3;
            //    OperationDict[si.StockID] = si;
            //}

            OperationDict = TargetDict;
            #region changeholding
            int i = 0;
            Task[] tasks = new Task[OperationDict.Count];
            foreach (StockInfo item in OperationDict.Values)
            {
                if (item.Proportion > 0)
                {
                    BuyInfo bi = new BuyInfo(item, "", 0);
                    tasks[i] = new Task(buyStock, bi);
                    tasks[i].Start();
                }
                else
                {
                    tasks[i] = new Task(sellStock, item);
                    tasks[i].Start();
                }
                i++;
            }
            #endregion
        }

        // Set strategy
        // Set logic spec

        /*public static void action1()
        {
            while (true)
            {
                OrderInsert1();
                OrderInsert2();
                Thread.Sleep(1000);
            }
        }

        public static void OrderInsert1()
        {
            if ()
            {

            }
        }*/

        // update() 利用timer
        /*
         * public void sheet_update()
        {

        }
        */
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void richTextBoxError_TextChanged(object sender, EventArgs e)
        {

        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MDCreate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                MDSubscribe();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
               TDCreate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
            //Thread.Sleep(2000);
            try
            {
                TDConnect();

            }
            catch (Exception)
            {

                throw;
            }
            //Thread.Sleep(2000);
            //log_Syn_update();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TDDisconnect();
            log_Syn_update();
        }

        private void panelContainer3_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {

        }

        private void btnStartCal_multi_Click(object sender, EventArgs e)
        {
            // 开始运算
            chosenStrategy = textStrategyMulti.Text;
            alpha = textAlphaMulti.Text;
            getTarget();
            string fileName = "..//..//Matlab//pictures//" + chosenStrategy + "//" + chosenStrategy + "_" + alpha.PadLeft(2, '0') + ".png";
            FileStream fsRead = new FileStream(@fileName, FileMode.Open);
            pictureBoxMulti.Image = Image.FromStream(fsRead);
            fsRead.Close();
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            string fileName = "..//..//Matlab//pictures//Compare//" +comboBoxStrategy.Text +".png";
            FileStream fsRead = new FileStream(@fileName, FileMode.Open);
            pictureBoxWatch.Image = Image.FromStream(fsRead);
            fsRead.Close();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Task t = new Task(startStrategy);
            if (btnStrategy.BackColor == Color.FromArgb(224, 224, 224)) // 未执行策略，要开始执行策略
            {
                btnStrategy.BackColor = Color.FromArgb(255, 128, 0);
                t.Wait();
            }
            else // 暂停策略
            {
                btnStrategy.BackColor = Color.FromArgb(255, 128, 0);
                t.Start();
            }
        }

        private void btnStartCal_simple_Click(object sender, EventArgs e)
        {
            // 开始运算
            chosenStrategy = textStrategy.Text;
            alpha = textAlpha.Text;
            getTarget();
            string fileName = "..//..//Matlab//pictures//" + chosenStrategy + "//" + chosenStrategy + "_" +alpha.PadLeft(2, '0') + ".png";
            FileStream fsRead = new FileStream(@fileName, FileMode.Open);
            pictureBoxSingle.Image = Image.FromStream(fsRead);
            fsRead.Close();
        }

        private void btnSave_Simple_Click(object sender, EventArgs e)
        {
            //保存策略

        }

        private void dockPanel4_Click(object sender, EventArgs e)
        {

        }






        public static void timer1000()
        {
            while (true)
            {
                // Show HV OV IV MV
                Thread.Sleep(1000);
            }
        }

        public static void timer3000()
        {
            while (true)
                // Show H OH T OT
                Thread.Sleep(3000);
        }

    }


}
