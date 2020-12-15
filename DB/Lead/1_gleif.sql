DROP TABLE IF EXISTS gleif_entity CASCADE;

CREATE TABLE IF NOT EXISTS gleif_entity(
	id BIGSERIAL PRIMARY KEY,
	lei VARCHAR(20),
	/*registration_authority_id VARCHAR(220),
	other_registration_authority_id VARCHAR(220),
	registration_authority_entity_id VARCHAR(220),*/
	legal_jurisdiction VARCHAR(6),
	category VARCHAR(16),
	legal_form_code VARCHAR(4),
	legal_form_other_legal_form VARCHAR(170),
	/*entity_associatedentity_type VARCHAR(15),
	entity_associatedentity_associatedlei VARCHAR(20),
	entity_associatedentity_associatedentityname VARCHAR(200),
	entity_associatedentity_associatedentityname_xmllang VARCHAR(8),*/
	status VARCHAR(30),
	expiration_date VARCHAR(32),
	entity_expiration_reason VARCHAR(20),
	successor_lei VARCHAR(20),
	successor_entity_name VARCHAR(300),
	successor_entity_name_xmllang VARCHAR(8),
	initial_registration_date VARCHAR(32),
	last_update_date VARCHAR(32),
	registration_status VARCHAR(20),
	next_renewal_date VARCHAR(32)
	/*registration_managing_lou VARCHAR(20),
	registration_validation_sources VARCHAR(25)*/
);

CREATE UNIQUE INDEX IF NOT EXISTS gleif_entity_pk
	ON public.gleif_entity USING btree
	(id ASC NULLS LAST);
		
CREATE UNIQUE INDEX IF NOT EXISTS gleif_entity_lei_idx
	ON public.gleif_entity USING btree
	(lei ASC NULLS LAST);	
		
ALTER TABLE public.gleif_entity
	CLUSTER ON gleif_entity_pk;

DROP TABLE IF EXISTS gleif_entity_name CASCADE;

CREATE TABLE IF NOT EXISTS gleif_entity_name(
	id BIGSERIAL PRIMARY KEY,
	entity_id bigint NOT NULL REFERENCES gleif_entity(id),
	name VARCHAR(400),
	xmllang VARCHAR(8),
	type VARCHAR(45)
);

CREATE UNIQUE INDEX IF NOT EXISTS gleif_entity_name_pk
	ON public.gleif_entity_name USING btree
	(id ASC NULLS LAST);

CREATE INDEX IF NOT EXISTS gleif_entity_name_entity_id_idx
	ON public.gleif_entity_name USING btree
	(entity_id ASC NULLS LAST);
		
CREATE INDEX IF NOT EXISTS gleif_entity_name_type_entity_id_idx
	ON public.gleif_entity_name USING btree
	(type ASC NULLS LAST, entity_id ASC NULLS LAST);	
		
ALTER TABLE public.gleif_entity_name
	CLUSTER ON gleif_entity_name_pk;

DROP TABLE IF EXISTS gleif_address CASCADE;

CREATE TABLE IF NOT EXISTS gleif_address(
	id BIGSERIAL PRIMARY KEY,
	entity_id bigint NOT NULL REFERENCES gleif_entity(id),
	xmllang VARCHAR(8),
	type VARCHAR(60),
	firstaddressline VARCHAR(300),
	addressnumber VARCHAR(100),
	addressnumberwithinbuilding VARCHAR(100),
	mailrouting VARCHAR(150),
	additionaladdressline_1 VARCHAR(120),
	additionaladdressline_2 VARCHAR(120),
	additionaladdressline_3 VARCHAR(120),
	city VARCHAR(100),
	region VARCHAR(10),
	country VARCHAR(2),
	postalcode VARCHAR(70)
);

CREATE UNIQUE INDEX IF NOT EXISTS gleif_address_pk
	ON public.gleif_address USING btree
	(id ASC NULLS LAST);
		
CREATE INDEX IF NOT EXISTS gleif_address_entity_id_idx
	ON public.gleif_address USING btree
	(entity_id ASC NULLS LAST);
		
CREATE INDEX IF NOT EXISTS gleif_address_type_entity_id_idx
	ON public.gleif_address USING btree
	(type ASC NULLS LAST, entity_id ASC NULLS LAST);
		
ALTER TABLE public.gleif_address
	CLUSTER ON gleif_address_pk;

/* CREATE TABLE gleif_validation_authority(
	id BIGSERIAL PRIMARY KEY,
	entity_id bigint NOT NULL REFERENCES gleif_entity(id),
	type VARCHAR(10),
	validation_authority_id VARCHAR(10),
	other_validation_authority_id VARCHAR(200),
	validation_authority_entity_id VARCHAR(200)
);

CREATE UNIQUE INDEX gleif_validation_authority_pk
	ON public.gleif_validation_authority USING btree
	(id ASC NULLS LAST);
		
CREATE INDEX gleif_validation_authority_entity_id_idx
	ON public.gleif_validation_authority USING btree
	(entity_id ASC NULLS LAST);
		
CREATE INDEX gleif_validation_authority_type_entity_id_idx
	ON public.gleif_validation_authority USING btree
	(type ASC NULLS LAST, entity_id ASC NULLS LAST);
		
ALTER TABLE public.gleif_validation_authority
	CLUSTER ON gleif_validation_authority_pk; */