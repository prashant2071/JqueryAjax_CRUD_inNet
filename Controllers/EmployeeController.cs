using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatatablesAjaxJqueryInsertUpdate.Models;

namespace DatatablesAjaxJqueryInsertUpdate.Controllers
{
    public class EmployeeController : Controller
    {
        MVCdatatablesajaxEntities1 db = new MVCdatatablesajaxEntities1();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            List<tblEmployee> lst = db.tblEmployees.ToList();

         
            return Json(new {data=lst},JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddorEdit(int id)
        {
            if (id == 0)
            {
                return View(new tblEmployee());
            }
            else
            {
                tblEmployee tb = db.tblEmployees.Where(a => a.EmployeeId == id).FirstOrDefault();

                return View(tb);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(tblEmployee tb)
        {
            if (tb.EmployeeId == 0)
            { 
                db.tblEmployees.Add(tb);
                db.SaveChanges();
                return Json(new { success = true, message = "Saved Sucessfully" }, JsonRequestBehavior.AllowGet);
            }
            else{
                //tblEmployee tbl = db.tblEmployees.Where(a => a.EmployeeId == tb.EmployeeId).FirstOrDefault();
                //tbl.Name = tb.Name;
                //tbl.Office = tb.Name;
                //tbl.Position = tb.Position;
                //tbl.Salary = tb.Salary;
                //tbl.Age = tb.Age;
                db.Entry(tb).State = EntityState.Modified;
                db.SaveChanges(); 
                return Json(new { success = true, message = "Updated Sucessfully" }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult Delete(int id)
        {
            tblEmployee tb = db.tblEmployees.Where(a => a.EmployeeId == id).FirstOrDefault();
            db.tblEmployees.Remove(tb);
            return Json(new { success = true, message = "Deleted Sucessfully" }, JsonRequestBehavior.AllowGet);


        }
    }
}