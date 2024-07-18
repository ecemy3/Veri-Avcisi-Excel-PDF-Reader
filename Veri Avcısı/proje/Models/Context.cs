using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data;
using System.Web.UI.WebControls;
namespace proje.Models
{
    public class Context:DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}