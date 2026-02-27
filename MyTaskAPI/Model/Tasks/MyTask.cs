using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTaskAPI.Model.Tasks
{
    public class MyTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string title { set; get; }
        public string? description { set; get; }
        public bool? isArchive { set; get; }

        [Required]
        public int statusId { set; get; }

        [ForeignKey("statusId")]
        public virtual TaskStatus status { set; get; }

        public int orderNumber { set; get; } = 0;

        public int? executorId { set; get; }
        [ForeignKey("executorId")]
        public virtual ExecutorTask? executor { set; get; }

        public double totalTime { set; get; } = 0;

        [Required]
        public DateTime createAt { set; get; } = DateTime.Now;

        [Required]
        public DateTime updateAt { set; get; } = DateTime.Now;

    }
}
