using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicClub.v3.DbCore.Services;

namespace MusicClub.v3.DbCore
{
    public class MusicClubDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly TenantService _tenantService;

        public MusicClubDbContext(DbContextOptions<MusicClubDbContext> options, TenantService tenantService) : base(options)
        {
            _tenantService = tenantService;

            _tenantService.OnTenantChanged += HandleOnTenantChanged;
        }

        private void HandleOnTenantChanged(object? _, int id)
        {
            CurrentTenantId = id;

            _tenantService.OnTenantChanged -= HandleOnTenantChanged;
        }

        public int CurrentTenantId { get; set; }

        public DbSet<Act> Acts => Set<Act>();

        public DbSet<Artist> Artists => Set<Artist>();

        public DbSet<Band> Bands => Set<Band>();

        public DbSet<Bandname> Bandnames => Set<Bandname>();

        public DbSet<Description> Descriptions => Set<Description>();

        public DbSet<DescriptionTranslation> DescriptionTranslations => Set<DescriptionTranslation>();

        public DbSet<GoogleCalendar> GoogleCalendars => Set<GoogleCalendar>();

        public DbSet<GoogleEvent> GoogleEvents => Set<GoogleEvent>();

        public DbSet<Function> Functions => Set<Function>();

        public DbSet<Image> Images => Set<Image>();

        public DbSet<Job> Jobs => Set<Job>();

        public DbSet<Language> Languages => Set<Language>();

        public DbSet<Lineup> Lineups => Set<Lineup>();

        public DbSet<Person> People => Set<Person>();

        public DbSet<Performance> Performances => Set<Performance>();

        public DbSet<Service> Services => Set<Service>();

        public DbSet<Tenancy> Tenancies => Set<Tenancy>();

        public DbSet<Tenant> Tenants => Set<Tenant>();

        public DbSet<Worker> Workers => Set<Worker>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Act>().HasQueryFilter(e => e.TenantId == CurrentTenantId);
            builder.Entity<Artist>().HasQueryFilter(e => e.TenantId == CurrentTenantId);
            builder.Entity<Band>().HasQueryFilter(e => e.TenantId == CurrentTenantId);
            builder.Entity<Bandname>().HasQueryFilter(e => e.TenantId == CurrentTenantId);
            builder.Entity<Description>().HasQueryFilter(e => e.TenantId == CurrentTenantId);
            builder.Entity<Function>().HasQueryFilter(e => e.TenantId == CurrentTenantId);
            builder.Entity<DescriptionTranslation>().HasQueryFilter(e => e.TenantId == CurrentTenantId);
            builder.Entity<GoogleCalendar>().HasQueryFilter(e => e.TenantId == CurrentTenantId);
            builder.Entity<GoogleEvent>().HasQueryFilter(e => e.TenantId == CurrentTenantId);
            builder.Entity<Image>().HasQueryFilter(e => e.TenantId == CurrentTenantId);
            builder.Entity<Job>().HasQueryFilter(e => e.TenantId == CurrentTenantId);
            builder.Entity<Lineup>().HasQueryFilter(e => e.TenantId == CurrentTenantId);
            builder.Entity<Performance>().HasQueryFilter(e => e.TenantId == CurrentTenantId);
            builder.Entity<Service>().HasQueryFilter(e => e.TenantId == CurrentTenantId);
            builder.Entity<Worker>().HasQueryFilter(e => e.TenantId == CurrentTenantId);

            builder.Entity<Act>()
                .HasOne(a => a.Description)
                .WithMany(i => i.Acts)
                .HasForeignKey(a => a.DescriptionId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.Entity<Act>()
                .HasOne(a => a.Image)
                .WithMany(i => i.Acts)
                .HasForeignKey(a => a.ImageId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.Entity<Act>()
                .HasOne(a => a.Lineup)
                .WithMany(l => l.Acts)
                .HasForeignKey(a => a.LineupId)
                .IsRequired(true);

            builder.Entity<ApplicationUser>()
                .HasOne(a => a.Person)
                .WithMany(p => p.ApplicationUsers)
                .HasForeignKey(a => a.PersonId)
                .IsRequired(true);

            builder.Entity<Artist>()
                .HasOne(a => a.Image)
                .WithMany(i => i.Artists)
                .HasForeignKey(a => a.ImageId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.Entity<Artist>()
                .HasOne(a => a.Person)
                .WithMany(p => p.Artists)
                .HasForeignKey(a => a.PersonId)
                .IsRequired(true);

            builder.Entity<Bandname>()
                 .HasOne(b => b.Band)
                 .WithMany(b => b.Bandnames)
                 .HasForeignKey(b => b.BandId)
                 .IsRequired(true);

            builder.Entity<DescriptionTranslation>()
                .HasOne(d => d.Description)
                .WithMany(d => d.DescriptionTranslations)
                .HasForeignKey(d => d.DescriptionId)
                .IsRequired(true);

            builder.Entity<DescriptionTranslation>()
                .HasOne(d => d.Language)
                .WithMany(l => l.DescriptionTranslations)
                .HasForeignKey(d => d.LanguageId)
                .IsRequired(true);

            builder.Entity<GoogleEvent>()
                .HasOne(g => g.Act)
                .WithOne(a => a.GoogleEvent)
                .HasForeignKey<Act>(a => a.GoogleEventId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.Entity<GoogleEvent>()
                .HasOne(g => g.GoogleCalendar)
                .WithMany(g => g.GoogleEvents)
                .HasForeignKey(g => g.GoogleCalendarId)
                .IsRequired(true);

            builder.Entity<Job>()
                .HasOne(j => j.Worker)
                .WithMany(w => w.Jobs)
                .HasForeignKey(j => j.WorkerId)
                .IsRequired(true);

            builder.Entity<Job>()
                .HasOne(j => j.Function)
                .WithMany(f => f.Jobs)
                .HasForeignKey(j => j.FunctionId)
                .IsRequired(true);

            builder.Entity<Job>()
                .HasOne(j => j.Act)
                .WithMany(a => a.Jobs)
                .HasForeignKey(j => j.ActId)
                .IsRequired(true);

            builder.Entity<Lineup>()
                .HasOne(l => l.Image)
                .WithMany(i => i.Lineups)
                .HasForeignKey(l => l.ImageId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.Entity<Performance>()
                .HasOne(p => p.Artist)
                .WithMany(a => a.Performances)
                .HasForeignKey(p => p.ArtistId)
                .IsRequired(true);

            builder.Entity<Performance>()
                .HasOne(p => p.Bandname)
                .WithMany(b => b.Performances)
                .HasForeignKey(p => p.BandnameId)
                .IsRequired(false);

            builder.Entity<Performance>()
                .HasOne(p => p.Image)
                .WithMany(i => i.Performances)
                .HasForeignKey(p => p.ImageId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.Entity<Performance>()
                .HasOne(p => p.Act)
                .WithMany(a => a.Performances)
                .HasForeignKey(p => p.ActId)
                .IsRequired(true);

            builder.Entity<Person>()
               .HasOne(p => p.Image)
               .WithMany(i => i.People)
               .HasForeignKey(p => p.ImageId)
               .OnDelete(DeleteBehavior.SetNull)
               .IsRequired(false);

            builder.Entity<Service>()
                 .HasOne(s => s.Worker)
                 .WithMany(w => w.Services)
                 .HasForeignKey(s => s.WorkerId)
                 .IsRequired(true);

            builder.Entity<Service>()
                .HasOne(s => s.Function)
                .WithMany(f => f.Services)
                .HasForeignKey(s => s.FunctionId)
                .IsRequired(true);

            builder.Entity<Service>()
                .HasOne(s => s.Lineup)
                .WithMany(l => l.Services)
                .HasForeignKey(s => s.LineupId)
                .IsRequired(true);

            builder.Entity<Tenancy>()
                .HasOne(t => t.Tenant)
                .WithMany(t => t.Tenancies)
                .HasForeignKey(t => t.TenantId)
                .IsRequired(true);

            builder.Entity<Tenancy>()
                .HasOne(t => t.ApplicationUser)
                .WithMany(a => a.Tenancies)
                .HasForeignKey(t => t.ApplicationUserId)
                .IsRequired(true);

            builder.Entity<Tenant>()
                .HasIndex(t => t.Name)
                .IsUnique();

            builder.Entity<Tenant>()
                .HasMany(t => t.Acts)
                .WithOne(a => a.Tenant)
                .HasForeignKey(a => a.TenantId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.Entity<Tenant>()
                .HasMany(t => t.Artists)
                .WithOne(a => a.Tenant)
                .HasForeignKey(a => a.TenantId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.Entity<Tenant>()
                .HasMany(t => t.Bands)
                .WithOne(a => a.Tenant)
                .HasForeignKey(a => a.TenantId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.Entity<Tenant>()
                .HasMany(t => t.Bandnames)
                .WithOne(a => a.Tenant)
                .HasForeignKey(a => a.TenantId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Tenant>()
                .HasMany(t => t.Descriptions)
                .WithOne(a => a.Tenant)
                .HasForeignKey(a => a.TenantId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Tenant>()
                .HasMany(t => t.DescriptionTranslations)
                .WithOne(a => a.Tenant)
                .HasForeignKey(a => a.TenantId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Tenant>()
                .HasMany(t => t.Functions)
                .WithOne(a => a.Tenant)
                .HasForeignKey(a => a.TenantId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Tenant>()
                .HasMany(t => t.GoogleCalendars)
                .WithOne(a => a.Tenant)
                .HasForeignKey(a => a.TenantId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Tenant>()
                .HasMany(t => t.GoogleEvents)
                .WithOne(a => a.Tenant)
                .HasForeignKey(a => a.TenantId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Tenant>()
                .HasMany(t => t.Images)
                .WithOne(a => a.Tenant)
                .HasForeignKey(a => a.TenantId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Tenant>()
                .HasMany(t => t.Jobs)
                .WithOne(a => a.Tenant)
                .HasForeignKey(a => a.TenantId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Tenant>()
                .HasMany(t => t.Lineups)
                .WithOne(a => a.Tenant)
                .HasForeignKey(a => a.TenantId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.Entity<Tenant>()
                .HasMany(t => t.Performances)
                .WithOne(a => a.Tenant)
                .HasForeignKey(a => a.TenantId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.Entity<Tenant>()
               .HasMany(t => t.People)
               .WithOne(a => a.Tenant)
               .HasForeignKey(a => a.TenantId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Tenant>()
                .HasMany(t => t.Services)
                .WithOne(a => a.Tenant)
                .HasForeignKey(a => a.TenantId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Tenant>()
                .HasMany(t => t.Workers)
                .WithOne(a => a.Tenant)
                .HasForeignKey(a => a.TenantId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Worker>()
                .HasOne(w => w.Person)
                .WithMany(p => p.Workers)
                .HasForeignKey(w => w.PersonId)
                .IsRequired(true);

            base.OnModelCreating(builder);

        }
    }
}
