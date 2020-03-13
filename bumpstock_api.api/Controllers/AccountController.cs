using bumpstock_api.api.Authentication;
using bumpstock_api.entity.Entity.Public;
using bumpstock_api.service.Service.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace bumpstock_api.api.Controllers
{
    [Route("v1/account")]
    public class AccountController : Controller
    {
        private readonly IPublicService _userService;

        public AccountController(IPublicService userService)
        {
            _userService = userService;
        }

        [HttpPost("addcontact")]
        [AllowAnonymous]
        public async Task<IActionResult> AddContact([FromBody]Contact contact)
        {
            if (contact == null)
                return BadRequest(new[] { new { property = "contact", message = "SyntaxError: JSON.parse: Your json is wrong" } });

            if (contact.Valid)
                contact = await _userService.AddContact(contact);

            if (contact.Invalid)
                return BadRequest(contact.Notifications.ToArray());

            return Ok(contact);
        }

        [HttpPost("activatecontact")]
        [AllowAnonymous]
        public async Task<IActionResult> ActivateContact([FromBody]ActivateContact activateContact)
        {
            if (activateContact == null)
                return BadRequest(new[] { new { property = "activatecontact", message = "SyntaxError: JSON.parse: Your json is wrong" } });

            if (activateContact.Invalid)
                return BadRequest(activateContact.Notifications.ToArray());

            activateContact = await _userService.ActivateContact(activateContact);

            if (activateContact == null)
                return BadRequest(new[] { new { property = "activatecontact", message = "Invalid code for activation" } });

            return Ok(new { activateContact.hash });
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody]JToken collection)
        {
            if (collection == null || collection["hash"] == null || string.IsNullOrEmpty(collection["hash"].ToString()))
                return BadRequest(new[] { new { property = "hash", message = "SyntaxError: JSON.parse: Your json is wrong" } });

            var hash = collection["hash"].ToString();
            var person = await _userService.Signin(hash);

            if (person == null)
                return Forbid();

            var token = TokenService.GenerateToken(person);

            return Ok(new { person.hash, token });
        }

        [HttpPost("token")]
        [AllowAnonymous]
        public IActionResult Token()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var dictionary = new Dictionary<string, string>();

            foreach (var claim in identity.Claims)
            {
                dictionary.Add(claim.Type, claim.Value);
            }

            return Ok(dictionary);
        }

        [HttpGet("test")]
        [AllowAnonymous]
        public IActionResult Test()
        {
            return Ok(new { System.DateTime.Now });
        }
    }
}
