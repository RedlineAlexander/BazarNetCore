﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BazarNetCore.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BazarNetCore
{
    public class Startup
    {
        public bool DEBUG_MODE = false;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllersWithViews();
            string connection = "";
            // �������� ������ ����������� �� ����� ������������
            string trace = "";
            if (DEBUG_MODE == true)
            {
                connection = Configuration.GetConnectionString("DefaultConnection");
                trace = "test connection good";
            }
            else if (DEBUG_MODE == false)
            {

                //connection = Configuration.GetConnectionString("TrainzInfoHostConnection");
                connection = Configuration.GetConnectionString("WebConnection");

                //connection = Configuration.GetConnectionString("TrainzInfoHostConnection");
                trace = ("server connection good!!" + connection);

            }
            // ��������� �������� MobileContext � �������� ������� � ����������
            services.AddDbContext<ApplicationContex>(options => options.UseSqlServer(connection));
            services.AddControllersWithViews();
            FileStream fileStreamLog = new FileStream(@"Trace.log", FileMode.Append);
            for (int i = 0; i < trace.Length; i++)
            {
                byte[] array = Encoding.Default.GetBytes(trace.ToString());
                fileStreamLog.Write(array, 0, array.Length);
            }

            fileStreamLog.Close();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
