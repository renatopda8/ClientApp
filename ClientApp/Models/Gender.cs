using System.ComponentModel.DataAnnotations;

namespace ClientApp.Models
{
    public enum Gender
    {
        [Display(Name = "Male")] Male = 1,
        [Display(Name = "Female")] Female = 2,
        [Display(Name = "Not Informed")] NotInformed = 3
    }
}