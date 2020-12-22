use Magar
go

drop table if exists dbo.LiveData
drop procedure if exists dbo.LogLiveData

create table LiveData
(
DeviceId nvarchar(50),
ParameterLabel nvarchar(50),
SensorDataSetId nvarchar(50),
Value nvarchar(20),
ValueUpdatedForUi nvarchar(20),
constraint pk_validFieldPoint foreign key(ParameterLabel, SensorDataSetId, DeviceId) references dbo.FieldPoints(Label, SensorDataSetIdentifier, FieldDeviceIdentifier)
)
go

create procedure LogLiveData
@DeviceId nvarchar(50),
@ParameterLabel nvarchar(50),
@SensorDataSetId nvarchar(50),
@Value nvarchar(20),
@ValueUpdatedForUi nvarchar(20)
as
begin
	set nocount on;
	declare @previousValue nvarchar(20);
	set @previousValue = (select top(1) Value from dbo.LiveData where DeviceId = @DeviceId and ParameterLabel = @ParameterLabel and SensorDataSetId = @SensorDataSetId);
	if @previousValue is null
		insert into dbo.LiveData(DeviceId, ParameterLabel, SensorDataSetId, Value, ValueUpdatedForUi) values(@DeviceId, @ParameterLabel, @SensorDataSetId, @Value, @ValueUpdatedForUi);
	else
		update dbo.LiveData set Value = @Value, ValueUpdatedForUi = @ValueUpdatedForUi where DeviceId = @DeviceId and ParameterLabel = @ParameterLabel and SensorDataSetId = @SensorDataSetId;
end