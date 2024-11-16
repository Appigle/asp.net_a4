namespace Asst4.Models
{
  public class UserBridgeClub
  {
    public int UserID { get; set; }
    public User User { get; set; }

    public int ClubID { get; set; }
    public BridgeClub BridgeClub { get; set; }
  }
}