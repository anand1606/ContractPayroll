USE [KOSI_ATTENDANCE]
GO
/****** Object:  Table [dbo].[Cont_DailyOth]    Script Date: 23/02/2018 18:23:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cont_DailyOth](
	[PayPeriod] [int] NOT NULL,
	[EmpUnqID] [nvarchar](10) NOT NULL,
	[tDate] [date] NOT NULL,
	[SrNo] [int] NOT NULL,
	[LeaveTyp] [nvarchar](3) NULL,
	[ABPR] [varchar](1) NOT NULL,
	[WrkHrs] [float] NOT NULL,
	[TpaHrs] [float] NOT NULL,
	[CBasic] [float] NOT NULL,
	[DaysPay] [int] NOT NULL,
	[Cal_Basic] [float] NOT NULL,
	[TpaAmt] [float] NOT NULL,
	[CostCode] [varchar](50) NULL,
	[CoCommRate] [float] NOT NULL,
	[CoCommAmt] [float] NOT NULL,
	[WODays] [int] NOT NULL,
	[CoCommWORate] [float] NOT NULL,
	[CoCommWOAmt] [float] NOT NULL,
	[AddDt] [datetime] NULL,
	[AddId] [varchar](50) NULL,
	[UpdDt] [datetime] NULL,
	[UpdId] [varchar](50) NULL,
 CONSTRAINT [PK_Cont_DailyOth] PRIMARY KEY CLUSTERED 
(
	[PayPeriod] ASC,
	[EmpUnqID] ASC,
	[tDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cont_MastBasic]    Script Date: 23/02/2018 18:23:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cont_MastBasic](
	[PayPeriod] [int] NOT NULL,
	[EmpUnqID] [nvarchar](10) NOT NULL,
	[SrNo] [int] NOT NULL,
	[FromDt] [date] NULL,
	[ToDt] [date] NULL,
	[cBasic] [float] NULL,
	[AddDt] [datetime] NULL,
	[Addid] [nvarchar](50) NULL,
	[UpdDt] [datetime] NULL,
	[UpdId] [nvarchar](50) NULL,
 CONSTRAINT [PK_Cont_MastBasic] PRIMARY KEY CLUSTERED 
(
	[PayPeriod] ASC,
	[EmpUnqID] ASC,
	[SrNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cont_MastEmp]    Script Date: 23/02/2018 18:23:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cont_MastEmp](
	[PayPeriod] [int] NOT NULL,
	[EmpUnqID] [nvarchar](10) NOT NULL,
	[EmpName] [nvarchar](100) NULL,
	[FatherName] [nvarchar](100) NULL,
	[BirthDt] [date] NULL,
	[JoinDt] [date] NULL,
	[Gender] [char](1) NOT NULL,
	[UnitCode] [nvarchar](3) NULL,
	[UnitDesc] [nvarchar](100) NULL,
	[DeptCode] [nvarchar](3) NULL,
	[DeptDesc] [varchar](100) NULL,
	[StatCode] [nvarchar](3) NULL,
	[StatDesc] [varchar](100) NULL,
	[DesgCode] [nvarchar](3) NULL,
	[DesgDesc] [varchar](100) NULL,
	[GradeCode] [nvarchar](3) NULL,
	[GradeDesc] [varchar](100) NULL,
	[CatCode] [nvarchar](3) NULL,
	[CatDesc] [varchar](100) NULL,
	[ContCode] [nvarchar](3) NULL,
	[ContDesc] [nvarchar](100) NULL,
	[ESINo] [varchar](100) NULL,
	[PFNo] [varchar](50) NULL,
	[Active] [bit] NOT NULL,
	[PFFlg] [bit] NOT NULL,
	[PTaxFlg] [bit] NOT NULL,
	[ESIFlg] [bit] NOT NULL,
	[LWFFlg] [bit] NOT NULL,
	[DeathFlg] [bit] NOT NULL,
	[CBasic] [float] NOT NULL,
	[LeftDt] [date] NULL,
	[AddDt] [datetime] NULL,
	[Addid] [varchar](50) NULL,
	[UpdDt] [datetime] NULL,
	[UpdID] [varchar](50) NULL,
 CONSTRAINT [PK_Cont_MastEmp] PRIMARY KEY CLUSTERED 
(
	[PayPeriod] ASC,
	[EmpUnqID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cont_MastFrm]    Script Date: 23/02/2018 18:23:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cont_MastFrm](
	[FormID] [int] NOT NULL,
	[SeqID] [int] NOT NULL,
	[FormName] [varchar](50) NOT NULL,
	[MenuName] [varchar](50) NULL,
	[FormDesc] [varchar](50) NULL,
	[SPRightsFlg] [bit] NOT NULL,
 CONSTRAINT [PK_Cont_FrmMast] PRIMARY KEY CLUSTERED 
(
	[FormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cont_MastPayPeriod]    Script Date: 23/02/2018 18:23:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cont_MastPayPeriod](
	[PayPeriod] [int] NOT NULL,
	[PayDesc] [varchar](100) NOT NULL,
	[FromDt] [date] NOT NULL,
	[ToDt] [date] NOT NULL,
	[isLocked] [bit] NOT NULL,
	[AddDt] [datetime] NULL,
	[Addid] [varchar](50) NULL,
	[UpdDt] [datetime] NULL,
	[UpdID] [varchar](50) NULL,
 CONSTRAINT [PK_Cont_MastPayPeriod] PRIMARY KEY CLUSTERED 
(
	[PayPeriod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cont_MastUser]    Script Date: 23/02/2018 18:23:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cont_MastUser](
	[UserID] [nvarchar](50) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[pass] [varchar](100) NULL,
	[Active] [bit] NOT NULL,
	[isAdmin] [bit] NOT NULL,
	[adddt] [smalldatetime] NULL,
	[addid] [nvarchar](10) NULL,
	[upddt] [smalldatetime] NULL,
	[updid] [nvarchar](10) NULL,
 CONSTRAINT [PK_Cont_UserMast] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cont_MthlyAtn]    Script Date: 23/02/2018 18:23:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cont_MthlyAtn](
	[PayPeriod] [int] NOT NULL,
	[EmpUnqID] [nvarchar](10) NOT NULL,
	[SrNo] [int] NOT NULL,
	[Adj_Basic] [float] NOT NULL,
	[Adj_TPAHrs] [float] NOT NULL,
	[Adj_TPAAmt]  AS (([ADJ_Basic]/(8))*[ADJ_TPAHrs]),
	[Adj_DaysPay] [float] NOT NULL,
	[Adj_DaysPayAmt]  AS ([ADJ_Basic]*[ADJ_DaysPay]),
	[Adj_Amt] [float] NOT NULL,
	[Adj_Remarks] [varchar](100) NULL,
	[Cal_Basic] [float] NOT NULL,
	[Cal_DaysPay] [float] NOT NULL,
	[Cal_WODays] [float] NOT NULL,
	[Cal_TpaHrs] [float] NOT NULL,
	[Cal_TpaAmt] [float] NOT NULL,
	[Tot_DaysPay] [float] NOT NULL,
	[Tot_EarnBasic] [float] NOT NULL,
	[Tot_TpaHrs] [float] NOT NULL,
	[Tot_TpaAmt] [float] NOT NULL,
	[Tot_Earnings] [float] NOT NULL,
	[Cal_PF] [float] NOT NULL,
	[Cal_EPF] [float] NOT NULL,
	[Cal_EPS] [float] NOT NULL,
	[CoCommRate] [float] NOT NULL,
	[Adj_CoCommDays] [float] NOT NULL,
	[Cal_CoCommDays] [float] NOT NULL,
	[Cal_CoWoCommDays] [float] NOT NULL,
	[Tot_CoCommDays] [float] NOT NULL,
	[Adj_CoCommAmt] [float] NOT NULL,
	[Cal_CoCommAmt] [float] NOT NULL,
	[Cal_CoCommWoAmt] [float] NOT NULL,
	[Cal_CoCommPFAmt] [float] NOT NULL,
	[Tot_CoCommAmt] [float] NOT NULL,
	[Cal_CoServTaxAmt] [float] NOT NULL,
	[Cal_CoEduTaxAmt] [float] NOT NULL,
	[AddDt] [datetime] NULL,
	[AddID] [varchar](50) NULL,
	[UpdDt] [datetime] NULL,
	[UpdID] [varchar](50) NULL,
 CONSTRAINT [PK_Cont_MthlyAtn] PRIMARY KEY CLUSTERED 
(
	[PayPeriod] ASC,
	[EmpUnqID] ASC,
	[SrNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cont_MthlyDed]    Script Date: 23/02/2018 18:23:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cont_MthlyDed](
	[PayPeriod] [int] NOT NULL,
	[EmpUnqID] [nvarchar](10) NOT NULL,
	[DedCode] [nvarchar](50) NOT NULL,
	[Amount] [float] NOT NULL,
	[AddDt] [datetime] NOT NULL,
	[AddID] [nvarchar](50) NOT NULL,
	[UpdDt] [datetime] NULL,
	[UpdId] [nvarchar](50) NULL,
 CONSTRAINT [PK_Cont_MthlyDed] PRIMARY KEY CLUSTERED 
(
	[PayPeriod] ASC,
	[EmpUnqID] ASC,
	[DedCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cont_MthlyPay]    Script Date: 23/02/2018 18:23:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cont_MthlyPay](
	[PayPeriod] [int] NOT NULL,
	[EmpUnqID] [nvarchar](10) NOT NULL,
	[Adj_TPAHrs] [float] NOT NULL,
	[Adj_TPAAmt] [float] NOT NULL,
	[Adj_DaysPay] [float] NOT NULL,
	[Adj_DaysPayAmt] [float] NOT NULL,
	[Adj_Amt] [float] NOT NULL,
	[Cal_Basic] [float] NOT NULL,
	[Cal_DaysPay] [float] NOT NULL,
	[Cal_WODays] [float] NOT NULL,
	[Cal_TpaHrs] [float] NOT NULL,
	[Cal_TpaAmt] [float] NOT NULL,
	[Tot_DaysPay] [float] NOT NULL,
	[Tot_EarnBasic] [float] NOT NULL,
	[Tot_TpaHrs] [float] NOT NULL,
	[Tot_TpaAmt] [float] NOT NULL,
	[Tot_Earnings] [float] NOT NULL,
	[Ded_PF] [float] NOT NULL,
	[Cal_EPF] [float] NOT NULL,
	[Cal_EPS] [float] NOT NULL,
	[Ded_ESI] [float] NOT NULL,
	[Ded_LWF] [float] NOT NULL,
	[Ded_DeathFund] [float] NOT NULL,
	[Ded_Other] [float] NOT NULL,
	[Ded_Mess] [float] NOT NULL,
	[Ded_PTax] [float] NOT NULL,
	[Tot_Ded] [float] NOT NULL,
	[NetPay] [float] NOT NULL,
	[Tot_CoCommDays] [float] NOT NULL,
	[Tot_CoCommAmt] [float] NOT NULL,
	[Tot_CoCommPFAmt] [float] NOT NULL,
	[Tot_CoComm] [float] NOT NULL,
	[Tot_CoServTax] [float] NOT NULL,
	[Tot_CoEduTax] [float] NOT NULL,
	[AddDt] [datetime] NULL,
	[AddID] [varchar](50) NULL,
	[UpdDt] [datetime] NULL,
	[UpdID] [varchar](50) NULL,
 CONSTRAINT [PK_Cont_MthlyPay] PRIMARY KEY CLUSTERED 
(
	[PayPeriod] ASC,
	[EmpUnqID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cont_ParaMast]    Script Date: 23/02/2018 18:23:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cont_ParaMast](
	[PayPeriod] [int] NOT NULL,
	[ParaCode] [nvarchar](10) NOT NULL,
	[ParaDesc] [nvarchar](200) NOT NULL,
	[RsPer] [char](1) NULL,
	[PValue] [numeric](18, 2) NULL,
	[FSlab] [numeric](18, 0) NULL,
	[TSlab] [numeric](18, 0) NULL,
	[BCFLG] [bit] NOT NULL,
	[AppFlg] [bit] NOT NULL,
	[AddDt] [datetime] NULL,
	[AddID] [varchar](50) NULL,
	[UpdDt] [datetime] NULL,
	[UpdID] [varchar](50) NULL,
 CONSTRAINT [PK_Cont_ParaMast] PRIMARY KEY CLUSTERED 
(
	[PayPeriod] ASC,
	[ParaCode] ASC,
	[ParaDesc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cont_UserRights]    Script Date: 23/02/2018 18:23:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cont_UserRights](
	[UserID] [nvarchar](10) NOT NULL,
	[FormID] [int] NOT NULL,
	[Add1] [bit] NOT NULL,
	[Update1] [bit] NOT NULL,
	[Delete1] [bit] NOT NULL,
	[View1] [bit] NOT NULL,
	[Adddt] [smalldatetime] NULL,
	[AddID] [nvarchar](10) NULL,
	[UpdDT] [smalldatetime] NULL,
	[UpdID] [nvarchar](10) NULL,
 CONSTRAINT [PK_Cont_UserRights_1] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[FormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Cont_DailyOth] ADD  CONSTRAINT [DF_Cont_DailyOth_SrNo]  DEFAULT ((0)) FOR [SrNo]
GO
ALTER TABLE [dbo].[Cont_DailyOth] ADD  CONSTRAINT [DF_Cont_DailyOth_ABPR]  DEFAULT ('A') FOR [ABPR]
GO
ALTER TABLE [dbo].[Cont_DailyOth] ADD  CONSTRAINT [DF_Table_1_TPA]  DEFAULT ((0)) FOR [TpaHrs]
GO
ALTER TABLE [dbo].[Cont_DailyOth] ADD  CONSTRAINT [DF_Cont_DailyOth_CBASIC]  DEFAULT ((0)) FOR [CBasic]
GO
ALTER TABLE [dbo].[Cont_DailyOth] ADD  CONSTRAINT [DF_Cont_DailyOth_DAYSPAY]  DEFAULT ((0)) FOR [DaysPay]
GO
ALTER TABLE [dbo].[Cont_DailyOth] ADD  CONSTRAINT [DF_Cont_DailyOth_Cal_Basic]  DEFAULT ((0)) FOR [Cal_Basic]
GO
ALTER TABLE [dbo].[Cont_DailyOth] ADD  CONSTRAINT [DF_Table_1_TPAAMT]  DEFAULT ((0)) FOR [TpaAmt]
GO
ALTER TABLE [dbo].[Cont_DailyOth] ADD  CONSTRAINT [DF_Cont_DailyOth_COCOMM]  DEFAULT ((0)) FOR [CoCommRate]
GO
ALTER TABLE [dbo].[Cont_DailyOth] ADD  CONSTRAINT [DF_Cont_DailyOth_CoCommAmt]  DEFAULT ((0)) FOR [CoCommAmt]
GO
ALTER TABLE [dbo].[Cont_DailyOth] ADD  CONSTRAINT [DF_Table_1_WODAYS]  DEFAULT ((0)) FOR [WODays]
GO
ALTER TABLE [dbo].[Cont_DailyOth] ADD  CONSTRAINT [DF_Cont_DailyOth_CoCommWORate]  DEFAULT ((0)) FOR [CoCommWORate]
GO
ALTER TABLE [dbo].[Cont_DailyOth] ADD  CONSTRAINT [DF_Cont_DailyOth_CoCommWOAmt]  DEFAULT ((0)) FOR [CoCommWOAmt]
GO
ALTER TABLE [dbo].[Cont_MastEmp] ADD  CONSTRAINT [DF_Cont_MastEmp_Gender]  DEFAULT ('M') FOR [Gender]
GO
ALTER TABLE [dbo].[Cont_MastEmp] ADD  CONSTRAINT [DF_Cont_MastEmp_Active]  DEFAULT ((0)) FOR [Active]
GO
ALTER TABLE [dbo].[Cont_MastEmp] ADD  CONSTRAINT [DF_Cont_MastEmp_PFFlg]  DEFAULT ((1)) FOR [PFFlg]
GO
ALTER TABLE [dbo].[Cont_MastEmp] ADD  CONSTRAINT [DF_Cont_MastEmp_PTaxFlg]  DEFAULT ((1)) FOR [PTaxFlg]
GO
ALTER TABLE [dbo].[Cont_MastEmp] ADD  CONSTRAINT [DF_Cont_MastEmp_ESIFlg]  DEFAULT ((1)) FOR [ESIFlg]
GO
ALTER TABLE [dbo].[Cont_MastEmp] ADD  CONSTRAINT [DF_Cont_MastEmp_LWFFlg]  DEFAULT ((1)) FOR [LWFFlg]
GO
ALTER TABLE [dbo].[Cont_MastEmp] ADD  CONSTRAINT [DF_Cont_MastEmp_DeathFlg]  DEFAULT ((1)) FOR [DeathFlg]
GO
ALTER TABLE [dbo].[Cont_MastEmp] ADD  CONSTRAINT [DF_Cont_MastEmp_CBasic]  DEFAULT ((0)) FOR [CBasic]
GO
ALTER TABLE [dbo].[Cont_MastFrm] ADD  CONSTRAINT [DF_Cont_MastFrm_SPRightsFlg]  DEFAULT ((0)) FOR [SPRightsFlg]
GO
ALTER TABLE [dbo].[Cont_MastPayPeriod] ADD  CONSTRAINT [DF_Cont_MastPayPeriod_isLocked]  DEFAULT ((0)) FOR [isLocked]
GO
ALTER TABLE [dbo].[Cont_MastUser] ADD  CONSTRAINT [DF_Cont_MastUser_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Cont_MastUser] ADD  CONSTRAINT [DF_Cont_MastUser_isAdmin]  DEFAULT ((0)) FOR [isAdmin]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_ADJ_Basic]  DEFAULT ((0)) FOR [Adj_Basic]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_ADJ_TPAHrs]  DEFAULT ((0)) FOR [Adj_TPAHrs]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_ADJ_DaysPay]  DEFAULT ((0)) FOR [Adj_DaysPay]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_ADJ_Amt]  DEFAULT ((0)) FOR [Adj_Amt]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_CalBasic]  DEFAULT ((0)) FOR [Cal_Basic]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Cal_DaysPay]  DEFAULT ((0)) FOR [Cal_DaysPay]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Cal_WODays]  DEFAULT ((0)) FOR [Cal_WODays]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Cal_TpaHrs]  DEFAULT ((0)) FOR [Cal_TpaHrs]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Cal_TpaAmt]  DEFAULT ((0)) FOR [Cal_TpaAmt]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Tot_DaysPay]  DEFAULT ((0)) FOR [Tot_DaysPay]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Tot_EarnBasic]  DEFAULT ((0)) FOR [Tot_EarnBasic]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Tot_TpaHrs]  DEFAULT ((0)) FOR [Tot_TpaHrs]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Tot_TpaAmt]  DEFAULT ((0)) FOR [Tot_TpaAmt]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Tot_Earnings]  DEFAULT ((0)) FOR [Tot_Earnings]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Cal_PF]  DEFAULT ((0)) FOR [Cal_PF]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Cal_EPF]  DEFAULT ((0)) FOR [Cal_EPF]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Cal_EPS]  DEFAULT ((0)) FOR [Cal_EPS]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_CoCommRate]  DEFAULT ((0)) FOR [CoCommRate]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Adj_CoCommDays]  DEFAULT ((0)) FOR [Adj_CoCommDays]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Cal_CoCommDays]  DEFAULT ((0)) FOR [Cal_CoCommDays]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Cal_CoWoCommDays]  DEFAULT ((0)) FOR [Cal_CoWoCommDays]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Tot_CoCommDays]  DEFAULT ((0)) FOR [Tot_CoCommDays]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Adj_CoCommAmt]  DEFAULT ((0)) FOR [Adj_CoCommAmt]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Cal_CoComm]  DEFAULT ((0)) FOR [Cal_CoCommAmt]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Cal_CoWoComm]  DEFAULT ((0)) FOR [Cal_CoCommWoAmt]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Cal_CoCommPFAmt]  DEFAULT ((0)) FOR [Cal_CoCommPFAmt]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Tot_CoCommAmt]  DEFAULT ((0)) FOR [Tot_CoCommAmt]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Cal_CoServTaxAmt]  DEFAULT ((0)) FOR [Cal_CoServTaxAmt]
GO
ALTER TABLE [dbo].[Cont_MthlyAtn] ADD  CONSTRAINT [DF_Cont_MthlyAtn_Cal_CoEduTaxAmt]  DEFAULT ((0)) FOR [Cal_CoEduTaxAmt]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_ADJ_TPAHrs]  DEFAULT ((0)) FOR [Adj_TPAHrs]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Adj_TPAAmt]  DEFAULT ((0)) FOR [Adj_TPAAmt]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_ADJ_DaysPay]  DEFAULT ((0)) FOR [Adj_DaysPay]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Adj_DaysPayAmt]  DEFAULT ((0)) FOR [Adj_DaysPayAmt]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_ADJ_Amt]  DEFAULT ((0)) FOR [Adj_Amt]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_CalBasic]  DEFAULT ((0)) FOR [Cal_Basic]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Cal_DaysPay]  DEFAULT ((0)) FOR [Cal_DaysPay]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Cal_WODays]  DEFAULT ((0)) FOR [Cal_WODays]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Cal_TpaHrs]  DEFAULT ((0)) FOR [Cal_TpaHrs]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Cal_TpaAmt]  DEFAULT ((0)) FOR [Cal_TpaAmt]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Tot_DaysPay]  DEFAULT ((0)) FOR [Tot_DaysPay]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Tot_EarnBasic]  DEFAULT ((0)) FOR [Tot_EarnBasic]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Tot_TpaHrs]  DEFAULT ((0)) FOR [Tot_TpaHrs]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Tot_TpaAmt]  DEFAULT ((0)) FOR [Tot_TpaAmt]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Tot_Earnings]  DEFAULT ((0)) FOR [Tot_Earnings]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Cal_PF]  DEFAULT ((0)) FOR [Ded_PF]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Cal_EPF]  DEFAULT ((0)) FOR [Cal_EPF]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Cal_EPS]  DEFAULT ((0)) FOR [Cal_EPS]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Cal_ESI]  DEFAULT ((0)) FOR [Ded_ESI]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Cal_LWF]  DEFAULT ((0)) FOR [Ded_LWF]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Ded_DeathFund]  DEFAULT ((0)) FOR [Ded_DeathFund]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Del_OtherDed]  DEFAULT ((0)) FOR [Ded_Other]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Ded_Mess]  DEFAULT ((0)) FOR [Ded_Mess]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Ded_PTax]  DEFAULT ((0)) FOR [Ded_PTax]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Tot_Ded]  DEFAULT ((0)) FOR [Tot_Ded]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_NetPay]  DEFAULT ((0)) FOR [NetPay]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Tot_CoCommDays]  DEFAULT ((0)) FOR [Tot_CoCommDays]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Cal_CoComm]  DEFAULT ((0)) FOR [Tot_CoCommAmt]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Cal_CoCommPFAmt]  DEFAULT ((0)) FOR [Tot_CoCommPFAmt]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Tot_CoComm]  DEFAULT ((0)) FOR [Tot_CoComm]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Tot_CoServTax]  DEFAULT ((0)) FOR [Tot_CoServTax]
GO
ALTER TABLE [dbo].[Cont_MthlyPay] ADD  CONSTRAINT [DF_Cont_MthlyPay_Tot_CoEduTax]  DEFAULT ((0)) FOR [Tot_CoEduTax]
GO
ALTER TABLE [dbo].[Cont_ParaMast] ADD  CONSTRAINT [DF_Cont_ParaMast_BCFLG]  DEFAULT ((0)) FOR [BCFLG]
GO
ALTER TABLE [dbo].[Cont_ParaMast] ADD  CONSTRAINT [DF_Cont_ParaMast_AppFlg]  DEFAULT ((1)) FOR [AppFlg]
GO
ALTER TABLE [dbo].[Cont_UserRights] ADD  CONSTRAINT [DF_Cont_UserRights_Add1]  DEFAULT ((0)) FOR [Add1]
GO
ALTER TABLE [dbo].[Cont_UserRights] ADD  CONSTRAINT [DF_Cont_UserRights_Update1]  DEFAULT ((0)) FOR [Update1]
GO
ALTER TABLE [dbo].[Cont_UserRights] ADD  CONSTRAINT [DF_Cont_UserRights_Delete1]  DEFAULT ((0)) FOR [Delete1]
GO
ALTER TABLE [dbo].[Cont_UserRights] ADD  CONSTRAINT [DF_Cont_UserRights_View1]  DEFAULT ((0)) FOR [View1]
GO
