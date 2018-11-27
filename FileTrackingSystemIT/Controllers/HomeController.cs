using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FileTrackingSystemIT.Models;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace FileTrackingSystemIT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["LogedUserFullname"] == null)
                return RedirectToAction("LogIn");
                var model = db.File_Record;
            return View(model.ToList());
        }

        FileTrackingSystemIT.Models.DB_FTSEntities db = new FileTrackingSystemIT.Models.DB_FTSEntities();

        [ValidateInput(false)]
        public ActionResult FTSPartial()
        {
            var model = db.File_Record;
            return PartialView("_FTSPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult FTSPartialAddNew(FileTrackingSystemIT.Models.File_Record item)
        {
            var model = db.File_Record;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_FTSPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult FTSPartialUpdate(FileTrackingSystemIT.Models.File_Record item)
        {
            var model = db.File_Record;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.ID == item.ID);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_FTSPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult FTSPartialDelete(System.Int32 ID)
        {
            var model = db.File_Record;
            if (ID >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.ID == ID);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_FTSPartial", model.ToList());
        }

        public ActionResult LogIn()
        {
            if (Session["LogedUserFullname"] != null)
                return RedirectToAction("Index");
            else
                return View();
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("LogIn");
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(User u)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using (db)
                {
                    var v = db.Users.Where(a => a.UserName.Equals(u.UserName) && a.UserPassword.Equals(u.UserPassword)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserID"] = v.UserID.ToString();
                        Session["LogedUserFullname"] = v.UserName.ToString();
                        Session["Role"] = v.Role.Role1.ToString();
                        if(v.Picture!=null)
                        Session["Picture"] = v.Picture.ToString();
                        return RedirectToAction("Index");
                    }
                    
                        
                }
            }
            ViewBag.Message = "          Invalid UserName or Password ";
            return View();
        }
        [HttpGet]
       public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp( User model,HttpPostedFileBase file)
        {
            if (Session["LogedUserID"] != null)
            {

            }

            var u = new User();
            u.DOB = model.DOB;
            u.Email = model.Email;
            u.Gender = model.Gender;
            u.IsActive = model.IsActive;
            u.Phone = model.Phone;
            u.UserName = model.UserName;
            u.UserPassword = model.UserPassword;
            //CodeForImageUpload
            string uploadPath = Server.MapPath("~/Uploads");
            file.SaveAs(Path.Combine(uploadPath, u.UserName+".jpeg"));
            
            u.Picture = "/Uploads"+"/"+u.UserName+".jpeg";

            db.Users.Add(u);
            db.SaveChanges();
            return View("LogIn");
        }
        public ActionResult Edit()
        {
            int id = Convert.ToInt16(Session["LogedUserID"]);
            var user = db.Users.Find(id);
            return View(user);
        }
        public ActionResult Report()
        {
            var s = Session["Role"].ToString();
            if (s == "Admin")
            {
                List<File_Record> fr = new List<File_Record>();
                fr = db.File_Record.ToList();
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Report"), "CrystalReport1.rpt"));

                rd.SetDataSource(fr);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);

                return File(stream, "application/pdf", "Report.pdf");
            }
            else
            {   
                
               Content("<script language='javascript' type='text/javascript'>alert('You do not have the privilages');</script>");
                return RedirectToAction("Index");
            }
                
        }

    }
}