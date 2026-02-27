using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyTaskAPI.Model.Tasks;
using System.Reflection.Metadata;


namespace MyTaskAPI.Model
{
    public class ApplicationDbContext : IdentityDbContext<
        ApplicationUser, ApplicationRole, string,
        IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        { 
        
        }

        public DbSet<Status> TaskStatuses { get; set; }

        public DbSet<ExecutorTask> Executors { set; get; }       

        public DbSet<MyTask> Tasks { set; get; }

        public DbSet<TimeSpent> TimeSpents { set; get; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });

            modelBuilder.Entity<Status>().HasData(
            new Status { id = 1, name= "To Do",  createAt= DateTime.Now , updateAt= DateTime.Now },
            new Status { id = 2, name = "In Progress", createAt = DateTime.Now, updateAt = DateTime.Now },
            new Status { id = 3, name = "Blocked", createAt = DateTime.Now, updateAt = DateTime.Now },
            new Status { id = 4, name = "Testing", createAt = DateTime.Now, updateAt = DateTime.Now },
            new Status { id = 5, name = "Done", createAt = DateTime.Now, updateAt = DateTime.Now }
        );


        }


    }       
}
