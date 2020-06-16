using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudCodeFirst.Models;
using System.Data.Entity;

namespace CrudCodeFirst.Controllers
{
    public class PersonaController : Controller
    {
        ApplicationDbContext Db = new ApplicationDbContext();
        // GET: Persona
        public ActionResult Index()
        {
            return View(Db.Personas.ToList());
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear([Bind(Include = "Id,Nombre,Nacimiento,Edad")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                Db.Personas.Add(persona);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(persona);
        }

        public ActionResult Editar(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Nombre,Nacimiento,Edad")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                var Editar = new Persona();
                Editar.Id = persona.Id;
                Editar.Nombre = persona.Nombre;
                Editar.Nacimiento = persona.Nacimiento;
                Editar.Edad = persona.Edad;
                Db.Entry(Editar).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(persona);
        }

        public ActionResult Eliminar()
        {
            return View();
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int? id)
        {
                var Persona = Db.Personas.Find(id);
                Db.Personas.Remove(Persona);
                Db.SaveChanges();
                return RedirectToAction("Index");
        }
    }
}