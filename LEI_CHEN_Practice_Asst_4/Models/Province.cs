using System.ComponentModel.DataAnnotations;

namespace Asst4.Models
{
  public class Province
  {
    [Key]
    public int ID { get; set; }

    [Required]
    [StringLength(30)]
    [Display(Name = "Province Name")]
    public string Name { get; set; }

    [Required]
    [StringLength(2)]
    [Display(Name = "Province Code")]
    public string Code { get; set; }

    public ICollection<City> Cities { get; set; }
  }
}