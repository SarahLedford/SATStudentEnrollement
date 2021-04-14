using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SATStudentEnrollment.DATA.EF;
using SATStudentEnrollment.UI.MVC.Utilities;

namespace SATStudentEnrollment.UI.MVC.Controllers
{
    [Authorize(Roles = "Admin, Scheduling")]
    public class StudentsController : Controller
    {
        private SATEntities db = new SATEntities();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.StudentStatus);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase studentPhoto)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                string file = "NoImage.png";

                if (studentPhoto != null)
                {
                    file = studentPhoto.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };
                    //check that the uploaded file ext is in our approved list of extensions
                    //check that the file is less than 4MB, which is the default
                    //allowed file-size by .NET
                    if (goodExts.Contains(ext.ToLower()) && studentPhoto.ContentLength <= 4194304)
                    {
                        //create a new filename *using a GUID
                        file = Guid.NewGuid() + ext;

                        #region Resize Image
                        string savePath = Server.MapPath("~/Content/images/StudentImages/");

                        Image convertedImage = Image.FromStream(studentPhoto.InputStream);

                        int maxImageSize = 500; //full size image width
                        int maxThumbSize = 100; //thumbnail size image width

                        imageService.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                        #endregion
                    }



                }
                //no matter what, update the PhotoURL with the value of the file variable
                student.PhotoUrl = file;

                #endregion
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase studentPhoto)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                string file = student.PhotoUrl;

                if (studentPhoto != null)
                {
                    file = student.PhotoUrl;

                    string ext = file.Substring(file.LastIndexOf('.'));

                    string[] goodExts = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (goodExts.Contains(ext.ToLower()) && studentPhoto.ContentLength <= 4194304)
                    {
                        file = Guid.NewGuid() + ext;

                        #region resize image
                        string savePath = Server.MapPath("~/Content/images/StudentImages/");

                        //Making an object of type Image from a stream of bytes that are coming from
                        //our HttpPosterFileBase object called bookCover
                        //HttpPostedFileBase bookCover -> bytes -> Image -> convertedImage
                        Image convertedImage = Image.FromStream(studentPhoto.InputStream);

                        int maxImageSize = 500;
                        int maxThumbSize = 100;

                        imageService.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                        #endregion

                        if (student.PhotoUrl != null && student.PhotoUrl != "NoImage.png")
                        {
                            imageService.Delete(savePath, student.PhotoUrl);
                        }

                        student.PhotoUrl = file;
                    }

                }
                #endregion
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
