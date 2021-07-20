using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheatreCMS3.Areas.Prod.Models;
using TheatreCMS3.Models;
using System.IO;
using System.Threading.Tasks;

namespace TheatreCMS3.Areas.Prod.Controllers
{
    public class ProductionPhotosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Prod/ProductionPhotoes
        public ActionResult Index()
        {
            return View(db.ProductionPhoto.ToList());
        }

        // GET: Prod/ProductionPhotoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionPhoto productionPhoto = db.ProductionPhoto.Find(id);
            if (productionPhoto == null)
            {
                return HttpNotFound();
            }
            return View(productionPhoto);

        }

        // GET: Prod/ProductionPhotoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Prod/ProductionPhotoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //bind takes input fields from cshtml and matches to model properties
        public ActionResult Create([Bind(Include = "ProPhotoId,Title,Description")] ProductionPhoto productionPhoto, HttpPostedFileBase photoFile)
        {
            if (ModelState.IsValid)
            {
                if(productionPhoto.Title == null || productionPhoto.Description == null|| photoFile == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                productionPhoto.PhotoFile = photoToByteArray(photoFile);
                
                db.ProductionPhoto.Add(productionPhoto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productionPhoto);
        }

        // GET: Prod/ProductionPhotoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionPhoto productionPhoto = db.ProductionPhoto.Find(id);
            if (productionPhoto == null)
            {
                return HttpNotFound();
            }
            return View(productionPhoto);
        }

        // POST: Prod/ProductionPhotoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProPhotoId,Title,Description")] ProductionPhoto productionPhoto, HttpPostedFileBase photoFile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productionPhoto).State = EntityState.Modified;
                //in case user has only changed title & description, not the file - photoFile will come as null. So, make it equal to the the original in db.
                              
                if (photoFile != null)
                {
                    productionPhoto.PhotoFile = photoToByteArray(photoFile);
                }
                else
                {
                    productionPhoto.PhotoFile = (db.ProductionPhoto.Find(productionPhoto.ProPhotoId)).PhotoFile;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productionPhoto);
        }

        // GET: Prod/ProductionPhotoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionPhoto productionPhoto = db.ProductionPhoto.Find(id);
            if (productionPhoto == null)
            {
                return HttpNotFound();
            }
            return View(productionPhoto);
        }

        // POST: Prod/ProductionPhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductionPhoto productionPhoto = db.ProductionPhoto.Find(id);
            db.ProductionPhoto.Remove(productionPhoto);
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


        //[HttpPost]
        //This method changes a photo to a byte array.
        public byte[] photoToByteArray(HttpPostedFileBase photoFile)
        {
            byte[] bytes;
            using (BinaryReader br = new BinaryReader(photoFile.InputStream))
            {
                bytes = br.ReadBytes(photoFile.ContentLength);

            }
            return bytes;
        }


        ////This method returns the byte[] property of the image with given photoId.
        //public HttpPostedFileBase byteArrId(int photoId)
        //{
        //    //get to the byte [] of the photo with given id.
        //    ProductionPhoto prodPhoto = db.ProductionPhoto.Find(photoId);
        //    byte[] bytes =  prodPhoto.PhotoFile;

        //    HttpPostedFileBase objFile = new MemoryPostedFile(bytes);
        //    return objFile;
        //}
        //This method takes a photoId, finds the photo, gets its byte[] and returns a file.
        public ActionResult RenderImage(int id)
        {
            ProductionPhoto prodPhoto = db.ProductionPhoto.Find(id);
            byte[] bytes = prodPhoto.PhotoFile;
            if (bytes == null) return View("Index");
            return File(bytes, "image/jpg");
        }
    }
}
