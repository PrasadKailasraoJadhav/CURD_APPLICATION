
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

using WebApp1.Models;

namespace WebApp1.Data
{
    public class ApplicationDbContex :DbContext
    {
        public ApplicationDbContex(DbContextOptions<ApplicationDbContex> options) : base(options)
        {

        }
        
        public DbSet<Category> Categories { get; set; }
        
    }
}
