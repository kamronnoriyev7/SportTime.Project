using System.ComponentModel.DataAnnotations;

namespace SportTime.Models
{
    /// <summary>
    /// Vazifa modelini ifodalaydi - har bir vazifaning asosiy ma'lumotlari
    /// </summary>
    public class TaskModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; } = false;

        public DateTime? DueDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? CompletedAt { get; set; }
    }
}