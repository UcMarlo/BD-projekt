/*
   czwartek, 23 marca 201717:09:29
   User: 
   Server: DESKTOP-2UKDSIA
   Database: System Napraw
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.[Activity Type]
	(
	act_type varchar(10) NOT NULL,
	act_name varchar(245) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.[Activity Type] ADD CONSTRAINT
	[PK_Activity Type] PRIMARY KEY CLUSTERED 
	(
	act_type
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.[Activity Type] SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Personel
	(
	id_person uniqueidentifier NOT NULL,
	first_name varchar(50) NOT NULL,
	second_name varchar(50) NOT NULL,
	role varchar(50) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Personel ADD CONSTRAINT
	PK_Personel PRIMARY KEY CLUSTERED 
	(
	id_person
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Personel SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.[Object Type]
	(
	type_code varchar(10) NOT NULL,
	type_name varchar(50) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.[Object Type] ADD CONSTRAINT
	[PK_Object Type] PRIMARY KEY CLUSTERED 
	(
	type_code
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.[Object Type] SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Client
	(
	id_client uniqueidentifier NOT NULL,
	first_name varchar(50) NOT NULL,
	second_name varchar(50) NOT NULL,
	phone varchar(15) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Client ADD CONSTRAINT
	PK_Client PRIMARY KEY CLUSTERED 
	(
	id_client
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Client SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Object
	(
	id_object uniqueidentifier NOT NULL,
	id_client uniqueidentifier NOT NULL,
	obj_type varchar(10) NOT NULL,
	name varchar(250) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Object ADD CONSTRAINT
	PK_Object PRIMARY KEY CLUSTERED 
	(
	id_object
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Object ADD CONSTRAINT
	FK_Object_Client FOREIGN KEY
	(
	id_client
	) REFERENCES dbo.Client
	(
	id_client
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Object ADD CONSTRAINT
	[FK_Object_Object Type] FOREIGN KEY
	(
	obj_type
	) REFERENCES dbo.[Object Type]
	(
	type_code
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Object SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Request
	(
	id_request uniqueidentifier NOT NULL,
	id_object uniqueidentifier NOT NULL,
	id_manager uniqueidentifier NOT NULL,
	description varchar(250) NOT NULL,
	result varchar(50) NULL,
	status varchar(50) NOT NULL ,
	date_reg datetime NOT NULL,
	date_fin_can datetime NULL,
	CONSTRAINT chk_status CHECK ([status] IN (('Finished' ,'Cancled','Progress', 'Open')))
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Request ADD CONSTRAINT
	PK_Request PRIMARY KEY CLUSTERED 
	(
	id_request
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Request ADD CONSTRAINT
	FK_Request_Object FOREIGN KEY
	(
	id_object
	) REFERENCES dbo.Object
	(
	id_object
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Request ADD CONSTRAINT
	FK_Request_Personel FOREIGN KEY
	(
	id_manager
	) REFERENCES dbo.Personel
	(
	id_person
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Request SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Activity
	(
	id_activity uniqueidentifier NOT NULL,
	id_request uniqueidentifier NOT NULL,
	id_worker uniqueidentifier NULL,
	seq int, 
	description varchar(254) NULL,
	result varchar(50) NULL,
	status varchar(50) NOT NULL,
	date_reg datetime NOT NULL,
	date_fin datetime NULL,
	act_type varchar(10) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Activity ADD CONSTRAINT
	PK_Activity PRIMARY KEY CLUSTERED 
	(
	id_activity
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Activity ADD CONSTRAINT
	[FK_Activity_Activity Type] FOREIGN KEY
	(
	act_type
	) REFERENCES dbo.[Activity Type]
	(
	act_type
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Activity ADD CONSTRAINT
	FK_Activity_Request FOREIGN KEY
	(
	id_request
	) REFERENCES dbo.Request
	(
	id_request
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Activity ADD CONSTRAINT
	FK_Activity_Personel FOREIGN KEY
	(
	id_worker
	) REFERENCES dbo.Personel
	(
	id_person
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Activity SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Adress
	(
	id_client uniqueidentifier NOT NULL,
	city varchar(254) NOT NULL,
	street varchar(254)  NULL,
	postcode varchar(50) NOT NULL,
	house varchar(10) NOT NULL,
	flat varchar(10)  NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Adress ADD CONSTRAINT
	FK_Adress_Client FOREIGN KEY
	(
	id_client
	) REFERENCES dbo.Client
	(
	id_client
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Adress SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
