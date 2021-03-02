/* Tags for Field Points of Equipments connected to Reactor */
delete from dbo.CommandPoints where Label in (select Label from dbo.FieldPoints where TypeOfAddress='FieldPoint')
delete from dbo.FieldPoints where TypeOfAddress='FieldPoint'
/* Tags for Reactor 1 */
	
INSERT [dbo].[FieldPoints] VALUES (N'BatchName', N'BatchName', N'FieldPoint', N'R1.BatchText_1', N'string', N'false', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'BatchNumber', N'BatchNumber', N'FieldPoint', N'R1.BatchText_2', N'string', N'false', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'BatchRemarks', N'BatchRemarks', N'FieldPoint', N'R1.BatchText_3', N'string', N'false', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'BatchScientistName', N'BatchScientistName', N'FieldPoint', N'R1.BatchText_5', N'string', N'false', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'BatchStatus', N'BatchStatus', N'FieldPoint', N'R1.BatchText_4', N'string', N'false', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'EmergencyStatus', N'EmergencyStatus', N'FieldPoint', N'R1.EmergencyStatus', N'bool', N'false', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'HeatCoolFailure', N'HeatCoolFailure', N'FieldPoint', N'R1.HeatCoolFailure', N'bool', N'false', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'HeatCoolSetPoint', N'HeatCoolSetPoint', N'FieldPoint', N'R1.HeatCoolSetPoint', N'float', N'true', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'HeatCoolStatus', N'HeatCoolStatus', N'FieldPoint', N'R1.HeatCoolStatus', N'bool', N'false', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'HeatCoolStatusFeedback', N'HeatCoolStatusFeedback', N'FieldPoint', N'R1.HeatCoolStatusFeedback', N'bool', N'false', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'JacketOutletTemperature', N'JacketOutletTemperature', N'FieldPoint', N'R1.JacketOutletTemperature', N'float', N'true', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'ReactorpH', N'ReactorpH', N'FieldPoint', N'R1.PHvalue', N'float', N'true', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'ReactorPressure', N'ReactorPressure', N'FieldPoint', N'R1.Pressure', N'float', N'true', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'ReactorMass', N'ReactorMass', N'FieldPoint', N'R1.ReactorMass', N'float', N'true', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'ReactorMassTemperature', N'ReactorMassTemperature', N'FieldPoint', N'R1.ReactorMassTemperature', N'float', N'true', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'RPM', N'RPM', N'FieldPoint', N'R1.StirrerCurrentSpeed', N'int', N'true', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'RunningBatchStatus', N'RunningBatchStatus', N'FieldPoint', N'R1.RunningBatchStatus', N'bool', N'false', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'ScrubberpH', N'ScrubberpH', N'FieldPoint', N'R1.ScrubberPh', N'float', N'false', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'StirrerCurrentSpeed', N'StirrerCurrentSpeed', N'FieldPoint', N'R1.StirrerCurrentSpeed', N'int', N'false', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'StirrerFailure', N'StirrerFailure', N'FieldPoint', N'R1.StirrerFailure', N'bool', N'false', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'StirrerSpeedSetPoint', N'StirrerSpeedSetPoint', N'FieldPoint', N'R1.StirrerSetPoint', N'int', N'false', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'StirrerStatus', N'StirrerStatus', N'FieldPoint', N'R1.StirrerStatus', N'bool', N'false', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'StirrerStatusFeedback', N'StirrerStatusFeedback', N'FieldPoint', N'R1.StirrerStatusFeedback', N'bool', N'false', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'UsedNow', N'Reactor_1|UsedNow', N'FieldPoint', N'R1.UsedNow', N'bool', N'false', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'VapourTemperature', N'VapourTemperature', N'FieldPoint', N'R1.VapourTemperature', N'float', N'true', N'true', N'sensorDataSet_1', N'Reactor_1')
INSERT [dbo].[FieldPoints] VALUES (N'VentTemperature', N'VentTemperature', N'FieldPoint', N'R1.VentTemperature', N'float', N'true', N'true', N'sensorDataSet_1', N'Reactor_1')

insert into dbo.CommandPoints select Label, SensorDataSetIdentifier, FieldDeviceIdentifier from dbo.FieldPoints where Label in ('HeatCoolStatus', 'StirrerStatus', 'EmergencyStatus')

