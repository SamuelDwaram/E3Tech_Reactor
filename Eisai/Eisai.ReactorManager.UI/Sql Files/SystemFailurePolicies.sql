delete from dbo.SystemAlarms where SystemFailureId>0
delete from dbo.SystemFailures
delete from dbo.SystemFailurePolicies
DBCC CHECKIDENT('SystemFailurePolicies', RESEED, 0);

INSERT [dbo].[SystemFailurePolicies] ([DeviceId], [DeviceLabel], [FailedResourceLabel], [TargetValue], [Title], [Message], [TroubleShootMessage], [FailureResourceType], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_1', N'RD/HBR-11', N'HeatCoolFailure', N'True', N'HC failure', N'', N'Please check connections', N'Device', 1, CAST(N'2020-05-19T09:34:48.877' AS DateTime))
GO	

INSERT [dbo].[SystemFailurePolicies] ([DeviceId], [DeviceLabel], [FailedResourceLabel], [TargetValue], [Title], [Message], [TroubleShootMessage], [FailureResourceType], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_2', N'RD/HBR-09', N'HeatCoolFailure', N'True', N'HC failure', N'', N'Please check connections', N'Device', 1, CAST(N'2020-05-19T09:34:48.877' AS DateTime))
GO	

INSERT [dbo].[SystemFailurePolicies] ([DeviceId], [DeviceLabel], [FailedResourceLabel], [TargetValue], [Title], [Message], [TroubleShootMessage], [FailureResourceType], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_3', N'RD/HBR-01', N'HeatCoolFailure', N'True', N'HC failure', N'', N'Please check connections', N'Device', 1, CAST(N'2020-05-19T09:34:48.877' AS DateTime))
GO	

INSERT [dbo].[SystemFailurePolicies] ([DeviceId], [DeviceLabel], [FailedResourceLabel], [TargetValue], [Title], [Message], [TroubleShootMessage], [FailureResourceType], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_4', N'RD/HBR-02', N'HeatCoolFailure', N'True', N'HC failure', N'', N'Please check connections', N'Device', 1, CAST(N'2020-05-19T09:34:48.877' AS DateTime))
GO	

INSERT [dbo].[SystemFailurePolicies] ([DeviceId], [DeviceLabel], [FailedResourceLabel], [TargetValue], [Title], [Message], [TroubleShootMessage], [FailureResourceType], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_5', N'RD/HBR-07', N'HeatCoolFailure', N'True', N'HC failure', N'', N'Please check connections', N'Device', 1, CAST(N'2020-05-19T09:34:48.877' AS DateTime))
GO

INSERT [dbo].[SystemFailurePolicies] ([DeviceId], [DeviceLabel], [FailedResourceLabel], [TargetValue], [Title], [Message], [TroubleShootMessage], [FailureResourceType], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_6', N'RD/HBR-14', N'HeatCoolFailure', N'True', N'HC failure', N'', N'Please check connections', N'Device', 1, CAST(N'2020-05-19T09:34:48.877' AS DateTime))
GO	

INSERT [dbo].[SystemFailurePolicies] ([DeviceId], [DeviceLabel], [FailedResourceLabel], [TargetValue], [Title], [Message], [TroubleShootMessage], [FailureResourceType], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_1', N'VFD-1', N'StirrerFailure', N'True', N'Stirrer failure', N'', N'Please check connections', N'Device', 1, CAST(N'2020-05-19T09:34:48.877' AS DateTime))
GO	

INSERT [dbo].[SystemFailurePolicies] ([DeviceId], [DeviceLabel], [FailedResourceLabel], [TargetValue], [Title], [Message], [TroubleShootMessage], [FailureResourceType], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_2', N'VFD-2', N'StirrerFailure', N'True', N'Stirrer failure', N'', N'Please check connections', N'Device', 1, CAST(N'2020-05-19T09:34:48.877' AS DateTime))
GO	

INSERT [dbo].[SystemFailurePolicies] ([DeviceId], [DeviceLabel], [FailedResourceLabel], [TargetValue], [Title], [Message], [TroubleShootMessage], [FailureResourceType], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_3', N'VFD-3', N'StirrerFailure', N'True', N'Stirrer failure', N'', N'Please check connections', N'Device', 1, CAST(N'2020-05-19T09:34:48.877' AS DateTime))
GO	

INSERT [dbo].[SystemFailurePolicies] ([DeviceId], [DeviceLabel], [FailedResourceLabel], [TargetValue], [Title], [Message], [TroubleShootMessage], [FailureResourceType], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_4', N'VFD-4', N'StirrerFailure', N'True', N'Stirrer failure', N'', N'Please check connections', N'Device', 1, CAST(N'2020-05-19T09:34:48.877' AS DateTime))
GO	

INSERT [dbo].[SystemFailurePolicies] ([DeviceId], [DeviceLabel], [FailedResourceLabel], [TargetValue], [Title], [Message], [TroubleShootMessage], [FailureResourceType], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_5', N'VFD-5', N'StirrerFailure', N'True', N'Stirrer failure', N'', N'Please check connections', N'Device', 1, CAST(N'2020-05-19T09:34:48.877' AS DateTime))
GO

INSERT [dbo].[SystemFailurePolicies] ([DeviceId], [DeviceLabel], [FailedResourceLabel], [TargetValue], [Title], [Message], [TroubleShootMessage], [FailureResourceType], [Status], [CreatedTimeStamp]) 
VALUES (N'Reactor_6', N'VFD-6', N'StirrerFailure', N'True', N'Stirrer failure', N'', N'Please check connections', N'Device', 1, CAST(N'2020-05-19T09:34:48.877' AS DateTime))
GO	
