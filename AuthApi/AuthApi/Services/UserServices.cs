using AuthApi.DatabaseContext;
using AuthApi.Interfaces;
using AuthApi.Models;
using AuthApi.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Services
{
    public class UserServices : IUserServices
    {
        private readonly ContextDb _context;

        public UserServices(ContextDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Registraion(CreateNewUser regUser)
        {
            var user = new User()
            {
                Email = regUser.Email,
                Password = regUser.Password,
                Name = regUser.Name,
                Description = regUser.Description,
                Role_Id = regUser.Role_id
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
            });
        }

        public async Task<IActionResult> Authorize(Auth authUser)
        {
            var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == authUser.Email && x.Password == authUser.Password);
            if( user == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(new
            {
                status = true,
                user
            });
        }

        public async Task<IActionResult> UpdateUser(UpdateUser updateUser)
        {
            var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.id_User == updateUser.id_User);
            if (user == null)
            {
                return new NotFoundResult();
            }

            user.Email = updateUser.Email;
            user.Password = updateUser.Password;
            user.Description = updateUser.Description;
            user.Name = updateUser.Name;
            user.Role_Id = updateUser.Role_id;

            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                user
            });
        }

        public async Task<IActionResult> CreateNewUser(CreateNewUser regUser)
        {
            var user = new User()
            {
                Email = regUser.Email,
                Password = regUser.Password,
                Name = regUser.Name,
                Description = regUser.Description,
                Role_Id = 2
            };
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                user
            });
        }

        public async Task<IActionResult> DeleteUser(int user_id)
        {
            var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.id_User == user_id);
            if (user == null)
            {
                return new NotFoundResult();
            }

            _context.Remove(user);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                user
            });
        }

        public async Task<IActionResult> GetAllUsers()
        {
            var user = await _context.Users.Include(x => x.Role).ToListAsync();
            return new OkObjectResult(new
            {
                status = true,
                user
            });
        }

    }
}
