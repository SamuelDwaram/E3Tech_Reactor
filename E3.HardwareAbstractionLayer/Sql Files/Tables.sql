use test_db
go

/* Controllers */
CREATE TABLE [dbo].[Controllers](
	[Identifier] [nvarchar](50) NOT NULL primary key,
	[Label] [nvarchar](50) NULL,
	[ProviderName] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[PortNumber] [nvarchar](50) NULL,
	[ResponseTime] int,
)
GO

/* Used Memory Addresses in Controllers */
CREATE TABLE [dbo].[UsedMemoryAddressesInControllers](
	[ControllerIdentifier] [nvarchar](50) not null foreign key references dbo.Controllers(Identifier),
	[UsedMemoryAddress] [nvarchar](50) not NULL,
	constraint PK_UsedMemoryAddress primary key(ControllerIdentifier, UsedMemoryAddress)
)
go

/* Field Devices */
CREATE TABLE [dbo].[FieldDevices](
	[Identifier] [nvarchar](50) NOT NULL primary key,
	[Label] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL
 )
GO

/* Field Devices and Connected Controllers */
CREATE TABLE [dbo].[FieldDevicesAndConnectedControllers](
	[FieldDeviceIdentifier] [nvarchar](50) not null foreign key references dbo.FieldDevices(Identifier),
	[ControllerIdentifier] [nvarchar](50) not null foreign key references dbo.Controllers(Identifier),
	constraint PK_FieldDeviceAndConnectedController primary key(FieldDeviceIdentifier, ControllerIdentifier)
)
go

/* Sensors Data Set */
CREATE TABLE [dbo].[SensorsDataSet](
	[Identifier] [nvarchar](50) NOT NULL,
	[Label] [nvarchar](50) NULL,
	[DataUnit] [nvarchar](50) NULL,
	[FieldDeviceIdentifier] [nvarchar](50) not NULL foreign key references dbo.FieldDevices(Identifier),
	constraint PK_SensorsDataSet primary key(Identifier, FieldDeviceIdentifier)
)
go

/* Field Points */
CREATE TABLE [dbo].[FieldPoints](
	[Label] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[TypeOfAddress] [nvarchar](50) NULL,
	[MemoryAddress] [nvarchar](50) NULL,
	[FieldPointDataType] [nvarchar](50) NULL,
	[ToBeLogged] [nvarchar](10) NULL,
	[RequireNotificationService] [nvarchar](10) NULL,
	[SensorDataSetIdentifier] [nvarchar](50) NOT NULL,
	[MaxValue] [nvarchar](50) NULL,
	[Offset] [float] NULL,
	[Multiplier] [float] NULL,
	[SourceControllerIdentifier] [nvarchar](50) not null foreign key references dbo.Controllers(Identifier),
	[FieldDeviceIdentifier] [nvarchar](50) not null,
	constraint PK_FieldPoint primary key(Label, SensorDataSetIdentifier, FieldDeviceIdentifier),
	constraint FK_FieldPoints_SensorsDataSet foreign key(SensorDataSetIdentifier, FieldDeviceIdentifier) 
		references dbo.SensorsDataSet(Identifier, FieldDeviceIdentifier)
)

/* Command Points */
CREATE TABLE [dbo].[CommandPoints](
	[Label] [nvarchar](50) NOT NULL,
	[SensorDataSetIdentifier] [nvarchar](50) NOT NULL,
	[FieldDeviceIdentifier] [nvarchar](50) NOT NULL,
	constraint FK_CommandPoints_FieldPoints foreign key(Label, SensorDataSetIdentifier, FieldDeviceIdentifier)
		references dbo.FieldPoints(Label, SensorDataSetIdentifier, FieldDeviceIdentifier)
)