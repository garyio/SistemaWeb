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
    public class SubcategoriasController : Controller
    {
        private SistemaWebEntities db = new SistemaWebEntities();

        // GET: Subcategorias
        public ActionResult Index()
        {
            var subcategoria = db.Subcategoria.Include(s => s.Categorias);
            return View(subcategoria.ToList());
        }

        // GET: Subcategorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategoria subcategoria = db.Subcategoria.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            return View(subcategoria);
        }

        // GET: Subcategorias/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nombre");
            return View();
        }

        // POST: Subcategorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoriaId,Nombre,Activo")] Subcategoria subcategoria)
        {
            if (ModelState.IsValid)
            {
                db.Subcategoria.Add(subcategoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nombre", subcategoria.CategoriaId);
            return View(subcategoria);
        }

        // GET: Subcategorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategoria subcategoria = db.Subcategoria.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nombre", subcategoria.CategoriaId);
            return View(subcategoria);
        }

        // POST: Subcategorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoriaId,Nombre,Activo")] Subcategoria subcategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subcategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nombre", subcategoria.CategoriaId);
            return View(subcategoria);
        }

        // GET: Subcategorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategoria subcategoria = db.Subcategoria.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            return View(subcategoria);
        }

        // POST: Subcategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subcategoria subcategoria = db.Subcategoria.Find(id);
            db.Subcategoria.Remove(subcategoria);
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
