using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleOLX.Models;

namespace SimpleOLX
{
    public class SimpleOLXDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        protected readonly IConfiguration _configuration;

        public DbSet<Advert> Adverts { get; set; }
        //public DbSet<RefreshToken> RefreshTokens { get; set; }

        public SimpleOLXDbContext(IConfiguration configuration, DbContextOptions<SimpleOLXDbContext> options) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entityBuilder =>
            {
                entityBuilder.Property(userEntity => userEntity.FirstName).IsRequired(true);
                entityBuilder.Property(userEntity => userEntity.LastName).IsRequired(true);
                entityBuilder.Property(userEntity => userEntity.CreationDate).IsRequired(true);
            });

            modelBuilder.Entity<Advert>(entityBuilder =>
            {
                entityBuilder.HasKey(advertEntity => advertEntity.Id);
                entityBuilder.Property(advertEntity => advertEntity.Title).IsRequired(true);
                entityBuilder.Property(advertEntity => advertEntity.Description).IsRequired(true);
                entityBuilder.Property(advertEntity => advertEntity.Mail).IsRequired(true);
                entityBuilder.Property(advertEntity => advertEntity.PhoneNumber).IsRequired(true);
                entityBuilder.Property(advertEntity => advertEntity.Category).IsRequired(true);
                entityBuilder.Property(advertEntity => advertEntity.Price).IsRequired(true);
                entityBuilder.Property(advertEntity => advertEntity.Image).IsRequired(true);
                entityBuilder.Property(advertEntity => advertEntity.LocalizationLatitude).IsRequired(true);
                entityBuilder.Property(advertEntity => advertEntity.LocalizationLongitude).IsRequired(true);
                entityBuilder.Property(advertEntity => advertEntity.CreationDate).IsRequired(true);
                entityBuilder.Property(advertEntity => advertEntity.UserOwnerId).IsRequired(true);

                entityBuilder.HasOne(advertEntity => advertEntity.UserOwner)
                             .WithMany(userEntity => userEntity.AdvertsOwned)
                             .HasForeignKey(advertEntity => advertEntity.UserOwnerId);
            });

            //modelBuilder.Entity<RefreshToken>(entityBuilder =>
            //{
            //    entityBuilder.HasKey(refreshTokenEntity => refreshTokenEntity.Id);
            //    entityBuilder.Property(refreshTokenEntity => refreshTokenEntity.Value).IsRequired(true);
            //    entityBuilder.Property(refreshTokenEntity => refreshTokenEntity.ExpiryDate).IsRequired(true);
            //    entityBuilder.Property(refreshTokenEntity => refreshTokenEntity.CreationDate).IsRequired(true);
            //    entityBuilder.Property(refreshTokenEntity => refreshTokenEntity.RevocationDate).IsRequired(false);
            //    entityBuilder.Property(refreshTokenEntity => refreshTokenEntity.RevocationDate).IsRequired(true);
            //    entityBuilder.Property(refreshTokenEntity => refreshTokenEntity.CreatedByIp).IsRequired(true);
            //    entityBuilder.Property(refreshTokenEntity => refreshTokenEntity.RevokedByIp).IsRequired(true);
            //    entityBuilder.Property(refreshTokenEntity => refreshTokenEntity.ReplacedByToken).IsRequired(true);
            //    entityBuilder.Property(refreshTokenEntity => refreshTokenEntity.ReasonRevoked).IsRequired(true);
            //    entityBuilder.Property(refreshTokenEntity => refreshTokenEntity.UserId).IsRequired(true);

            //    entityBuilder.HasOne(refreshTokenEntity => refreshTokenEntity.User)
            //                 .WithMany(userEntity => userEntity.RefreshTokens)
            //                 .HasForeignKey(refreshTokenEntity => refreshTokenEntity.UserId);
            //});
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<Enum>().HaveConversion<string>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite(_configuration.GetConnectionString("SimpleOLXDatabase"));
        }
    }
}
