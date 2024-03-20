using Ada.DataAccess.Data;
using Ada.DataAccess.Repository;
using Ada.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ada.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitofwork;

        public CategoryController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public IActionResult Index()
        {
            List<Category> CategoryList = _unitofwork.CategoryRepository.GetAll().ToList();
            return View(CategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("", "The Name and Display Order fields cannot be the same");
            }

            if (obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "The name can't be test");
            }

            if (ModelState.IsValid)
            {
                _unitofwork.CategoryRepository.Add(obj);
                _unitofwork.Save();
                return RedirectToAction("Index");
            }
            return View();
            
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDB = _unitofwork.CategoryRepository.Get(u=>u.Id == id);
            //Category? categoryFromDB = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDB = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (categoryFromDB == null)
            {
                return NotFound();
            }

            return View(categoryFromDB);

        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitofwork.CategoryRepository.Update(obj);
                _unitofwork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Category? categoryFromDB = _db.Categories.Find(id);

            // Other options to find the category
            Category? categoryFromDB = _unitofwork.CategoryRepository.Get(u => u.Id == id);
            //Category categoryFromDB = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (categoryFromDB == null)
            {
                return NotFound();
            }
            return View(categoryFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _unitofwork.CategoryRepository.Get(u=>u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitofwork.CategoryRepository.Remove(obj);
            _unitofwork.Save(); 
            return RedirectToAction("Index");

        }
    }
}
