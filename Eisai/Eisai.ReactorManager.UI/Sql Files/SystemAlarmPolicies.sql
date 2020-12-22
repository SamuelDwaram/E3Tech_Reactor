USE [EisaiReactorDB]
GO

delete from dbo.SystemAlarms
delete from dbo.SystemAlarmParameters
delete from dbo.SystemAlarmPolicies
DBCC CHECKIDENT('SystemAlarmPolicies', RESEED, 0);
DBCC CHECKIDENT('SystemAlarms', RESEED, 0);

--Reactor_1
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_1', N'RD/GSA-01 20L', N'Individual', N'High&Low& Jacket Temp', N'', 0, CAST(N'2020-07-14T12:52:29.310' AS DateTime))
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_1', N'RD/GSA-01 20L', N'Individual', N'High&Low& pH', N'', 0, CAST(N'2020-07-14T12:52:29.313' AS DateTime))
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp])
VALUES (N'Reactor_1', N'RD/GSA-01 20L', N'Individual', N'High&Low& Pressure', N'', 0, CAST(N'2020-07-14T12:52:29.317' AS DateTime))
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp])
VALUES (N'Reactor_1', N'RD/GSA-01 20L', N'Individual', N'High&Low& MassTemp', N'', 1, CAST(N'2020-07-14T12:52:29.317' AS DateTime))
GO

--Reactor_2
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_2', N'RD/GSA-02 30L', N'Individual', N'High&Low& Jacket Temp', N'', 0, CAST(N'2020-07-14T12:52:29.310' AS DateTime))
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_2', N'RD/GSA-02 30L', N'Individual', N'High&Low& pH', N'', 0, CAST(N'2020-07-14T12:52:29.313' AS DateTime))
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp])
VALUES (N'Reactor_2', N'RD/GSA-02 30L', N'Individual', N'High&Low& Pressure', N'', 0, CAST(N'2020-07-14T12:52:29.317' AS DateTime))
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp])
VALUES (N'Reactor_2', N'RD/GSA-02 30L', N'Individual', N'High&Low& MassTemp', N'', 1, CAST(N'2020-07-14T12:52:29.317' AS DateTime))
GO

--Reactor_3
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_3', N'RD/GSA-03 100L', N'Individual', N'High&Low& Jacket Temp', N'', 0, CAST(N'2020-07-14T12:52:29.310' AS DateTime))
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_3', N'RD/GSA-03 100L', N'Individual', N'High&Low& pH', N'', 0, CAST(N'2020-07-14T12:52:29.313' AS DateTime))
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp])
VALUES (N'Reactor_3', N'RD/GSA-03 100L', N'Individual', N'High&Low& Pressure', N'', 0, CAST(N'2020-07-14T12:52:29.317' AS DateTime))
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp])
VALUES (N'Reactor_3', N'RD/GSA-03 100L', N'Individual', N'High&Low& MassTemp', N'', 1, CAST(N'2020-07-14T12:52:29.317' AS DateTime))
GO

--Reactor_4
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_4', N'RD/GSA-04 50L', N'Individual', N'High&Low& Jacket Temp', N'', 0, CAST(N'2020-07-14T12:52:29.310' AS DateTime))
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_4', N'RD/GSA-04 50L', N'Individual', N'High&Low& pH', N'', 0, CAST(N'2020-07-14T12:52:29.313' AS DateTime))
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp])
VALUES (N'Reactor_4', N'RD/GSA-04 50L', N'Individual', N'High&Low& Pressure', N'', 0, CAST(N'2020-07-14T12:52:29.317' AS DateTime))
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp])
VALUES (N'Reactor_4', N'RD/GSA-04 50L', N'Individual', N'High&Low& MassTemp', N'', 1, CAST(N'2020-07-14T12:52:29.317' AS DateTime))
GO

--Reactor_5
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_5', N'RD/GSA-05 10L', N'Individual', N'High&Low& Jacket Temp', N'', 0, CAST(N'2020-07-14T12:52:29.310' AS DateTime))
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_5', N'RD/GSA-05 10L', N'Individual', N'High&Low& pH', N'', 0, CAST(N'2020-07-14T12:52:29.313' AS DateTime))
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp])
VALUES (N'Reactor_5', N'RD/GSA-05 10L', N'Individual', N'High&Low& Pressure', N'', 0, CAST(N'2020-07-14T12:52:29.317' AS DateTime))
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp])
VALUES (N'Reactor_5', N'RD/GSA-05 10L', N'Individual', N'High&Low& MassTemp', N'', 1, CAST(N'2020-07-14T12:52:29.317' AS DateTime))
GO

--Reactor_6
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_6', N'RD/GSA-06 30L', N'Individual', N'High&Low& Jacket Temp', N'', 0, CAST(N'2020-07-14T12:52:29.310' AS DateTime))
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_6', N'RD/GSA-06 30L', N'Individual', N'High&Low& pH', N'', 0, CAST(N'2020-07-14T12:52:29.313' AS DateTime))
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp])
VALUES (N'Reactor_6', N'RD/GSA-06 30L', N'Individual', N'High&Low& Pressure', N'', 0, CAST(N'2020-07-14T12:52:29.317' AS DateTime))
GO
INSERT [dbo].[SystemAlarmPolicies] ([DeviceId], [DeviceLabel], [PolicyType], [Title], [Message], [Status], [CreatedTimeStamp])
VALUES (N'Reactor_6', N'RD/GSA-06 30L', N'Individual', N'High&Low& MassTemp', N'', 1, CAST(N'2020-07-14T12:52:29.317' AS DateTime))
GO
