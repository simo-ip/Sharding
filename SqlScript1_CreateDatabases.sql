USE [master]
GO

/****** Object:  Database [DemoAppDb1]    Script Date: 3/22/2015 7:46:53 PM ******/
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'DemoAppDb1')
BEGIN
    CREATE DATABASE [DemoAppDb1];

	ALTER DATABASE [DemoAppDb1] SET COMPATIBILITY_LEVEL = 100;

	PRINT 'DemoAppDb1 created successfully'
END
ELSE
BEGIN
	print 'DemoAppDb1 already exists';
END

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'DemoAppDb2')
BEGIN
    CREATE DATABASE [DemoAppDb2];

	ALTER DATABASE [DemoAppDb2] SET COMPATIBILITY_LEVEL = 100;

	PRINT 'DemoAppDb2 created successfully'
END
ELSE
BEGIN
	print 'DemoAppDb2 already exists';
END

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'DemoAppDb3')
BEGIN
    CREATE DATABASE [DemoAppDb3];

	ALTER DATABASE [DemoAppDb3] SET COMPATIBILITY_LEVEL = 100;

	PRINT 'DemoAppDb3 created successfully'
END
ELSE
BEGIN
	print 'DemoAppDb3 already exists';
END
