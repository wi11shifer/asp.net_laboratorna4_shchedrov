using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace laboratorna4.Controllers
{
    [Route("Library")]
    public class LibraryController : Controller
    {
        private readonly IConfiguration _configuration;

        public LibraryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Content("Ласкаво просимо до нашої бібліотеки!");
        }

        [HttpGet("Books")]
        public IActionResult Books()
        {
            var books = _configuration.GetSection("books").Get<string[]>();

            if (books == null)
            {
                return NotFound("Книги не знайдено");
            }

            return Json(books);
        }

        [HttpGet("Profile/{id?}")]
        public IActionResult Profile(int? id)
        {
            var profiles = _configuration.GetSection("profiles").Get<Profile[]>();

            if (profiles == null)
            {
                return NotFound("Профілі не знайдено");
            }

            if (id.HasValue && id >= 0 && id < profiles.Length)
            {
                var profile = profiles[id.Value];
                return Json(profile);
            }
            else
            {
                var profile = profiles[0];
                return Json(profile);
            }
        }
    }

    public class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
