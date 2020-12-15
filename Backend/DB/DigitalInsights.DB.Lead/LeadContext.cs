using DigitalInsights.DB.Lead.GLEIF;
using Microsoft.EntityFrameworkCore;

namespace DigitalInsights.DB.Lead
{
    public partial class LeadContext : DbContext
    {
        public LeadContext()
        {
        }

        public LeadContext(DbContextOptions<LeadContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GleifAddress> GleifAddress { get; set; }
        public virtual DbSet<GleifEntity> GleifEntity { get; set; }
        public virtual DbSet<GleifEntityName> GleifEntityName { get; set; }
        public virtual DbSet<GleifValidationAuthority> GleifValidationAuthority { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=di-dev.c19yqc3su48v.us-east-2.rds.amazonaws.com;Port=5432;Database=DI-lead;Username=postgres;Password=2wsx##edc");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GleifAddress>(entity =>
            {
                entity.ToTable("gleif_address");

                entity.HasIndex(e => e.EntityId)
                    .HasName("gleif_address_entity_id_idx");

                entity.HasIndex(e => e.Id)
                    .HasName("gleif_address_pk")
                    .IsUnique();

                entity.HasIndex(e => new { e.Type, e.EntityId })
                    .HasName("gleif_address_type_entity_id_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Additionaladdressline1)
                    .HasColumnName("additionaladdressline_1")
                    .HasMaxLength(100);

                entity.Property(e => e.Additionaladdressline2)
                    .HasColumnName("additionaladdressline_2")
                    .HasMaxLength(100);

                entity.Property(e => e.Additionaladdressline3)
                    .HasColumnName("additionaladdressline_3")
                    .HasMaxLength(100);

                entity.Property(e => e.Addressnumber)
                    .HasColumnName("addressnumber")
                    .HasMaxLength(100);

                entity.Property(e => e.Addressnumberwithinbuilding)
                    .HasColumnName("addressnumberwithinbuilding")
                    .HasMaxLength(100);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(100);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(100);

                entity.Property(e => e.EntityId).HasColumnName("entity_id");

                entity.Property(e => e.Firstaddressline)
                    .HasColumnName("firstaddressline")
                    .HasMaxLength(100);

                entity.Property(e => e.Mailrouting)
                    .HasColumnName("mailrouting")
                    .HasMaxLength(100);

                entity.Property(e => e.Postalcode)
                    .HasColumnName("postalcode")
                    .HasMaxLength(20);

                entity.Property(e => e.Region)
                    .HasColumnName("region")
                    .HasMaxLength(100);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(10);

                entity.Property(e => e.Xmllang)
                    .HasColumnName("xmllang")
                    .HasMaxLength(8);

            });

            modelBuilder.Entity<GleifEntity>(entity =>
            {
                entity.ToTable("gleif_entity");

                entity.HasIndex(e => e.Id)
                    .HasName("gleif_entity_pk")
                    .IsUnique();

                entity.HasIndex(e => e.Lei)
                    .HasName("gleif_entity_lei_idx")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasMaxLength(100);

                /*entity.Property(e => e.EntityAssociatedEntityAssociatedEntityName)
                    .HasColumnName("entity_associatedentity_associatedentityname")
                    .HasMaxLength(300);

                entity.Property(e => e.EntityAssociatedEntityAssociatedEntityNameXmlLang)
                    .HasColumnName("entity_associatedentity_associatedentityname_xmllang")
                    .HasMaxLength(8);

                entity.Property(e => e.EntityAssociatedEntityAssociatedlei)
                    .HasColumnName("entity_associatedentity_associatedlei")
                    .HasMaxLength(20);

                entity.Property(e => e.EntityAssociatedEntityType)
                    .HasColumnName("entity_associatedentity_type")
                    .HasMaxLength(50);*/

                entity.Property(e => e.EntityExpirationReason)
                    .HasColumnName("entity_expiration_reason")
                    .HasMaxLength(300);

                entity.Property(e => e.ExpirationDate)
                    .HasColumnName("expiration_date")
                    .HasMaxLength(32);

                entity.Property(e => e.InitialRegistrationDate)
                    .HasColumnName("initial_registration_date")
                    .HasMaxLength(32);

                entity.Property(e => e.LastUpdateDate)
                    .HasColumnName("last_update_date")
                    .HasMaxLength(32);

                entity.Property(e => e.LegalFormCode)
                    .HasColumnName("legal_form_code")
                    .HasMaxLength(50);

                entity.Property(e => e.LegalFormOtherLegalForm)
                    .HasColumnName("legal_form_other_legal_form")
                    .HasMaxLength(50);

                entity.Property(e => e.LegalJurisdiction)
                    .HasColumnName("legal_jurisdiction")
                    .HasMaxLength(100);

                entity.Property(e => e.Lei)
                    .HasColumnName("lei")
                    .HasMaxLength(20);

                entity.Property(e => e.NextRenewalDate)
                    .HasColumnName("next_renewal_date")
                    .HasMaxLength(32);

                /*entity.Property(e => e.OtherRegistrationAuthorityId)
                    .HasColumnName("other_registration_authority_id")
                    .HasMaxLength(50);

                entity.Property(e => e.RegistrationAuthorityEntityId)
                    .HasColumnName("registration_authority_entity_id")
                    .HasMaxLength(50);

                entity.Property(e => e.RegistrationAuthorityId)
                    .HasColumnName("registration_authority_id")
                    .HasMaxLength(50);

                entity.Property(e => e.RegistrationManagingLou)
                    .HasColumnName("registration_managing_lou")
                    .HasMaxLength(100);*/

                entity.Property(e => e.RegistrationStatus)
                    .HasColumnName("registration_status")
                    .HasMaxLength(10);

                /*entity.Property(e => e.RegistrationValidationSources)
                    .HasColumnName("registration_validation_sources")
                    .HasMaxLength(100);*/

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(10);

                entity.Property(e => e.SuccessorEntityName)
                    .HasColumnName("successor_entity_name")
                    .HasMaxLength(300);

                entity.Property(e => e.SuccessorEntityNameXmllang)
                    .HasColumnName("successor_entity_name_xmllang")
                    .HasMaxLength(8);

                entity.Property(e => e.SuccessorLei)
                    .HasColumnName("successor_lei")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<GleifEntityName>(entity =>
            {
                entity.ToTable("gleif_entity_name");

                entity.HasIndex(e => e.EntityId)
                    .HasName("gleif_entity_name_entity_id_idx");

                entity.HasIndex(e => e.Id)
                    .HasName("gleif_entity_name_pk")
                    .IsUnique();

                entity.HasIndex(e => new { e.Type, e.EntityId })
                    .HasName("gleif_entity_name_type_entity_id_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EntityId).HasColumnName("entity_id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(300);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(10);

                entity.Property(e => e.Xmllang)
                    .HasColumnName("xmllang")
                    .HasMaxLength(8);

            });

            /*modelBuilder.Entity<GleifValidationAuthority>(entity =>
            {
                entity.ToTable("gleif_validation_authority");

                entity.HasIndex(e => e.EntityId)
                    .HasName("gleif_validation_authority_entity_id_idx");

                entity.HasIndex(e => e.Id)
                    .HasName("gleif_validation_authority_pk")
                    .IsUnique();

                entity.HasIndex(e => new { e.Type, e.EntityId })
                    .HasName("gleif_validation_authority_type_entity_id_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EntityId).HasColumnName("entity_id");

                entity.Property(e => e.OtherValidationAuthorityId)
                    .HasColumnName("other_validation_authority_id")
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(10);

                entity.Property(e => e.ValidationAuthorityEntityId)
                    .HasColumnName("validation_authority_entity_id")
                    .HasMaxLength(50);

                entity.Property(e => e.ValidationAuthorityId)
                    .HasColumnName("validation_authority_id")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Entity)
                    .WithMany(p => p.GleifValidationAuthorities)
                    .HasForeignKey(d => d.EntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("gleif_validation_authority_entity_id_fkey");
            });*/

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
