using AuthApi.DatabaseContext;
using AuthApi.Interfaces;
using AuthApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly ContextDb _context;
        
        public UserController(IUserServices userServices, ContextDb context)
        {
            _userServices = userServices;
            _context = context;
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registraion([FromBody]CreateNewUser regUser)
        {
            return await _userServices.Registraion(regUser);
        }

        [HttpPost]
        [Route("Authorize")]
        public async Task<IActionResult> Authorize([FromBody] Auth auth)
        {
            return await _userServices.Authorize(auth);
        }
        [HttpPost]
        [Route("CreateNewUser")]
        public async Task<IActionResult> CreateNewUser([FromBody] CreateNewUser createNewUser)
        {
            return await _userServices.CreateNewUser(createNewUser);
        }
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUser updateUser)
        {
            return await _userServices.UpdateUser(updateUser);
        }
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return await _userServices.GetAllUsers();
        }
        [HttpDelete]
        [Route("DeleteUsers")]
        public async Task<IActionResult> DeleteUsers(int user_id)
        {
            return await _userServices.DeleteUser(user_id);
        }


    }
}
