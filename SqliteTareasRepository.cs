using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.Sqlite;

public class SqliteTareasRepository : ITareasRepository
{
    private readonly string constr;
    public SqliteTareasRepository(){
        constr = "Data Source=app.db";
    }
    public void ActualizarTarea(Tarea model)
    {
        string sql = "UPDATE Tareas SET Descripcion = @Descripcion, Prioridad = @Prioridad WHERE Id = @Id";
        using (var conn = new SqliteConnection(constr)){
            conn.Execute(sql,model);
        }
    }

    public void BorrarTarea(int id)
    {
        string sql = "DELETE FROM Tareas WHERE Id = @Id";
        using (var conn = new SqliteConnection(constr)){
            conn.Execute(sql,new {Id = id});
        }
    }

    public void ComentarTarea(ComentarioEnTarea comentario)
    {
        string sql = "INSERT INTO Comentarios (TareaId, Comentario, Fecha) VALUES (@TareaId, @Comentario, @Fecha)";
        using (var conn = new SqliteConnection(constr)){
            conn.Execute(sql,comentario);
        }
    }

    public void CrearTarea(Tarea model)
    {
        string sql = "INSERT INTO Tareas (Descripcion, Prioridad) VALUES (@Descripcion, @Prioridad)";
        using (var conn = new SqliteConnection(constr)){
            conn.Execute(sql,model);
        }
    }

    public Tarea LeerTareaPorId(int id)
    {
        string sql = "SELECT Id,Descripcion,Prioridad FROM Tareas WHERE Id = @Id";
        string sqlComentariosEnTarea = "SELECT Id,TareaId,Fecha,Comentario FROM Comentarios WHERE TareaId = @Id";
        using (var conn = new SqliteConnection(constr)){
            var tarea = conn.QueryFirstOrDefault<Tarea>(sql,new {Id = id});
            var comentarios = conn.Query<ComentarioEnTarea>(sqlComentariosEnTarea,new {Id = id});

            if (comentarios != null){
                tarea.Comentarios.AddRange(comentarios);
            }

            return tarea;
        }
    }

    public List<Tarea> LeerTodasLasTareas()
    {
        string sql = "SELECT Id,Descripcion,Prioridad FROM Tareas";
        using (var conn = new SqliteConnection(constr)){
            var tareas = conn.Query<Tarea>(sql).ToList();
            return tareas;
        }
    }
}