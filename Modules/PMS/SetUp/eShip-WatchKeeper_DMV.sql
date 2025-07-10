--IF NOT  EXISTS (SELECT name FROM sys.databases WHERE name = N'eMANAGER')
--CREATE DATABASE [eMANAGER]
--GO

USE [eShip]
GO

/****** Object:  StoredProcedure [dbo].[sp_CP_IUDAILYWORKHRS]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CP_IUDAILYWORKHRS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_CP_IUDAILYWORKHRS]
GO
/****** Object:  UserDefinedFunction [dbo].[GETREST_SLOTS_IN_NEXT_24]    Script Date: 08/07/2012 16:57:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GETREST_SLOTS_IN_NEXT_24]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GETREST_SLOTS_IN_NEXT_24]
GO
/****** Object:  StoredProcedure [dbo].[sp_CP_IUCP_CrewDailyLocation]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CP_IUCP_CrewDailyLocation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_CP_IUCP_CrewDailyLocation]
GO
/****** Object:  StoredProcedure [dbo].[sp_CP_IUVESSELCREWSIGNONOFF]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CP_IUVESSELCREWSIGNONOFF]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_CP_IUVESSELCREWSIGNONOFF]
GO
/****** Object:  StoredProcedure [dbo].[sp_CP_IUVesselCrewList]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CP_IUVesselCrewList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_CP_IUVesselCrewList]
GO
/****** Object:  UserDefinedFunction [dbo].[GET_MAXREST_IN24]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_MAXREST_IN24]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GET_MAXREST_IN24]
GO
/****** Object:  UserDefinedFunction [dbo].[GET_MAXREST_IN7]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_MAXREST_IN7]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GET_MAXREST_IN7]
GO
/****** Object:  StoredProcedure [dbo].[sp_CP_GETDAILYWORKHRS]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CP_GETDAILYWORKHRS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_CP_GETDAILYWORKHRS]
GO
/****** Object:  StoredProcedure [dbo].[sp_CP_GETDAILYWORKHRSMONTHLY]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CP_GETDAILYWORKHRSMONTHLY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_CP_GETDAILYWORKHRSMONTHLY]
GO
/****** Object:  StoredProcedure [dbo].[sp_CP_GETDAILYWORKHRSMONTHLY_BKP]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CP_GETDAILYWORKHRSMONTHLY_BKP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_CP_GETDAILYWORKHRSMONTHLY_BKP]
GO
/****** Object:  StoredProcedure [dbo].[sp_CP_IUDNCREASON]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CP_IUDNCREASON]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_CP_IUDNCREASON]
GO
/****** Object:  StoredProcedure [dbo].[sp_CP_DeleteVesselCrewList]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CP_DeleteVesselCrewList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_CP_DeleteVesselCrewList]
GO



/****** Object:  View [dbo].[RH_NCList]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[RH_NCList]'))
DROP VIEW [dbo].[RH_NCList]
GO

/****** Object:  Table [dbo].[CP_CrewDailyLocation]    Script Date: 06/15/2012 16:39:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CP_CrewDailyLocation]') AND type in (N'U'))
DROP TABLE [dbo].[CP_CrewDailyLocation]
GO
/****** Object:  StoredProcedure [dbo].[Create_Procedure]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Create_Procedure]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Create_Procedure]
GO
/****** Object:  Table [dbo].[CP_CrewHoursLog]    Script Date: 06/15/2012 16:39:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CP_CrewHoursLog]') AND type in (N'U'))
DROP TABLE [dbo].[CP_CrewHoursLog]
GO
/****** Object:  Table [dbo].[CP_VesselCrewSignOnOff]    Script Date: 06/15/2012 16:39:12 ******/
--IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CP_VesselCrewSignOnOff]') AND type in (N'U'))
--DROP TABLE [dbo].[CP_VesselCrewSignOnOff]
--GO
--/****** Object:  Table [dbo].[CP_VesselCrewList]    Script Date: 06/15/2012 16:39:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CP_VesselCrewList]') AND type in (N'U'))
DROP TABLE [dbo].[CP_VesselCrewList]
GO
/****** Object:  StoredProcedure [dbo].[ExecQuery]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExecQuery]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ExecQuery]
GO
/****** Object:  Table [dbo].[CP_Settings]    Script Date: 06/15/2012 16:39:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CP_Settings]') AND type in (N'U'))
DROP TABLE [dbo].[CP_Settings]
GO
/****** Object:  Table [dbo].[RH_DateLineSetting]    Script Date: 06/15/2012 16:39:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RH_DateLineSetting]') AND type in (N'U'))
DROP TABLE [dbo].[RH_DateLineSetting]
GO

/****** Object:  UserDefinedFunction [dbo].[CSVtoTable2Float]    Script Date: 06/15/2012 16:39:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CSVtoTable2Float]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[CSVtoTable2Float]
GO
/****** Object:  UserDefinedFunction [dbo].[GetAllDateOfMonth]    Script Date: 06/15/2012 16:39:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllDateOfMonth]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetAllDateOfMonth]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Get_DaysRequired]    Script Date: 06/15/2012 16:39:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_Get_DaysRequired]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_Get_DaysRequired]
GO
/****** Object:  Table [dbo].[CP_CrewDailyWorkRestHours]    Script Date: 06/15/2012 16:39:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CP_CrewDailyWorkRestHours]') AND type in (N'U'))
DROP TABLE [dbo].[CP_CrewDailyWorkRestHours]
GO
/****** Object:  Table [dbo].[CP_NonConformance]    Script Date: 06/15/2012 16:39:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CP_NonConformance]') AND type in (N'U'))
DROP TABLE [dbo].[CP_NonConformance]
GO
/****** Object:  Table [dbo].[CP_NonConformanceReason]    Script Date: 06/15/2012 16:39:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CP_NonConformanceReason]') AND type in (N'U'))
DROP TABLE [dbo].[CP_NonConformanceReason]
GO
/****** Object:  Table [dbo].[CP_Rank]    Script Date: 06/15/2012 16:39:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CP_Rank]') AND type in (N'U'))
DROP TABLE [dbo].[CP_Rank]
GO
/****** Object:  Table [dbo].[PMS_CREW_HISTORY]    Script Date: 06/15/2012 16:39:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PMS_CREW_HISTORY]') AND type in (N'U'))
DROP TABLE [dbo].[PMS_CREW_HISTORY]
GO
/****** Object:  Table [dbo].[RH_CrewMonthData]    Script Date: 06/15/2012 16:39:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RH_CrewMonthData]') AND type in (N'U'))
DROP TABLE [dbo].[RH_CrewMonthData]
GO
/****** Object:  Table [dbo].[ER_G113_Report]    Script Date: 06/15/2012 16:39:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ER_G113_Report]') AND type in (N'U'))
DROP TABLE [dbo].[ER_G113_Report]
GO
/****** Object:  Table [dbo].[PMS_CREW_TRAININGCOMPLETED]    Script Date: 06/15/2012 16:39:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PMS_CREW_TRAININGCOMPLETED]') AND type in (N'U'))
DROP TABLE [dbo].[PMS_CREW_TRAININGCOMPLETED]
GO
/****** Object:  Table [dbo].[PMS_CREW_TRAININGREQUIREMENT]    Script Date: 06/15/2012 16:39:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PMS_CREW_TRAININGREQUIREMENT]') AND type in (N'U'))
DROP TABLE [dbo].[PMS_CREW_TRAININGREQUIREMENT]
GO

/****** Object:  StoredProcedure [dbo].[sp_CP_RESETPASSWORD]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CP_RESETPASSWORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_CP_RESETPASSWORD]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_INSERTCP_VESSELCREWLIST]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_INSERTCP_VESSELCREWLIST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_INSERTCP_VESSELCREWLIST]
GO
/****** Object:  StoredProcedure [dbo].[sp_ship_import_PMS_CREW_HISTORY]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ship_import_PMS_CREW_HISTORY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ship_import_PMS_CREW_HISTORY]
GO
/****** Object:  StoredProcedure [dbo].[sp_ship_import_MP_VSL_SparesReqMaster]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ship_import_MP_VSL_SparesReqMaster]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ship_import_MP_VSL_SparesReqMaster]
GO
/****** Object:  StoredProcedure [dbo].[sp_ship_import_MP_VSL_StoreReqMaster]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ship_import_MP_VSL_StoreReqMaster]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ship_import_MP_VSL_StoreReqMaster]
GO
/****** Object:  StoredProcedure [dbo].[sp_ship_import_MP_VSL_StoreReqMaster1]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ship_import_MP_VSL_StoreReqMaster1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ship_import_MP_VSL_StoreReqMaster1]
GO
/****** Object:  StoredProcedure [dbo].[sp_ship_import_MP_VSL_ProvisionMaster]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ship_import_MP_VSL_ProvisionMaster]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ship_import_MP_VSL_ProvisionMaster]
GO
/****** Object:  StoredProcedure [dbo].[sp_ship_import_PMS_CREW_HISTORY_RESETLOG]    Script Date: 06/15/2012 16:39:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ship_import_PMS_CREW_HISTORY_RESETLOG]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ship_import_PMS_CREW_HISTORY_RESETLOG]
GO
/****** Object:  Table [dbo].[CP_NCType]    Script Date: 06/15/2012 16:39:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CP_NCType]') AND type in (N'U'))
DROP TABLE [dbo].[CP_NCType]
GO

/****** Object:  Table [dbo].[RH_DateLineSetting]    Script Date: 06/15/2012 16:39:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RH_DateLineSetting]') AND type in (N'U'))
DROP TABLE [dbo].[RH_DateLineSetting]
GO

/****** Object:  Table [dbo].[RH_NCType]    Script Date: 06/15/2012 16:39:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RH_NCType]') AND type in (N'U'))
DROP TABLE [dbo].[RH_NCType]
GO

/****** Object:  Table [dbo].[CP_NCType]    Script Date: 06/15/2012 16:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CP_NCType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CP_NCType](
	[NCTypeId] [int] NOT NULL,
	[NCTypeName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_CP_NCType] PRIMARY KEY CLUSTERED 
(
	[NCTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[RH_DateLineSetting]    Script Date: 06/15/2012 16:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RH_DateLineSetting]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RH_DateLineSetting](
	[VesselCode] [char](3) NOT NULL,
	[RPeriod] [smalldatetime] NOT NULL,
	[DateLine] [smallint] NOT NULL,
	[Location] [char](1) NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [smalldatetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedOn] [smalldatetime] NULL,
 CONSTRAINT [PK_RH_DateLineSetting] PRIMARY KEY CLUSTERED 
(
	[VesselCode] ASC,
	[RPeriod] ASC,
	[DateLine] ASC,
	[Location] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PMS_CREW_HISTORY]    Script Date: 06/15/2012 16:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PMS_CREW_HISTORY]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PMS_CREW_HISTORY](
	[VesselCode] [char](3) NOT NULL,
	[CrewId] [bigint] NOT NULL,
	[ContractId] [bigint] NOT NULL,
	[CrewNumber] [varchar](10) NULL,
	[DOB] [smalldatetime] NULL,
	[DJC] [smalldatetime] NULL,
	[CrewName] [varchar](100) NULL,
	[RankId] [bigint] NOT NULL,
	[SignOnDate] [smalldatetime] NULL,
	[SignOffDate] [smalldatetime] NULL,
	[ReliefDuedate] [smalldatetime] NULL,
 CONSTRAINT [PK_PMS_CREW_HISTORY_1] PRIMARY KEY CLUSTERED 
(
	[VesselCode] ASC,
	[CrewId] ASC,
	[ContractId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[RH_CrewMonthData]    Script Date: 06/15/2012 16:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RH_CrewMonthData]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RH_CrewMonthData](
	[VesselCode] [char](3) NOT NULL,
	[CrewNumber] [varchar](10) NOT NULL,
	[ForDate] [smalldatetime] NOT NULL,
	[DateLine] [smallint] NOT NULL,
	[WorkLog] [varchar](48) NULL,
	[WorkLogCount] [numeric](3, 1) NULL,
	[RestLogCount] [numeric](3, 1) NULL,
	[RestIn24] [numeric](3, 1) NULL,
	[RestIn7] [numeric](4, 1) NULL,
	[MinTop2Sum] [numeric](3, 1) NULL,
	[Single6Found] [numeric](3, 1) NULL,
	[Status] [char](1) NULL,
	[Remarks] [varchar](max) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [smalldatetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedOn] [smalldatetime] NULL,
 CONSTRAINT [PK_RH_CrewMonthData_1] PRIMARY KEY CLUSTERED 
(
	[VesselCode] ASC,
	[CrewNumber] ASC,
	[ForDate] ASC,
	[DateLine] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ER_G113_Report]    Script Date: 06/15/2012 16:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ER_G113_Report]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ER_G113_Report](
	[AssMgntID] [bigint] NOT NULL,
	[VesselCode] [varchar](3) NULL,
	[Occasion] [varchar](255) NULL,
	[PeapType] [varchar](255) NULL,
	[AssName] [varchar](255) NULL,
	[AssLname] [varchar](255) NULL,
	[CrewNo] [varchar](255) NULL,
	[rank] [varchar](255) NULL,
	[AppraisalFromDate] [datetime] NULL,
	[AppraisalToDate] [datetime] NULL,
	[DatejoinedComp] [datetime] NULL,
	[DatejoinedVessel] [datetime] NULL,
	[PerformanceScore] [decimal](18, 2) NULL,
	[CompetenciesScore] [decimal](18, 2) NULL,
	[PerScale1] [int] NULL,
	[PerScale2] [int] NULL,
	[PerScale3] [int] NULL,
	[perAss1] [varchar](max) NULL,
	[perAss2] [varchar](max) NULL,
	[perAss3] [varchar](max) NULL,
	[AssScale1] [int] NULL,
	[AssScale2] [int] NULL,
	[AssScale3] [int] NULL,
	[AssScale4] [int] NULL,
	[AssScale5] [int] NULL,
	[AssScale6] [int] NULL,
	[AssScale7] [int] NULL,
	[AssScale8] [int] NULL,
	[PotSecA] [varchar](max) NULL,
	[PotSecB] [varchar](255) NULL,
	[PotSecB1] [varchar](max) NULL,
	[PotSecB2] [varchar](max) NULL,
	[PotSecC1] [varchar](max) NULL,
	[PotSecC2] [varchar](max) NULL,
	[RemAppraiseeComment] [varchar](max) NULL,
	[RemAppraiseeName] [varchar](255) NULL,
	[RemAppraiseeRank] [varchar](255) NULL,
	[RemAppraiseeCrewNo] [varchar](255) NULL,
	[RemAppraiseeDate] [datetime] NULL,
	[RemAppraiserComment] [varchar](max) NULL,
	[RemAppraiserName] [varchar](255) NULL,
	[RemAppraiserCrewNo] [varchar](255) NULL,
	[RemAppraiserRank] [varchar](255) NULL,
	[RemAppraiserDate] [datetime] NULL,
	[RemMasterComments] [varchar](max) NULL,
	[RemMasterName] [varchar](255) NULL,
	[RemMasterRank] [varchar](255) NULL,
	[RemMasterDate] [datetime] NULL,
	[DateJoinedVesselAppraisee] [datetime] NULL,
	[ReviewedBy] [varchar](255) NULL,
	[ReviewedDate] [datetime] NULL,
	[ReviewedComment] [varchar](8000) NULL,
	[Status] [int] NULL,
	[AppMasterCrewNo] [varchar](255) NULL,
	[ShipSoftRank] [int] NULL,
	[IsTrainingRequired] [int] NULL,
	[AppraisalRecievedDate] [datetime] NULL,
	[Location] [char](1) NULL,
	[CrewAppraisalId] [bigint] NULL,
	[CrewBonusId] [int] NULL,
	[ExportedBy] [varchar](100) NULL,
	[ExportedOn] [smalldatetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PMS_CREW_TRAININGCOMPLETED]    Script Date: 06/15/2012 16:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PMS_CREW_TRAININGCOMPLETED]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PMS_CREW_TRAININGCOMPLETED](
	[VesselCode] [varchar](3) NULL,
	[TrainingRequirementId] [bigint] NOT NULL,
	[Crewid] [int] NOT NULL,
	[TrainingId] [int] NOT NULL,
	[TrainingName] [varchar](5000) NULL,
	[Source] [varchar](8) NOT NULL,
	[N_DueDate] [smalldatetime] NULL,
	[PlannedFor] [smalldatetime] NULL,
	[LastDone] [smalldatetime] NULL,
	[FromDate] [smalldatetime] NULL,
	[ToDate] [smalldatetime] NULL,
	[UpdateBy] [varchar](50) NULL,
	[UpdatedOn] [smalldatetime] NULL,
	[OfficeRecdOn] [smalldatetime] NULL,
	[Attachment] [image] NULL,
	[AttachmentFileName] [varchar](500) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PMS_CREW_TRAININGREQUIREMENT]    Script Date: 06/15/2012 16:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PMS_CREW_TRAININGREQUIREMENT]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PMS_CREW_TRAININGREQUIREMENT](
	[VesselCode] [varchar](3) NULL,
	[TrainingRequirementId] [bigint] NOT NULL,
	[Crewid] [int] NOT NULL,
	[TrainingId] [int] NOT NULL,
	[TrainingName] [varchar](5000) NULL,
	[Source] [varchar](8) NOT NULL,
	[N_DueDate] [smalldatetime] NULL,
	[PlannedFor] [smalldatetime] NULL,
	[LastDone] [smalldatetime] NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[RH_NCType]    Script Date: 06/15/2012 16:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RH_NCType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RH_NCType](
	[NCTypeId] [int] NOT NULL,
	[NCTypeName] [varchar](100) NULL,
 CONSTRAINT [PK_RH_NCType] PRIMARY KEY CLUSTERED 
(
	[NCTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

/****** Object:  StoredProcedure [dbo].[sp_cp_INSERTCP_VESSELCREWLIST]    Script Date: 06/15/2012 16:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_INSERTCP_VESSELCREWLIST]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[sp_cp_INSERTCP_VESSELCREWLIST]
(
@CREWNUMBER VARCHAR(6),
@CREWNAME VARCHAR(100),
@PASSWORD VARCHAR(50),
@RANKID int,
@SIGNONDATE smalldatetime,
@SIGNOFFDATE smalldatetime,
@WATCHKEEPER BIT
)
AS
BEGIN
SET NOCOUNT ON

INSERT INTO CP_VESSELCREWLIST
(
CREWNUMBER,
CREWNAME,
PASSWORD,
RANKID,
SIGNONDATE,
SIGNOFFDATE,
WATCHKEEPER
)
Values
(
@CREWNUMBER,
@CREWNAME,
@PASSWORD,
@RANKID,
@SIGNONDATE,
@SIGNOFFDATE,
@WATCHKEEPER
)
END





' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ship_import_PMS_CREW_HISTORY]    Script Date: 06/15/2012 16:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ship_import_PMS_CREW_HISTORY]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE proc [dbo].[sp_ship_import_PMS_CREW_HISTORY]  
(  
 @VesselCode varchar(3),  
 @CrewId bigint,  
 @ContractId bigint,  
 @CrewNumber varchar(10),  
 @DOB smalldatetime,  
 @DJC smalldatetime,  
 @CrewName Varchar(100),  
 @RankId bigint,  
 @SignOnDate smalldatetime,  
 @SignOffDate smalldatetime,  
 @ReliefDueDate smalldatetime  
)  
as  
begin  
 insert into PMS_CREW_HISTORY (VesselCode,CrewId,ContractId,CrewNumber,DOB,DJC,CrewName,RankId,SignOnDate,SignOffDate,ReliefDueDate)  
 values(@VesselCode,@CrewId,@ContractId,@CrewNumber,@DOB,@DJC,@CrewName,@RankId,@SignOnDate,@SignOffDate,@ReliefDueDate)  
end
' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ship_import_MP_VSL_SparesReqMaster]    Script Date: 06/15/2012 16:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ship_import_MP_VSL_SparesReqMaster]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE proc [dbo].[sp_ship_import_MP_VSL_SparesReqMaster]  
(  
 @VesselCode varchar(3),  
 @SpareReqId bigint,  
 @ReceivedOn smalldatetime  
)  
as  
begin  
 UPDATE MP_VSL_SparesReqMaster set ReceivedOn=@ReceivedOn WHERE VesselCode=@VesselCode AND SpareReqId=@SpareReqId  
end
' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ship_import_MP_VSL_StoreReqMaster]    Script Date: 06/15/2012 16:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ship_import_MP_VSL_StoreReqMaster]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE proc [dbo].[sp_ship_import_MP_VSL_StoreReqMaster]  
(  
 @VesselCode varchar(3),  
 @StoreReqId bigint,  
 @ReceivedOn smalldatetime  
)  
as  
begin  
 UPDATE MP_VSL_StoreReqMaster set ReceivedOn=@ReceivedOn WHERE VesselCode=@VesselCode AND StoreReqId=@StoreReqId  
end  
' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ship_import_MP_VSL_StoreReqMaster1]    Script Date: 06/15/2012 16:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ship_import_MP_VSL_StoreReqMaster1]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE proc [dbo].[sp_ship_import_MP_VSL_StoreReqMaster1]  
(  
 @VesselCode varchar(3),  
 @StoreReqId bigint,  
 @ReceivedOn smalldatetime  
)  
as  
begin  
 UPDATE MP_VSL_StoreReqMaster1 set ReceivedOn=@ReceivedOn WHERE VesselCode=@VesselCode AND StoreReqId=@StoreReqId  
end  
' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ship_import_MP_VSL_ProvisionMaster]    Script Date: 06/15/2012 16:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ship_import_MP_VSL_ProvisionMaster]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE proc [dbo].[sp_ship_import_MP_VSL_ProvisionMaster]  
(  
 @VesselCode varchar(3),  
 @ProvisionId bigint,  
 @ReceivedOn smalldatetime  
)  
as  
begin  
 UPDATE MP_VSL_ProvisionMaster set ReceivedOn=@ReceivedOn WHERE VesselCode=@VesselCode AND ProvisionId=@ProvisionId  
end  
' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ship_import_PMS_CREW_HISTORY_RESETLOG]    Script Date: 06/15/2012 16:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ship_import_PMS_CREW_HISTORY_RESETLOG]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE proc [dbo].[sp_ship_import_PMS_CREW_HISTORY_RESETLOG]  
AS  
BEGIN  
 DECLARE @CREWNUMBER VARCHAR(10)  
 DECLARE @FORDATE SMALLDATETIME  
   
 DECLARE CS CURSOR FOR SELECT CREWNUMBER,FORDATE FROM RH_CrewMonthData WHERE FORDATE>DATEADD(MM,-1,FORDATE) ORDER BY CREWNUMBER,FORDATE   
 OPEN CS  
 FETCH NEXT FROM CS INTO @CREWNUMBER,@FORDATE  
 WHILE(@@FETCH_STATUS=0)  
 BEGIN  
  IF NOT EXISTS  
  (  
   SELECT CrewNumber FROM PMS_CREW_HISTORY WHERE CrewNumber=@CREWNUMBER   
   AND   
   (  
    ( DBO.getDatePart(@FORDATE) >= DBO.getDatePart(SIGNONDATE) AND DBO.getDatePart(@FORDATE) <=DBO.getDatePart(SignOffDate) )   
    OR  
    ( DBO.getDatePart(@FORDATE) >= DBO.getDatePart(SIGNONDATE) AND SignOffDate IS NULL)   
   )  
  )  
  BEGIN  
   DELETE FROM RH_CrewMonthData WHERE CREWNUMBER=@CREWNUMBER AND ForDate=@FORDATE  
  END  
 FETCH NEXT FROM CS INTO @CREWNUMBER,@FORDATE  
 END  
 CLOSE CS  
 DEALLOCATE CS  
END 
' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CP_RESETPASSWORD]    Script Date: 06/15/2012 16:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CP_RESETPASSWORD]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_CP_RESETPASSWORD]
(
@CREWNUMBER varchar(6),
@PASSWORD varchar(100)

)
AS
BEGIN
	SET NOCOUNT ON
		UPDATE  CP_VesselCrewList SET PASSWORD=@PASSWORD WHERE CREWNUMBER=@CREWNUMBER
END



' 
END
GO
/****** Object:  Table [dbo].[CP_Rank]    Script Date: 06/15/2012 16:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CP_Rank]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CP_Rank](
	[RankId] [int] IDENTITY(1,1) NOT NULL,
	[RankGroupId] [int] NOT NULL,
	[RankLevel] [smallint] NULL,
	[RankCode] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RankName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[OffCrew] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OffGroup] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LeavesAllowed] [numeric](5, 1) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [smalldatetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedOn] [smalldatetime] NULL,
	[StatusId] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Rank_Mum] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Smou_Code] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SireRankType] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SireRank] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
END
GO
/****** Object:  Table [dbo].[CP_NonConformanceReason]    Script Date: 06/15/2012 16:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CP_NonConformanceReason]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CP_NonConformanceReason](
	[CrewNumber] [varchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NCDate] [smalldatetime] NOT NULL,
	[Reason] [int] NULL
)
END
GO
/****** Object:  Table [dbo].[CP_NonConformance]    Script Date: 06/15/2012 16:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CP_NonConformance]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CP_NonConformance](
	[CrewNumber] [varchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NCDate] [smalldatetime] NOT NULL,
	[CalcDate] [smalldatetime] NULL,
	[NCTYPE] [int] NULL,
	[CompTime] [datetime] NULL
)
END
GO
/****** Object:  Table [dbo].[CP_CrewDailyWorkRestHours]    Script Date: 06/15/2012 16:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CP_CrewDailyWorkRestHours]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CP_CrewDailyWorkRestHours](
	[CrewNumber] [varchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TransDate] [smalldatetime] NOT NULL,
	[WorkRest] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FromTime] [float] NOT NULL,
	[ToTime] [float] NULL,
	[Duration] [float] NULL,
	[Restin7] [float] NULL,
	[Restin24] [float] NULL,
	[EnteredBy] [bit] NULL,
	[EnteredOn] [smalldatetime] NULL
)
END
GO
/****** Object:  UserDefinedFunction [dbo].[CSVtoTable2Float]    Script Date: 06/15/2012 16:39:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CSVtoTable2Float]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Pankaj K. Verma>
-- Create date: <05-Apr-2011>
-- Description:	<Convert CSV to Table>
--SELECT * FROM DBO.CSVtoTable2Float(''1.2'',''1'','','')
-- =============================================
CREATE FUNCTION [dbo].[CSVtoTable2Float]
(
	@LIST varchar(7000),
	@LIST1 varchar(7000),
	@Delimeter varchar(10)
)
RETURNS @RET1 TABLE (RESULT FLOAT,RESULT1 FLOAT)
AS
BEGIN
	DECLARE @RET TABLE(RESULT FLOAT,RESULT1 FLOAT)
	
	IF LTRIM(RTRIM(@LIST))='''' RETURN  

	DECLARE @START BIGINT
	DECLARE @LASTSTART BIGINT
	SET @LASTSTART=0

	DECLARE @START1 BIGINT
	DECLARE @LASTSTART1 BIGINT
	SET @LASTSTART1=0

	SET @START=CHARINDEX(@Delimeter,@LIST,0)
	SET @START1=CHARINDEX(@Delimeter,@LIST1,0)

	IF @START=0
	INSERT INTO @RET VALUES(SUBSTRING(@LIST,0,LEN(@LIST)+1),SUBSTRING(@LIST1,0,LEN(@LIST1)+1))

	WHILE(@START >0)
	BEGIN
		INSERT INTO @RET VALUES(SUBSTRING(@LIST,@LASTSTART,@START-@LASTSTART),SUBSTRING(@LIST1,@LASTSTART1,@START1-@LASTSTART1))
		SET @LASTSTART=@START+1
		SET @LASTSTART1=@START1+1

		SET @START=CHARINDEX(@Delimeter,@LIST,@START+1)
		SET @START1=CHARINDEX(@Delimeter,@LIST1,@START1+1)

		IF(@START=0)
		INSERT INTO @RET VALUES(SUBSTRING(@LIST,@LASTSTART,LEN(@LIST)+1),SUBSTRING(@LIST1,@LASTSTART1,LEN(@LIST1)+1))
	END
	
	INSERT INTO @RET1 SELECT * FROM @RET
	RETURN 
END





' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetAllDateOfMonth]    Script Date: 06/15/2012 16:39:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllDateOfMonth]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[GetAllDateOfMonth]  
(  
 @MONTH INT,  
 @YEAR INT  
)  
RETURNS @RET1 TABLE (RESULT smalldatetime)  
AS  
BEGIN  
 DECLARE @RET TABLE(RESULT smalldatetime)  
   
   
 Declare @st smalldatetime  
 Declare @nd datetime  
  
 set @st=convert(varchar,@YEAR)+''-''+convert(varchar,@MONTH)+''-01''  
 SELECT  @nd=convert(smalldatetime, DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@st)+1,0)))  
  
 while (@st<@nd)  
 begin  
  insert into @RET values(@st)  
  set @st= dateadd(dd,1, @st)  
 end  
 INSERT INTO @RET1 SELECT * FROM @RET  
 RETURN   
END 
' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Get_DaysRequired]    Script Date: 06/15/2012 16:39:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_Get_DaysRequired]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[fn_Get_DaysRequired]  
(  
 @SignOnDate smalldatetime ,  
 @SignOffDate smalldatetime,  
 @StartDate smalldatetime,  
 @EndDate smalldatetime  
)  
RETURNS int  
BEGIN  
 declare @ret int   
   
 declare @FilterStartDate smalldatetime declare @FilterEndDate smalldatetime  
  
   
 if(@SignOnDate>@StartDate)  
 begin  
  set @FilterStartDate=@SignOnDate  
 end  
 else  
 begin  
  set @FilterStartDate=@StartDate  
 end  
  
 if(@SignOffDate<@EndDate)  
 begin  
  set @FilterEndDate=@SignOffDate  
 end  
 else  
 begin  
  set @FilterEndDate=@EndDate  
 end  
   
  
 set @ret=DATEDIFF(d,@FilterStartDate,@FilterEndDate)+1  
  
 RETURN @ret  
  
END   
' 
END
GO
/****** Object:  Table [dbo].[CP_Settings]    Script Date: 06/15/2012 16:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CP_Settings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CP_Settings](
	[VesselCode] [varchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VesselName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Lrimonumber] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Flag] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AdminPassword] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
END
GO
/****** Object:  StoredProcedure [dbo].[ExecQuery]    Script Date: 06/15/2012 16:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExecQuery]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'----------------------------------------------------
/****** Object:  StoredProcedure [dbo].[ExecQuery]    Script Date: 04/26/2010 12:22:33 ******/
CREATE PROCEDURE [dbo].[ExecQuery]
@Query varchar(7000)
AS
BEGIN
	EXEC (@Query)
END
' 
END
GO
/****** Object:  Table [dbo].[CP_VesselCrewList]    Script Date: 06/15/2012 16:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CP_VesselCrewList]') AND type in (N'U'))
BEGIN
--CREATE TABLE [dbo].[CP_VesselCrewList](
--	[CrewNumber] [varchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
--	[Password] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
--	[CrewName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
--	[Active] [bit] NULL,
-- CONSTRAINT [PK_CP_VesselCrewList] PRIMARY KEY CLUSTERED 
--(
--	[CrewNumber] ASC
--)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
--)
CREATE TABLE [dbo].[CP_VesselCrewList](
	[CrewNumber] [varchar](6) NULL,
	[Password] [varchar](50) NULL,
	[CrewName] [varchar](100) NULL,
	[RankId] [int] NULL,
	[SignOnDate] [smalldatetime] NULL,
	[SignOffDate] [smalldatetime] NULL,
	[WatchKeeper] [bit] NULL,
	[Active] [bit] NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[CP_VesselCrewSignOnOff]    Script Date: 06/15/2012 16:39:12 ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CP_VesselCrewSignOnOff]') AND type in (N'U'))
--BEGIN
--CREATE TABLE [dbo].[CP_VesselCrewSignOnOff](
--	[TableId] [bigint] NOT NULL,
--	[CrewNumber] [varchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
--	[SignOnDt] [smalldatetime] NULL,
--	[SignOffDt] [smalldatetime] NULL,
--	[WatchKeeper] [bit] NULL,
--	[RankId] [int] NULL,
-- CONSTRAINT [PK_CP_VesselCrewSignOnOff] PRIMARY KEY CLUSTERED 
--(
--	[TableId] ASC
--)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
--)
--END
--GO
/****** Object:  Table [dbo].[CP_CrewHoursLog]    Script Date: 06/15/2012 16:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CP_CrewHoursLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CP_CrewHoursLog](
	[TableId] [bigint] NOT NULL,
	[CrewNumber] [varchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ForDate] [smalldatetime] NULL,
	[UpdatedOn] [smalldatetime] NULL,
 CONSTRAINT [PK_CP_CrewHoursLog] PRIMARY KEY CLUSTERED 
(
	[TableId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  StoredProcedure [dbo].[Create_Procedure]    Script Date: 06/15/2012 16:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Create_Procedure]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [dbo].[Create_Procedure] 
(
@TableName varchar(100)
)
as 
begin

select Name from
(
SELECT -12 as id,0 as ORDINAL_POSITION,''SET ANSI_NULLS ON'' as Name from (select getdate() as a) b 
union
SELECT -11 as id,0 as ORDINAL_POSITION,''GO'' as Name from (select getdate() as a) b 
union
SELECT -10 as id,0 as ORDINAL_POSITION,''SET QUOTED_IDENTIFIER ON'' as Name from (select getdate() as a) b 
union
SELECT -9 as id,0 as ORDINAL_POSITION,''GO'' as Name from (select getdate() as a) b 
union
SELECT -8 as id,0 as ORDINAL_POSITION,''CREATE PROCEDURE dbo.<--->'' as Name from (select getdate() as a) b 
union
SELECT -7 as id,0,''('' from (select getdate() as a) b 
union
select -6 as  id,ORDINAL_POSITION, ''@''+ upper(Column_Name) + '' '' + Data_Type + (case when CHARACTER_MAXIMUM_LENGTH is NULL then '''' else ''('' + CONVERT(VARCHAR,CHARACTER_MAXIMUM_LENGTH) + '')'' end) + (case when ORDINAL_POSITION<>(select max(ORDINAL_POSITION) from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME=@TableName and table_schema = ''dbo'') then '','' else '''' end)
from 
INFORMATION_SCHEMA.COLUMNS where TABLE_NAME=
@TableName and table_schema = ''dbo''
union
SELECT -5 as id,0,'')'' from (select getdate() as a) b 
union
SELECT -4 as id,0,''AS'' from (select getdate() as a) b 
union
SELECT -3 as id,0,''BEGIN'' from (select getdate() as a) b 
union
SELECT -2 as id,0,''SET NOCOUNT ON'' from (select getdate() as a) b 
union
SELECT 0 as id,0,''INSERT INTO '' + @TableName from (select getdate() as a) b 
union
SELECT 1 as id,0,''(''  from (select getdate() as a) b 
union
select 2 as  id,ORDINAL_POSITION,upper(Column_Name) + '' '' + (case when ORDINAL_POSITION<>(select max(ORDINAL_POSITION) from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME=@TableName and table_schema = ''dbo'') then '','' else '''' end) 
from 
INFORMATION_SCHEMA.COLUMNS where TABLE_NAME=
@TableName and table_schema = ''dbo''
union
SELECT 3 as id,0,'')'' from (select getdate() as a) b 
union
SELECT 4 as id,0,''Values'' from (select getdate() as a) b
union
SELECT 5 as id,0,''('' from (select getdate() as a) b 
union
select 6 as  id,ORDINAL_POSITION, ''@''+ upper(Column_Name) + (case when ORDINAL_POSITION<>(select max(ORDINAL_POSITION) from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME=@TableName and table_schema = ''dbo'') then '','' else '''' end)
from 
INFORMATION_SCHEMA.COLUMNS where TABLE_NAME=
@TableName and table_schema = ''dbo''
union
SELECT 7 as id,0,'')'' from (select getdate() as a) b 
union
SELECT 8 as id,0,''END'' from (select getdate() as a) b 
) d order by id,ordinal_position
end


' 
END
GO
/****** Object:  Table [dbo].[CP_CrewDailyLocation]    Script Date: 06/15/2012 16:39:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CP_CrewDailyLocation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CP_CrewDailyLocation](
	[CrewNumber] [varchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TableId] [int] NOT NULL,
	[TDate] [smalldatetime] NOT NULL,
	[Location] [int] NULL,
 CONSTRAINT [PK_CP_CrewDailyLocation] PRIMARY KEY CLUSTERED 
(
	[CrewNumber] ASC,
	[TableId] ASC,
	[TDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO


/****** Object:  View [dbo].[RH_NCList]    Script Date: 06/15/2012 16:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[RH_NCList]'))
EXEC dbo.sp_executesql @statement = N'Create view [dbo].[RH_NCList]
as
SELECT A.*,NCTypeName
FROM
(
SELECT VESSELCODE,CrewNumber,FORDATE,DATELINE,24 AS NCId,Remarks FROM RH_CrewMonthData WHERE RESTIN24<10 
UNION
SELECT VESSELCODE,CrewNumber,FORDATE,DATELINE,2 AS NCId,Remarks FROM RH_CrewMonthData WHERE MinTop2Sum<10 
UNION
SELECT VESSELCODE,CrewNumber,FORDATE,DATELINE,6 AS NCId,Remarks FROM RH_CrewMonthData WHERE Single6Found<6
UNION
SELECT VESSELCODE,CrewNumber,FORDATE,DATELINE,7 AS NCId,Remarks FROM RH_CrewMonthData WHERE RESTIN7<77 
) A INNER JOIN RH_NCType ON A.NCId=RH_NCType.NCTypeId
'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'VW_CP_NCLIST', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "M"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CP_NCType"
            Begin Extent = 
               Top = 6
               Left = 236
               Bottom = 95
               Right = 396
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_CP_NCLIST'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'VW_CP_NCLIST', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_CP_NCLIST'
GO
/****** Object:  StoredProcedure [dbo].[sp_CP_DeleteVesselCrewList]    Script Date: 06/15/2012 16:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CP_DeleteVesselCrewList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Proc [dbo].[sp_CP_DeleteVesselCrewList]
(
 @TableID int
)
AS
BEGIN
Declare @CREWNUMBER VARCHAR(6)
seleCT @CREWNUMBER =CREWNUMBER  FROM CP_VesselCrewSignOnOff WHERE TableID =@TableID 

DELETE from CP_VesselCrewList WHERE CREWNUMBER=@CREWNUMBER 
DELETE from dbo.CP_CrewDailyWorkRestHours WHERE CREWNUMBER=@CREWNUMBER 
DELETE from dbo.CP_CrewHoursLog WHERE CREWNUMBER=@CREWNUMBER 
DELETE from dbo.CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER 
DELETE from dbo.CP_NonConformanceReason WHERE CREWNUMBER=@CREWNUMBER 
DELETE from CP_VesselCrewSignOnOff WHERE CREWNUMBER=@CREWNUMBER 

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CP_IUDNCREASON]    Script Date: 06/15/2012 16:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CP_IUDNCREASON]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE proc [dbo].[sp_CP_IUDNCREASON]
(
@CREWNUMBER VARCHAR(6),
@ONDATE SMALLDATETIME,
@REASON INT
)
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM CP_NonConformanceReason WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=@ONDATE)
		INSERT INTO CP_NonConformanceReason VALUES(@CREWNUMBER,@ONDATE,@REASON)
	ELSE
		UPDATE CP_NonConformanceReason SET REASON=@REASON WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=@ONDATE
END



' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CP_GETDAILYWORKHRSMONTHLY_BKP]    Script Date: 06/15/2012 16:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CP_GETDAILYWORKHRSMONTHLY_BKP]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_CP_GETDAILYWORKHRSMONTHLY_BKP]
(
@VESSELID INT,
@CREWID INT,
@MONTH INT,
@YEAR INT
--@CREWNUMBER VARCHAR(6),
--@MONTH INT,
--@YEAR INT
)
AS
BEGIN 

DECLARE @CREWNUMBER VARCHAR(6)
SET @CREWNUMBER=''Y00010''
--@MONTH INT,
--@YEAR INT

-- EXEC sp_CP_GETDAILYWORKHRSMONTHLY ''y00010'',6,2012  	 
--DROP TABLE #RES
SET NOCOUNT ON
CREATE TABLE #RES
(
TDATE SMALLDATETIME,
HR_0000 CHAR(7),
HR_0050 CHAR(7),
HR_0100 CHAR(7),
HR_0150 CHAR(7),
HR_0200 CHAR(7),
HR_0250 CHAR(7),
HR_0300 CHAR(7),
HR_0350 CHAR(7),
HR_0400 CHAR(7),
HR_0450 CHAR(7),
HR_0500 CHAR(7),
HR_0550 CHAR(7),
HR_0600 CHAR(7),
HR_0650 CHAR(7),
HR_0700 CHAR(7),
HR_0750 CHAR(7),
HR_0800 CHAR(7),
HR_0850 CHAR(7),
HR_0900 CHAR(7),
HR_0950 CHAR(7),
HR_1000 CHAR(7),
HR_1050 CHAR(7),
HR_1100 CHAR(7),
HR_1150 CHAR(7),
HR_1200 CHAR(7),
HR_1250 CHAR(7),
HR_1300 CHAR(7),
HR_1350 CHAR(7),
HR_1400 CHAR(7),
HR_1450 CHAR(7),
HR_1500 CHAR(7),
HR_1550 CHAR(7),
HR_1600 CHAR(7),
HR_1650 CHAR(7),
HR_1700 CHAR(7),
HR_1750 CHAR(7),
HR_1800 CHAR(7),
HR_1850 CHAR(7),
HR_1900 CHAR(7),
HR_1950 CHAR(7),
HR_2000 CHAR(7),
HR_2050 CHAR(7),
HR_2100 CHAR(7),
HR_2150 CHAR(7),
HR_2200 CHAR(7),
HR_2250 CHAR(7),
HR_2300 CHAR(7),
HR_2350 CHAR(7),
WORK NUMERIC(3,1),
REST NUMERIC(4,1),
COMMENTS VARCHAR(500),
R24 NUMERIC(3,1),
R7 NUMERIC(4,1),
NC_SUN CHAR(1)
)

DECLARE @DAYS INT
DECLARE @DCNT INT
DECLARE @SDATE SMALLDATETIME
DECLARE @EDATE SMALLDATETIME

--DECLARE @VESSELID INT
--DECLARE @CREWID INT
--DECLARE @MONTH INT
--DECLARE @YEAR INT
--
--SET @VESSELID =1
--SET @CREWID =3539
--SET @MONTH =5
--SET @YEAR =2012

SET @SDATE=STR(@YEAR) + ''-'' + STR(@MONTH) + ''-01''
SET @EDATE=DATEADD(MONTH,1,@SDATE)
SET @DAYS=DATEDIFF(DAY,@SDATE,@EDATE)
SET @EDATE=DATEADD(DAY,-1,@EDATE)

WHILE (@SDATE <=@EDATE)
BEGIN

	DECLARE @NC CHAR(1)
	SET @NC=''''
	IF EXISTS(SELECT * FROM dbo.CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=@SDATE)
		SET @NC=''N''
	ELSE
	BEGIN
		IF (DATENAME(WEEKDAY,@SDATE)=''Sunday'')
		SET @NC=''S''
	END
 
	INSERT INTO #RES(TDATE,HR_0000, HR_0050 ,HR_0100 ,HR_0150 ,HR_0200 ,HR_0250 ,HR_0300 ,HR_0350 ,HR_0400 ,HR_0450 ,HR_0500 ,HR_0550 ,HR_0600 ,HR_0650 ,HR_0700 ,HR_0750 ,HR_0800 ,HR_0850 ,HR_0900 ,HR_0950 ,HR_1000 ,HR_1050 ,HR_1100 ,HR_1150 ,HR_1200 ,HR_1250 ,HR_1300 ,HR_1350 ,HR_1400 ,HR_1450 ,HR_1500 ,HR_1550 ,HR_1600 ,HR_1650 ,HR_1700 ,HR_1750 ,HR_1800 ,HR_1850 ,HR_1900 ,HR_1950 ,HR_2000 ,HR_2050 ,HR_2100 ,HR_2150 ,HR_2200 ,HR_2250 ,HR_2300 ,HR_2350,NC_SUN) 
			  VALUES(@SDATE,''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',@NC)
	DECLARE @CNT FLOAT
	DECLARE @TREST NUMERIC(3,1)
	DECLARE @TWORK NUMERIC(3,1)
	SET @TREST=24
	SET @TWORK=0
	SET @CNT=0
	WHILE (@CNT<=23.5)
	BEGIN
		IF EXISTS( SELECT * FROM CP_CrewDailyWorkRestHours WHERE WORKREST=''W'' AND CREWNUMBER=@CREWNUMBER AND TRANSDATE=@SDATE AND @CNT BETWEEN FROMTIME AND TOTIME )
		BEGIN
			DECLARE @QRY VARCHAR(500)
			IF @CNT=0
			BEGIN
				SET @QRY=''UPDATE #RES SET HR_0000=''''W'''' WHERE TDATE='''''' + REPLACE(CONVERT(VARCHAR,@SDATE,106),'' '',''-'') + ''''''''
				SET @TWORK=@TWORK+.5
			END
			ELSE
			BEGIN
				SET @QRY=''UPDATE #RES SET HR_'' + REPLACE(STR(CAST(@CNT AS INT),2),'' '',''0'') + RIGHT(CAST(@CNT*100 AS INT),2) + ''=''''W'''' WHERE TDATE='''''' + REPLACE(CONVERT(VARCHAR,@SDATE,106),'' '',''-'') + ''''''''
				SET @TWORK=@TWORK+.5
			END
			EXEC ( @QRY )
		END
		SET @CNT=@CNT+.5
	END
	PRINT @TWORK
	
	SET @TREST=@TREST-@TWORK
	UPDATE #RES 
	SET 
	WORK=@TWORK,
	REST=@TREST, 
	COMMENTS=(SELECT NCREASONNAME FROM CP_NCREASON M WHERE M.NCREASONID IN(SELECT TOP 1 REASON FROM CP_NonConformanceReason WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=@SDATE))
	WHERE TDATE=@SDATE

	SET @SDATE=DATEADD(DAY,1,@SDATE)
END
--------------------
UPDATE #RES
SET 
R7=DBO.GET_MAXREST_IN7(@CREWNUMBER,TDATE),
R24=DBO.GET_MAXREST_IN24(@CREWNUMBER,TDATE)
--------------------
SELECT * FROM #RES ORDER BY TDATE
END

--SELECT * FROM CP_CrewDailyWorkRestHours WHERE CREWID=10






' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CP_GETDAILYWORKHRSMONTHLY]    Script Date: 06/15/2012 16:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CP_GETDAILYWORKHRSMONTHLY]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_CP_GETDAILYWORKHRSMONTHLY]
(
@CREWNUMBER VARCHAR(6),
@MONTH INT,
@YEAR INT
)
AS
BEGIN 

-- EXEC sp_CP_GETDAILYWORKHRSMONTHLY ''y00010'',6,2012  	 
--DROP TABLE #RES
SET NOCOUNT ON
CREATE TABLE #RES
(
TDATE SMALLDATETIME,
HR_0000 CHAR(7),
HR_0050 CHAR(7),
HR_0100 CHAR(7),
HR_0150 CHAR(7),
HR_0200 CHAR(7),
HR_0250 CHAR(7),
HR_0300 CHAR(7),
HR_0350 CHAR(7),
HR_0400 CHAR(7),
HR_0450 CHAR(7),
HR_0500 CHAR(7),
HR_0550 CHAR(7),
HR_0600 CHAR(7),
HR_0650 CHAR(7),
HR_0700 CHAR(7),
HR_0750 CHAR(7),
HR_0800 CHAR(7),
HR_0850 CHAR(7),
HR_0900 CHAR(7),
HR_0950 CHAR(7),
HR_1000 CHAR(7),
HR_1050 CHAR(7),
HR_1100 CHAR(7),
HR_1150 CHAR(7),
HR_1200 CHAR(7),
HR_1250 CHAR(7),
HR_1300 CHAR(7),
HR_1350 CHAR(7),
HR_1400 CHAR(7),
HR_1450 CHAR(7),
HR_1500 CHAR(7),
HR_1550 CHAR(7),
HR_1600 CHAR(7),
HR_1650 CHAR(7),
HR_1700 CHAR(7),
HR_1750 CHAR(7),
HR_1800 CHAR(7),
HR_1850 CHAR(7),
HR_1900 CHAR(7),
HR_1950 CHAR(7),
HR_2000 CHAR(7),
HR_2050 CHAR(7),
HR_2100 CHAR(7),
HR_2150 CHAR(7),
HR_2200 CHAR(7),
HR_2250 CHAR(7),
HR_2300 CHAR(7),
HR_2350 CHAR(7),
WORK NUMERIC(3,1),
REST NUMERIC(4,1),
COMMENTS VARCHAR(500),
R24 NUMERIC(3,1),
R7 NUMERIC(4,1),
NC_SUN CHAR(1)
)

DECLARE @DAYS INT
DECLARE @DCNT INT
DECLARE @SDATE SMALLDATETIME
DECLARE @EDATE SMALLDATETIME

--DECLARE @VESSELID INT
--DECLARE @CREWID INT
--DECLARE @MONTH INT
--DECLARE @YEAR INT
--
--SET @VESSELID =1
--SET @CREWID =3539
--SET @MONTH =5
--SET @YEAR =2012

SET @SDATE=STR(@YEAR) + ''-'' + STR(@MONTH) + ''-01''
SET @EDATE=DATEADD(MONTH,1,@SDATE)
SET @DAYS=DATEDIFF(DAY,@SDATE,@EDATE)
SET @EDATE=DATEADD(DAY,-1,@EDATE)

WHILE (@SDATE <=@EDATE)
BEGIN

	DECLARE @NC CHAR(1)
	SET @NC=''''
	IF EXISTS(SELECT * FROM dbo.CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=@SDATE)
		SET @NC=''N''
	ELSE
	BEGIN
		IF (DATENAME(WEEKDAY,@SDATE)=''Sunday'')
		SET @NC=''S''
	END
 
	INSERT INTO #RES(TDATE,HR_0000, HR_0050 ,HR_0100 ,HR_0150 ,HR_0200 ,HR_0250 ,HR_0300 ,HR_0350 ,HR_0400 ,HR_0450 ,HR_0500 ,HR_0550 ,HR_0600 ,HR_0650 ,HR_0700 ,HR_0750 ,HR_0800 ,HR_0850 ,HR_0900 ,HR_0950 ,HR_1000 ,HR_1050 ,HR_1100 ,HR_1150 ,HR_1200 ,HR_1250 ,HR_1300 ,HR_1350 ,HR_1400 ,HR_1450 ,HR_1500 ,HR_1550 ,HR_1600 ,HR_1650 ,HR_1700 ,HR_1750 ,HR_1800 ,HR_1850 ,HR_1900 ,HR_1950 ,HR_2000 ,HR_2050 ,HR_2100 ,HR_2150 ,HR_2200 ,HR_2250 ,HR_2300 ,HR_2350,NC_SUN) 
			  VALUES(@SDATE,''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',''R'',@NC)
	DECLARE @CNT FLOAT
	DECLARE @TREST NUMERIC(3,1)
	DECLARE @TWORK NUMERIC(3,1)
	SET @TREST=24
	SET @TWORK=0
	SET @CNT=0
	WHILE (@CNT<=23.5)
	BEGIN
		IF EXISTS( SELECT * FROM CP_CrewDailyWorkRestHours WHERE WORKREST=''W'' AND CREWNUMBER=@CREWNUMBER AND TRANSDATE=@SDATE AND @CNT BETWEEN FROMTIME AND TOTIME )
		BEGIN
			DECLARE @QRY VARCHAR(500)
			IF @CNT=0
			BEGIN
				SET @QRY=''UPDATE #RES SET HR_0000=''''W'''' WHERE TDATE='''''' + REPLACE(CONVERT(VARCHAR,@SDATE,106),'' '',''-'') + ''''''''
				SET @TWORK=@TWORK+.5
			END
			ELSE
			BEGIN
				SET @QRY=''UPDATE #RES SET HR_'' + REPLACE(STR(CAST(@CNT AS INT),2),'' '',''0'') + RIGHT(CAST(@CNT*100 AS INT),2) + ''=''''W'''' WHERE TDATE='''''' + REPLACE(CONVERT(VARCHAR,@SDATE,106),'' '',''-'') + ''''''''
				SET @TWORK=@TWORK+.5
			END
			EXEC ( @QRY )
		END
		SET @CNT=@CNT+.5
	END
	PRINT @TWORK
	
	SET @TREST=@TREST-@TWORK
	UPDATE #RES 
	SET 
	WORK=@TWORK,
	REST=@TREST, 
	COMMENTS=(SELECT NCREASONNAME FROM CP_NCREASON M WHERE M.NCREASONID IN(SELECT TOP 1 REASON FROM CP_NonConformanceReason WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=@SDATE))
	WHERE TDATE=@SDATE

	SET @SDATE=DATEADD(DAY,1,@SDATE)
END
--------------------
UPDATE #RES
SET 
R7=DBO.GET_MAXREST_IN7(@CREWNUMBER,TDATE),
R24=DBO.GET_MAXREST_IN24(@CREWNUMBER,TDATE)
--------------------
SELECT * FROM #RES ORDER BY TDATE
END

--SELECT * FROM CP_CrewDailyWorkRestHours WHERE CREWID=10






' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CP_GETDAILYWORKHRS]    Script Date: 06/15/2012 16:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CP_GETDAILYWORKHRS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[sp_CP_GETDAILYWORKHRS]
(
@CREWNUMBER VARCHAR(6),
@ONDATE SMALLDATETIME
)
AS
BEGIN
--   EXEC sp_CP_GETDAILYWORKHRS ''S00125'',''09-MAY-2012''
DECLARE @TAB TABLE (WTime float,WR CHAR(1))
DECLARE @CNT FLOAT
SET @CNT=0
WHILE (@CNT<=23.5)
BEGIN
	IF EXISTS( SELECT * FROM CP_CrewDailyWorkRestHours WHERE WORKREST=''R'' AND CREWNUMBER=@CREWNUMBER AND TRANSDATE=@ONDATE AND @CNT BETWEEN FROMTIME AND TOTIME )
	BEGIN
		INSERT INTO @TAB VALUES(@CNT,''R'')
	END
	ELSE
	BEGIN
		INSERT INTO @TAB VALUES(@CNT,''W'')
	END
	SET @CNT=@CNT+.5
END
SELECT * FROM @TAB ORDER BY WTime
END

--------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------------------





' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GET_MAXREST_IN7]    Script Date: 06/15/2012 16:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_MAXREST_IN7]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[GET_MAXREST_IN7]
(
@CREWNUMBER VARCHAR(6),
@FORDATE SMALLDATETIME
)
RETURNS NUMERIC(4,1)
AS
BEGIN

--DECLARE @VESSELID INT
--DECLARE @CREWID INT
--DECLARE @FORDATE SMALLDATETIME
--
--SET @VESSELID=49
--SET @CREWID=10
--SET @FORDATE=''17-MAY-2012''

	DECLARE @TDATE SMALLDATETIME
	DECLARE @RET TABLE (COL NUMERIC(4,1))
	SET @TDATE=@FORDATE
	SET @FORDATE=DATEADD(DAY,-6,@FORDATE)
	WHILE (@FORDATE <= @TDATE)
	BEGIN
		INSERT INTO @RET SELECT SUM(DURATION) FROM CP_CrewDailyWorkRestHours WHERE WORKREST=''R'' AND CREWNUMBER=@CREWNUMBER AND TRANSDATE>=@FORDATE AND TRANSDATE<=DATEADD(DAY,6,@FORDATE)
	SET @FORDATE=DATEADD(DAY,1,@FORDATE)
	END
--SELECT COL FROM @RET
RETURN ISNULL((SELECT MIN(COL) FROM @RET),0)
END


' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GET_MAXREST_IN24]    Script Date: 06/15/2012 16:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_MAXREST_IN24]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[GET_MAXREST_IN24]
(
@CREWNUMBER VARCHAR(6),
@FORDATE SMALLDATETIME
)
RETURNS NUMERIC(4,1)
AS
BEGIN

--DECLARE @VESSELID INT
--DECLARE @CREWID INT
--DECLARE @FORDATE SMALLDATETIME

DECLARE @WLAST NUMERIC(3,1)
DECLARE @WNEXT NUMERIC(3,1)
DECLARE @FTIME FLOAT
DECLARE @TTIME FLOAT
DECLARE @T1 FLOAT
DECLARE @T2 FLOAT
DECLARE @D1 SMALLDATETIME
DECLARE @D2 SMALLDATETIME

--SET @VESSELID=49
--SET @CREWID=10
--SET @FORDATE=''18-MAY-2012''

SET @WLAST=0
SET @WNEXT=0
SET @FTIME=0
SET @TTIME=0
SET @T1=0
SET @T2=0

	DECLARE @RET TABLE (COL NUMERIC(4,1))
	-- SET 0.0
	SET @D1=DATEADD(DAY,-1,@FORDATE)
	SET @D2=@FORDATE
	SET @TTIME=0.0
	SET @FTIME=@TTIME+0.5

	SELECT @WLAST=SUM(CASE WHEN FROMTIME < @FTIME THEN TOTIME-@FTIME+0.5 ELSE DURATION END) FROM CP_CrewDailyWorkRestHours WHERE WORKREST=''R'' AND CREWNUMBER=@CREWNUMBER AND TRANSDATE=@D1 AND TOTIME>=@FTIME
	SELECT @WNEXT=SUM(CASE WHEN TOTIME < @TTIME THEN DURATION ELSE @TTIME-FROMTIME+0.5 END) FROM CP_CrewDailyWorkRestHours WHERE WORKREST=''R'' AND CREWNUMBER=@CREWNUMBER AND TRANSDATE=@D2 AND FROMTIME<=@TTIME
	INSERT INTO @RET SELECT ISNULL(@WLAST,0)+ISNULL(@WNEXT,0)
	------------------------------------------------
	DECLARE CS CURSOR FOR SELECT TRANSDATE,FROMTIME,TOTIME FROM CP_CrewDailyWorkRestHours WHERE WORKREST=''R'' and CREWNUMBER=@CREWNUMBER AND TRANSDATE=@FORDATE
	OPEN CS
	FETCH NEXT FROM CS INTO @FORDATE,@FTIME,@TTIME
	WHILE(@@FETCH_STATUS=0)
	BEGIN
		-------------- LAST
		SET @WLAST=0
		SET @WNEXT=0
		SET @D1=DATEADD(DAY,-1,@FORDATE)
		SET @D2=@FORDATE
		SET @T2=@FTIME-0.5
		SET @T1=@T2+0.5
		SELECT @WLAST=SUM(CASE WHEN FROMTIME < @T1 THEN TOTIME-@T1+0.5 ELSE DURATION END) FROM CP_CrewDailyWorkRestHours WHERE WORKREST=''R'' AND CREWNUMBER=@CREWNUMBER AND TRANSDATE=@D1 AND TOTIME>=@T1
		SELECT @WNEXT=SUM(CASE WHEN TOTIME < @T2 THEN DURATION ELSE @T2-FROMTIME+0.5 END) FROM CP_CrewDailyWorkRestHours WHERE WORKREST=''R'' AND CREWNUMBER=@CREWNUMBER AND TRANSDATE=@D2 AND FROMTIME<=@T2
		INSERT INTO @RET SELECT ISNULL(@WLAST,0)+ISNULL(@WNEXT,0)
--		-------------- NEXT
		SET @WLAST=0
		SET @WNEXT=0
		SET @D1=@FORDATE
		SET @D2=DATEADD(DAY,1,@FORDATE)
		SET @T1=@TTIME+0.5
		SET @T2=@T1-0.5
		SELECT @WLAST=SUM(CASE WHEN FROMTIME < @T1 THEN TOTIME-@T1+0.5 ELSE DURATION END) FROM CP_CrewDailyWorkRestHours WHERE WORKREST=''R'' AND CREWNUMBER=@CREWNUMBER AND TRANSDATE=@D1 AND TOTIME>=@T1
		SELECT @WNEXT=SUM(CASE WHEN TOTIME < @T2 THEN DURATION ELSE @T2-FROMTIME+0.5 END) FROM CP_CrewDailyWorkRestHours WHERE WORKREST=''R'' AND CREWNUMBER=@CREWNUMBER AND TRANSDATE=@D2 AND FROMTIME<=@T2
		INSERT INTO @RET SELECT ISNULL(@WLAST,0)+ISNULL(@WNEXT,0)
	FETCH NEXT FROM CS INTO @FORDATE,@FTIME,@TTIME
	END
	CLOSE CS
	DEALLOCATE CS	
	------------------------------------------------
	-- SET 23.5
	SET @D1=@FORDATE
	SET @D2=DATEADD(DAY,1,@FORDATE)
	SET @FTIME=23.5
	SET @TTIME=@FTIME-0.5

	SELECT @WLAST=SUM(CASE WHEN FROMTIME < @FTIME THEN TOTIME-@FTIME+0.5 ELSE DURATION END) FROM CP_CrewDailyWorkRestHours WHERE WORKREST=''R'' AND CREWNUMBER=@CREWNUMBER AND TRANSDATE=@D1 AND TOTIME>=@FTIME
	SELECT @WNEXT=SUM(CASE WHEN TOTIME < @TTIME THEN DURATION ELSE @TTIME-FROMTIME+0.5 END) FROM CP_CrewDailyWorkRestHours WHERE WORKREST=''R'' AND CREWNUMBER=@CREWNUMBER AND TRANSDATE=@D2 AND FROMTIME<=@TTIME
	INSERT INTO @RET SELECT ISNULL(@WLAST,0)+ISNULL(@WNEXT,0)


	--SELECT COL FROM @RET
RETURN ISNULL((SELECT MIN(COL) FROM @RET),0)
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CP_IUVesselCrewList]    Script Date: 08/03/2012 16:54:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CP_IUVesselCrewList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_CP_IUVesselCrewList]
(
	@TableID int,
	@CREWNUMBER varchar(6),
	@CREWNAME varchar(100),
	@RANKID int,
	@SIGNONDATE smalldatetime,
	@WATCHKEEPER bit
)
AS
BEGIN
SET NOCOUNT ON
	if not exists(select * from CP_VesselCrewList where CREWNUMBER=@CREWNUMBER  )
	BEGIN
		INSERT INTO CP_VesselCrewList
		(
			CREWNUMBER ,
			CREWNAME ,
			Active	
		)
		Values
		(
			@CREWNUMBER,
			@CREWNAME
			,1		
		)
		
	select @TableID=isnull(max(TableID),0)+1 from CP_VesselCrewSignOnOff
	INSERT INTO dbo.CP_VesselCrewSignOnOff
	(TableID,CrewNumber ,SignOnDt,WATCHKEEPER,RANKID )	VALUES(@TableID,@CREWNUMBER,@SIGNONDATE,@WATCHKEEPER,@RANKID )
      
      
      
  
		
	END
ELSE
BEGIN
	UPDATE CP_VesselCrewList SET
	CREWNAME=@CREWNAME	
	WHERE CREWNUMBER=@CREWNUMBER

	UPDATE  dbo.CP_VesselCrewSignOnOff SET
	   SignOnDt=@SIGNONDATE
      ,WatchKeeper=@WATCHKEEPER
      ,RankId=@RANKID
	WHERE TableID=@TableID

END
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CP_IUVESSELCREWSIGNONOFF]    Script Date: 06/15/2012 16:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CP_IUVESSELCREWSIGNONOFF]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE proc [dbo].[sp_CP_IUVESSELCREWSIGNONOFF]
(
@CREWNUMBER VARCHAR(6)
,@SIGNONDATE DATETIME
,@WATCHKEEPER BIT
,@RANKID INT
)
AS
BEGIN
	DECLARE @TableID INT 
	select @TableID=isnull(max(TableID),0)+1 from CP_VesselCrewSignOnOff
	INSERT INTO dbo.CP_VesselCrewSignOnOff
	(TableID,CrewNumber ,SignOnDt,WATCHKEEPER,RANKID )	VALUES(@TableID,@CREWNUMBER,@SIGNONDATE,@WATCHKEEPER,@RANKID )

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CP_IUCP_CrewDailyLocation]    Script Date: 06/15/2012 16:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CP_IUCP_CrewDailyLocation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[sp_CP_IUCP_CrewDailyLocation]
(
 @CrewNumber VARCHAR(6)
,@TableId INT
,@TDate smalldatetime
,@Location INT
)
AS
BEGIN

	IF NOT EXISTS(SELECT * FROM dbo.CP_CrewDailyLocation WHERE CrewNumber=@CrewNumber AND TableId=@TableId AND TDate=@TDate)
	BEGIN
		INSERT INTO dbo.CP_CrewDailyLocation (CrewNumber,TableId,TDate,Location )
		VALUES(@CrewNumber,@TableId,@TDate,@Location )
	END
	ELSE
	BEGIN
		UPDATE dbo.CP_CrewDailyLocation SET Location =@Location 
		WHERE CrewNumber=@CrewNumber AND TableId=@TableId AND TDate=@TDate 
	END
END
' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GETREST_SLOTS_IN_NEXT_24]    Script Date: 08/07/2012 16:57:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GETREST_SLOTS_IN_NEXT_24]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[GETREST_SLOTS_IN_NEXT_24]
(
	@TEMPDT SMALLDATETIME ,
	@CREWNUMBER VARCHAR(6),
	@FT NUMERIC(3,1)
)
RETURNS @DURS1 TABLE(COL NUMERIC(3,1))
AS
BEGIN

--	DECLARE @TEMPDT SMALLDATETIME 
--	DECLARE @CREWID INT
--	DECLARE @FT NUMERIC(3,1)
--
--	SET @CREWID=10
--	SET @TEMPDT=''29-AUG-2012''
--	SET @FT=5

	DECLARE @TT NUMERIC(3,1)

	DECLARE @DURS TABLE(COL NUMERIC(3,1))
	DECLARE @TAB TABLE(PK INT IDENTITY,TDATE SMALLDATETIME,FT NUMERIC(3,1),TT NUMERIC(3,1),DURATION NUMERIC(3,1))

	IF(@FT=0)
	BEGIN
		SET @TT=23.5
		INSERT INTO @DURS
		SELECT DURATION FROM CP_CrewDailyWorkRestHours WHERE CREWNUMBER=@CREWNUMBER AND WORKREST=''R'' AND TRANSDATE=@TEMPDT
	END
	ELSE
	BEGIN
		SET @TT=@FT-0.5
		INSERT INTO @TAB
		SELECT TRANSDATE,FROMTIME,TOTIME,DURATION FROM CP_CrewDailyWorkRestHours WHERE CREWNUMBER=@CREWNUMBER AND WORKREST=''R'' AND 
		(
			(TRANSDATE=@TEMPDT AND TOTIME >=@FT)
			OR
			(TRANSDATE=DATEADD(DAY,1,@TEMPDT) AND FROMTIME <=@TT)
		) ORDER BY TRANSDATE,FROMTIME

		DECLARE @T1 NUMERIC(3,1)
		DECLARE @T2 NUMERIC(3,1)
		DECLARE @DUR NUMERIC(3,1)
		DECLARE @PK INT

		DECLARE @D1 SMALLDATETIME
		DECLARE @D2 SMALLDATETIME
		SET @D1=@TEMPDT
		SET @D2=DATEADD(DAY,1,@TEMPDT)
		WHILE EXISTS(SELECT * FROM @TAB)
		BEGIN
			SELECT TOP 1 @PK=PK,@TEMPDT=TDATE,@T1=FT,@T2=TT,@DUR=DURATION FROM @TAB ORDER BY PK
			IF(@TEMPDT=@D1)
			BEGIN
				IF(@T1<@FT)
					SET @DUR=@T2-@FT +.5
				IF(@T2=23.5)
				BEGIN
					IF EXISTS(SELECT * FROM @TAB WHERE TDATE=@D2 AND FT=0)
					BEGIN	

						SET @DUR=@DUR + (SELECT (CASE WHEN TT>@TT THEN @TT-FT+0.5 ELSE DURATION END) FROM @TAB WHERE TDATE=@D2 AND FT=0)
						DELETE FROM @TAB WHERE TDATE=@D2 AND FT=0
					END
				END
				INSERT INTO @DURS VALUES (@DUR)
			END
			IF(@TEMPDT=@D2)
			BEGIN	
				IF(@T2>@TT)
				SET @DUR=@TT-@T1 +.5
				INSERT INTO @DURS VALUES (@DUR)
			END
			DELETE FROM @TAB WHERE PK=@PK
		END
	END
	INSERT INTO @DURS1 SELECT * FROM @DURS
	RETURN
END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CP_IUDAILYWORKHRS]    Script Date: 08/08/2012 12:34:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CP_IUDAILYWORKHRS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[sp_CP_IUDAILYWORKHRS]
(
@CREWNUMBER VARCHAR(6),
@ONDATE SMALLDATETIME,
@HRS_FROMLIST_Rest VARCHAR(1000),
@HRS_TOLIST_Rest VARCHAR(1000),
@HRS_FROMLIST_Work VARCHAR(1000),
@HRS_TOLIST_Work VARCHAR(1000),
@CALENDERORPOST CHAR(1)
)
AS
BEGIN 
DECLARE @TABLEID INT 
DECLARE @COMPTIME DATETIME
SET @TABLEID=NULL
SET @COMPTIME=GETDATE()
SET NOCOUNT ON
IF(@CALENDERORPOST=''P'')
BEGIN
	DELETE FROM CP_CrewDailyWorkRestHours WHERE CREWNUMBER=@CREWNUMBER AND TRANSDATE=@ONDATE
	INSERT INTO CP_CrewDailyWorkRestHours (CREWNUMBER,TRANSDATE,WORKREST,FROMTIME,TOTIME,DURATION,ENTEREDBY,ENTEREDON) SELECT @CREWNUMBER,@ONDATE,''R'',RESULT,RESULT1,RESULT1-RESULT+.5,1,GETDATE() FROM DBO.CSVtoTable2Float(@HRS_FROMLIST_Rest,@HRS_TOLIST_Rest,'','')	
	INSERT INTO CP_CrewDailyWorkRestHours (CREWNUMBER,TRANSDATE,WORKREST,FROMTIME,TOTIME,DURATION,ENTEREDBY,ENTEREDON) SELECT @CREWNUMBER,@ONDATE,''W'',RESULT,RESULT1,RESULT1-RESULT+.5,1,GETDATE() FROM DBO.CSVtoTable2Float(@HRS_FROMLIST_Work,@HRS_TOLIST_Work,'','')	
	DECLARE @PKID BIGINT
	SELECT @PKID=ISNULL(MAX(TABLEID),0)+1 FROM CP_CREWHOURSLOG
	INSERT INTO CP_CREWHOURSLOG (TABLEID,CREWNUMBER,FORDATE,UPDATEDON) VALUES(@PKID,@CREWNUMBER,@ONDATE,GETDATE())
END
------------------------------------ GENERATING DATA FOR -1 MONTH -15 TO +1 MONTH +15 DAYS ON THE CALC DATE
DECLARE @CNT INT
DECLARE @TEMPDT SMALLDATETIME
DECLARE @ENDDT SMALLDATETIME
SET @TEMPDT=STR(YEAR(@ONDATE))+ ''-''  + STR(MONTH(@ONDATE)) + ''-01''
SET @ENDDT=DATEADD(DAY,-1,DATEADD(MONTH,1,@TEMPDT))
SET @TEMPDT=DATEADD(DAY,-15,@TEMPDT)
SET @ENDDT=DATEADD(DAY,15,@ENDDT)
WHILE(@TEMPDT<=@ENDDT)
BEGIN
	IF NOT EXISTS(SELECT * FROM CP_CrewDailyWorkRestHours WHERE CREWNUMBER=@CREWNUMBER AND TRANSDATE=@TEMPDT)
	BEGIN
		INSERT INTO CP_CrewDailyWorkRestHours (CREWNUMBER,TRANSDATE,WORKREST,FROMTIME,TOTIME,DURATION,ENTEREDBY,ENTEREDON) SELECT @CREWNUMBER,@TEMPDT,''R'',RESULT,RESULT1,24.0,0,GETDATE() FROM DBO.CSVtoTable2Float(''0'',''23.5'','','')
	END
	SET @TEMPDT=DATEADD(DAY,1,@TEMPDT)
END
IF(@CALENDERORPOST=''C'')
BEGIN
	RETURN;
END
------------------------------------ 
SET @TEMPDT=DATEADD(DAY,-6,@ONDATE)
--DECLARE @FROMTIME FLOAT
DECLARE @STTIME FLOAT
DECLARE @ENDTIME FLOAT
DECLARE @T_FULLDAYWORK BIT
DECLARE @CALCDATE SMALLDATETIME 

DECLARE @TEMPCS TABLE(TRANSDATE SMALLDATETIME,FROMTIME FLOAT)
INSERT INTO @TEMPCS
SELECT TRANSDATE,FROMTIME FROM CP_CrewDailyWorkRestHours WHERE CREWNUMBER=@CREWNUMBER AND WORKREST=''W'' AND TRANSDATE BETWEEN @TEMPDT AND DATEADD(DAY,6,@ONDATE) ORDER BY TRANSDATE ASC,FROMTIME ASC
SET @CALCDATE=@TEMPDT
WHILE (@CALCDATE<=DATEADD(DAY,6,@ONDATE))
BEGIN
	IF NOT EXISTS(SELECT * FROM @TEMPCS WHERE TRANSDATE=@CALCDATE)
	INSERT INTO @TEMPCS VALUES(@CALCDATE,0.0)

	SET @CALCDATE=DATEADD(DAY,1,@CALCDATE)
END

DECLARE CS CURSOR FOR SELECT TRANSDATE,FROMTIME FROM @TEMPCS ORDER BY TRANSDATE ASC,FROMTIME ASC
OPEN CS
FETCH NEXT FROM CS INTO @TEMPDT,@STTIME
WHILE(@@FETCH_STATUS=0)
BEGIN

	SET @ENDTIME=@STTIME-.5

	DECLARE @NC7 CHAR(1)
	DECLARE @PARTFIRST FLOAT
	DECLARE @PARTMIDDLE FLOAT
	DECLARE @PARTLAST FLOAT

	----====================================================================
	---------------- 7 DAY CALCULATION
	DECLARE @RESTIN7 FLOAT
	SET @RESTIN7=0

	SET @PARTFIRST =0
	SET @PARTMIDDLE =0
	SET @PARTLAST =0
	SET @NC7=''N''

	SELECT @RESTIN7=DBO.[GET_MAXREST_IN7](@CREWNUMBER,@TEMPDT)
	UPDATE CP_CrewDailyWorkRestHours SET RESTIN7=@RESTIN7 WHERE WORKREST=''W'' AND CREWNUMBER=@CREWNUMBER AND TRANSDATE=@TEMPDT AND FROMTIME=@STTIME 
	
	----------------- NON CONFORMANCE ENTRY
	DELETE FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE BETWEEN @TEMPDT AND DATEADD(DAY,6,@TEMPDT) AND COMPTIME <> @COMPTIME AND NCTYPE=7 -- AND CALCDATE=@ONDATE
		
	IF (@RESTIN7 < 77 )
	BEGIN
		IF NOT EXISTS(SELECT * FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=@TEMPDT AND CALCDATE=@ONDATE AND NCTYPE=7 AND COMPTIME=@COMPTIME)
			INSERT INTO CP_NonConformance VALUES(@CREWNUMBER,@TEMPDT,@ONDATE,7,@COMPTIME)
	END
	----------------------------------------------- 24 hr
	DECLARE @RESTIN24 FLOAT
	SET @RESTIN24=0
	---------------- 24 HOUR CALCULATION 
	IF(@TEMPDT=@ONDATE)
	BEGIN
		SELECT @RESTIN24=[dbo].[GET_MAXREST_IN24](@CREWNUMBER,@TEMPDT)
		UPDATE CP_CrewDailyWorkRestHours SET RESTIN24=@RESTIN24 WHERE WORKREST=''W'' AND CREWNUMBER=@CREWNUMBER AND TRANSDATE=@TEMPDT AND FROMTIME=@STTIME 
		----------------- NON CONFORMANCE ENTRY -- FOR 24
		DELETE FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE BETWEEN @TEMPDT AND DATEADD(DAY,1,@TEMPDT) AND COMPTIME <> @COMPTIME AND NCTYPE=24 AND CALCDATE=@ONDATE
		IF (@RESTIN24 < 10 )
		BEGIN
			IF NOT EXISTS(SELECT * FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=@TEMPDT AND CALCDATE=@ONDATE AND NCTYPE=24 AND COMPTIME=@COMPTIME)
			INSERT INTO CP_NonConformance VALUES(@CREWNUMBER,@TEMPDT,@ONDATE,24,@COMPTIME)
			
--			IF NOT EXISTS(SELECT * FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=DATEADD(DAY,1,@TEMPDT) AND CALCDATE=@ONDATE AND NCTYPE=24 AND COMPTIME=@COMPTIME)
--			INSERT INTO CP_NonConformance VALUES(@CREWNUMBER,DATEADD(DAY,1,@TEMPDT),@ONDATE,24,@COMPTIME)
		END
	END
	----====================================================================
FETCH NEXT FROM CS INTO @TEMPDT,@STTIME
END
CLOSE CS 
DEALLOCATE CS
--------------------------
----DECLARE CS1 CURSOR FOR SELECT TRANSDATE,FROMTIME FROM CP_CrewDailyWorkRestHours WHERE WORKREST=''R'' AND TRANSDATE=@TEMPDT ORDER BY TRANSDATE ASC,FROMTIME ASC
----OPEN CS1
----FETCH NEXT FROM CS1 INTO @TEMPDT,@STTIME
----WHILE(@@FETCH_STATUS=0)
----BEGIN
----		SET @ENDTIME=@STTIME-.5
----		DECLARE @DURATIONS TABLE(PERIOD NUMERIC(3,1),DDAY INT)
----
----		INSERT INTO @DURATIONS 
----		SELECT 
----		CASE 
----		WHEN FROMTIME=0.0 THEN DURATION + ( SELECT TOP 1 DURATION FROM CP_CrewDailyWorkRestHours D WHERE D.WORKREST=''R'' AND CREWNUMBER=@CREWNUMBER AND TRANSDATE=DATEADD(DAY,-1,@TEMPDT) ORDER BY FROMTIME DESC)
----		WHEN TOTIME=23.5 THEN DURATION + ( SELECT TOP 1 DURATION FROM CP_CrewDailyWorkRestHours D WHERE D.WORKREST=''R'' AND CREWNUMBER=@CREWNUMBER AND TRANSDATE=DATEADD(DAY,1,@TEMPDT) ORDER BY FROMTIME ASC)
----		ELSE DURATION
----		END,1
----		FROM CP_CrewDailyWorkRestHours 
----		WHERE WORKREST=''R'' AND CREWNUMBER=@CREWNUMBER AND TRANSDATE=@TEMPDT AND FROMTIME >=@STTIME
----		-- LASTDAY
----		IF @STTIME<>0.0 
----		BEGIN
----			INSERT INTO @DURATIONS 
----			SELECT ISNULL(CASE WHEN TOTIME < @ENDTIME THEN DURATION ELSE @ENDTIME-FROMTIME+.5 END,0),2 FROM CP_CrewDailyWorkRestHours 
----			WHERE WORKREST=''R'' AND CREWNUMBER=@CREWNUMBER AND TRANSDATE=dateadd(day,1,@TEMPDT) AND FROMTIME <=@STTIME
----		END
----		----------------- NON CONFORMANCE ENTRY -- FOR 6
----		DELETE FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE BETWEEN @TEMPDT AND DATEADD(DAY,1,@TEMPDT) AND COMPTIME <> @COMPTIME AND NCTYPE=6 AND CALCDATE=@ONDATE
----		IF (NOT EXISTS(SELECT * FROM @DURATIONS WHERE PERIOD >=6.0))
----		BEGIN
----			IF NOT EXISTS(SELECT * FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=@TEMPDT AND CALCDATE=@ONDATE AND NCTYPE=6 AND COMPTIME=@COMPTIME)
----			INSERT INTO CP_NonConformance VALUES(@CREWNUMBER,@TEMPDT,@ONDATE,6,@COMPTIME)
----			IF NOT EXISTS(SELECT * FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=DATEADD(DAY,1,@TEMPDT) AND CALCDATE=@ONDATE AND NCTYPE=6 AND COMPTIME=@COMPTIME)
----			INSERT INTO CP_NonConformance VALUES(@CREWNUMBER,DATEADD(DAY,1,@TEMPDT),@ONDATE,6,@COMPTIME)
----		END
----		----------------- NON CONFORMANCE ENTRY -- FOR 2
----		DELETE FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE BETWEEN @TEMPDT AND DATEADD(DAY,1,@TEMPDT) AND COMPTIME <> @COMPTIME AND NCTYPE=2 AND CALCDATE=@ONDATE
----		IF (SELECT SUM(PERIOD) FROM (SELECT TOP 2 PERIOD FROM @DURATIONS ORDER BY PERIOD DESC) A) < 10 AND (SELECT SUM(PERIOD) FROM @DURATIONS) >= 10
----		BEGIN
----			IF NOT EXISTS(SELECT * FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=@TEMPDT AND CALCDATE=@ONDATE AND NCTYPE=2 AND COMPTIME=@COMPTIME)
----			INSERT INTO CP_NonConformance VALUES(@CREWNUMBER,@TEMPDT,@ONDATE,2,@COMPTIME)
----			IF NOT EXISTS(SELECT * FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=DATEADD(DAY,1,@TEMPDT) AND CALCDATE=@ONDATE AND NCTYPE=2 AND COMPTIME=@COMPTIME)
----			INSERT INTO CP_NonConformance VALUES(@CREWNUMBER,DATEADD(DAY,1,@TEMPDT),@ONDATE,2,@COMPTIME)
----		END
----
----FETCH NEXT FROM CS1 INTO @TEMPDT,@STTIME
----END
----CLOSE CS1 
----DEALLOCATE CS1


DELETE FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE BETWEEN DATEADD(DAY,-1,@ONDATE) AND DATEADD(DAY,1,@ONDATE) AND NCTYPE=6 AND CALCDATE=@ONDATE 
DELETE FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE BETWEEN DATEADD(DAY,-1,@ONDATE) AND DATEADD(DAY,1,@ONDATE) AND NCTYPE=2 AND CALCDATE=@ONDATE 

DECLARE @DURATIONS TABLE(PERIOD NUMERIC(3,1))
DECLARE @DUR_LOOP TABLE(PK INT IDENTITY,TRANSDATE SMALLDATETIME,FROMTIME NUMERIC(3,1))
INSERT INTO @DUR_LOOP 
SELECT TRANSDATE,(CASE WHEN (TRANSDATE=DATEADD(DAY,-1,@ONDATE) AND FROMTIME <=0.5) THEN 0.5 ELSE FROMTIME END) AS FROMTIME
FROM CP_CrewDailyWorkRestHours WHERE CREWNUMBER=@CREWNUMBER AND WORKREST=''W'' 
AND 
(
	( TRANSDATE=DATEADD(DAY,-1,@ONDATE) AND TOTIME >=0.5 )
	OR
	( TRANSDATE=@ONDATE AND FROMTIME <=23.5)
)
ORDER BY TRANSDATE,FROMTIME

IF NOT EXISTS(SELECT * FROM @DUR_LOOP WHERE TRANSDATE=DATEADD(DAY,-1,@ONDATE))
	INSERT INTO @DUR_LOOP VALUES(DATEADD(DAY,-1,@ONDATE),23.5)

DECLARE @DEL_PK INT
SET @DEL_PK=0
WHILE EXISTS(SELECT TOP 1 * FROM @DUR_LOOP ORDER BY PK)
BEGIN	
		SELECT TOP 1 @DEL_PK=PK,@TEMPDT=TRANSDATE,@STTIME=FROMTIME FROM @DUR_LOOP ORDER BY PK

		DELETE FROM @DURATIONS
		INSERT INTO @DURATIONS SELECT * FROM DBO.GETREST_SLOTS_IN_NEXT_24(@TEMPDT,@CREWNUMBER,@STTIME)
		
		IF (NOT EXISTS(SELECT * FROM @DURATIONS WHERE PERIOD >=6.0))		
		BEGIN
			IF ( @TEMPDT < @ONDATE ) 
			BEGIN
				IF NOT EXISTS(SELECT * FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=DATEADD(DAY,-1,@ONDATE) AND CALCDATE=@ONDATE AND NCTYPE=6 AND COMPTIME=@COMPTIME)
					INSERT INTO CP_NonConformance VALUES(@CREWNUMBER,DATEADD(DAY,-1,@ONDATE),@ONDATE,6,@COMPTIME)
				IF NOT EXISTS(SELECT * FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=@ONDATE AND CALCDATE=@ONDATE AND NCTYPE=6 AND COMPTIME=@COMPTIME)
					INSERT INTO CP_NonConformance VALUES(@CREWNUMBER,@ONDATE,@ONDATE,6,@COMPTIME)
			END

			IF ( @TEMPDT = @ONDATE )
			BEGIN
				IF(@STTIME=0)
				BEGIN
					IF NOT EXISTS(SELECT * FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=@ONDATE AND CALCDATE=@ONDATE AND NCTYPE=6 AND COMPTIME=@COMPTIME)
						INSERT INTO CP_NonConformance VALUES(@CREWNUMBER,@ONDATE,@ONDATE,6,@COMPTIME)
				END
				ELSE
				BEGIN
					IF NOT EXISTS(SELECT * FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=@ONDATE AND CALCDATE=@ONDATE AND NCTYPE=6 AND COMPTIME=@COMPTIME)
						INSERT INTO CP_NonConformance VALUES(@CREWNUMBER,@ONDATE,@ONDATE,6,@COMPTIME)
					IF NOT EXISTS(SELECT * FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=DATEADD(DAY,1,@ONDATE) AND CALCDATE=@ONDATE AND NCTYPE=6 AND COMPTIME=@COMPTIME)
						INSERT INTO CP_NonConformance VALUES(@CREWNUMBER,DATEADD(DAY,1,@ONDATE),@ONDATE,6,@COMPTIME)
				END
			END
		END

		IF (SELECT SUM(PERIOD) FROM (SELECT TOP 2 PERIOD FROM @DURATIONS ORDER BY PERIOD DESC) A) < 10 AND (SELECT SUM(PERIOD) FROM @DURATIONS) >= 10
		BEGIN
			IF ( @TEMPDT < @ONDATE ) 
			BEGIN
				IF NOT EXISTS(SELECT * FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=DATEADD(DAY,-1,@ONDATE) AND CALCDATE=@ONDATE AND NCTYPE=2 AND COMPTIME=@COMPTIME)
					INSERT INTO CP_NonConformance VALUES(@CREWNUMBER,DATEADD(DAY,-1,@ONDATE),@ONDATE,2,@COMPTIME)
				IF NOT EXISTS(SELECT * FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=@ONDATE AND CALCDATE=@ONDATE AND NCTYPE=2 AND COMPTIME=@COMPTIME)
					INSERT INTO CP_NonConformance VALUES(@CREWNUMBER,@ONDATE,@ONDATE,2,@COMPTIME)
			END

			IF ( @TEMPDT = @ONDATE )
			BEGIN
				IF(@STTIME=0)
				BEGIN
					IF NOT EXISTS(SELECT * FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=@ONDATE AND CALCDATE=@ONDATE AND NCTYPE=2 AND COMPTIME=@COMPTIME)
						INSERT INTO CP_NonConformance VALUES(@CREWNUMBER,@ONDATE,@ONDATE,2,@COMPTIME)
				END
				ELSE
				BEGIN
					IF NOT EXISTS(SELECT * FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=@ONDATE AND CALCDATE=@ONDATE AND NCTYPE=2 AND COMPTIME=@COMPTIME)
						INSERT INTO CP_NonConformance VALUES(@CREWNUMBER,@ONDATE,@ONDATE,2,@COMPTIME)
					IF NOT EXISTS(SELECT * FROM CP_NonConformance WHERE CREWNUMBER=@CREWNUMBER AND NCDATE=DATEADD(DAY,1,@ONDATE) AND CALCDATE=@ONDATE AND NCTYPE=2 AND COMPTIME=@COMPTIME)
						INSERT INTO CP_NonConformance VALUES(@CREWNUMBER,DATEADD(DAY,1,@ONDATE),@ONDATE,2,@COMPTIME)
				END
			END
		END
	DELETE FROM @DUR_LOOP WHERE PK=@DEL_PK
END

END
--EXEC DBO.sp_CP_IUDAILYWORKHRS 1,''S00031'',''14-MAY-2012'','''','''',''0'',''23.5'',''P''




' 
END
GO
/****** Object:  StoredProcedure [dbo].[GET_DATA_PACKET]    Script Date: 08/03/2012 14:54:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_DATA_PACKET]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GET_DATA_PACKET]
GO

/****** Object:  StoredProcedure [dbo].[GET_DATA_PACKET]    Script Date: 08/03/2012 14:54:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_DATA_PACKET]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[GET_DATA_PACKET]
(
	@VESSCODE VARCHAR(3),
	@MONTH INT ,
	@YEAR INT
)
AS
BEGIN
	DECLARE @FDATE DATETIME
	DECLARE @TDATE DATETIME
	
	SET @FDATE=CONVERT(DATETIME,CONVERT(VARCHAR(4),@YEAR )+''-''+CONVERT(VARCHAR(2),@MONTH)+''-01'')
	SET @TDATE =DATEADD(mm,1, @FDATE)
--	SET @FDATE=DATEADD(DD,-7,@FDATE)
--	SET @TDATE =DATEADD(DD,8,@TDATE )
	

	SELECT @VESSCODE as VESSCODE,@MONTH as [MONTH],@YEAR AS [YEAR]
	Select * from dbo.CP_VesselCrewSignOnOff 
	WHERE	(SIGNONDT>=  @FDATE AND SIGNOFFDT<= @TDATE ) -- MIDDLE AND EQUAL
			OR (SIGNONDT>=  @FDATE AND SIGNONDT<= @TDATE )			-- SIGN ON DATE IN BETWEEN
			OR (SIGNOFFDT>=  @FDATE AND SIGNOFFDT<= @TDATE )			-- SIGN OFF DATE IN BETWEEN
			OR (SIGNONDT<=@TDATE AND SIGNOFFDT IS NULL ) 			--
			OR (SIGNONDT<= @FDATE AND SIGNOFFDT >= @TDATE )			--
	Select * from dbo.CP_CrewDailyLocation WHERE TDATE>= @FDATE AND TDATE< @TDATE 
	Select * from dbo.CP_CrewDailyWorkRestHours WHERE TRANSDATE>= @FDATE AND TRANSDATE< @TDATE 
	Select * from dbo.CP_CrewHoursLog WHERE FORDATE>= @FDATE AND FORDATE< @TDATE 
	Select * from dbo.CP_NonConformance WHERE NCDATE>= @FDATE AND NCDATE< @TDATE 
	Select * from dbo.CP_NonConformanceReason WHERE NCDATE>= @FDATE AND NCDATE< @TDATE 
	
			

END

' 
END
GO

--- =================================================== DATA =================================================

/****** Object:  Table [dbo].[CP_NCReason]    Script Date: 06/15/2012 16:44:23 ******/
DELETE FROM [dbo].[CP_NCReason]
GO
/****** Object:  Table [dbo].[CP_Settings]    Script Date: 06/15/2012 16:44:23 ******/
DELETE FROM [dbo].[CP_Settings]
GO
/****** Object:  Table [dbo].[CP_Rank]    Script Date: 06/15/2012 16:44:23 ******/
DELETE FROM [dbo].[CP_Rank]
GO
/****** Object:  Table [dbo].[CP_NCType]    Script Date: 06/15/2012 16:44:23 ******/
DELETE FROM [dbo].[CP_NCType]
GO
/****** Object:  Table [dbo].[CP_NCType]    Script Date: 06/15/2012 16:44:23 ******/
INSERT [dbo].[CP_NCType] ([NCTypeId], [NCTypeName]) VALUES (2, N'Minimum Total Rest Comprises More Than 2 Periods.')
INSERT [dbo].[CP_NCType] ([NCTypeId], [NCTypeName]) VALUES (6, N'No Minimum 6 Hrs Consecutive Rest Period.')
INSERT [dbo].[CP_NCType] ([NCTypeId], [NCTypeName]) VALUES (7, N'Minimum 77 Hrs Rest in Each 7 Day Period.')
INSERT [dbo].[CP_NCType] ([NCTypeId], [NCTypeName]) VALUES (24, N'Minimum 10 Hrs Rest in Any 24 Hrs Period.')
/****** Object:  Table [dbo].[CP_Rank]    Script Date: 06/15/2012 16:44:23 ******/
SET IDENTITY_INSERT [dbo].[CP_Rank] ON
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (1, 1, 2, N'MSTR', N'MASTER', N'O', N'D', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'1', N'SMOU', N'Officer', N'Master')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (2, 2, 5, N'C/O', N'Chief Officer', N'O', N'D', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'2', N'SMOU', N'Officer', N'Chief Officer')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (4, 3, 9, N' 2/O', N'Second Officer', N'O', N'D', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'3', N'SMOU', N'Officer', N'2nd Officer')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (6, 3, 14, N'3/O', N'Third Officer', N'O', N'D', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'4', N'SMOU', N'Officer', N'3rd Officer')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (8, 3, 20, N'J/O', N'JUNIOR OFFICER', N'O', N'D', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N' ', N' ', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (9, 7, 25, N'DCDT', N'Deck Cadet', N'R', N'D', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'5', N'SMOU', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (12, 51, 35, N'C/E', N'Chief Engineer', N'O', N'E', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'18', N'SMOU', N'Engineer', N'Chief Engineer')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (13, 51, 35, N'AC/E', N'ADDITIONAL C/ENG', N'O', N'E', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'24', N'SMOU', N'Engineer', N'Chief Engineer')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (15, 53, 45, N'1 A/E', N'Second Engineer', N'O', N'E', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'19', N'SMOU', N'Engineer', N'1st Engineer')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (16, 54, 49, N'2 A/E', N'Third Engineer', N'O', N'E', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'20', N'SMOU', N'Engineer', N'2nd Engineer')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (17, 54, 63, N'3 A/E', N'Fourth Engineer', N'O', N'E', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'21', N'SMOU', N'Engineer', N'3rd Engineer')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (19, 54, 58, N'J/E', N'Junior Engineer', N'O', N'E', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'22', N'SMOU', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (21, 59, 65, N'ECDT', N' ENGINE CADET', N'R', N'E', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'23', N'SMOU', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (22, 60, 67, N'R/E', N' REEFER ENGINEER', N'O', N'E', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'D', N' ', N' ', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (23, 147, 68, N'G/E', N' GAS ENGINEER', N'O', N'E', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, CAST(0xA10102BF AS SmallDateTime), N'A', N' ', N' ', N'Engineer', N'Gas/Cargo Engineer')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (24, 62, 69, N'ETO', N' Electrical Engineer', N'O', N'E', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'D', N' ', N' ', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (25, 62, 72, N'E/O', N' Electronic Officer', N'O', N'E', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N' ', N'SMOU', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (26, 62, 73, N'AE/O', N' ASST. ELECTRICAL OFFICER', N'O', N'E', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N' ', N'SMOU', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (27, 62, 75, N'TE/O', N' TRAINEE E/O', N'O', N'E', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'D', N' ', N' ', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (28, 10, 23, N'P/M', N' PUMPMAN', N'R', N'D', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'11', N'SOS', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (29, 8, 79, N'T/PM', N' TR.PUMPMAN', N'R', N'D', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N' ', N' ', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (30, 65, 82, N'FTR', N' FITTER', N'R', N'E', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'29', N'SOS', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (31, 100, 85, N' GP1', N' GP RATING1', N'R', N'G', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'D', N' ', N' ', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (32, 101, 86, N' GP2', N' GP RATING2', N'R', N'G', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'D', N' ', N' ', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (33, 102, 87, N' GP3', N' GP RATING3', N'R', N'G', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'D', N' ', N' ', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (34, 12, 23, N'BSN', N' BOSUN', N'R', N'D', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'13', N'SOS', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (35, 8, 24, N' A/B', N' ABLE BODIED SEAMAN', N'R', N'D', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'14', N'SOS', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (36, 8, 23, N' O/S', N' ORDINARY SEAMAN', N'R', N'D', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'14', N'SOS', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (37, 8, 100, N' TO/S', N' TRAINEE O/S', N'R', N'D', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'15', N'SOS', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (38, 66, 64, N' OLR', N' OILER', N'R', N'E', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'30', N'SOS', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (39, 66, 104, N'M/M', N' MOTORMAN', N'R', N'E', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'31', N'SOS', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (40, 66, 105, N'WPR', N' WIPER', N'R', N'E', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N' ', N' ', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (41, 66, 106, N' TWPR', N' TRAINEE WIPER', N'R', N'E', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N' ', N' ', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (42, 125, 110, N' C/CK', N' CHIEF COOK', N'R', N'C', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'45', N'SOS', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (43, 125, 117, N' 2/CK', N' 2ND COOK', N'R', N'C', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N' ', N' ', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (44, 125, 117, N' T/CK', N' TRAINEE COOK', N'R', N'C', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N' ', N' ', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (45, 125, 117, N'STWD', N' STEWARD', N'R', N'D', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'46', N'SOS', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (46, 125, 118, N'MSM', N'MSM', N'R', N'C', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'46', N'SOS', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (47, 125, 119, N' TMSM', N' TRAINEE MESSMAN', N'R', N'C', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'D', N' ', N' ', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (48, 141, 200, N'SUPT', N' SUPERINTENDENT', N' ', N'X', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N' ', N' ', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (49, 142, 201, N'SUPY', N' SUPERNUMERARY', N' ', N' ', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, CAST(0x9A79042D AS SmallDateTime), N'A', N' ', N' ', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (50, 66, 125, N'WLDR', N' WELDER', N'R', N'E', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'29', N'SOS', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (51, 71, 128, N' TECH', N' TECHNICIAN', N'R', N'E', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'D', N' ', N' ', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (53, 103, 135, N' RFTR', N' REPAIR TEAM FITTER', N'R', N'G', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N' ', N' ', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (54, 104, 136, N' RWDR', N' REPAIR TEAM WELDER', N'R', N'G', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'D', N' ', N' ', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (55, 105, 137, N'RCLR', N' RPTM CLEANER', N'R', N'D', CAST(0.0 AS Numeric(5, 1)), 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'29', N'SOS', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (60, 2, 5, N'AST C/O', N'AST C/O', N'O', N'D', NULL, 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'7', N'SMOU', N'Officer', N'Chief Officer')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (169, 1, 3, N'TR MSTR', N'TR MASTER', N'O', N'D', NULL, 1, CAST(0x9D160383 AS SmallDateTime), 1, NULL, N'A', N'6', N'SMOU', N'Officer', N'Master')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (170, 145, 139, N'R A/B', N'Riding A/B', N'R', N' ', NULL, 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N' ', N'SOS', N' ', N' ')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (171, 3, 15, N'A 3/O', N'A 3/O', N'O', N'D', NULL, 1, CAST(0xA06D03EA AS SmallDateTime), 1, NULL, N'A', N'4', N'SMOU', N'Officer', N'3rd Officer')
INSERT [dbo].[CP_Rank] ([RankId], [RankGroupId], [RankLevel], [RankCode], [RankName], [OffCrew], [OffGroup], [LeavesAllowed], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [StatusId], [Rank_Mum], [Smou_Code], [SireRankType], [SireRank]) VALUES (172, 8, 23, N'R/OS', N'R/OS', N'R', N'G', NULL, 1, CAST(0x9E7F039A AS SmallDateTime), NULL, NULL, N'A', N'14', N' ', N' ', N' ')
SET IDENTITY_INSERT [dbo].[CP_Rank] OFF
/****** Object:  Table [dbo].[CP_Settings]    Script Date: 06/15/2012 16:44:23 ******/
INSERT [dbo].[CP_Settings] ([VesselCode], [VesselName], [Lrimonumber], [Flag], [AdminPassword]) VALUES (N'DMV', N'DEMO VESSEL', N'', N'BAHAMAS', N'vx/kiVeG9SLWq8U6azCFRg==')
/****** Object:  Table [dbo].[CP_NCReason]    Script Date: 06/15/2012 16:44:23 ******/
INSERT [dbo].[CP_NCReason] ([NCReasonId], [NCReasonName]) VALUES (1, N'At Sea Cargo operations')
INSERT [dbo].[CP_NCReason] ([NCReasonId], [NCReasonName]) VALUES (2, N'At Sea Navigation')
INSERT [dbo].[CP_NCReason] ([NCReasonId], [NCReasonName]) VALUES (3, N'At Sea Ship Maintenance')
INSERT [dbo].[CP_NCReason] ([NCReasonId], [NCReasonName]) VALUES (4, N'In Port Cargo Operations')
INSERT [dbo].[CP_NCReason] ([NCReasonId], [NCReasonName]) VALUES (5, N'In Port Ship Maintenance')
INSERT [dbo].[CP_NCReason] ([NCReasonId], [NCReasonName]) VALUES (6, N'In Port Maneuvering')


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Accounts](
	[AccountId] [int] NULL,
	[AccountNumber] [int] NULL,
	[AccountName] [varchar](50) NULL,
	[prtype] [char](1) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (2, 1300, N'Bonded Stores on Board', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (65, 5179, N'Cabin/Galley Stores', NULL)
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (231, 1780, N'CLS - Victualling', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (66, 5180, N'Victuals', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (67, 5301, N'Paint & Equipment', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (68, 5302, N'Mooring & Rigging Equipmt', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (69, 5303, N'Nautical & Office Equipmt', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (70, 5304, N'Safety Fire Fighting Eq', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (71, 5305, N'Other Deck Consumables', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (72, 5311, N'Tools & Instruments', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (73, 5312, N'Chemicals & Gases', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (74, 5315, N'Electrical Stores', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (75, 5316, N'Packing & Gaskets', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (76, 5317, N'Other Engine Consumables', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (77, 5318, N'Welding Equipment', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (78, 5331, N'ME System Oil', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (79, 5332, N'ME Cylinder Oil', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (80, 5333, N'Hydraulic Oil', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (81, 5334, N'Other Lubricants', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (260, 5335, N'AE System Oil', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (82, 5401, N'Outside Shell & Main Deck', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (83, 5402, N'Double Bottom', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (84, 5403, N'Double Skin', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (85, 5404, N'Super Structure', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (86, 5405, N'Cargo Tanks Maintenance', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (87, 5406, N'Agency Pre-funding Technical', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (88, 5407, N'Cargo Tanks Phenoline', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (89, 5408, N'Cargo Tanks Stainless Ste', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (90, 5415, N'Mooring / Rigging', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (91, 5416, N'Navigation & Communication Equipment', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (92, 5417, N'Communication Equip', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (93, 5418, N'Safety Equipment', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (94, 5425, N'Officer/Crew Accomodation', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (95, 5426, N'Garbage / Sanitation', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (96, 5431, N'Main Engine & Propulsion', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (97, 5432, N'Propeller Shafting/Steer', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (98, 5435, N'Auxiliary Diesels', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (99, 5437, N'Auxiliary Machinery', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (100, 5438, N'Boiler', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (101, 5439, N'Oil System', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (102, 5440, N'Other System', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (103, 5442, N'Electrical & Ctrl Equip / Automation', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (104, 5444, N'Cargo System', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (105, 5445, N'Cargo Ctrl System', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (106, 5446, N'Tank Cleaning System', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (107, 5447, N'Tank Heating System', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (108, 5448, N'Other Cargo Handling Syst', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (109, 5449, N'Other Repairs/Maintenance', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (110, 5451, N'Land Transport/Storage', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (239, 5453, N'Paint & Equipment', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (111, 5461, N'Survey Fees', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (116, 5522, N'Freshwater-Crew', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (117, 5523, N'Fuel Testing', N'T')
INSERT [dbo].[tbl_Accounts] ([AccountId], [AccountNumber], [AccountName], [prtype]) VALUES (174, 7410, N'Supplier-Docking', N'T')
GO

/****** Object:  Table [dbo].[Cir_Category]    Script Date: 06/15/2012 16:44:23 ******/
DELETE FROM [dbo].[Cir_Category]
GO

INSERT [dbo].[Cir_Category] ([CirCatId], [CirCatName]) VALUES (1, N'HSQE')
INSERT [dbo].[Cir_Category] ([CirCatId], [CirCatName]) VALUES (2, N'M&T')
INSERT [dbo].[Cir_Category] ([CirCatId], [CirCatName]) VALUES (3, N'TECHNICAL')
INSERT [dbo].[Cir_Category] ([CirCatId], [CirCatName]) VALUES (4, N'VETTING')
GO

/****** Object:  Table [dbo].[RH_NCType]    Script Date: 06/15/2012 16:44:23 ******/
DELETE FROM [dbo].[RH_NCType]
GO
/****** Object:  Table [dbo].[RH_NCType]    Script Date: 06/15/2012 16:44:23 ******/
INSERT [dbo].[RH_NCType] ([NCTypeId], [NCTypeName]) VALUES (2, N'Minimum Total Rest Comprises More Than 2 Periods.')
INSERT [dbo].[RH_NCType] ([NCTypeId], [NCTypeName]) VALUES (6, N'No Minimum 6 Hrs Consecutive Rest Period.')
INSERT [dbo].[RH_NCType] ([NCTypeId], [NCTypeName]) VALUES (7, N'Minimum 77 Hrs Rest in Each 7 Day Period.')
INSERT [dbo].[RH_NCType] ([NCTypeId], [NCTypeName]) VALUES (24, N'Minimum 10 Hrs Rest in Any 24 Hrs Period.')

