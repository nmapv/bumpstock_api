use bumpstock_api
if object_id(N'dbo.Contact', N'U') is null
begin
	create table Contact
	(
		id int not null identity(1,1),
		ddi int not null,
		ddd int not null,
		cellphone bigint not null,
		register_date datetime not null
		
		constraint contact_pk_id primary key (id)
		WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
		constraint person_uni_ddidddcellphone unique(email)
	);
	create nonclustered index contact_idx_ddidddcellphone on Contact (ddi, ddd, cellphone);	
end