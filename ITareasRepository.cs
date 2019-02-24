using System;
using System.Collections.Generic;

internal interface ITareasRepository
{
    List<Tarea> LeerTodasLasTareas();
    Tarea LeerTareaPorId(int id);
    void CrearTarea(Tarea model);
    void ActualizarTarea(Tarea model);
    void BorrarTarea(int id);
    void ComentarTarea(ComentarioEnTarea comentario);
}