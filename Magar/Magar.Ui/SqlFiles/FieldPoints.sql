use Magar
go

delete from dbo.LiveData
delete from dbo.FieldPoints

INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_1', N'27(P)', N'FieldPoint', N'40000|1', N'float', N'false', N'false', N'sensorsDataSet_1', N'600', NULL, 9.7, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_10', N'27(S)', N'FieldPoint', N'40009|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_11', N'28(C)', N'FieldPoint', N'40010|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_12', N'M/E NC P', N'FieldPoint', N'40011|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_13', N'33(P)', N'FieldPoint', N'40012|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_14', N'Fuel Oil S', N'FieldPoint', N'40013|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_15', N'2(P)', N'FieldPoint', N'40014|1', N'float', N'false', N'false', N'sensorsDataSet_1', N'600', NULL, 9.7, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_16', N'2(S)', N'FieldPoint', N'40015|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_17', N'7(S)', N'FieldPoint', N'40016|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_18', N'M/E NC S', N'FieldPoint', N'40017|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_19', N'33(S)', N'FieldPoint', N'40018|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_2', N'Fuel Oil Settling Tank', N'FieldPoint', N'40001|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_20', N'9(P)', N'FieldPoint', N'40019|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_21', N'3(P)', N'FieldPoint', N'40020|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_22', N'3(S)', N'FieldPoint', N'40021|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_23', N'M/E F/W P', N'FieldPoint', N'40022|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_24', N'17(C)', N'FieldPoint', N'40023|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_25', N'15(P)', N'FieldPoint', N'40024|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_26', N'6(C)', N'FieldPoint', N'40025|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_27', N'14(C)', N'FieldPoint', N'40026|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_28', N'500KW P', N'FieldPoint', N'40027|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_29', N'M/E F/W S', N'FieldPoint', N'40028|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_3', N'Fuel Oil Ru Tank Common', N'FieldPoint', N'40002|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_30', N'15(S)', N'FieldPoint', N'40029|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_31', N'8 AR', N'FieldPoint', N'40030|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_32', N'10 AR', N'FieldPoint', N'40031|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_33', N'500KW S', N'FieldPoint', N'40032|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_34', N'23(C)', N'FieldPoint', N'40033|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_35', N'Fuel Oil P', N'FieldPoint', N'40034|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_36', N'Tank 36', N'FieldPoint', N'40035|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_37', N'Tank 37', N'FieldPoint', N'40036|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_38', N'Tank 38', N'FieldPoint', N'40037|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_39', N'Tank 39', N'FieldPoint', N'40038|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_4', N'Tank 4', N'FieldPoint', N'40003|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_40', N'Tank 40', N'FieldPoint', N'40039|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_5', N'Tank 5', N'FieldPoint', N'40004|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_6', N'Tank 6', N'FieldPoint', N'40005|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_7', N'Tank 7', N'FieldPoint', N'40006|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_8', N'Tank 8', N'FieldPoint', N'40007|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
INSERT [dbo].[FieldPoints] ([Label], [Description], [TypeOfAddress], [MemoryAddress], [FieldPointDataType], [ToBeLogged], [RequireNotificationService], [SensorDataSetIdentifier], [MaxValue], [Offset], [Multiplier], [SourceControllerIdentifier], [FieldDeviceIdentifier]) VALUES (N'Tank_9', N'Tank 9', N'FieldPoint', N'40008|1', N'float', N'false', N'false', N'sensorsDataSet_1', NULL, NULL, NULL, N'Slave_1', N'Device_1')
GO
