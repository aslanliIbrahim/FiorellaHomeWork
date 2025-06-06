﻿using FiorellaHomeWork.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorellaHomeWork.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Slide> Slides { get; set; }
        public DbSet<Introduction> Introduction { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<ProductImage> productImages { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<HeaderLogo> headerLogos { get; set; }


    }
}
