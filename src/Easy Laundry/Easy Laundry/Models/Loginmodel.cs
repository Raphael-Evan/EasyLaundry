using System.ComponentModel.DataAnnotations;

namespace Easy_Laundry.Models
{
    public class Loginmodel
    {
        [Key]
        public int UId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
