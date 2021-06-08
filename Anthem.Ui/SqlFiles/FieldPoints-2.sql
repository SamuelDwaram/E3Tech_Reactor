/* Tags for Field Points of Equipments connected to Reactor */
delete from dbo.CommandPoints
delete from dbo.FieldPoints

/*insert into dbo.FieldPoints values('', '', '', '', '', '', '', '', '')*/

/* Tags for Reactor 1 */
	insert into dbo.FieldPoints values('UsedNow', 'Reactor_1|UsedNow', 'FieldPoint', 'R1.UsedNow', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('EmergencyStatus', 'EmergencyStatus', 'FieldPoint', 'R1.EmergencyStatus', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('BatchName', 'BatchName', 'FieldPoint', 'R1.BatchText_1', 'string', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('BatchNumber', 'BatchNumber', 'FieldPoint', 'R1.BatchText_2', 'string', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('BatchRemarks', 'BatchRemarks', 'FieldPoint', 'R1.BatchText_3', 'string', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('BatchStatus', 'BatchStatus', 'FieldPoint', 'R1.BatchText_4', 'string', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('BatchScientistName', 'BatchScientistName', 'FieldPoint', 'R1.BatchText_5', 'string', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	
	insert into dbo.FieldPoints values('Test', 'Test', 'FieldPoint', 'R1.Test', 'int', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	
	insert into dbo.FieldPoints values('Valve_1', 'Valve_1', 'FieldPoint', 'IO.R1.VentValve', 'bool', 'true', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_2', 'Valve_2', 'FieldPoint', 'IO.R1.N2PurgeValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_3', 'Valve_3', 'FieldPoint', 'IO.R1.TransferValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_4', 'Valve_4', 'FieldPoint', 'IO.R2.VentValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_5', 'Valve_5', 'FieldPoint', 'IO.R2.N2PurgeValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_6', 'Valve_6', 'FieldPoint', 'IO.R2.FillInValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_7', 'Valve_7', 'FieldPoint', 'IO.R2.TransferValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_8', 'Valve_8', 'FieldPoint', 'IO.R3.VentValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_9', 'Valve_9', 'FieldPoint', 'IO.R3.N2PurgeValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_10', 'Valve_10', 'FieldPoint', 'IO.R3.FillInHeaderValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_11', 'Valve_11', 'FieldPoint', 'IO.R3.TransferValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_12', 'Valve_12', 'FieldPoint', 'IO.R4.VentValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_13', 'Valve_13', 'FieldPoint', 'IO.R4.N2PurgeValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_14', 'Valve_14', 'FieldPoint', 'IO.R4.FillInHeaderValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_15', 'Valve_15', 'FieldPoint', 'IO.R4.TransferValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	/*
	insert into dbo.FieldPoints values('Valve_16', 'Valve_16', 'FieldPoint', 'IO.AA1.N2PurgeValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_17', 'Valve_17', 'FieldPoint', 'IO.AA1.FillInValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_18', 'Valve_18', 'FieldPoint', 'IO.AA1.FillInHeaderValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_19', 'Valve_19', 'FieldPoint', 'IO.AA1.VentValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_20', 'Valve_20', 'FieldPoint', 'IO.AA1.TransferValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_21', 'Valve_21', 'FieldPoint', 'IO.AA2.VentValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_22', 'Valve_22', 'FieldPoint', 'IO.AA2.FillInHeaderValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_23', 'Valve_23', 'FieldPoint', 'IO.AA2.FillInValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_24', 'Valve_24', 'FieldPoint', 'IO.AA2.N2PurgeValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_25', 'Valve_25', 'FieldPoint', 'IO.AA2.TransferValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_26', 'Valve_26', 'FieldPoint', 'IO.MVA.FillInHeaderValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_27', 'Valve_27', 'FieldPoint', 'IO.MVA.FlushingValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_28', 'Valve_28', 'FieldPoint', 'IO.MVA.VentValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_29', 'Valve_29', 'FieldPoint', 'IO.MVA.TransferValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_30', 'Valve_30', 'FieldPoint', 'IO.CommonDrainValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_31', 'Valve_31', 'FieldPoint', 'IO.MVA.TransferValve_2', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_32', 'Valve_32', 'FieldPoint', 'IO.DCM.FillInValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_33', 'Valve_33', 'FieldPoint', 'IO.DCM.VentValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_34', 'Valve_34', 'FieldPoint', 'IO.DCM.N2PurgeValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_35', 'Valve_35', 'FieldPoint', 'IO.DCM.TransferValve_1', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_36', 'Valve_36', 'FieldPoint', 'IO.DCM.TransferValve_2', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_37', 'Valve_37', 'FieldPoint', 'IO.RV.FlushingValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_38', 'Valve_38', 'FieldPoint', 'IO.RV.FillInHeaderValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_39', 'Valve_39', 'FieldPoint', 'IO.RV.TransferValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_40', 'Valve_40', 'FieldPoint', 'IO.SS.TransferValve_1', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_41', 'Valve_41', 'FieldPoint', 'IO.SS.TransferValve_2', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_42', 'Valve_42', 'FieldPoint', 'IO.RV.VentValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_43', 'Valve_43', 'FieldPoint', 'IO.MVB.TransferValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_44', 'Valve_44', 'FieldPoint', 'IO.MVB.FillInHeaderValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_45', 'Valve_45', 'FieldPoint', 'IO.MVB.N2PurgeValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_46', 'Valve_46', 'FieldPoint', 'IO.MVB.VentValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_47', 'Valve_47', 'FieldPoint', 'IO.R5.TransferValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_48', 'Valve_48', 'FieldPoint', 'IO.R5.FillInHeaderValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_49', 'Valve_49', 'FieldPoint', 'IO.R5.N2PurgeValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_50', 'Valve_50', 'FieldPoint', 'R5.VentValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_51', 'Valve_51', 'FieldPoint', 'IO.MVA.N2PurgeValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_52', 'Valve_52', 'FieldPoint', 'IO.RV.N2PurgeValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	*/
	insert into dbo.FieldPoints values('Valve_53', 'Valve_53', 'FieldPoint', 'IO.R1.FillInValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Valve_54', 'Valve_54', 'FieldPoint', 'IO.R3.FillInValve', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')

	insert into dbo.FieldPoints values('Pump_1', 'Pump_1', 'FieldPoint', 'IO.R1.TransferPump', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Pump_2', 'Pump_2', 'FieldPoint', 'IO.R2.TransferPump', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Pump_3', 'Pump_3', 'FieldPoint', 'IO.R3.TransferPump', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Pump_4', 'Pump_4', 'FieldPoint', 'IO.R4.TransferPump', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	/*
	insert into dbo.FieldPoints values('Pump_5', 'Pump_5', 'FieldPoint', 'IO.MVA.TransferPump', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Pump_6', 'Pump_6', 'FieldPoint', 'IO.DCM.TransferPump', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Pump_7', 'Pump_7', 'FieldPoint', 'IO.SS.TransferPump', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	insert into dbo.FieldPoints values('Pump_8', 'Pump_8', 'FieldPoint', 'IO.MVB.TransferPump', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
	*/
	insert into dbo.FieldPoints values('Pump_9', 'Pump_9', 'FieldPoint', 'IO.R5.TransferPump', 'bool', 'false', 'true', 'sensorDataSet_1', 'Reactor_1')
		insert into dbo.CommandPoints select Label, SensorDataSetIdentifier, FieldDeviceIdentifier from dbo.FieldPoints where CHARINDEX('test', Label) > 0

	insert into dbo.CommandPoints select Label, SensorDataSetIdentifier, FieldDeviceIdentifier from dbo.FieldPoints where CHARINDEX('Valve_', Label) > 0 or CHARINDEX('Pump_', Label) > 0
