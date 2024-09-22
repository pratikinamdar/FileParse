using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using FileParsing.Models;
namespace PersonalElectionMgmt.Models
{
    public class Db : DbContext
    {
        public DbSet<Data> Data { get; set; }

        public string connstr;

        public Db(string connstr)
        {
            this.connstr = connstr;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(connstr);
    }
}
