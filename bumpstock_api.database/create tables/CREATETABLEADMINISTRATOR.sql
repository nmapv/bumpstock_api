use messenger_api
if object_id(N'dbo.Administrator', N'U') is null
begin
	create table Administrator
	(
		id int not null identity(1,1),
		person_id int not null,
		register_date datetime not null,
	
		constraint administrator_pk_id primary key (id)
		WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
		constraint administrator_uni_personid unique(person_id)		
	);
end