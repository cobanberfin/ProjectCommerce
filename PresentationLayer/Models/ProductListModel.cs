using EntityLayer;
using System;
using System.Collections.Generic;

namespace PresentationLayer.Models
{
    public class PageInfo
    {
        public int TotalProducts { get; set; }
        public int ItemsPerPage { get; set; } //her sayfada kac eleman olsun
        public int CurrentPage { get; set; } //o anda hangı sayfadayız
        public string CurrentCategory { get; set; } //o anda aktıf kategori tutsun

        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalProducts / ItemsPerPage);
        }

    }



    public class ProductListModel
    {
        public List<Product> Products { get; set; }
        public PageInfo  PageInfo { get; set; }

    }
}
