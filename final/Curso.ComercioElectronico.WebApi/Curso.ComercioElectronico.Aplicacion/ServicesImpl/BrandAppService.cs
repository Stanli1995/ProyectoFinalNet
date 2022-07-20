using AutoMapper;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.CursoElectronico.Dominio.Entities;
using Curso.CursoElectronico.Dominio.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.ServicesImpl
{
    public class BrandAppService : IBrandAppService
    {
        private readonly IGenericRepository<Brand> gnericRepository;
        private readonly IBrandRepository brand;
        private readonly IValidator<CreateBrandDto> validator;
        private readonly IMapper mapper;

        public BrandAppService(IGenericRepository<Brand> genericRepository, IValidator<CreateBrandDto> validator, IMapper mapper)
        {
            gnericRepository = genericRepository;
            this.validator = validator;
            this.mapper = mapper;
            this.brand = brand;
        }
       
        public async Task<ICollection<BrandDto>> GetAllAsync()
        {
            var query = await gnericRepository.GetAsync();

            var result = query.Select(x => new BrandDto
            {
                Code = x.Codigo,
               Nombre = x.Nombre,
                CreateDate = x.CreationDate
            });
            return result.ToList();
        }

        public async Task<List<BrandDto>> GetAsync(string code)
        {
            var entity = await brand.GetAsync(code);
            var listBrandsDto = mapper.Map<List<BrandDto>>(entity);
            return listBrandsDto;
        }

        //DE ESTA MANERA SOLO ENVIO EN EL POST SOLO LOS CAMPOS DE CODIGO Y NOMBRE EN EL POSTMAN
        public async Task CreateAsync(CreateBrandDto brandDto)
        {
            await validator.ValidateAndThrowAsync(brandDto);
            
            var brand = new Brand {
                Codigo = brandDto.Code,
                Nombre = brandDto.Description,
                CreationDate = DateTime.Now //aqui se setea la fecha de creacion 
            };
            
       
            await gnericRepository.CreateAsync(brand);
            
          
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var bran = await gnericRepository.GetAsync(id);

            await gnericRepository.DeleteAsync(bran);
            return true;
        }

        public async Task UpdateAsync(string id, CreatePutBrandDto putBrandDto)
        {
            var bran = await gnericRepository.GetAsync(id);

            bran.Codigo = putBrandDto.Code;
            bran.Nombre = putBrandDto.Description;
            bran.CreationDate = DateTime.Now;

            await gnericRepository.UpdateAsync(bran);
        }

        public async Task<ResultadoPaginacionBrand<BrandDto>> GetListaAsync(string? search = "", int offset = 0, int limite = 10, string sort = "Code", string order = "asc")
        {
            var query = gnericRepository.GetQueryable();
            //Filtrando los no eliminados
            query = query.Where(x => x.IsDeleted == false);

            //0. BUsqueda
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Codigo.ToUpper().Contains(search)
                
                );
            }

            //1. Total 
            var total = await query.CountAsync(); //SIEMPRE LLEVAR UN ORDEN PARA QUE LA CONSULTA DEVUELVA TODOS LOS DATOS 

            //2. Paginacion 
            query = query.Skip(offset).Take(limite);

            //3. Ordenamiento
            if (!string.IsNullOrEmpty(sort))
            {
                //Soporta por el nombre

                //sort => nombre, precio si es otro lanza excepcion 
                switch (sort.ToUpper())
                {
                    case "CODE":
                        query = query.OrderBy(x => x.Codigo);
                        break;
                    default:
                        throw new ArgumentException($"el parametro sort {sort} n es soportado!");
                }
            }

            var result = query.Select(x => new BrandDto
            {
                Code = x.Codigo,
                Nombre = x.Nombre,
                CreateDate = x.CreationDate
            });

            var items = await result.ToListAsync();

            var resultado = new ResultadoPaginacionBrand<BrandDto>();
            resultado.Total = total;
            resultado.Item = items;
            return resultado;
        }
    }
}

