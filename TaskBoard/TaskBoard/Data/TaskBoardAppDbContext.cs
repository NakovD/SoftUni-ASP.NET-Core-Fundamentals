using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskBoard.Data.Models;
using Task = TaskBoard.Data.Models.Task;

namespace TaskBoard.Data
{
    public class TaskBoardAppDbContext : IdentityDbContext
    {
        private IdentityUser TestUser { get; set; }

        private Board OpenBoard { get; set; }

        private Board InProgressBoard { get; set; }

        private Board DoneBoard { get; set; }


        public DbSet<Task> Tasks { get; set; } = null!;

        public DbSet<Board> Boards { get; set; } = null!;


        public TaskBoardAppDbContext(DbContextOptions<TaskBoardAppDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Task>()
                .HasOne(t => t.Board)
                .WithMany(b => b.Tasks)
                .HasForeignKey(t => t.BoardId)
                .OnDelete(DeleteBehavior.Restrict);

            SeedUsers();

            builder.Entity<IdentityUser>()
                .HasData(TestUser);

            SeedBoards();
            builder.Entity<Board>()
                .HasData(OpenBoard, InProgressBoard, DoneBoard);

            builder.Entity<Task>()
                .HasData(new Task
                {
                    Id = 1,
                    Title = "Improve CSS styles",
                    Description = "Implement better styling for all public pages",
                    CreatedOn = DateTime.Now.AddDays(-200),
                    OwnerId = TestUser.Id,
                    BoardId = OpenBoard.Id
                },
                new Task
                {
                    Id = 2,
                    Title = "Android Client App",
                    Description = "Create android client app for the taskboard RestFull APi",
                    CreatedOn = DateTime.Now.AddMonths(-5),
                    OwnerId = TestUser.Id,
                    BoardId = OpenBoard.Id
                },
                new Task
                {
                    Id = 3,
                    Title = "Desktop CLient app",
                    Description = "Create windows forms desktop app client for the taskboard Restfull API",
                    CreatedOn = DateTime.Now.AddMonths(-200),
                    OwnerId = TestUser.Id,
                    BoardId = InProgressBoard.Id
                },
                new Task
                {
                    Id = 4,
                    Title = "Create tasks",
                    Description = "Implement [Create task] page for adding new tasks",
                    CreatedOn = DateTime.Now.AddYears(-1),
                    OwnerId = TestUser.Id,
                    BoardId = DoneBoard.Id
                });

            base.OnModelCreating(builder);
        }

        private void SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            TestUser = new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "test@abv.bg",
                NormalizedUserName = "TEST@ABV.BG"
            };

            TestUser.PasswordHash = hasher.HashPassword(TestUser, "very strong password");
        }

        private void SeedBoards()
        {
            OpenBoard = new Board
            {
                Id = 1,
                Name = "Open"
            };

            InProgressBoard = new Board
            {
                Id = 2,
                Name = "In progress"
            };

            DoneBoard = new Board
            {
                Id = 3,
                Name = "Done"
            };
        }
    }
}