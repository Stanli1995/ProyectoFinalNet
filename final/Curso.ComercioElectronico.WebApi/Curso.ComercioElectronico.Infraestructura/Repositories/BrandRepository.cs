﻿using Curso.CursoElectronico.Dominio.Entities;
using Curso.CursoElectronico.Dominio.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Infraestructura.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ComercioElectronicoDbContext cntext;
        public BrandRepository(ComercioElectronicoDbContext context)
        {
            cntext = context;
        }

        public async Task<ICollection<Brand>> GetAsync()
        {
            return await cntext.Brands.ToListAsync();
        }

        public async Task<List<Brand>> GetAsync(string code)
        {
            var query = cntext.Brands.AsQueryable();
            if (!string.IsNullOrEmpty(code))
            {
                query = from brand in query
                        where brand.Codigo.Contains(code) || brand.Nombre.Contains(code)
                        select brand;
            };
            return await query.ToListAsync();
        }
    }
}
