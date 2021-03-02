delete from dbo.SystemAlarms
delete from dbo.SystemAlarmParameters
delete from dbo.SystemAlarmPolicies
DBCC CHECKIDENT('SystemAlarmPolicies', RESEED, 0);
DBCC CHECKIDENT('SystemAlarms', RESEED, 0);

--Reactor_1
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_1', N'ATR-30L', N'Individual', N'High&Low& Reactor pH', N'', 0, GETDATE())
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_1', N'ATR-30L', N'Individual', N'High&Low& Scrubber pH', N'', 0, GETDATE())
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp])
VALUES (N'Reactor_1', N'ATR-30L', N'Individual', N'High&Low& Reactor Pressure', N'', 0, GETDATE())
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp])
VALUES (N'Reactor_1', N'ATR-30L', N'Individual', N'High&Low& Vent Temp', N'', 0, GETDATE())
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp])
VALUES (N'Reactor_1', N'ATR-30L', N'Individual', N'High&Low& Mass Temp', N'', 0, GETDATE())
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp])
VALUES (N'Reactor_1', N'ATR-30L', N'Individual', N'High&Low& Mass Temp', N'', 1, GETDATE())
GO
