﻿using Ada.DataAccess.Repository;
using Ada.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ada.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        public CourseController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public IActionResult Index()
        {
            List<Course> CourseList = _unitofwork.CourseRepository.GetAll().ToList();
            return View(CourseList);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitofwork.CategoryRepository.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            ViewBag.CategoryList = CategoryList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course obj)
        {
            if (obj != null && obj.Title == obj.Description.ToString())
            {
                ModelState.AddModelError("Title", "The Title and Description fields cannot be the same");
            }
            if (obj != null && obj.Title.ToLower() == "test")
            {
                ModelState.AddModelError("", "The Title field cannot be 'test'");
            }
            if (ModelState.IsValid)
            {
                _unitofwork.CourseRepository.Add(obj);
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
            Course? courseFromDB = _unitofwork.CourseRepository.Get(u => u.Id == id);

            // Other options to find the category
            //Category categoryFromDB = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category categoryFromDB = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (courseFromDB == null)
            {
                return NotFound();
            }
            return View(courseFromDB);
        }

        [HttpPost]
        public IActionResult Edit(Course obj)
        {
            if (ModelState.IsValid)
            {
                _unitofwork.CourseRepository.Update(obj);
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
            Course? courseFromDB = _unitofwork.CourseRepository.Get(u => u.Id == id);

            // Other options to find the category
            //Category categoryFromDB = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category categoryFromDB = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (courseFromDB == null)
            {
                return NotFound();
            }
            return View(courseFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Course? obj = _unitofwork.CourseRepository.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitofwork.CourseRepository.Remove(obj);
            _unitofwork.Save();
            return RedirectToAction("Index");
        }
    }
}
