USE [master]
GO

/****** Object:  Database [DemoAppDb3]    Script Date: 3/22/2015 7:46:53 PM ******/
CREATE DATABASE [DemoAppDb3] 
--ON  PRIMARY 
--( NAME = N'DemoAppDb3', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.EXPRESS2008\MSSQL\DATA\DemoAppDb3.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
-- LOG ON 
--( NAME = N'DemoAppDb3_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.EXPRESS2008\MSSQL\DATA\DemoAppDb3_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [DemoAppDb3] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DemoAppDb3].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [DemoAppDb3] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [DemoAppDb3] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [DemoAppDb3] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [DemoAppDb3] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [DemoAppDb3] SET ARITHABORT OFF 
GO

ALTER DATABASE [DemoAppDb3] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [DemoAppDb3] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [DemoAppDb3] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [DemoAppDb3] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [DemoAppDb3] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [DemoAppDb3] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [DemoAppDb3] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [DemoAppDb3] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [DemoAppDb3] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [DemoAppDb3] SET  DISABLE_BROKER 
GO

ALTER DATABASE [DemoAppDb3] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [DemoAppDb3] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [DemoAppDb3] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [DemoAppDb3] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [DemoAppDb3] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [DemoAppDb3] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [DemoAppDb3] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [DemoAppDb3] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [DemoAppDb3] SET  MULTI_USER 
GO

ALTER DATABASE [DemoAppDb3] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [DemoAppDb3] SET DB_CHAINING OFF 
GO

ALTER DATABASE [DemoAppDb3] SET  READ_WRITE 
GO
USE [DemoAppDb3]
GO

/****** Object:  Table [dbo].[Comments]    Script Date: 3/22/2015 1:32:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Comments](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()),
	[Text] [varchar](255) NULL,
	[PictureId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Pictures]    Script Date: 3/22/2015 1:32:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pictures](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()),
	[Data] [varbinary](max) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Pictures] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/22/2015 1:32:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()),
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [IX_Users]    Script Date: 23.3.2015 ã. 10:50:42 ÷. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users] ON [dbo].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Comments] FOREIGN KEY([Id])
REFERENCES [dbo].[Comments] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Comments]
GO
ALTER TABLE [dbo].[Pictures]  WITH CHECK ADD  CONSTRAINT [FK_Pictures_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Pictures] CHECK CONSTRAINT [FK_Pictures_Users]
GO
/****** Object:  StoredProcedure [dbo].[PicturesDelete]    Script Date: 3/22/2015 1:32:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PicturesDelete]
(
    @Id uniqueidentifier
)
AS
    SET NOCOUNT OFF;
DELETE FROM dbo.Pictures WHERE Id = @Id


GO
/****** Object:  StoredProcedure [dbo].[PicturesGetAll]    Script Date: 3/22/2015 1:32:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PicturesGetAll] 
AS
BEGIN
	SET NOCOUNT ON;

    SELECT Id, [Data], UserId FROM dbo.Pictures
END


GO
/****** Object:  StoredProcedure [dbo].[PicturesGetById]    Script Date: 3/22/2015 1:32:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PicturesGetById]
(
	@Id uniqueidentifier
) 
AS
BEGIN
	SET NOCOUNT ON;

    SELECT p.Id, p.[Data], p.UserId, CASE WHEN c.[Text] IS NULL THEN '' ELSE c.[Text] END as Comment 
	FROM dbo.Pictures p
	LEFT JOIN [dbo].[Comments] c on p.Id = c.[PictureId]
	WHERE p.Id = @Id
END


GO
/****** Object:  StoredProcedure [dbo].[PicturesInsert]    Script Date: 3/22/2015 1:32:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PicturesInsert] 
(
	@Data varbinary(MAX), 
	@UserId uniqueidentifier,
	@Comment varchar(255) = NULL
)
AS
BEGIN
	DECLARE @Id uniqueidentifier
	SET @Id = NEWID() 

	INSERT INTO Pictures
		(Id, [Data], UserId)
	VALUES
		(@Id, @Data, @UserId)

	IF EXISTS (SELECT 1 FROM Comments WHERE [PictureId] = @Id)
	BEGIN
		UPDATE [dbo].[Comments] 
		SET [Text] = @Comment
			WHERE [PictureId] = @Id;
	END
	ELSE
	BEGIN
		INSERT INTO Comments
		( [Text], [PictureId])
	VALUES
		( @Comment, @Id)
	END
END

GO
/****** Object:  StoredProcedure [dbo].[PicturesUpdate]    Script Date: 3/22/2015 1:32:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PicturesUpdate]
(
    @Id Uniqueidentifier,
    @Data varbinary(MAX) = NULL,
    @Comment varchar(MAX) = NULL
)
AS
    SET NOCOUNT OFF;

IF @Data IS NOT NULL
BEGIN
	UPDATE [dbo].[Pictures] 
	SET [Data] = @Data
		WHERE Id= @Id;
END
IF @Comment IS NOT NULL
BEGIN
	IF EXISTS (SELECT 1 FROM Comments WHERE [PictureId] = @Id)
	BEGIN

		UPDATE [dbo].[Comments] 
		SET [Text] = @Comment
			WHERE [PictureId] = @Id;

	END
	ELSE
	BEGIN
		INSERT INTO Comments
		( [Text], [PictureId])
	VALUES
		( @Comment, @Id)
	END
END

SELECT Id, [Data], UserId FROM dbo.Pictures WHERE Id = @Id


GO
/****** Object:  StoredProcedure [dbo].[UsersDelete]    Script Date: 3/22/2015 1:32:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsersDelete]
(
    @Id nchar(5)
)
AS
    SET NOCOUNT OFF;
DELETE FROM dbo.Users WHERE Id = @Id

GO
/****** Object:  StoredProcedure [dbo].[UsersGetAll]    Script Date: 3/22/2015 1:32:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsersGetAll] 
AS
BEGIN
	SET NOCOUNT ON;

    SELECT * FROM dbo.Users
END

GO
/****** Object:  StoredProcedure [dbo].[UsersGetById]    Script Date: 3/22/2015 1:32:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsersGetById]
(
	@Id uniqueidentifier
) 
AS
BEGIN
	SET NOCOUNT ON;

    SELECT Id, Email, [Password] FROM dbo.Users
	WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[UsersInsert]    Script Date: 3/22/2015 1:32:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsersInsert] 
(
	@Email nvarchar(50), 
	@Password nvarchar(50),
	@Id uniqueidentifier out
)
AS
BEGIN
	INSERT INTO Users
		(Email, [Password])
	VALUES
		(@Email, @Password)

SELECT @Id = Id
	FROM dbo.Users
	WHERE Email = @Email
END

GO
/****** Object:  StoredProcedure [dbo].[UsersUpdate]    Script Date: 3/22/2015 1:32:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsersUpdate]
(
    @Id Uniqueidentifier,
    @Email nvarchar(50),
    @Password nvarchar(50)
)
AS
    SET NOCOUNT OFF;
UPDATE [dbo].[Users] 
SET Id = @Id, 
	Email = @Email,
	[Password] = @Password 
	
	WHERE Id= @Id;

GO
/****** Object:  StoredProcedure [dbo].[UsersValidate]    Script Date: 3/22/2015 1:32:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UsersValidate]
(
    @Email nvarchar(50),
    @Password nvarchar(50)
) 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id, Email, [Password] FROM dbo.Users 
	WHERE Email = @Email AND [Password] = @Password
END