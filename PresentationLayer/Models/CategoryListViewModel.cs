using EntityLayer;

using System.Collections.Generic;

namespace ShoesShop.WebUI.Models
{
    public class CategoryListViewModel
    {
        public List<Category> Categories { get; set; }
        public string SelectedCategory { get; set; }
    }
}
