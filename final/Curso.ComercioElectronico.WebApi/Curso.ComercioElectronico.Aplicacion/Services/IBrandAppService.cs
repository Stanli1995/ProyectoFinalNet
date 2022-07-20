using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.CursoElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface IBrandAppService
    {
        //Metodo para obtener marcas del producto con paginacion, ordenamiento y busquedas 
        Task<ResultadoPaginacionBrand<BrandDto>> GetListaAsync(string? search = "", int offset = 0, int limite = 10, string sort = "Code", string order = "asc");

        //Task<ICollection<Brand>> GetAsync();
        Task<ICollection<BrandDto>> GetAllAsync();
        Task<List<BrandDto>> GetAsync(string code);
        Task CreateAsync(CreateBrandDto brandDto);
        Task UpdateAsync(string id,CreatePutBrandDto putBrandDto);
        Task<bool> DeleteAsync(string id);

    }
    public class ResultadoPaginacionBrand<T>
    {
        public int Total { get; set; }
        public ICollection<T> Item { get; set; } = new List<T>();
    }
}
