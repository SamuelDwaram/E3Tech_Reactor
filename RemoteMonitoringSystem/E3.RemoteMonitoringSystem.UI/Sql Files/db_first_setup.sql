USE [master]
GO

ALTER DATABASE [Kabra] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Kabra].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Kabra] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Kabra] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Kabra] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Kabra] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Kabra] SET ARITHABORT OFF 
GO
ALTER DATABASE [Kabra] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Kabra] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Kabra] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Kabra] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Kabra] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Kabra] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Kabra] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Kabra] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Kabra] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Kabra] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Kabra] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Kabra] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Kabra] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Kabra] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Kabra] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Kabra] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Kabra] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Kabra] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Kabra] SET  MULTI_USER 
GO
ALTER DATABASE [Kabra] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Kabra] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Kabra] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Kabra] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Kabra] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Kabra] SET QUERY_STORE = OFF
GO
USE [Kabra]
GO
/****** Object:  UserDefinedFunction [dbo].[GetAvgVoltage]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create function [dbo].[GetAvgVoltage](
@Voltage_1 float,
@Voltage_2 float,
@Voltage_3 float
)returns float
as
begin
	declare @result float;
	set @result = (@Voltage_1 + @Voltage_2 + @Voltage_3) / 3;
	return round(@result,1);
end
GO
/****** Object:  UserDefinedFunction [dbo].[IsDataRecordingForDevice]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[IsDataRecordingForDevice](@deviceId nvarchar(50))
returns bit
as
begin
	if object_id(@deviceId, 'u') is not null 
		return 1;
	return 0;
end
GO
/****** Object:  UserDefinedFunction [dbo].[IsValidFieldPoint]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* FieldPointIdentifier in Alarm Policy Unit check function */
CREATE FUNCTION [dbo].[IsValidFieldPoint] (
    @FieldDeviceIdentifier nvarchar(50),
	@FieldPointIdentifier nvarchar(50)
)
RETURNS VARCHAR(5)
AS
BEGIN
    IF @FieldPointIdentifier in (SELECT Label FROM dbo.FieldPoints WHERE SensorDataSetIdentifier 
									= any(select Identifier from dbo.SensorsDataSet where FieldDeviceIdentifier = @FieldDeviceIdentifier))
        return 'True'
    return 'False'
END
GO
/****** Object:  Table [dbo].[CommandPoints]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommandPoints](
	[Label] [nvarchar](50) NOT NULL,
	[SensorDataSetIdentifier] [nvarchar](50) NOT NULL,
	[FieldDeviceIdentifier] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Controllers]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Controllers](
	[Identifier] [nvarchar](50) NOT NULL,
	[Label] [nvarchar](50) NULL,
	[ProviderName] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[PortNumber] [nvarchar](50) NULL,
	[ResponseTime] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeviceMaintenanceInfo]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeviceMaintenanceInfo](
	[FieldDeviceIdentifier] [nvarchar](255) NULL,
	[ReasonForMaintenance] [nvarchar](255) NULL,
	[MaintenanceDate] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldDevices]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldDevices](
	[Identifier] [nvarchar](50) NOT NULL,
	[Label] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldDevicesAndConnectedControllers]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldDevicesAndConnectedControllers](
	[FieldDeviceIdentifier] [nvarchar](50) NOT NULL,
	[ControllerIdentifier] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_FieldDeviceAndConnectedController] PRIMARY KEY CLUSTERED 
(
	[FieldDeviceIdentifier] ASC,
	[ControllerIdentifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldPoints]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
	[SourceControllerIdentifier] [nvarchar](50) NOT NULL,
	[FieldDeviceIdentifier] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_FieldPoint] PRIMARY KEY CLUSTERED 
(
	[Label] ASC,
	[SensorDataSetIdentifier] ASC,
	[FieldDeviceIdentifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motor_1]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motor_1](
	[Current_R] [float] NULL,
	[Current_Y] [float] NULL,
	[Current_B] [float] NULL,
	[Voltage_1] [float] NULL,
	[Voltage_2] [float] NULL,
	[Voltage_3] [float] NULL,
	[Vibration] [float] NULL,
	[TimeStamp] [datetime] NULL,
	[Status] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motor_10]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motor_10](
	[Current_R] [float] NULL,
	[Current_Y] [float] NULL,
	[Current_B] [float] NULL,
	[Voltage_1] [float] NULL,
	[Voltage_2] [float] NULL,
	[Voltage_3] [float] NULL,
	[Vibration] [float] NULL,
	[TimeStamp] [datetime] NULL,
	[Status] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motor_11]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motor_11](
	[Current_R] [float] NULL,
	[Current_Y] [float] NULL,
	[Current_B] [float] NULL,
	[Voltage_1] [float] NULL,
	[Voltage_2] [float] NULL,
	[Voltage_3] [float] NULL,
	[Vibration] [float] NULL,
	[TimeStamp] [datetime] NULL,
	[Status] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motor_12]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motor_12](
	[Current_R] [float] NULL,
	[Current_Y] [float] NULL,
	[Current_B] [float] NULL,
	[Voltage_1] [float] NULL,
	[Voltage_2] [float] NULL,
	[Voltage_3] [float] NULL,
	[Vibration] [float] NULL,
	[TimeStamp] [datetime] NULL,
	[Status] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motor_13]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motor_13](
	[Current_R] [float] NULL,
	[Current_Y] [float] NULL,
	[Current_B] [float] NULL,
	[Voltage_1] [float] NULL,
	[Voltage_2] [float] NULL,
	[Voltage_3] [float] NULL,
	[Vibration] [float] NULL,
	[TimeStamp] [datetime] NULL,
	[Status] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motor_14]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motor_14](
	[Current_R] [float] NULL,
	[Current_Y] [float] NULL,
	[Current_B] [float] NULL,
	[Voltage_1] [float] NULL,
	[Voltage_2] [float] NULL,
	[Voltage_3] [float] NULL,
	[Vibration] [float] NULL,
	[TimeStamp] [datetime] NULL,
	[Status] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motor_15]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motor_15](
	[Current_R] [float] NULL,
	[Current_Y] [float] NULL,
	[Current_B] [float] NULL,
	[Voltage_1] [float] NULL,
	[Voltage_2] [float] NULL,
	[Voltage_3] [float] NULL,
	[Vibration] [float] NULL,
	[TimeStamp] [datetime] NULL,
	[Status] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motor_16]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motor_16](
	[Current_R] [float] NULL,
	[Current_Y] [float] NULL,
	[Current_B] [float] NULL,
	[Voltage_1] [float] NULL,
	[Voltage_2] [float] NULL,
	[Voltage_3] [float] NULL,
	[Vibration] [float] NULL,
	[TimeStamp] [datetime] NULL,
	[Status] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motor_17]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motor_17](
	[Current_R] [float] NULL,
	[Current_Y] [float] NULL,
	[Current_B] [float] NULL,
	[Voltage_1] [float] NULL,
	[Voltage_2] [float] NULL,
	[Voltage_3] [float] NULL,
	[Vibration] [float] NULL,
	[TimeStamp] [datetime] NULL,
	[Status] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motor_2]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motor_2](
	[Current_R] [float] NULL,
	[Current_Y] [float] NULL,
	[Current_B] [float] NULL,
	[Voltage_1] [float] NULL,
	[Voltage_2] [float] NULL,
	[Voltage_3] [float] NULL,
	[Vibration] [float] NULL,
	[TimeStamp] [datetime] NULL,
	[Status] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motor_3]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motor_3](
	[Current_R] [float] NULL,
	[Current_Y] [float] NULL,
	[Current_B] [float] NULL,
	[Voltage_1] [float] NULL,
	[Voltage_2] [float] NULL,
	[Voltage_3] [float] NULL,
	[Vibration] [float] NULL,
	[TimeStamp] [datetime] NULL,
	[Status] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motor_4]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motor_4](
	[Current_R] [float] NULL,
	[Current_Y] [float] NULL,
	[Current_B] [float] NULL,
	[Voltage_1] [float] NULL,
	[Voltage_2] [float] NULL,
	[Voltage_3] [float] NULL,
	[Vibration] [float] NULL,
	[TimeStamp] [datetime] NULL,
	[Status] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motor_5]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motor_5](
	[Current_R] [float] NULL,
	[Current_Y] [float] NULL,
	[Current_B] [float] NULL,
	[Voltage_1] [float] NULL,
	[Voltage_2] [float] NULL,
	[Voltage_3] [float] NULL,
	[Vibration] [float] NULL,
	[TimeStamp] [datetime] NULL,
	[Status] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motor_6]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motor_6](
	[Current_R] [float] NULL,
	[Current_Y] [float] NULL,
	[Current_B] [float] NULL,
	[Voltage_1] [float] NULL,
	[Voltage_2] [float] NULL,
	[Voltage_3] [float] NULL,
	[Vibration] [float] NULL,
	[TimeStamp] [datetime] NULL,
	[Status] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motor_7]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motor_7](
	[Current_R] [float] NULL,
	[Current_Y] [float] NULL,
	[Current_B] [float] NULL,
	[Voltage_1] [float] NULL,
	[Voltage_2] [float] NULL,
	[Voltage_3] [float] NULL,
	[Vibration] [float] NULL,
	[TimeStamp] [datetime] NULL,
	[Status] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motor_8]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motor_8](
	[Current_R] [float] NULL,
	[Current_Y] [float] NULL,
	[Current_B] [float] NULL,
	[Voltage_1] [float] NULL,
	[Voltage_2] [float] NULL,
	[Voltage_3] [float] NULL,
	[Vibration] [float] NULL,
	[TimeStamp] [datetime] NULL,
	[Status] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motor_9]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motor_9](
	[Current_R] [float] NULL,
	[Current_Y] [float] NULL,
	[Current_B] [float] NULL,
	[Voltage_1] [float] NULL,
	[Voltage_2] [float] NULL,
	[Voltage_3] [float] NULL,
	[Vibration] [float] NULL,
	[TimeStamp] [datetime] NULL,
	[Status] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SensorsDataSet]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SensorsDataSet](
	[Identifier] [nvarchar](50) NOT NULL,
	[Label] [nvarchar](50) NULL,
	[DataUnit] [nvarchar](50) NULL,
	[FieldDeviceIdentifier] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SensorsDataSet] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC,
	[FieldDeviceIdentifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemAlarmParameters]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemAlarmParameters](
	[Id] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[ParametersType] [nvarchar](20) NULL,
	[RatedValue] [float] NULL,
	[VariationPercentage] [float] NULL,
	[VariationType] [nvarchar](10) NULL,
	[UpperLimit] [float] NULL,
	[LowerLimit] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemAlarmPolicies]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemAlarmPolicies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DeviceId] [nvarchar](50) NULL,
	[DeviceLabel] [nvarchar](50) NULL,
	[PolicyType] [nvarchar](10) NULL,
	[Title] [nvarchar](max) NULL,
	[Message] [nvarchar](max) NULL,
	[Status] [bit] NULL,
	[CreatedTimeStamp] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemAlarms]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemAlarms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemAlarmPolicyId] [int] NULL,
	[SystemAlarmParameterName] [nvarchar](50) NULL,
	[SystemFailureId] [int] NULL,
	[DeviceId] [nvarchar](50) NULL,
	[DeviceLabel] [nvarchar](50) NULL,
	[FieldPointLabel] [nvarchar](50) NULL,
	[Title] [nvarchar](50) NULL,
	[Message] [nvarchar](max) NULL,
	[State] [nvarchar](30) NULL,
	[RaisedTimeStamp] [datetime] NULL,
	[AcknowledgedTimeStamp] [datetime] NULL,
	[Type] [nvarchar](20) NULL,
	[TimeStamp] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemFailurePolicies]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemFailurePolicies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DeviceId] [nvarchar](50) NULL,
	[DeviceLabel] [nvarchar](50) NULL,
	[FailedResourceLabel] [nvarchar](50) NULL,
	[TargetValue] [nvarchar](10) NULL,
	[Title] [nvarchar](50) NULL,
	[Message] [nvarchar](max) NULL,
	[TroubleShootMessage] [nvarchar](max) NULL,
	[FailureResourceType] [nvarchar](20) NULL,
	[Status] [bit] NULL,
	[CreatedTimeStamp] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemFailures]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemFailures](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemFailurePolicyId] [int] NULL,
	[DeviceId] [nvarchar](50) NULL,
	[DeviceLabel] [nvarchar](50) NULL,
	[Title] [nvarchar](50) NULL,
	[TroubleShootMessage] [nvarchar](max) NULL,
	[FailedResourceLabel] [nvarchar](50) NULL,
	[FailureState] [nvarchar](20) NULL,
	[FailureType] [nvarchar](20) NULL,
	[TimeStamp] [datetime] NULL,
	[RaisedTimeStamp] [datetime] NULL,
	[AcknowledgedTimeStamp] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsedMemoryAddressesInControllers]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsedMemoryAddressesInControllers](
	[ControllerIdentifier] [nvarchar](50) NOT NULL,
	[UsedMemoryAddress] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UsedMemoryAddress] PRIMARY KEY CLUSTERED 
(
	[ControllerIdentifier] ASC,
	[UsedMemoryAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CommandPoints]  WITH CHECK ADD  CONSTRAINT [FK_CommandPoints_FieldPoints] FOREIGN KEY([Label], [SensorDataSetIdentifier], [FieldDeviceIdentifier])
REFERENCES [dbo].[FieldPoints] ([Label], [SensorDataSetIdentifier], [FieldDeviceIdentifier])
GO
ALTER TABLE [dbo].[CommandPoints] CHECK CONSTRAINT [FK_CommandPoints_FieldPoints]
GO
ALTER TABLE [dbo].[FieldDevicesAndConnectedControllers]  WITH CHECK ADD FOREIGN KEY([ControllerIdentifier])
REFERENCES [dbo].[Controllers] ([Identifier])
GO
ALTER TABLE [dbo].[FieldDevicesAndConnectedControllers]  WITH CHECK ADD FOREIGN KEY([ControllerIdentifier])
REFERENCES [dbo].[Controllers] ([Identifier])
GO
ALTER TABLE [dbo].[FieldDevicesAndConnectedControllers]  WITH CHECK ADD FOREIGN KEY([FieldDeviceIdentifier])
REFERENCES [dbo].[FieldDevices] ([Identifier])
GO
ALTER TABLE [dbo].[FieldDevicesAndConnectedControllers]  WITH CHECK ADD FOREIGN KEY([FieldDeviceIdentifier])
REFERENCES [dbo].[FieldDevices] ([Identifier])
GO
ALTER TABLE [dbo].[FieldPoints]  WITH CHECK ADD FOREIGN KEY([SourceControllerIdentifier])
REFERENCES [dbo].[Controllers] ([Identifier])
GO
ALTER TABLE [dbo].[FieldPoints]  WITH CHECK ADD FOREIGN KEY([SourceControllerIdentifier])
REFERENCES [dbo].[Controllers] ([Identifier])
GO
ALTER TABLE [dbo].[FieldPoints]  WITH CHECK ADD  CONSTRAINT [FK_FieldPoints_SensorsDataSet] FOREIGN KEY([SensorDataSetIdentifier], [FieldDeviceIdentifier])
REFERENCES [dbo].[SensorsDataSet] ([Identifier], [FieldDeviceIdentifier])
GO
ALTER TABLE [dbo].[FieldPoints] CHECK CONSTRAINT [FK_FieldPoints_SensorsDataSet]
GO
ALTER TABLE [dbo].[SensorsDataSet]  WITH CHECK ADD FOREIGN KEY([FieldDeviceIdentifier])
REFERENCES [dbo].[FieldDevices] ([Identifier])
GO
ALTER TABLE [dbo].[SensorsDataSet]  WITH CHECK ADD FOREIGN KEY([FieldDeviceIdentifier])
REFERENCES [dbo].[FieldDevices] ([Identifier])
GO
ALTER TABLE [dbo].[SystemAlarmParameters]  WITH CHECK ADD FOREIGN KEY([Id])
REFERENCES [dbo].[SystemAlarmPolicies] ([Id])
GO
ALTER TABLE [dbo].[SystemAlarmPolicies]  WITH CHECK ADD FOREIGN KEY([DeviceId])
REFERENCES [dbo].[FieldDevices] ([Identifier])
GO
ALTER TABLE [dbo].[SystemAlarms]  WITH CHECK ADD FOREIGN KEY([DeviceId])
REFERENCES [dbo].[FieldDevices] ([Identifier])
GO
ALTER TABLE [dbo].[SystemAlarms]  WITH CHECK ADD FOREIGN KEY([SystemAlarmPolicyId])
REFERENCES [dbo].[SystemAlarmPolicies] ([Id])
GO
ALTER TABLE [dbo].[SystemAlarms]  WITH CHECK ADD FOREIGN KEY([SystemFailureId])
REFERENCES [dbo].[SystemFailures] ([Id])
GO
ALTER TABLE [dbo].[SystemFailurePolicies]  WITH CHECK ADD FOREIGN KEY([DeviceId])
REFERENCES [dbo].[FieldDevices] ([Identifier])
GO
ALTER TABLE [dbo].[SystemFailures]  WITH CHECK ADD FOREIGN KEY([DeviceId])
REFERENCES [dbo].[FieldDevices] ([Identifier])
GO
ALTER TABLE [dbo].[SystemFailures]  WITH CHECK ADD FOREIGN KEY([SystemFailurePolicyId])
REFERENCES [dbo].[SystemFailurePolicies] ([Id])
GO
ALTER TABLE [dbo].[UsedMemoryAddressesInControllers]  WITH CHECK ADD FOREIGN KEY([ControllerIdentifier])
REFERENCES [dbo].[Controllers] ([Identifier])
GO
ALTER TABLE [dbo].[UsedMemoryAddressesInControllers]  WITH CHECK ADD FOREIGN KEY([ControllerIdentifier])
REFERENCES [dbo].[Controllers] ([Identifier])
GO
ALTER TABLE [dbo].[SystemAlarmParameters]  WITH CHECK ADD  CONSTRAINT [check_ParametersType] CHECK  (([ParametersType]='RatedValueVariations' OR [ParametersType]='Limits'))
GO
ALTER TABLE [dbo].[SystemAlarmParameters] CHECK CONSTRAINT [check_ParametersType]
GO
ALTER TABLE [dbo].[SystemAlarmParameters]  WITH CHECK ADD  CONSTRAINT [check_VariationType] CHECK  (([VariationType]='Both' OR [VariationType]='Below' OR [VariationType]='Above'))
GO
ALTER TABLE [dbo].[SystemAlarmParameters] CHECK CONSTRAINT [check_VariationType]
GO
ALTER TABLE [dbo].[SystemAlarmPolicies]  WITH CHECK ADD  CONSTRAINT [check_PolicyType] CHECK  (([PolicyType]='Group' OR [PolicyType]='Individual'))
GO
ALTER TABLE [dbo].[SystemAlarmPolicies] CHECK CONSTRAINT [check_PolicyType]
GO
ALTER TABLE [dbo].[SystemAlarms]  WITH CHECK ADD  CONSTRAINT [check_State] CHECK  (([State]='Resolved' OR [State]='Acknowledged' OR [State]='Raised'))
GO
ALTER TABLE [dbo].[SystemAlarms] CHECK CONSTRAINT [check_State]
GO
ALTER TABLE [dbo].[SystemAlarms]  WITH CHECK ADD  CONSTRAINT [check_Type] CHECK  (([Type]='System' OR [Type]='Process'))
GO
ALTER TABLE [dbo].[SystemAlarms] CHECK CONSTRAINT [check_Type]
GO
ALTER TABLE [dbo].[SystemFailurePolicies]  WITH CHECK ADD CHECK  (([FailureResourceType]='Controller' OR [FailureResourceType]='Device'))
GO
ALTER TABLE [dbo].[SystemFailures]  WITH CHECK ADD CHECK  (([FailureState]='Resolved' OR [FailureState]='Acknowledged' OR [FailureState]='Raised'))
GO
ALTER TABLE [dbo].[SystemFailures]  WITH CHECK ADD CHECK  (([FailureType]='Communication' OR [FailureType]='Hardware' OR [FailureType]='System'))
GO
/****** Object:  StoredProcedure [dbo].[AcknowledgeAlarm]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Acknowledges the Alarm */
create procedure [dbo].[AcknowledgeAlarm]
@AlarmIdentifier nvarchar(50)
as
begin
	set nocount on;
	update [dbo].[AlarmTable] set AlarmState = 'Acknowledged', TimeActionDone = CURRENT_TIMESTAMP where AlarmId = @AlarmIdentifier
end
GO
/****** Object:  StoredProcedure [dbo].[DeleteSystemAlarmPolicy]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--delete SystemAlarmPolicy
create procedure [dbo].[DeleteSystemAlarmPolicy]
@Id int
as
begin
	set nocount on;
	delete from dbo.SystemAlarmParameters where Id = @Id;
	delete from dbo.SystemAlarmPolicies where Id = @Id;
end
GO
/****** Object:  StoredProcedure [dbo].[DeleteSystemFailurePolicy]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--delete SystemFailurePolicy
create procedure [dbo].[DeleteSystemFailurePolicy]
@Id int
as
begin
	set nocount on;
	delete from dbo.SystemFailurePolicies where Id = @Id;
end
GO
/****** Object:  StoredProcedure [dbo].[DismissAlarm]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Dismisses the Alarm */
create procedure [dbo].[DismissAlarm]
@AlarmIdentifier nvarchar(50)
as
begin
	set nocount on;
	update [dbo].[AlarmTable] set AlarmState = 'Dismissed', TimeActionDone = CURRENT_TIMESTAMP where AlarmId = @AlarmIdentifier
end
GO
/****** Object:  StoredProcedure [dbo].[GetAlarmDetails]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Get Alarm Details */
create procedure [dbo].[GetAlarmDetails]
@AlarmIdentifier nvarchar(50)
as
begin
	set nocount on;
	select * from [dbo].[AlarmTable] where AlarmId = @AlarmIdentifier;
end
GO
/****** Object:  StoredProcedure [dbo].[GetAlarmPolicySetList]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Get Alarm Policy Sets for Given Alarm Policy */
create procedure [dbo].[GetAlarmPolicySetList]
@PolicyIdentifier nvarchar(50)
as
begin
	set nocount on;
	select * from dbo.AlarmPolicySets where PolicyIdentifier = @PolicyIdentifier;
end
GO
/****** Object:  StoredProcedure [dbo].[GetAlarmPolicyUnitList]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Get Alarm Policy Units for Given Alarm Policy Set */
create procedure [dbo].[GetAlarmPolicyUnitList]
@PolicySetIdentifier nvarchar(50)
as
begin
	set nocount on;
	select * from dbo.AlarmPolicyUnits where PolicySetIdentifier = @PolicySetIdentifier;
end
GO
/****** Object:  StoredProcedure [dbo].[GetAlarmsBetweenTimeStamps]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetAlarmsBetweenTimeStamps]
@DeviceId nvarchar(50),
@StartTime datetime, 
@EndTime datetime
as
begin
	set nocount on;
	select * from dbo.SystemAlarms where DeviceId = @DeviceId and (RaisedTimeStamp between @StartTime and @EndTime);
end
GO
/****** Object:  StoredProcedure [dbo].[GetAllAlarmPolicies]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Get All Alarm Policies */
create procedure [dbo].[GetAllAlarmPolicies]
as
begin
	set nocount on;
	select * from dbo.AlarmPolicies;
end
GO
/****** Object:  StoredProcedure [dbo].[GetAllAlarms]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Reads all the Alarms */
create procedure [dbo].[GetAllAlarms]
as
begin
	set nocount on;
	select * from [dbo].AlarmTable order by TimeOccured desc
end
GO
/****** Object:  StoredProcedure [dbo].[GetAllControllers]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Get all available controllers */
create procedure [dbo].[GetAllControllers]
as
begin
	set nocount on;
	select * from dbo.Controllers;
end
GO
/****** Object:  StoredProcedure [dbo].[GetAllDevicesData]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[GetAllDevicesData]
@StartDate nvarchar(25),
@EndDate nvarchar(25)
as
begin
	set nocount on;
	select * from dbo.FieldDevices;
	
	DECLARE @id nvarchar(50)
	DECLARE @label NVARCHAR(100)
	DECLARE @getId CURSOR

	SET @getId = CURSOR FOR
	SELECT Identifier, Label from dbo.FieldDevices

	OPEN @getId
	FETCH NEXT
	FROM @getId INTO @id, @label
	WHILE @@FETCH_STATUS = 0
	BEGIN
		declare @query nvarchar(max);
		set @query = 'select * from dbo.' + @id + ' where TimeStamp between '''+ @StartDate +''' and '''+ @EndDate +''' order by TimeStamp';
		exec sp_executesql @query;
		
		FETCH NEXT
		FROM @getId INTO @id, @label
	END

	CLOSE @getId
	DEALLOCATE @getId
end
GO
/****** Object:  StoredProcedure [dbo].[GetAllSystemAlarmPolicies]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--get all SystemAlarmPolices
create procedure [dbo].[GetAllSystemAlarmPolicies]
as
begin
	set nocount on;
	select * from dbo.SystemAlarmPolicies inner join dbo.SystemAlarmParameters on dbo.SystemAlarmPolicies.Id=dbo.SystemAlarmParameters.Id;
end
GO
/****** Object:  StoredProcedure [dbo].[GetAllSystemAlarms]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Get All System Alarms
create procedure [dbo].[GetAllSystemAlarms]
as
begin
	set nocount on;
	declare @timeLimit datetime = (select DATEADD(DD, -1, GETDATE()));
	select * from dbo.SystemAlarms where CONVERT(varchar, RaisedTimeStamp, 21) >= CONVERT(varchar, @timeLimit, 21);
end
GO
/****** Object:  StoredProcedure [dbo].[GetAllSystemFailurePolicies]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--get all SystemFailurePolices
create procedure [dbo].[GetAllSystemFailurePolicies]
as
begin
	set nocount on;
	select * from dbo.SystemFailurePolicies;
end
GO
/****** Object:  StoredProcedure [dbo].[GetAllSystemFailures]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Get All System Failures
create procedure [dbo].[GetAllSystemFailures]
as
begin
	set nocount on;
	declare @timeLimit datetime = (select DATEADD(DD, -1, GETDATE()));
	select * from dbo.SystemFailures inner join dbo.SystemFailurePolicies on dbo.SystemFailures.SystemFailurePolicyId=dbo.SystemFailurePolicies.Id
	where CONVERT(varchar, TimeStamp, 21) >= CONVERT(varchar, @timeLimit, 21);
end
GO
/****** Object:  StoredProcedure [dbo].[GetAvailableFieldDevices]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Get Available FieldDevices */
create procedure [dbo].[GetAvailableFieldDevices]
as
begin
	set nocount on;
	select Identifier, Label from dbo.FieldDevices;
end
GO
/****** Object:  StoredProcedure [dbo].[GetAvailableFieldPointsInFieldDevice]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Get Available Field Points in a Field Device */
create procedure [dbo].[GetAvailableFieldPointsInFieldDevice]
@FieldDeviceIdentifier nvarchar(50)
as
begin
	set nocount on;
	select Label from dbo.FieldPoints where RequireNotificationService='true' and SensorDataSetIdentifier in (select Identifier from dbo.SensorsDataSet where FieldDeviceIdentifier = @FieldDeviceIdentifier);
end
GO
/****** Object:  StoredProcedure [dbo].[GetControllerInfo]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Gets the controller info from database */
create procedure [dbo].[GetControllerInfo]
@ControllerIdentifier nvarchar(max)
as
begin
	set nocount on;
	select * from dbo.Controllers where Identifier = @ControllerIdentifier;
end
GO
/****** Object:  StoredProcedure [dbo].[GetControllersConnectedToFieldDevice]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Gets the controllers connected to the field device */
create procedure [dbo].[GetControllersConnectedToFieldDevice]
@FieldDeviceIdentifier nvarchar(max)
as
begin
	set nocount on;
	select * from dbo.FieldDevicesAndConnectedControllers where FieldDeviceIdentifier = @FieldDeviceIdentifier;
end
GO
/****** Object:  StoredProcedure [dbo].[GetDataRecordedDevices]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*Get the Available Devices for which Data is being recorded*/
create procedure [dbo].[GetDataRecordedDevices]
as
begin
	set nocount on;
	create table #AvailableDevices(DeviceId nvarchar(50), DeviceLabel nvarchar(50));
	insert into #AvailableDevices select Identifier, Label from dbo.FieldDevices where dbo.IsDataRecordingForDevice(Identifier) = 1;
	select * from #AvailableDevices
end
GO
/****** Object:  StoredProcedure [dbo].[GetFieldDevicesList]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Gets the Field Devices List from database */
create procedure [dbo].[GetFieldDevicesList]
as
begin
	set nocount on;
	select * from dbo.FieldDevices;
end
GO
/****** Object:  StoredProcedure [dbo].[GetFieldPointsInSensorsDataSet]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Gets the field points in the Sensor Data Set */
create procedure [dbo].[GetFieldPointsInSensorsDataSet]
@SensorsDataSetIdentifier nvarchar(max)
as
begin
	set nocount on;
	select * from dbo.FieldPoints where SensorDataSetIdentifier = @SensorsDataSetIdentifier;
end
GO
/****** Object:  StoredProcedure [dbo].[GetLastMaintenaceDate]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Get Last Maintenace Date of Device */
create procedure [dbo].[GetLastMaintenaceDate]
@FieldDeviceIdentifier nvarchar(50)
as
begin
	set nocount on;
	select * from dbo.DeviceMaintenanceInfo where FieldDeviceIdentifier = @FieldDeviceIdentifier;
end
GO
/****** Object:  StoredProcedure [dbo].[GetPdfDetails]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* Get the Pdf Details from the database */
create procedure [dbo].[GetPdfDetails]
@Identifier nvarchar(50)
as
begin
	set nocount on;
	select * from dbo.PdfFiles where Identifier = @Identifier;
end
GO
/****** Object:  StoredProcedure [dbo].[GetRunningTime]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GetRunningTime]
@DeviceId nvarchar(25),
@StartDate nvarchar(25),
@EndDate nvarchar(25)
as
begin
	set nocount on;
	declare @query nvarchar(max);
	create table #TempTable(RunningTime float);
	set @query = 'select count(*) from dbo.' + @DeviceId + ' where TimeStamp between '''+ @StartDate +''' and '''+ @EndDate +''' and Status = 1';
	insert into #TempTable exec sp_executesql @query;
	select top(1) RunningTime from #TempTable;
end
GO
/****** Object:  StoredProcedure [dbo].[GetSensorsDataSetInFieldDevice]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Gets the sensors data set in the field device */
create procedure [dbo].[GetSensorsDataSetInFieldDevice]
@FieldDeviceIdentifier nvarchar(max)
as
begin
	set nocount on;
	select * from dbo.SensorsDataSet where FieldDeviceIdentifier = @FieldDeviceIdentifier;
end
GO
/****** Object:  StoredProcedure [dbo].[GetUsedMemoryAddressesOfGivenController]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Get the Used Memory Addresses of a controller */
create procedure [dbo].[GetUsedMemoryAddressesOfGivenController]
@ControllerIdentifier nvarchar(50)
as
begin
	set nocount on;
	select * from dbo.UsedMemoryAddressesInControllers where ControllerIdentifier=@ControllerIdentifier;
end
GO
/****** Object:  StoredProcedure [dbo].[InsertSystemAlarm]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Insert System Alarm
create procedure [dbo].[InsertSystemAlarm]
@SystemAlarmPolicyId int = 0,
@SystemAlarmParameterName nvarchar(50) = '',
@SystemFailureId int = 0,
@DeviceId nvarchar(50),
@DeviceLabel nvarchar(50),
@FieldPointLabel nvarchar(50),
@Title nvarchar(50),
@Message nvarchar(max),
@State nvarchar(20),
@Type nvarchar(20),
@TimeStamp datetime
as
begin
	set nocount on;
	insert into dbo.SystemAlarms(SystemAlarmPolicyId, SystemAlarmParameterName, SystemFailureId, DeviceId, DeviceLabel, FieldPointLabel, Title, Message, State, Type,TimeStamp, RaisedTimeStamp)
	values(iif(@SystemAlarmPolicyId = 0, null, @SystemAlarmPolicyId), iif(@SystemAlarmParameterName = '', null, @SystemAlarmParameterName),
	iif(@SystemFailureId = 0, null, @SystemFailureId), @DeviceId, @DeviceLabel, @FieldPointLabel, @Title, @Message, @State, @Type, @TimeStamp, @TimeStamp);
end
GO
/****** Object:  StoredProcedure [dbo].[InsertSystemAlarmPolicy]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--insert SystemAlarmPolicy
create procedure [dbo].[InsertSystemAlarmPolicy]
@DeviceId nvarchar(50),
@DeviceLabel nvarchar(50),
@PolicyType nvarchar(10),
@Title nvarchar(max),
@Message nvarchar(max),
@Status bit,
@ParameterName nvarchar(50),
@ParametersType nvarchar(50),
@RatedValue float,
@VariationPercentage float,
@VariationType nvarchar(10),
@UpperLimit float,
@LowerLimit float
as
begin
	set nocount on;
	insert into dbo.SystemAlarmPolicies values(@DeviceId, @DeviceLabel, @PolicyType, @Title, @Message, @Status, GETDATE());
	declare @alarmPolicyId int = (select SCOPE_IDENTITY() SystemAlarmPolicies);
	insert into dbo.SystemAlarmParameters 
		values(@alarmPolicyId, @ParameterName, @ParametersType, @RatedValue, @VariationPercentage, @VariationType, @UpperLimit, @LowerLimit);
end
GO
/****** Object:  StoredProcedure [dbo].[InsertSystemFailure]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Insert System Failure
CREATE procedure [dbo].[InsertSystemFailure]
@SystemFailurePolicyId int,
@DeviceId nvarchar(50),
@DeviceLabel nvarchar(50),
@Title nvarchar(50),
@TroubleShootMessage nvarchar(max),
@FailedResourceLabel nvarchar(50),
@FailureState nvarchar(20),
@FailureType nvarchar(20),
@TimeStamp datetime
as
begin
	set nocount on;
	insert into dbo.SystemFailures(SystemFailurePolicyId, DeviceId, DeviceLabel, Title, TroubleShootMessage, FailedResourceLabel, FailureState, FailureType, TimeStamp, RaisedTimeStamp)
	values(@SystemFailurePolicyId, @DeviceId, @DeviceLabel, @Title, @TroubleShootMessage, @FailedResourceLabel, @FailureState, @FailureType, @TimeStamp, @TimeStamp);
end
GO
/****** Object:  StoredProcedure [dbo].[InsertSystemFailurePolicy]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--insert SystemFailurePolicy
create procedure [dbo].[InsertSystemFailurePolicy]
@DeviceId nvarchar(50),
@DeviceLabel nvarchar(50),
@FailedResourceLabel nvarchar(50),
@TargetValue nvarchar(10),
@Title nvarchar(max),
@Message nvarchar(max),
@TroubleShootMessage nvarchar(max),
@FailureResourceType nvarchar(20),
@Status bit,
@CreatedTimeStamp datetime
as
begin
	set nocount on;
	insert into dbo.SystemFailurePolicies values(@DeviceId, @DeviceLabel, @FailedResourceLabel, @TargetValue, @Title, @Message, @TroubleShootMessage, @FailureResourceType, @Status, @CreatedTimeStamp);
end
GO
/****** Object:  StoredProcedure [dbo].[LogAlarm]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Alarm Table Procedures */
/* Log the Alarm */
CREATE procedure [dbo].[LogAlarm]
@Title nvarchar(max),
@MonitoringDataPointsByteArray varbinary(max),
@ResolutionMessage nvarchar(max),
@AlarmType nvarchar(50),
@AlarmSeverity nvarchar(50)
as
begin
	set nocount on;
	declare @newAlarmId nvarchar(50);
	set @newAlarmId = (select convert(varchar, getdate(), 21));
	insert into dbo.AlarmTable(AlarmId, TimeOccured, MonitoringDataPoints, Title, ResolutionMessage, AlarmType, AlarmState, AlarmSeverity, PopupNotificationAcknowledgement, AudioNotificationAcknowledgement) 
		values(@newAlarmId, GETDATE(), @MonitoringDataPointsByteArray, @Title, @ResolutionMessage, @AlarmType, 'Raised', @AlarmSeverity, 0, 0);
end
GO
/****** Object:  StoredProcedure [dbo].[LogLiveData]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* Logs Live Data for the Field Device*/
create procedure [dbo].[LogLiveData]
@FieldDeviceIdentifier nvarchar(50),
@Current_R nvarchar(10) = null,
@Current_Y nvarchar(10) = null,
@Current_B nvarchar(10) = null,
@Voltage_1 nvarchar(10) = null,
@Voltage_2 nvarchar(10) = null,
@Voltage_3 nvarchar(10) = null,
@Vibration nvarchar(10) = null,
@Status nvarchar(10) = null
as
begin
	set nocount on;
	declare @i_R nvarchar(10) = IIF(@Current_R is null, 0, cast(@Current_R as float));
	declare @i_Y nvarchar(10) = IIF(@Current_Y is null, 0, cast(@Current_Y as float));
	declare @i_B nvarchar(10) = IIF(@Current_B is null, 0, cast(@Current_B as float));
	declare @v_1 nvarchar(10) = IIF(@Voltage_1 is null, 0, cast(@Voltage_1 as float));
	declare @v_2 nvarchar(10) = IIF(@Voltage_2 is null, 0, cast(@Voltage_2 as float));
	declare @v_3 nvarchar(10) = IIF(@Voltage_3 is null, 0, cast(@Voltage_3 as float));
	declare @vib nvarchar(10) = IIF(@Vibration is null, 0, cast(@Vibration as float));
	declare @s nvarchar(10) = cast(@Status as bit);

	declare @query nvarchar(max);
	set @query = 'insert into dbo.' + @FieldDeviceIdentifier + ' values(' + @i_R + ',' + @i_Y + ',' + @i_B + ',' + @v_1 + ',' + @v_2 + ',' + @v_3 + ',' + @vib + ',' + @s + ',GetDate())';
	exec sp_executesql @query;
end
GO
/****** Object:  StoredProcedure [dbo].[ModifyAlarmState]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Modify Alarm State
create procedure [dbo].[ModifyAlarmState]
@Id int,
@AlarmState nvarchar(20)
as
begin
	set nocount on;
	if @AlarmState = 'Acknowledged'
		update dbo.SystemAlarms set State = @AlarmState, AcknowledgedTimeStamp = GETDATE(), TimeStamp = GETDATE() where Id = @Id;
	else		
		update dbo.SystemAlarms set State = @AlarmState, TimeStamp = GETDATE() where Id = @Id;
end
GO
/****** Object:  StoredProcedure [dbo].[ModifyFailureState]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Modify Failure State
CREATE procedure [dbo].[ModifyFailureState]
@Id int,
@FailureState nvarchar(20)
as
begin
	set nocount on;
	if @FailureState = 'Acknowledged'
		update dbo.SystemFailures set FailureState = @FailureState, AcknowledgedTimeStamp = GETDATE(), TimeStamp = GETDATE() where Id = @Id;
	else 
		update dbo.SystemFailures set FailureState = @FailureState, TimeStamp = GETDATE() where Id = @Id;
end
GO
/****** Object:  StoredProcedure [dbo].[PerformAlarmAction]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Perform Alarm Action */
CREATE procedure [dbo].[PerformAlarmAction]
@AlarmIdentifier nvarchar(50),
@ChangeStateAction nvarchar(50) = null,
@SupressNotificationAction varchar(10) = null
as
begin
	set nocount on;
	if @ChangeStateAction is null
		update dbo.AlarmTable set AudioNotificationAcknowledgement = 1, PopupNotificationAcknowledgement = 1 where AlarmId = @AlarmIdentifier;
	else
		update dbo.AlarmTable set AlarmState = @ChangeStateAction, TimeActionDone=GETDATE(), AudioNotificationAcknowledgement = 1, PopupNotificationAcknowledgement = 1  where AlarmId = @AlarmIdentifier;
end
GO
/****** Object:  StoredProcedure [dbo].[ReadFieldDeviceData]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* Reads the field device data*/
create procedure [dbo].[ReadFieldDeviceData]
@FieldDeviceIdentifier nvarchar(50),
@Current_R nvarchar(10) = null,
@Current_Y nvarchar(10) = null,
@Current_B nvarchar(10) = null,
@Voltage_1 nvarchar(10) = null,
@Voltage_2 nvarchar(10) = null,
@Voltage_3 nvarchar(10) = null,
@Vibration nvarchar(10) = null,
@startTime nvarchar(25),
@endTime nvarchar(25)
as
begin
	set nocount on;
	declare @query nvarchar(max);
	set @query = 'select ';
	if @Current_R is not null
		set @query = @query + 'Current_R as [I_R(A)],';
	if @Current_Y is not null
		set @query = @query + 'Current_Y as [I_Y(A)],';
	if @Current_B is not null
		set @query = @query + 'Current_B as [I_B(A)],';
	if @Voltage_1 is not null or @Voltage_2 is not null or @Voltage_3 is not null
		set @query = @query + 'dbo.GetAvgVoltage(Voltage_1, Voltage_2, Voltage_3) as [Vol_Avg(V)],';
	if @Vibration is not null
		set @query = @query + 'Vibration as [Vib(mm/s)],';
	set @query = @query + 'format(TimeStamp, ''HH:mm:ss dd-MM-yyyy'') as TIME from dbo.' + @FieldDeviceIdentifier + ' where TimeStamp between '''+ @startTime+''' and '''+ @endTime +''' order by TimeStamp';
	exec sp_executesql @query;
end
GO
/****** Object:  StoredProcedure [dbo].[SelectCommandPoints]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Fetches the command points for the FieldDevice */
CREATE procedure [dbo].[SelectCommandPoints]
@FieldDeviceIdentifier nvarchar(50)
as
	begin
	set nocount on;
	select * from dbo.FieldPoints 
	where SensorDataSetIdentifier = any(select SensorDataSetIdentifier from [dbo].CommandPoints where FieldDeviceIdentifier = @FieldDeviceIdentifier)
	and Label = any(select Label from [dbo].CommandPoints where FieldDeviceIdentifier = @FieldDeviceIdentifier);
end
GO
/****** Object:  StoredProcedure [dbo].[SelectFieldPoints]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Gets the Field Points for the SensorsDataSet*/
create procedure [dbo].[SelectFieldPoints]
@SensorDataSetIdentifier nvarchar(50)
as
begin
	set nocount on;
	select * from [dbo].[FieldPoints] where SensorDataSetIdentifier = @SensorDataSetIdentifier
end
GO
/****** Object:  StoredProcedure [dbo].[SelectSensorDataSet]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Gets the SensorsDataSet for the Field Device*/
create procedure [dbo].[SelectSensorDataSet]
@FieldDeviceIdentifier nvarchar(50)
as
begin
	set nocount on;
	select * from [dbo].[SensorsDataSet] where FieldDeviceIdentifier = @FieldDeviceIdentifier
end
GO
/****** Object:  StoredProcedure [dbo].[UpdateMaintenanceDate]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* Update Maintenance Date of Device */
create procedure [dbo].[UpdateMaintenanceDate]
@FieldDeviceIdentifier nvarchar(50),
@ReasonForMaintenance nvarchar(max),
@UpdatedMaintenanceDate datetime
as
begin
	set nocount on;
	insert into dbo.DeviceMaintenanceInfo values(@FieldDeviceIdentifier, @ReasonForMaintenance, @UpdatedMaintenanceDate);
end
GO
/****** Object:  StoredProcedure [dbo].[UpdateSystemAlarm]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Update SystemAlarm
create procedure [dbo].[UpdateSystemAlarm]
@Id int,
@State nvarchar(20),
@TimeStamp datetime,
@RaisedTimeStamp datetime,
@AcknowledgedTimeStamp datetime
as
begin
	set nocount on;
	update dbo.SystemAlarms set State = @State, TimeStamp = @TimeStamp, RaisedTimeStamp = @RaisedTimeStamp, AcknowledgedTimeStamp = @AcknowledgedTimeStamp
	where Id = @Id;
end
GO
/****** Object:  StoredProcedure [dbo].[UpdateSystemAlarmPolicyAndAlarmParameters]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--update SystemAlarmPolicy and SystemAlarmParameters
create procedure [dbo].[UpdateSystemAlarmPolicyAndAlarmParameters]
@Id int,
@PolicyType nvarchar(10),
@Title nvarchar(max),
@Message nvarchar(max),
@Status bit,
@ParametersType nvarchar(50),
@RatedValue float,
@VariationPercentage float,
@VariationType nvarchar(10),
@UpperLimit float,
@LowerLimit float
as
begin
	set nocount on;
	update dbo.SystemAlarmPolicies set PolicyType = @PolicyType, Title = @Title, Message = @Message, Status = @Status where Id = @Id;
	update dbo.SystemAlarmParameters set ParametersType = @ParametersType, RatedValue = @RatedValue, VariationPercentage = @VariationPercentage, 
		VariationType = @VariationType, UpperLimit = @UpperLimit, LowerLimit = @LowerLimit where Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[UpdateSystemFailurePolicy]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--update SystemFailurePolicy
create procedure [dbo].[UpdateSystemFailurePolicy]
@Id int,
@TargetValue nvarchar(10),
@Title nvarchar(max),
@Message nvarchar(max),
@TroubleShootMessage nvarchar(max),
@FailureResourceType nvarchar(20),
@Status bit
as
begin
	set nocount on;
	update dbo.SystemFailurePolicies set TargetValue = @TargetValue, Title = @Title, Message = @Message, TroubleShootMessage = @TroubleShootMessage,
	FailureResourceType = @FailureResourceType, Status = @Status where Id = @Id;
end
GO
/****** Object:  StoredProcedure [dbo].[UploadPdf]    Script Date: 20-08-2020 10:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* Upload the pdf to database */
create procedure [dbo].[UploadPdf]
@Identifier nvarchar(50),
@Name nvarchar(50),
@Content varbinary(max)
as
begin
	set nocount on;
	insert into dbo.PdfFiles values(@Identifier, @Name, @Content, CAST(GETDATE() as date));
end
GO
USE [master]
GO
ALTER DATABASE [Kabra] SET  READ_WRITE 
GO
