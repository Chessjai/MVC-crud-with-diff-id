using Stage2.Models;
using Stage2.StudentRepo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stage2.DBCLASS;

namespace Stage2.Controllers
{
    public class HomeController : Controller
    {
        Defaultdbcontext db = new Defaultdbcontext();
        Repo model = new Repo();
        // GET: Home

        public ActionResult GetAll()
        {

            Repo obj = new Repo();
            ModelState.Clear();
            return View(obj.getstudentmodel());

        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Student obj)
        {


            if (ModelState.IsValid)
            {
                Repo obj1 = new Repo();
                obj1.addstudent(obj);
                return RedirectToAction("GetAll");

            }
            return View();


        }
        [HttpGet]
        public ActionResult Edit(string id, Repo obj)
        {
            Repo obj1 = new Repo();
            return View(obj.GetStudentByID(id));


        }
        [HttpPost]
        public ActionResult Edit(string studentid, Student obj)
        {
            if (ModelState.IsValid)
            {
                Repo obj1 = new Repo();
                obj1.UpdateStudents(obj);
                return RedirectToAction("GetAll");

            }
            return View();

        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            var std = db.students.Where(x => x.studentid == id).FirstOrDefault();
            return View(std);

        }

        public ActionResult delete(string id)
        {
            if (ModelState.IsValid)
            {
                Repo obj1 = new Repo();
                obj1.delete(id);
                return RedirectToAction("GetAll");


            }
            return View();
        }
        public ActionResult Details(string id)
        {
            var data = model.getstudentmodel().Find(Stud => Stud.studentid == id);
            return View(data);
        }


      

       
    }


}
