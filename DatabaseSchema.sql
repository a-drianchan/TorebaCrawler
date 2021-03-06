USE [master]
GO
/****** Object:  Database [TorebaMachine]    Script Date: 2020-07-29 2:58:43 PM ******/
CREATE DATABASE [TorebaMachine]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TorebaMachine', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TorebaMachine.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TorebaMachine_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TorebaMachine_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TorebaMachine] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TorebaMachine].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TorebaMachine] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TorebaMachine] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TorebaMachine] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TorebaMachine] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TorebaMachine] SET ARITHABORT OFF 
GO
ALTER DATABASE [TorebaMachine] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TorebaMachine] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TorebaMachine] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TorebaMachine] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TorebaMachine] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TorebaMachine] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TorebaMachine] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TorebaMachine] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TorebaMachine] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TorebaMachine] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TorebaMachine] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TorebaMachine] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TorebaMachine] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TorebaMachine] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TorebaMachine] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TorebaMachine] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TorebaMachine] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TorebaMachine] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TorebaMachine] SET  MULTI_USER 
GO
ALTER DATABASE [TorebaMachine] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TorebaMachine] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TorebaMachine] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TorebaMachine] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TorebaMachine] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TorebaMachine] SET QUERY_STORE = OFF
GO
USE [TorebaMachine]
GO
/****** Object:  Table [dbo].[Machine]    Script Date: 2020-07-29 2:58:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Machine](
	[MachineID] [int] NOT NULL,
	[PrizeID] [int] NOT NULL,
	[MachineName] [nvarchar](150) NOT NULL,
	[Cost] [int] NOT NULL,
	[MachineType] [int] NOT NULL,
	[CostType] [int] NOT NULL,
	[StatusType] [int] NOT NULL,
	[SpecialTagType] [int] NOT NULL,
 CONSTRAINT [PK_Machine] PRIMARY KEY CLUSTERED 
(
	[MachineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Replay]    Script Date: 2020-07-29 2:58:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Replay](
	[ReplayID] [int] NOT NULL,
	[MachineID] [int] NOT NULL,
	[ReplayLink] [nvarchar](150) NULL,
	[PrizeName] [nvarchar](150) NOT NULL,
	[Time] [datetime] NOT NULL,
 CONSTRAINT [ReplayID] PRIMARY KEY CLUSTERED 
(
	[ReplayID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Machine_GetByName]    Script Date: 2020-07-29 2:58:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Machine_GetByName]
	@Name nvarchar(150)
as
begin
	SET NOCOUNT ON;
	select *
	from dbo.Machine
	where Name = @Name
end
GO
/****** Object:  StoredProcedure [dbo].[Machine_InsertUpdate]    Script Date: 2020-07-29 2:58:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Machine_InsertUpdate]
(@MachineID int, 
@PrizeID int, 
@MachineName nvarchar(150), 
@Cost int, 
@MachineType int, 
@CostType int,
@StatusType int,
@SpecialTagType int)
as
begin
		if exists (select * 
					from dbo.Machine
					where MachineID = @MachineID)
			update dbo.Machine
			set PrizeID = @PrizeID,
			MachineName = @MachineName,
			Cost = @Cost,
			MachineType = @MachineType,
			CostType = @CostType,
			StatusType = @StatusType,
			SpecialTagType = @SpecialTagType
			where MachineID = @MachineID;
		else
			insert into dbo.Machine(MachineID, PrizeID, MachineName, Cost, MachineType, CostType, StatusType, SpecialTagType)
			values(@MachineID, @PrizeID, @MachineName, @Cost, @MachineType, @CostType, @StatusType, @SpecialTagType);

end
GO
/****** Object:  StoredProcedure [dbo].[Replay_GetByMachineID]    Script Date: 2020-07-29 2:58:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Replay_GetByMachineID]
	@MachineID int
as
begin
	SET NOCOUNT ON;
	select *
	from dbo.Replay
	where MachineID = @MachineID;
end
GO
/****** Object:  StoredProcedure [dbo].[Replay_GetByReplayID]    Script Date: 2020-07-29 2:58:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Replay_GetByReplayID]
	@ReplayID int
as
begin
	SET NOCOUNT ON;
	select *
	from dbo.Replay
	where ReplayID = @ReplayID;
end
GO
/****** Object:  StoredProcedure [dbo].[Replay_Insert]    Script Date: 2020-07-29 2:58:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Replay_Insert]
(@ReplayID int, 
@MachineID int, 
@ReplayLink nvarchar(150), 
@PrizeName nvarchar(150), 
@Time Datetime 
)
as
begin
		if not exists (select * 
					from dbo.Replay
					where ReplayID = @ReplayID)
			
			insert into dbo.Replay(ReplayID, MachineID, ReplayLink, PrizeName, Time)
			values(@ReplayID, @MachineID, @ReplayLink, @PrizeName, @Time);

end
GO
USE [master]
GO
ALTER DATABASE [TorebaMachine] SET  READ_WRITE 
GO
