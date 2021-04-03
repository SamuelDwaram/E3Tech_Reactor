delete from dbo.CommandPoints where Label in (select Label from dbo.FieldPoints where TypeOfAddress='Ramp')
delete from dbo.FieldPoints where TypeOfAddress='Ramp' and FieldDeviceIdentifier='Reactor_1'

--insert into dbo.FieldPoints values('', '', 'Ramp', 'R1.', '', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');	
/* Tags for End Block(Recipe Status, 'Reactor_1') */
	insert into dbo.FieldPoints values('Stirrer|Status', 'Stirrer|Status', 'Ramp', 'R1.StirrerRampStatus', 'bool', 'false', 'false', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Stirrer|Ended', 'Stirrer|Ended', 'Ramp', 'R1.StirrerRampEnded', 'bool', 'false', 'false', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Stirrer|NumberOfSteps', 'Stirrer|NumberOfSteps','Ramp', 'R1.StirrerRampNumberOfSteps', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Stirrer|CurrentStep', 'Stirrer|CurrentStep', 'Ramp', 'R1.StirrerRampstep', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|SkipStep', 'Stirrer|SkipStep', 'Ramp', 'R1.StirrerRampStepSkipStatus', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|ClearRamp', 'Stirrer|ClearRamp', 'Ramp', 'R1.StirrerRampClearStatus', 'bool', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|RemainingTime', 'Stirrer|RemainingTime', 'Ramp', 'R1.StirrerRampRemainingTime', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	
	insert into dbo.FieldPoints values('Stirrer|StartTime|0', 'Stirrer|StartTime|0', 'Ramp', 'R1.StirrerRampStartedTime[0]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|EndTime|0', 'Stirrer|EndTime|0', 'Ramp', 'R1.StirrerRampEndedTime[0]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|SetPoint|0', 'Stirrer|SetPoint|0', 'Ramp', 'R1.StirrerRampSetPoint[0]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|MinsToMaintain|0', 'Stirrer|MinsToMaintain|0', 'Ramp', 'R1.StirrerRampTime[0]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');

	insert into dbo.FieldPoints values('Stirrer|StartTime|1', 'Stirrer|StartTime|1', 'Ramp', 'R1.StirrerRampStartedTime[1]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|EndTime|1', 'Stirrer|EndTime|1', 'Ramp', 'R1.StirrerRampEndedTime[1]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|SetPoint|1', 'Stirrer|SetPoint|1', 'Ramp', 'R1.StirrerRampSetPoint[1]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|MinsToMaintain|1', 'Stirrer|MinsToMaintain|1', 'Ramp', 'R1.StirrerRampTime[1]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	
	insert into dbo.FieldPoints values('Stirrer|StartTime|2', 'Stirrer|StartTime|2', 'Ramp', 'R1.StirrerRampStartedTime[2]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|EndTime|2', 'Stirrer|EndTime|2', 'Ramp', 'R1.StirrerRampEndedTime[2]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|SetPoint|2', 'Stirrer|SetPoint|2', 'Ramp', 'R1.StirrerRampSetPoint[2]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|MinsToMaintain|2', 'Stirrer|MinsToMaintain|2', 'Ramp', 'R1.StirrerRampTime[2]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	
	insert into dbo.FieldPoints values('Stirrer|StartTime|3', 'Stirrer|StartTime|3', 'Ramp', 'R1.StirrerRampStartedTime[3]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|EndTime|3', 'Stirrer|EndTime|3', 'Ramp', 'R1.StirrerRampEndedTime[3]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|SetPoint|3', 'Stirrer|SetPoint|3', 'Ramp', 'R1.StirrerRampSetPoint[3]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|MinsToMaintain|3', 'Stirrer|MinsToMaintain|3', 'Ramp', 'R1.StirrerRampTime[3]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	
	insert into dbo.FieldPoints values('Stirrer|StartTime|4', 'Stirrer|StartTime|4', 'Ramp', 'R1.StirrerRampStartedTime[4]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|EndTime|4', 'Stirrer|EndTime|4', 'Ramp', 'R1.StirrerRampEndedTime[4]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|SetPoint|4', 'Stirrer|SetPoint|4', 'Ramp', 'R1.StirrerRampSetPoint[4]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|MinsToMaintain|4', 'Stirrer|MinsToMaintain|4', 'Ramp', 'R1.StirrerRampTime[4]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	
	insert into dbo.FieldPoints values('Stirrer|StartTime|5', 'Stirrer|StartTime|5', 'Ramp', 'R1.StirrerRampStartedTime[5]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|EndTime|5', 'Stirrer|EndTime|5', 'Ramp', 'R1.StirrerRampEndedTime[5]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|SetPoint|5', 'Stirrer|SetPoint|5', 'Ramp', 'R1.StirrerRampSetPoint[5]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|MinsToMaintain|5', 'Stirrer|MinsToMaintain|5', 'Ramp', 'R1.StirrerRampTime[5]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	
	insert into dbo.FieldPoints values('Stirrer|StartTime|6', 'Stirrer|StartTime|6', 'Ramp', 'R1.StirrerRampStartedTime[6]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|EndTime|6', 'Stirrer|EndTime|6', 'Ramp', 'R1.StirrerRampEndedTime[6]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|SetPoint|6', 'Stirrer|SetPoint|6', 'Ramp', 'R1.StirrerRampSetPoint[6]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|MinsToMaintain|6', 'Stirrer|MinsToMaintain|6', 'Ramp', 'R1.StirrerRampTime[6]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	
	insert into dbo.FieldPoints values('Stirrer|StartTime|7', 'Stirrer|StartTime|7', 'Ramp', 'R1.StirrerRampStartedTime[0]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|EndTime|7', 'Stirrer|EndTime|7', 'Ramp', 'R1.StirrerRampEndedTime[0]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|SetPoint|7', 'Stirrer|SetPoint|7', 'Ramp', 'R1.StirrerRampSetPoint[0]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Stirrer|MinsToMaintain|7', 'Stirrer|MinsToMaintain|7', 'Ramp', 'R1.StirrerRampTime[0]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	
	insert into dbo.FieldPoints values('Hc|Status', 'Hc|Status', 'Ramp', 'R1.HeatCoolRampStatus', 'bool', 'false', 'false', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Hc|Ended', 'Hc|Ended', 'Ramp', 'R1.HeatCoolRampEnded', 'bool', 'false', 'false', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Hc|NumberOfSteps', 'Hc|NumberOfSteps','Ramp', 'R1.HeatCoolRampNumberOfSteps', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Hc|CurrentStep', 'Hc|CurrentStep', 'Ramp', 'R1.HeatCoolRampstep', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|SkipStep', 'Hc|SkipStep', 'Ramp', 'R1.HeatCoolRampStepSkipStatus', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|ClearRamp', 'Hc|ClearRamp', 'Ramp', 'R1.HeatCoolRampClearStatus', 'bool', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|RemainingTime', 'Hc|RemainingTime', 'Ramp', 'R1.HeatCoolRampRemainingTime', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	
	insert into dbo.FieldPoints values('Hc|StartTime|0', 'Hc|StartTime|0', 'Ramp', 'R1.HeatCoolRampStartedTime[0]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|EndTime|0', 'Hc|EndTime|0', 'Ramp', 'R1.HeatCoolRampEndedTime[0]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|SetPoint|0', 'Hc|SetPoint|0', 'Ramp', 'R1.HeatCoolRampSetPoint[0]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|MinsToMaintain|0', 'Hc|MinsToMaintain|0', 'Ramp', 'R1.HeatCoolRampTime[0]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');

	insert into dbo.FieldPoints values('Hc|StartTime|1', 'Hc|StartTime|1', 'Ramp', 'R1.HeatCoolRampStartedTime[1]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|EndTime|1', 'Hc|EndTime|1', 'Ramp', 'R1.HeatCoolRampEndedTime[1]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|SetPoint|1', 'Hc|SetPoint|1', 'Ramp', 'R1.HeatCoolRampSetPoint[1]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|MinsToMaintain|1', 'Hc|MinsToMaintain|1', 'Ramp', 'R1.HeatCoolRampTime[1]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	
	insert into dbo.FieldPoints values('Hc|StartTime|2', 'Hc|StartTime|2', 'Ramp', 'R1.HeatCoolRampStartedTime[2]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|EndTime|2', 'Hc|EndTime|2', 'Ramp', 'R1.HeatCoolRampEndedTime[2]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|SetPoint|2', 'Hc|SetPoint|2', 'Ramp', 'R1.HeatCoolRampSetPoint[2]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|MinsToMaintain|2', 'Hc|MinsToMaintain|2', 'Ramp', 'R1.HeatCoolRampTime[2]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	
	insert into dbo.FieldPoints values('Hc|StartTime|3', 'Hc|StartTime|3', 'Ramp', 'R1.HeatCoolRampStartedTime[3]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|EndTime|3', 'Hc|EndTime|3', 'Ramp', 'R1.HeatCoolRampEndedTime[3]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|SetPoint|3', 'Hc|SetPoint|3', 'Ramp', 'R1.HeatCoolRampSetPoint[3]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|MinsToMaintain|3', 'Hc|MinsToMaintain|3', 'Ramp', 'R1.HeatCoolRampTime[3]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	
	insert into dbo.FieldPoints values('Hc|StartTime|4', 'Hc|StartTime|4', 'Ramp', 'R1.HeatCoolRampStartedTime[4]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|EndTime|4', 'Hc|EndTime|4', 'Ramp', 'R1.HeatCoolRampEndedTime[4]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|SetPoint|4', 'Hc|SetPoint|4', 'Ramp', 'R1.HeatCoolRampSetPoint[4]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|MinsToMaintain|4', 'Hc|MinsToMaintain|4', 'Ramp', 'R1.HeatCoolRampTime[4]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	
	insert into dbo.FieldPoints values('Hc|StartTime|5', 'Hc|StartTime|5', 'Ramp', 'R1.HeatCoolRampStartedTime[5]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|EndTime|5', 'Hc|EndTime|5', 'Ramp', 'R1.HeatCoolRampEndedTime[5]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|SetPoint|5', 'Hc|SetPoint|5', 'Ramp', 'R1.HeatCoolRampSetPoint[5]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|MinsToMaintain|5', 'Hc|MinsToMaintain|5', 'Ramp', 'R1.HeatCoolRampTime[5]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	
	insert into dbo.FieldPoints values('Hc|StartTime|6', 'Hc|StartTime|6', 'Ramp', 'R1.HeatCoolRampStartedTime[6]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|EndTime|6', 'Hc|EndTime|6', 'Ramp', 'R1.HeatCoolRampEndedTime[6]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|SetPoint|6', 'Hc|SetPoint|6', 'Ramp', 'R1.HeatCoolRampSetPoint[6]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|MinsToMaintain|6', 'Hc|MinsToMaintain|6', 'Ramp', 'R1.HeatCoolRampTime[6]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	
	insert into dbo.FieldPoints values('Hc|StartTime|7', 'Hc|StartTime|7', 'Ramp', 'R1.HeatCoolRampStartedTime[0]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|EndTime|7', 'Hc|EndTime|7', 'Ramp', 'R1.HeatCoolRampEndedTime[0]', 'string', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|SetPoint|7', 'Hc|SetPoint|7', 'Ramp', 'R1.HeatCoolRampSetPoint[0]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	insert into dbo.FieldPoints values('Hc|MinsToMaintain|7', 'Hc|MinsToMaintain|7', 'Ramp', 'R1.HeatCoolRampTime[0]', 'int', 'false', 'false', 'sensorDataSet_1', 'Reactor_1');
	
	-- Update command points in db
	insert into dbo.CommandPoints select Label, SensorDataSetIdentifier, FieldDeviceIdentifier from dbo.FieldPoints where TypeOfAddress='Ramp'
