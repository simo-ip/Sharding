--USE [DemoAppDb1]
--GO
--USE [DemoAppDb2]
--GO
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
	[Thumbnail] [varbinary](max) NULL,
	[CreatedOn] [datetime] NULL,
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

    SELECT p.Id, p.Thumbnail, p.UserId, CASE WHEN c.[Text] IS NULL THEN '' ELSE c.[Text] END as Comment 
	FROM dbo.Pictures p
	LEFT JOIN [dbo].[Comments] c on p.Id = c.[PictureId]
	Order by CreatedOn DESC
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
	@Thumbnail varbinary(max),
	@UserId uniqueidentifier,
	@Comment varchar(255) = NULL
)
AS
BEGIN
	DECLARE @Id uniqueidentifier
	SET @Id = NEWID() 

	INSERT INTO Pictures
		(Id, [Data], Thumbnail, UserId, CreatedOn)
	VALUES
		(@Id, @Data, @Thumbnail, @UserId, GETDATE())

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
	@Thumbnail varbinary(max) = NULL,
    @Comment varchar(MAX) = NULL
)
AS
    SET NOCOUNT OFF;

IF @Data IS NOT NULL
BEGIN
	UPDATE [dbo].[Pictures] 
	SET [Data] = @Data,
	[Thumbnail] = @Thumbnail
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

--SELECT Id, [Data], UserId FROM dbo.Pictures WHERE Id = @Id


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


/****** Object:  StoredProcedure [dbo].[UsersValidate]    Script Date: 24.3.2015 ã. 16:25:03 ÷. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UsersFindByEmail]
(
    @Email nvarchar(50)
) 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id, Email, [Password] FROM dbo.Users 
	WHERE Email = @Email
END
GO


