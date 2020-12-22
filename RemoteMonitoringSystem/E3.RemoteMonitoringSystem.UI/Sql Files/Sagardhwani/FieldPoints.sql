use Sagardhwani
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

insert into dbo.FieldPoints values('Current_R', 'Current_R', 'FieldPoint', '30006|2', 'float', 'true', 'false', 'currentSensors_1', null, null, null, 'ModbusRtuController_1', 'Motor_1')
insert into dbo.FieldPoints values('Current_Y', 'Current_Y', 'FieldPoint', '30008|2', 'float', 'true', 'false', 'currentSensors_1', null, null, null, 'ModbusRtuController_1', 'Motor_1')
insert into dbo.FieldPoints values('Current_B', 'Current_B', 'FieldPoint', '30010|2', 'float', 'true', 'false', 'currentSensors_1', null, null, null, 'ModbusRtuController_1', 'Motor_1')
insert into dbo.FieldPoints values('Voltage_1', 'Voltage_1', 'FieldPoint', '30000|2', 'float', 'true', 'false', 'voltageSensors_1', null, null, 1.73, 'ModbusRtuController_1', 'Motor_1')
insert into dbo.FieldPoints values('Voltage_2', 'Voltage_2', 'FieldPoint', '30002|2', 'float', 'true', 'false', 'voltageSensors_1', null, null, 1.73, 'ModbusRtuController_1', 'Motor_1')
insert into dbo.FieldPoints values('Voltage_3', 'Voltage_3', 'FieldPoint', '30004|2', 'float', 'true', 'false', 'voltageSensors_1', null, null, 1.73, 'ModbusRtuController_1', 'Motor_1')
insert into dbo.FieldPoints values('Status', 'Status', 'FieldPoint', '30006|2', 'bool', 'false', 'false', 'statusSensors_1', null, null, null, 'ModbusRtuController_1', 'Motor_1')

insert into dbo.FieldPoints values('Current_R', 'Current_R', 'FieldPoint', '30006|2', 'float', 'true', 'false', 'currentSensors_2', null, null, null, 'ModbusRtuController_2', 'Motor_2')
insert into dbo.FieldPoints values('Current_Y', 'Current_Y', 'FieldPoint', '30008|2', 'float', 'true', 'false', 'currentSensors_2', null, null, null, 'ModbusRtuController_2', 'Motor_2')
insert into dbo.FieldPoints values('Current_B', 'Current_B', 'FieldPoint', '30010|2', 'float', 'true', 'false', 'currentSensors_2', null, null, null, 'ModbusRtuController_2', 'Motor_2')
insert into dbo.FieldPoints values('Voltage_1', 'Voltage_1', 'FieldPoint', '30000|2', 'float', 'true', 'false', 'voltageSensors_2', null, null, 1.73, 'ModbusRtuController_2', 'Motor_2')
insert into dbo.FieldPoints values('Voltage_2', 'Voltage_2', 'FieldPoint', '30002|2', 'float', 'true', 'false', 'voltageSensors_2', null, null, 1.73, 'ModbusRtuController_2', 'Motor_2')
insert into dbo.FieldPoints values('Voltage_3', 'Voltage_3', 'FieldPoint', '30004|2', 'float', 'true', 'false', 'voltageSensors_2', null, null, 1.73, 'ModbusRtuController_2', 'Motor_2')
insert into dbo.FieldPoints values('Status', 'Status', 'FieldPoint', '30006|2', 'bool', 'false', 'false', 'statusSensors_2', null, null, null, 'ModbusRtuController_2', 'Motor_2')

insert into dbo.FieldPoints values('Current_R', 'Current_R', 'FieldPoint', '30006|2', 'float', 'true', 'false', 'currentSensors_3', null, null, null, 'ModbusRtuController_3', 'Motor_3')
insert into dbo.FieldPoints values('Current_Y', 'Current_Y', 'FieldPoint', '30008|2', 'float', 'true', 'false', 'currentSensors_3', null, null, null, 'ModbusRtuController_3', 'Motor_3')
insert into dbo.FieldPoints values('Current_B', 'Current_B', 'FieldPoint', '30010|2', 'float', 'true', 'false', 'currentSensors_3', null, null, null, 'ModbusRtuController_3', 'Motor_3')
insert into dbo.FieldPoints values('Voltage_1', 'Voltage_1', 'FieldPoint', '30000|2', 'float', 'true', 'false', 'voltageSensors_3', null, null, 1.73, 'ModbusRtuController_3', 'Motor_3')
insert into dbo.FieldPoints values('Voltage_2', 'Voltage_2', 'FieldPoint', '30002|2', 'float', 'true', 'false', 'voltageSensors_3', null, null, 1.73, 'ModbusRtuController_3', 'Motor_3')
insert into dbo.FieldPoints values('Voltage_3', 'Voltage_3', 'FieldPoint', '30004|2', 'float', 'true', 'false', 'voltageSensors_3', null, null, 1.73, 'ModbusRtuController_3', 'Motor_3')
insert into dbo.FieldPoints values('Vibration', 'Vibration', 'FieldPoint', '40002|1', 'float', 'true', 'false', 'vibrationSensors_3', '25', null, null, 'ModbusRtuController_22', 'Motor_3')
insert into dbo.FieldPoints values('Status', 'Status', 'FieldPoint', '30006|2', 'bool', 'false', 'false', 'statusSensors_3', null, null, null, 'ModbusRtuController_3', 'Motor_3')

insert into dbo.FieldPoints values('Current_R', 'Current_R', 'FieldPoint', '30006|2', 'float', 'true', 'false', 'currentSensors_4', null, null, null, 'ModbusRtuController_4', 'Motor_4')
insert into dbo.FieldPoints values('Current_Y', 'Current_Y', 'FieldPoint', '30008|2', 'float', 'true', 'false', 'currentSensors_4', null, null, null, 'ModbusRtuController_4', 'Motor_4')
insert into dbo.FieldPoints values('Current_B', 'Current_B', 'FieldPoint', '30010|2', 'float', 'true', 'false', 'currentSensors_4', null, null, null, 'ModbusRtuController_4', 'Motor_4')
insert into dbo.FieldPoints values('Voltage_1', 'Voltage_1', 'FieldPoint', '30000|2', 'float', 'true', 'false', 'voltageSensors_4', null, null, 1.73, 'ModbusRtuController_4', 'Motor_4')
insert into dbo.FieldPoints values('Voltage_2', 'Voltage_2', 'FieldPoint', '30002|2', 'float', 'true', 'false', 'voltageSensors_4', null, null, 1.73, 'ModbusRtuController_4', 'Motor_4')
insert into dbo.FieldPoints values('Voltage_3', 'Voltage_3', 'FieldPoint', '30004|2', 'float', 'true', 'false', 'voltageSensors_4', null, null, 1.73, 'ModbusRtuController_4', 'Motor_4')
insert into dbo.FieldPoints values('Vibration', 'Vibration', 'FieldPoint', '40003|1', 'float', 'true', 'false', 'vibrationSensors_4', '25', null, null, 'ModbusRtuController_22', 'Motor_4')
insert into dbo.FieldPoints values('Status', 'Status', 'FieldPoint', '30006|2', 'bool', 'false', 'false', 'statusSensors_4', null, null, null, 'ModbusRtuController_4', 'Motor_4')

insert into dbo.FieldPoints values('Current_R', 'Current_R', 'FieldPoint', '30006|2', 'float', 'true', 'false', 'currentSensors_5', null, null, null, 'ModbusRtuController_5', 'Motor_5')
insert into dbo.FieldPoints values('Current_Y', 'Current_Y', 'FieldPoint', '30008|2', 'float', 'true', 'false', 'currentSensors_5', null, null, null, 'ModbusRtuController_5', 'Motor_5')
insert into dbo.FieldPoints values('Current_B', 'Current_B', 'FieldPoint', '30010|2', 'float', 'true', 'false', 'currentSensors_5', null, null, null, 'ModbusRtuController_5', 'Motor_5')
insert into dbo.FieldPoints values('Voltage_1', 'Voltage_1', 'FieldPoint', '30000|2', 'float', 'true', 'false', 'voltageSensors_5', null, null, 1.73, 'ModbusRtuController_5', 'Motor_5')
insert into dbo.FieldPoints values('Voltage_2', 'Voltage_2', 'FieldPoint', '30002|2', 'float', 'true', 'false', 'voltageSensors_5', null, null, 1.73, 'ModbusRtuController_5', 'Motor_5')
insert into dbo.FieldPoints values('Voltage_3', 'Voltage_3', 'FieldPoint', '30004|2', 'float', 'true', 'false', 'voltageSensors_5', null, null, 1.73, 'ModbusRtuController_5', 'Motor_5')
insert into dbo.FieldPoints values('Vibration', 'Vibration', 'FieldPoint', '40000|1', 'float', 'true', 'false', 'vibrationSensors_5', '25', null, null, 'ModbusRtuController_22', 'Motor_5')
insert into dbo.FieldPoints values('Status', 'Status', 'FieldPoint', '30006|2', 'bool', 'false', 'false', 'statusSensors_5', null, null, null, 'ModbusRtuController_5', 'Motor_5')

insert into dbo.FieldPoints values('Current_R', 'Current_R', 'FieldPoint', '30006|2', 'float', 'true', 'false', 'currentSensors_6', null, null, null, 'ModbusRtuController_6', 'Motor_6')
insert into dbo.FieldPoints values('Current_Y', 'Current_Y', 'FieldPoint', '30008|2', 'float', 'true', 'false', 'currentSensors_6', null, null, null, 'ModbusRtuController_6', 'Motor_6')
insert into dbo.FieldPoints values('Current_B', 'Current_B', 'FieldPoint', '30010|2', 'float', 'true', 'false', 'currentSensors_6', null, null, null, 'ModbusRtuController_6', 'Motor_6')
insert into dbo.FieldPoints values('Voltage_1', 'Voltage_1', 'FieldPoint', '30000|2', 'float', 'true', 'false', 'voltageSensors_6', null, null, 1.73, 'ModbusRtuController_6', 'Motor_6')
insert into dbo.FieldPoints values('Voltage_2', 'Voltage_2', 'FieldPoint', '30002|2', 'float', 'true', 'false', 'voltageSensors_6', null, null, 1.73, 'ModbusRtuController_6', 'Motor_6')
insert into dbo.FieldPoints values('Voltage_3', 'Voltage_3', 'FieldPoint', '30004|2', 'float', 'true', 'false', 'voltageSensors_6', null, null, 1.73, 'ModbusRtuController_6', 'Motor_6')
insert into dbo.FieldPoints values('Vibration', 'Vibration', 'FieldPoint', '40001|1', 'float', 'true', 'false', 'vibrationSensors_6', '25', null, null, 'ModbusRtuController_22', 'Motor_6')
insert into dbo.FieldPoints values('Status', 'Status', 'FieldPoint', '30006|2', 'bool', 'false', 'false', 'statusSensors_6', null, null, null, 'ModbusRtuController_6', 'Motor_6')

insert into dbo.FieldPoints values('Current_R', 'Current_R', 'FieldPoint', '30006|2', 'float', 'true', 'false', 'currentSensors_7', null, null, null, 'ModbusRtuController_7', 'Motor_7')
insert into dbo.FieldPoints values('Current_Y', 'Current_Y', 'FieldPoint', '30008|2', 'float', 'true', 'false', 'currentSensors_7', null, null, null, 'ModbusRtuController_7', 'Motor_7')
insert into dbo.FieldPoints values('Current_B', 'Current_B', 'FieldPoint', '30010|2', 'float', 'true', 'false', 'currentSensors_7', null, null, null, 'ModbusRtuController_7', 'Motor_7')
insert into dbo.FieldPoints values('Voltage_1', 'Voltage_1', 'FieldPoint', '30000|2', 'float', 'true', 'false', 'voltageSensors_7', null, null, 1.73, 'ModbusRtuController_7', 'Motor_7')
insert into dbo.FieldPoints values('Voltage_2', 'Voltage_2', 'FieldPoint', '30002|2', 'float', 'true', 'false', 'voltageSensors_7', null, null, 1.73, 'ModbusRtuController_7', 'Motor_7')
insert into dbo.FieldPoints values('Voltage_3', 'Voltage_3', 'FieldPoint', '30004|2', 'float', 'true', 'false', 'voltageSensors_7', null, null, 1.73, 'ModbusRtuController_7', 'Motor_7')
insert into dbo.FieldPoints values('Status', 'Status', 'FieldPoint', '30006|2', 'bool', 'false', 'false', 'statusSensors_7', null, null, null, 'ModbusRtuController_7', 'Motor_7')

insert into dbo.FieldPoints values('Current_R', 'Current_R', 'FieldPoint', '30006|2', 'float', 'true', 'false', 'currentSensors_8', null, null, null, 'ModbusRtuController_8', 'Motor_8')
insert into dbo.FieldPoints values('Current_Y', 'Current_Y', 'FieldPoint', '30008|2', 'float', 'true', 'false', 'currentSensors_8', null, null, null, 'ModbusRtuController_8', 'Motor_8')
insert into dbo.FieldPoints values('Current_B', 'Current_B', 'FieldPoint', '30010|2', 'float', 'true', 'false', 'currentSensors_8', null, null, null, 'ModbusRtuController_8', 'Motor_8')
insert into dbo.FieldPoints values('Voltage_1', 'Voltage_1', 'FieldPoint', '30000|2', 'float', 'true', 'false', 'voltageSensors_8', null, null, 1.73, 'ModbusRtuController_8', 'Motor_8')
insert into dbo.FieldPoints values('Voltage_2', 'Voltage_2', 'FieldPoint', '30002|2', 'float', 'true', 'false', 'voltageSensors_8', null, null, 1.73, 'ModbusRtuController_8', 'Motor_8')
insert into dbo.FieldPoints values('Voltage_3', 'Voltage_3', 'FieldPoint', '30004|2', 'float', 'true', 'false', 'voltageSensors_8', null, null, 1.73, 'ModbusRtuController_8', 'Motor_8')
insert into dbo.FieldPoints values('Vibration', 'Vibration', 'FieldPoint', '40004|1', 'float', 'true', 'false', 'vibrationSensors_8', '25', null, null, 'ModbusRtuController_22', 'Motor_8')
insert into dbo.FieldPoints values('Status', 'Status', 'FieldPoint', '30006|2', 'bool', 'false', 'false', 'statusSensors_8', null, null, null, 'ModbusRtuController_8', 'Motor_8')

