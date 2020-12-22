use test_db
go

--#region [Field Points for each Field Device]
/*
Current_R float,
Current_Y float,
Current_B float,
Voltage_1 float,
Voltage_2 float,
Voltage_3 float,
Vibration float,
TimeStamp datetime
*/
--#endregion

insert into dbo.FieldPoints values('Current_R', 'Current_R', 'FieldPoint', '30006|2', 'float', 'true', 'false', 'currentSensors_1', null, null, 1.73, 'ModbusRtuController_11', 'Motor_1')
insert into dbo.FieldPoints values('Current_Y', 'Current_Y', 'FieldPoint', '30008|2', 'float', 'true', 'false', 'currentSensors_1', null, null, 1.73, 'ModbusRtuController_11', 'Motor_1')
insert into dbo.FieldPoints values('Current_B', 'Current_B', 'FieldPoint', '30010|2', 'float', 'true', 'false', 'currentSensors_1', null, null, 1.73, 'ModbusRtuController_11', 'Motor_1')
insert into dbo.FieldPoints values('Voltage_1', 'Voltage_1', 'FieldPoint', '30000|2', 'float', 'true', 'false', 'voltageSensors_1', null, null, null, 'ModbusRtuController_11', 'Motor_1')
insert into dbo.FieldPoints values('Voltage_2', 'Voltage_2', 'FieldPoint', '30002|2', 'float', 'true', 'false', 'voltageSensors_1', null, null, null, 'ModbusRtuController_11', 'Motor_1')
insert into dbo.FieldPoints values('Voltage_3', 'Voltage_3', 'FieldPoint', '30004|2', 'float', 'true', 'false', 'voltageSensors_1', null, null, null, 'ModbusRtuController_11', 'Motor_1')
insert into dbo.FieldPoints values('Vibration', 'Vibration', 'FieldPoint', '40000|1', 'float', 'true', 'false', 'vibrationSensors_1', '25', null, null, 'ModbusRtuController_1', 'Motor_1')
insert into dbo.FieldPoints values('Temperature', 'Temperature', 'FieldPoint', '40001|1', 'float', 'true', 'false', 'temperatureSensors_1', '100', null, null, 'ModbusRtuController_1', 'Motor_1')
insert into dbo.FieldPoints values('Status', 'Status', 'FieldPoint', '30006|2', 'bool', 'false', 'false', 'statusSensors_1', null, null, null, 'ModbusRtuController_11', 'Motor_1')

insert into dbo.FieldPoints values('Current_R', 'Current_R', 'FieldPoint', '30006|2', 'float', 'true', 'false', 'currentSensors_2', null, null, 1.73, 'ModbusRtuController_12', 'Motor_2')
insert into dbo.FieldPoints values('Current_Y', 'Current_Y', 'FieldPoint', '30008|2', 'float', 'true', 'false', 'currentSensors_2', null, null, 1.73, 'ModbusRtuController_12', 'Motor_2')
insert into dbo.FieldPoints values('Current_B', 'Current_B', 'FieldPoint', '30010|2', 'float', 'true', 'false', 'currentSensors_2', null, null, 1.73, 'ModbusRtuController_12', 'Motor_2')
insert into dbo.FieldPoints values('Voltage_1', 'Voltage_1', 'FieldPoint', '30000|2', 'float', 'true', 'false', 'voltageSensors_2', null, null, null, 'ModbusRtuController_12', 'Motor_2')
insert into dbo.FieldPoints values('Voltage_2', 'Voltage_2', 'FieldPoint', '30002|2', 'float', 'true', 'false', 'voltageSensors_2', null, null, null, 'ModbusRtuController_12', 'Motor_2')
insert into dbo.FieldPoints values('Voltage_3', 'Voltage_3', 'FieldPoint', '30004|2', 'float', 'true', 'false', 'voltageSensors_2', null, null, null, 'ModbusRtuController_12', 'Motor_2')
insert into dbo.FieldPoints values('Vibration', 'Vibration', 'FieldPoint', '40002|1', 'float', 'true', 'false', 'vibrationSensors_2', '25', null, null, 'ModbusRtuController_1', 'Motor_2')
insert into dbo.FieldPoints values('Temperature', 'Temperature', 'FieldPoint', '40003|1', 'float', 'true', 'false', 'temperatureSensors_2', '100', null, null, 'ModbusRtuController_1', 'Motor_2')
insert into dbo.FieldPoints values('Status', 'Status', 'FieldPoint', '30006|2', 'bool', 'false', 'false', 'statusSensors_2', null, null, null, 'ModbusRtuController_12', 'Motor_2')

insert into dbo.FieldPoints values('Current_R', 'Current_R', 'FieldPoint', '30006|2', 'float', 'true', 'false', 'currentSensors_3', null, null, 1.73, 'ModbusRtuController_13', 'Motor_3')
insert into dbo.FieldPoints values('Current_Y', 'Current_Y', 'FieldPoint', '30008|2', 'float', 'true', 'false', 'currentSensors_3', null, null, 1.73, 'ModbusRtuController_13', 'Motor_3')
insert into dbo.FieldPoints values('Current_B', 'Current_B', 'FieldPoint', '30010|2', 'float', 'true', 'false', 'currentSensors_3', null, null, 1.73, 'ModbusRtuController_13', 'Motor_3')
insert into dbo.FieldPoints values('Voltage_1', 'Voltage_1', 'FieldPoint', '30000|2', 'float', 'true', 'false', 'voltageSensors_3', null, null, null, 'ModbusRtuController_13', 'Motor_3')
insert into dbo.FieldPoints values('Voltage_2', 'Voltage_2', 'FieldPoint', '30002|2', 'float', 'true', 'false', 'voltageSensors_3', null, null, null, 'ModbusRtuController_13', 'Motor_3')
insert into dbo.FieldPoints values('Voltage_3', 'Voltage_3', 'FieldPoint', '30004|2', 'float', 'true', 'false', 'voltageSensors_3', null, null, null, 'ModbusRtuController_13', 'Motor_3')
insert into dbo.FieldPoints values('Vibration', 'Vibration', 'FieldPoint', '40004|1', 'float', 'true', 'false', 'vibrationSensors_3', '25', null, null, 'ModbusRtuController_1', 'Motor_3')
insert into dbo.FieldPoints values('Temperature', 'Temperature', 'FieldPoint', '40005|1', 'float', 'true', 'false', 'temperatureSensors_3', '100', null, null, 'ModbusRtuController_1', 'Motor_3')
insert into dbo.FieldPoints values('Status', 'Status', 'FieldPoint', '30006|2', 'bool', 'false', 'false', 'statusSensors_3', null, null, null, 'ModbusRtuController_13', 'Motor_3')

insert into dbo.FieldPoints values('Current_R', 'Current_R', 'FieldPoint', '30006|2', 'float', 'true', 'false', 'currentSensors_4', null, null, 1.73, 'ModbusRtuController_14', 'Motor_4')
insert into dbo.FieldPoints values('Current_Y', 'Current_Y', 'FieldPoint', '30008|2', 'float', 'true', 'false', 'currentSensors_4', null, null, 1.73, 'ModbusRtuController_14', 'Motor_4')
insert into dbo.FieldPoints values('Current_B', 'Current_B', 'FieldPoint', '30010|2', 'float', 'true', 'false', 'currentSensors_4', null, null, 1.73, 'ModbusRtuController_14', 'Motor_4')
insert into dbo.FieldPoints values('Voltage_1', 'Voltage_1', 'FieldPoint', '30000|2', 'float', 'true', 'false', 'voltageSensors_4', null, null, null, 'ModbusRtuController_14', 'Motor_4')
insert into dbo.FieldPoints values('Voltage_2', 'Voltage_2', 'FieldPoint', '30002|2', 'float', 'true', 'false', 'voltageSensors_4', null, null, null, 'ModbusRtuController_14', 'Motor_4')
insert into dbo.FieldPoints values('Voltage_3', 'Voltage_3', 'FieldPoint', '30004|2', 'float', 'true', 'false', 'voltageSensors_4', null, null, null, 'ModbusRtuController_14', 'Motor_4')
insert into dbo.FieldPoints values('Vibration', 'Vibration', 'FieldPoint', '40006|1', 'float', 'true', 'false', 'vibrationSensors_4', '25', null, null, 'ModbusRtuController_1', 'Motor_4')
insert into dbo.FieldPoints values('Temperature', 'Temperature', 'FieldPoint', '40007|1', 'float', 'true', 'false', 'temperatureSensors_4', '100', null, null, 'ModbusRtuController_1', 'Motor_4')
insert into dbo.FieldPoints values('Status', 'Status', 'FieldPoint', '30006|2', 'bool', 'false', 'false', 'statusSensors_4', null, null, null, 'ModbusRtuController_14', 'Motor_4')

insert into dbo.FieldPoints values('Current_R', 'Current_R', 'FieldPoint', '30006|2', 'float', 'true', 'false', 'currentSensors_5', null, null, 1.73, 'ModbusRtuController_15', 'Motor_5')
insert into dbo.FieldPoints values('Current_Y', 'Current_Y', 'FieldPoint', '30008|2', 'float', 'true', 'false', 'currentSensors_5', null, null, 1.73, 'ModbusRtuController_15', 'Motor_5')
insert into dbo.FieldPoints values('Current_B', 'Current_B', 'FieldPoint', '30010|2', 'float', 'true', 'false', 'currentSensors_5', null, null, 1.73, 'ModbusRtuController_15', 'Motor_5')
insert into dbo.FieldPoints values('Voltage_1', 'Voltage_1', 'FieldPoint', '30000|2', 'float', 'true', 'false', 'voltageSensors_5', null, null, null, 'ModbusRtuController_15', 'Motor_5')
insert into dbo.FieldPoints values('Voltage_2', 'Voltage_2', 'FieldPoint', '30002|2', 'float', 'true', 'false', 'voltageSensors_5', null, null, null, 'ModbusRtuController_15', 'Motor_5')
insert into dbo.FieldPoints values('Voltage_3', 'Voltage_3', 'FieldPoint', '30004|2', 'float', 'true', 'false', 'voltageSensors_5', null, null, null, 'ModbusRtuController_15', 'Motor_5')
insert into dbo.FieldPoints values('Vibration', 'Vibration', 'FieldPoint', '40000|1', 'float', 'true', 'false', 'vibrationSensors_5', '25', null, null, 'ModbusRtuController_2', 'Motor_5')
insert into dbo.FieldPoints values('Temperature', 'Temperature', 'FieldPoint', '40001|1', 'float', 'true', 'false', 'temperatureSensors_5', '100', null, null, 'ModbusRtuController_2', 'Motor_5')
insert into dbo.FieldPoints values('Status', 'Status', 'FieldPoint', '30006|2', 'bool', 'false', 'false', 'statusSensors_5', null, null, null, 'ModbusRtuController_15', 'Motor_5')

insert into dbo.FieldPoints values('Current_R', 'Current_R', 'FieldPoint', '30006|2', 'float', 'true', 'false', 'currentSensors_6', null, null, 1.73, 'ModbusRtuController_16', 'Motor_6')
insert into dbo.FieldPoints values('Current_Y', 'Current_Y', 'FieldPoint', '30008|2', 'float', 'true', 'false', 'currentSensors_6', null, null, 1.73, 'ModbusRtuController_16', 'Motor_6')
insert into dbo.FieldPoints values('Current_B', 'Current_B', 'FieldPoint', '30010|2', 'float', 'true', 'false', 'currentSensors_6', null, null, 1.73, 'ModbusRtuController_16', 'Motor_6')
insert into dbo.FieldPoints values('Voltage_1', 'Voltage_1', 'FieldPoint', '30000|2', 'float', 'true', 'false', 'voltageSensors_6', null, null, null, 'ModbusRtuController_16', 'Motor_6')
insert into dbo.FieldPoints values('Voltage_2', 'Voltage_2', 'FieldPoint', '30002|2', 'float', 'true', 'false', 'voltageSensors_6', null, null, null, 'ModbusRtuController_16', 'Motor_6')
insert into dbo.FieldPoints values('Voltage_3', 'Voltage_3', 'FieldPoint', '30004|2', 'float', 'true', 'false', 'voltageSensors_6', null, null, null, 'ModbusRtuController_16', 'Motor_6')
insert into dbo.FieldPoints values('Vibration', 'Vibration', 'FieldPoint', '40002|1', 'float', 'true', 'false', 'vibrationSensors_6', '25', null, null, 'ModbusRtuController_2', 'Motor_6')
insert into dbo.FieldPoints values('Temperature', 'Temperature', 'FieldPoint', '40003|1', 'float', 'true', 'false', 'temperatureSensors_6', '100', null, null, 'ModbusRtuController_2', 'Motor_6')
insert into dbo.FieldPoints values('Status', 'Status', 'FieldPoint', '30006|2', 'bool', 'false', 'false', 'statusSensors_6', null, null, null, 'ModbusRtuController_16', 'Motor_6')

insert into dbo.FieldPoints values('Current_R', 'Current_R', 'FieldPoint', '30006|2', 'float', 'true', 'false', 'currentSensors_7', null, null, 1.73, 'ModbusRtuController_17', 'Motor_7')
insert into dbo.FieldPoints values('Current_Y', 'Current_Y', 'FieldPoint', '30008|2', 'float', 'true', 'false', 'currentSensors_7', null, null, 1.73, 'ModbusRtuController_17', 'Motor_7')
insert into dbo.FieldPoints values('Current_B', 'Current_B', 'FieldPoint', '30010|2', 'float', 'true', 'false', 'currentSensors_7', null, null, 1.73, 'ModbusRtuController_17', 'Motor_7')
insert into dbo.FieldPoints values('Voltage_1', 'Voltage_1', 'FieldPoint', '30000|2', 'float', 'true', 'false', 'voltageSensors_7', null, null, null, 'ModbusRtuController_17', 'Motor_7')
insert into dbo.FieldPoints values('Voltage_2', 'Voltage_2', 'FieldPoint', '30002|2', 'float', 'true', 'false', 'voltageSensors_7', null, null, null, 'ModbusRtuController_17', 'Motor_7')
insert into dbo.FieldPoints values('Voltage_3', 'Voltage_3', 'FieldPoint', '30004|2', 'float', 'true', 'false', 'voltageSensors_7', null, null, null, 'ModbusRtuController_17', 'Motor_7')
insert into dbo.FieldPoints values('Vibration', 'Vibration', 'FieldPoint', '40000|1', 'float', 'true', 'false', 'vibrationSensors_7', '25', null, null, 'ModbusRtuController_3', 'Motor_7')
insert into dbo.FieldPoints values('Temperature', 'Temperature', 'FieldPoint', '40001|1', 'float', 'true', 'false', 'temperatureSensors_7', '100', null, null, 'ModbusRtuController_3', 'Motor_7')
insert into dbo.FieldPoints values('Status', 'Status', 'FieldPoint', '30006|2', 'bool', 'false', 'false', 'statusSensors_7', null, null, null, 'ModbusRtuController_17', 'Motor_7')

insert into dbo.FieldPoints values('Current_R', 'Current_R', 'FieldPoint', '30006|2', 'float', 'true', 'false', 'currentSensors_8', null, null, 1.73, 'ModbusRtuController_18', 'Motor_8')
insert into dbo.FieldPoints values('Current_Y', 'Current_Y', 'FieldPoint', '30008|2', 'float', 'true', 'false', 'currentSensors_8', null, null, 1.73, 'ModbusRtuController_18', 'Motor_8')
insert into dbo.FieldPoints values('Current_B', 'Current_B', 'FieldPoint', '30010|2', 'float', 'true', 'false', 'currentSensors_8', null, null, 1.73, 'ModbusRtuController_18', 'Motor_8')
insert into dbo.FieldPoints values('Voltage_1', 'Voltage_1', 'FieldPoint', '30000|2', 'float', 'true', 'false', 'voltageSensors_8', null, null, null, 'ModbusRtuController_18', 'Motor_8')
insert into dbo.FieldPoints values('Voltage_2', 'Voltage_2', 'FieldPoint', '30002|2', 'float', 'true', 'false', 'voltageSensors_8', null, null, null, 'ModbusRtuController_18', 'Motor_8')
insert into dbo.FieldPoints values('Voltage_3', 'Voltage_3', 'FieldPoint', '30004|2', 'float', 'true', 'false', 'voltageSensors_8', null, null, null, 'ModbusRtuController_18', 'Motor_8')
insert into dbo.FieldPoints values('Vibration', 'Vibration', 'FieldPoint', '40003|1', 'float', 'true', 'false', 'vibrationSensors_8', '25', null, null, 'ModbusRtuController_3', 'Motor_8')
insert into dbo.FieldPoints values('Temperature', 'Temperature', 'FieldPoint', '40004|1', 'float', 'true', 'false', 'temperatureSensors_8', '100', null, null, 'ModbusRtuController_3', 'Motor_8')
insert into dbo.FieldPoints values('Status', 'Status', 'FieldPoint', '30006|2', 'bool', 'false', 'false', 'statusSensors_8', null, null, null, 'ModbusRtuController_18', 'Motor_8')

insert into dbo.FieldPoints values('Current_R', 'Current_R', 'FieldPoint', '30006|2', 'float', 'true', 'false', 'currentSensors_9', null, null, 1.73, 'ModbusRtuController_19', 'Motor_9')
insert into dbo.FieldPoints values('Current_Y', 'Current_Y', 'FieldPoint', '30008|2', 'float', 'true', 'false', 'currentSensors_9', null, null, 1.73, 'ModbusRtuController_19', 'Motor_9')
insert into dbo.FieldPoints values('Current_B', 'Current_B', 'FieldPoint', '30010|2', 'float', 'true', 'false', 'currentSensors_9', null, null, 1.73, 'ModbusRtuController_19', 'Motor_9')
insert into dbo.FieldPoints values('Voltage_1', 'Voltage_1', 'FieldPoint', '30000|2', 'float', 'true', 'false', 'voltageSensors_9', null, null, null, 'ModbusRtuController_19', 'Motor_9')
insert into dbo.FieldPoints values('Voltage_2', 'Voltage_2', 'FieldPoint', '30002|2', 'float', 'true', 'false', 'voltageSensors_9', null, null, null, 'ModbusRtuController_19', 'Motor_9')
insert into dbo.FieldPoints values('Voltage_3', 'Voltage_3', 'FieldPoint', '30004|2', 'float', 'true', 'false', 'voltageSensors_9', null, null, null, 'ModbusRtuController_19', 'Motor_9')
insert into dbo.FieldPoints values('Vibration', 'Vibration', 'FieldPoint', '40005|1', 'float', 'true', 'false', 'vibrationSensors_9', '25', null, null, 'ModbusRtuController_3', 'Motor_9')
insert into dbo.FieldPoints values('Temperature', 'Temperature', 'FieldPoint', '40006|1', 'float', 'true', 'false', 'temperatureSensors_9', '100', null, null, 'ModbusRtuController_3', 'Motor_9')
insert into dbo.FieldPoints values('Status', 'Status', 'FieldPoint', '30006|2', 'bool', 'false', 'false', 'statusSensors_9', null, null, null, 'ModbusRtuController_19', 'Motor_9')

insert into dbo.FieldPoints values('Current_R', 'Current_R', 'FieldPoint', '30006|2', 'float', 'true', 'false', 'currentSensors_10', null, null, 1.73, 'ModbusRtuController_20', 'Motor_10')
insert into dbo.FieldPoints values('Current_Y', 'Current_Y', 'FieldPoint', '30008|2', 'float', 'true', 'false', 'currentSensors_10', null, null, 1.73, 'ModbusRtuController_20', 'Motor_10')
insert into dbo.FieldPoints values('Current_B', 'Current_B', 'FieldPoint', '30010|2', 'float', 'true', 'false', 'currentSensors_10', null, null, 1.73, 'ModbusRtuController_20', 'Motor_10')
insert into dbo.FieldPoints values('Voltage_1', 'Voltage_1', 'FieldPoint', '30000|2', 'float', 'true', 'false', 'voltageSensors_10', null, null, null, 'ModbusRtuController_20', 'Motor_10')
insert into dbo.FieldPoints values('Voltage_2', 'Voltage_2', 'FieldPoint', '30002|2', 'float', 'true', 'false', 'voltageSensors_10', null, null, null, 'ModbusRtuController_20', 'Motor_10')
insert into dbo.FieldPoints values('Voltage_3', 'Voltage_3', 'FieldPoint', '30004|2', 'float', 'true', 'false', 'voltageSensors_10', null, null, null, 'ModbusRtuController_20', 'Motor_10')
insert into dbo.FieldPoints values('Vibration', 'Vibration', 'FieldPoint', '40000|1', 'float', 'true', 'false', 'vibrationSensors_10', '25', null, null, 'ModbusRtuController_4', 'Motor_10')
insert into dbo.FieldPoints values('Temperature', 'Temperature', 'FieldPoint', '40001|1', 'float', 'true', 'false', 'temperatureSensors_10', '100', null, null, 'ModbusRtuController_4', 'Motor_10')
insert into dbo.FieldPoints values('Status', 'Status', 'FieldPoint', '30006|2', 'bool', 'false', 'false', 'statusSensors_10', null, null, null, 'ModbusRtuController_20', 'Motor_10')
insert into dbo.FieldPoints values('Status_1', 'Status_1', 'FieldPoint', '40003|1', 'bool', 'false', 'false', 'statusSensors_10', null, null, null, 'ModbusRtuController_4', 'Motor_10')
insert into dbo.FieldPoints values('Status_2', 'Status_2', 'FieldPoint', '40002|1', 'bool', 'false', 'false', 'statusSensors_10', null, null, null, 'ModbusRtuController_3', 'Motor_10')
insert into dbo.FieldPoints values('Vibration_1', 'Vibration_1', 'FieldPoint', '40003|1', 'float', 'false', 'false', 'vibrationSensors_10', '25', null, null, 'ModbusRtuController_4', 'Motor_10')
insert into dbo.FieldPoints values('Vibration_2', 'Vibration_2', 'FieldPoint', '40002|1', 'float', 'false', 'false', 'vibrationSensors_10', '25', null, null, 'ModbusRtuController_3', 'Motor_10')
insert into dbo.FieldPoints values('Temperature_1', 'Temperature_1', 'FieldPoint', '40001|1', 'float', 'true', 'false', 'temperatureSensors_10', '100', null, null, 'ModbusRtuController_4', 'Motor_10')
insert into dbo.FieldPoints values('Temperature_2', 'Temperature_2', 'FieldPoint', '40003|1', 'float', 'true', 'false', 'temperatureSensors_10', '100', null, null, 'ModbusRtuController_4', 'Motor_10')

--insert into dbo.FieldPoints values('Current_R', 'Current_R', 'FieldPoint', '30006|2', 'float', 'true', 'false', 'currentSensors_11', null, null, 1.73, 'ModbusRtuController_21', 'Motor_11')
--insert into dbo.FieldPoints values('Current_Y', 'Current_Y', 'FieldPoint', '30008|2', 'float', 'true', 'false', 'currentSensors_11', null, null, 1.73, 'ModbusRtuController_21', 'Motor_11')
--insert into dbo.FieldPoints values('Current_B', 'Current_B', 'FieldPoint', '30010|2', 'float', 'true', 'false', 'currentSensors_11', null, null, 1.73, 'ModbusRtuController_21', 'Motor_11')
--insert into dbo.FieldPoints values('Voltage_1', 'Voltage_1', 'FieldPoint', '30000|2', 'float', 'true', 'false', 'voltageSensors_11', null, null, null, 'ModbusRtuController_21', 'Motor_11')
--insert into dbo.FieldPoints values('Voltage_2', 'Voltage_2', 'FieldPoint', '30002|2', 'float', 'true', 'false', 'voltageSensors_11', null, null, null, 'ModbusRtuController_21', 'Motor_11')
--insert into dbo.FieldPoints values('Voltage_3', 'Voltage_3', 'FieldPoint', '30004|2', 'float', 'true', 'false', 'voltageSensors_11', null, null, null, 'ModbusRtuController_21', 'Motor_11')
--insert into dbo.FieldPoints values('Vibration', 'Vibration', 'FieldPoint', '40002|1', 'float', 'true', 'false', 'vibrationSensors_11', '25', null, null, 'ModbusRtuController_3', 'Motor_11')
--insert into dbo.FieldPoints values('Temperature', 'Temperature', 'FieldPoint', '40003|1', 'float', 'true', 'false', 'temperatureSensors_11', '100', null, null, 'ModbusRtuController_4', 'Motor_11')
--insert into dbo.FieldPoints values('Status', 'Status', 'FieldPoint', '30006|2', 'bool', 'false', 'false', 'statusSensors_11', null, null, null, 'ModbusRtuController_21', 'Motor_11')

insert into dbo.FieldPoints values('Vibration', 'Vibration', 'FieldPoint', '40002|1', 'float', 'true', 'false', 'vibrationSensors_12', '25', null, null, 'ModbusRtuController_4', 'Motor_12')

