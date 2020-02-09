using ERPAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERPAPI.Data
{
    public class DataInitializer
    {
        #region variables
        private ERPContext _db;
        private RoleManager<Role> _roleMgr;
        private UserManager<User> _userMgr;
        #endregion

        public DataInitializer(UserManager<User> userMgr, RoleManager<Role> roleMgr, ERPContext db)
        {
            _userMgr = userMgr;
            _roleMgr = roleMgr;
            _db = db;
        }

        public async Task Seed()
        {
            try
            {
                // check root admin user
                var user = await _userMgr.FindByNameAsync("root");
                if (user == null)
                {
                    if (!(await _roleMgr.RoleExistsAsync("admin")))
                    {
                        var role = new Role("admin");
                        await _roleMgr.CreateAsync(role);
                        await _roleMgr.AddClaimAsync(role, new Claim("admin", "True"));
                    }

                    user = new User()
                    {
                        UserName = "root",
                        FirstName = "root",
                        LastName = "root",
                        Email = ""
                    };

                    var userResult = await _userMgr.CreateAsync(user, "123456");
                    var roleResult = await _userMgr.AddToRoleAsync(user, "admin");
                    var claimResult = await _userMgr.AddClaimAsync(user, new Claim("admin", "True"));

                    if (!userResult.Succeeded || !roleResult.Succeeded || !claimResult.Succeeded)
                    {
                        throw new InvalidOperationException("Failed to build user and roles");
                    }

                    _db.AuthClients.Add(new AuthClient()
                    {
                        Name = "ERPLite",
                        Active = true,
                        AllowedOrigin = "*",
                        ApplicationType = ApplicationType.JavaScript,
                        RefreshTokenLifeTime = 21600
                    });

                    await _db.SaveChangesAsync();
                }
            }
            catch
            {

            }
            return;
        }
    }
}
