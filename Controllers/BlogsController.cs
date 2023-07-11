using BloggieMvc.Models.ViewModels;
using BloggieMvc.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BloggieMvc.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostInterface blogPostInterface;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        private readonly Microsoft.AspNetCore.Identity.SignInManager<IdentityUser> signInManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager;

        public BlogsController(IBlogPostInterface blogPostInterface, IBlogPostLikeRepository blogPostLikeRepository, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.blogPostInterface = blogPostInterface;
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index(string urlHandle)
        {

            var getURLhandle = await blogPostInterface.GetByUrlHandleAsync(urlHandle);
            var blogPostViewModel = new BlogDetailsViewModel();
            var liked = false;
            




            if (getURLhandle != null)
            {
                var totalLikes = await blogPostLikeRepository.GetTotalLikes(getURLhandle.Id);

                if (signInManager.IsSignedIn(User))
                {
                    //post beğenilince hangi kullanıcı beğendi
                    var allLikesForBlog =  await blogPostLikeRepository.GetLikesForBlog(getURLhandle.Id);

                    var userId = userManager.GetUserId(User);
                    if (userId != null)
                    {
                        var likeFromUser = allLikesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = likeFromUser != null;
                    }
                }



                blogPostViewModel = new BlogDetailsViewModel
                {
                    Id = getURLhandle.Id,
                    Author = getURLhandle.Author,
                    Content = getURLhandle.Content,
                    FeaturedImageUrl = getURLhandle.FeaturedImageUrl,
                    Heading = getURLhandle.Heading,
                    PageTitle = getURLhandle.PageTitle,
                    PublishedDate = getURLhandle.PublishedDate,
                    ShortDescription = getURLhandle.ShortDescription,
                    Tags = getURLhandle.Tags,
                    UrlHandle = getURLhandle.UrlHandle,
                    Visible = getURLhandle.Visible,
                    TotalLikes = totalLikes,
                    Liked =liked


                };

            }
            return View(blogPostViewModel);
        }
    }
}
