-- DICTIONARIES --

-- Code for industry table --

DROP TABLE IF EXISTS industries CASCADE;

CREATE TABLE IF NOT EXISTS industries
(
	id INT PRIMARY KEY,
	name VARCHAR(100) NOT NULL
);

INSERT INTO industries(id, name) VALUES
	(0, 'Health Care and Social Assistance'),
	(1, 'Accommodation and Food Services'),
	(2, 'Finance and Insurance'),
	(3, 'Educational Services'),
	(4, 'Management of Companies and Enterprises'),
	(5, 'Wholesale Trade'),
	(6, 'Information'),
	(7, 'Other Services (except Public Administration)'),
	(8, 'Public Administration'),
	(9, 'Agriculture, Forestry, Fishing and Hunting'),
	(10, 'Retail Trade'),
	(11, 'Manufacturing'),
	(12, 'Transportation and Warehousing'),
	(13, 'Construction'),
	(14, 'Administrative and Support and Waste Management and Remediation Services'),
	(15, 'Utilities'),
	(16, 'Mining, Quarrying, and Oil and Gas Extraction'),
	(17, 'Real Estate and Rental and Leasing'),
	(18, 'Professional, Scientific, and Technical Services'),
	(19, 'Arts, Entertainment, and Recreation');
	
-- Code for industry table --

DROP TABLE IF EXISTS industry_codes CASCADE;

CREATE TABLE IF NOT EXISTS industry_codes
(
	id INT PRIMARY KEY,
	name VARCHAR(20) NOT NULL
);

INSERT INTO industry_codes(id, name) VALUES
	(0, 'NACE'),
	(1, 'NAICS'),
	(2, 'Local');

-- Code for Gender table --

DROP TABLE IF EXISTS genders CASCADE;

CREATE TABLE IF NOT EXISTS genders
(
	id INT PRIMARY KEY,
	name VARCHAR(50) NOT NULL
);

INSERT INTO genders(id, name) VALUES
	(0, 'Female'),
	(1, 'Male'),
	(2, 'Other');
	
-- Code for Marital status table --

DROP TABLE IF EXISTS marital_statuses CASCADE;

CREATE TABLE IF NOT EXISTS marital_statuses
(
	id INT PRIMARY KEY,
	name VARCHAR(50) NOT NULL
);

INSERT INTO marital_statuses(id, name) VALUES
	(0, 'No'),
	(1, 'Yes');
	
-- Code for Race table --

DROP TABLE IF EXISTS races CASCADE;

CREATE TABLE IF NOT EXISTS races
(
	id INT PRIMARY KEY,
	name VARCHAR(50) NOT NULL
);

INSERT INTO races(id, name) VALUES
	(0, 'Arab'),
	(1, 'Hispanic'),
	(2, 'Black'),
	(3, 'Asian'),
	(4, 'Caucasian'),
	(5, 'Indigenous');
	
-- Code for Religion table --

DROP TABLE IF EXISTS religions CASCADE;

CREATE TABLE IF NOT EXISTS religions
(
	id INT PRIMARY KEY,
	name VARCHAR(50) NOT NULL
);

INSERT INTO religions(id, name) VALUES
	(0, 'Muslim'),
	(1, 'Christian'),
	(2, 'Hindu'),
	(3, 'Buddhism'),
	(4, 'Judaism'),
	(5, 'Other');
	
-- Code for Role type table --

DROP TABLE IF EXISTS role_types CASCADE;

CREATE TABLE IF NOT EXISTS role_types
(
	id INT PRIMARY KEY,
	name VARCHAR(50) NOT NULL
);

INSERT INTO role_types(id, name) VALUES
	(1, 'Executive'),
	(2, 'Board'),
	(3, 'Both');
	
-- Code for Regions table --

DROP TABLE IF EXISTS regions CASCADE;

CREATE TABLE IF NOT EXISTS regions
(
	id INT PRIMARY KEY,
	name VARCHAR(50) NOT NULL
);

INSERT INTO regions(id, name) VALUES
	(0, 'North America'),
	(1, 'South America'),
	(2, 'Central America'),
	(3, 'Europe'),
	(4, 'Oceania'),
	(5, 'Asia'),
	(6, 'Africa'),
	(7, 'Middle East');

-- Code for Education levels table --

DROP TABLE IF EXISTS education_levels CASCADE;

CREATE TABLE IF NOT EXISTS education_levels
(
	id INT PRIMARY KEY,
	name VARCHAR(30) NOT NULL
);

INSERT INTO education_levels(id, name) VALUES
	(0, 'Elementary'),
	(1, 'High School'),
	(2, 'Bachelor'),
	(3, 'Master'),
	(4, 'Master+');

-- Code for Education subjects table --

DROP TABLE IF EXISTS education_subjects CASCADE;

CREATE TABLE IF NOT EXISTS education_subjects
(
	id INT PRIMARY KEY,
	name VARCHAR(100) NOT NULL
);

INSERT INTO education_subjects(id, name) VALUES
	(0, 'Agriculture'),
	(1, 'Archaeology & Ethnology'),
	(2, 'Architecture'),
	(3, 'Art & Literature'),
	(4, 'Astronomy'),
	(5, 'Bioengineering'),
	(6, 'Biology'),
	(7, 'Business/Business Administration'),
	(8, 'Chemistry'),
	(9, 'Communications, Journalism & Writing'),
	(10, 'Computer Science/Information Systems'),
	(11, 'Economics'),
	(12, 'Education'),
	(13, 'Engineering'),
	(14, 'Environmental Science'),
	(15, 'Ergonomics'),
	(16, 'Finance & Accounting'),
	(17, 'Forestry'),
	(18, 'Geography'),
	(19, 'History'),
	(20, 'Human Resources'),
	(21, 'Humanities, Ethnic And Cultural Studies'),
	(22, 'Law'),
	(23, 'Library And Information Science'),
	(24, 'Linguistics'),
	(25, 'Logistics And Supply Chain'),
	(26, 'Management'),
	(27, 'Manufacturing'),
	(28, 'Mathematics'),
	(29, 'Medicine & Health Sciences'),
	(30, 'Military'),
	(31, 'Music, Film & Dance'),
	(32, 'Philosophy'),
	(33, 'Physics'),
	(34, 'Political Science'),
	(35, 'Psychology'),
	(36, 'Public Affairs'),
	(37, 'Religious Studies'),
	(38, 'Sales & Marketing'),
	(39, 'Sociology');

-- Code for country table

DROP TABLE IF EXISTS countries CASCADE;

CREATE TABLE IF NOT EXISTS countries
(
	id SERIAL PRIMARY KEY,
	name VARCHAR(45) NOT NULL,
	code VARCHAR(3) NOT NULL
);

INSERT INTO countries(name, code) VALUES
	('Afghanistan','AF'),
	('Albania','AL'),
	('Algeria','DZ'),
	('Andorra','AD'),
	('Angola','AO'),
	('Argentina','AR'),
	('Armenia','AM'),
	('Australia','AU'),
	('Austria','AT'),
	('Azerbaijan','AZ'),
	('Bahamas','BS'),
	('Bahrain','BH'),
	('Bangladesh','BD'),
	('Belarus','BY'),
	('Belgium','BE'),
	('Belize','BZ'),
	('Benin','BJ'),
	('Bermuda','BM'),
	('Bhutan','BT'),
	('Bolivia','BO'),
	('Bosnia and Herzegovina','BA'),
	('Botswana','BW'),
	('Brazil','BR'),
	('Brunei','BN'),
	('Bulgaria','BG'),
	('Burkina Faso','BF'),
	('Burundi','BI'),
	('Cabo Verde','CP'),
	('Cambodia','KH'),
	('Cameroon','CM'),
	('Canada','CA'),
	('Central African Republic','CF'),
	('Chad','TD'),
	('Chile','CL'),
	('China','CN'),
	('Colombia','CO'),
	('Comoros','KM'),
	('Congo','CG'),
	('Cook Islands','CK'),
	('Costa Rica','CR'),
	('Croatia','HR'),
	('Cuba','CU'),
	('Curaçao','CW'),
	('Cyprus','CY'),
	('Czech Republic','CZ'),
	('Côte d''Ivoire','CI'),
	('DR Congo','COD'),
	('Denmark','DK'),
	('Djibouti','DJ'),
	('Dominican Republic','DO'),
	('Ecuador','EC'),
	('Egypt','EG'),
	('El Salvador','SV'),
	('Equatorial Guinea','GQ'),
	('Eritrea','ER'),
	('Estonia','EE'),
	('Eswatini','SW'),
	('Ethiopia','ET'),
	('Fiji','FJ'),
	('Finland','FI'),
	('France','FR'),
	('French Guiana','GF'),
	('French Polynesia','PF'),
	('Gabon','GA'),
	('Gambia','GM'),
	('Georgia','GE'),
	('Germany','DE'),
	('Ghana','GH'),
	('Greece','GR'),
	('Greenland','GL'),
	('Guadeloupe','GP'),
	('Guam','GU'),
	('Guatemala','GT'),
	('Guinea','GN'),
	('Guinea-Bissau','GW'),
	('Guyana','GY'),
	('Haiti','HT'),
	('Honduras','HN'),
	('Hong Kong','HK'),
	('Hungary','HU'),
	('Iceland','IS'),
	('India','IN'),
	('Indonesia','ID'),
	('Iran','IR'),
	('Iraq','IQ'),
	('Ireland','IE'),
	('Israel','IL'),
	('Italy','IT'),
	('Jamaica','JM'),
	('Japan','JP'),
	('Jordan','JO'),
	('Kazakhstan','KZ'),
	('Kenya','KE'),
	('Kiribati','KI'),
	('Kosovo','XK'),
	('Kuwait','KW'),
	('Kyrgyzstan','KG'),
	('Laos','LA'),
	('Latvia','LV'),
	('Lebanon','LB'),
	('Lesotho','LS'),
	('Liberia','LR'),
	('Libya','LY'),
	('Liechtenstein','LI'),
	('Lithuania','LT'),
	('Luxembourg','LU'),
	('Madagascar','MG'),
	('Malawi','MW'),
	('Malaysia','MY'),
	('Maldives','MV'),
	('Mali','ML'),
	('Malta','MT'),
	('Marshall Islands','MH'),
	('Martinique','MQ'),
	('Mauritania','MR'),
	('Mauritius','MU'),
	('Mexico','MX'),
	('Moldova','MD'),
	('Monaco','MC'),
	('Mongolia','MN'),
	('Montenegro','ME'),
	('Morocco','MA'),
	('Mozambique','MZ'),
	('Myanmar','MM'),
	('Namibia','NA'),
	('Nepal','NP'),
	('Netherlands','NL'),
	('New Caledonia','NC'),
	('New Zealand','NZ'),
	('Nicaragua','NI'),
	('Niger','NE'),
	('Nigeria','NG'),
	('North Korea','PRK'),
	('North Macedonia','MK'),
	('Norway','NO'),
	('Oman','OM'),
	('Pakistan','PK'),
	('Palau','PW'),
	('Panama','PA'),
	('Papua New Guinea','PG'),
	('Paraguay','PY'),
	('Peru','PE'),
	('Philippines','PH'),
	('Poland','PL'),
	('Portugal','PT'),
	('Puerto Rico','PR'),
	('Qatar','QA'),
	('Romania','RO'),
	('Russian Federation','RU'),
	('Rwanda','RW'),
	('Saint Kitts and Nevis','KN'),
	('Saint Lucia','LC'),
	('Saint Vincent and the Grenadines','VC'),
	('Samoa','WS'),
	('San Marino','SM'),
	('Sao Tome & Principe','ST'),
	('Saudi Arabia','SA'),
	('Senegal','SN'),
	('Serbia','RS'),
	('Seychelles','SC'),
	('Sierra Leone','SL'),
	('Singapore','SG'),
	('Slovak Republic','SK'),
	('Slovenia','SI'),
	('Solomon Islands','SB'),
	('Somalia','SO'),
	('South Africa','ZA'),
	('South Korea','KO'),
	('South Sudan','SS'),
	('Spain','ES'),
	('Sri Lanka','LK'),
	('State of Palestine','PS'),
	('Sudan','SD'),
	('Suriname','SR'),
	('Sweden','SE'),
	('Switzerland','CH'),
	('Syria','SY'),
	('Taiwan','TW'),
	('Tajikistan','TJ'),
	('Tanzania','TZ'),
	('Thailand','TH'),
	('Timor-Leste','TL'),
	('Togo','TG'),
	('Tonga','TO'),
	('Trinidad and Tobago','TT'),
	('Tunisia','TN'),
	('Turkey','TR'),
	('Turkmenistan','TM'),
	('Uganda','UG'),
	('Ukraine','UA'),
	('United Arab Emirates','AE'),
	('United Kingdom','GB'),
	('United States Virgin Islands','VI'),
	('United States','US'),
	('Uruguay','UY'),
	('Uzbekistan','UZ'),
	('Vanuatu','VU'),
	('Venezuela','VE'),
	('Vietnam','VN'),
	('Yemen','YE'),
	('Zambia','ZM'),
	('Zimbabwe','ZW');

-- DATA TABLES --

--Code for country Demographics table

DROP TABLE IF EXISTS country_demographics;

CREATE TABLE IF NOT EXISTS country_demographics
(
	id SERIAL PRIMARY KEY,
	country_id INT REFERENCES countries(id),
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
	country_id INT REFERENCES countries(id),
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
	country_id INT REFERENCES countries(id),
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
	country_id INT REFERENCES countries(id),
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
	country_id INT REFERENCES countries(id),
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
	country_id INT REFERENCES countries(id),
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
	country_id INT REFERENCES countries(id),
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
	country_id INT REFERENCES countries(id),
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
	country_id INT REFERENCES countries(id),
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
	country_id INT REFERENCES countries(id),
	cities_pop FLOAT NOT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for country Political table

DROP TABLE IF EXISTS country_political;

CREATE TABLE IF NOT EXISTS country_political
(
	id SERIAL PRIMARY KEY,
	country_id INT REFERENCES countries(id),
	democracy FLOAT NOT NULL,
	corruption FLOAT NOT NULL,
	freedom_speech FLOAT NOT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for industry country table

DROP TABLE IF EXISTS industry_country;

CREATE TABLE IF NOT EXISTS industry_country
(	id SERIAL PRIMARY KEY,
	industry_id INT REFERENCES industries(id),
	country_id INT REFERENCES countries(id),
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

--Code for address table:

DROP TABLE IF EXISTS address CASCADE;

CREATE TABLE IF NOT EXISTS address 
(
	id SERIAL PRIMARY KEY,
	address_line VARCHAR(650) NOT NULL,
	address_number VARCHAR(100) NULL DEFAULT NULL,
	country_id INT REFERENCES countries(id),
	city VARCHAR(100) NOT NULL,
	postal_code VARCHAR(70) NULL DEFAULT NULL,
	region VARCHAR(50) NULL DEFAULT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX address_id_pk
    ON public.address USING btree
    (id ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.address
    CLUSTER ON address_id_pk;

--Code for Company table

DROP TABLE IF EXISTS company CASCADE;

CREATE TABLE IF NOT EXISTS company
(
	id SERIAL PRIMARY KEY,
	lei CHAR(20) NULL DEFAULT NULL,
	legal_name VARCHAR(400) NULL DEFAULT NULL,
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
	name_type VARCHAR(50) NOT NULL,
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

--Code for Company Public Data table

DROP TABLE IF EXISTS company_public_data CASCADE;

CREATE TABLE IF NOT EXISTS company_public_data
(
	id SERIAL PRIMARY KEY,
	company_id INT REFERENCES company(id) ON DELETE CASCADE,
	legal_address_editable BOOLEAN NOT NULL DEFAULT TRUE,
	legal_address_id INT REFERENCES address(id),
	hq_address_editable BOOLEAN NOT NULL DEFAULT TRUE,
	hq_address_id INT REFERENCES address(id),
	
	num_employees INT NULL DEFAULT NULL,
	
	senior_mgmt_gender_ratio_female DOUBLE PRECISION DEFAULT NULL,
	middle_mgmt_gender_ratio_female DOUBLE PRECISION DEFAULT NULL,
	all_employees_gender_ratio_female DOUBLE PRECISION DEFAULT NULL,
	gender_pay_gap_female DOUBLE PRECISION DEFAULT NULL,
	
	executives_visible_race_minority DOUBLE PRECISION DEFAULT NULL,
	board_visible_race_minority DOUBLE PRECISION DEFAULT NULL,
	middle_mgmt_visible_race_minority DOUBLE PRECISION DEFAULT NULL,
	all_employees_visible_race_minority DOUBLE PRECISION DEFAULT NULL,
	
	total_hours_worked INT DEFAULT NULL,
	
	total_turnover_rate DOUBLE PRECISION DEFAULT NULL,
	voluntary_turnover_rate DOUBLE PRECISION DEFAULT NULL,
	involuntary_turnover_rate DOUBLE PRECISION DEFAULT NULL,
	
	DI_on_website BOOLEAN DEFAULT NULL,
	company_offers_training BOOLEAN DEFAULT NULL,
	company_has_social_impact_programs BOOLEAN DEFAULT NULL,
	company_supplier_spending_with_DI DOUBLE PRECISION DEFAULT NULL,
	company_has_program_for_advancing_minorities BOOLEAN DEFAULT NULL,
	
	company_measures_engagement BOOLEAN DEFAULT NULL,
	engagement_survey DOUBLE PRECISION DEFAULT NULL,
	engagement_survey_response_rate DOUBLE PRECISION DEFAULT NULL,
	
	fatalities INT DEFAULT NULL,
	sickness_ansense DOUBLE PRECISION DEFAULT NULL,
	total_recordable_injuries INT DEFAULT NULL,

	effective_from TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX company_public_data_id_pk
    ON public.company_public_data USING btree
    (id ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.company_public_data
    CLUSTER ON company_public_data_id_pk;
	
CREATE INDEX company_public_data_company_id_idx
    ON public.company_public_data USING btree
    (company_id ASC NULLS LAST)
    TABLESPACE pg_default;
	
--Code for Company Private Data table

DROP TABLE IF EXISTS company_private_data CASCADE;

CREATE TABLE IF NOT EXISTS company_private_data
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

CREATE UNIQUE INDEX company_private_data_id_pk
    ON public.company_private_data USING btree
    (id ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.company_private_data
    CLUSTER ON company_private_data_id_pk;
	
CREATE INDEX company_private_data_company_id_idx
    ON public.company_private_data USING btree
    (company_id ASC NULLS LAST)
    TABLESPACE pg_default;
	
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

--Code for country Company table

DROP TABLE IF EXISTS company_country;

CREATE TABLE IF NOT EXISTS company_country
(
	company_country_id SERIAL PRIMARY KEY,
	company_id INT REFERENCES company(id) ON DELETE CASCADE,
	country_id INT REFERENCES countries(id),
	legal_jurisdiction BOOLEAN NOT NULL DEFAULT FALSE,
	ticker VARCHAR(10) DEFAULT NULL,
	stock_index VARCHAR(10) DEFAULT NULL,
	is_primary BOOLEAN NOT NULL DEFAULT FALSE,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Company Industry table

DROP TABLE IF EXISTS company_industry;

CREATE TABLE IF NOT EXISTS company_industry
(
	id SERIAL PRIMARY KEY,
	company_id INT REFERENCES company(id) ON DELETE CASCADE,
	industry_id INT REFERENCES industries(id) DEFAULT NULL,
	industry_code INT REFERENCES industry_codes(id) DEFAULT NULL,
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

--Code for Person table

DROP TABLE IF EXISTS person CASCADE;

CREATE TABLE IF NOT EXISTS person
(
	id SERIAL PRIMARY KEY,
	name VARCHAR(135) NOT NULL,
	age SMALLINT NULL DEFAULT NULL,
	birth_year SMALLINT NULL DEFAULT NULL,
	gender INT REFERENCES genders(id) DEFAULT NULL,
	picture VARCHAR(45) NULL DEFAULT NULL,
	race INT REFERENCES races(id) DEFAULT NULL,
	religion INT REFERENCES religions(id) DEFAULT NULL,
	married INT REFERENCES marital_statuses(id) DEFAULT NULL,
	has_kids SMALLINT NULL DEFAULT NULL,
	education_level INT REFERENCES education_levels(id) DEFAULT NULL,
	education_subject INT REFERENCES education_subjects(id) DEFAULT NULL,
	education_institute VARCHAR(100) NULL DEFAULT NULL,
	sexuality VARCHAR(45) NULL DEFAULT NULL,
	visible_disability VARCHAR(45) NULL DEFAULT NULL,
	urban INT NULL DEFAULT NULL,
	website VARCHAR(200) DEFAULT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Person country table

DROP TABLE IF EXISTS person_country;

CREATE TABLE IF NOT EXISTS person_country
(
	person_country_id SERIAL PRIMARY KEY,
	person_id INT REFERENCES person(id) ON DELETE CASCADE,
	country_id INT REFERENCES countries(id),
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for role table

DROP TABLE IF EXISTS role;

CREATE TABLE IF NOT EXISTS role
(
	id SERIAL PRIMARY KEY,
	company_id INT REFERENCES company(id) ON DELETE CASCADE,
	person_id INT REFERENCES person(id) ON DELETE CASCADE,
	is_effective SMALLINT NULL DEFAULT NULL,
	role_type INT REFERENCES role_types(id) NOT NULL,
	title VARCHAR(200) NOT NULL,
	base_salary DOUBLE PRECISION DEFAULT NULL,
	other_incentives DOUBLE PRECISION DEFAULT NULL,
	effective_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);