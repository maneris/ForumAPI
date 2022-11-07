﻿using ForumAPI.Auth.Model;
using Microsoft.AspNetCore.Identity;

namespace ForumAPI.Data
{
    public class AuthDBSeeder
    {
        private readonly UserManager<ForumRestUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthDBSeeder(UserManager<ForumRestUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await AddDefaultRoles();
            await AddAdminUser();
            await AddRegisteredUser();
        }

        private async Task AddAdminUser()
        {
            var newAdminUser = new ForumRestUser()
            {
                UserName = "admin",
                Email = "admin@admin.com"
            };

            var existingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
            if (existingAdminUser == null)
            {
                var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "VerySafePassword1!");
                if (createAdminUserResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newAdminUser, ForumRoles.All);
                }
            }
        }
        private async Task AddRegisteredUser()
        {
            var newRegisteredUser = new ForumRestUser()
            {
                UserName = "JonasDon",
                Email = "JonDon@gmail.com"
            };        

            var existingRegisteredUser = await _userManager.FindByNameAsync(newRegisteredUser.UserName);
            if (existingRegisteredUser == null)
            {
                var createAdminUserResult = await _userManager.CreateAsync(newRegisteredUser, "MediocarePassword1!");
                if (createAdminUserResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newRegisteredUser, ForumRoles.RegisteredUsers);
                }
            }
        }
        private async Task AddDefaultRoles()
        {
            foreach (var role in ForumRoles.All)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);
                if (!roleExists)
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
