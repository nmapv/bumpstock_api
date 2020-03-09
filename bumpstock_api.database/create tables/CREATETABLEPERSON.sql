use bumpstock_api
if object_id(N'dbo.Person', N'U') is null
begin
	create table Person
	(
		id int not null identity(1,1),
		first_name varchar(40) null,
		last_name varchar(40) null,
		hash varchar(500) not null,
		register_date datetime not null
		
	
		constraint person_pk_id primary key (id)
		WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
		constraint person_uni_login unique(hash)		
	);
	create nonclustered index person_idx_hash on Person (hash);
end