use Sagardhwani
go

/* Insert Controllers */

insert into dbo.Controllers values('ModbusRtuController_1', 'RMU 1', 'ModbusRtu', '01', 'COM3', 100)
insert into dbo.Controllers values('ModbusRtuController_2', 'RMU 2', 'ModbusRtu', '02', 'COM3', 100)
insert into dbo.Controllers values('ModbusRtuController_3', 'RMU 3', 'ModbusRtu', '03', 'COM3', 100)
insert into dbo.Controllers values('ModbusRtuController_4', 'RMU 4', 'ModbusRtu', '04', 'COM3', 100)
insert into dbo.Controllers values('ModbusRtuController_5', 'RMU 5', 'ModbusRtu', '05', 'COM3', 100)
insert into dbo.Controllers values('ModbusRtuController_6', 'RMU 6', 'ModbusRtu', '06', 'COM3', 100)
insert into dbo.Controllers values('ModbusRtuController_7', 'RMU 7', 'ModbusRtu', '07', 'COM3', 100)
insert into dbo.Controllers values('ModbusRtuController_8', 'RMU 8', 'ModbusRtu', '08', 'COM3', 100)

insert into dbo.Controllers values('ModbusRtuController_22', 'RMS 1', 'ModbusRtu', '22', 'COM3', 100)


/* Insert Used Memory Addresses */

insert into dbo.UsedMemoryAddressesInControllers values('ModbusRtuController_1', '30000|12')
insert into dbo.UsedMemoryAddressesInControllers values('ModbusRtuController_2', '30000|12')
insert into dbo.UsedMemoryAddressesInControllers values('ModbusRtuController_3', '30000|12')
insert into dbo.UsedMemoryAddressesInControllers values('ModbusRtuController_4', '30000|12')
insert into dbo.UsedMemoryAddressesInControllers values('ModbusRtuController_5', '30000|12')
insert into dbo.UsedMemoryAddressesInControllers values('ModbusRtuController_6', '30000|12')
insert into dbo.UsedMemoryAddressesInControllers values('ModbusRtuController_7', '30000|12')
insert into dbo.UsedMemoryAddressesInControllers values('ModbusRtuController_8', '30000|12')

insert into dbo.UsedMemoryAddressesInControllers values('ModbusRtuController_22', '40000|8')
