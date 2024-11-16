using System.ComponentModel.DataAnnotations;

namespace Asst4.Models
{

  // Models/City.cs
  public class City
  {
    [Key]
    public int ID { get; set; }

    [Required]
    [StringLength(30)]
    [Display(Name = "City Name")]
    public string Name { get; set; }

    [Required]
    [StringLength(2)]
    [Display(Name = "Province Code")]
    public string ProvCode { get; set; }

    public Province Province { get; set; }
    public ICollection<User> Users { get; set; }
  }
}