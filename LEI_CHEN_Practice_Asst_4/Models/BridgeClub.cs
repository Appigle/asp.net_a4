using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asst4.Models
{
  public class BridgeClub
  {
    [Key]
    public int ClubID { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Club Name")]
    public string ClubName { get; set; }

    [Required]
    [StringLength(30)]
    [Display(Name = "City")]
    public string CityName { get; set; }

    // Navigation properties
    public City? City { get; set; }

    public ICollection<UserBridgeClub>? UserBridgeClubs { get; set; }

    [NotMapped]
    [Display(Name = "Number of Members")]
    public int MembersBodySize => UserBridgeClubs?.Count ?? 0;
  }
}