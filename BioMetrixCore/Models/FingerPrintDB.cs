using BioMetrixCore.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioMetrixCore.Models
{
    public class FingerPrintDB : DbContext
    {
        public FingerPrintDB(): base("name=FingerPrintDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FingerPrintDB, Configuration>());
        }
        public DbSet<Log> Logs { get; set; }
    }
}
