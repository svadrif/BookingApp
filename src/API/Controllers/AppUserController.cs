using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private static List<AppUser> users = new List<AppUser>
        {

            new AppUser
            {
                TelegramId = 23572216,
                FirstName =  "Tony",
                LastName = "Stark",
                UserName =  "@johndoe",
                isDeleted = false,
            },
            new AppUser
            {

                TelegramId = 25572216,
                FirstName =  "Jimm",
                LastName = "Carry",
                UserName =  "@sigmamale",
                isDeleted = false,
            }
        };

        [HttpGet]
        public async Task<ActionResult<List<AppUser>>> Get()
        {
            return Ok(users);
        }
        [HttpPost]
        public async Task<ActionResult<List<AppUser>>> Post()
        {
            AppUser user = new AppUser
            {

                TelegramId = 177013,
                FirstName = "Asuka",
                LastName = "Langley",
                UserName = "@002",
                isDeleted = false,
            };
            users.Add(user);
            return Ok(users);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<AppUser>> Put(AppUser request,int id)
        {
            var user = users.Find(h => h.TelegramId == id);
            if (user == null || user.isDeleted == true)
                return BadRequest($"User with id is not found");
            user.TelegramId = request.TelegramId;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UserName = request.UserName;
            return Ok(users);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = users.Find(h => h.TelegramId == id);
            if (user == null || user.isDeleted == true)
                return BadRequest($"User with {id} was not found");
            user.isDeleted = true;
            return Ok($"The user with {id} was deleted!");
        }

    }
}
