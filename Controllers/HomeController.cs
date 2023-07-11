using BloggieMvc.Models;
using BloggieMvc.Models.ViewModels;
using BloggieMvc.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BloggieMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostInterface blogPostInterface;
        private readonly ITagInterface tagInterface;

        public HomeController(ILogger<HomeController> logger,IBlogPostInterface blogPostInterface, ITagInterface tagInterface)
        {
            _logger = logger;
            this.blogPostInterface = blogPostInterface;
            this.tagInterface = tagInterface;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await blogPostInterface.GetAllBlogPostsAsync();
            var tags = await tagInterface.GetAllTagAsync();

            var homeViewModel = new HomeViewModel
            {
                BlogPosts = blogs,
                Tags = tags,
            };
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}