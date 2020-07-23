using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudTable.Models;
using CrudTable.Models.vistaModel;

namespace CrudTable.Controllers
{
    public class PersonaController : Controller
    {
        // GET: Persona
        public ActionResult Index()
        {
            return View();
        }

        
        //Listar elementos
        public ActionResult Listar()
        {
            return View(obtenerLista());
        }

        //Metodo para obtener elementos del modelo y hacer uso de los mismos en diferentes apartados
        IEnumerable<listPersona> obtenerLista()
        {
            List<listPersona> lst;

            using (DBEntities db = new DBEntities())
            {
              
                    lst = (from d in db.tabla
                           select new listPersona
                           {
                               Id = d.id,
                               Nombre = d.nombre,
                               Correo = d.correo

                           }).ToList();


                return lst;
            }
        }

 
        [HttpGet]
        public ActionResult Editar(int id)
        {

            Persona per = new Persona();

            using (DBEntities db = new DBEntities())
            {
                var oTabla = db.tabla.Find(id);

                per.Nombre = oTabla.nombre;
                per.Correo = oTabla.correo;
                per.Fecha_Nacimiento = oTabla.fecha_nacimiento;
                per.Id = oTabla.id;

            }


            return View(per);
        }




        [HttpPost]
        public ActionResult Editar(Persona per)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    using (DBEntities db = new DBEntities())
                    {
                        var oTabla = db.tabla.Find(per.Id);

                        oTabla.nombre = per.Nombre;
                        oTabla.correo = per.Correo;


                        db.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                    }
                }

                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "Listar", obtenerLista()), message = "Editado exitosamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);

            }

        }


        [HttpPost]
        public ActionResult Nuevo(Persona per)
        {
            try
            {
                using (DBEntities db = new DBEntities())
                {
                    var oTabla = new tabla();

                    oTabla.id = per.Id;
                    oTabla.nombre = per.Nombre;
                    oTabla.correo = per.Correo;
                    oTabla.fecha_nacimiento = per.Fecha_Nacimiento;

                    db.tabla.Add(oTabla);
                    db.SaveChanges();

                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "Listar", obtenerLista()), message = "Guardado exitosamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                using (DBEntities db = new DBEntities())
                {
                    var oTabla = db.tabla.Find(id);

                    db.tabla.Remove(oTabla);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "Listar", obtenerLista()), message = "Eliminado correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }





    }
}