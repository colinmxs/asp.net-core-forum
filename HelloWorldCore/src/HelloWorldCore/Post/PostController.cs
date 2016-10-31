using HelloWorldCore.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HelloWorldCore.Post
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PostController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var posts = _db.UrlPosts.ToList();
            return View(posts);
        }
    }
}