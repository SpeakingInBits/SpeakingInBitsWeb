using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpeakingInBitsWeb.Data;
using SpeakingInBitsWeb.Models;

namespace SpeakingInBitsWeb.Controllers;

[Authorize(Roles = Roles.Instructor)]
public class CoursesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public CoursesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Courses
    public async Task<IActionResult> Index()
    {
        string? userId = _userManager.GetUserId(User);
        var courses = await _context.Courses
            .Where(c => c.CourseInstructor != null && c.CourseInstructor.Id == userId)
            .ToListAsync();
        return View(courses);
    }

    // GET: Courses/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Courses/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Course course)
    {
        string? userId = _userManager.GetUserId(User);
        Instructor? instructor = await _context.Users.OfType<Instructor>()
            .FirstOrDefaultAsync(i => i.Id == userId);
        
        if (instructor != null)
        {
            course.CourseInstructor = instructor;
        }

        // Remove CourseInstructor from ModelState since it's being set here programmatically
        // It must be removed because it's happening after model binding and validation
        ModelState.Remove(nameof(Course.CourseInstructor));

        if (ModelState.IsValid)
        {
            _context.Add(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(course);
    }

    // GET: Courses/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        

        // Fetch the course from the database, including the instructor
        var courseToUpdate = await _context.Courses
            .Include(c => c.CourseInstructor)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (courseToUpdate == null)
        {
            return NotFound();
        }

        // Get current user ID to check ownership
        string? userId = _userManager.GetUserId(User);
        if (courseToUpdate.CourseInstructor == null || courseToUpdate.CourseInstructor.Id != userId)
        {
            return Forbid();
        }

        return View(courseToUpdate);
    }

    // POST: Courses/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Course course)
    {
        if (id != course.Id)
        {
            return NotFound();
        }

        // Remove CourseInstructor from ModelState since we're not updating it
        ModelState.Remove(nameof(Course.CourseInstructor));

        if (ModelState.IsValid)
        {
            // Get current user ID
            string? userId = _userManager.GetUserId(User);

            // Fetch the course from the database, including the instructor
            var courseToUpdate = await _context.Courses
                .Include(c => c.CourseInstructor)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (courseToUpdate == null)
            {
                return NotFound();
            }

            // Check ownership
            if (courseToUpdate.CourseInstructor == null || courseToUpdate.CourseInstructor.Id != userId)
            {
                return Forbid();
            }

            // Update allowed properties only (do not update instructor)
            courseToUpdate.Title = course.Title;
            courseToUpdate.Description = course.Description;
            courseToUpdate.CourseCode = course.CourseCode;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        return View(course);
    }

    // GET: Courses/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        // Get current user ID
        string? userId = _userManager.GetUserId(User);

        // Fetch the course from the database, including the instructor
        var courseToDelete = await _context.Courses
            .Include(c => c.CourseInstructor)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (courseToDelete == null)
        {
            return NotFound();
        }

        // Check ownership
        if (courseToDelete.CourseInstructor == null || courseToDelete.CourseInstructor.Id != userId)
        {
            return Forbid();
        }

        return View(courseToDelete);
    }

    // POST: Courses/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        // Get current user ID
        string? userId = _userManager.GetUserId(User);

        // Fetch the course from the database, including the instructor
        var courseToDelete = await _context.Courses
            .Include(c => c.CourseInstructor)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (courseToDelete == null)
        {
            return NotFound();
        }

        // Check ownership
        if (courseToDelete.CourseInstructor == null || courseToDelete.CourseInstructor.Id != userId)
        {
            return Forbid();
        }

        _context.Courses.Remove(courseToDelete);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
