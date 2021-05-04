using BazarNetCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazarNetCore.Data
{
    public class ApplicationContex : DbContext
    {
        public ApplicationContex(DbContextOptions<ApplicationContex> options)
            : base(options)
        {
            string trace = "SERVER START!!";
            //Database.EnsureCreated();   // создаем базу данных при первом обращении
            Trace.WriteLine(this);
            Trace.WriteLine(trace);
            try
            {
                FileStream fileStreamLog = new FileStream(@"Trace.log", FileMode.Append);
                for (int i = 0; i < trace.Length; i++)
                {
                    byte[] array = Encoding.Default.GetBytes(trace.ToString());
                    fileStreamLog.Write(array, 0, array.Length);
                }

                fileStreamLog.Close();
            }
            catch (Exception exp)
            {
                Trace.WriteLine(exp.ToString());
            }
        }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Prodacts> Prodacts { get; set; }
        public DbSet<ProdactType> ProdactTypes { get; set; }
        public DbSet<Salers> Salers { get; set; }
    }
}
