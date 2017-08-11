using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BDModel;

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
            return View(articulos);
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
    }
}
