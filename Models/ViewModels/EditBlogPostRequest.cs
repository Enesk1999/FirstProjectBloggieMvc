using Microsoft.AspNetCore.Mvc.Rendering;

namespace BloggieMvc.Models.ViewModels
{
    public class EditBlogPostRequest
    {
        public EditBlogPostRequest()
        {
            SelectedTag = Array.Empty<string>();
        }
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        //Tag görünümü
        public IEnumerable<SelectListItem> Tags { get; set; }
        //Seçilen tag
        public string[] SelectedTag { get; set; }
    }
}
