using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asst4;
using LEI_CHEN_Practice_Asst_4.Data;
using Asst4.Utilities;
using Asst4.Models;

namespace LEI_CHEN_Practice_Asst_4.Controllers
{
  public class UserController : Controller
  {
    private readonly LEI_CHEN_Practice_Asst_4_DbContext _context;

    public UserController(LEI_CHEN_Practice_Asst_4_DbContext context)
    {
      _context = context;
    }


    // GET: User/Index
    [HttpGet]
    public async Task<IActionResult> Index(string sortOrder, string searchString)
    {
      ViewBag.CurrentFilter = searchString;
      ViewBag.CityNameSortParm = sortOrder == "city" ? "city_desc" : "city";

      IQueryable<User> usersQuery = _context.Users
          .Include(u => u.City)
          .Include(u => u.UserBridgeClubs);

      if (!String.IsNullOrEmpty(searchString))
      {
        usersQuery = usersQuery.Where(u => u.Name.Contains(searchString)
                               || u.CityName.Contains(searchString));
      }

      usersQuery = sortOrder switch
      {
        "city" => usersQuery.OrderBy(u => u.CityName),
        "city_desc" => usersQuery.OrderByDescending(u => u.CityName),
        _ => usersQuery.OrderBy(u => u.Name)
      };

      var users = await usersQuery.ToListAsync();

      var allClubs = await _context.BridgeClubs.ToListAsync();
      var userClubs = await _context.UserBridgeClubs.ToListAsync();
      ViewBag.UserClubs = users.ToDictionary(
                 u => u.ID,
                 u => userClubs.Where(uc => uc.UserID == u.ID)
                              .Select(uc => uc.BridgeClub)
                              .ToList()
             );
      ViewBag.AvailableClubs = users.ToDictionary(
          u => u.ID,
          u => allClubs.Where(c => !userClubs.Any(uc =>
              uc.UserID == u.ID && uc.ClubID == c.ClubID)).ToList()
      );

      return View(users);
    }

    // GET: User/ClubIndex
    [HttpGet]
    public async Task<IActionResult> ClubIndex()
    {
      var clubs = await _context.BridgeClubs
          .Include(c => c.City)
          .Include(c => c.UserBridgeClubs)
          .ToListAsync();
      return View(clubs);
    }

    // GET: User/NewClub
    [HttpGet]
    public IActionResult NewClub()
    {
      ViewBag.Cities = _context.Cities.ToList();
      return View();
    }

    // POST: User/CreateClub
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateClub([Bind("ClubName,CityName")] BridgeClub bridgeClub)
    {
      if (ModelState.IsValid)
      {
        _context.Add(bridgeClub);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(ClubIndex));
      }
      ViewBag.Cities = _context.Cities.ToList();
      return View("NewClub", bridgeClub);
    }

    // POST: User/JoinClub
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> JoinClub(int userId, int clubId)
    {
      if (clubId == 0 || userId == 0)
        return RedirectToAction(nameof(Index));

      var userBridgeClub = new UserBridgeClub
      {
        UserID = userId,
        ClubID = clubId
      };

      _context.UserBridgeClubs.Add(userBridgeClub);
      await _context.SaveChangesAsync();

      return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LeaveClub(int userId, int clubId)
    {
      var membership = await _context.UserBridgeClubs
          .FirstOrDefaultAsync(uc => uc.UserID == userId && uc.ClubID == clubId);

      if (membership != null)
      {
        _context.UserBridgeClubs.Remove(membership);
        await _context.SaveChangesAsync();
      }

      return RedirectToAction(nameof(Index));
    }

    // GET: User
    public async Task<IActionResult> Index2()
    {
      var users = await _context.Users
           .Include(u => u.City)
           .ThenInclude(c => c.Province)
           .ToListAsync();
      return View(users);
    }

    // GET: User/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
        return NotFound();

      var user = await _context.Users
          .Include(u => u.City)
          .ThenInclude(c => c.Province)
          .FirstOrDefaultAsync(m => m.ID == id);

      if (user == null)
        return NotFound();

      return View(user);
    }
    [HttpGet]
    [Route("Users/GetProvinceForCity")]
    public JsonResult GetProvinceForCity(string cityName)
    {
      var province = _context.Cities
          .Include(c => c.Province)
          .Where(c => c.Name == cityName)
          .Select(c => c.Province.Name)
          .FirstOrDefault();

      return Json(province);
    }

    // GET: User/Create
    public IActionResult Create()
    {
      ViewData["Cities"] = new SelectList(_context.Cities, "Name", "Name");
      return View();
    }

    // POST: User/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,Name,Address,CityName,PostalCode")] User user)
    {
      if (ModelState.IsValid)
      {
        user.PostalCode = StringUtilities.PostalCode(user.PostalCode);

        if (string.IsNullOrEmpty(user.PostalCode))
        {
          ModelState.AddModelError("PostalCode", "Invalid postal code format");
          ViewData["Cities"] = new SelectList(_context.Cities, "Name", "Name", user.CityName);
          return View(user);
        }

        _context.Add(user);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      ViewData["Cities"] = new SelectList(_context.Cities, "Name", "Name", user.CityName);
      return View(user);
    }

    // GET: User/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var user = await _context.Users.FindAsync(id);
      if (user == null)
      {
        return NotFound();
      }
      return View(user);
    }

    // POST: User/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Address,CityName,PostalCode")] User user)
    {
      if (id != user.ID)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(user);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!UserExists(user.ID))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(user);
    }

    // GET: User/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var user = await _context.Users
          .FirstOrDefaultAsync(m => m.ID == id);
      if (user == null)
      {
        return NotFound();
      }

      return View(user);
    }

    // POST: User/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var user = await _context.Users.FindAsync(id);
      if (user != null)
      {
        _context.Users.Remove(user);
      }

      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool UserExists(int id)
    {
      return _context.Users.Any(e => e.ID == id);
    }
  }
}
