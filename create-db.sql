USE [master]
GO
/****** Object:  Database [TaskPlaner]    Script Date: 12/22/2022 1:52:45 PM ******/
CREATE DATABASE [TaskPlaner]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TaskPlaner', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.DATL\MSSQL\DATA\TaskPlaner.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TaskPlaner_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.DATL\MSSQL\DATA\TaskPlaner_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TaskPlaner] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TaskPlaner].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TaskPlaner] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TaskPlaner] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TaskPlaner] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TaskPlaner] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TaskPlaner] SET ARITHABORT OFF 
GO
ALTER DATABASE [TaskPlaner] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TaskPlaner] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TaskPlaner] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TaskPlaner] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TaskPlaner] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TaskPlaner] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TaskPlaner] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TaskPlaner] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TaskPlaner] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TaskPlaner] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TaskPlaner] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TaskPlaner] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TaskPlaner] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TaskPlaner] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TaskPlaner] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TaskPlaner] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TaskPlaner] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TaskPlaner] SET RECOVERY FULL 
GO
ALTER DATABASE [TaskPlaner] SET  MULTI_USER 
GO
ALTER DATABASE [TaskPlaner] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TaskPlaner] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TaskPlaner] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TaskPlaner] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [TaskPlaner] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TaskPlaner] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TaskPlaner', N'ON'
GO
ALTER DATABASE [TaskPlaner] SET QUERY_STORE = OFF
GO
USE [TaskPlaner]
GO
/****** Object:  Table [dbo].[Card]    Script Date: 12/22/2022 1:52:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Card](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](25) NULL,
	[UpdatedBy] [nvarchar](25) NULL,
	[DueDate] [datetime] NULL,
	[ProjectId] [int] NULL,
	[PriorityId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CardAssign]    Script Date: 12/22/2022 1:52:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CardAssign](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[CardId] [int] NULL,
	[IsAssigned] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CardHistory]    Script Date: 12/22/2022 1:52:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CardHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](200) NULL,
	[CardId] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](25) NULL,
	[ActionType] [nvarchar](25) NULL,
	[UpdatedBy] [nvarchar](25) NULL,
	[UserId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CardMovement]    Script Date: 12/22/2022 1:52:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CardMovement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CardId] [int] NULL,
	[PhaseId] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](25) NULL,
	[UpdatedBy] [nvarchar](25) NULL,
	[IsCurrent] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CardTag]    Script Date: 12/22/2022 1:52:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CardTag](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CardId] [int] NULL,
	[TagId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phase]    Script Date: 12/22/2022 1:52:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phase](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[AcceptMoveId] [int] NULL,
	[Code] [varchar](5) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Priority]    Script Date: 12/22/2022 1:52:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Priority](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[Color] [varchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 12/22/2022 1:52:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](25) NULL,
	[UpdatedBy] [nvarchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectMember]    Script Date: 12/22/2022 1:52:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectMember](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[ProjectId] [int] NULL,
	[IsActive] [bit] NULL,
	[IsOwner] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectPhase]    Script Date: 12/22/2022 1:52:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectPhase](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[PhaseId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tag]    Script Date: 12/22/2022 1:52:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tag](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[Color] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Todo]    Script Date: 12/22/2022 1:52:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Todo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[IsCheck] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](25) NULL,
	[UpdatedBy] [nvarchar](25) NULL,
	[ParentId] [int] NULL,
	[CardId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/22/2022 1:52:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](20) NULL,
	[Password] [varchar](20) NULL,
	[Image] [text] NULL,
	[FirstName] [nvarchar](25) NULL,
	[LastName] [nvarchar](25) NULL,
	[IsActive] [bit] NULL,
	[Phone] [varchar](15) NULL,
	[Birthday] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Card] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Card] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[CardAssign] ADD  DEFAULT ('True') FOR [IsAssigned]
GO
ALTER TABLE [dbo].[CardHistory] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[CardHistory] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[CardMovement] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[CardMovement] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[CardMovement] ADD  DEFAULT ('True') FOR [IsCurrent]
GO
ALTER TABLE [dbo].[Project] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Project] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[ProjectMember] ADD  DEFAULT ('True') FOR [IsActive]
GO
ALTER TABLE [dbo].[ProjectMember] ADD  DEFAULT ('False') FOR [IsOwner]
GO
ALTER TABLE [dbo].[Todo] ADD  DEFAULT ('False') FOR [IsCheck]
GO
ALTER TABLE [dbo].[Todo] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Todo] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ('True') FOR [IsActive]
GO
ALTER TABLE [dbo].[Card]  WITH CHECK ADD  CONSTRAINT [fk_Card_Priority] FOREIGN KEY([PriorityId])
REFERENCES [dbo].[Priority] ([Id])
GO
ALTER TABLE [dbo].[Card] CHECK CONSTRAINT [fk_Card_Priority]
GO
ALTER TABLE [dbo].[Card]  WITH CHECK ADD  CONSTRAINT [fk_Card_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[Card] CHECK CONSTRAINT [fk_Card_Project]
GO
ALTER TABLE [dbo].[CardAssign]  WITH CHECK ADD  CONSTRAINT [fk_assign_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[CardAssign] CHECK CONSTRAINT [fk_assign_User]
GO
ALTER TABLE [dbo].[CardAssign]  WITH CHECK ADD  CONSTRAINT [fk_CardAssign] FOREIGN KEY([CardId])
REFERENCES [dbo].[Card] ([Id])
GO
ALTER TABLE [dbo].[CardAssign] CHECK CONSTRAINT [fk_CardAssign]
GO
ALTER TABLE [dbo].[CardHistory]  WITH CHECK ADD  CONSTRAINT [fk_CardHistory_Card] FOREIGN KEY([CardId])
REFERENCES [dbo].[Card] ([Id])
GO
ALTER TABLE [dbo].[CardHistory] CHECK CONSTRAINT [fk_CardHistory_Card]
GO
ALTER TABLE [dbo].[CardHistory]  WITH CHECK ADD  CONSTRAINT [fk_CardHistory_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[CardHistory] CHECK CONSTRAINT [fk_CardHistory_User]
GO
ALTER TABLE [dbo].[CardMovement]  WITH CHECK ADD  CONSTRAINT [fk_CardMovement_Card] FOREIGN KEY([CardId])
REFERENCES [dbo].[Card] ([Id])
GO
ALTER TABLE [dbo].[CardMovement] CHECK CONSTRAINT [fk_CardMovement_Card]
GO
ALTER TABLE [dbo].[CardMovement]  WITH CHECK ADD  CONSTRAINT [fk_CardMovement_Phase] FOREIGN KEY([PhaseId])
REFERENCES [dbo].[Phase] ([Id])
GO
ALTER TABLE [dbo].[CardMovement] CHECK CONSTRAINT [fk_CardMovement_Phase]
GO
ALTER TABLE [dbo].[CardTag]  WITH CHECK ADD  CONSTRAINT [fk_CardTag_Card] FOREIGN KEY([CardId])
REFERENCES [dbo].[Card] ([Id])
GO
ALTER TABLE [dbo].[CardTag] CHECK CONSTRAINT [fk_CardTag_Card]
GO
ALTER TABLE [dbo].[CardTag]  WITH CHECK ADD  CONSTRAINT [fk_CardTag_Tag] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tag] ([Id])
GO
ALTER TABLE [dbo].[CardTag] CHECK CONSTRAINT [fk_CardTag_Tag]
GO
ALTER TABLE [dbo].[ProjectMember]  WITH CHECK ADD  CONSTRAINT [fk_member_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[ProjectMember] CHECK CONSTRAINT [fk_member_User]
GO
ALTER TABLE [dbo].[ProjectMember]  WITH CHECK ADD  CONSTRAINT [fk_ProjectMember] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[ProjectMember] CHECK CONSTRAINT [fk_ProjectMember]
GO
ALTER TABLE [dbo].[ProjectPhase]  WITH CHECK ADD  CONSTRAINT [fk_ProjectPhase_Phase] FOREIGN KEY([PhaseId])
REFERENCES [dbo].[Phase] ([Id])
GO
ALTER TABLE [dbo].[ProjectPhase] CHECK CONSTRAINT [fk_ProjectPhase_Phase]
GO
ALTER TABLE [dbo].[ProjectPhase]  WITH CHECK ADD  CONSTRAINT [fk_ProjectPhase_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[ProjectPhase] CHECK CONSTRAINT [fk_ProjectPhase_Project]
GO
ALTER TABLE [dbo].[Todo]  WITH CHECK ADD  CONSTRAINT [fk_Todo_Card] FOREIGN KEY([CardId])
REFERENCES [dbo].[Card] ([Id])
GO
ALTER TABLE [dbo].[Todo] CHECK CONSTRAINT [fk_Todo_Card]
GO
ALTER TABLE [dbo].[Todo]  WITH CHECK ADD  CONSTRAINT [fk_Todo_Todo] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Todo] ([Id])
GO
ALTER TABLE [dbo].[Todo] CHECK CONSTRAINT [fk_Todo_Todo]
GO
USE [master]
GO
ALTER DATABASE [TaskPlaner] SET  READ_WRITE 
GO
