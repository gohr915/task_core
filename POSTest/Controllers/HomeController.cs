using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using POSTest.NewFolder;
using POSTest.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace POSTest.Controllers
{
    public class HomeController : Controller
    {
        public IRepositoryItem Itemrepo { get; }
        public HomeController(IRepositoryItem  itemrepo, IWebHostEnvironment hostEnvironment)
        {
            Itemrepo = itemrepo;
            HostEnvironment = hostEnvironment;
        }

        
        public IWebHostEnvironment HostEnvironment { get; }

        public async  Task<IActionResult> Index()
        {
            var ls = await Itemrepo.get_all_items();

            return View(ls.ToList());
        }

        public async Task<IActionResult> Delete(int id ) {

            if (id > 0 )
            {
                await Itemrepo.delete_item(id);
                

                return RedirectToAction("index");
            }

            return View();
        }


        public async Task<ActionResult> CreateEdit(int? id)
        {
            List<Size> n = new List<Size>(); 
            


            ItemSizeVM vm = new ItemSizeVM();
            if (id.HasValue)
            {
                var qurable = await Itemrepo.get_all_items();
                var item =  qurable.Where(x => x.Id == id).FirstOrDefault();
                //Write your get user details code here.  

                vm.Name = item.Name;
                vm.Price = item.Price;
                vm.ImagePath = item.Picture;
                vm.Sizes = item.Sizes.ToList();
               
            }
            return PartialView("_ProductAddorUpdate", vm);

        }





        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> CreateEdit(ItemSizeVM model)
        {
            //validate user  
            if (!ModelState.IsValid)
                return PartialView("_ProductAddorUpdate", model);
            var path = UploadedFile(model);
            Item item = new Item
            {
                Name = model.Name,
                Price = model.Price,
                Picture = path,
                Sizes = model.Sizes
            };
            var id = await Itemrepo.add_item(item);

            //save user into database   
            return RedirectToAction("index");
        }

       
    



        private string UploadedFile(ItemSizeVM model)
        {
            string uniqueFileName = null;

            if (model.ItemImage != null)
            {
                string uploadsFolder = Path.Combine(HostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ItemImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ItemImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

    }
}
