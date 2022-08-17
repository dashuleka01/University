using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Models;

namespace DataAccess
{
    public class UniContext : DbContext
    {
        public UniContext() { }
        public UniContext(DbContextOptions<UniContext> options) : base(options)
        {
            //this.Database.EnsureDeleted();
            //this.Database.EnsureCreated();
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonGroup> LessonGroup { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TypeLesson> TypeClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<LessonGroup>(entity => {
                entity.HasKey(lg => new { lg.GroupId, lg.LessonId });

                entity.HasOne(lg => lg.Group)
                .WithMany(g => g.LessonGroups)
                .HasForeignKey(lg => lg.GroupId);

                entity.HasOne(lg => lg.Lesson)
                .WithMany(l => l.LessonGroups)
                .HasForeignKey(lg => lg.LessonId);
            });
        }
    }
}

