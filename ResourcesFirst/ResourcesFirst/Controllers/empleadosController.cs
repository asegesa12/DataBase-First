using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ResourcesFirst.Models;

namespace ResourcesFirst.Controllers
{
    public class empleadosController : Controller
    {
        private EkkoEntities db = new EkkoEntities();

        // GET: empleados
        public ActionResult Index(string busqueda)
        {
            var empleados = db.empleados.Include(e => e.cargos).Include(e => e.departamentos);

            ViewData["CurrentFilter"] = busqueda;

            var lista = from x in db.empleados
                        select x;

            if (String.IsNullOrEmpty(busqueda))
            {
                return View(db.empleados.ToList());
            }
            else
            {
                lista = lista.Where(x => x.nombre.Contains(busqueda) || x.departamentos.codigoDepartamento.Contains(busqueda));
                return View(lista);
            }
        }

        // GET: empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleados empleados = db.empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // GET: empleados/Create
        public ActionResult Create()
        {
            ViewBag.id_cargos = new SelectList(db.cargos, "id_cargos", "codigoCargo");
            ViewBag.id_departamento = new SelectList(db.departamentos, "id_departamento", "codigoDepartamento");
            return View();
        }

        // POST: empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,codigoEmpleado,nombre,apellido,telefono,fechaIngreso,salario,estado,id_departamento,id_cargos")] empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.empleados.Add(empleados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_cargos = new SelectList(db.cargos, "id_cargos", "codigoCargo", empleados.id_cargos);
            ViewBag.id_departamento = new SelectList(db.departamentos, "id_departamento", "codigoDepartamento", empleados.id_departamento);
            return View(empleados);
        }

        // GET: empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleados empleados = db.empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cargos = new SelectList(db.cargos, "id_cargos", "codigoCargo", empleados.id_cargos);
            ViewBag.id_departamento = new SelectList(db.departamentos, "id_departamento", "codigoDepartamento", empleados.id_departamento);
            return View(empleados);
        }

        // POST: empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,codigoEmpleado,nombre,apellido,telefono,fechaIngreso,salario,estado,id_departamento,id_cargos")] empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cargos = new SelectList(db.cargos, "id_cargos", "codigoCargo", empleados.id_cargos);
            ViewBag.id_departamento = new SelectList(db.departamentos, "id_departamento", "codigoDepartamento", empleados.id_departamento);
            return View(empleados);
        }

        // GET: empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleados empleados = db.empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            empleados empleados = db.empleados.Find(id);
            db.empleados.Remove(empleados);
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
