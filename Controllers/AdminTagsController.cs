using BloggieMvc.Data;
using BloggieMvc.Models.Domain;
using BloggieMvc.Models.ViewModels;
using BloggieMvc.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggieMvc.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagInterface tagInterface;

        public AdminTagsController(ITagInterface tagInter)
        {
            tagInterface = tagInter;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        
        public async Task<IActionResult> Add(AddTagRequest request)
        {
            var tag = new Tag                   //AddtagRequest/Viewmodele gelen bilgi Domain modelde eşleştiriyor
            {
                Name= request.Name,
                DisplayName=request.DisplayName,
            };
           
            await tagInterface.AddTagAsync(tag);
            return RedirectToAction("List");            //RETURN VİEW KULLANILMAZ BAŞKA BİR SAYFAYA YÖNLENDİRİLİRKEN ONUN YERİNE REDİRECTTOACTION KULLANIRIZ
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var taglist = await tagInterface.GetAllTagAsync();
            return View(taglist);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var findTag = await tagInterface.GetSingleTagAsync(id);      //burada UI kısmında edit linkine basıldığında ıd eşlemeleri yapılır

            if (findTag != null)
            {
                var editTagRequest = new EditTagRequest             //Burada domaindali ver viewmodelse atılır YANİ BİZE HAZIR BİLGİLER GELİR GETMETHOD
                {
                    Id = findTag.Id,
                    Name = findTag.Name,
                    DisplayName = findTag.DisplayName,
                };
                return View(editTagRequest);
            }
            
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var viewmodelTag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };
            var updateTag = await tagInterface.UpdateTagAsync(viewmodelTag);
            if (updateTag != null)
            {
                
            }
            else
            {

            }
            return RedirectToAction("Edit", new {id = editTagRequest.Id});
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest) 
        {
            await tagInterface.DeleteTagAsync(editTagRequest.Id);
            if(editTagRequest != null)
            {
                //doğru ise
                return RedirectToAction("List");
            }
            //hata varsa

            return RedirectToAction("Edit",new {id = editTagRequest.Id});
        }



    }
}
