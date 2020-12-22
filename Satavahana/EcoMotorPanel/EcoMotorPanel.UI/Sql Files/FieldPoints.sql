use [Satavahana.EcoMotorPanel]
go

insert into dbo.FieldPoints values('ExcitationCurrent', 'ExcitationCurrent', 'FieldPoint', '40000|1', 'float', 'false', 'false', 'analogSensors_2',null, null, null, 'ModbusRtuController_2', 'Motor_2')
insert into dbo.FieldPoints values('ArmatureCurrent', 'ArmatureCurrent', 'FieldPoint', '40001|1', 'float', 'false', 'false', 'analogSensors_2',null, null, null, 'ModbusRtuController_2', 'Motor_2')
insert into dbo.FieldPoints values('RPM', 'RPM', 'FieldPoint', '40002|1', 'float', 'false', 'false', 'analogSensors_2',null, null, null, 'ModbusRtuController_2', 'Motor_2')
insert into dbo.FieldPoints values('Voltage', 'Voltage', 'FieldPoint', '40003|1', 'float', 'false', 'false', 'analogSensors_2',null, null, null, 'ModbusRtuController_2', 'Motor_2')
insert into dbo.FieldPoints values('MotorStatusIndicator', 'MotorStatusIndicator', 'FieldPoint', '40015|1', 'float', 'false', 'false', 'statusSensors_2',null, null, null, 'ModbusRtuController_2', 'Motor_2')
insert into dbo.FieldPoints values('PropulsionWheelInput', 'PropulsionWheelInput', 'FieldPoint', '40018|1', 'float', 'false', 'false', 'statusSensors_2',null, null, null, 'ModbusRtuController_2', 'Motor_2')
insert into dbo.FieldPoints values('VoltmeterSelector', 'VoltmeterSelector', 'FieldPoint', '40019|1', 'float', 'false', 'false', 'statusSensors_2',null, null, null, 'ModbusRtuController_2', 'Motor_2')
insert into dbo.FieldPoints values('EmergencySwitch', 'EmergencySwitch', 'FieldPoint', '40028|1', 'float', 'false', 'false', 'statusSensors_2',null, null, null, 'ModbusRtuController_2', 'Motor_2')
insert into dbo.FieldPoints values('GovernorWheelInput', 'GovernorWheelInput', 'FieldPoint', '40029|1', 'float', 'false', 'false', 'speedSensors_2', null, null, null, 'ModbusRtuController_2', 'Motor_2')
