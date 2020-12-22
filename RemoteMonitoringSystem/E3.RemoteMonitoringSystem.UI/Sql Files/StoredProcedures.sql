drop procedure if exists dbo.GetRunningTime;
go
create procedure dbo.GetRunningTime
@DeviceId nvarchar(25)
as
begin
	set nocount on;
	declare @deviceExists bit;
	set @deviceExists = iif((select count(*) from sys.tables where name=@DeviceId) > 0, 1,0);
	if @deviceExists = 0 
		select 0;
	else
	begin
		--Get the last switched on time stamp
		declare @lastSwitchedOnTimeStamp datetime, @query nvarchar(max), @runningMinutes int;
		declare @tempTable Table(TimeStamp datetime);
		set @query = 'select top(1) TimeStamp from dbo.'+ @DeviceId +' where Status=0 order by TimeStamp desc';
		insert into @tempTable exec sp_executesql @query;
		set @lastSwitchedOnTimeStamp = (select top(1) TimeStamp from @tempTable);
		delete @tempTable;
	
		--Calculate the number of minutes based on last switched on time stamp
		declare @dummy nvarchar(30);
		set @dummy = FORMAT(@lastSwitchedOnTimeStamp, 'yyyy-MM-dd HH:mm:ss.fff');
		set @query = 'select count(*) from dbo.'+ @DeviceId +' where TimeStamp > '''+@dummy+'''';
		declare @tempTable_2 Table(RunningTime int);
		insert into @tempTable_2 exec sp_executesql @query;
		set @runningMinutes = (select top(1) RunningTime from @tempTable_2);
		delete @tempTable_2;
		select @runningMinutes;
	end
end
go
