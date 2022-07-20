using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.CursoElectronico.Dominio.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.WebApi.Controllers
{
    //SEGMENTOS: 
    //api/
 
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //mediante este atributo se necesita que el usuario este AUTENFIFICADO para poder acceder 
    public class ProductController : ControllerBase, IProductAppService
    {
        private readonly IProductAppService _productAppService;
        public ProductController(IProductAppService poductAppService)
        {
            _productAppService = poductAppService;
        }
        [HttpGet]
        
        public Task<ICollection<ProductDto>> GetAllAsync()
        {
            return _productAppService.GetAllAsync();
        }
        
        [HttpGet("Id")]
        public Task<ProductDto> GetAsync(Guid Id)
        {
            return _productAppService.GetAsync(Id);        
        }
        
        [HttpPost]
        public Task CreateAsync(CreateProductDto productDto)
        {
            return _productAppService.CreateAsync(productDto);
        }

        [HttpPut("Id")]
        public Task UpdateAsync(Guid id, CreateProductDto putproductDto)
        {
            return _productAppService.UpdateAsync(id,putproductDto);
        }

        [HttpDelete("Id")]
        public Task<bool> DeleteAsync(Guid id)
        {
            return _productAppService.DeleteAsync(id);
        }

        //PAGINACION
        [HttpGet("/lista")]
        public async Task<ICollection<ProductDto>> GetListaAsync(int limit=10)
        {
            return await _productAppService.GetListaAsync(limit);
        }

        [HttpGet("/Paginacion")]
        
        public async Task<ResultadoPaginacion<ProductDto>> GetListaAsync(string? search = "", int offset = 0, int limite = 10, string sort = "Name", string order = "asc")
        {
            return await _productAppService.GetListaAsync(search, offset, limite, sort, order);
        }
    }  
}

