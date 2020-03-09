use bumpstock_api_log
if object_id(N'dbo.Log', N'U') is null
begin
	create table Log
	(
		id int not null identity(1,1),
		machinename nvarchar(200) null,
		logged datetime not null,
		[level] varchar(5) not null,
		[message] nvarchar(max) not null,
		logger nvarchar(300) null,
		properties nvarchar(300) null,
		callsite nvarchar(300) null,
		exception nvarchar(max) null

		constraint log_pk_id primary key (id)
		WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	);
end