using AlumniMuctr.Enums;
using System.ComponentModel.DataAnnotations;

namespace AlumniMuctr.Models
{
    public class FunSaturdayReg
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int NewsId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Status Status { get; set; }
    }
}
