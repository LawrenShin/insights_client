using System;
using DigitalInsights.DB.Common.Enums;
using DigitalInsights.DB.Silver.Entities;
using DigitalInsights.DB.Silver.Entities.CountryData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;

namespace DigitalInsights.DB.Silver
{
    public partial class SilverContext : DbContext
    {
        private ILoggerFactory _loggerFactory;

        public SilverContext()
        {
        }

        public SilverContext(DbContextOptions<SilverContext> options)
            : base(options)
        {
        }

        public SilverContext(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyCountry> CompanyCountries { get; set; }
        public virtual DbSet<CompanyIndustry> CompanyIndustries { get; set; }
        public virtual DbSet<CompanyMatch> CompanyMatches { get; set; }
        public virtual DbSet<CompanyName> CompanyNames { get; set; }
        public virtual DbSet<CompanyPrivateData> CompanyPrivateData { get; set; }
        public virtual DbSet<CompanyPublicData> CompanyPublicData { get; set; }
        public virtual DbSet<Entities.CompanyQuestion> CompanyQuestionnaires { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CountryAge> CountryAges { get; set; }
        public virtual DbSet<CountryDemographics> CountryDemographics { get; set; }
        public virtual DbSet<CountryDisability> CountryDisabilities { get; set; }
        public virtual DbSet<CountryEconomy> CountryEconomies { get; set; }
        public virtual DbSet<CountryEdu> CountryEdus { get; set; }
        public virtual DbSet<CountryGender> CountryGenders { get; set; }
        public virtual DbSet<CountryPolitical> CountryPoliticals { get; set; }
        public virtual DbSet<CountryRace> CountryRaces { get; set; }
        public virtual DbSet<CountryReligion> CountryReligions { get; set; }
        public virtual DbSet<CountrySex> CountrySexes { get; set; }
        public virtual DbSet<CountryUrban> CountryUrbans { get; set; }
        public virtual DbSet<Entities.EducationLevel> EducationLevels { get; set; }
        public virtual DbSet<Entities.EducationSubject> EducationSubjects { get; set; }
        public virtual DbSet<Entities.Industry> Industries { get; set; }
        public virtual DbSet<IndustryCountry> IndustryCountries { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PersonCountry> PersonCountries { get; set; }
        public virtual DbSet<Entities.Region> Regions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=di-dev.c19yqc3su48v.us-east-2.rds.amazonaws.com;Port=5432;Database=DI-silver;Username=postgres;Password=2wsx##edc");

                if (_loggerFactory != null) {
                    optionsBuilder.UseLoggerFactory(_loggerFactory);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("address");

                entity.HasIndex(e => e.Id, "address_id_pk")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddressLine)
                    .IsRequired()
                    .HasMaxLength(650)
                    .HasColumnName("address_line");

                entity.Property(e => e.AddressNumber)
                    .HasMaxLength(100)
                    .HasColumnName("address_number")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("city");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(70)
                    .HasColumnName("postal_code")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.Region)
                    .HasMaxLength(50)
                    .HasColumnName("region")
                    .HasDefaultValueSql("NULL::character varying");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("address_country_id_fkey");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.HasIndex(e => e.Id, "company_id_pk")
                    .IsUnique();

                entity.HasIndex(e => e.LegalName, "company_legal_name_idx");

                entity.HasIndex(e => e.Lei, "company_lei_idx")
                    .HasMethod("hash");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LegalName)
                    .HasMaxLength(400)
                    .HasColumnName("legal_name")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.Lei)
                    .HasMaxLength(20)
                    .HasColumnName("lei")
                    .HasDefaultValueSql("NULL::bpchar")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<CompanyCountry>(entity =>
            {
                entity.ToTable("company_country");

                entity.Property(e => e.CompanyCountryId).HasColumnName("company_country_id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsPrimary).HasColumnName("is_primary");

                entity.Property(e => e.LegalJurisdiction).HasColumnName("legal_jurisdiction");

                entity.Property(e => e.StockIndex)
                    .HasMaxLength(10)
                    .HasColumnName("stock_index")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.Ticker)
                    .HasMaxLength(10)
                    .HasColumnName("ticker")
                    .HasDefaultValueSql("NULL::character varying");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyCountries)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("company_country_company_id_fkey");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CompanyCountries)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("company_country_country_id_fkey");
            });

            modelBuilder.Entity<CompanyIndustry>(entity =>
            {
                entity.ToTable("company_industry");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                var industryCodeConverter = new ValueConverter<IndustryCode?, int?>(
                    v => v != null ? (int)v : null,
                    v => v != null ? (IndustryCode)v : null
                    );

                entity.Property(e => e.IndustryCode)
                    .HasColumnName("industry_code")
                    .HasConversion(industryCodeConverter);

                entity.Property(e => e.IndustryCode).HasColumnName("industry_code");

                var industryConverter = new ValueConverter<Common.Enums.Industry?, int?>(
                    v => v != null ? (int)v : null,
                    v => v != null ? (Common.Enums.Industry)v : null
                    );

                entity.Property(e => e.Industry)
                    .HasColumnName("industry_id")
                    .HasConversion(industryConverter);

                entity.Property(e => e.PrimarySecondary)
                    .HasMaxLength(1)
                    .HasColumnName("primary_secondary");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyIndustries)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("company_industry_company_id_fkey");
            });

            modelBuilder.Entity<CompanyMatch>(entity =>
            {
                entity.ToTable("company_match");

                entity.HasIndex(e => e.Id, "company_match_id_pk")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(400)
                    .HasColumnName("name");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyMatches)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("company_match_company_id_fkey");
            });

            modelBuilder.Entity<CompanyName>(entity =>
            {
                entity.ToTable("company_name");

                entity.HasIndex(e => e.CompanyId, "company_name_company_id_idx");

                entity.HasIndex(e => e.Id, "company_name_id_pk")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(400)
                    .HasColumnName("name");

                entity.Property(e => e.NameType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name_type");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyNames)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("company_name_company_id_fkey");
            });

            modelBuilder.Entity<CompanyPrivateData>(entity =>
            {
                entity.ToTable("company_private_data");

                entity.HasIndex(e => e.CompanyId, "company_private_data_company_id_idx");

                entity.HasIndex(e => e.Id, "company_private_data_id_pk")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BelowNationalAvgIncome).HasColumnName("below_national_avg_income");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.DisabledEmployees).HasColumnName("disabled_employees");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.HierarchyLevel).HasColumnName("hierarchy_level");

                entity.Property(e => e.MedianSalary).HasColumnName("median_salary");

                entity.Property(e => e.RetentionRate).HasColumnName("retention_rate");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyPrivateData)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("company_private_data_company_id_fkey");
            });

            modelBuilder.Entity<CompanyPublicData>(entity =>
            {
                entity.ToTable("company_public_data");

                entity.HasIndex(e => e.CompanyId, "company_public_data_company_id_idx");

                entity.HasIndex(e => e.Id, "company_public_data_id_pk")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AllEmployeesGenderRatioFemale).HasColumnName("all_employees_gender_ratio_female");

                entity.Property(e => e.AllEmployeesVisibleRaceMinority).HasColumnName("all_employees_visible_race_minority");

                entity.Property(e => e.BoardVisibleRaceMinority).HasColumnName("board_visible_race_minority");

                entity.Property(e => e.CompanyHasProgramForAdvancingMinorities).HasColumnName("company_has_program_for_advancing_minorities");

                entity.Property(e => e.CompanyHasSocialImpactPrograms).HasColumnName("company_has_social_impact_programs");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.CompanyMeasuresEngagement).HasColumnName("company_measures_engagement");

                entity.Property(e => e.CompanyOffersTraining).HasColumnName("company_offers_training");

                entity.Property(e => e.CompanySupplierSpendingWithDi).HasColumnName("company_supplier_spending_with_di");

                entity.Property(e => e.DIOnWebsite).HasColumnName("di_on_website");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.EngagementSurvey).HasColumnName("engagement_survey");

                entity.Property(e => e.EngagementSurveyResponseRate).HasColumnName("engagement_survey_response_rate");

                entity.Property(e => e.ExecutivesVisibleRaceMinority).HasColumnName("executives_visible_race_minority");

                entity.Property(e => e.Fatalities).HasColumnName("fatalities");

                entity.Property(e => e.GenderPayGapFemale).HasColumnName("gender_pay_gap_female");

                entity.Property(e => e.HqAddressEditable)
                    .IsRequired()
                    .HasColumnName("hq_address_editable")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.HqAddressId).HasColumnName("hq_address_id");

                entity.Property(e => e.InvoluntaryTurnoverRate).HasColumnName("involuntary_turnover_rate");

                entity.Property(e => e.LegalAddressEditable)
                    .IsRequired()
                    .HasColumnName("legal_address_editable")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.LegalAddressId).HasColumnName("legal_address_id");

                entity.Property(e => e.MiddleMgmtGenderRatioFemale).HasColumnName("middle_mgmt_gender_ratio_female");

                entity.Property(e => e.MiddleMgmtVisibleRaceMinority).HasColumnName("middle_mgmt_visible_race_minority");

                entity.Property(e => e.NumEmployees).HasColumnName("num_employees");

                entity.Property(e => e.SeniorMgmtGenderRatioFemale).HasColumnName("senior_mgmt_gender_ratio_female");

                entity.Property(e => e.SicknessAnsense).HasColumnName("sickness_ansense");

                entity.Property(e => e.TotalHoursWorked).HasColumnName("total_hours_worked");

                entity.Property(e => e.TotalRecordableInjuries).HasColumnName("total_recordable_injuries");

                entity.Property(e => e.TotalTurnoverRate).HasColumnName("total_turnover_rate");

                entity.Property(e => e.VoluntaryTurnoverRate).HasColumnName("voluntary_turnover_rate");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyPublicData)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("company_public_data_company_id_fkey");

                entity.HasOne(d => d.HqAddress)
                    .WithMany(p => p.CompanyPublicDatumHqAddresses)
                    .HasForeignKey(d => d.HqAddressId)
                    .HasConstraintName("company_public_data_hq_address_id_fkey");

                entity.HasOne(d => d.LegalAddress)
                    .WithMany(p => p.CompanyPublicDatumLegalAddresses)
                    .HasForeignKey(d => d.LegalAddressId)
                    .HasConstraintName("company_public_data_legal_address_id_fkey");
            });

            modelBuilder.Entity<Entities.CompanyQuestion>(entity =>
            {
                entity.ToTable("company_questionnaire");

                entity.HasIndex(e => e.CompanyId, "company_questionnaire_company_id_idx");

                entity.HasIndex(e => e.Id, "company_questionnaire_id_pk")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Answer).HasColumnName("answer");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                var converter = new ValueConverter<Common.Enums.CompanyQuestion, int>(
                    v => (int)v,
                    v => (Common.Enums.CompanyQuestion)v
                    );

                entity.Property(e => e.Question)
                    .HasColumnName("question")
                    .HasConversion(converter);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyQuestionnaires)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("company_questionnaire_company_id_fkey");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("countries");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasColumnName("code");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<CountryAge>(entity =>
            {
                entity.ToTable("country_age");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Avg18).HasColumnName("avg18");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.Dis19).HasColumnName("dis19");

                entity.Property(e => e.Dis39).HasColumnName("dis39");

                entity.Property(e => e.Dis59).HasColumnName("dis59");

                entity.Property(e => e.Dis70).HasColumnName("dis70");

                entity.Property(e => e.Disx).HasColumnName("disx");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Ministers).HasColumnName("ministers");

                entity.Property(e => e.Parliament).HasColumnName("parliament");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryAges)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("country_age_country_id_fkey");
            });

            modelBuilder.Entity<CountryDemographics>(entity =>
            {
                entity.ToTable("country_demographics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ImmigrantPercent).HasColumnName("immigrant_percent");

                entity.Property(e => e.ImmigrantPop).HasColumnName("immigrant_pop");

                entity.Property(e => e.Population).HasColumnName("population");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryDemographics)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("country_demographics_country_id_fkey");
            });

            modelBuilder.Entity<CountryDisability>(entity =>
            {
                entity.ToTable("country_disability");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.DiscriminationLaw).HasColumnName("discrimination_law");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.HealthFundingGdp).HasColumnName("health_funding_gdp");

                entity.Property(e => e.HealthFundingType).HasColumnName("health_funding_type");

                entity.Property(e => e.Overweight).HasColumnName("overweight");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryDisabilities)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("country_disability_country_id_fkey");
            });

            modelBuilder.Entity<CountryEconomy>(entity =>
            {
                entity.ToTable("country_economy");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AvgIncome).HasColumnName("avg_income");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.EqualityLevel).HasColumnName("equality_level");

                entity.Property(e => e.FemaleUnemploy).HasColumnName("female_unemploy");

                entity.Property(e => e.Gdp).HasColumnName("gdp");

                entity.Property(e => e.GdpPerCapita).HasColumnName("gdp_per_capita");

                entity.Property(e => e.GdpWorld).HasColumnName("gdp_world");

                entity.Property(e => e.LabourForce).HasColumnName("labour_force");

                entity.Property(e => e.LabourForcePercent).HasColumnName("labour_force_percent");

                entity.Property(e => e.MaleUnemploy).HasColumnName("male_unemploy");

                entity.Property(e => e.Poor).HasColumnName("poor");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryEconomies)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("country_economy_country_id_fkey");
            });

            modelBuilder.Entity<CountryEdu>(entity =>
            {
                entity.ToTable("country_edu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActualEducation).HasColumnName("actual_education");

                entity.Property(e => e.BachelorFemale).HasColumnName("bachelor_female");

                entity.Property(e => e.BachelorMale).HasColumnName("bachelor_male");

                entity.Property(e => e.BachelorMf).HasColumnName("bachelor_mf");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ElementaryFemale).HasColumnName("elementary_female");

                entity.Property(e => e.ElementaryMale).HasColumnName("elementary_male");

                entity.Property(e => e.ElementaryMf).HasColumnName("elementary_mf");

                entity.Property(e => e.ExpectedEducation).HasColumnName("expected_education");

                entity.Property(e => e.HighSchoolFemale).HasColumnName("high_school_female");

                entity.Property(e => e.HighSchoolMale).HasColumnName("high_school_male");

                entity.Property(e => e.HighSchoolMf).HasColumnName("high_school_mf");

                entity.Property(e => e.MasterFemale).HasColumnName("master_female");

                entity.Property(e => e.MasterMale).HasColumnName("master_male");

                entity.Property(e => e.MasterMf).HasColumnName("master_mf");

                entity.Property(e => e.PublicFundFund).HasColumnName("public_fund_fund");

                entity.Property(e => e.PublicFundingGdp).HasColumnName("public_funding_gdp");

                entity.Property(e => e.TotalMf).HasColumnName("total_mf");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryEdus)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("country_edu_country_id_fkey");
            });

            modelBuilder.Entity<CountryGender>(entity =>
            {
                entity.ToTable("country_gender");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FemaleMinisterShare).HasColumnName("female_minister_share");

                entity.Property(e => e.FemaleParliamentShare).HasColumnName("female_parliament_share");

                entity.Property(e => e.FemalePop).HasColumnName("female_pop");

                entity.Property(e => e.FemalePromotionPolicy).HasColumnName("female_promotion_policy");

                entity.Property(e => e.FemaleWorkForce).HasColumnName("female_work_force");

                entity.Property(e => e.FemaleWorkForcePercent).HasColumnName("female_work_force_percent");

                entity.Property(e => e.FemaleWorkForcePercentPop).HasColumnName("female_work_force_percent_pop");

                entity.Property(e => e.GenderEduGap).HasColumnName("gender_edu_gap");

                entity.Property(e => e.GenderHealthGap).HasColumnName("gender_health_gap");

                entity.Property(e => e.GenderPolGap).HasColumnName("gender_pol_gap");

                entity.Property(e => e.GenderWorkGap).HasColumnName("gender_work_gap");

                entity.Property(e => e.IncomeGap).HasColumnName("income_gap");

                entity.Property(e => e.LifeFemale).HasColumnName("life_female");

                entity.Property(e => e.LifeMale).HasColumnName("life_male");

                entity.Property(e => e.MalePop).HasColumnName("male_pop");

                entity.Property(e => e.MaterintyLeave).HasColumnName("materinty_leave");

                entity.Property(e => e.PaternityLeave).HasColumnName("paternity_leave");

                entity.Property(e => e.WomenEdu).HasColumnName("women_edu");

                entity.Property(e => e.WomenViolence).HasColumnName("women_violence");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryGenders)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("country_gender_country_id_fkey");
            });

            modelBuilder.Entity<CountryPolitical>(entity =>
            {
                entity.ToTable("country_political");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Corruption).HasColumnName("corruption");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.Democracy).HasColumnName("democracy");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FreedomSpeech).HasColumnName("freedom_speech");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryPoliticals)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("country_political_country_id_fkey");
            });

            modelBuilder.Entity<CountryRace>(entity =>
            {
                entity.ToTable("country_race");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Arab).HasColumnName("arab");

                entity.Property(e => e.Asian).HasColumnName("asian");

                entity.Property(e => e.Black).HasColumnName("black");

                entity.Property(e => e.Caucasian).HasColumnName("caucasian");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.DiscriminationLaw).HasColumnName("discrimination_law");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Hispanic).HasColumnName("hispanic");

                entity.Property(e => e.Indegineous).HasColumnName("indegineous");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryRaces)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("country_race_country_id_fkey");
            });

            modelBuilder.Entity<CountryReligion>(entity =>
            {
                entity.ToTable("country_religion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Buddishm).HasColumnName("buddishm");

                entity.Property(e => e.Christian).HasColumnName("christian");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Freedom).HasColumnName("freedom");

                entity.Property(e => e.Hindu).HasColumnName("hindu");

                entity.Property(e => e.Judaism).HasColumnName("judaism");

                entity.Property(e => e.Muslim).HasColumnName("muslim");

                entity.Property(e => e.Other).HasColumnName("other");

                entity.Property(e => e.Statereligion).HasColumnName("statereligion");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryReligions)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("country_religion_country_id_fkey");
            });

            modelBuilder.Entity<CountrySex>(entity =>
            {
                entity.ToTable("country_sex");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.HomosexualPop).HasColumnName("homosexual_pop");

                entity.Property(e => e.HomosexualTolerance).HasColumnName("homosexual_tolerance");

                entity.Property(e => e.SameAdopt).HasColumnName("same_adopt");

                entity.Property(e => e.SameMarriage).HasColumnName("same_marriage");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountrySexes)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("country_sex_country_id_fkey");
            });

            modelBuilder.Entity<CountryUrban>(entity =>
            {
                entity.ToTable("country_urban");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CitiesPop).HasColumnName("cities_pop");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryUrbans)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("country_urban_country_id_fkey");
            });

            modelBuilder.Entity<Entities.Industry>(entity =>
            {
                entity.ToTable("industries");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Entities.Industry>(entity =>
            {
                entity.ToTable("industries");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Entities.EducationLevel>(entity =>
            {
                entity.ToTable("education_levels");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Entities.EducationSubject>(entity =>
            {
                entity.ToTable("education_subjects");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Entities.Region>(entity =>
            {
                entity.ToTable("regions");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<IndustryCountry>(entity =>
            {
                entity.ToTable("industry_country");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AvgPay).HasColumnName("avg_pay");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.DiPledge).HasColumnName("di_pledge");

                entity.Property(e => e.DisabilitiesPledge).HasColumnName("disabilities_pledge");

                entity.Property(e => e.EducationSpend).HasColumnName("education_spend");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FlexibleHoursPledge).HasColumnName("flexible_hours_pledge");

                entity.Property(e => e.HarassmentPledge).HasColumnName("harassment_pledge");

                entity.Property(e => e.IndustryDiversity).HasColumnName("industry_diversity");

                var industryConverter = new ValueConverter<Common.Enums.Industry?, int?>(
                   v => v != null ? (int)v : null,
                   v => v != null ? (Common.Enums.Industry)v : null
                   );

                entity.Property(e => e.Industry)
                    .HasColumnName("industry_id")
                    .HasConversion(industryConverter);

                entity.Property(e => e.LgbtPledge).HasColumnName("lgbt_pledge");

                entity.Property(e => e.MaterintyLeavePledge).HasColumnName("materinty_leave_pledge");

                entity.Property(e => e.NumEmployees).HasColumnName("num_employees");

                entity.Property(e => e.PaternityLeavePledge).HasColumnName("paternity_leave_pledge");

                entity.Property(e => e.RententionRate).HasColumnName("rentention_rate");

                entity.Property(e => e.WomenEmployeedPercent).HasColumnName("women_employeed_percent");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.IndustryCountries)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("industry_country_country_id_fkey");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.BirthYear).HasColumnName("birth_year");

                entity.Property(e => e.EducationInstitute)
                    .HasMaxLength(100)
                    .HasColumnName("education_institute")
                    .HasDefaultValueSql("NULL::character varying");

                var levelConverter = new ValueConverter<Common.Enums.EducationLevel?, int?>(
                    v => v != null ? (int)v : null,
                    v => v != null ? (Common.Enums.EducationLevel)v : null
                    ); 

                entity.Property(e => e.EducationLevel)
                    .HasColumnName("education_level")
                    .HasConversion(levelConverter);

                var subjectConverter = new ValueConverter<Common.Enums.EducationSubject?, int?>(
                    v => v != null ? (int)v : null,
                    v => v != null ? (Common.Enums.EducationSubject)v : null
                    );

                entity.Property(e => e.EducationSubject)
                    .HasColumnName("education_subject")
                    .HasConversion(subjectConverter);

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.HasKids).HasColumnName("has_kids");

                var marriedConverter = new ValueConverter<MaritalStatus?, int?>(
                    v => v != null ? (int)v : null,
                    v => v != null ? (MaritalStatus)v : null
                    );

                entity.Property(e => e.Married)
                    .HasColumnName("married")
                    .HasConversion(marriedConverter);

                entity.Property(e => e.Married).HasColumnName("married");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(135)
                    .HasColumnName("name");

                entity.Property(e => e.Picture)
                    .HasMaxLength(45)
                    .HasColumnName("picture")
                    .HasDefaultValueSql("NULL::character varying");

                var raceConverter = new ValueConverter<Race?, int?>(
                    v => v != null ? (int)v : null,
                    v => v != null ? (Race)v : null
                    );

                entity.Property(e => e.Race)
                    .HasColumnName("race")
                    .HasConversion(raceConverter);

                var religionConverter = new ValueConverter<Religion?, int?>(
                    v => v != null ? (int)v : null,
                    v => v != null ? (Religion)v : null
                    );

                entity.Property(e => e.Religion)
                    .HasColumnName("religion")
                    .HasConversion(religionConverter);

                entity.Property(e => e.Sexuality)
                    .HasMaxLength(45)
                    .HasColumnName("sexuality")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.Urban).HasColumnName("urban");

                entity.Property(e => e.VisibleDisability)
                    .HasMaxLength(45)
                    .HasColumnName("visible_disability")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.Website)
                    .HasMaxLength(200)
                    .HasColumnName("website")
                    .HasDefaultValueSql("NULL::character varying");
            });

            modelBuilder.Entity<PersonCountry>(entity =>
            {
                entity.ToTable("person_country");

                entity.Property(e => e.PersonCountryId).HasColumnName("person_country_id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.PersonCountries)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("person_country_country_id_fkey");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonCountries)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("person_country_person_id_fkey");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BaseSalary).HasColumnName("base_salary");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effective_from")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsEffective).HasColumnName("is_effective");

                entity.Property(e => e.OtherIncentives).HasColumnName("other_incentives");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.RoleType).HasColumnName("role_type");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("title");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("role_company_id_fkey");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("role_person_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
