using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BloggieMvc.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Seed Roles (User, Admin, SuperAdmin)  VAROLAN 3 KULLANICI TÜRÜNÜ EKLEDİK 
            var AdminRoleId = "f9e9e7f2-f093-4820-8832-483a5f43a56b";
            var SuperAdminRoleId = "08faf044-ae9a-4e49-b8f9-77cd80f271d1";
            var UserRoleId = "84228e46-9def-4bc4-9056-b49fa1a519bb";


            var roles = new List<IdentityRole>
            {
                //Consoledan yeni guid çıkartmak için /görünüm/diğer pencereler/c# etkileşimi
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id= AdminRoleId,
                    ConcurrencyStamp= AdminRoleId
                },
                new IdentityRole
                {
                    Name="SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id=SuperAdminRoleId,
                    ConcurrencyStamp = SuperAdminRoleId
                },
                new IdentityRole
                {
                    Name= "User",
                    NormalizedName="User",
                    Id=UserRoleId,
                    ConcurrencyStamp= UserRoleId
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);




            //Seed SuperAdminUser   (SUPER ADMİNİ EKLEDİK VE HASHLEDİK)

            var superAdminId = "a6e9779e-5d9a-41f7-a5a6-4769f27cfacd";
            var superAdminUser = new IdentityUser
            {

                Id = superAdminId,
                UserName = "superadmon@bloggoe.com",
                Email = "superadmon@bloggoe.com",
                NormalizedEmail = "superadmon@bloggoe.com",
                NormalizedUserName = "superadmon@bloggoe.com"



            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "Superadmin123@");

            builder.Entity<IdentityUser>().HasData(superAdminUser);




            //Add All roles to SuperAdminUser       (SUPER ADMİNİN İŞLEVLERİ SEEDLEDİK)

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId= AdminRoleId,
                    UserId= superAdminId

                },
                new IdentityUserRole<string>
                {
                    RoleId=SuperAdminRoleId,
                    UserId=superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId=UserRoleId,
                    UserId=superAdminId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
