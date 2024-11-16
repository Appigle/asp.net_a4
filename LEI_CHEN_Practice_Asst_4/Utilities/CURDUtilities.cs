using Asst4.Models;
using LEI_CHEN_Practice_Asst_4.Data;
using Microsoft.EntityFrameworkCore;

namespace Asst4.Utilities
{
  public static class CRUDUtilities
  {
    private static IServiceProvider _serviceProvider;

    public static void Initialize(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }

    public static bool UserExists(string userName)
    {
      using var scope = _serviceProvider.CreateScope();
      using var context = scope.ServiceProvider.GetRequiredService<LEI_CHEN_Practice_Asst_4_DbContext>();
      return context.Users.Any(u => u.Name == userName);
    }

    public static bool UserExists(User user)
    {
      if (user == null) return false;
      return UserExists(user.Name);
    }

    public static string ProvinceOfCity(string cityName)
    {
      if (string.IsNullOrEmpty(cityName))
        return string.Empty;

      using var scope = _serviceProvider.CreateScope();
      using var context = scope.ServiceProvider.GetRequiredService<LEI_CHEN_Practice_Asst_4_DbContext>();

      return context.Cities
          .Include(c => c.Province)
          .Where(c => c.Name == cityName)
          .Select(c => c.Province.Name)
          .FirstOrDefault() ?? string.Empty;
    }

    public static bool LivesInProv(string name, string prov)
    {
      using var scope = _serviceProvider.CreateScope();
      using var context = scope.ServiceProvider.GetRequiredService<LEI_CHEN_Practice_Asst_4_DbContext>();

      return context.Users
          .Include(u => u.City)
          .ThenInclude(c => c.Province)
          .Any(u => u.Name == name && u.City.Province.Code == prov);
    }
  }
}