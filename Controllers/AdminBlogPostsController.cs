using BloggieMvc.Models.Domain;
using BloggieMvc.Models.ViewModels;
using BloggieMvc.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BloggieMvc.Controllers
{

    [Authorize(Roles ="Admin")]
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagInterface tagInterface;
        private readonly IBlogPostInterface blogPostInterface;
        public AdminBlogPostsController(ITagInterface tagInterface, IBlogPostInterface blogPostInterface)
        {
            this.tagInterface = tagInterface;
            this.blogPostInterface = blogPostInterface;

        }
        
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagInterface.GetAllTagAsync();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            var blogposts = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                PublishedDate = addBlogPostRequest.PublishedDate,
                UrlHandle = addBlogPostRequest.UrlHandle,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,

            };
            //Tags routing
            var seciliTags = new List<Tag>();
            foreach (var selectedTagId in addBlogPostRequest.SelectedTag)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);    //ıd öğreniyoruz
                var exitingTag = await tagInterface.GetSingleTagAsync(selectedTagIdAsGuid); //idsine göre çağırıyoruz
                if (exitingTag != null)
                {
                    seciliTags.Add(exitingTag);
                }
            }
            //Domaine ekleniyor
            blogposts.Tags = seciliTags;


            await blogPostInterface.AddBlogPostAsync(blogposts);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogpost = await blogPostInterface.GetAllBlogPostsAsync();
            return View(blogpost);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var getSingleBlogPost = await blogPostInterface.GetBlogPostAsync(id);
            var tags = await tagInterface.GetAllTagAsync();

            if(getSingleBlogPost != null)
            {
                var editBlogModel = new EditBlogPostRequest
                {
                    Id = getSingleBlogPost.Id,
                    Author = getSingleBlogPost.Author,
                    Content = getSingleBlogPost.Content,
                    FeaturedImageUrl = getSingleBlogPost.FeaturedImageUrl,
                    Heading = getSingleBlogPost.Heading,
                    PageTitle = getSingleBlogPost.PageTitle,
                    PublishedDate = getSingleBlogPost.PublishedDate,
                    ShortDescription = getSingleBlogPost.ShortDescription,
                    UrlHandle = getSingleBlogPost.UrlHandle,
                    Visible = getSingleBlogPost.Visible,
                    Tags = tags.Select(t => new SelectListItem { Text = t.Name, Value = t.Id.ToString() }),
                    SelectedTag = getSingleBlogPost.Tags.Select(a => a.Id.ToString()).ToArray(),
                };
                return View(editBlogModel);
            }

            
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            var blogPostDomainModel = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Author = editBlogPostRequest.Author,
                Content = editBlogPostRequest.Content,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                PublishedDate = editBlogPostRequest.PublishedDate,
                ShortDescription = editBlogPostRequest.ShortDescription,
                UrlHandle = editBlogPostRequest.UrlHandle,
                Visible= editBlogPostRequest.Visible,
                
            };

            var selectedTags = new List<Tag>();
            foreach (var selectedTag in editBlogPostRequest.SelectedTag)
            {
                if (Guid.TryParse(selectedTag, out var tag))
                {
                    var foundtag = await tagInterface.GetSingleTagAsync(tag);
                    if(foundtag != null)
                    {
                        selectedTags.Add(foundtag);
                    }
                }
            }
            blogPostDomainModel.Tags = selectedTags;

            var updateBlogPost = await blogPostInterface.UpdateBlogPostAsync(blogPostDomainModel);
            if(updateBlogPost != null)
            {
                return RedirectToAction("List");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
        {
            var deletePost = await blogPostInterface.DeleteBlogPostAsync(editBlogPostRequest.Id);
            if(deletePost != null)
            {
                //doğru çalıştı 
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new {id = editBlogPostRequest.Id});
        }
    }
}
