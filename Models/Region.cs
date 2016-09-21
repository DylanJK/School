using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeddingsForYou.Models
{
    public class Region
    {
        public string RegionCode { get; set; }
        public string RegionName { get; set; }


        public static IQueryable<Region> GetRegions()
        {
            return new List<Region>
            {
                new Region {

                    RegionCode = "AU",
                    RegionName = "Auckland"
                },
                new Region{

                    RegionCode = "HM",
                    RegionName = "Hamilton"
                },
                new Region{

                    RegionCode = "WT",
                    RegionName = "Wellington"
                },
                new Region{

                    RegionCode = "DN",
                    RegionName = "Dunedin"
                },
                new Region{

                    RegionCode = "CH",
                    RegionName = "Christchurch"
                }
            }.AsQueryable();
        }
    }
}