using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTaskAPI.Model.Tasks
{
    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { set; get; }
        public string name { set; get; }
        [Required]
        public DateTime createAt { set; get; } = DateTime.Now;
        [Required]
        public DateTime updateAt { set; get; } = DateTime.Now;
    }
}
