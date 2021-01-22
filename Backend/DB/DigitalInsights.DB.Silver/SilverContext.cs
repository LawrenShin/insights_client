using System;
using DigitalInsights.DB.Common.Enums;
using DigitalInsights.DB.Silver.Entities;
using DigitalInsights.DB.Silver.Entities.CompanyData;
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
        public virtual DbSet<CompanyBoardStatistics> CompanyBoardStatistics { get; set; }
        public virtual DbSet<CompanyCountry> CompanyCountries { get; set; }
        public virtual DbSet<CompanyDIMetrics> CompanyDIMetrics { get; set; }
        public virtual DbSet<CompanyDisabilityMetrics> CompanyDisabilityMetrics { get; set; }
        public virtual DbSet<CompanyEducationMetrics> CompanyEducationMetrics { get; set; }
        public virtual DbSet<CompanyExecutiveStatistics> CompanyExecutiveStatistics { get; set; }
        public virtual DbSet<CompanyFamilyMetrics> CompanyFamilyMetrics { get; set; }
        public virtual DbSet<CompanyGenderMetrics> CompanyGenderMetrics { get; set; }
        public virtual DbSet<CompanyHealthMetrics> CompanyHealthMetrics { get; set; }
        public virtual DbSet<CompanyHierarchyMetrics> CompanyHierarchyMetrics { get; set; }
        public virtual DbSet<CompanyIdentity> CompanyIdentities { get; set; }
        public virtual DbSet<CompanyIndustry> CompanyIndustries { get; set; }
        public virtual DbSet<CompanyJobMetrics> CompanyJobMetrics { get; set; }
        public virtual DbSet<CompanyKeyFinancialsMetrics> CompanyKeyFinancialsMetrics { get; set; }
        public virtual DbSet<CompanyLegalInformation> CompanyLegalInformations { get; set; }
        public virtual DbSet<CompanyMatch> CompanyMatches { get; set; }
        public virtual DbSet<CompanyName> CompanyNames { get; set; }
        public virtual DbSet<CompanyNationalityMetrics> CompanyNationalityMetrics { get; set; }
        public virtual DbSet<CompanyOwnershipMetrics> CompanyOwnershipMetrics { get; set; }
        public virtual DbSet<CompanyPoliticalMetrics> CompanyPoliticalMetrics { get; set; }
        public virtual DbSet<CompanyRaceMetrics> CompanyRaceMetrics { get; set; }
        public virtual DbSet<CompanyReligionMetrics> CompanyReligionMetrics { get; set; }
        public virtual DbSet<CompanySentimentScoreMetrics> CompanySentimentScoreMetrics { get; set; }
        public virtual DbSet<CompanySexualityMetrics> CompanySexualityMetrics { get; set; }
        public virtual DbSet<CompanyUrbanizationMetrics> CompanyUrbanizationMetrics { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CountryAge> CountryAges { get; set; }
        public virtual DbSet<CountryDemographics> CountryDemographics { get; set; }
        public virtual DbSet<CountryDisability> CountryDisabilities { get; set; }
        public virtual DbSet<CountryEconomicEquality> CountryEconomicEqualities { get; set; }
        public virtual DbSet<CountryEconomicPower> CountryEconomicPowers { get; set; }
        public virtual DbSet<CountryEducation> CountryEducations { get; set; }
        public virtual DbSet<CountryGender> CountryGenders { get; set; }
        public virtual DbSet<CountryIndustry> CountryIndustries { get; set; }
        public virtual DbSet<CountryInfrastructure> CountryInfrastructures { get; set; }
        public virtual DbSet<CountryLaborAndSocialProtection> CountryLaborAndSocialProtections { get; set; }
        public virtual DbSet<CountryLaborForce> CountryLaborForces { get; set; }
        public virtual DbSet<CountryPolitical> CountryPoliticals { get; set; }
        public virtual DbSet<CountryPrivateSectorAndTrade> CountryPrivateSectorsAndTrades { get; set; }
        public virtual DbSet<CountryPublicSector> CountryPublicSectors { get; set; }
        public virtual DbSet<CountryRace> CountryRaces { get; set; }
        public virtual DbSet<CountryReligion> CountryReligions { get; set; }
        public virtual DbSet<CountrySexuality> CountrySexualities { get; set; }
        public virtual DbSet<CountryUrbanization> CountryUrbanizations { get; set; }
        public virtual DbSet<CountryUtility> CountryUtilities { get; set; }
        public virtual DbSet<Entities.Industry> Industries { get; set; }
        public virtual DbSet<Entities.EducationLevel> EducationLevels { get; set; }
        public virtual DbSet<Entities.EducationSubject> EducationSubjects { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PropertyMetadata> PropertyMetadata { get; set; }
        public virtual DbSet<PersonNationality> PersonNationalities { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=di-dev.c19yqc3su48v.us-east-2.rds.amazonaws.com;Port=5432;Database=DI-silver-dev;Username=postgres;Password=2wsx##edc");

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
                entity.ToTable("addresses");

                entity.HasIndex(e => e.Id, "addressesidpk")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                var addressTypeConverter = new ValueConverter<Common.Enums.AddressType?, int?>(
                   v => v != null ? (int)v : null,
                   v => v != null ? (Common.Enums.AddressType)v : null
                   );
                entity.Property(e => e.AddressType)
                    .HasColumnName("addresstype")
                    .HasConversion(addressTypeConverter);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("city");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsEditable)
                    .IsRequired()
                    .HasColumnName("iseditable")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.PostCode)
                    .HasMaxLength(70)
                    .HasColumnName("postcode")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.State)
                    .HasMaxLength(100)
                    .HasColumnName("state")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.StreetOne)
                    .IsRequired()
                    .HasMaxLength(650)
                    .HasColumnName("streetone");

                entity.Property(e => e.StreetTwo)
                    .IsRequired()
                    .HasMaxLength(650)
                    .HasColumnName("streettwo");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyAddresses)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("addresses_companyid_fkey");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("addresses_CountryId_fkey");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("companies");

                entity.HasIndex(e => e.Id, "companiesidpk")
                    .IsUnique();

                entity.HasIndex(e => e.LegalName, "companieslegalnameidx");

                entity.HasIndex(e => e.LEI, "companyleiidx")
                    .HasMethod("hash");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DirectParent).HasColumnName("directparent");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LegalName)
                    .HasMaxLength(400)
                    .HasColumnName("legalname")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.LEI)
                    .HasMaxLength(20)
                    .HasColumnName("lei")
                    .HasDefaultValueSql("NULL::bpchar")
                    .IsFixedLength(true);

                entity.Property(e => e.UltimateParent).HasColumnName("ultimateparent");

                entity.HasOne(d => d.DirectParentNavigation)
                    .WithMany(p => p.InverseDirectParentNavigation)
                    .HasForeignKey(d => d.DirectParent)
                    .HasConstraintName("companies_directparent_fkey");

                entity.HasOne(d => d.UltimateParentNavigation)
                    .WithMany(p => p.InverseUltimateParentNavigation)
                    .HasForeignKey(d => d.UltimateParent)
                    .HasConstraintName("companies_ultimateparent_fkey");
            });

            modelBuilder.Entity<CompanyBoardStatistics>(entity =>
            {
                entity.ToTable("companyboardstatistics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ArabPercentage).HasColumnName("arabpercentage");

                entity.Property(e => e.AsianPercentage).HasColumnName("asianpercentage");

                entity.Property(e => e.AverageAge).HasColumnName("averageage");

                entity.Property(e => e.AverageEducationLength).HasColumnName("averageeducationlength");

                entity.Property(e => e.BlackPercentage).HasColumnName("blackpercentage");

                entity.Property(e => e.CaucasianPercentage).HasColumnName("caucasianpercentage");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FemaleRatio).HasColumnName("femaleratio");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.HispanicPercentage).HasColumnName("hispanicpercentage");

                entity.Property(e => e.IndigenousPercentage).HasColumnName("indigenouspercentage");

                entity.Property(e => e.MembersNumber).HasColumnName("membersnumber");

                entity.Property(e => e.SalaryAverage).HasColumnName("salaryaverage");

                entity.Property(e => e.SalaryMean).HasColumnName("salarymean");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyBoardStatistics)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companyboardstatistics_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanyCountry>(entity =>
            {
                entity.ToTable("companycountries");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Ticker)
                    .HasMaxLength(10)
                    .HasColumnName("ticker")
                    .HasDefaultValueSql("NULL::character varying");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyCountries)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companycountries_CompanyId_fkey");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CompanyCountries)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("companycountries_CountryId_fkey");
            });

            modelBuilder.Entity<CompanyDIMetrics>(entity =>
            {
                entity.ToTable("companydimetrics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.DICodeConduct).HasColumnName("dicodeconduct");

                entity.Property(e => e.DIComplaint).HasColumnName("dicomplaint");

                entity.Property(e => e.DIDivision).HasColumnName("didivision");

                entity.Property(e => e.DIEarningCall).HasColumnName("diearningcall");

                entity.Property(e => e.DIFTEPosition).HasColumnName("difteposition");

                entity.Property(e => e.DIPolicyEstablished).HasColumnName("dipolicyestablished");

                entity.Property(e => e.DIPosition).HasColumnName("diposition");

                entity.Property(e => e.DIPositionExecutive).HasColumnName("dipositionexecutive");

                entity.Property(e => e.DIPublicAvailable).HasColumnName("dipublicavailable");

                entity.Property(e => e.DISupplyChain).HasColumnName("disupplychain");

                entity.Property(e => e.DISupplySpendRevenueRatio).HasColumnName("disupplyspendrevenueratio");

                entity.Property(e => e.DITalentGoals).HasColumnName("ditalentgoals");

                entity.Property(e => e.DIWebsite).HasColumnName("diwebsite");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.EmployEngagement).HasColumnName("employengagement");

                entity.Property(e => e.EmploySatisfactionSurvey).HasColumnName("employsatisfactionsurvey");

                entity.Property(e => e.EmploySurveyResponseRate).HasColumnName("employsurveyresponserate");

                entity.Property(e => e.HarassmentPolicy).HasColumnName("harassmentpolicy");

                entity.Property(e => e.HolidaySupport)
                    .HasMaxLength(300)
                    .HasColumnName("holidaysupport")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.ManagingDiverse).HasColumnName("managingdiverse");

                entity.Property(e => e.MentorProgram).HasColumnName("mentorprogram");

                entity.Property(e => e.Retaliation).HasColumnName("retaliation");

                entity.Property(e => e.SocialEvents).HasColumnName("socialevents");

                entity.Property(e => e.SocialProgram).HasColumnName("socialprogram");

                entity.Property(e => e.SupplySpend).HasColumnName("supplyspend");

                entity.Property(e => e.ValueDISupplySpend).HasColumnName("valuedisupplyspend");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyDIMetrics)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companydimetrics_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanyDisabilityMetrics>(entity =>
            {
                entity.ToTable("companydisabilitymetrics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.DisabelMental).HasColumnName("disabelmental");

                entity.Property(e => e.DisabelPhysical).HasColumnName("disabelphysical");

                entity.Property(e => e.DisabelProgram).HasColumnName("disabelprogram");

                entity.Property(e => e.DisabelTotal).HasColumnName("disabeltotal");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.WheelchairAccess).HasColumnName("wheelchairaccess");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyDisabilityMetrics)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companydisabilitymetrics_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanyEducationMetrics>(entity =>
            {
                entity.ToTable("companyeducationmetrics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BachelorShare).HasColumnName("bachelorshare");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.EducationLeaveSupport).HasColumnName("educationleavesupport");

                entity.Property(e => e.EducationSupportProgram).HasColumnName("educationsupportprogram");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ElementaryShare).HasColumnName("elementaryshare");

                entity.Property(e => e.HighschoolShare).HasColumnName("highschoolshare");

                entity.Property(e => e.MasterShare).HasColumnName("mastershare");

                entity.Property(e => e.StudentDebt).HasColumnName("studentdebt");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyEducationMetrics)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companyeducationmetrics_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanyExecutiveStatistics>(entity =>
            {
                entity.ToTable("companyexecutivestatistics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ArabPercentage).HasColumnName("arabpercentage");

                entity.Property(e => e.AsianPercentage).HasColumnName("asianpercentage");

                entity.Property(e => e.AverageAge).HasColumnName("averageage");

                entity.Property(e => e.AverageEducationLength).HasColumnName("averageeducationlength");

                entity.Property(e => e.BlackPercentage).HasColumnName("blackpercentage");

                entity.Property(e => e.CaucasianPercentage).HasColumnName("caucasianpercentage");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FemaleRatio).HasColumnName("femaleratio");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.HispanicPercentage).HasColumnName("hispanicpercentage");

                entity.Property(e => e.IndigenousPercentage).HasColumnName("indigenouspercentage");

                entity.Property(e => e.MembersNumber).HasColumnName("membersnumber");

                entity.Property(e => e.SalaryAverage).HasColumnName("salaryaverage");

                entity.Property(e => e.SalaryMean).HasColumnName("salarymean");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyExecutiveStatistics)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companyexecutivestatistics_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanyFamilyMetrics>(entity =>
            {
                entity.ToTable("companyfamilymetrics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ParentalGender).HasColumnName("parentalgender");

                entity.Property(e => e.ParentalLeave).HasColumnName("parentalleave");

                entity.Property(e => e.ParentalTime).HasColumnName("parentaltime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyFamilyMetrics)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companyfamilymetrics_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanyGenderMetrics>(entity =>
            {
                entity.ToTable("companygendermetrics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
                
                entity.Property(e => e.GenderMale).HasColumnName("gendermale");

                entity.Property(e => e.GenderOther).HasColumnName("genderother");

                entity.Property(e => e.GenderPayGap).HasColumnName("genderpaygap");

                entity.Property(e => e.GenderRatioAll).HasColumnName("genderratioall");

                entity.Property(e => e.GenderRatioBoard).HasColumnName("genderratioboard");

                entity.Property(e => e.GenderRatioMiddle).HasColumnName("genderratiomiddle");

                entity.Property(e => e.GenderRatioSenior).HasColumnName("genderratiosenior");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyGenderMetrics)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companygendermetrics_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanyHealthMetrics>(entity =>
            {
                entity.ToTable("companyhealthmetrics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AgeAverage).HasColumnName("ageaverage");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Fatalities).HasColumnName("fatalities");

                entity.Property(e => e.HealthTRI).HasColumnName("healthtri");

                entity.Property(e => e.HealthTRIR).HasColumnName("healthtrir");

                entity.Property(e => e.SickAbsence).HasColumnName("sickabsence");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyHealthMetrics)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companyhealthmetrics_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanyHierarchyMetrics>(entity =>
            {
                entity.ToTable("companyhierarchymetrics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.HierachyLevel).HasColumnName("hierachylevel");

                entity.Property(e => e.Intranet).HasColumnName("intranet");

                entity.Property(e => e.OrganizationalStructure).HasColumnName("organizationalstructure");

                entity.Property(e => e.TownHalls).HasColumnName("townhalls");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyHierarchyMetrics)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companyhierarchymetrics_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanyIdentity>(entity =>
            {
                entity.ToTable("companyidentities");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ISIN).HasColumnName("isin");

                entity.Property(e => e.OtherLabel)
                    .HasMaxLength(50)
                    .HasColumnName("otherlabel")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.OtherNumber).HasColumnName("othernumber");

                entity.Property(e => e.TaxId).HasColumnName("taxid");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyIdentities)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("CompanyIdentities_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanyIndustry>(entity =>
            {
                entity.ToTable("companyindustries");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                var industryCodeConverter = new ValueConverter<IndustryCode?, int?>(
                    v => v != null ? (int)v : null,
                    v => v != null ? (IndustryCode)v : null
                    );

                entity.Property(e => e.IndustryCode)
                    .HasColumnName("industrycode")
                    .HasConversion(industryCodeConverter);

                var industryConverter = new ValueConverter<Common.Enums.Industry?, int?>(
                    v => v != null ? (int)v : null,
                    v => v != null ? (Common.Enums.Industry)v : null
                    );

                entity.Property(e => e.Industry)
                    .HasColumnName("industryid")
                    .HasConversion(industryConverter);

                entity.Property(e => e.IsPrimary).HasColumnName("isprimary");

                entity.Property(e => e.TradeDescription)
                    .HasMaxLength(100)
                    .HasColumnName("tradedescription")
                    .HasDefaultValueSql("NULL::character varying");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyIndustries)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companyindustries_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanyJobMetrics>(entity =>
            {
                entity.ToTable("companyjobmetrics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AverageSalary).HasColumnName("averagesalary");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.EmployTraining).HasColumnName("employtraining");

                entity.Property(e => e.EmployTurnoverFired).HasColumnName("employturnoverfired");

                entity.Property(e => e.EmployTurnoverTotal).HasColumnName("employturnovertotal");

                entity.Property(e => e.EmployTurnoverVoluntary).HasColumnName("employturnovervoluntary");

                entity.Property(e => e.JobTenureAverage).HasColumnName("jobtenureaverage");

                entity.Property(e => e.MedianSalary).HasColumnName("mediansalary");

                entity.Property(e => e.TotalHours).HasColumnName("totalhours");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyJobMetrics)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companyjobmetrics_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanyKeyFinancialsMetrics>(entity =>
            {
                entity.ToTable("companykeyfinancialsmetrics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Employees).HasColumnName("employees");

                entity.Property(e => e.OperatingRevenue).HasColumnName("operatingrevenue");

                entity.Property(e => e.TotalAssets).HasColumnName("totalassets");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyKeyFinancialsMetrics)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companykeyfinancialsmetrics_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanyLegalInformation>(entity =>
            {
                entity.ToTable("companylegalinformation");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.CompanyIndex)
                    .HasMaxLength(80)
                    .HasColumnName("companyindex")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.CompanyPublic).HasColumnName("companypublic");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IncorporationDate).HasColumnName("incorporationdate");

                entity.Property(e => e.LegalForm)
                    .HasMaxLength(80)
                    .HasColumnName("legalform")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.Status)
                    .HasMaxLength(80)
                    .HasColumnName("status")
                    .HasDefaultValueSql("NULL::character varying");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyLegalInformations)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companylegalinformation_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanyMatch>(entity =>
            {
                entity.ToTable("companymatches");

                entity.HasIndex(e => e.Id, "companymatchesidpk")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(400)
                    .HasColumnName("name");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyMatches)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companymatches_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanyName>(entity =>
            {
                entity.ToTable("companynames");

                entity.HasIndex(e => e.CompanyId, "companynamesCompanyIdidx");

                entity.HasIndex(e => e.Id, "companynamesidpk")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(400)
                    .HasColumnName("name");

                entity.Property(e => e.NameType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nametype");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyNames)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("companynames_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanyNationalityMetrics>(entity =>
            {
                entity.ToTable("companynationalitymetrics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.CultureERG).HasColumnName("cultureerg");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
                
                entity.Property(e => e.NationalDifferent).HasColumnName("nationaldifferent");

                entity.Property(e => e.NationalNumberOpeRation).HasColumnName("nationalnumberoperation");

                entity.Property(e => e.NationalTopFive)
                    .HasMaxLength(100)
                    .HasColumnName("nationaltopfive")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.SupportLanguages).HasColumnName("supportlanguages");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyNationalityMetrics)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companynationalitymetrics_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanyOwnershipMetrics>(entity =>
            {
                entity.ToTable("companyownershipmetrics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.DisabledOwned25Percents).HasColumnName("disabledowned25percents");

                entity.Property(e => e.DisabledOwnedMajority).HasColumnName("disabledownedmajority");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LGBTOwned25Percents).HasColumnName("lgbtowned25percents");

                entity.Property(e => e.LGBTOwnedMajority).HasColumnName("lgbtownedmajority");

                entity.Property(e => e.MinorityOwned25Percents).HasColumnName("minorityowned25percents");

                entity.Property(e => e.MinorityOwnedMajority).HasColumnName("minorityownedmajority");

                entity.Property(e => e.VeteranOwned25Percents).HasColumnName("veteranowned25percents");

                entity.Property(e => e.VetranOwnedMajority).HasColumnName("vetranownedmajority");

                entity.Property(e => e.WomanOwned25Percents).HasColumnName("womanowned25percents");

                entity.Property(e => e.WomanOwnedMajority).HasColumnName("womanownedmajority");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyOwnershipMetrics)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companyownershipmetrics_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanyPoliticalMetrics>(entity =>
            {
                entity.ToTable("companypoliticalmetrics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.NonDiscriminationPolitical).HasColumnName("nondiscriminationpolitical");

                entity.Property(e => e.PoliticalVote).HasColumnName("politicalvote");

                entity.Property(e => e.SupportPolitical).HasColumnName("supportpolitical");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyPoliticalMetrics)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companypoliticalmetrics_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanyRaceMetrics>(entity =>
            {
                entity.ToTable("companyracemetrics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.RaceArab).HasColumnName("racearab");

                entity.Property(e => e.RaceAsian).HasColumnName("raceasian");

                entity.Property(e => e.RaceBlack).HasColumnName("raceblack");

                entity.Property(e => e.RaceCaucasian).HasColumnName("racecaucasian");

                entity.Property(e => e.RaceHispanic).HasColumnName("racehispanic");

                entity.Property(e => e.RaceIndigenous).HasColumnName("raceindigenous");

                entity.Property(e => e.RaceRatioAll).HasColumnName("raceratioall");

                entity.Property(e => e.RaceRatioBoard).HasColumnName("raceratioboard");

                entity.Property(e => e.RaceRatioExececutive).HasColumnName("raceratioexececutive");

                entity.Property(e => e.RaceRatioMiddle).HasColumnName("raceratiomiddle");

                entity.Property(e => e.RaceRatioSenior).HasColumnName("raceratiosenior");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyRaceMetrics)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companyracemetrics_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanyReligionMetrics>(entity =>
            {
                entity.ToTable("companyreligionmetrics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BuddhismShare).HasColumnName("buddhismshare");

                entity.Property(e => e.ChristianShare).HasColumnName("christianshare");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.HinduShare).HasColumnName("hindushare");

                entity.Property(e => e.HolidayReligion).HasColumnName("holidayreligion");

                entity.Property(e => e.JudaismShare).HasColumnName("judaismshare");

                entity.Property(e => e.MuslimShare).HasColumnName("muslimshare");

                entity.Property(e => e.NonDiscriminationReligion).HasColumnName("nondiscriminationreligion");

                entity.Property(e => e.OtherShare).HasColumnName("othershare");

                entity.Property(e => e.PrayerRoom).HasColumnName("prayerroom");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyReligionMetrics)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companyreligionmetrics_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanySentimentScoreMetrics>(entity =>
            {
                entity.ToTable("companysentimentscoremetrics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.SentimentNegative).HasColumnName("sentimentnegative");

                entity.Property(e => e.SentimentPositive).HasColumnName("sentimentpositive");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanySentimentScoreMetrics)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companysentimentscoremetrics_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanySexualityMetrics>(entity =>
            {
                entity.ToTable("companysexualitymetrics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LBGTQForum).HasColumnName("lbgtqforum");

                entity.Property(e => e.NonDiscriminationSexuality).HasColumnName("nondiscriminationsexuality");

                entity.Property(e => e.SexualityData).HasColumnName("sexualitydata");

                entity.Property(e => e.SupportDifferentSexuality).HasColumnName("supportdifferentsexuality");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanySexualityMetrics)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companysexualitymetrics_CompanyId_fkey");
            });

            modelBuilder.Entity<CompanyUrbanizationMetrics>(entity =>
            {
                entity.ToTable("companyurbanizationmetrics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.RuralSites).HasColumnName("ruralsites");

                entity.Property(e => e.UrbanSites).HasColumnName("urbansites");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyUrbanizationMetrics)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("companyurbanizationmetrics_CompanyId_fkey");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("countries");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ISOCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasColumnName("isocode");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<CountryAge>(entity =>
            {
                entity.ToTable("countryages");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AgeAverage18).HasColumnName("ageaverage18");

                entity.Property(e => e.AgeDistribution19).HasColumnName("agedistribution19");

                entity.Property(e => e.AgeDistribution39).HasColumnName("agedistribution39");

                entity.Property(e => e.AgeDistribution59).HasColumnName("agedistribution59");

                entity.Property(e => e.AgeDistribution79).HasColumnName("agedistribution79");

                entity.Property(e => e.AgeDistributionx).HasColumnName("agedistributionx");

                entity.Property(e => e.AgeMinisters).HasColumnName("ageministers");

                entity.Property(e => e.AgeParliament).HasColumnName("ageparliament");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryAges)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countryages_CountryId_fkey");
            });

            modelBuilder.Entity<CountryDemographics>(entity =>
            {
                entity.ToTable("countrydemographics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ImmigrantPercentage).HasColumnName("immigrantpercentage");

                entity.Property(e => e.ImmigrantPopulation).HasColumnName("immigrantpopulation");

                entity.Property(e => e.Population).HasColumnName("population");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryDemographics)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countrydemographics_CountryId_fkey");
            });

            modelBuilder.Entity<CountryDisability>(entity =>
            {
                entity.ToTable("countrydisabilities");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.DisabilityDiscriminationLaw).HasColumnName("disabilitydiscriminationlaw");

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.HealthFundingGDP).HasColumnName("healthfundinggdp");

                entity.Property(e => e.HealthType).HasColumnName("healthtype");

                entity.Property(e => e.Overweight).HasColumnName("overweight");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryDisabilities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countrydisabilities_CountryId_fkey");
            });

            modelBuilder.Entity<CountryEconomicEquality>(entity =>
            {
                entity.ToTable("countryeconomicequalities");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.EqualityIndex).HasColumnName("equalityindex");

                entity.Property(e => e.Poor).HasColumnName("poor");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryEconomicEqualities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countryeconomicequalities_CountryId_fkey");
            });

            modelBuilder.Entity<CountryEconomicPower>(entity =>
            {
                entity.ToTable("countryeconomicpowers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.GDP).HasColumnName("gdp");

                entity.Property(e => e.GDPPerCapita).HasColumnName("gdppercapita");

                entity.Property(e => e.GDPWorld).HasColumnName("gdpworld");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryEconomicPowers)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countryeconomicpowers_CountryId_fkey");
            });

            modelBuilder.Entity<CountryEducation>(entity =>
            {
                entity.ToTable("countryeducation");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActualEducation).HasColumnName("actualeducation");

                entity.Property(e => e.BachelorFemale).HasColumnName("bachelorfemale");

                entity.Property(e => e.BachelorMale).HasColumnName("bachelormale");

                entity.Property(e => e.BachelorMaleFemale).HasColumnName("bachelormalefemale");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.DoctoralFemale).HasColumnName("doctoralfemale");

                entity.Property(e => e.DoctoralMale).HasColumnName("doctoralmale");

                entity.Property(e => e.DoctoralMaleFemale).HasColumnName("doctoralmalefemale");

                entity.Property(e => e.EducationPublicFundFund).HasColumnName("educationpublicfundfund");

                entity.Property(e => e.EducationPublicFundingGDP).HasColumnName("educationpublicfundinggdp");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ElementaryFemale).HasColumnName("elementaryfemale");

                entity.Property(e => e.ElementaryMale).HasColumnName("elementarymale");

                entity.Property(e => e.ElementaryMaleFemale).HasColumnName("elementarymalefemale");

                entity.Property(e => e.ExpectedEducation).HasColumnName("expectededucation");

                entity.Property(e => e.FemaleLiteracy).HasColumnName("femaleliteracy");

                entity.Property(e => e.HighSchoolFemale).HasColumnName("highschoolfemale");

                entity.Property(e => e.HighSchoolMale).HasColumnName("highschoolmale");

                entity.Property(e => e.HighSchoolMaleFemale).HasColumnName("highschoolmalefemale");

                entity.Property(e => e.MaleLiteracy).HasColumnName("maleliteracy");

                entity.Property(e => e.MasterFemale).HasColumnName("masterfemale");

                entity.Property(e => e.MasterMale).HasColumnName("mastermale");

                entity.Property(e => e.MasterMaleFemale).HasColumnName("mastermalefemale");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryEducations)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countryeducation_CountryId_fkey");
            });

            modelBuilder.Entity<CountryGender>(entity =>
            {
                entity.ToTable("countrygenders");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.EducatedFemaleUnemploy).HasColumnName("educatedfemaleunemploy");

                entity.Property(e => e.EducatedMaleUnemploy).HasColumnName("educatedmaleunemploy");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FemaleMinisterShare).HasColumnName("femaleministershare");

                entity.Property(e => e.FemaleParliamentShare).HasColumnName("femaleparliamentshare");

                entity.Property(e => e.FemalePopulationpercetage).HasColumnName("femalepopulationpercetage");

                entity.Property(e => e.FemalePromotionPolicy).HasColumnName("femalepromotionpolicy");

                entity.Property(e => e.FemaleSuicide).HasColumnName("femalesuicide");

                entity.Property(e => e.FemaleWorkforce).HasColumnName("femaleworkforce");

                entity.Property(e => e.FemaleWorkforcePercentage).HasColumnName("femaleworkforcepercentage");

                entity.Property(e => e.FemaleWorkforcePopulationPercentage).HasColumnName("femaleworkforcepopulationpercentage");

                entity.Property(e => e.FirmsFemaleManager).HasColumnName("firmsfemalemanager");

                entity.Property(e => e.FirmsFemaleOwnership).HasColumnName("firmsfemaleownership");

                entity.Property(e => e.GenderEducationcationGap).HasColumnName("gendereducationcationgap");

                entity.Property(e => e.GenderHealthGap).HasColumnName("genderhealthgap");

                entity.Property(e => e.GenderPoliticalGap).HasColumnName("genderpoliticalgap");

                entity.Property(e => e.GenderWorkGap).HasColumnName("genderworkgap");

                entity.Property(e => e.IncomeGap).HasColumnName("incomegap");

                entity.Property(e => e.LifeExpectancyFemale).HasColumnName("lifeexpectancyfemale");

                entity.Property(e => e.LifeExpectancyMale).HasColumnName("lifeexpectancymale");

                entity.Property(e => e.MalePopulationPercentage).HasColumnName("malepopulationpercentage");

                entity.Property(e => e.MaleSuicide).HasColumnName("malesuicide");

                entity.Property(e => e.Maternity).HasColumnName("maternity");

                entity.Property(e => e.Paternity).HasColumnName("paternity");

                entity.Property(e => e.WomenEducation).HasColumnName("womeneducation");

                entity.Property(e => e.WomenViolence).HasColumnName("womenviolence");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryGenders)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countrygenders_CountryId_fkey");
            });

            modelBuilder.Entity<CountryIndustry>(entity =>
            {
                entity.ToTable("countryindustries");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.AveragePay).HasColumnName("averagepay");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.EducationSpend).HasColumnName("education_spend");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Employees).HasColumnName("employees");

                entity.Property(e => e.FlexibleHours).HasColumnName("flexiblehours");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.Harassment).HasColumnName("harassment");

                var industryConverter = new ValueConverter<Common.Enums.Industry?, int?>(
                    v => v != null ? (int)v : null,
                    v => v != null ? (Common.Enums.Industry)v : null
                    );

                entity.Property(e => e.Industry)
                    .HasColumnName("industryid")
                    .HasConversion(industryConverter);

                entity.Property(e => e.InjuriesFatal).HasColumnName("injuriesfatal");

                entity.Property(e => e.InjuriesNonFatal).HasColumnName("injuriesnonfatal");

                entity.Property(e => e.LBGT).HasColumnName("lbgt");

                entity.Property(e => e.Maternity).HasColumnName("maternity");

                entity.Property(e => e.Paternity).HasColumnName("paternity");

                entity.Property(e => e.Race).HasColumnName("race");

                entity.Property(e => e.Retention).HasColumnName("retention");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryIndustries)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("countryindustries_CountryId_fkey");
            });

            modelBuilder.Entity<CountryInfrastructure>(entity =>
            {
                entity.ToTable("countryinfrastructures");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CellularSubscriptions).HasColumnName("cellularsubscriptions");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.InternetUse).HasColumnName("internetuse");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryInfrastructures)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countryinfrastructures_CountryId_fkey");
            });

            modelBuilder.Entity<CountryLaborAndSocialProtection>(entity =>
            {
                entity.ToTable("countrylaborandsocialprotection");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FemaleManagementPercentage).HasColumnName("femalemanagementpercentage");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryLaborAndSocialProtections)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countrylaborandsocialprotection_CountryId_fkey");
            });

            modelBuilder.Entity<CountryLaborForce>(entity =>
            {
                entity.ToTable("countrylaborforces");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AverageIncome).HasColumnName("averageincome");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FemaleUnemployment).HasColumnName("femaleunemployment");

                entity.Property(e => e.LaborForce).HasColumnName("laborforce");

                entity.Property(e => e.LaborForcePercentage).HasColumnName("laborforcepercentage");

                entity.Property(e => e.MaleUnemployment).HasColumnName("maleunemployment");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryLaborForces)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countrylaborforces_CountryId_fkey");
            });

            modelBuilder.Entity<CountryPolitical>(entity =>
            {
                entity.ToTable("countrypolitical");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CorruptionIndex).HasColumnName("corruptionindex");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.DemocracyIndex).HasColumnName("democracyindex");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FreeSpeechIndex).HasColumnName("freespeechindex");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryPoliticals)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countrypolitical_CountryId_fkey");
            });

            modelBuilder.Entity<CountryPrivateSectorAndTrade>(entity =>
            {
                entity.ToTable("countryprivatesectorsandtrades");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CostOfBusiness).HasColumnName("costofbusiness");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.EaseOfBusiness).HasColumnName("easeofbusiness");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FirmsBribery).HasColumnName("firmsbribery");

                entity.Property(e => e.FirmsTraining).HasColumnName("firmstraining");

                entity.Property(e => e.StartupBusiness).HasColumnName("startupbusiness");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryPrivateSectorsAndTrades)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countryprivatesectorsandtrades_CountryId_fkey");
            });

            modelBuilder.Entity<CountryPublicSector>(entity =>
            {
                entity.ToTable("countrypublicsectors");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.HumanCapital).HasColumnName("humancapital");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryPublicSectors)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countrypublicsectors_CountryId_fkey");
            });

            modelBuilder.Entity<CountryRace>(entity =>
            {
                entity.ToTable("countryraces");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Arab).HasColumnName("arab");

                entity.Property(e => e.Asian).HasColumnName("asian");

                entity.Property(e => e.Black).HasColumnName("black");

                entity.Property(e => e.Caucasian).HasColumnName("caucasian");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.CountryRaceHarassment).HasColumnName("countryraceharassment");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Hispanic).HasColumnName("hispanic");

                entity.Property(e => e.Indegineous).HasColumnName("indegineous");

                entity.Property(e => e.RaceDiscriminationLaw).HasColumnName("racediscriminationlaw");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryRaces)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countryraces_CountryId_fkey");
            });

            modelBuilder.Entity<CountryReligion>(entity =>
            {
                entity.ToTable("countryreligions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Buddishm).HasColumnName("buddishm");

                entity.Property(e => e.Christian).HasColumnName("christian");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Hindu).HasColumnName("hindu");

                entity.Property(e => e.Judaism).HasColumnName("judaism");

                entity.Property(e => e.Muslim).HasColumnName("muslim");

                entity.Property(e => e.Other).HasColumnName("other");

                entity.Property(e => e.ReligionFreedom).HasColumnName("religionfreedom");

                entity.Property(e => e.StateReligion).HasColumnName("statereligion");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryReligions)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countryreligions_CountryId_fkey");
            });

            modelBuilder.Entity<CountrySexuality>(entity =>
            {
                entity.ToTable("countrysexualities");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ConversionTherapy).HasColumnName("conversiontherapy");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.HomosexualPopulation).HasColumnName("homosexualpopulation");

                entity.Property(e => e.LGBTAdoption).HasColumnName("lgbtadoption");

                entity.Property(e => e.LGBTAntiLaws).HasColumnName("lgbtantilaws");

                entity.Property(e => e.LGBTDeathSentences).HasColumnName("lgbtdeathsentences");

                entity.Property(e => e.LGBTDiscriminationLaw).HasColumnName("lgbtdiscriminationlaw");

                entity.Property(e => e.LGBTMarketing).HasColumnName("lgbtmarketing");

                entity.Property(e => e.LGBTMarriage).HasColumnName("lgbtmarriage");

                entity.Property(e => e.LGBTMurders).HasColumnName("lgbtmurders");

                entity.Property(e => e.LGBTTolerance).HasColumnName("lgbttolerance");

                entity.Property(e => e.TransgenderRights).HasColumnName("transgenderrights");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountrySexualities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countrysexualities_CountryId_fkey");
            });

            modelBuilder.Entity<CountryUrbanization>(entity =>
            {
                entity.ToTable("countryurbanization");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LiveCities).HasColumnName("livecities");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryUrbanizations)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countryurbanization_CountryId_fkey");
            });

            modelBuilder.Entity<CountryUtility>(entity =>
            {
                entity.ToTable("countryutilities");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccessToDrinkingWater).HasColumnName("accesstodrinkingwater");

                entity.Property(e => e.AccessToElectricity).HasColumnName("accesstoelectricity");

                entity.Property(e => e.AccessToHandWashing).HasColumnName("accesstohandwashing");

                entity.Property(e => e.AccessToSanitation).HasColumnName("accesstosanitation");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.SlumsPopulation).HasColumnName("slumspopulation");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryUtilities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countryutilities_CountryId_fkey");
            });

            modelBuilder.Entity<Entities.EducationLevel>(entity =>
            {
                entity.ToTable("educationlevels");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Entities.EducationSubject>(entity =>
            {
                entity.ToTable("educationsubjects");

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
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("people");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.EducationInstitute)
                    .HasMaxLength(100)
                    .HasColumnName("educationinstitute")
                    .HasDefaultValueSql("NULL::character varying");


                var levelConverter = new ValueConverter<Common.Enums.EducationLevel?, int?>(
                    v => v != null ? (int)v : null,
                    v => v != null ? (Common.Enums.EducationLevel)v : null
                    );

                entity.Property(e => e.HighEducation)
                    .HasColumnName("higheducation")
                    .HasConversion(levelConverter);


                var subjectConverter = new ValueConverter<Common.Enums.EducationSubject?, int?>(
                    v => v != null ? (int)v : null,
                    v => v != null ? (Common.Enums.EducationSubject)v : null
                    );

                entity.Property(e => e.EducationSubject)
                    .HasColumnName("educationsubject")
                    .HasConversion(subjectConverter);

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                var genderConverter = new ValueConverter<Common.Enums.Gender?, int?>(
                    v => v != null ? (int)v : null,
                    v => v != null ? (Common.Enums.Gender)v : null
                    );

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasConversion(genderConverter);

                entity.Property(e => e.Kids).HasColumnName("kids");

                entity.Property(e => e.Married).HasColumnName("married");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(135)
                    .HasColumnName("name");

                var raceConverter = new ValueConverter<Common.Enums.Race?, int?>(
                    v => v != null ? (int)v : null,
                    v => v != null ? (Common.Enums.Race)v : null
                    );

                entity.Property(e => e.Race)
                    .HasColumnName("race")
                    .HasConversion(raceConverter);

                var religionConverter = new ValueConverter<Common.Enums.Religion?, int?>(
                    v => v != null ? (int)v : null,
                    v => v != null ? (Common.Enums.Religion)v : null
                    );

                entity.Property(e => e.Religion)
                    .HasColumnName("religion")
                    .HasConversion(religionConverter);

                entity.Property(e => e.RandomName)
                    .IsRequired()
                    .HasMaxLength(135)
                    .HasColumnName("randomname");

                entity.Property(e => e.Sexuality)
                    .HasMaxLength(45)
                    .HasColumnName("sexuality")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.Urban).HasColumnName("urban");

                entity.Property(e => e.VisibleDisability)
                    .HasMaxLength(45)
                    .HasColumnName("visibledisability")
                    .HasDefaultValueSql("NULL::character varying");

            });

            modelBuilder.Entity<PersonNationality>(entity =>
            {
                entity.ToTable("personnationalities");

                entity.Property(e => e.PersonNationalityId).HasColumnName("id");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.PersonId).HasColumnName("personid");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.PersonNationalities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("personnationalities_CountryId_fkey");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonNationalities)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("personnationalities_personid_fkey");
            });

            modelBuilder.Entity<Entities.Race>(entity =>
            {
                entity.ToTable("races");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Entities.Religion>(entity =>
            {
                entity.ToTable("religions");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BaseSalary).HasColumnName("basesalary");

                entity.Property(e => e.CompanyId).HasColumnName("companyid");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnName("effectivefrom")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.JobTenure).HasColumnName("jobtenure");

                entity.Property(e => e.OtherIncentives).HasColumnName("otherincentives");

                entity.Property(e => e.PersonId).HasColumnName("personid");

                var roleConverter = new ValueConverter<Common.Enums.RoleType?, int?>(
                    v => v != null ? (int)v : null,
                    v => v != null ? (Common.Enums.RoleType)v : null
                    );

                entity.Property(e => e.RoleType)
                    .HasColumnName("roletype")
                    .HasConversion(roleConverter);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("title");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("roles_CompanyId_fkey");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("roles_personid_fkey");
            });

            modelBuilder.Entity<PropertyMetadata>(entity =>
            {
                entity.ToTable("propertymetadata");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AllowsNull).IsRequired()
                    .HasColumnName("allowsnull")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.IsEditable)
                    .HasColumnName("iseditable")
                    .HasDefaultValueSql("true");

                var fieldTypeConverter = new ValueConverter<Common.Enums.FieldType, int>(
                    v => (int)v,
                    v => (Common.Enums.FieldType)v
                    );

                entity.Property(e => e.FieldType)
                    .HasColumnName("fieldtype")
                    .HasConversion(fieldTypeConverter);

                entity.Property(e => e.EntityName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("EntityName");

                entity.Property(e => e.PropertyName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("propertyname");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("Description")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.DropDownDictionary)
                    .HasMaxLength(30)
                    .HasColumnName("DropDownDictionary")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.FrontendName)
                    .HasMaxLength(200)
                    .HasColumnName("frontendname")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.RangeHigh)
                    .HasMaxLength(15)
                    .HasColumnName("name")
                    .HasDefaultValueSql("NULL::character varying"); ;

                entity.Property(e => e.RangeLow)
                    .HasMaxLength(15)
                    .HasColumnName("name")
                    .HasDefaultValueSql("NULL::character varying"); ;

                entity.Property(e => e.ChildrenEntityName)
                    .HasMaxLength(30)
                    .HasColumnName("childrenentityname")
                    .HasDefaultValueSql("NULL::character varying");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
