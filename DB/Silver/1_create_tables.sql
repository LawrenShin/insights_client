--Code for country_ table

DROP TABLE IF EXISTS country CASCADE;

CREATE TABLE IF NOT EXISTS country
(
	id SERIAL PRIMARY KEY,
	name VARCHAR(45) NOT NULL,
	code VARCHAR(3) NOT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for address table: 

DROP TABLE IF EXISTS address CASCADE;

CREATE TABLE IF NOT EXISTS address 
(
	id SERIAL PRIMARY KEY,
	address_line VARCHAR(650) NOT NULL,
	address_number VARCHAR(100) NULL DEFAULT NULL,
	country_id INT REFERENCES country(id),
	city VARCHAR(100) NOT NULL,
	postal_code VARCHAR(70) NULL DEFAULT NULL,
	region VARCHAR(10) NULL DEFAULT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX address_id_pk
    ON public.address USING btree
    (id ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.address
    CLUSTER ON address_id_pk;


--Code for country Demographics table

DROP TABLE IF EXISTS country_demographics;

CREATE TABLE IF NOT EXISTS country_demographics
(
	id SERIAL PRIMARY KEY,
	country_id INT REFERENCES country(id),
	population FLOAT NOT NULL,
	immigrant_pop FLOAT NOT NULL,
	immigrant_percent FLOAT NOT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for country_Economy table

DROP TABLE IF EXISTS country_economy;

CREATE TABLE IF NOT EXISTS country_economy
(
	id SERIAL PRIMARY KEY,
	country_id INT REFERENCES country(id),
	gdp DOUBLE PRECISION NOT NULL,
	gdp_per_capita DOUBLE PRECISION NOT NULL,
	labour_force DOUBLE PRECISION NOT NULL,
	gdp_world FLOAT NOT NULL,
	labour_force_percent FLOAT NOT NULL,
	male_unemploy FLOAT NOT NULL,
	female_unemploy FLOAT NOT NULL,
	avg_income FLOAT NOT NULL,
	poor FLOAT NOT NULL,
	equality_level FLOAT NOT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for country Gender table

DROP TABLE IF EXISTS country_gender;

CREATE TABLE IF NOT EXISTS country_gender
(
	id SERIAL PRIMARY KEY,
	country_id INT REFERENCES country(id),
	male_pop FLOAT NOT NULL,
	female_pop FLOAT NOT NULL,
	women_edu FLOAT NOT NULL,
	female_work_force FLOAT NOT NULL,
	female_work_force_percent FLOAT NOT NULL,
	female_work_force_percent_pop FLOAT NOT NULL,
	materinty_leave FLOAT NOT NULL,
	paternity_leave FLOAT NOT NULL,
	gender_work_gap FLOAT NOT NULL,
	gender_health_gap FLOAT NOT NULL,
	gender_edu_gap FLOAT NOT NULL,
	gender_pol_gap FLOAT NOT NULL,
	income_gap FLOAT NOT NULL,
	women_violence FLOAT NOT NULL,
	female_parliament_share FLOAT NOT NULL,
	female_minister_share FLOAT NOT NULL,
	female_promotion_policy FLOAT NOT NULL,
	life_male FLOAT NOT NULL,
	life_female FLOAT NOT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for country_Age table


DROP TABLE IF EXISTS country_age;

CREATE TABLE IF NOT EXISTS country_age
(
	id SERIAL PRIMARY KEY,
	country_id INT REFERENCES country(id),
	dis19 FLOAT NOT NULL,
	dis39 FLOAT NOT NULL,
	dis59 FLOAT NOT NULL,
	dis70 FLOAT NOT NULL,
	disx FLOAT NOT NULL,
	avg18 FLOAT NOT NULL,
	parliament FLOAT NOT NULL,
	ministers FLOAT NOT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for country_Religion table

DROP TABLE IF EXISTS country_religion;

CREATE TABLE IF NOT EXISTS country_religion 
(
	id SERIAL PRIMARY KEY,
	country_id INT REFERENCES country(id),
	muslim FLOAT NOT NULL,
	christian FLOAT NOT NULL,
	hindu FLOAT NOT NULL,
	buddishm FLOAT NOT NULL,
	judaism FLOAT NOT NULL,
	other FLOAT NOT NULL,
	statereligion FLOAT NOT NULL,
	freedom FLOAT NOT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for country Edu table

DROP TABLE IF EXISTS country_edu;

CREATE TABLE IF NOT EXISTS country_edu 
(
	id SERIAL PRIMARY KEY,
	country_id INT REFERENCES country(id),
	elementary_mf FLOAT NOT NULL,
	elementary_male FLOAT NOT NULL,
	elementary_female FLOAT NOT NULL,
	high_school_mf FLOAT NOT NULL,
	high_school_male FLOAT NOT NULL,
	high_school_female FLOAT NOT NULL,
	bachelor_mf FLOAT NOT NULL,
	bachelor_male FLOAT NOT NULL,
	bachelor_female FLOAT NOT NULL,
	master_mf FLOAT NOT NULL,
	master_male FLOAT NOT NULL,
	master_female FLOAT NOT NULL,
	total_mf FLOAT NOT NULL,
	expected_education FLOAT NOT NULL,
	actual_education FLOAT NOT NULL,
	public_funding_gdp FLOAT NOT NULL,
	public_fund_fund FLOAT NOT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for country Race table

DROP TABLE IF EXISTS country_race;

CREATE TABLE IF NOT EXISTS country_race 
(
	id SERIAL PRIMARY KEY,
	country_id INT REFERENCES country(id),
	black FLOAT NOT NULL,
	asian FLOAT NOT NULL,
	hispanic FLOAT NOT NULL,
	arab FLOAT NOT NULL,
	caucasian FLOAT NOT NULL,
	indegineous FLOAT NOT NULL,
	discrimination_law FLOAT NOT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for country_Sex table

DROP TABLE IF EXISTS country_sex;

CREATE TABLE IF NOT EXISTS country_sex
(
	id SERIAL PRIMARY KEY,
	country_id INT REFERENCES country(id),
	same_marriage FLOAT NOT NULL,
	homosexual_tolerance FLOAT NOT NULL,
	homosexual_pop FLOAT NOT NULL,
	same_adopt FLOAT NOT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);


--Code for country_disability table

DROP TABLE IF EXISTS country_disability;

CREATE TABLE IF NOT EXISTS country_disability
(
	id SERIAL PRIMARY KEY,
	country_id INT REFERENCES country(id),
	disabled FLOAT NOT NULL,
	discrimination_law FLOAT NOT NULL,
	overweight FLOAT NOT NULL,
	health_funding_gdp FLOAT NOT NULL,
	health_funding_type FLOAT NOT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for country_Urban table

DROP TABLE IF EXISTS country_urban;

CREATE TABLE IF NOT EXISTS country_urban 
(
	id SERIAL PRIMARY KEY,
	country_id INT REFERENCES country(id),
	cities_pop FLOAT NOT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for country Political table

DROP TABLE IF EXISTS country_political;

CREATE TABLE IF NOT EXISTS country_political
(
	id SERIAL PRIMARY KEY,
	country_id INT REFERENCES country(id),
	democracy FLOAT NOT NULL,
	corruption FLOAT NOT NULL,
	freedom_speech FLOAT NOT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Company table

DROP TABLE IF EXISTS company CASCADE;

CREATE TABLE IF NOT EXISTS company
(
	id SERIAL PRIMARY KEY,
	lei CHAR(20) NULL DEFAULT NULL,
	legal_name VARCHAR(400) NULL DEFAULT NULL,
	legal_jurisdiction VARCHAR(6) DEFAULT NULL,
	status VARCHAR(30) DEFAULT NULL,
	num_employees INT NULL DEFAULT NULL,
	legal_id INT REFERENCES address(id) ,
	hq_id INT REFERENCES address(id),
	effective_from TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX company_id_pk
    ON public.company USING btree
    (id ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.company
    CLUSTER ON company_id_pk;
	
CREATE INDEX company_lei_idx
    ON public.company USING hash
    (lei COLLATE pg_catalog."default")
    TABLESPACE pg_default;
	
CREATE INDEX company_legal_name_idx
    ON public.company USING btree
    (legal_name COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;

--Code for Company Name table

DROP TABLE IF EXISTS company_name CASCADE;

CREATE TABLE IF NOT EXISTS company_name
(
	id SERIAL PRIMARY KEY,
	company_id INT REFERENCES company(id) ON DELETE CASCADE,
	name VARCHAR(400) NOT NULL,
	type VARCHAR(50) NOT NULL,
	effective_from TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX company_name_id_pk
    ON public.company_name USING btree
    (id ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.company_name
    CLUSTER ON company_name_id_pk;
	
CREATE INDEX company_name_company_id_idx
    ON public.company_name USING btree
    (company_id ASC NULLS LAST)
    TABLESPACE pg_default;

--Code for Company Extended Data table

DROP TABLE IF EXISTS company_extended_data CASCADE;

CREATE TABLE IF NOT EXISTS company_extended_data
(
	id SERIAL PRIMARY KEY,
	company_id INT REFERENCES company(id) ON DELETE CASCADE,
	below_national_avg_income DOUBLE PRECISION DEFAULT NULL,
	disabled_employees DOUBLE PRECISION DEFAULT NULL,
	hierarchy_level INT DEFAULT NULL,
	median_salary DOUBLE PRECISION DEFAULT NULL,
	retention_rate DOUBLE PRECISION DEFAULT NULL,
	effective_from TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX company_extended_data_id_pk
    ON public.company_extended_data USING btree
    (id ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.company_extended_data
    CLUSTER ON company_extended_data_id_pk;
	
CREATE INDEX company_extended_data_company_id_idx
    ON public.company_extended_data USING btree
    (company_id ASC NULLS LAST)
    TABLESPACE pg_default;

--Code for country Company table

DROP TABLE IF EXISTS company_country;

CREATE TABLE IF NOT EXISTS company_country
(
	company_country_id SERIAL PRIMARY KEY,
	company_id INT REFERENCES company(id) ON DELETE CASCADE,
	country_id INT REFERENCES country(id),
	legal_jurisdiction BOOLEAN NOT NULL,
	ticker VARCHAR(10) DEFAULT NULL,
	is_primary BOOLEAN NOT NULL,
	company_country_effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Industry table

DROP TABLE IF EXISTS industry CASCADE;

CREATE TABLE IF NOT EXISTS industry
(
	id SERIAL PRIMARY KEY,
	name VARCHAR(100) NOT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Company Industry table

DROP TABLE IF EXISTS company_industry;

CREATE TABLE IF NOT EXISTS company_industry
(
	id SERIAL PRIMARY KEY,
	company_id INT REFERENCES company(id) ON DELETE CASCADE,
	industry_id INT REFERENCES industry(id),
	primary_secondary CHAR(1) NOT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Company Questionnaire table

DROP TABLE IF EXISTS company_questionnaire;

CREATE TABLE IF NOT EXISTS company_questionnaire
(
	id SERIAL PRIMARY KEY,
	company_id INT REFERENCES company(id) ON DELETE CASCADE,
	question SMALLINT NOT NULL,
	answer INT NOT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX company_questionnaire_id_pk
    ON public.company_questionnaire USING btree
    (id ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.company_questionnaire
    CLUSTER ON company_questionnaire_id_pk;
	
CREATE INDEX company_questionnaire_company_id_idx
    ON public.company_questionnaire USING btree
    (company_id ASC NULLS LAST)
    TABLESPACE pg_default;

--Code for industry country table

DROP TABLE IF EXISTS industry_country;

CREATE TABLE IF NOT EXISTS industry_country
(	id SERIAL PRIMARY KEY,
	industry_id INT REFERENCES industry(id),
	country_id INT REFERENCES country(id),
	num_employees INT NOT NULL,
	avg_pay FLOAT NOT NULL,
	rentention_rate FLOAT NOT NULL,
	education_spend FLOAT NOT NULL,
	flexible_hours_pledge FLOAT NOT NULL,
	di_pledge FLOAT NOT NULL,
	harassment_pledge FLOAT NOT NULL,
	industry_diversity FLOAT NOT NULL,
	women_employeed_percent FLOAT NOT NULL,
	materinty_leave_pledge FLOAT NOT NULL,
	paternity_leave_pledge FLOAT NOT NULL,
	lgbt_pledge FLOAT NOT NULL,
	disabilities_pledge FLOAT NOT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Person table

DROP TABLE IF EXISTS person CASCADE;

CREATE TABLE IF NOT EXISTS person
(
	id SERIAL PRIMARY KEY,
	address INT REFERENCES address(id),
	name VARCHAR(135) NOT NULL,
	age SMALLINT NULL DEFAULT NULL,
	birth_year SMALLINT NULL DEFAULT NULL,
	gender VARCHAR(20) NULL DEFAULT NULL,
	picture VARCHAR(45) NULL DEFAULT NULL,
	race VARCHAR(45) NULL DEFAULT NULL,
	religion VARCHAR(45) NULL DEFAULT NULL,
	married VARCHAR(45) NULL DEFAULT NULL,
	high_edu VARCHAR(45) NULL DEFAULT NULL,
	edu_subject VARCHAR(100) NULL DEFAULT NULL,
	edu_institute VARCHAR(100) NULL DEFAULT NULL,
	sexuality VARCHAR(45) NULL DEFAULT NULL,
	disability VARCHAR(45) NULL DEFAULT NULL,
	base_salary DOUBLE PRECISION DEFAULT NULL,
	other_incentive DOUBLE PRECISION DEFAULT NULL,
	urban INT NULL DEFAULT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Person country table

DROP TABLE IF EXISTS person_country;

CREATE TABLE IF NOT EXISTS person_country
(
	person_country_id SERIAL PRIMARY KEY,
	person_id INT REFERENCES person(id) ON DELETE CASCADE,
	country_id INT REFERENCES country(id),
	person_country_effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for role table

DROP TABLE IF EXISTS role;

CREATE TABLE IF NOT EXISTS role
(
	id SERIAL PRIMARY KEY,
	company_id INT REFERENCES company(id) ON DELETE CASCADE,
	person_id INT REFERENCES person(id),
	is_effective SMALLINT NULL DEFAULT NULL,
	role_type SMALLINT NOT NULL,
	title VARCHAR(200) NOT NULL,
	base_salary INT NULL DEFAULT NULL,
	incentive_options VARCHAR(45) NULL DEFAULT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Code for matching table

DROP TABLE IF EXISTS company_match CASCADE;

CREATE TABLE IF NOT EXISTS company_match
(
	id SERIAL PRIMARY KEY,
	name VARCHAR(400) NOT NULL,
	company_id INT REFERENCES company(id) ON DELETE CASCADE DEFAULT NULL,
	effective_from TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX company_match_id_pk
    ON public.company_match USING btree
    (id ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.company_match
    CLUSTER ON company_match_id_pk;