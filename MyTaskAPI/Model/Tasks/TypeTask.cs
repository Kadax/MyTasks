using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTaskAPI.Model.Tasks
{
    public class TypeTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public required string name { get; set; }
        public string color { set; get; }
    }
}
