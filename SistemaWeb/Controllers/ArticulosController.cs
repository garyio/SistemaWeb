using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BDModel;
using System.IO;

namespace SistemaWeb.Controllers
{
    public class ArticulosController : Controller
    {
        private SistemaWebEntities db = new SistemaWebEntities();

        // GET: Articulos
        public ActionResult Index()
        {
            var articulos = db.Articulos.Include(a => a.Proveedor).Include(a => a.Subcategoria).Include(a => a.UnidadMedida);
            return View(articulos.ToList());
        }

        // GET: Articulos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulos articulos = db.Articulos.Find(id);
            if (articulos == null)
            {
                return HttpNotFound();
            }
            return View(articulos);
        }

        // GET: Articulos/Create
        public ActionResult Create()
        {
            ViewBag.ProveedorId = new SelectList(db.Proveedor, "Id", "NombreFiscal");
            ViewBag.SubcategoriaId = new SelectList(db.Subcategoria, "Id", "Nombre");
            ViewBag.UnidadId = new SelectList(db.UnidadMedida, "Id", "Nombre");
            //return PartialView();
            return View();
        }

        // POST: Articulos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Codigo,SubcategoriaId,Descripcion,PrecioCompra,PrecioVenta,Existencia,FechaAlta,Activo,UnidadId,ProveedorId")] Articulos articulos)
        {
            if (ModelState.IsValid)
            {
                db.Articulos.Add(articulos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProveedorId = new SelectList(db.Proveedor, "Id", "NombreFiscal", articulos.ProveedorId);
            ViewBag.SubcategoriaId = new SelectList(db.Subcategoria, "Id", "Nombre", articulos.SubcategoriaId);
            ViewBag.UnidadId = new SelectList(db.UnidadMedida, "Id", "Nombre", articulos.UnidadId);
            return PartialView("Create", articulos);
        }

        // GET: Articulos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulos articulos = db.Articulos.Find(id);
            if (articulos == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProveedorId = new SelectList(db.Proveedor, "Id", "NombreFiscal", articulos.ProveedorId);
            ViewBag.SubcategoriaId = new SelectList(db.Subcategoria, "Id", "Nombre", articulos.SubcategoriaId);
            ViewBag.UnidadId = new SelectList(db.UnidadMedida, "Id", "Nombre", articulos.UnidadId);
            return View(articulos);
        }

        // POST: Articulos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo,SubcategoriaId,Descripcion,PrecioCompra,PrecioVenta,Existencia,FechaAlta,Activo,UnidadId,ProveedorId")] Articulos articulos)
        {

            if (ModelState.IsValid)
            {
                db.Entry(articulos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProveedorId = new SelectList(db.Proveedor, "Id", "NombreFiscal", articulos.ProveedorId);
            ViewBag.SubcategoriaId = new SelectList(db.Subcategoria, "Id", "Nombre", articulos.SubcategoriaId);
            ViewBag.UnidadId = new SelectList(db.UnidadMedida, "Id", "Nombre", articulos.UnidadId);
            return View(articulos);
        }

        // GET: Articulos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulos articulos = db.Articulos.Find(id);
            if (articulos == null)
            {
                return HttpNotFound();
            }
            return View(articulos);
        }

        // POST: Articulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Articulos articulos = db.Articulos.Find(id);
            db.Articulos.Remove(articulos);
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

        // GET: Articulos/UploadFile
        public ActionResult UploadFiles(int? id)
        {
            string _articulosHTML = string.Empty;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulos articulos = db.Articulos.Find(id);
            if (articulos == null)
            {
                return HttpNotFound();
            }
            else
            {
                string filepath = Server.MapPath("~/ImagenesArticulos/Articulo_" + articulos.Id);
                DirectoryInfo d = new DirectoryInfo(filepath);
                ViewBag.articulosHTML = d.GetFiles().OrderBy(f => f.CreationTime);
            }


            return View(articulos);
        }

        //POST: Articulos/UploadFiles
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult UploadFiles(HttpPostedFileBase[] files, [Bind(Include = "Id,Codigo,SubcategoriaId,Descripcion,PrecioCompra,PrecioVenta,Existencia,FechaAlta,Activo,UnidadId,ProveedorId")] Articulos articulos)
        {
            try
            {
                articulos = db.Articulos.Find(articulos.Id);
                if (articulos == null)
                {
                    return HttpNotFound();
                }

                if (files.Length > 0)
                {
                    foreach (HttpPostedFileBase file in files)
                    {

                        //var fileContent = Request.Files[file];
                        if (file != null && file.ContentLength > 0)
                        {
                            // get a stream
                            var stream = file.InputStream;
                            // and optionally write the file to disk
                            var fileName = file.FileName;
                            var path = Path.Combine(Server.MapPath("~/ImagenesArticulos/Articulo_" + articulos.Id), fileName);
                            if (this.CreateFolderIfNeeded(Server.MapPath("~/ImagenesArticulos/Articulo_" + articulos.Id)))
                                file.SaveAs(path);
                            DirectoryInfo d = new DirectoryInfo(Server.MapPath("~/ImagenesArticulos/Articulo_" + articulos.Id));
                            ViewBag.articulosHTML = d.GetFiles().OrderBy(f => f.CreationTime);
                        }

                    }
                    TempData["message_UploadFiles_success"] = "Imagenes cargadas exitosamente";
                }
                
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["message_UploadFiles_failed"] = "error al cargar las Imagenes " + ex.InnerException;
                return RedirectToAction("Index");
            }
        }

        //POST: Articulos/deleteFile/1
        //[ResponseType(typeof(Articulos))]
        [HttpPost]
        public ActionResult deleteFile(string id, string filename)
        {
            string filepath = Server.MapPath("~/ImagenesArticulos/Articulo_" + id.ToString());
            DirectoryInfo d = new DirectoryInfo(filepath);

            string fullPath = Path.Combine(filepath, filename);

            if (!System.IO.File.Exists(fullPath))
            {
                ViewBag.articulosHTML = d.GetFiles().OrderBy(f => f.CreationTime);
                return Json("The Image doesn't exit, Please verify .!");
            }

            try
            {
                System.IO.File.Delete(fullPath);
                ViewBag.articulosHTML = d.GetFiles().OrderBy(f => f.CreationTime);
                return Json("Image Deleted .!");
            }
            catch (Exception e)
            {
                ViewBag.articulosHTML = d.GetFiles().OrderBy(f => f.CreationTime);
                return Json("Error, " + e.InnerException);
            }

        }

        public bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception ex)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }

        //[HttpPost]
        //public ActionResult UploadFiles(int id)
        //{
        //    try
        //    {
        //        HttpPostedFileBase hpf = HttpContext.Request.Files["file"] as HttpPostedFileBase;
        //        string tag = HttpContext.Request.Params["tags"];// The same param name that you put in extrahtml if you have some.
        //        string category = HttpContext.Request.Params["category"];

        //        DirectoryInfo di = Directory.CreateDirectory(Server.MapPath("~/ImagenesArticulos/Articulo_" + id));// If you don't have the folder yet, you need to create.
        //        string sentFileName = Path.GetFileName(hpf.FileName); //it can be just a file name or a user local path! it depends on the used browser. So we need to ensure that this var will contain just the file name.
        //        string savedFileName = Path.Combine(di.FullName, sentFileName);
        //        hpf.SaveAs(savedFileName);

        //        var msg = new { msg = "File Uploaded", filename = hpf.FileName, url = savedFileName };
        //        return Json(msg);
        //    }
        //    catch (Exception e)
        //    {
        //        //If you want this working with a custom error you need to change in file jquery.uploadfile.js, the name of 
        //        //variable customErrorKeyStr in line 85, from jquery-upload-file-error to jquery_upload_file_error 
        //        var msg = new { jquery_upload_file_error = e.Message };
        //        return Json(msg);
        //    }
        //}

        //[Route("{url}")]
        //public ActionResult DownloadFile(string url)
        //{
        //    return File(url, "application/pdf");
        //}

        //[HttpPost]
        //public ActionResult DeleteFile(string url)
        //{
        //    try
        //    {
        //        System.IO.File.Delete(url);
        //        var msg = new { msg = "File Deleted!" };
        //        return Json(msg);
        //    }
        //    catch (Exception e)
        //    {
        //        //If you want this working with a custom error you need to change the name of 
        //        //variable customErrorKeyStr in line 85, from jquery-upload-file-error to jquery_upload_file_error 
        //        var msg = new { jquery_upload_file_error = e.Message };
        //        return Json(msg);
        //    }
        //}

        // POST: Articulos/UploadFile
        //[HttpPost]
        //public ActionResult UploadFile(HttpPostedFileBase file)
        //{
        //    try
        //    {
        //        if (file.ContentLength > 0)
        //        {
        //            string _FileName = Path.GetFileName(file.FileName);
        //            string _path = Path.Combine(Server.MapPath("~/ImagenesArticulos"), _FileName);
        //            file.SaveAs(_path);
        //        }
        //        ViewBag.Message = "File Uploaded Successfully!!";
        //        return Json(new { success = ViewBag.Counter }, JsonRequestBehavior.AllowGet);
        //        //return PartialView();
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Message = "File upload failed!!";
        //        return Json("Error occurred. Error details: " + ex.Message, JsonRequestBehavior.AllowGet);
        //    }
        //}

    }
}
