using Microsoft.AspNetCore.Identity;
using ShoppingCartMvcUI.Constants;

namespace ShoppingCartMvcUI.Data
{
    public class DbSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider service) 
        {
            var userMgr = service.GetService<UserManager<IdentityUser>>();
            var roleMgr = service.GetService<RoleManager<IdentityRole>>();

            //adding roles to db
            await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));

            //creating admin user

            var admin = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email= "admin@gmail.com",
                EmailConfirmed=true
            };

            var userInDB = await userMgr.FindByEmailAsync(admin.Email);
            if (userInDB is null) 
            {
                await userMgr.CreateAsync(admin, "Admin@123");
                await userMgr.AddToRoleAsync(admin, Roles.Admin.ToString());
            }   
        
        }
    }
}
