using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Surface.WinfairTDAPI
{
    class Struct
    {
        public static string[] ORDER_STATUS_NAME = { "未报", "已报", "队列中", "部成", "撤单中", "部成撤单中", "已撤", "部成部撤", "已成", "失败", "未知" };
        public static string[] ORDER_DIRECTION_NAME = { "买入", "卖出", "融资买入", "融券卖出", "担保买入", "担保卖出", "未知", "场内认购", "场内申购", "场内赎回", "场外认购", "场外申购", "场外赎回", "ETF认购", "ETF申购", "ETF赎回" };
        public static string[] ORDER_OFFSET_NAME = { "开仓", "平仓", "平今", "强平", "未知" };

        // 未报，已报，队列中，部成，撤单中，部成撤单中，已撤，部成部撤，已成，失败，未知
        public enum ORDER_STATUS { UNSENT, SENDED, QUEUEING, PARTTRADED, CANCELING, PARTTRADED_CANCELING, CANCELED, PARTTRADED_CANCELED, ALLTRADED, FAILED, UNKNOWNSTATUS };
        public enum ORDER_DIRECTION { BID, ASK, MARGINBUY, SHORTSELL, COLLBUY, COLLSELL, UNKNOWNDIRECTION, LSTSUB, LSTPUR, LSTRDM, OTCSUB, OTCPUR, OTCRDM, ETFSUB, ETFPUR, ETFRDM };
        public enum ORDER_OFFSET { OPEN, CLOSE, CLOSETODAY, CLOSEBYFORCE, UNKNOWNOFFSET };

        
        [StructLayout(LayoutKind.Sequential, Size = 336), Serializable]
        public struct tagSTRUCT_INSTRUMENT
        {
            /// 合约代码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 31)]
            public string InstrumentID;

            /// 交易所代码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string ExchangeID;

            /// 合约名称
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 21)]
            public string InstrumentName;

            /// 合约在交易所的代码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 31)]
            public string ExchangeInstID;

            /// 产品代码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 31)]
            public string ProductID;

            /// 产品类型
            public byte ProductClass;

            /// 交割年份
            public int DeliveryYear;

            /// 交割月
            public int DeliveryMonth;

            /// 市价单最大下单量
            public int MaxMarketOrderVolume;

            /// 市价单最小下单量
            public int MinMarketOrderVolume;

            /// 限价单最大下单量
            public int MaxLimitOrderVolume;

            /// 限价单最小下单量
            public int MinLimitOrderVolume;

            /// 合约数量乘数
            public int VolumeMultiple;

            /// 最小变动价位
            public double PriceTick;

            /// 创建日
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string CreateDate;

            /// 上市日
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string OpenDate;

            /// 到期日
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string ExpireDate;

            /// 开始交割日
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string StartDelivDate;

            /// 结束交割日
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string EndDelivDate;

            /// 合约生命周期状态
            public byte InstLifePhase;

            /// 当前是否交易
            public int IsTrading;

            /// 持仓类型
            public byte PositionType;

            /// 报单能否撤单
            public int OrderCanBeWithdraw;

            /// 最小买下单单位
            public int MinBuyVolume;

            /// 最小卖下单单位
            public int MinSellVolume;

            /// 股票权限模版代码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 31)]
            public string RightModelID;

            /// 持仓交易类型
            public byte PosTradeType;

            /// 市场代码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 31)]
            public string MarketID;

            /// 期权执行价格
            public double ExecPrice;

            /// 期权单手保证金
            public double UnitMargin;

            /// 合约类型
            public byte InstrumentType;

            /// 持仓日期类型
            public byte PositionDateType;

            /// 多头保证金率
            public double LongMarginRatio;

            /// 空头保证金率
            public double ShortMarginRatio;
        }

        [StructLayout(LayoutKind.Sequential, Size = 432), Serializable]
        public struct tagSTRUCT_DEAL_FIXED
        {
            /// 成交编号
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szTradeID;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szTradingAdapterTag;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szStockPoolTag;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szFundID;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szCombiID;

            /// 成交日期
            public uint nTradeDate;

            /// 成交时间
            public uint nTradeTime;

            public uint dwUniqueID;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szTradingDay;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szExchangeID;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szOrderSysID;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szOrderLocalHandleID;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szOrderRef;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szIdentity;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szHandleID;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szInstrumentID;

            public ORDER_OFFSET ordOffset;

            public ORDER_DIRECTION ordDirection;

            public uint nDealVolume;

            public double dTotalAmount;

            public double dAveragePrice;
        }

        [StructLayout(LayoutKind.Sequential, Size = 904), Serializable]
        public struct tagSTRUCT_ORDER_FIXED
        {
            public ORDER_STATUS ordStatus;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szTradingAdapterTag;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szStockPoolTag;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szFundID;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szCombiID;

            public uint dwUniqueID;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szSessionID;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szFrontID;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szTradingDay;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szOrderRef;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szExchangeID;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szOrderSysID;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szOrderLocalID;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szIdentity;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szLocalHandleID;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szInstrumentID;

            public ORDER_OFFSET ordOffset;

            public ORDER_DIRECTION ordDirection;

            public byte cPriceType;

            public uint nOrderVolume;

            public double dOrderPrice;

            public uint nDealVolume;

            public double dTotalAmount;

            public double dAveragePrice;

            public uint nCanceledVolume;

            public uint nOrderDate;

            public uint nOrderTime;

            public uint nUpdateDate;

            public uint nUpdateTime;

            public tagSTRUCT_INSTRUMENT stInstrument;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30 * 2)]
            public string szErrorMsg;
        }

        [StructLayout(LayoutKind.Sequential, Size = 200), Serializable]
        public struct tagSTRUCT_CASH_FIXED
        {
            public int nCurrencyType;

            public double dTotalAmount;

            public double dAvaliableAmount;

            public double dFrozenAmount;

            public double dWithdrawableAmount;

            public double dMargin;

            public double dFuturesFloatingPL;

            public double dCommission;

            public double dTax;

            public double dFee;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szTradingAdapterTag;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szStockPoolTag;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szFundID;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szCombiID;

            /*
            tagSTRUCT_CASH_FIXED()
            {
                nCurrencyType=0;
                dTotalAmount=-1;
                dAvaliableAmount=-1;
                dFrozenAmount=-1;
                dWithdrawableAmount=-1;
                dMargin=0;
                dFuturesFloatingPL=0;
                dCommission=0;
                dTax=0;
                dFee=0;
                memset(szTradingAdapterTag,0,30);
                memset(szStockPoolTag,0,30);
                memset(szFundID,0,30);
                memset(szCombiID,0,30);
            }
            tagSTRUCT_CASH_FIXED& operator = (const tagSTRUCT_CASH_FIXED &rhs)
            {
                nCurrencyType=rhs.nCurrencyType;
                dTotalAmount=rhs.dTotalAmount;
                dAvaliableAmount=rhs.dAvaliableAmount;
                dFrozenAmount=rhs.dFrozenAmount;
                dWithdrawableAmount=rhs.dWithdrawableAmount;
                dMargin=rhs.dMargin;
                dFuturesFloatingPL=rhs.dFuturesFloatingPL;
                dCommission=rhs.dCommission;
                dTax=rhs.dTax;
                dFee=rhs.dFee;
                strcpy_s(szTradingAdapterTag,30,rhs.szTradingAdapterTag);
                strcpy_s(szStockPoolTag,30,rhs.szStockPoolTag);
                strcpy_s(szFundID,30,rhs.szFundID);
                strcpy_s(szCombiID,30,rhs.szCombiID);
                return *this;
            }
            */
        }

        [StructLayout(LayoutKind.Sequential, Size = 352), Serializable]
        public struct tagSTRUCT_HOLDING_FIXED
        {
            /// 交易所
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szExchangeID;

            /// 证券代码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szCode;

            /// 证券名称
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szName;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szTradingAdapterTag;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szStockPoolTag;

            /// 组合ID
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szCombiID;

            /// 基金产品ID
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szFundID;

            /// 证券类型
            public int nSecuType;

            /// 方向
            public ORDER_DIRECTION ordDirection;

            /// 总量
            public int nTotalVolume;

            /// 可用量
            public int nAvailableVolume;

            /// 买入冻结量
            public int nBuyFrozenVol;

            /// 卖出冻结量
            public int nSellFrozenVol;

            /// 市值
            public double dMarketValue;

            /// 当前价
            public double dCurrentPrice;

            /// 平均成本价
            public double dCostPrice;

            /// 浮动盈亏
            public double dFloatingPL;

            /// 浮动收益率
            public double dFloatingReturn;

            /// 收益率
            public double dReturn;

            /// 平均平仓价
            public double dClosePrice;

            /// 占用保证金
            public double dMargin;

            /// 平仓盈亏
            public double dClosedPL;

            /// 今买量
            public int nBuyVolToday;

            /// 今卖量
            public int nSellVolToday;

            public int nOpenedVolume;

            public double dAverageOpenPrice;

            public int nClosedVolume;

            public double dAverageClosePrice;
        }
    }
}
