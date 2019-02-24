using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace proyecto_clase_ingweb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            InitDB();
        }

        private void InitDB()
        {
            var con = new SqliteConnection("Data Source=app.db");
            var cmdTareasCreate = new SqliteCommand("CREATE TABLE IF NOT EXISTS Tareas ( " +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," + 
                "Descripcion TEXT NOT NULL, " + 
                "Prioridad INTEGER NOT NULL" + 
            ")");
            var cmdComentarioEnTareaCreate = new SqliteCommand("CREATE TABLE IF NOT EXISTS Comentarios ( " +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," + 
                "Comentario TEXT NOT NULL, " + 
                "Fecha TEXT NOT NULL," + 
                "TareaId INTEGER NOT NULL" + 
            ")");

            cmdTareasCreate.Connection = con;
            cmdComentarioEnTareaCreate.Connection = con;
            
            try {
                con.Open();
                cmdTareasCreate.ExecuteNonQuery();
                cmdComentarioEnTareaCreate.ExecuteNonQuery();
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}
