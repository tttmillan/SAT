using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SAT.DATA.EF;
using SAT.UI.MVC.Models;
using SAT.UI.MVC.Utilities;



namespace SAT.UI.MVC.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private SATEntities db = new SATEntities();

        // GET: Students
        public ActionResult Index()
        {
            var Students = db.Students.Include(s => s.StudentStatus);
            return View(Students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student Student = db.Students.Find(id);
            if (Student == null)
            {
                return HttpNotFound();
            }
            return View(Student);
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
        public ActionResult Create([Bind(Include = "StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student Student, HttpPostedFileBase photoUrl)//variable must match the name attribute in the input.
        {
            if (ModelState.IsValid)
            {
                //******Added Region From Org.
                #region File Upload
                //default value for the image path - noImage.png
                string imageName = "noImage.png";

                //Check the input from the form and see if it is not null
                if (photoUrl != null)
                {
                    //get the file name and save to a variable
                    imageName = photoUrl.FileName;

                    //get the extension or file type from the uploaded file
                    string ext = imageName.Substring(imageName.LastIndexOf("."));

                    //assemble a list of acceptable file types
                    string[] goodext = new string[] { ".jpeg", ".jpg", ".png", ".gif" };

                    //Then compare the file type against the list of acceptable file types
                    if (goodext.Contains(ext.ToLower()) && photoUrl.ContentLength <= 4194304)
                    {
                        //If valid file type, rename the image to a GUID - to make sure each file name that we create is unique
                        imageName = Guid.NewGuid() + ext;

                        #region Resize Image and Save to server
                        string savePath = Server.MapPath("~/Content/images/StudentImages/");

                        //Below we actually pass the data from the image to our program - Image Service that will find its dimensions and resize the image
                        Image convertedImage = Image.FromStream(photoUrl.InputStream);

                        //set the max size of our image here
                        int maxImageSize = 500;

                        //set the thumbnail
                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, imageName, convertedImage, maxImageSize, maxThumbSize);
                        #endregion

                        //No matter what we will update the photo URL with the value of the file variable
                        Student.PhotoUrl = imageName;
                    }
                    #endregion

                    ////Save the file to the web server
                    //photoUrl.SaveAs(Server.MapPath("~/Content/images/StudentImages/" + imageName));
                }
                //else
                //    {
                //        //This is for when the file is not the right ext
                //        imageName = "noImage.png";
                //    }
                //}

                ////Save to the database no matter whether the file is noImage or it is valid and we have an image to save.
                //Student.PhotoUrl = imageName;

               


                db.Students.Add(Student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", Student.SSID);
            return View(Student);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student Student = db.Students.Find(id);
            if (Student == null)
            {
                return HttpNotFound();
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", Student.SSID);
            return View(Student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student Student, HttpPostedFileBase PhotoUrl)//Added comma and everything after to par en
        {
            if (ModelState.IsValid)
            {
                //Added region to org
                #region File Upload

                string imageName = "NoImage.png";

                //check file upload to see if the user has chosen a new file
                if (PhotoUrl != null)
                {
                    //get the file name and assign to a variable

                    //string imageName = PhotoUrl.FileName;

                    //find the extension
                    string ext = imageName.Substring(imageName.LastIndexOf("."));

                    //declare a collection of good exts
                    string[] goodExts = new string[] { ".jpeg", ".jpg", ".png", ".gif" };

                    //check the variable against the good exts - if good rename and save the file
                    if (goodExts.Contains(ext.ToLower()) && PhotoUrl.ContentLength <= 4194304)
                    {
                        //Rename the file
                        imageName = Guid.NewGuid() + ext;

                        #region Resize Image and Save to server
                        string savePath = Server.MapPath("~/Content/images/StudentImages/");

                        //Below we actually pass the data from the image to our program - Image Service that will find its dimensions and resize the image
                        Image convertedImage = Image.FromStream(PhotoUrl.InputStream);

                        //set the max size of our image here
                        int maxImageSize = 500;

                        //set the thumbnail
                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, imageName, convertedImage, maxImageSize, maxThumbSize);
                        #endregion

                        //save it to the web server

                        //PhotoUrl.SaveAs(Server.MapPath("~/Content/images/StudentImages/" + imageName));

                        //Delete the old file - ensure we are not deleting the noImage.png
                        //More functionality to come in MVC3

                        //string currentFile = Request.Params["PhotoUrl"];

                        if (Student.PhotoUrl != "noImage.png" && Student.PhotoUrl != null)
                        {
                            //delete the previously associated image from the web server
                            string path = Server.MapPath("~/Content/images/StudentImages/");
                            ImageUtility.Delete(path, Student.PhotoUrl);

                            //System.IO.File.Delete(Server.MapPath("~/Content/images/StudentImages/" + currentFile));
                        }

                    Student.PhotoUrl = imageName;
                    }//only if the image meets all of the criteria - send the image name to the database
                }
                //if the image does not meet our criteria OR no file was included, the HiddenFor() in the Edit View will maintain the current image with no manual interaction from the user.

                #endregion
                db.Entry(Student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", Student.SSID);
            return View(Student);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student Student = db.Students.Find(id);
            if (Student == null)
            {
                return HttpNotFound();
            }
            return View(Student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Student Student = db.Students.Find(id);
            //Soft delete - switch the bool/bit value to the opposite of its current value
            //changes inactive to active and vice versa
            //Student.IsActive = !Student.IsActive;
            db.Students.Remove(Student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        //// POST: Courses/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Course course = db.Courses.Find(id);
        //    //Soft delete - switch the bool/bit value to the opposite of its current value
        //    //changes inactive to active and vice versa
        //    course.IsActive = !course.IsActive;

        //    //db.Courses.Remove(course);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


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
