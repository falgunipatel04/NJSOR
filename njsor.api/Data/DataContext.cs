﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using njsor.api.Model;

namespace njsor.api.Data
{
 public class DataContext : DbContext
 {
  public DataContext(DbContextOptions<DataContext> options):base(options){}
  
  public DbSet<Value> Values{get;set;}
  public DbSet<User> Users{get;set;}
 }
}
