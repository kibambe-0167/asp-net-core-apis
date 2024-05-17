using Microsoft.EntityFrameworkCore;
using assessment.Models;
using Azure.Core;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data.Common;
using System.Security.Principal;
using System.Security;
using System.Threading;

namespace assessment.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext( DbContextOptions<UserDbContext> options ) : base( options ) {}

        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Server=localhost\SQLEXPRESS01;Database=assessment;Trusted_Connection=True;
            // "Server=.;Database=Assessment;Integrated Security=True"
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS01;Database=assessmentdb;Trusted_Connection=True;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
