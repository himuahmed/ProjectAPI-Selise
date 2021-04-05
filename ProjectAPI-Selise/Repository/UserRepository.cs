using Microsoft.AspNetCore.Identity;
using ProjectAPI_Selise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjectAPI_Selise.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<UserModel> _userManager;

        public UserRepository(UserManager<UserModel> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _userManager.Users.AnyAsync(u => u.UserName == username))
                return true;

            return false;
        }


        public async Task<bool> register(UserRegistrationModel userRegistrationModel)
        {
            var newUser = new UserModel()
            {
                UserName = userRegistrationModel.Email,
                Email = userRegistrationModel.Email,
                Name = userRegistrationModel.Name
            };

            var result = await _userManager.CreateAsync(newUser, userRegistrationModel.Password);
            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }
    }
}
