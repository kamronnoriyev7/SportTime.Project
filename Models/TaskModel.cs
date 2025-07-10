using SQLite; // SQLite uchun atributlardan foydalanish uchun

namespace ToDoListApp.Models
{
    public class TaskModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } // Avtomatik ortib boruvchi unikal ID

        [MaxLength(255)]
        public string Title { get; set; } // Vazifa sarlavhasi

        public string Description { get; set; } // Vazifa tavsifi

        public bool IsCompleted { get; set; } // Vazifa bajarilganligi holati

        public DateTime DueDate { get; set; } // Vazifaning tugash sanasi
    }
}
