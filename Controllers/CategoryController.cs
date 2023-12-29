using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using WebApp1.Data;
using WebApp1.Models;


namespace WebApp1.Controllers
{
    public class CategoryController : Controller
    {
       private readonly ApplicationDbContex _context;

        public CategoryController(ApplicationDbContex context)
         {
            _context = context;
        }

        public async Task<ActionResult> Index(string sort, string search, int page = 1)
        {
            
            var category =await _context.Categories.ToListAsync();

            ViewBag.Sort = sort;
            ViewBag.Search = search;
            if (search != null)
            {
                category = category.Where(s => s.Name!.Contains(search)).ToList();
            }
            switch (sort)
            {
                case "Name_desc":
                    category = category.OrderByDescending(s => s.Name).ToList();
                    break;

                default:
                    category = category.OrderBy(s => s.Name).ToList();
                    break;
            }
            /*if(sort == "Name_desc")
            {
                category = category.OrderByDescending(s => s.Name).ToList();
            }
            if (sort == "Name_asc")
            {
                category = category.OrderByDescending(s => s.Name).ToList();
            }
            else
            {
                category = category.OrderBy(s => s.Name).ToList();
            }*/
            ViewBag.TotalPages = Math.Ceiling(category.Count() / 4.0);

            category = category.Skip((page - 1) * 4).Take(5).ToList();

            return View(category);

        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["success"] = "Created!";
                return RedirectToAction("Index");
            }
            return View(category);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                TempData["success"] = "Updated!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var category = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            if (id == null || id == 0)
            {
                return NotFound();
            }

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteData(int? id)
        {
            var category = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            TempData["success"] = "Deleted!";
            return RedirectToAction("Index");
        }
    }
}