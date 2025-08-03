using MarvelsApp.Models;
using MarvelsApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarvelsApp.Controllers
{
    public class CharactersController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _environment;
        public CharactersController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            var characters = _context.Characters.OrderByDescending(c =>c.Id).ToList();
            return View(characters);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Add(CharacterDto characterDto)
        {
            if(characterDto.Image == null)
            {
                ModelState.AddModelError("Image", "The image file is required");
            }
            if (!ModelState.IsValid)
            {
                return View(characterDto);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(characterDto.Image!.FileName);
            string imageFullPath = _environment.WebRootPath + "/images/" + newFileName;

            using(var stream = System.IO.File.Create(imageFullPath))
            {
                characterDto.Image.CopyTo(stream);
            }

            Character character = new Character()
            {
                Name = characterDto.Name,
                RealName = characterDto.RealName,
                Alias = characterDto.Alias,
                Gender = characterDto.Gender,
                Category = characterDto.Category,
                Species = characterDto.Species,
                Origin = characterDto.Origin,
                FirstAppearance = characterDto.FirstAppearance,
                ImageUrl = newFileName,
                Creator = characterDto.Creator,
            };

            _context.Characters.Add(character);
            _context.SaveChanges();

            return RedirectToAction("Index", "Characters");
        }

        public IActionResult Edit(int id)
        {
            var character = _context.Characters.Find(id);
            if(character == null)
            {
                return RedirectToAction("Index", "Characters");
            }

            var characterDto = new CharacterDto()
            {
                Name = character.Name,
                RealName = character.RealName,
                Alias = character.Alias,
                Gender = character.Gender,
                Category = character.Category,
                Species = character.Species,
                Origin = character.Origin,
                FirstAppearance = character.FirstAppearance,
                Creator = character.Creator,
                Description = character.Description
            };

            ViewData["characterId"] = character.Id;
            ViewData["ImageUrl"] = character.ImageUrl;

            return View(characterDto);
        }

        [HttpPost]

        public IActionResult Edit(int id, CharacterDto characterDto)
        {
            var character = _context.Characters.Find(id);

            if(character == null)
            {
                return RedirectToAction("Index", "Characters");
            }
            if (!ModelState.IsValid)
            {
                ViewData["characterId"] = character.Id;
                ViewData["ImageUrl"] = character.ImageUrl;
                return View(characterDto);
            }

            string newFileName = character.ImageUrl;

            if(characterDto.Image != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(characterDto.Image!.FileName);
                string imageFullPath = _environment.WebRootPath + "/images/" + newFileName;

                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    characterDto.Image.CopyTo(stream);
                }
                string oldImageUrl = _environment.WebRootPath + "/images/" + character.ImageUrl;
                System.IO.File.Delete(oldImageUrl);
            }

            character.Name = characterDto.Name;
            character.RealName = characterDto.RealName;
            character.Alias = characterDto.Alias;
            character.Gender = characterDto.Gender;
            character.Category = characterDto.Category;
            character.Species = characterDto.Species;
            character.Origin = characterDto.Origin;
            character.FirstAppearance = characterDto.FirstAppearance;
            character.Creator = characterDto.Creator;
            character.Description = characterDto.Description;
            character.ImageUrl = newFileName;

            _context.SaveChanges();

            return RedirectToAction("Index", "Characters");
        }

        public IActionResult Delete(int id)
        {
            var character = _context.Characters.Find(id);
            if(character == null)
            {
                return RedirectToAction("Index", "Characters");
            }
            string imageFullUrl = _environment.WebRootPath + "/images/" + character.ImageUrl;
            System.IO.File.Delete(imageFullUrl);

            _context.Characters.Remove(character);
            _context.SaveChanges();

            return RedirectToAction("Index", "Characters");
        }

        public IActionResult Search(string query)
        {
            if (string.IsNullOrEmpty(query))
                return RedirectToAction("Index", "Characters");

            var results = _context.Characters
                .Where(c => c.Name.Contains(query))
                .ToList();

            ViewBag.SearchQuery = query;
            return View(results);
        }
    }
}
