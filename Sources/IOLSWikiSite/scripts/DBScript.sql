USE [master]
GO

/****** Object:  Database [IOLSWiki]    Script Date: 02/27/2015 16:27:29 ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'IOLSWiki')
DROP DATABASE [IOLSWiki]
GO

USE [master]
GO

/****** Object:  Database [IOLSWiki]    Script Date: 02/27/2015 16:27:29 ******/
CREATE DATABASE [IOLSWiki] ON  PRIMARY 
( NAME = N'IOLSWiki', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\IOLSWiki.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'IOLSWiki_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\IOLSWiki_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [IOLSWiki] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IOLSWiki].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [IOLSWiki] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [IOLSWiki] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [IOLSWiki] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [IOLSWiki] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [IOLSWiki] SET ARITHABORT OFF 
GO

ALTER DATABASE [IOLSWiki] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [IOLSWiki] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [IOLSWiki] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [IOLSWiki] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [IOLSWiki] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [IOLSWiki] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [IOLSWiki] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [IOLSWiki] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [IOLSWiki] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [IOLSWiki] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [IOLSWiki] SET  DISABLE_BROKER 
GO

ALTER DATABASE [IOLSWiki] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [IOLSWiki] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [IOLSWiki] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [IOLSWiki] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [IOLSWiki] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [IOLSWiki] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [IOLSWiki] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [IOLSWiki] SET  READ_WRITE 
GO

ALTER DATABASE [IOLSWiki] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [IOLSWiki] SET  MULTI_USER 
GO

ALTER DATABASE [IOLSWiki] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [IOLSWiki] SET DB_CHAINING OFF 
GO

USE [IOLSWiki]
GO

/****** Table ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WikiRecords_CreatationTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WikiRecords] DROP CONSTRAINT [DF_WikiRecords_CreatationTime]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WikiRecords_LastModificationTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WikiRecords] DROP CONSTRAINT [DF_WikiRecords_LastModificationTime]
END
GO
/****** Object:  Table [dbo].[WikiRecords]    Script Date: 02/27/2015 16:29:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WikiRecords]') AND type in (N'U'))
DROP TABLE [dbo].[WikiRecords]
GO
/****** Object:  Table [dbo].[WikiRecords]    Script Date: 02/27/2015 16:29:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WikiRecords](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Keyword1] [nvarchar](100) NOT NULL,
	[Keyword2] [nvarchar](100) NOT NULL,
	[Keyword3] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](2000) NOT NULL,
	[CreatationTime] [datetime] NOT NULL,
	[LastModificationTime] [datetime] NOT NULL,
	[Author] [nvarchar](50) NOT NULL,
	[LastEditor] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_WikiRecords] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[WikiRecords] ADD  CONSTRAINT [DF_WikiRecords_CreatationTime]  DEFAULT (getdate()) FOR [CreatationTime]
GO
ALTER TABLE [dbo].[WikiRecords] ADD  CONSTRAINT [DF_WikiRecords_LastModificationTime]  DEFAULT (getdate()) FOR [LastModificationTime]
GO

/****** Stored Procedure ******/

/****** Object:  StoredProcedure [dbo].[CREATE_WIKIRECORD]    Script Date: 02/27/2015 16:31:09 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CREATE_WIKIRECORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CREATE_WIKIRECORD]
GO
/****** Object:  StoredProcedure [dbo].[CREATE_WIKIRECORD]    Script Date: 02/27/2015 16:31:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CREATE_WIKIRECORD]
	-- Add the parameters for the stored procedure here
	@NAME			NVARCHAR(100),
	@KEYWORD1		NVARCHAR(100),
	@KEYWORD2		NVARCHAR(100),
	@KEYWORD3		NVARCHAR(100),
	@DESCRIPTION	NVARCHAR(2000),
	@AUTHOR			NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO WikiRecords 
							(
								Name,
								Keyword1,
								Keyword2,
								Keyword3,
								[Description],
								Author,
								LastEditor
							)
							VALUES
							(
								@NAME,
								@KEYWORD1,
								@KEYWORD2,
								@KEYWORD3,
								@DESCRIPTION,
								@AUTHOR,
								@AUTHOR
							)
END
GO

/****** Object:  StoredProcedure [dbo].[DELETE_WIKIRECORD]    Script Date: 02/27/2015 16:31:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DELETE_WIKIRECORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DELETE_WIKIRECORD]
GO
/****** Object:  StoredProcedure [dbo].[DELETE_WIKIRECORD]    Script Date: 02/27/2015 16:31:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DELETE_WIKIRECORD]
	-- Add the parameters for the stored procedure here
	@ID	INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM WikiRecords WHERE id = @ID
END
GO

/****** Object:  StoredProcedure [dbo].[GET_WIKIRECORDS]    Script Date: 02/27/2015 16:32:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_WIKIRECORDS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GET_WIKIRECORDS]
GO
/****** Object:  StoredProcedure [dbo].[GET_WIKIRECORDS]    Script Date: 02/27/2015 16:32:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GET_WIKIRECORDS]
	-- Add the parameters for the stored procedure here
	@KEYWORD	NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    DECLARE @FUZZYRESULT NVARCHAR(200)
    SET @FUZZYRESULT = '%' + @KEYWORD + '%'
	SELECT * FROM WikiRecords WHERE	Name			LIKE @FUZZYRESULT
								 OR Keyword1		LIKE @FUZZYRESULT 
								 OR Keyword2		LIKE @FUZZYRESULT 
								 OR Keyword3		LIKE @FUZZYRESULT 
								 OR [Description]	LIKE @FUZZYRESULT
END
GO

/****** Object:  StoredProcedure [dbo].[IS_NAME_EXISTED]    Script Date: 02/27/2015 16:33:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IS_NAME_EXISTED]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[IS_NAME_EXISTED]
GO
/****** Object:  StoredProcedure [dbo].[IS_NAME_EXISTED]    Script Date: 02/27/2015 16:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[IS_NAME_EXISTED]
	-- Add the parameters for the stored procedure here
	@NAME		NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    IF EXISTS(SELECT id FROM WikiRecords WHERE Name = @NAME)
    BEGIN
		RETURN 1;
    END
    ELSE
    BEGIN
		RETURN 0;
    END
END
GO

/****** Object:  StoredProcedure [dbo].[UPDATE_WIKIRECORD]    Script Date: 02/27/2015 16:33:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPDATE_WIKIRECORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UPDATE_WIKIRECORD]
GO
/****** Object:  StoredProcedure [dbo].[UPDATE_WIKIRECORD]    Script Date: 02/27/2015 16:33:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UPDATE_WIKIRECORD]
	-- Add the parameters for the stored procedure here
	@ID				INT,
	@KEYWORD1		NVARCHAR(100),
	@KEYWORD2		NVARCHAR(100),
	@KEYWORD3		NVARCHAR(100),
	@DESCRIPTION	NVARCHAR(2000),
	@LASTEDITOR		NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE WikiRecords	SET		Keyword1				= @KEYWORD1,
								Keyword2				= @KEYWORD2,
								Keyword3				= @KEYWORD3,
								[Description]			= @DESCRIPTION,
								LastEditor				= @LASTEDITOR,
								LastModificationTime	= GETDATE()
						WHERE	id = @ID
							
END
GO

/****** Object:  StoredProcedure [dbo].[GET_WIKIRECORD_BYID]    Script Date: 03/05/2015 17:33:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_WIKIRECORD_BYID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GET_WIKIRECORD_BYID]
GO
/****** Object:  StoredProcedure [dbo].[GET_WIKIRECORD_BYID]    Script Date: 03/05/2015 17:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GET_WIKIRECORD_BYID]
	-- Add the parameters for the stored procedure here
	@ID	INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM WikiRecords WHERE ID = @ID
END
GO