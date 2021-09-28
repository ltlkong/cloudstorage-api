using ltl_cloudstorage.Dtos;
using ltl_cloudstorage.Models;
using Microsoft.EntityFrameworkCore;
using ltl_cloudstorage.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ltl_cloudstorage.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : BaseController
    {
        private readonly UserService _userService;
        public ProfileController(CSDbContext context, UserService userService) : base(context)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Get user and information.
            User user = GetCurrentUser();
            Profile profile = await _context.Profiles.FindAsync(user.Id);

            return Ok(new { user, profile});
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserDto userDto)
        {
						Profile profile = await _context.Profiles.FindAsync(GetCurrentUser().Id);

            try
            {

								bool haveProfile = profile != null;

								if(haveProfile)
									return BadRequest(new {msg = "User already have profile"});

                await _userService.CreateInfoAsync(GetCurrentUser().Id);

								profile = await _context.Profiles.FindAsync(GetCurrentUser().Id);
								User user = await _context.Users.FindAsync(GetCurrentUser().Id);
								user.Avatar = userDto.Avatar;
								user.DisplayName = userDto.DisplayName;
								profile.Introduction = userDto.Introduction;

								_context.Entry(user).State = EntityState.Modified;
								_context.Entry(profile).State = EntityState.Modified;
								await _context.SaveChangesAsync();

            }
            catch
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Create),profile);       
        }
        [HttpPut]
        public async Task<IActionResult> Update(PropValueDto propValue)
        {       
            try
            {
                bool isUpdated = await _userService.UpdateInfoAsync(GetCurrentUser(), propValue.Prop, propValue.Value);

                if (!isUpdated)
                    return BadRequest();
            }
            catch
            {
                return BadRequest();
            }    

            return Ok(new { msg = "Updated" });
        }
    }
}
