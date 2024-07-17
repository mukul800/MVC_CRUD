using Microsoft.AspNetCore.Mvc;
using MVC_Crud.DAL;
using MVC_Crud.Models;
using System.Collections.Generic;
using static MVC_Crud.BLL.parameters;

namespace MVC_Crud.Controllers
{
    public class StudentController : Controller
    {
        private readonly DataAccess _dataAccess;
        public StudentController(DataAccess dataAccess)
        { 
            _dataAccess = dataAccess;
        }
        public async Task<IActionResult> Index()
        {
            List<StudentModel> students = await _dataAccess.GetStudents();
            return View(students);
        }
        public async Task<StudentModel> GetStudentById(int id) 
        { 
            StudentModel student =await _dataAccess.GetStudentById(id);
            return student;
        }
        public async Task<IActionResult> Edit(int id)
        {
            StudentModel student = await _dataAccess.GetStudentById(id);
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(StudentModel objParams) 
        { 
            int res = _dataAccess.Edit(objParams);
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(StudentModel objParams)
        {
            int res = _dataAccess.Insert(objParams);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            int res = _dataAccess.Delete(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterParams objParams)
        {
            var res = await _dataAccess.register(objParams);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<string> GetUsers()
        {
            string res = await _dataAccess.GetUsers();
            return res;
        }
    }
}
