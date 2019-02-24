using System;
using System.Collections.Generic;

public class Tarea
{
    public int Id {get; set;}
    public string Descripcion {get; set;}
    public int Prioridad {get; set;}
    public List<ComentarioEnTarea> Comentarios {get; set;} = new List<ComentarioEnTarea>();
}

public class ComentarioEnTarea
{
    public int Id {get; set;}
    public int TareaId {get; set;}
    public string Comentario {get; set;}
    public DateTime Fecha {get; set;}
}