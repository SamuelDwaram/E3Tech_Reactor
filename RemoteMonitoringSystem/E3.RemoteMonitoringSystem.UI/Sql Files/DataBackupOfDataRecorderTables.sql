use Sunayna
go

create procedure dbo.GetAllDevicesData
@StartDate nvarchar(25),
@EndDate nvarchar(25)
as
begin
	set nocount on;
	select * from dbo.FieldDevices;
	
	DECLARE @id nvarchar(50)
	DECLARE @label NVARCHAR(100)
	DECLARE @getId CURSOR

	SET @getId = CURSOR FOR
	SELECT Identifier, Label from dbo.FieldDevices

	OPEN @getId
	FETCH NEXT
	FROM @getId INTO @id, @label
	WHILE @@FETCH_STATUS = 0
	BEGIN
		declare @query nvarchar(max);
		set @query = 'select * from dbo.' + @id + ' where TimeStamp between '''+ @StartDate +''' and '''+ @EndDate +''' order by TimeStamp';
		exec sp_executesql @query;
		
		FETCH NEXT
		FROM @getId INTO @id, @label
	END

	CLOSE @getId
	DEALLOCATE @getId
end
go