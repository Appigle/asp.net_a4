using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Asst4.Utilities;

namespace Asst4.Models
{

  // Models/User.cs
  public class User
  {
    [Key]
    public int ID { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Full Name")]
    public string Name { get; set; }

    [Required]
    [StringLength(100)]
    public string Address { get; set; }

    [Required]
    [StringLength(30)]
    [Display(Name = "City")]
    public string CityName { get; set; }

    [Required]
    [StringLength(6)]
    [Display(Name = "Postal Code")]
    [RegularExpression(@"^[A-Za-z]\d[A-Za-z]\d[A-Za-z]\d$",
     ErrorMessage = "Please enter a valid Canadian postal code (format: A1A1A1)")]
    public string PostalCode { get; set; }

    public City? City { get; set; }
    private string _province;

    [NotMapped]
    public string Province
    {
      get
      {
        if (_province == null && City?.Province != null)
        {
          _province = City.Province.Name;
        }
        return _province ?? CRUDUtilities.ProvinceOfCity(CityName);
      }
    }

    [NotMapped]
    [Display(Name = "Address")]
    public string FullAddress => $"{Address}, {CityName}, {City?.Province?.Code ?? ""}, {PostalCode}";

    public ICollection<UserBridgeClub>? UserBridgeClubs { get; set; }

  }
}