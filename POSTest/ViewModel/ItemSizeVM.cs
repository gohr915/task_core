using Microsoft.AspNetCore.Http;
using POSTest.NewFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace POSTest.ViewModel
{
    public class ItemSizeVM
    {
        public string  Name { get; set; }

        public double Price { get; set; }

        [Required]
        public IFormFile ItemImage { get; set; }

        public List<Size> Sizes { get; set; }

        public string  ImagePath { get; set; }
    }
}
