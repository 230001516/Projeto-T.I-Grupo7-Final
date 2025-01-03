using Microsoft.AspNetCore.Identity;
using TI_Grupo7.Areas.Identity.Data;

namespace TI_Projeto_Grupo7.Areas.Identity.Data
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Worker.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.User.ToString()));
        }

        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var admin = new ApplicationUser
            {
                UserName = "admin@evolubank.pt",
                Email = "admin@evolubank.pt",
                EmailConfirmed = true,
                PhoneNumber = "919567812",
                PhoneNumberConfirmed = true,
                NIF = 882226649,
                Address = "45. Street, New York",
                FirstName = "Desesperado",
                Surname = "PorUm20",
                is_worker = 0
            };

            var user = await userManager.FindByEmailAsync(admin.Email);
            if (user == null) {
                await userManager.CreateAsync(admin, "$toraW3Need20!");
                await userManager.AddToRoleAsync(admin, Enums.Roles.Admin.ToString());
                await userManager.AddToRoleAsync(admin, Enums.Roles.Worker.ToString());
                await userManager.AddToRoleAsync(admin, Enums.Roles.User.ToString());
            }
        } 
    }
}

