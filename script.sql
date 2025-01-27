USE [smart_factory]
GO
/****** Object:  Table [dbo].[result]    Script Date: 2024-07-10 오후 3:53:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[result]') AND type in (N'U'))
DROP TABLE [dbo].[result]
GO
/****** Object:  Table [dbo].[employee]    Script Date: 2024-07-10 오후 3:53:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[employee]') AND type in (N'U'))
DROP TABLE [dbo].[employee]
GO
USE [master]
GO
/****** Object:  Database [smart_factory]    Script Date: 2024-07-10 오후 3:53:20 ******/
DROP DATABASE [smart_factory]
GO
/****** Object:  Database [smart_factory]    Script Date: 2024-07-10 오후 3:53:20 ******/
CREATE DATABASE [smart_factory]
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [smart_factory].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [smart_factory] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [smart_factory] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [smart_factory] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [smart_factory] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [smart_factory] SET ARITHABORT OFF 
GO
ALTER DATABASE [smart_factory] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [smart_factory] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [smart_factory] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [smart_factory] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [smart_factory] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [smart_factory] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [smart_factory] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [smart_factory] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [smart_factory] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [smart_factory] SET  DISABLE_BROKER 
GO
ALTER DATABASE [smart_factory] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [smart_factory] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [smart_factory] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [smart_factory] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [smart_factory] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [smart_factory] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [smart_factory] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [smart_factory] SET RECOVERY FULL 
GO
ALTER DATABASE [smart_factory] SET  MULTI_USER 
GO
ALTER DATABASE [smart_factory] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [smart_factory] SET DB_CHAINING OFF 
GO
ALTER DATABASE [smart_factory] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [smart_factory] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [smart_factory] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [smart_factory] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'smart_factory', N'ON'
GO
ALTER DATABASE [smart_factory] SET QUERY_STORE = ON
GO
ALTER DATABASE [smart_factory] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [smart_factory]
GO
/****** Object:  Table [dbo].[employee]    Script Date: 2024-07-10 오후 3:53:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employee](
	[employeeId] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](20) NOT NULL,
	[gender] [varchar](10) NOT NULL,
	[email] [varchar](50) NULL,
	[phone] [int] NOT NULL,
	[address] [varchar](50) NULL,
	[position] [varchar](20) NOT NULL,
	[profile] [image] NULL,
	[LoginIdx] [int] NOT NULL,
	[LoginId] [varchar](30) NOT NULL,
	[LoginPw] [varchar](30) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[result]    Script Date: 2024-07-10 오후 3:53:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[result](
	[plastic] [int] NOT NULL,
	[paper] [int] NOT NULL,
	[can] [int] NOT NULL
) ON [PRIMARY]
GO
INSERT [dbo].[result] ([plastic], [paper], [can]) VALUES (1, 2, 1)
GO
USE [master]
GO
ALTER DATABASE [smart_factory] SET  READ_WRITE 
GO
