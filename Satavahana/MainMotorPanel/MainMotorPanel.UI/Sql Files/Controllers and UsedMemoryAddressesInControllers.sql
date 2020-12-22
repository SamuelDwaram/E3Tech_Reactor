use [Satavahana.MainMotorPanel]
go

/* Insert Controllers */
insert into dbo.Controllers values('ModbusRtuController_1', 'PCB 1', 'ModbusRtu', '1', 'COM4', 100)

/* Insert Used Memory Addresses */
insert into dbo.UsedMemoryAddressesInControllers values('ModbusRtuController_1', '40000|30')


