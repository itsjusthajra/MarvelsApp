using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarvelsApp.Models;
using MarvelsApp.Services;

namespace MarvelsApp.Controllers
{
    public class TeamCharactersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamCharactersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TeamCharacters
        public async Task<IActionResult> Index()
        {
            var data = _context.Teams
         .Select(t => new TeamWithCharactersViewModel
         {
            TeamName = t.Name,
            Characters = t.TeamCharacters
                          .Select(tc => tc.Character)
                          .ToList()
         })
          .ToList();

          var vm = new TeamsPageViewModel { Teams = data };

           return View(vm);
        }

       
        public async Task<IActionResult> List()
        {
            var applicationDbContext = _context.TeamCharacters.Include(t => t.Character).Include(t => t.Team);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: TeamCharacters/Create
        public IActionResult Create()
        {
            ViewData["CharacterId"] = new SelectList(_context.Characters, "Id", "Name");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
            return View();
        }

        // POST: TeamCharacters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("TeamId,CharacterId")] TeamCharacter teamCharacter)
        {
            // Log incoming values
            Console.WriteLine($"[DEBUG] Incoming TeamId: {teamCharacter.TeamId}, CharacterId: {teamCharacter.CharacterId}");

            // Show all model errors in console
            if (!ModelState.IsValid)
            {
                Console.WriteLine("[DEBUG] ModelState is invalid:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"[DEBUG] Error: {error.ErrorMessage}");
                }
            }

            // Validate both dropdowns were actually selected
            if (teamCharacter.TeamId == 0 || teamCharacter.CharacterId == 0)
            {
                ModelState.AddModelError("", "You must select both a team and a character.");
            }

            // Prevent duplicate team-character pairs
            bool exists = await _context.TeamCharacters
                .AnyAsync(tc => tc.TeamId == teamCharacter.TeamId && tc.CharacterId == teamCharacter.CharacterId);
            if (exists)
            {
                ModelState.AddModelError("", "This character is already assigned to this team.");
            }

            // If still valid, save
            if (ModelState.IsValid)
            {
                _context.Add(teamCharacter);
                await _context.SaveChangesAsync();
                Console.WriteLine("[DEBUG] Insert successful.");
                return RedirectToAction(nameof(Index));
            }

            // Repopulate dropdowns if we return the view
            ViewData["CharacterId"] = new SelectList(_context.Characters, "Id", "Name", teamCharacter.CharacterId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", teamCharacter.TeamId);
            return View(teamCharacter);
        }

        // GET: TeamCharacters/Edit/5
        public async Task<IActionResult> Edit(int? teamId, int? characterId)
        {
            if (teamId == null || characterId == null)
            {
                return NotFound();
            }

            var teamCharacter = await _context.TeamCharacters.FindAsync(teamId, characterId); // both keys here

            if (teamCharacter == null)
            {
                return NotFound();
            }
            ViewData["CharacterId"] = new SelectList(_context.Characters, "Id", "Name", teamCharacter.CharacterId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", teamCharacter.TeamId);
            return View(teamCharacter);
        }

        // POST: TeamCharacters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int originalTeamId, int originalCharacterId, [Bind("TeamId,CharacterId")] TeamCharacter teamCharacter)
        {
            if (ModelState.IsValid)
            {
                var existing = await _context.TeamCharacters
                    .FindAsync(originalTeamId, originalCharacterId);

                if (existing == null)
                {
                    return NotFound();
                }

                // Remove the old record if the keys changed
                if (originalTeamId != teamCharacter.TeamId || originalCharacterId != teamCharacter.CharacterId)
                {
                    _context.TeamCharacters.Remove(existing);
                    _context.TeamCharacters.Add(teamCharacter);
                }
                else
                {
                    _context.Entry(existing).CurrentValues.SetValues(teamCharacter);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CharacterId"] = new SelectList(_context.Characters, "Id", "Name", teamCharacter.CharacterId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", teamCharacter.TeamId);
            return View(teamCharacter);
        }


        // GET: TeamCharacters/Delete/5
        public async Task<IActionResult> Delete(int? teamId, int? characterId)
        {
            if (teamId == null || characterId == null)
            {
                return NotFound();
            }

            var teamCharacter = await _context.TeamCharacters
                .Include(t => t.Character)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(m => m.TeamId == teamId && m.CharacterId == characterId);

            if (teamCharacter != null)
            {
                _context.TeamCharacters.Remove(teamCharacter);
                await _context.SaveChangesAsync();
                return RedirectToAction("List", "TeamCharacters");
            }

            
            
            return NotFound();
            
            
        }


        private bool TeamCharacterExists(int teamId, int characterId)
        {
            return _context.TeamCharacters.Any(e => e.TeamId == teamId && e.CharacterId == characterId);
        }

    }
}
