using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Surface.WinfairMDAPI
{
    class Struct
    {

        [StructLayout(LayoutKind.Sequential, Size = 102), Serializable]
        public struct CSecurityMyQryInstrumentField
        {
            ///合约代码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 31)]
            public string InstrumentID;

            ///交易所代码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string ExchangeID;

            ///合约在交易所的代码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 31)]
            public string ExchangeInstID;

            ///产品代码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 31)]
            public string ProductID;

        }


        [StructLayout(LayoutKind.Sequential, Size = 744), Serializable]
        public struct tagCThostMyDepthMarketDataFieldEx
        {
            ///交易日
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string TradingDay;

            ///合约代码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 31)]
            public string InstrumentID;

            ///交易所代码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string ExchangeID;

            ///合约在交易所的代码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 31)]
            public string ExchangeInstID;

            ///最新价
            public double LastPrice;

            ///上次结算价
            public double PreSettlementPrice;

            ///昨收盘
            public double PreClosePrice;

            ///昨持仓量
            public double PreOpenInterest;

            ///今开盘
            public double OpenPrice;

            ///最高价
            public double HighestPrice;

            ///最低价
            public double LowestPrice;

            ///数量
            public int Volume;

            ///成交金额
            public double Turnover;

            ///持仓量
            public double OpenInterest;

            ///今收盘
            public double ClosePrice;

            ///本次结算价
            public double SettlementPrice;

            ///涨停板价
            public double UpperLimitPrice;

            ///跌停板价
            public double LowerLimitPrice;

            ///昨虚实度
            public double PreDelta;

            ///今虚实度
            public double CurrDelta;

            ///最后修改时间
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string UpdateTime;

            ///最后修改毫秒
            public int UpdateMillisec;

            ///申买价一
            public double BidPrice1;

            ///申买量一
            public int BidVolume1;

            ///申卖价一
            public double AskPrice1;

            ///申卖量一
            public int AskVolume1;

            ///申买价二
            public double BidPrice2;

            ///申买量二
            public int BidVolume2;

            ///申卖价二
            public double AskPrice2;

            ///申卖量二
            public int AskVolume2;

            ///申买价三
            public double BidPrice3;

            ///申买量三
            public int BidVolume3;

            ///申卖价三
            public double AskPrice3;

            ///申卖量三
            public int AskVolume3;

            ///申买价四
            public double BidPrice4;

            ///申买量四
            public int BidVolume4;

            ///申卖价四
            public double AskPrice4;

            ///申卖量四
            public int AskVolume4;

            ///申买价五
            public double BidPrice5;

            ///申买量五
            public int BidVolume5;

            ///申卖价五
            public double AskPrice5;

            ///申卖量五
            public int AskVolume5;

            ///当日均价
            public double AveragePrice;

            ///合约名称
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 21)]
            public string InstrumentName;

            ///净值估值
            public double IOPV;

            ///到期收益率
            public double YieldToMaturity;

            ///动态参考价格
            public double AuctionPrice;

            ///交易阶段
            public byte TradingPhase;

            ///开仓限制
            public byte OpenRestriction;

            ///成交笔数
            public int TradeCount;

            ///委托买入总量
            public int TotalBidVolume;

            ///加权平均委买价
            public double WeightedAvgBidPrice;

            ///债券加权平均委买价
            public double AltWeightedAvgBidPrice;

            ///委托卖出总量
            public int TotalAskVolume;

            ///加权平均委卖价
            public double WeightedAvgAskPrice;

            ///债券加权平均委卖价格
            public double AltWeightedAvgAskPrice;

            ///买价深度
            public int BidPriceLevel;

            ///卖价深度
            public int AskPriceLevel;

            ///实际买总委托笔数一
            public int BidCount1;

            ///实际买总委托笔数二
            public int BidCount2;

            ///实际买总委托笔数三
            public int BidCount3;

            ///实际买总委托笔数四
            public int BidCount4;

            ///实际买总委托笔数五
            public int BidCount5;

            ///申买价六
            public double BidPrice6;

            ///申买量六
            public int BidVolume6;

            ///实际买总委托笔数六
            public int BidCount6;

            ///申买价七
            public double BidPrice7;

            ///申买量七
            public int BidVolume7;

            ///实际买总委托笔数七
            public int BidCount7;

            ///申买价八
            public double BidPrice8;

            ///申买量八
            public int BidVolume8;

            ///实际买总委托笔数八
            public int BidCount8;

            ///申买价九
            public double BidPrice9;

            ///申买量九
            public int BidVolume9;

            ///实际买总委托笔数九
            public int BidCount9;

            ///申买价十
            public double BidPriceA;

            ///申买量十
            public int BidVolumeA;

            ///实际买总委托笔数十
            public int BidCountA;

            ///实际卖总委托笔数一
            public int AskCount1;

            ///实际卖总委托笔数二
            public int AskCount2;

            ///实际卖总委托笔数三
            public int AskCount3;

            ///实际卖总委托笔数四
            public int AskCount4;

            ///实际卖总委托笔数五
            public int AskCount5;

            ///申卖价六
            public double AskPrice6;

            ///申卖量六
            public int AskVolume6;

            ///实际卖总委托笔数六
            public int AskCount6;

            ///申卖价七
            public double AskPrice7;

            ///申卖量七
            public int AskVolume7;

            ///实际卖总委托笔数七
            public int AskCount7;

            ///申卖价八
            public double AskPrice8;

            ///申卖量八
            public int AskVolume8;

            ///实际卖总委托笔数八
            public int AskCount8;

            ///申卖价九
            public double AskPrice9;

            ///申卖量九
            public int AskVolume9;

            ///实际卖总委托笔数九
            public int AskCount9;

            ///申卖价十
            public double AskPriceA;

            ///申卖量十
            public int AskVolumeA;

            ///实际卖总委托笔数十
            public int AskCountA;

            ///数据来源
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 31)]
            public string SourceTag;
        }

        [StructLayout(LayoutKind.Sequential, Size = 336), Serializable]
        public struct tagSTRUCT_INSTRUMENT
        {
            ///合约代码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 31)]
            public string InstrumentID;

            ///交易所代码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string ExchangeID;

            ///合约名称
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 21)]
            public string InstrumentName;

            ///合约在交易所的代码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 31)]
            public string ExchangeInstID;

            ///产品代码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 31)]
            public string ProductID;

            ///产品类型
            public byte ProductClass;

            ///交割年份
            public int DeliveryYear;

            ///交割月
            public int DeliveryMonth;

            ///市价单最大下单量
            public int MaxMarketOrderVolume;

            ///市价单最小下单量
            public int MinMarketOrderVolume;

            ///限价单最大下单量
            public int MaxLimitOrderVolume;

            ///限价单最小下单量
            public int MinLimitOrderVolume;

            ///合约数量乘数
            public int VolumeMultiple;

            ///最小变动价位
            public double PriceTick;

            ///创建日
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string CreateDate;

            ///上市日
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string OpenDate;

            ///到期日
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string ExpireDate;

            ///开始交割日
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string StartDelivDate;

            ///结束交割日
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string EndDelivDate;

            ///合约生命周期状态
            public byte InstLifePhase;

            ///当前是否交易
            public int IsTrading;

            ///持仓类型
            public byte PositionType;

            ///报单能否撤单
            public int OrderCanBeWithdraw;

            ///最小买下单单位
            public int MinBuyVolume;

            ///最小卖下单单位
            public int MinSellVolume;

            ///股票权限模版代码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 31)]
            public string RightModelID;

            ///持仓交易类型
            public byte PosTradeType;

            ///市场代码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 31)]
            public string MarketID;

            ///期权执行价格
            public double ExecPrice;

            ///期权单手保证金
            public double UnitMargin;

            ///合约类型
            public byte InstrumentType;

            ///持仓日期类型
            public byte PositionDateType;

            ///多头保证金率
            public double LongMarginRatio;

            ///空头保证金率
            public double ShortMarginRatio;
        }
    }
}
