using Microsoft.AspNetCore.Mvc;
using POSTest.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSTest.Controllers
{
    public class ProductsController : Controller
    {
        public ProductsController(IRepositoryItem itemrepo)
        {
            Itemrepo = itemrepo;
        }

        public IRepositoryItem Itemrepo { get; }

        public async Task<IActionResult> Index()
        {

          var q =  await  Itemrepo.get_all_items();
            return View(q.ToList());
        }
    }
}
