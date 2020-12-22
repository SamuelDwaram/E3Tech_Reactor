drop table if exists dbo.TankUnits
go
create table TankUnits
(
FieldPointId nvarchar(50),
SensorsDataSetId nvarchar(50),
DeviceId nvarchar(50),
Units nvarchar(50),
constraint chk_FieldPoint foreign key(FieldPointId, SensorsDataSetId, DeviceId) references dbo.FieldPoints(Label, SensorDataSetIdentifier, FieldDeviceIdentifier) 
)

insert into dbo.TankUnits(FieldPointId, SensorsDataSetId, DeviceId) select Label, SensorDataSetIdentifier, FieldDeviceIdentifier from dbo.FieldPoints

update dbo.TankUnits set Units='T' where FieldPointId='Tank_1'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_1'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_2'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_3'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_4'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_5'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_6'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_7'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_8'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_9'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_10'

update dbo.TankUnits set Units='T' where FieldPointId='Tank_11'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_12'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_13'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_14'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_15'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_16'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_17'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_18'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_19'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_20'

update dbo.TankUnits set Units='T' where FieldPointId='Tank_21'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_22'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_23'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_24'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_25'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_26'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_27'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_28'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_29'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_30'

update dbo.TankUnits set Units='T' where FieldPointId='Tank_31'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_32'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_33'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_34'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_35'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_36'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_37'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_38'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_39'
update dbo.TankUnits set Units='T' where FieldPointId='Tank_40'

select * from dbo.TankUnits