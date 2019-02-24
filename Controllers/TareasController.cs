using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proyecto_clase_ingweb.Models;

namespace proyecto_clase_ingweb.Controllers
{
    public class TareasController : Controller
    {

        private readonly ITareasRepository tareas;

        public TareasController(){
            tareas = new SqliteTareasRepository();
        }
        // GET: Tareas
        public ActionResult Index()
        {
            var lista = tareas.LeerTodasLasTareas();
            return View(lista);
        }

        // GET: Tareas/Details/5
        public ActionResult Details(int id)
        {
            var tarea = tareas.LeerTareaPorId(id);
            return View(tarea);
        }

        // GET: Tareas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tareas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tarea model)
        {
            try
            {
                // TODO: Add insert logic here
                tareas.CrearTarea(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Tareas/Edit/5
        public ActionResult Edit(int id)
        {
            var model = tareas.LeerTareaPorId(id);
            return View(model);
        }

        // POST: Tareas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tarea model)
        {
            var tarea = tareas.LeerTareaPorId(id);

            if (tarea == null){
                return NotFound();
            }

            tareas.ActualizarTarea(model);                
            return RedirectToAction(nameof(Index));
        }

        // GET: Tareas/Delete/5
        public ActionResult Delete(int id)
        {
            var model = tareas.LeerTareaPorId(id);

            if (model == null){
                return NotFound();
            }

            return View(model);
        }

        // POST: Tareas/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tarea model)
        {
            tareas.BorrarTarea(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: Tareas/Comentar/5
        [HttpPost]
        public ActionResult Comentar(int id,string comentario)
        {
            var tarea = tareas.LeerTareaPorId(id);

            if (tarea == null){
                return NotFound();
            }

            var comentarioEnTarea = new ComentarioEnTarea(){
                Comentario = comentario,
                TareaId = id,
                Fecha = DateTime.Now    
            };

            tareas.ComentarTarea(comentarioEnTarea);
            return RedirectToAction(nameof(Details),new { id = id});
        }
    }
}