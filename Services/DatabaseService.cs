using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListApp.Models;

namespace ToDoListApp.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;
        private readonly string _dbPath;

        public DatabaseService()
        {
            // Ma'lumotlar bazasi fayli uchun yo'lni aniqlash
            // Har bir platforma uchun mos joyda saqlanadi
            _dbPath = Path.Combine(FileSystem.AppDataDirectory, "ToDoTasks.db3");
            InitializeAsync().SafeFireAndForget(false);
        }

        private async Task InitializeAsync()
        {
            if (_database is not null)
                return;

            // Ma'lumotlar bazasiga ulanish va TaskModel jadvalini yaratish (agar mavjud bo'lmasa)
            _database = new SQLiteAsyncConnection(_dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
            await _database.CreateTableAsync<TaskModel>();
        }

        // Barcha vazifalarni olish
        public async Task<List<TaskModel>> GetTasksAsync()
        {
            await InitializeAsync(); // Bazani ishga tushirishni ta'minlash
            return await _database.Table<TaskModel>().ToListAsync();
        }

        // ID bo'yicha vazifani olish
        public async Task<TaskModel> GetTaskAsync(int id)
        {
            await InitializeAsync();
            return await _database.Table<TaskModel>().Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        // Yangi vazifani saqlash yoki mavjudini yangilash
        public async Task<int> SaveTaskAsync(TaskModel task)
        {
            await InitializeAsync();
            if (task.Id != 0)
            {
                // Mavjud vazifani yangilash
                return await _database.UpdateAsync(task);
            }
            else
            {
                // Yangi vazifani qo'shish
                return await _database.InsertAsync(task);
            }
        }

        // Vazifani o'chirish
        public async Task<int> DeleteTaskAsync(TaskModel task)
        {
            await InitializeAsync();
            return await _database.DeleteAsync(task);
        }
    }

    // SafeFireAndForget xatoliklarni yutib yubormaslik uchun yordamchi class
    public static class TaskExtensions
    {
        public static async void SafeFireAndForget(this Task task, bool returnToCallingContext, Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(returnToCallingContext);
            }
            catch (Exception ex) when (onException != null)
            {
                onException(ex);
            }
            catch (Exception ex)
            {
                // Global xatolikni qayd etish mexanizmi (masalan, AppCenter Crashes)
                System.Diagnostics.Debug.WriteLine($"SafeFireAndForget Error: {ex}");
            }
        }
    }
}
