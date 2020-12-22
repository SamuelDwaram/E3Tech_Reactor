drop procedure if exists dbo.LogLiveData
go
/* Logs Live Data for the Field Device*/
create procedure [dbo].[LogLiveData]
@FieldDeviceIdentifier nvarchar(50),
@Current_R nvarchar(10) = null,
@Current_Y nvarchar(10) = null,
@Current_B nvarchar(10) = null,
@Voltage_1 nvarchar(10) = null,
@Voltage_2 nvarchar(10) = null,
@Voltage_3 nvarchar(10) = null,
@Vibration nvarchar(10) = null,
@Status nvarchar(10) = null
as
begin
	set nocount on;
	declare @i_R nvarchar(10) = IIF(@Current_R is null, 0, cast(@Current_R as float));
	declare @i_Y nvarchar(10) = IIF(@Current_Y is null, 0, cast(@Current_Y as float));
	declare @i_B nvarchar(10) = IIF(@Current_B is null, 0, cast(@Current_B as float));
	declare @v_1 nvarchar(10) = IIF(@Voltage_1 is null, 0, cast(@Voltage_1 as float));
	declare @v_2 nvarchar(10) = IIF(@Voltage_2 is null, 0, cast(@Voltage_2 as float));
	declare @v_3 nvarchar(10) = IIF(@Voltage_3 is null, 0, cast(@Voltage_3 as float));
	declare @vib nvarchar(10) = IIF(@Vibration is null, 0, cast(@Vibration as float));
	declare @s nvarchar(10) = cast(@Status as bit);

	declare @query nvarchar(max);
	set @query = 'insert into dbo.' + @FieldDeviceIdentifier + '(Current_R, Current_Y, Current_B, Voltage_1, Voltage_2, Voltage_3, Vibration, Status, TimeStamp) values(' + @i_R + ',' + @i_Y + ',' + @i_B + ',' + @v_1 + ',' + @v_2 + ',' + @v_3 + ',' + @vib + ',' + @s + ',GetDate())';
	exec sp_executesql @query;
end
go

drop procedure if exists dbo.ReadFieldDeviceData
go
/* Reads the field device data*/
create procedure [dbo].[ReadFieldDeviceData]
@FieldDeviceIdentifier nvarchar(50),
@Current_R nvarchar(10) = null,
@Current_Y nvarchar(10) = null,
@Current_B nvarchar(10) = null,
@Voltage_1 nvarchar(10) = null,
@Voltage_2 nvarchar(10) = null,
@Voltage_3 nvarchar(10) = null,
@Vibration nvarchar(10) = null,
@startTime nvarchar(25),
@endTime nvarchar(25)
as
begin
	set nocount on;
	declare @query nvarchar(max);
	set @query = 'select ';
	if @Current_R is not null
		set @query = @query + 'Current_R as [I_R(A)],';
	if @Current_Y is not null
		set @query = @query + 'Current_Y as [I_Y(A)],';
	if @Current_B is not null
		set @query = @query + 'Current_B as [I_B(A)],';
	if @Voltage_1 is not null or @Voltage_2 is not null or @Voltage_3 is not null
		set @query = @query + 'dbo.GetAvgVoltage(Voltage_1, Voltage_2, Voltage_3) as [Vol_Avg(V)],';
	if @Vibration is not null
		set @query = @query + 'Vibration as [Vib(mm/s)],';
	set @query = @query + 'format(TimeStamp, ''HH:mm:ss dd-MM-yyyy'') as TIME from dbo.' + @FieldDeviceIdentifier + ' where TimeStamp between '''+ @startTime+''' and '''+ @endTime +''' order by TimeStamp';
	exec sp_executesql @query;
end
