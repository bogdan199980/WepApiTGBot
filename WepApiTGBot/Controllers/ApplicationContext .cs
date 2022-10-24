using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using TelegramClass;

namespace WepApiTGBot.Controllers
{
    public class ApplicationContext : DbContext
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DbSet<UserSetting> Users { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<UserSetting>().HasKey(b => b.db_id);
        //   modelBuilder.Entity<UserSetting>().HasData( new UserSetting { user_id = 1, is_bot = false, language_code = "UA", username="bogdan" });
        }
    }
}
