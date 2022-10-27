using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;


namespace RepositoryLayer.Context
{
    public class FundooContext
    {
        public FundooContext(DbContextOptions options) : base(options)

        {
        }
        public DbSet<UserEntity> Usertable { get; set; }
    }
}
