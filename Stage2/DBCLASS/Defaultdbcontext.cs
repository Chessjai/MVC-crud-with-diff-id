using Stage2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Stage2.DBCLASS
{
    public class Defaultdbcontext:DbContext
  
    {
        public Defaultdbcontext():base("MyConnectionString")
        {
            Database.SetInitializer<Defaultdbcontext>(null);
        }
        public DbSet<Student> students { get; set; }
    }
}