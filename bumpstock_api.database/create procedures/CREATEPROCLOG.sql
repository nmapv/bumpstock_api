use bumpstock_api_log
if (object_id('proc_log_insert') is not null)
	drop procedure proc_log_insert
go
create procedure [dbo].[proc_log_insert] (
  @machineName nvarchar(200),
  @level varchar(5),
  @message nvarchar(max),
  @logger nvarchar(300),
  @properties nvarchar(max),
  @callsite nvarchar(300),
  @exception nvarchar(max)
) as
begin
  insert into [dbo].[Log] (
    [machinename],
    [logged],
    [level],
    [message],
    [logger],
    [properties],
    [callsite],
    [exception]
  ) values (
    @machineName,
    getdate(),
    @level,
    @message,
    @logger,
    @properties,
    @callsite,
    @exception
  );
end