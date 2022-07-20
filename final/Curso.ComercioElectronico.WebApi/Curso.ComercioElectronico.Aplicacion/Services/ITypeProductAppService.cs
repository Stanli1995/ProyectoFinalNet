using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.CursoElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface ITypeProductAppService
    {
        //Metodo para obtener tipos de productos con paginacion, ordenamiento y busquedas 
        Task<ResultadoPaginacionTypeProduct<TypeProductDto>> GetListaAsync(string? search = "", int offset = 0, int limite = 10, string sort = "Code", string order = "asc");
        ///Task<ICollection<TypeProduct>> GetAsync();
        Task<ICollection<TypeProductDto>> GetAllAsync();
        Task<List<TypeProductDto>> GetAsync(string codigo);
        Task CreateAsync(CreateTypeProductDto TypeProductDto);
        Task UpdateAsync(string id, CreateTypeProductDto putTypeProductDto);
        Task<bool> DeleteAsync(string id);
    }
    public class ResultadoPaginacionTypeProduct<T>
    {
        public int Total { get; set; }
        public ICollection<T> Item { get; set; } = new List<T>();
    }
}
