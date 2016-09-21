using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingsForYou.Models
{
    public class Category
    {
        //public string CountryCode { get; set; }
        public int CategoryID { get; set; }
       
        public string CategoryName { get; set; }

        public static IQueryable<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category
                    {
                        CategoryID=1,
                        CategoryName = "Beverages"
                    },
                new Category
                    {
                        CategoryID=2,
                        CategoryName = "Cakes"
                    },
                new Category
                    {
                        CategoryID=3,
                        CategoryName = "Cateres"
                    },
                new Category
                    {
                        CategoryID=4,
                        CategoryName = "Churches"
                    },
                new Category
                    {
                        CategoryID=5,
                        CategoryName = "Ceremony Venues"
                    },
                new Category
                    {
                        CategoryID=6,
                        CategoryName = "Bridal Cars"
                    },
                new Category
                    {
                        CategoryID=7,
                        CategoryName = "Bridal Shoes"
                    },
                new Category
                    {
                        CategoryID=8,
                        CategoryName = "Florists"
                    },
                new Category
                    {
                        CategoryID=9,
                        CategoryName = "Dance Lessons"
                    },
                new Category
                    {
                        CategoryID=10,
                        CategoryName = "Mens Wear"
                    },
                new Category
                    {
                        CategoryID=11,
                        CategoryName = "Reception Venues"
                    },
                new Category
                    {
                        CategoryID=12,
                        CategoryName = "Photographers"
                    },
                new Category
                    {
                        CategoryID=13,
                        CategoryName = "Live Music"
                    },
                new Category
                    {
                        CategoryID=14,
                        CategoryName = "Accommodation"
                    }
            }.AsQueryable();
        }
    }
}