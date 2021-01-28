-- DICTIONARIES --

-- Code for Address Types table --

DROP TABLE IF EXISTS AddressTypes CASCADE;

CREATE TABLE IF NOT EXISTS AddressTypes
(
	Id INT PRIMARY KEY,
	Name VARCHAR(50) NOT NULL
);

INSERT INTO AddressTypes(Id, Name) VALUES
	(0, 'Legal'),
	(1, 'HQ');

-- Code for industry table --

DROP TABLE IF EXISTS Industries CASCADE;

CREATE TABLE IF NOT EXISTS Industries
(
	Id INT PRIMARY KEY,
	Name VARCHAR(100) NOT NULL
);

INSERT INTO Industries(Id, Name) VALUES
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

DROP TABLE IF EXISTS IndustryCodes CASCADE;

CREATE TABLE IF NOT EXISTS IndustryCodes
(
	Id INT PRIMARY KEY,
	Name VARCHAR(20) NOT NULL
);

INSERT INTO IndustryCodes(Id, Name) VALUES
	(0, 'NACE'),
	(1, 'NAICS'),
	(2, 'Local');

-- Code for Genders table --

DROP TABLE IF EXISTS Genders CASCADE;

CREATE TABLE IF NOT EXISTS Genders
(
	Id INT PRIMARY KEY,
	Name VARCHAR(50) NOT NULL
);

INSERT INTO Genders(Id, Name) VALUES
	(0, 'Female'),
	(1, 'Male'),
	(2, 'Other');
	
-- Code for Race table --

DROP TABLE IF EXISTS Races CASCADE;

CREATE TABLE IF NOT EXISTS Races
(
	Id INT PRIMARY KEY,
	Name VARCHAR(50) NOT NULL
);

INSERT INTO Races(Id, Name) VALUES
	(0, 'Arab'),
	(1, 'Hispanic'),
	(2, 'Black'),
	(3, 'Asian'),
	(4, 'Caucasian'),
	(5, 'Indigenous');
	
-- Code for Religion table --

DROP TABLE IF EXISTS Religions CASCADE;

CREATE TABLE IF NOT EXISTS Religions
(
	Id INT PRIMARY KEY,
	Name VARCHAR(50) NOT NULL
);

INSERT INTO Religions(Id, Name) VALUES
	(0, 'Muslim'),
	(1, 'Christian'),
	(2, 'Hindu'),
	(3, 'Buddhism'),
	(4, 'Judaism'),
	(5, 'Other');
	
-- Code for Role type table --

DROP TABLE IF EXISTS RoleTypes CASCADE;

CREATE TABLE IF NOT EXISTS RoleTypes
(
	Id INT PRIMARY KEY,
	Name VARCHAR(50) NOT NULL
);

INSERT INTO RoleTypes(Id, Name) VALUES
	(1, 'Executive'),
	(2, 'Board'),
	(3, 'Both');
	
-- Code for Regions table --

DROP TABLE IF EXISTS Regions CASCADE;

CREATE TABLE IF NOT EXISTS Regions
(
	Id INT PRIMARY KEY,
	Name VARCHAR(50) NOT NULL
);

INSERT INTO Regions(Id, Name) VALUES
	(0, 'North America'),
	(1, 'South America'),
	(2, 'Central America'),
	(3, 'Europe'),
	(4, 'Oceania'),
	(5, 'Asia'),
	(6, 'Africa'),
	(7, 'Middle East');

-- Code for Education levels table --

DROP TABLE IF EXISTS EducationLevels CASCADE;

CREATE TABLE IF NOT EXISTS EducationLevels
(
	Id INT PRIMARY KEY,
	Name VARCHAR(30) NOT NULL
);

INSERT INTO EducationLevels(Id, Name) VALUES
	(0, 'Elementary'),
	(1, 'High School'),
	(2, 'Bachelor'),
	(3, 'Master'),
	(4, 'Master+');

-- Code for Education subjects table --

DROP TABLE IF EXISTS EducationSubjects CASCADE;

CREATE TABLE IF NOT EXISTS EducationSubjects
(
	Id INT PRIMARY KEY,
	Name VARCHAR(100) NOT NULL
);

INSERT INTO EducationSubjects(Id, Name) VALUES
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

DROP TABLE IF EXISTS Countries CASCADE;

CREATE TABLE IF NOT EXISTS Countries
(
	Id SERIAL PRIMARY KEY,
	Name VARCHAR(45) NOT NULL,
	ISOCode VARCHAR(3) NOT NULL
);

INSERT INTO Countries(Name, ISOCode) VALUES
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

DROP TABLE IF EXISTS FieldTypes CASCADE;
CREATE TABLE IF NOT EXISTS FieldTypes
(
	Id SMALLINT PRIMARY KEY,
	Name VARCHAR(15) NOT NULL
);

INSERT INTO FieldTypes(Id, Name) VALUES
	(0, 'String'),
	(1, 'Boolean'),
	(2, 'Integer'),
	(3, 'Float'),
	(4, 'Date'),
	(5, 'Percentage'),
	(6, 'DropDown'),
	(7, 'Array'),
	(8, 'Person');
	
DROP TABLE IF EXISTS PropertyMetadata CASCADE;
CREATE TABLE IF NOT EXISTS PropertyMetadata
(
	Id SERIAL PRIMARY KEY,
	EntityName VARCHAR(30) NOT NULL,
	PropertyName VARCHAR(30) NOT NULL,
	FrontendName VARCHAR(200) DEFAULT NULL,
	Description VARCHAR(200) DEFAULT NULL,
	FieldType SMALLINT NOT NULL REFERENCES FieldTypes(Id) ON DELETE CASCADE,
	AllowsNull BOOLEAN NOT NULL DEFAULT TRUE,
	IsEditable BOOLEAN NOT NULL DEFAULT TRUE,
	DropDownDictionary VARCHAR(30) DEFAULT NULL,
	ChildrenEntityName VARCHAR(30) DEFAULT NULL,
	RangeLow VARCHAR(15) DEFAULT NULL,
	RangeHigh VARCHAR(15) DEFAULT NULL,
	FieldOrder INT NOT NULL
);

INSERT INTO PropertyMetadata(
	EntityName,
	PropertyName,
	FrontendName,
	Description,
	FieldType,
	AllowsNull,
	IsEditable,
	DropDownDictionary,
	ChildrenEntityName,
	RangeLow,
	RangeHigh,
	FieldOrder) 
	VALUES
	('Company','id',NULL,NULL,2,False,False,NULL,NULL,NULL,NULL,0),
	('Company','lei','LEI','Identifier according to GLEIF database',0,True,True,NULL,NULL,NULL,NULL,1),
	('Company','legalName','Legal Name of company',NULL,0,False,True,NULL,NULL,NULL,NULL,2),
	('Company','keyFinancialsMetrics','Key Financials Metrics',NULL,8,False,True,NULL,'CompanyKeyFinancialsMetrics',NULL,NULL,3),
	('Company','countries','Countries',NULL,7,False,True,NULL,'CompanyCountry',NULL,NULL,4),
	('Company','industries','Industries',NULL,7,False,True,NULL,'CompanyIndustry',NULL,NULL,5),
	('Company','executiveStatistics','Executive statistics',NULL,8,False,True,NULL,'CompanyExecutiveStatistics',NULL,NULL,6),
	('Company','boardStatistics','Board statistics',NULL,8,False,True,NULL,'CompanyBoardStatistics',NULL,NULL,7),
	('Company','diMetrics','DI Metrics',NULL,8,False,True,NULL,'CompanyDIMetrics',NULL,NULL,8),
	('Company','jobMetrics','Job Metrics',NULL,8,False,True,NULL,'CompanyJobMetrics',NULL,NULL,9),
	('Company','raceMetrics','Race Metrics',NULL,8,False,True,NULL,'CompanyRaceMetrics',NULL,NULL,10),
	('Company','genderMetrics','Gender Metrics',NULL,8,False,True,NULL,'CompanyGenderMetrics',NULL,NULL,11),
	('Company','healthMetrics','Health Metrics',NULL,8,False,True,NULL,'CompanyHealthMetrics',NULL,NULL,12),
	('Company','names','Company names',NULL,7,False,True,NULL,'CompanyName',NULL,NULL,13),
	('Company','addresses','Addresses',NULL,7,False,True,NULL,'Address',NULL,NULL,14),
	('Company','roles','Board/Executive roles',NULL,7,False,True,NULL,'Role',NULL,NULL,15),
	('CompanyName','name','Name',NULL,0,False,True,NULL,NULL,NULL,NULL,16),
	('CompanyName','nameType','NameType',NULL,0,False,True,NULL,NULL,NULL,NULL,17),
	('CompanyCountry','country','Country',NULL,6,True,True,'countries',NULL,NULL,NULL,18),
	('CompanyCountry','isoCode',NULL,NULL,0,True,False,NULL,NULL,NULL,NULL,19),
	('CompanyCountry','ticker','Ticker',NULL,0,True,True,NULL,NULL,NULL,NULL,20),
	('Address','addressType','Address type',NULL,6,False,True,'addressTypes',NULL,NULL,NULL,21),
	('Address','isEditable','Is address editable',NULL,1,False,False,NULL,NULL,NULL,NULL,22),
	('Address','streetOne','Street Address 1',NULL,0,True,True,NULL,NULL,NULL,NULL,23),
	('Address','streetTwo','Street Address 2',NULL,0,True,True,NULL,NULL,NULL,NULL,24),
	('Address','postCode','PostCode',NULL,2,True,True,NULL,NULL,NULL,NULL,25),
	('Address','city','City',NULL,0,True,True,NULL,NULL,NULL,NULL,26),
	('Address','state','State',NULL,0,True,True,NULL,NULL,NULL,NULL,27),
	('Address','country','Country',NULL,6,True,True,'countries',NULL,NULL,NULL,28),
	('Address','isoCode',NULL,NULL,0,True,False,NULL,NULL,NULL,NULL,29),
	('CompanyIndustry','industry','Industry name',NULL,6,False,True,'industries',NULL,NULL,NULL,30),
	('CompanyIndustry','industryCode','Industry code source',NULL,6,False,True,'industryCodes',NULL,NULL,NULL,31),
	('CompanyIndustry','tradeDescription','Description of a trade',NULL,0,True,True,NULL,NULL,NULL,NULL,32),
	('CompanyIndustry','isPrimary','Is this trade primary for a company?',NULL,1,True,True,NULL,NULL,NULL,NULL,33),
	('CompanyKeyFinancialsMetrics','employees','Number of Employees',NULL,2,True,True,NULL,NULL,'0',NULL,34),
	('CompanyExecutiveStatistics','femaleRatio','Female Gender Ratio of Executives',NULL,5,True,True,NULL,NULL,'0','100',35),
	('CompanyBoardStatistics','femaleRatio','Female Gender Ratio of Board',NULL,5,True,True,NULL,NULL,'0','100',36),
	('CompanyGenderMetrics','genderRatioSenior','Senior Management female Ratio','Group consists of C-level, ExecutiveDirectors/ExecutiveVicePresidents, 	(SeniorDirectors/SeniorVicePresidents, VicePresidents,% female',5,True,True,NULL,NULL,'0','100',37),
	('CompanyGenderMetrics','genderRatioMiddle','Middle Management female Ratio','Group consists of Directors, Senior Managers,  Managers and Team Leads, % female',5,True,True,NULL,NULL,'0','100',38),
	('CompanyGenderMetrics','genderRatioAll','Percentage of female employees','Group consists of all FTE and Part time employee, % female',5,True,True,NULL,NULL,'0','100',39),
	('CompanyGenderMetrics','genderPayGap','Gender Pay Gap in percentage (female)','% of female gap to men',5,True,True,NULL,NULL,'0','100',40),
	('CompanyGenderMetrics','genderRatioBoard','Board female Ratio','% of female gap to men',5,True,True,NULL,NULL,'0','100',41),
	('CompanyRaceMetrics','raceRatioExececutive','Ratio of visible minority in Executives',NULL,5,True,True,NULL,NULL,'0','100',42),
	('CompanyRaceMetrics','raceRatioBoard','Ratio of visible minority in Board',NULL,5,True,True,NULL,NULL,'0','100',43),
	('CompanyRaceMetrics','raceRatioSenior','Ratio of visible minority in Senior Management','Group consists of C-level, ExecutiveDirectors/ExecutiveVicePresidents,  SeniorDirectors/SeniorVicePresidents, VicePresidents, % minority',5,True,True,NULL,NULL,'0','100',44),
	('CompanyRaceMetrics','raceRatioMiddle','Ratio of visible minority in Middle Management','Group consists of Directors, Senior Managers,  Managers and Team Leads, % minority',5,True,True,NULL,NULL,'0','100',45),
	('CompanyRaceMetrics','raceRatioAll','Ratio of visible minority in all employees','Group consists of all FTE and Part time employee, % minority',5,True,True,NULL,NULL,'0','100',46),
	('CompanyJobMetrics','totalHours','Total hours worked (FTE and contractors)',NULL,2,True,True,NULL,NULL,'0',NULL,47),
	('CompanyJobMetrics','employTurnoverTotal','Total employee turnover rate',NULL,5,True,True,NULL,NULL,'0','100',48),
	('CompanyJobMetrics','employTurnoverVoluntary','Voluntary employee turnover rate',NULL,5,True,True,NULL,NULL,'0','100',49),
	('CompanyJobMetrics','employTurnoverFired','Unvoluntary employee turnover rate',NULL,5,True,True,NULL,NULL,'0','100',50),
	('CompanyJobMetrics','employTraining','Does company offer training and development?',NULL,1,True,True,NULL,NULL,NULL,NULL,51),
	('CompanyDIMetrics','socialProgram','Does company have social impact program(s) in place?',NULL,1,True,True,NULL,NULL,NULL,NULL,52),
	('CompanyDIMetrics','retaliation','Does company have no retaliation policy in place?',NULL,1,True,True,NULL,NULL,NULL,NULL,53),
	('CompanyDIMetrics','supplySpend','Percentage of company''s supplier spending is with D&I suppliers?',NULL,5,True,True,NULL,NULL,'0','100',54),
	('CompanyDIMetrics','valueDISupplySpend','Value of company''s supplier spending with D&I suppliers?',NULL,3,True,True,NULL,NULL,NULL,NULL,55),
	('CompanyDIMetrics','diSupplySpendRevenueRatio','DI supply spend to revenue ratio',NULL,5,True,True,NULL,NULL,'0','100',56),
	('CompanyDIMetrics','mentorProgram','Does company have talent program in place for advancing minority groups',NULL,1,True,True,NULL,NULL,NULL,NULL,57),
	('CompanyDIMetrics','socialEvents','Does company have social events with attendance from different departments?',NULL,1,True,True,NULL,NULL,NULL,NULL,58),
	('CompanyDIMetrics','employEngagement','Does company meassure peoples Engagement/Satisfaction or similar employee metric?',NULL,1,True,True,NULL,NULL,NULL,NULL,59),
	('CompanyDIMetrics','employSatisfactionSurvey','Engagement/Satisfaction Survey - score',NULL,3,True,True,NULL,NULL,'0','100',60),
	('CompanyDIMetrics','employSurveyResponseRate','Engagement/Satisfaction Survey - response rate',NULL,5,True,True,NULL,NULL,'0','100',61),
	('CompanyDIMetrics','diPolicyEstablished','Does company have D&I policy established?',NULL,1,True,True,NULL,NULL,NULL,NULL,62),
	('CompanyDIMetrics','diPublicAvailable','Does company make any D&I policy publicly available?',NULL,1,True,True,NULL,NULL,NULL,NULL,63),
	('CompanyDIMetrics','diWebsite','Does D&I have a specific place on company''s website?',NULL,1,True,True,NULL,NULL,NULL,NULL,64),
	('CompanyDIMetrics','diPosition','Is Head of D&I an established position?',NULL,1,True,True,NULL,NULL,NULL,NULL,65),
	('CompanyDIMetrics','diFTEPosition','Is Head of D&I a sole position?',NULL,1,True,True,NULL,NULL,NULL,NULL,66),
	('CompanyDIMetrics','diPositionExecutive','Is Head of D&I part of Executive team?',NULL,1,True,True,NULL,NULL,NULL,NULL,67),
	('CompanyHealthMetrics','fatalities','Number of Fatalities in a year',NULL,2,True,True,NULL,NULL,'0',NULL,68),
	('CompanyHealthMetrics','sickAbsence','Sickness absence - percentage',NULL,5,True,True,NULL,NULL,'0','100',69),
	('CompanyHealthMetrics','healthTRI','Total recordable injuries (TRIs)',NULL,2,True,True,NULL,NULL,'0',NULL,70),
	('CompanyHealthMetrics','healthTRIR','Total recordable injury rate (TRIR)','TotalHours/HealthTRI',2,True,True,NULL,NULL,'0',NULL,71),
	('Person','name','Name of a person',NULL,0,True,True,NULL,NULL,NULL,NULL,72),
	('Person','age','Age of a person',NULL,2,True,True,NULL,NULL,'0',NULL,73),
	('Person','gender','Gender of a person',NULL,6,True,True,'Genders',NULL,NULL,NULL,74),
	('Person','race','Race/Ethnicity of a person',NULL,6,True,True,'Races',NULL,NULL,NULL,75),
	('Person','religion','Religion of a person',NULL,6,True,True,'Religions',NULL,NULL,NULL,76),
	('Person','married','Is person married?',NULL,1,True,True,NULL,NULL,NULL,NULL,77),
	('Person','kids','Does person have kids?',NULL,1,True,True,NULL,NULL,NULL,NULL,78),
	('Person','highEducation','Highest education level of a person',NULL,6,True,True,'educationLevels',NULL,NULL,NULL,79),
	('Person','educationSubject','Educational subject of a person',NULL,6,True,True,'educationSubjects',NULL,NULL,NULL,80),
	('Person','educationInstitute','Educational Institute of a person',NULL,0,True,True,NULL,NULL,NULL,NULL,81),
	('Person','sexuality','Person''s sexuality',NULL,0,True,True,NULL,NULL,NULL,NULL,82),
	('Person','visibleDisability','Does person have visible disability?',NULL,0,True,True,NULL,NULL,NULL,NULL,83),
	('Person','urban','Did person grow up in Urban location?',NULL,1,True,True,NULL,NULL,NULL,NULL,84),
	('Person','nationalities','Person Nationalities',NULL,7,False,True,NULL,'PersonNationality',NULL,NULL,85),
	('PersonNationality','country','Country',NULL,6,False,True,'Countries',NULL,NULL,NULL,86),
	('PersonNationality','isoCode',NULL,NULL,0,True,False,NULL,NULL,NULL,NULL,87),
	('Role','roleType','Role type',NULL,6,False,True,'RoleTypes',NULL,NULL,NULL,88),
	('Role','title','Title',NULL,0,False,True,NULL,NULL,NULL,NULL,89),
	('Role','baseSalary','Base salary',NULL,3,True,True,NULL,NULL,'0',NULL,90),
	('Role','otherIncentives','Other incentives',NULL,3,True,True,NULL,NULL,'0',NULL,91),
	('Role','jobTenure','Job tenure',NULL,2,True,True,NULL,NULL,'0',NULL,92),
	('Role','personId','Person',NULL,6,False,True,'people',NULL,NULL,NULL,93);


-- DATA TABLES --

--Code for Users table

DROP TABLE IF EXISTS Users;

CREATE TABLE IF NOT EXISTS Users
(
	UserName VARCHAR(50) NOT NULL PRIMARY KEY,
	Password VARCHAR(300) NOT NULL
);

CREATE UNIQUE INDEX UsersIdPk
    ON public.Users USING btree
    (UserName ASC)
    TABLESPACE pg_default;

ALTER TABLE public.Users
    CLUSTER ON UsersIdPk;

--Code for country Demographics table

DROP TABLE IF EXISTS CountryDemographics;

CREATE TABLE IF NOT EXISTS CountryDemographics
(
	Id SERIAL PRIMARY KEY,
	CountryId INT NOT NULL REFERENCES Countries(Id),
	Population FLOAT DEFAULT NULL,
	ImmigrantPopulation FLOAT DEFAULT NULL,
	ImmigrantPercentage FLOAT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Country Economic Powers table

DROP TABLE IF EXISTS CountryEconomicPowers;

CREATE TABLE IF NOT EXISTS CountryEconomicPowers
(
	Id SERIAL PRIMARY KEY,
	CountryId INT NOT NULL REFERENCES Countries(Id),
	GDP DOUBLE PRECISION DEFAULT NULL,
	GDPWorld DOUBLE PRECISION DEFAULT NULL,
	GDPPerCapita DOUBLE PRECISION DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Country Labor Forces table

DROP TABLE IF EXISTS CountryLaborForces;

CREATE TABLE IF NOT EXISTS CountryLaborForces
(
	Id SERIAL PRIMARY KEY,
	CountryId INT NOT NULL REFERENCES Countries(Id),
	LaborForce DOUBLE PRECISION DEFAULT NULL,
	LaborForcePercentage FLOAT DEFAULT NULL,
	MaleUnemployment FLOAT DEFAULT NULL,
	FemaleUnemployment FLOAT DEFAULT NULL,
	AverageIncome FLOAT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for country Gender table

DROP TABLE IF EXISTS CountryGenders;

CREATE TABLE IF NOT EXISTS CountryGenders
(
	Id SERIAL PRIMARY KEY,
	CountryId INT NOT NULL REFERENCES Countries(Id),
	MalePopulationPercentage FLOAT DEFAULT NULL,
	FemalePopulationPercetage FLOAT DEFAULT NULL,
	FemaleWorkForce FLOAT DEFAULT NULL,
	FemaleWorkForcePercentage FLOAT DEFAULT NULL,
	FemaleWorkForcePopulationPercentage FLOAT DEFAULT NULL,
	GenderWorkGap FLOAT DEFAULT NULL,
	GenderHealthGap FLOAT DEFAULT NULL,
	GenderEducationcationGap FLOAT DEFAULT NULL,
	GenderPoliticalGap FLOAT DEFAULT NULL,
	FemalePromotionPolicy FLOAT DEFAULT NULL,
	WomenEducation FLOAT DEFAULT NULL,
	Maternity FLOAT DEFAULT NULL,
	Paternity FLOAT DEFAULT NULL,
	IncomeGap FLOAT DEFAULT NULL,
	WomenViolence FLOAT DEFAULT NULL,
	FemaleParliamentShare FLOAT DEFAULT NULL,
	FemaleMinisterShare FLOAT DEFAULT NULL,
	LifeExpectancyMale FLOAT DEFAULT NULL,
	LifeExpectancyFemale FLOAT DEFAULT NULL,
	MaleSuicide FLOAT DEFAULT NULL,
	FemaleSuicide FLOAT DEFAULT NULL,
	EducatedMaleUnemploy FLOAT DEFAULT NULL,
	EducatedFemaleUnemploy FLOAT DEFAULT NULL,
	FirmsFemaleOwnership FLOAT DEFAULT NULL,
	FirmsFemaleManager FLOAT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Country Ages table


DROP TABLE IF EXISTS CountryAges;

CREATE TABLE IF NOT EXISTS CountryAges
(
	Id SERIAL PRIMARY KEY,
	CountryId INT NOT NULL REFERENCES Countries(Id),
	AgeDistribution19 FLOAT DEFAULT NULL,
	AgeDistribution39 FLOAT DEFAULT NULL,
	AgeDistribution59 FLOAT DEFAULT NULL,
	AgeDistribution79 FLOAT DEFAULT NULL,
	AgeDistributionX FLOAT DEFAULT NULL,
	AgeAverage18 FLOAT DEFAULT NULL,
	AgeParliament FLOAT DEFAULT NULL,
	AgeMinisters FLOAT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Country Religions table

DROP TABLE IF EXISTS CountryReligions;

CREATE TABLE IF NOT EXISTS CountryReligions
(
	Id SERIAL PRIMARY KEY,
	CountryId INT NOT NULL REFERENCES Countries(Id),
	Muslim FLOAT DEFAULT NULL,
	Christian FLOAT DEFAULT NULL,
	Hindu FLOAT DEFAULT NULL,
	Buddishm FLOAT DEFAULT NULL,
	Judaism FLOAT DEFAULT NULL,
	Other FLOAT DEFAULT NULL,
	StateReligion BOOLEAN DEFAULT NULL,
	ReligionFreedom BOOLEAN DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Country Education table

DROP TABLE IF EXISTS CountryEducation;

CREATE TABLE IF NOT EXISTS CountryEducation 
(
	Id SERIAL PRIMARY KEY,
	CountryId INT NOT NULL REFERENCES Countries(Id),
	ElementaryMaleFemale FLOAT DEFAULT NULL,
	ElementaryMale FLOAT DEFAULT NULL,
	ElementaryFemale FLOAT DEFAULT NULL,
	HighSchoolMaleFemale FLOAT DEFAULT NULL,
	HighSchoolMale FLOAT DEFAULT NULL,
	HighSchoolFemale FLOAT DEFAULT NULL,
	BachelorMaleFemale FLOAT DEFAULT NULL,
	BachelorMale FLOAT DEFAULT NULL,
	BachelorFemale FLOAT DEFAULT NULL,
	MasterMaleFemale FLOAT DEFAULT NULL,
	MasterMale FLOAT DEFAULT NULL,
	MasterFemale FLOAT DEFAULT NULL,
	DoctoralMaleFemale FLOAT DEFAULT NULL,
	DoctoralMale FLOAT DEFAULT NULL,
	DoctoralFemale FLOAT DEFAULT NULL,
	ExpectedEducation FLOAT DEFAULT NULL,
	ActualEducation FLOAT DEFAULT NULL,
	EducationPublicFundingGDP FLOAT DEFAULT NULL,
	EducationPublicFundFund FLOAT DEFAULT NULL,
	MaleLiteracy FLOAT DEFAULT NULL,
	FemaleLiteracy FLOAT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Country Race table

DROP TABLE IF EXISTS CountryRaces;

CREATE TABLE IF NOT EXISTS CountryRaces
(
	Id SERIAL PRIMARY KEY,
	CountryId INT NOT NULL REFERENCES Countries(Id),
	Black FLOAT DEFAULT NULL,
	Asian FLOAT DEFAULT NULL,
	Hispanic FLOAT DEFAULT NULL,
	Arab FLOAT DEFAULT NULL,
	Caucasian FLOAT DEFAULT NULL,
	Indegineous FLOAT DEFAULT NULL,
	RaceDiscriminationLaw FLOAT DEFAULT NULL,
	CountryRaceHarassment FLOAT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Country Sexualities table

DROP TABLE IF EXISTS CountrySexualities;

CREATE TABLE IF NOT EXISTS CountrySexualities
(
	Id SERIAL PRIMARY KEY,
	CountryId INT NOT NULL REFERENCES Countries(Id),
	LGBTMarriage BOOLEAN DEFAULT NULL,
	LGBTTolerance FLOAT DEFAULT NULL,
	HomosexualPopulation FLOAT DEFAULT NULL,
	LGBTAdoption BOOLEAN DEFAULT NULL,
	TransgenderRights FLOAT DEFAULT NULL,
	ConversionTherapy FLOAT DEFAULT NULL,
	LGBTMarketing FLOAT DEFAULT NULL,
	LGBTAntiLaws FLOAT DEFAULT NULL,
	LGBTDeathSentences FLOAT DEFAULT NULL,
	LGBTMurders FLOAT DEFAULT NULL,
	LGBTDiscriminationLaw FLOAT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Country Disabilities table

DROP TABLE IF EXISTS CountryDisabilities;

CREATE TABLE IF NOT EXISTS CountryDisabilities
(
	Id SERIAL PRIMARY KEY,
	CountryId INT NOT NULL REFERENCES Countries(Id),
	DisabilityDiscriminationLaw FLOAT DEFAULT NULL,
	Disabled FLOAT DEFAULT NULL,
	Overweight FLOAT DEFAULT NULL,
	HealthFundingGDP FLOAT DEFAULT NULL,
	HealthType FLOAT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Country Urbanization table

DROP TABLE IF EXISTS CountryUrbanization;

CREATE TABLE IF NOT EXISTS CountryUrbanization 
(
	Id SERIAL PRIMARY KEY,
	CountryId INT NOT NULL REFERENCES Countries(Id),
	LiveCities FLOAT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Country Political table

DROP TABLE IF EXISTS CountryPolitical;

CREATE TABLE IF NOT EXISTS CountryPolitical
(
	Id SERIAL PRIMARY KEY,
	CountryId INT NOT NULL REFERENCES Countries(Id),
	DemocracyIndex FLOAT DEFAULT NULL,
	CorruptionIndex FLOAT DEFAULT NULL,
	FreeSpeechIndex FLOAT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Country Economic Equalities table

DROP TABLE IF EXISTS CountryEconomicEqualities;

CREATE TABLE IF NOT EXISTS CountryEconomicEqualities 
(
	Id SERIAL PRIMARY KEY,
	CountryId INT NOT NULL REFERENCES Countries(Id),
	Poor FLOAT DEFAULT NULL,
	EqualityIndex FLOAT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Country Utilities table

DROP TABLE IF EXISTS CountryUtilities;

CREATE TABLE IF NOT EXISTS CountryUtilities 
(
	Id SERIAL PRIMARY KEY,
	CountryId INT NOT NULL REFERENCES Countries(Id),
	SlumsPopulation FLOAT DEFAULT NULL,
	AccessToElectricity FLOAT DEFAULT NULL,
	AccessToDrinkingWater FLOAT DEFAULT NULL,
	AccessToSanitation FLOAT DEFAULT NULL,
	AccessToHandwashing FLOAT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Country Infrastructures table

DROP TABLE IF EXISTS CountryInfrastructures;

CREATE TABLE IF NOT EXISTS CountryInfrastructures
(
	Id SERIAL PRIMARY KEY,
	CountryId INT NOT NULL REFERENCES Countries(Id),
	InternetUse FLOAT DEFAULT NULL,
	CellularSubscriptions FLOAT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Country Labor And Social Protection table

DROP TABLE IF EXISTS CountryLaborAndSocialProtection;

CREATE TABLE IF NOT EXISTS CountryLaborAndSocialProtection 
(
	Id SERIAL PRIMARY KEY,
	CountryId INT NOT NULL REFERENCES Countries(Id),
	FemaleManagementPercentage FLOAT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Country Private Sectors and Trades table

DROP TABLE IF EXISTS CountryPrivateSectorsAndTrades;

CREATE TABLE IF NOT EXISTS CountryPrivateSectorsAndTrades 
(
	Id SERIAL PRIMARY KEY,
	CountryId INT NOT NULL REFERENCES Countries(Id),
	CostOfBusiness FLOAT DEFAULT NULL,
	FirmsBribery FLOAT DEFAULT NULL,
	EaseOfBusiness FLOAT DEFAULT NULL,
	FirmsTraining FLOAT DEFAULT NULL,
	StartupBusiness FLOAT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Country Public Sectors table

DROP TABLE IF EXISTS CountryPublicSectors;

CREATE TABLE IF NOT EXISTS CountryPublicSectors 
(
	Id SERIAL PRIMARY KEY,
	CountryId INT NOT NULL REFERENCES Countries(Id),
	HumanCapital FLOAT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for CountryIndustries table

DROP TABLE IF EXISTS CountryIndustries;

CREATE TABLE IF NOT EXISTS CountryIndustries
(	
	Id SERIAL PRIMARY KEY,
	IndustryId INT NOT NULL REFERENCES Industries(Id),
	CountryId INT REFERENCES Countries(Id),
	Employees INT DEFAULT NULL,
	AveragePay FLOAT DEFAULT NULL,
	Retention FLOAT DEFAULT NULL,
	FlexibleHours FLOAT DEFAULT NULL,
	Education_spend FLOAT DEFAULT NULL,
	Race FLOAT DEFAULT NULL,
	Age FLOAT DEFAULT NULL,
	Harassment FLOAT DEFAULT NULL,
	Gender FLOAT DEFAULT NULL,
	Maternity FLOAT DEFAULT NULL,
	Paternity FLOAT DEFAULT NULL,
	LBGT FLOAT DEFAULT NULL,
	Disabled FLOAT DEFAULT NULL,
	InjuriesNonFatal FLOAT DEFAULT NULL,
	InjuriesFatal FLOAT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Companies table

DROP TABLE IF EXISTS Companies CASCADE;

CREATE TABLE IF NOT EXISTS Companies
(
	Id SERIAL PRIMARY KEY,
	LEI CHAR(20) NULL DEFAULT NULL,
	LegalName VARCHAR(400) NULL DEFAULT NULL,
	DirectParent INT REFERENCES Companies(Id) DEFAULT NULL,
	UltimateParent INT REFERENCES Companies(Id) DEFAULT NULL,
	EffectiveFrom TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompaniesIdPK
    ON public.companies USING btree
    (Id ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.Companies
    CLUSTER ON CompaniesIdPK;
	
CREATE INDEX CompanyLEIIdx
    ON public.Companies USING hash
    (lei COLLATE pg_catalog."default")
    TABLESPACE pg_default;
	
CREATE INDEX CompaniesLegalNameIdx
    ON public.Companies USING btree
    (LegalName COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;

--Code for Addresses table:

DROP TABLE IF EXISTS Addresses CASCADE;

CREATE TABLE IF NOT EXISTS Addresses
(
	Id SERIAL PRIMARY KEY,
	AddressType INT NOT NULL REFERENCES AddressTypes(Id),
	CompanyId INT NOT NULL REFERENCES Companies(Id),
	CountryId INT REFERENCES Countries(Id),
	IsEditable BOOLEAN NOT NULL DEFAULT TRUE,
	StreetOne VARCHAR(650) DEFAULT NULL,
	StreetTwo VARCHAR(650) DEFAULT NULL,
	Postcode VARCHAR(70) DEFAULT NULL,
	City VARCHAR(100) DEFAULT NULL,
	State VARCHAR(100) DEFAULT NULL,

	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX AddressesIdPK
    ON public.Addresses USING btree
    (Id ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.Addresses
    CLUSTER ON AddressesIdPK;
	
CREATE INDEX AddressesCompanyIdIdx
    ON public.Addresses USING hash
    (CompanyId)
    TABLESPACE pg_default;

--Code for Company Identities table

DROP TABLE IF EXISTS CompanyIdentities CASCADE;

CREATE TABLE IF NOT EXISTS CompanyIdentities
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	ISIN BIGINT DEFAULT NULL,
	TaxID BIGINT DEFAULT NULL,
	OtherNumber BIGINT DEFAULT NULL,
	OtherLabel VARCHAR(50) DEFAULT NULL,
	EffectiveFrom TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyIdentitiesCompanyIdPK
    ON public.CompanyIdentities USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyIdentities
    CLUSTER ON CompanyIdentitiesCompanyIdPK;

--Code for Company Names table

DROP TABLE IF EXISTS CompanyNames CASCADE;

CREATE TABLE IF NOT EXISTS CompanyNames
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT REFERENCES Companies(Id) ON DELETE CASCADE,
	Name VARCHAR(400) NOT NULL,
	NameType VARCHAR(50) NOT NULL,
	EffectiveFrom TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyNamesIdPk
    ON public.CompanyNames USING btree
    (Id ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyNames
    CLUSTER ON CompanyNamesIdPk;
	
CREATE INDEX CompanyNamesCompanyIdIdx
    ON public.CompanyNames USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

-- Code for Company Urbanization table

DROP TABLE IF EXISTS CompanyUrbanizationMetrics CASCADE;

CREATE TABLE IF NOT EXISTS CompanyUrbanizationMetrics
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	UrbanSites INT DEFAULT NULL,
	RuralSites INT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyUrbanizationMetricsCompanyIdPK
    ON public.CompanyUrbanizationMetrics USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyUrbanizationMetrics
    CLUSTER ON CompanyUrbanizationMetricsCompanyIdPK;

-- Code for Company Health table

DROP TABLE IF EXISTS CompanyHealthMetrics CASCADE;

CREATE TABLE IF NOT EXISTS CompanyHealthMetrics
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	AgeAverage FLOAT DEFAULT NULL,
	Fatalities INT DEFAULT NULL,
	SickAbsence FLOAT DEFAULT NULL,
	HealthTRI INT DEFAULT NULL,
	HealthTRIR INT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyHealthMetricsCompanyIdPK
    ON public.CompanyHealthMetrics USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyHealthMetrics
    CLUSTER ON CompanyHealthMetricsCompanyIdPK;

-- Code for Company Sexuality table

DROP TABLE IF EXISTS CompanySexualityMetrics CASCADE;

CREATE TABLE IF NOT EXISTS CompanySexualityMetrics
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	NonDiscriminationSexuality BOOLEAN DEFAULT NULL,
	SupportDifferentSexuality BOOLEAN DEFAULT NULL,
	LBGTQForum BOOLEAN DEFAULT NULL,
	SexualityData BOOLEAN DEFAULT NULL,
	EffectiveFrom TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanySexualityMetricsCompanyIdPK
    ON public.CompanySexualityMetrics USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanySexualityMetrics
    CLUSTER ON CompanySexualityMetricsCompanyIdPK;

-- Code for Company Nationality table

DROP TABLE IF EXISTS CompanyNationalityMetrics CASCADE;

CREATE TABLE IF NOT EXISTS CompanyNationalityMetrics
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	NationalNumberOperation INT DEFAULT NULL,
	NationalDifferent INT DEFAULT NULL,
	NationalTopFive VARCHAR(100) DEFAULT NULL,
	CultureERG BOOLEAN DEFAULT NULL,
	SupportLanguages BOOLEAN DEFAULT NULL,
	EffectiveFrom TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyNationalityMetricsCompanyIdPK
    ON public.CompanyNationalityMetrics USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyNationalityMetrics
    CLUSTER ON CompanyNationalityMetricsCompanyIdPK;

-- Code for Company Family table

DROP TABLE IF EXISTS CompanyFamilyMetrics CASCADE;

CREATE TABLE IF NOT EXISTS CompanyFamilyMetrics
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	ParentalLeave BOOLEAN DEFAULT NULL,
	ParentalTime INT DEFAULT NULL,
	ParentalGender BOOLEAN DEFAULT NULL,
	EffectiveFrom TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyFamilyMetricsCompanyIdPK
    ON public.CompanyFamilyMetrics USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyFamilyMetrics
    CLUSTER ON CompanyFamilyMetricsCompanyIdPK;

-- Code for Company Sentiment Scores table

DROP TABLE IF EXISTS CompanySentimentScoreMetrics CASCADE;

CREATE TABLE IF NOT EXISTS CompanySentimentScoreMetrics
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	SentimentNegative INT DEFAULT NULL,
	SentimentPositive INT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanySentimentScoreMetricsCompanyIdPK
    ON public.CompanySentimentScoreMetrics USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanySentimentScoreMetrics
    CLUSTER ON CompanySentimentScoreMetricsCompanyIdPK;

-- Code for Company Hierarchy table

DROP TABLE IF EXISTS CompanyHierarchyMetrics CASCADE;

CREATE TABLE IF NOT EXISTS CompanyHierarchyMetrics
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	HierachyLevel INT DEFAULT NULL,
	TownHalls BOOLEAN DEFAULT NULL,
	Intranet BOOLEAN DEFAULT NULL,
	OrganizationalStructure BOOLEAN DEFAULT NULL,
	EffectiveFrom TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyHierarchyMetricsCompanyIdPK
    ON public.CompanyHierarchyMetrics USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyHierarchyMetrics
    CLUSTER ON CompanyHierarchyMetricsCompanyIdPK;

-- Code for Company Job table

DROP TABLE IF EXISTS CompanyJobMetrics CASCADE;

CREATE TABLE IF NOT EXISTS CompanyJobMetrics
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	TotalHours INT DEFAULT NULL,
	JobTenureAverage FLOAT DEFAULT NULL,
	AverageSalary FLOAT DEFAULT NULL,
	MedianSalary FLOAT DEFAULT NULL,
	EmployTurnoverTotal FLOAT DEFAULT NULL,
	EmployTurnoverVoluntary FLOAT DEFAULT NULL,
	EmployTurnoverFired FLOAT DEFAULT NULL,
	EmployTraining BOOLEAN DEFAULT NULL,
	EffectiveFrom TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyJobMetricsCompanyIdPK
    ON public.CompanyJobMetrics USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyJobMetrics
    CLUSTER ON CompanyJobMetricsCompanyIdPK;

-- Code for Company Key Financials table

DROP TABLE IF EXISTS CompanyKeyFinancialsMetrics CASCADE;

CREATE TABLE IF NOT EXISTS CompanyKeyFinancialsMetrics
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	OperatingRevenue FLOAT DEFAULT NULL,
	TotalAssets INT DEFAULT NULL,
	Employees INT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyKeyFinancialsMetricsCompanyIdPK
    ON public.CompanyKeyFinancialsMetrics USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyKeyFinancialsMetrics
    CLUSTER ON CompanyKeyFinancialsMetricsCompanyIdPK;

-- Code for Company Ownerships table

DROP TABLE IF EXISTS CompanyOwnershipMetrics CASCADE;

CREATE TABLE IF NOT EXISTS CompanyOwnershipMetrics
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	MinorityOwnedMajority BOOLEAN DEFAULT NULL,
	MinorityOwned25Percents BOOLEAN DEFAULT NULL,
	WomanOwnedMajority BOOLEAN DEFAULT NULL,
	WomanOwned25Percents BOOLEAN DEFAULT NULL,
	DisabledOwnedMajority BOOLEAN DEFAULT NULL,
	DisabledOwned25Percents BOOLEAN DEFAULT NULL,
	LGBTOwnedMajority BOOLEAN DEFAULT NULL,
	LGBTOwned25Percents BOOLEAN DEFAULT NULL,
	VetranOwnedMajority BOOLEAN DEFAULT NULL,
	VeteranOwned25Percents BOOLEAN DEFAULT NULL,
	EffectiveFrom TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyOwnershipMetricsCompanyIdPK
    ON public.CompanyOwnershipMetrics USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyOwnershipMetrics
    CLUSTER ON CompanyOwnershipMetricsCompanyIdPK;

-- Code for Company Political table

DROP TABLE IF EXISTS CompanyPoliticalMetrics CASCADE;

CREATE TABLE IF NOT EXISTS CompanyPoliticalMetrics
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	NonDiscriminationPolitical BOOLEAN DEFAULT NULL,
	SupportPolitical BOOLEAN DEFAULT NULL,
	PoliticalVote BOOLEAN DEFAULT NULL,
	EffectiveFrom TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyPoliticalMetricsCompanyIdPK
    ON public.CompanyPoliticalMetrics USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyPoliticalMetrics
    CLUSTER ON CompanyPoliticalMetricsCompanyIdPK;

-- Code for Company Genders table

DROP TABLE IF EXISTS CompanyGenderMetrics CASCADE;

CREATE TABLE IF NOT EXISTS CompanyGenderMetrics
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	GenderMale FLOAT DEFAULT NULL,
	GenderOther FLOAT DEFAULT NULL,
	GenderRatioSenior FLOAT DEFAULT NULL,
	GenderRatioMiddle FLOAT DEFAULT NULL,
	GenderRatioAll FLOAT DEFAULT NULL,
	GenderPayGap FLOAT DEFAULT NULL,
	GenderRatioBoard FLOAT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyGenderMetricsCompanyIdPK
    ON public.CompanyGenderMetrics USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyGenderMetrics
    CLUSTER ON CompanyGenderMetricsCompanyIdPK;

-- Code for Company Race table

DROP TABLE IF EXISTS CompanyRaceMetrics CASCADE;

CREATE TABLE IF NOT EXISTS CompanyRaceMetrics
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	RaceBlack FLOAT DEFAULT NULL,
	RaceAsian FLOAT DEFAULT NULL,
	RaceHispanic FLOAT DEFAULT NULL,
	RaceArab FLOAT DEFAULT NULL,
	RaceCaucasian FLOAT DEFAULT NULL,
	RaceIndigenous FLOAT DEFAULT NULL,
	RaceRatioExececutive FLOAT DEFAULT NULL,
	RaceRatioBoard FLOAT DEFAULT NULL,
	RaceRatioSenior FLOAT DEFAULT NULL,
	RaceRatioMiddle FLOAT DEFAULT NULL,
	RaceRatioAll FLOAT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyRaceMetricsCompanyIdPK
    ON public.CompanyRaceMetrics USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyRaceMetrics
    CLUSTER ON CompanyRaceMetricsCompanyIdPK;

-- Code for Company Disability table

DROP TABLE IF EXISTS CompanyDisabilityMetrics CASCADE;

CREATE TABLE IF NOT EXISTS CompanyDisabilityMetrics
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	DisabelTotal INT DEFAULT NULL,
	DisabelMental INT DEFAULT NULL,
	DisabelPhysical INT DEFAULT NULL,
	DisabelProgram BOOLEAN DEFAULT NULL,
	WheelChairAccess BOOLEAN DEFAULT NULL,
	EffectiveFrom TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyDisabilityMetricsCompanyIdPK
    ON public.CompanyDisabilityMetrics USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyDisabilityMetrics
    CLUSTER ON CompanyDisabilityMetricsCompanyIdPK;

-- Code for Company Education table

DROP TABLE IF EXISTS CompanyEducationMetrics CASCADE;

CREATE TABLE IF NOT EXISTS CompanyEducationMetrics
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	ElementaryShare FLOAT DEFAULT NULL,
	HighSchoolShare FLOAT DEFAULT NULL,
	BachelorShare FLOAT DEFAULT NULL,
	MasterShare FLOAT DEFAULT NULL,
	EducationLeaveSupport BOOLEAN DEFAULT NULL,
	EducationSupportProgram BOOLEAN DEFAULT NULL,
	StudentDebt BOOLEAN DEFAULT NULL,
	EffectiveFrom TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyEducationMetricsCompanyIdPK
    ON public.CompanyEducationMetrics USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyEducationMetrics
    CLUSTER ON CompanyEducationMetricsCompanyIdPK;

-- Code for Company Religion table

DROP TABLE IF EXISTS CompanyReligionMetrics CASCADE;

CREATE TABLE IF NOT EXISTS CompanyReligionMetrics
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	MuslimShare FLOAT DEFAULT NULL,
	ChristianShare FLOAT DEFAULT NULL,
	HinduShare FLOAT DEFAULT NULL,
	BuddhismShare FLOAT DEFAULT NULL,
	JudaismShare FLOAT DEFAULT NULL,
	OtherShare FLOAT DEFAULT NULL,
	NonDiscriminationReligion BOOLEAN DEFAULT NULL,
	HolidayReligion BOOLEAN DEFAULT NULL,
	PrayerRoom BOOLEAN DEFAULT NULL,
	EffectiveFrom TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyReligionMetricsCompanyIdPK
    ON public.CompanyReligionMetrics USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyReligionMetrics
    CLUSTER ON CompanyReligionMetricsCompanyIdPK;

-- Code for Company DI metrics table

DROP TABLE IF EXISTS CompanyDIMetrics CASCADE;

CREATE TABLE IF NOT EXISTS CompanyDIMetrics
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	HarassmentPolicy BOOLEAN DEFAULT NULL,
	SocialProgram BOOLEAN DEFAULT NULL,
	Retaliation BOOLEAN DEFAULT NULL,
	SupplySpend FLOAT DEFAULT NULL,
	ValueDISupplySpend FLOAT DEFAULT NULL,
	DISupplySpendRevenueRatio FLOAT DEFAULT NULL,
	MentorProgram BOOLEAN DEFAULT NULL,
	SocialEvents BOOLEAN DEFAULT NULL,
	EmployEngagement BOOLEAN DEFAULT NULL,
	EmploySatisfactionSurvey FLOAT DEFAULT NULL,
	EmploySurveyResponseRate FLOAT DEFAULT NULL,
	DIPolicyEstablished BOOLEAN DEFAULT NULL,
	DIPublicAvailable BOOLEAN DEFAULT NULL,
	DIWebsite BOOLEAN DEFAULT NULL,
	DIPosition BOOLEAN DEFAULT NULL,
	DIFTEPosition BOOLEAN DEFAULT NULL,
	DIPositionExecutive BOOLEAN DEFAULT NULL,
	DIDivision BOOLEAN DEFAULT NULL,
	DICodeConduct BOOLEAN DEFAULT NULL,
	ManagingDiverse BOOLEAN DEFAULT NULL,
	DIComplaint BOOLEAN DEFAULT NULL,
	DISupplyChain BOOLEAN DEFAULT NULL,
	DITalentGoals BOOLEAN DEFAULT NULL,
	DIEarningCall BOOLEAN DEFAULT NULL,
	HolidaySupport VARCHAR(300) DEFAULT NULL,
	EffectiveFrom TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyDIMetricsCompanyIdPK
    ON public.CompanyDIMetrics USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyDIMetrics
    CLUSTER ON CompanyDIMetricsCompanyIdPK;

-- Code for matching table

DROP TABLE IF EXISTS CompanyMatches CASCADE;

CREATE TABLE IF NOT EXISTS CompanyMatches
(
	Id SERIAL PRIMARY KEY,
	Name VARCHAR(400) NOT NULL,
	CompanyId INT NULL DEFAULT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	EffectiveFrom TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyMatchesIdPK
    ON public.CompanyMatches USING btree
    (Id ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyMatches
    CLUSTER ON CompanyMatchesIdPK;
	
CREATE INDEX CompanyMatchesCompanyIdIdx
    ON public.CompanyMatches USING hash
    (CompanyId)
    TABLESPACE pg_default;

--Code for Company Countries table

DROP TABLE IF EXISTS CompanyCountries;

CREATE TABLE IF NOT EXISTS CompanyCountries
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	CountryId INT NOT NULL REFERENCES Countries(Id),
	Ticker VARCHAR(10) DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyCountriesIdPK
    ON public.CompanyCountries USING btree
    (Id ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyCountries
    CLUSTER ON CompanyCountriesIdPK;
	
CREATE INDEX CompanyCountriesCompanyIdIdx
    ON public.CompanyCountries USING hash
    (CompanyId)
    TABLESPACE pg_default;

--Code for Company Legal Information table

DROP TABLE IF EXISTS CompanyLegalInformation;

CREATE TABLE IF NOT EXISTS CompanyLegalInformation
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	LegalForm VARCHAR(80) DEFAULT NULL,
	CompanyPublic BOOLEAN DEFAULT NULL,
	CompanyIndex VARCHAR(80) DEFAULT NULL,
	Status VARCHAR(80) DEFAULT NULL,
	IncorporationDate TIMESTAMP DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyCountriesCompanyIdPK
    ON public.CompanyCountries USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyCountries
    CLUSTER ON CompanyCountriesCompanyIdPK;

--Code for Company Industry table

DROP TABLE IF EXISTS CompanyIndustries;

CREATE TABLE IF NOT EXISTS CompanyIndustries
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	IndustryId INT NOT NULL REFERENCES Industries(Id),
	IndustryCode INT REFERENCES IndustryCodes(Id) NOT NULL,
	TradeDescription VARCHAR(100) DEFAULT NULL,
	IsPrimary BOOLEAN DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

--Code for Company Executive Statistics table

DROP TABLE IF EXISTS CompanyExecutiveStatistics CASCADE;

CREATE TABLE IF NOT EXISTS CompanyExecutiveStatistics
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	MembersNumber INT NOT NULL,
	SalaryAverage FLOAT DEFAULT NULL,
	SalaryMean FLOAT DEFAULT NULL,
	FemaleRatio FLOAT DEFAULT NULL,
	ArabPercentage FLOAT DEFAULT NULL,
	HispanicPercentage FLOAT DEFAULT NULL,
	BlackPercentage FLOAT DEFAULT NULL,
	AsianPercentage FLOAT DEFAULT NULL,
	CaucasianPercentage FLOAT DEFAULT NULL,
	IndigenousPercentage FLOAT DEFAULT NULL,
	AverageAge FLOAT DEFAULT NULL,
	AverageEducationLength FLOAT DEFAULT NULL,
	Height FLOAT DEFAULT NULL,
	Weight FLOAT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyExecutiveStatisticsCompanyIdPK
    ON public.CompanyExecutiveStatistics USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyExecutiveStatistics
    CLUSTER ON CompanyExecutiveStatisticsCompanyIdPK;

--Code for Company Board Statistics table

DROP TABLE IF EXISTS CompanyBoardStatistics CASCADE;

CREATE TABLE IF NOT EXISTS CompanyBoardStatistics
(
	Id SERIAL PRIMARY KEY,
	CompanyId INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	MembersNumber INT NOT NULL,
	SalaryAverage FLOAT DEFAULT NULL,
	SalaryMean FLOAT DEFAULT NULL,
	FemaleRatio FLOAT DEFAULT NULL,
	ArabPercentage FLOAT DEFAULT NULL,
	HispanicPercentage FLOAT DEFAULT NULL,
	BlackPercentage FLOAT DEFAULT NULL,
	AsianPercentage FLOAT DEFAULT NULL,
	CaucasianPercentage FLOAT DEFAULT NULL,
	IndigenousPercentage FLOAT DEFAULT NULL,
	AverageAge FLOAT DEFAULT NULL,
	AverageEducationLength FLOAT DEFAULT NULL,
	Height FLOAT DEFAULT NULL,
	Weight FLOAT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX CompanyBoardStatisticsCompanyIdPK
    ON public.CompanyBoardStatistics USING btree
    (CompanyId ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.CompanyBoardStatistics
    CLUSTER ON CompanyBoardStatisticsCompanyIdPK;

--Code for Person table

DROP TABLE IF EXISTS People CASCADE;

CREATE TABLE IF NOT EXISTS People
(
	Id SERIAL PRIMARY KEY,
	Name VARCHAR(135) NOT NULL,
	RandomName VARCHAR(135) DEFAULT NULL,
	Age SMALLINT NULL DEFAULT NULL,
	Gender INT REFERENCES Genders(Id) DEFAULT NULL,
	Race INT REFERENCES Races(id) DEFAULT NULL,
	Religion INT REFERENCES Religions(id) DEFAULT NULL,
	Married BOOLEAN DEFAULT NULL,
	Kids BOOLEAN DEFAULT NULL,
	HighEducation INT REFERENCES EducationLevels(id) DEFAULT NULL,
	EducationSubject INT REFERENCES EducationSubjects(id) DEFAULT NULL,
	EducationInstitute VARCHAR(100) NULL DEFAULT NULL,
	Sexuality VARCHAR(45) NULL DEFAULT NULL,
	VisibleDisability VARCHAR(45) NULL DEFAULT NULL,
	Urban BOOLEAN NULL DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX PeopleIdPK
    ON public.People USING btree
    (Id ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.People
    CLUSTER ON PeopleIdPK;
	
CREATE INDEX PeopleNameIdx
    ON public.People USING hash
    (Name COLLATE pg_catalog."default")
    TABLESPACE pg_default;

--Code for Person nationalities table

DROP TABLE IF EXISTS PersonNationalities;

CREATE TABLE IF NOT EXISTS PersonNationalities
(
	Id SERIAL PRIMARY KEY,
	PersonId INT NOT NULL REFERENCES People(Id) ON DELETE CASCADE,
	CountryId INT NOT NULL REFERENCES Countries(Id),
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX PersonNationalitiesIdPK
    ON public.PersonNationalities USING btree
    (Id ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.PersonNationalities
    CLUSTER ON PersonNationalitiesIdPK;
	
CREATE INDEX PersonNationalitiesPersonIdx
    ON public.PersonNationalities USING hash
    (PersonId)
    TABLESPACE pg_default;

--Code for role table

DROP TABLE IF EXISTS Roles;

CREATE TABLE IF NOT EXISTS Roles
(
	Id SERIAL PRIMARY KEY,
	Companyid INT NOT NULL REFERENCES Companies(Id) ON DELETE CASCADE,
	PersonId INT NOT NULL REFERENCES People(Id) ON DELETE CASCADE,
	RoleType INT NOT NULL REFERENCES RoleTypes(Id),
	Title VARCHAR(200) NOT NULL,
	BaseSalary DOUBLE PRECISION DEFAULT NULL,
	OtherIncentives DOUBLE PRECISION DEFAULT NULL,
	JobTenure INT DEFAULT NULL,
	EffectiveFrom TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX RolesIdPK
    ON public.Roles USING btree
    (Id ASC NULLS LAST)
    TABLESPACE pg_default;

ALTER TABLE public.Roles
    CLUSTER ON RolesIdPK;
	
CREATE INDEX RolesPersonIdx
    ON public.Roles USING btree
    (PersonId)
    TABLESPACE pg_default;