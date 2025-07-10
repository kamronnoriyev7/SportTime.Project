using Microsoft.EntityFrameworkCore;
using SportTime.Models;

namespace SportTime.Services
{
    /// <summary>
    /// SQLite ma'lumotlar bazasi bilan ishlash uchun servis
    /// Entity Framework Core orqali CRUD operatsiyalarini amalga oshiradi
    /// </summary>
    public class DatabaseService : DbContext
    {
        public DbSet<TaskModel> Tasks { get; set; }

        private readonly string _dbPath;

        public DatabaseService()
        {
            // Ma'lumotlar bazasi faylini mahalliy qurilmada saqlash yo'li
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _dbPath = Path.Combine(folder, "TodoTasks.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // TaskModel konfiguratsiyasi
            modelBuilder.Entity<TaskModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("datetime('now')");
            });
        }

        /// <summary>
        /// Ma'lumotlar bazasini yaratish va migratsiya qilish
        /// </summary>
        public async Task InitializeDatabaseAsync()
        {
            await Database.EnsureCreatedAsync();
        }

        /// <summary>
        /// Barcha vazifalarni olish
        /// </summary>
        public async Task<List<TaskModel>> GetAllTasksAsync()
        {
            return await Tasks.OrderByDescending(t => t.CreatedAt).ToListAsync();
        }

        /// <summary>
        /// Yangi vazifa qo'shish
        /// </summary>
        public async Task<TaskModel> AddTaskAsync(TaskModel task)
        {
            Tasks.Add(task);
            await SaveChangesAsync();
            return task;
        }

        /// <summary>
        /// Vazifani yangilash
        /// </summary>
        public async Task<TaskModel> UpdateTaskAsync(TaskModel task)
        {
            Tasks.Update(task);
            await SaveChangesAsync();
            return task;
        }

        /// <summary>
        /// Vazifani o'chirish
        /// </summary>
        public async Task DeleteTaskAsync(int taskId)
        {
            var task = await Tasks.FindAsync(taskId);
            if (task != null)
            {
                Tasks.Remove(task);
                await SaveChangesAsync();
            }
        }

        /// <summary>
        /// Vazifani bajarildi deb belgilash
        /// </summary>
        public async Task ToggleTaskCompletionAsync(int taskId)
        {
            var task = await Tasks.FindAsync(taskId);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
                task.CompletedAt = task.IsCompleted ? DateTime.Now : null;
                await SaveChangesAsync();
            }
        }
    }
}