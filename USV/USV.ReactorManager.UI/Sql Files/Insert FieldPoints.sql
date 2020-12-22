use USV_ReactorDB
go

/* Tags for Reactor 1 */
	insert into dbo.FieldPoints values('UsedNow', 'Reactor_1|UsedNow', 'FieldPoint', 'R1.UsedNow', 'bool', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('RunningBatchStatus', 'RunningBatchStatus', 'FieldPoint', 'R1.RunningBatchStatus', 'bool', 'false', 'true', 'sensorDataSet_1')

	insert into dbo.FieldPoints values('StirrerStatus', 'StirrerStatus', 'FieldPoint', 'R1.StirrerStatus', 'bool', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('StirrerSpeedSetPoint', 'StirrerSpeedSetPoint', 'FieldPoint', 'R1.StirrerSpeedSetPoint', 'int', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('StirrerStatusFeedback', 'StirrerStatusFeedback', 'FieldPoint', 'R1.StirrerStatusFeedback', 'bool', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('StirrerCurrentSpeed', 'StirrerCurrentSpeed', 'FieldPoint', 'R1.StirrerCurrentSpeed', 'int', 'false', 'true', 'sensorDataSet_1')

	insert into dbo.FieldPoints values('ReactorMassTemperature', 'ReactorMassTemperature', 'FieldPoint', 'R1.ReactorMassTemperature', 'float', 'true', 'true', 'sensorDataSet_1')

	insert into dbo.FieldPoints values('EmergencyStatus', 'EmergencyStatus', 'FieldPoint', 'R1.EmergencyStatus', 'bool', 'false', 'true', 'sensorDataSet_1')
	
	insert into dbo.FieldPoints values('BatchName', 'BatchName', 'FieldPoint', 'R1.BatchText_1', 'string', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('BatchNumber', 'BatchNumber', 'FieldPoint', 'R1.BatchText_2', 'string', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('BatchRemarks', 'BatchRemarks', 'FieldPoint', 'R1.BatchText_3', 'string', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('BatchStatus', 'BatchStatus', 'FieldPoint', 'R1.BatchText_4', 'string', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('BatchScientistName', 'BatchScientistName', 'FieldPoint', 'R1.BatchText_5', 'string', 'false', 'true', 'sensorDataSet_1')

	insert into dbo.FieldPoints values('RemoteModeSelection', 'RemoteModeSelection', 'FieldPoint', 'R1.RemoteModeSelection', 'bool', 'false', 'true', 'sensorDataSet_1')
	
	insert into dbo.FieldPoints values('DosingPumpStatus_1', 'DosingPumpStatus_1', 'FieldPoint', 'R1.DosingPumpStatus_1', 'bool', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('DosingPumpFlowSetPoint_1', 'DosingPumpFlowSetPoint_1', 'FieldPoint', 'R1.DosingPumpFlowSetPoint_1', 'float', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('DosingPumpStatusFeedback_1', 'DosingPumpStatusFeedback_1', 'FieldPoint', 'R1.DosingPumpStatusFeedback_1', 'bool', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('DosingPumpCurrentDosingFlow_1', 'DosingPumpCurrentDosingFlow_1', 'FieldPoint', 'R1.DosingPumpCurrentDosingFlow_1', 'float', 'false', 'true', 'sensorDataSet_1')

	insert into dbo.FieldPoints values('DosingPumpStatus_2', 'DosingPumpStatus_2', 'FieldPoint', 'R1.DosingPumpStatus_2', 'bool', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('DosingPumpStatusFeedback_2', 'DosingPumpStatusFeedback_2', 'FieldPoint', 'R1.DosingPumpStatusFeedback_2', 'bool', 'false', 'true', 'sensorDataSet_1')

	insert into dbo.FieldPoints values('DosingPumpStatus_3', 'DosingPumpStatus_3', 'FieldPoint', 'R1.DosingPumpStatus_3', 'bool', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('DosingPumpFlowSetPoint_3', 'DosingPumpFlowSetPoint_3', 'FieldPoint', 'R1.DosingPumpFlowSetPoint_3', 'float', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('DosingPumpStatusFeedback_3', 'DosingPumpStatusFeedback_3', 'FieldPoint', 'R1.DosingPumpStatusFeedback_3', 'bool', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('DosingPumpCurrentDosingFlow_3', 'DosingPumpCurrentDosingFlow_3', 'FieldPoint', 'R1.DosingPumpCurrentDosingFlow_3', 'float', 'false', 'true', 'sensorDataSet_1')
	
	insert into dbo.FieldPoints values('DosingPumpStatus_4', 'DosingPumpStatus_4', 'FieldPoint', 'R1.DosingPumpStatus_4', 'bool', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('DosingPumpStatusFeedback_4', 'DosingPumpStatusFeedback_4', 'FieldPoint', 'R1.DosingPumpStatusFeedback_4', 'bool', 'false', 'true', 'sensorDataSet_1')

	insert into dbo.FieldPoints values('DosingPumpTotalizerValue_1', 'DosingPumpTotalizerValue_1', 'FieldPoint', 'R1.DosingPumpTotalizerValue_1', 'float', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('DosingPumpTotalizerValue_3', 'DosingPumpTotalizerValue_3', 'FieldPoint', 'R1.DosingPumpTotalizerValue_3', 'float', 'false', 'true', 'sensorDataSet_1')

	insert into dbo.FieldPoints values('SovStatus_1', 'SovStatus_1', 'FieldPoint', 'R1.SovStatus_1', 'bool', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('SovStatus_2', 'SovStatus_2', 'FieldPoint', 'R1.SovStatus_2', 'bool', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('SovStatus_3', 'SovStatus_3', 'FieldPoint', 'R1.SovStatus_3', 'bool', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('SovStatus_4', 'SovStatus_4', 'FieldPoint', 'R1.SovStatus_4', 'bool', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('SovStatus_5', 'SovStatus_5', 'FieldPoint', 'R1.SovStatus_5', 'bool', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('SovStatus_6', 'SovStatus_6', 'FieldPoint', 'R1.SovStatus_6', 'bool', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('SovStatus_7', 'SovStatus_7', 'FieldPoint', 'R1.SovStatus_7', 'bool', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('SovStatus_8', 'SovStatus_8', 'FieldPoint', 'R1.SovStatus_8', 'bool', 'false', 'true', 'sensorDataSet_1')
	insert into dbo.FieldPoints values('SovStatus_9', 'SovStatus_9', 'FieldPoint', 'R1.SovStatus_9', 'bool', 'false', 'true', 'sensorDataSet_1')


