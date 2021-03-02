delete from dbo.SystemAlarmParameters

INSERT [dbo].[SystemAlarmParameters] ([Id], [Name], [ParametersType], [RatedValue], [VariationPercentage], [VariationType], [UpperLimit], [LowerLimit]) 
VALUES (1, N'ReactorpH', N'Limits', 0, 0, N'Both', 0, 0)
GO
INSERT [dbo].[SystemAlarmParameters] ([Id], [Name], [ParametersType], [RatedValue], [VariationPercentage], [VariationType], [UpperLimit], [LowerLimit])
VALUES (2, N'ScrubberpH', N'Limits', 0, 0, N'Both', 0, 0)
GO
INSERT [dbo].[SystemAlarmParameters] ([Id], [Name], [ParametersType], [RatedValue], [VariationPercentage], [VariationType], [UpperLimit], [LowerLimit])
VALUES (3, N'ReactorPressure', N'Limits', 0, 0, N'Both', 0, 0)
GO
INSERT [dbo].[SystemAlarmParameters] ([Id], [Name], [ParametersType], [RatedValue], [VariationPercentage], [VariationType], [UpperLimit], [LowerLimit])
VALUES (4, N'VentTemperature', N'Limits', 0, 0, N'Both', 30, 25)
GO
INSERT [dbo].[SystemAlarmParameters] ([Id], [Name], [ParametersType], [RatedValue], [VariationPercentage], [VariationType], [UpperLimit], [LowerLimit])
VALUES (5, N'ReactorMassTemperature', N'Limits', 0, 0, N'Both', 30, 25)
GO
