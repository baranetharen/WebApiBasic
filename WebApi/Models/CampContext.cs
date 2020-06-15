using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class CampContext :DbContext 
    {
        public CampContext():base("DefaultConnection")
        {

        }

        public DbSet<Camp> Camps { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Talk> Talks { get; set; }
    }
}