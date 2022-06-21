using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLiteDemo
{
    public class RequestRecordContext : DbContext
    {
        public DbSet<RequestRecord> Records { get; set; }
        public string DbPath { get; set; }
        public static readonly Microsoft.Extensions.Logging.LoggerFactory _myLoggerFactory =
    new LoggerFactory(new[] {
        new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
    });
        public RequestRecordContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
           
            DbPath = System.IO.Path.Join(path, "records.db");
        }
        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
            options.UseLoggerFactory(_myLoggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RequestRecord>().ToTable("RequestRecord");
            
        }
    }
    public class RequestRecord
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
