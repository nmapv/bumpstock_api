use bumpstock_api
if object_id(N'dbo.ActivateContact', N'U') is null
begin
	create table ActivateContact
	(
		id int not null identity(1,1),
		contact_id int not null,
		person_id int not null,
		activation_code int not null,
		activation_date datetime null,
		register_date datetime not null,
		
		
		constraint activatecontact_pk_id primary key (id)
		WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	);
	create nonclustered index activatecontact_idx_contactid on ActivateContact (contact_id, activation_date)
	create nonclustered index activatecontact_idx_personid on ActivateContact (person_id);
end