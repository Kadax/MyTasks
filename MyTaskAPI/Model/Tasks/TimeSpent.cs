using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTaskAPI.Model.Tasks
{
    public class TimeSpent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public double time { set; get; }

        [Required]
        public int taskId { set; get; }

        [ForeignKey("taskId")]
        public virtual required MyTask task { set; get; }

        [Required]
        public DateTime createAt { set; get; } = DateTime.Now;

        [Required]
        public DateTime updateAt { set; get; } = DateTime.Now;



    }
}
                                              