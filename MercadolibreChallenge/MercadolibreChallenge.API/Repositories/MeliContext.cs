using MercadolibreChallenge.API.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MercadolibreChallenge.API.Repositories
{
    public class MeliContext : DbContext
    {
        private static readonly string WorkingDirectory = Environment.CurrentDirectory;
        private string _connString;


        public MeliContext() : base()
        {
            _connString = GetDataSource();
        }

        public MeliContext(string connString) : base()
        {
            _connString = connString;
        }

        public DbSet<Human> Humans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(_connString);

        private string GetDataSource()
        {
            //habria que sacar el datasource de un config
            //string projectDirectory = Directory.GetParent(WorkingDirectory).FullName + "\\";
            string projectDirectory = WorkingDirectory + "\\";
            string dataSource = string.Empty;
#if DEBUG
            projectDirectory = Directory.GetParent(WorkingDirectory).FullName + "\\";
            dataSource = $"Data Source={projectDirectory}MercadolibreChallenge.API\\Meli.db";
#else
            // dataSource = $"Data Source={projectDirectory}onesolutionsdb.db";
            dataSource = "Data Source=D:\\DISTRINANDO_WMS_PRODUCCION\\TEST_CROCS\\onesolutionsdb.db";
#endif
            return dataSource;
        }
    }
}
