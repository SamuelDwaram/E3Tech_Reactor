use test_db
go
  
/* Insert Field devices */
  insert into dbo.FieldDevices values('Motor_6', 'BOW THRUSTER NO-1(FWD)', 'THRUSTER MOTORS')
  insert into dbo.FieldDevices values('Motor_5', 'BOW THRUSTER NO-2(AFT)', 'THRUSTER MOTORS')
  insert into dbo.FieldDevices values('Motor_4', 'STERN THRUSTER NO-3(FWD)', 'THRUSTER MOTORS')
  insert into dbo.FieldDevices values('Motor_3', 'STERN THRUSTER NO-4(AFT)','THRUSTER MOTORS')
  
  insert into dbo.FieldDevices values('Motor_8', 'ATU-1', 'ATU & REF MOTORS')
  insert into dbo.FieldDevices values('Motor_9', 'ATU-2', 'ATU & REF MOTORS')
  insert into dbo.FieldDevices values('Motor_10', 'REF PLANT', 'ATU & REF MOTORS')
  --insert into dbo.FieldDevices values('Motor_11', 'REF-2', 'ATU & REF MOTORS')
  
  insert into dbo.FieldDevices values('Motor_1', 'STEEERING MOTOR PORT', 'STEERING MOTORS')
  insert into dbo.FieldDevices values('Motor_2', 'STEEERING MOTOR STBD', 'STEERING MOTORS')

  insert into dbo.FieldDevices values('Motor_7', 'FIRE MAIN MOTOR', 'FIRE MAINS')
  insert into dbo.FieldDevices values('Motor_12', 'FIRE PUMP', 'FIRE MAINS')


  
  
/* Insert Field Devices and Connected Controllers */
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_1', 'ModbusRtuController_1')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_2', 'ModbusRtuController_1')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_3', 'ModbusRtuController_1')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_4', 'ModbusRtuController_1')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_5', 'ModbusRtuController_2')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_6', 'ModbusRtuController_2')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_7', 'ModbusRtuController_3')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_8', 'ModbusRtuController_3')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_9', 'ModbusRtuController_3')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_10', 'ModbusRtuController_3')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_10', 'ModbusRtuController_4')
  --insert into dbo.FieldDevicesAndConnectedControllers values('Motor_11', 'ModbusRtuController_4')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_12', 'ModbusRtuController_4')

  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_1', 'ModbusRtuController_11')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_2', 'ModbusRtuController_12')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_3', 'ModbusRtuController_13')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_4', 'ModbusRtuController_14')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_5', 'ModbusRtuController_15')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_6', 'ModbusRtuController_16')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_7', 'ModbusRtuController_17')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_8', 'ModbusRtuController_18')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_9', 'ModbusRtuController_19')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_10', 'ModbusRtuController_20')
  --insert into dbo.FieldDevicesAndConnectedControllers values('Motor_11', 'ModbusRtuController_21')
  
  