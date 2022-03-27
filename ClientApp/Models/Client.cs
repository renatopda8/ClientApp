using System.ComponentModel.DataAnnotations;

namespace ClientApp.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string? Name { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only non negative ages are allowed.")]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [StringLength(100)]
        public string? State { get; set; }

        [Required]
        [StringLength(100)]
        public string? City { get; set; }
    }
}