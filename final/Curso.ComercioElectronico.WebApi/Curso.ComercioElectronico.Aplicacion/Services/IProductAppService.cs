using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.CursoElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface IProductAppService
    {
        //Metodo para obtener productos con paginacion, ordenamiento y busquedas 
        Task<ResultadoPaginacion<ProductDto>> GetListaAsync(string? search="",int offset=0,int limite=10, string sort = "Name",string order = "asc");
        Task<ICollection<ProductDto>> GetAllAsync();

        //METODO PARA PAGINACION
       Task<ICollection<ProductDto>> GetListaAsync(int limit=10); //limita  para cargar nuestra paginas de productos 
        Task<ProductDto> GetAsync(Guid Id);
        Task CreateAsync(CreateProductDto productDto);
        Task UpdateAsync(Guid id, CreateProductDto putproductDto);
        Task<bool> DeleteAsync(Guid id);
        
    }

    public class ResultadoPaginacion<T>
    { 
        public int Total { get; set; }
        public ICollection<T> Item { get; set; } = new List<T>();
    }
}
