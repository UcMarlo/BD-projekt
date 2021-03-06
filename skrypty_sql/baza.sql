USE [master]
GO
CREATE DATABASE [System Napraw]
ALTER DATABASE [System Napraw] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [System Napraw].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [System Napraw] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [System Napraw] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [System Napraw] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [System Napraw] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [System Napraw] SET ARITHABORT OFF 
GO
ALTER DATABASE [System Napraw] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [System Napraw] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [System Napraw] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [System Napraw] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [System Napraw] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [System Napraw] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [System Napraw] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [System Napraw] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [System Napraw] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [System Napraw] SET  DISABLE_BROKER 
GO
ALTER DATABASE [System Napraw] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [System Napraw] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [System Napraw] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [System Napraw] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [System Napraw] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [System Napraw] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [System Napraw] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [System Napraw] SET RECOVERY FULL 
GO
ALTER DATABASE [System Napraw] SET  MULTI_USER 
GO
ALTER DATABASE [System Napraw] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [System Napraw] SET DB_CHAINING OFF 
GO
ALTER DATABASE [System Napraw] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [System Napraw] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [System Napraw] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'System Napraw', N'ON'
GO
ALTER DATABASE [System Napraw] SET QUERY_STORE = OFF
GO
USE [System Napraw]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [System Napraw]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Activity](
	[id_activity] [uniqueidentifier] NOT NULL,
	[id_request] [uniqueidentifier] NOT NULL,
	[id_personel] [uniqueidentifier] NULL,
	[description] [varchar](250) NULL,
	[result] [varchar](50) NULL,
	[status] [int] NOT NULL,
	[date_reg] [date] NOT NULL,
	[date_fin] [date] NULL,
	[act_type] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED 
(
	[id_activity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Activity Type](
	[act_type] [varchar](10) NOT NULL,
	[act_name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Activity Type] PRIMARY KEY CLUSTERED 
(
	[act_type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Adress](
	[id_client] [uniqueidentifier] NOT NULL,
	[adress] [varchar](50) NOT NULL,
	[city] [varchar](50) NOT NULL,
	[postcode] [varchar](50) NOT NULL,
	[id_adress] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Adress] PRIMARY KEY CLUSTERED 
(
	[id_adress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Client](
	[id_client] [uniqueidentifier] NOT NULL,
	[first_name] [varchar](50) NOT NULL,
	[second_name] [varchar](50) NOT NULL,
	[phone] [varchar](15) NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[id_client] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Object](
	[id_object] [uniqueidentifier] NOT NULL,
	[id_client] [uniqueidentifier] NOT NULL,
	[obj_type] [varchar](10) NOT NULL,
	[name] [varchar](250) NOT NULL,
 CONSTRAINT [PK_Object] PRIMARY KEY CLUSTERED 
(
	[id_object] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Object Type](
	[type_code] [varchar](10) NOT NULL,
	[type_name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Object Type] PRIMARY KEY CLUSTERED 
(
	[type_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Personel](
	[id_person] [uniqueidentifier] NOT NULL,
	[first_name] [varchar](50) NOT NULL,
	[second_name] [varchar](50) NOT NULL,
	[role] [int] NOT NULL,
	[password] [varchar](1000) NOT NULL,
	[email] [varchar](200) NOT NULL,
	[retire_date] [datetime] NULL,
 CONSTRAINT [PK_Personel] PRIMARY KEY CLUSTERED 
(
	[id_person] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Request](
	[id_request] [uniqueidentifier] NOT NULL,
	[id_object] [uniqueidentifier] NOT NULL,
	[id_manager] [uniqueidentifier] NOT NULL,
	[description] [varchar](250) NULL,
	[result] [varchar](50) NULL,
	[status] [int] NOT NULL,
	[date_reg] [date] NOT NULL,
	[date_fin_can] [date] NULL,
 CONSTRAINT [PK_Request] PRIMARY KEY CLUSTERED 
(
	[id_request] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [Activity] ([id_activity], [id_request], [id_personel], [description], [result], [status], [date_reg], [date_fin], [act_type]) VALUES (N'e4af704f-d1a8-47e1-a0bd-2bfadb01f36a', N'501299e8-e72f-4a8f-adfa-cc07fe2639e0', N'28065f33-553c-4d06-802e-9981a6dba136', N'test', N'test', 0, CAST(N'2017-07-05' AS Date), CAST(N'2017-07-05' AS Date), N'nap')
INSERT [Activity] ([id_activity], [id_request], [id_personel], [description], [result], [status], [date_reg], [date_fin], [act_type]) VALUES (N'53e9f4a7-8d33-477d-80ff-628dbcc56158', N'04f047c8-f4f4-4739-9323-411ad4bc0e76', N'28065f33-553c-4d06-802e-9981a6dba136', N'wymien ekran', N'', 0, CAST(N'2017-07-07' AS Date), CAST(N'2017-07-07' AS Date), N'nap')
INSERT [Activity] ([id_activity], [id_request], [id_personel], [description], [result], [status], [date_reg], [date_fin], [act_type]) VALUES (N'f4ce5253-ef2e-470b-bcb5-633fd944e4a2', N'04f047c8-f4f4-4739-9323-411ad4bc0e76', NULL, N'test', N'', 0, CAST(N'2017-07-17' AS Date), CAST(N'2017-07-17' AS Date), N'tes')
INSERT [Activity] ([id_activity], [id_request], [id_personel], [description], [result], [status], [date_reg], [date_fin], [act_type]) VALUES (N'984d826c-9135-493a-b1d6-817a27cfd77b', N'501299e8-e72f-4a8f-adfa-cc07fe2639e0', N'f47cce76-821d-4c79-9069-7be9b2d3d93b', N'test', N'finished', 2, CAST(N'2017-07-06' AS Date), CAST(N'2017-07-06' AS Date), N'nap')
INSERT [Activity] ([id_activity], [id_request], [id_personel], [description], [result], [status], [date_reg], [date_fin], [act_type]) VALUES (N'29eaf468-7dfd-47a2-a49e-be4a9b092f1b', N'04f047c8-f4f4-4739-9323-411ad4bc0e76', N'28065f33-553c-4d06-802e-9981a6dba136', N'wymien baterie', N'', 0, CAST(N'2017-07-07' AS Date), CAST(N'2017-07-07' AS Date), N'nap')
INSERT [Activity Type] ([act_type], [act_name]) VALUES (N'nap', N'napraw')
INSERT [Activity Type] ([act_type], [act_name]) VALUES (N'tes', N'test')
INSERT [Adress] ([id_client], [adress], [city], [postcode], [id_adress]) VALUES (N'00000000-0000-0000-0000-000000000000', N'asdasd', N'adsasd', N'asdasd', N'00000000-0000-0000-0000-000000000000')
INSERT [Adress] ([id_client], [adress], [city], [postcode], [id_adress]) VALUES (N'603515da-0958-4e79-be97-4b576c6113d0', N'rajwa', N'rajwa', N'test', N'7442b50d-86bd-42ea-81d4-142947a79911')
INSERT [Adress] ([id_client], [adress], [city], [postcode], [id_adress]) VALUES (N'2b54f8c1-5a5a-4d59-9898-31c590b853fc', N'rybnik', N'filokow', N'2', N'57ff8d71-a337-450c-9c02-241a361ceeee')
INSERT [Adress] ([id_client], [adress], [city], [postcode], [id_adress]) VALUES (N'5ca451aa-dcf9-4464-96f5-2ed1cab48dfa', N'asfaf', N'asfasfasfasf', N'asfaf', N'8bba8c6a-805c-49f0-ba9e-7da2ba768acb')
INSERT [Adress] ([id_client], [adress], [city], [postcode], [id_adress]) VALUES (N'd32ae216-00bf-46e5-b23e-c81a2f62066c', N'test', N'test', N'test', N'cd609f21-d8de-484b-a1c2-a7ea9d5968ba')
INSERT [Client] ([id_client], [first_name], [second_name], [phone]) VALUES (N'00000000-0000-0000-0000-000000000000', N'adasd', N'asdas', N'asdasd')
INSERT [Client] ([id_client], [first_name], [second_name], [phone]) VALUES (N'5ca451aa-dcf9-4464-96f5-2ed1cab48dfa', N'fasfas', N'asfasf', N'safasf')
INSERT [Client] ([id_client], [first_name], [second_name], [phone]) VALUES (N'2b54f8c1-5a5a-4d59-9898-31c590b853fc', N'adam', N'wnuk', N'tets')
INSERT [Client] ([id_client], [first_name], [second_name], [phone]) VALUES (N'603515da-0958-4e79-be97-4b576c6113d0', N'test', N'test', N'3123')
INSERT [Client] ([id_client], [first_name], [second_name], [phone]) VALUES (N'd32ae216-00bf-46e5-b23e-c81a2f62066c', N'sdad', N'asda', N'dsad')
INSERT [Object] ([id_object], [id_client], [obj_type], [name]) VALUES (N'eb65970e-829c-4c81-a137-6e7b7313e61a', N'603515da-0958-4e79-be97-4b576c6113d0', N'lap', N'aaa')
INSERT [Object] ([id_object], [id_client], [obj_type], [name]) VALUES (N'09402a4a-6b91-4ce1-a22f-7975799629a8', N'603515da-0958-4e79-be97-4b576c6113d0', N'tel', N'iphone 6')
INSERT [Object] ([id_object], [id_client], [obj_type], [name]) VALUES (N'88426a31-8487-4f62-b58e-edf483778957', N'00000000-0000-0000-0000-000000000000', N'lap', N'test')
INSERT [Object Type] ([type_code], [type_name]) VALUES (N'lap', N'laptop')
INSERT [Object Type] ([type_code], [type_name]) VALUES (N't', N'test')
INSERT [Object Type] ([type_code], [type_name]) VALUES (N'tel', N'telefon')
INSERT [Personel] ([id_person], [first_name], [second_name], [role], [password], [email], [retire_date]) VALUES (N'e56231cb-6f7b-4364-b5f0-7975afbd29e0', N'Dominik', N'Rajwa', 2, N'mJO41tGof2Y=', N'test', NULL)
INSERT [Personel] ([id_person], [first_name], [second_name], [role], [password], [email], [retire_date]) VALUES (N'f47cce76-821d-4c79-9069-7be9b2d3d93b', N'Adam', N'Wnuk', 1, N'mJO41tGof2Y=', N'adam', NULL)
INSERT [Personel] ([id_person], [first_name], [second_name], [role], [password], [email], [retire_date]) VALUES (N'28065f33-553c-4d06-802e-9981a6dba136', N'Ola', N'Kowol', 1, N'mJO41tGof2Y=', N'ola3', NULL)
INSERT [Personel] ([id_person], [first_name], [second_name], [role], [password], [email], [retire_date]) VALUES (N'105c2272-30e9-4cd1-84aa-ae497fb42ebf', N'Janusz', N'Skorupa', 1, N'mJO41tGof2Y=', N'janusz', NULL)
INSERT [Personel] ([id_person], [first_name], [second_name], [role], [password], [email], [retire_date]) VALUES (N'f5c765fe-ae20-4dbe-a95a-c5bdeb0bf88b', N'domini', N'sdas', 0, N'mJO41tGof2Y=', N'dominik', NULL)
INSERT [Request] ([id_request], [id_object], [id_manager], [description], [result], [status], [date_reg], [date_fin_can]) VALUES (N'1623fbc2-5845-4544-83b5-3620ad3a21c2', N'eb65970e-829c-4c81-a137-6e7b7313e61a', N'f5c765fe-ae20-4dbe-a95a-c5bdeb0bf88b', N'test

', N'tset', 0, CAST(N'2017-07-06' AS Date), CAST(N'2017-07-07' AS Date))
INSERT [Request] ([id_request], [id_object], [id_manager], [description], [result], [status], [date_reg], [date_fin_can]) VALUES (N'04f047c8-f4f4-4739-9323-411ad4bc0e76', N'09402a4a-6b91-4ce1-a22f-7975799629a8', N'f5c765fe-ae20-4dbe-a95a-c5bdeb0bf88b', N'test
', N'', 0, CAST(N'2017-07-08' AS Date), NULL)
INSERT [Request] ([id_request], [id_object], [id_manager], [description], [result], [status], [date_reg], [date_fin_can]) VALUES (N'5e841661-ea2f-4b8a-9668-481ac0c87121', N'eb65970e-829c-4c81-a137-6e7b7313e61a', N'f5c765fe-ae20-4dbe-a95a-c5bdeb0bf88b', N'youtube
', N'youtube', 0, CAST(N'2017-07-28' AS Date), CAST(N'2017-07-30' AS Date))
INSERT [Request] ([id_request], [id_object], [id_manager], [description], [result], [status], [date_reg], [date_fin_can]) VALUES (N'357f1515-fc86-480c-a9a7-be5bb416db18', N'88426a31-8487-4f62-b58e-edf483778957', N'f5c765fe-ae20-4dbe-a95a-c5bdeb0bf88b', N'adam
', N'', 0, CAST(N'2017-07-07' AS Date), NULL)
INSERT [Request] ([id_request], [id_object], [id_manager], [description], [result], [status], [date_reg], [date_fin_can]) VALUES (N'501299e8-e72f-4a8f-adfa-cc07fe2639e0', N'eb65970e-829c-4c81-a137-6e7b7313e61a', N'f5c765fe-ae20-4dbe-a95a-c5bdeb0bf88b', N'test
', N'test', 0, CAST(N'2017-07-04' AS Date), CAST(N'2017-07-05' AS Date))
ALTER TABLE [Activity]  WITH CHECK ADD  CONSTRAINT [FK_Activity_Activity Type] FOREIGN KEY([act_type])
REFERENCES [Activity Type] ([act_type])
GO
ALTER TABLE [Activity] CHECK CONSTRAINT [FK_Activity_Activity Type]
GO
ALTER TABLE [Activity]  WITH CHECK ADD  CONSTRAINT [FK_Activity_Personel] FOREIGN KEY([id_personel])
REFERENCES [Personel] ([id_person])
GO
ALTER TABLE [Activity] CHECK CONSTRAINT [FK_Activity_Personel]
GO
ALTER TABLE [Activity]  WITH CHECK ADD  CONSTRAINT [FK_Activity_Request] FOREIGN KEY([id_request])
REFERENCES [Request] ([id_request])
GO
ALTER TABLE [Activity] CHECK CONSTRAINT [FK_Activity_Request]
GO
ALTER TABLE [Adress]  WITH CHECK ADD  CONSTRAINT [FK_Adress_Client] FOREIGN KEY([id_client])
REFERENCES [Client] ([id_client])
GO
ALTER TABLE [Adress] CHECK CONSTRAINT [FK_Adress_Client]
GO
ALTER TABLE [Object]  WITH CHECK ADD  CONSTRAINT [FK_Object_Client] FOREIGN KEY([id_client])
REFERENCES [Client] ([id_client])
GO
ALTER TABLE [Object] CHECK CONSTRAINT [FK_Object_Client]
GO
ALTER TABLE [Object]  WITH CHECK ADD  CONSTRAINT [FK_Object_Object Type] FOREIGN KEY([obj_type])
REFERENCES [Object Type] ([type_code])
GO
ALTER TABLE [Object] CHECK CONSTRAINT [FK_Object_Object Type]
GO
ALTER TABLE [Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Object] FOREIGN KEY([id_object])
REFERENCES [Object] ([id_object])
GO
ALTER TABLE [Request] CHECK CONSTRAINT [FK_Request_Object]
GO
ALTER TABLE [Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Personel] FOREIGN KEY([id_manager])
REFERENCES [Personel] ([id_person])
GO
ALTER TABLE [Request] CHECK CONSTRAINT [FK_Request_Personel]
GO
USE [master]
GO
ALTER DATABASE [System Napraw] SET  READ_WRITE 
GO
