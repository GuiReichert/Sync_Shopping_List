using Lista_De_Compras.Models;
using Lista_De_Compras.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lista_De_Compras.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController: ControllerBase
    {
        private IProdutoService _produtoService;

        public CategoriaController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> GetAllCategorias()
        {

            try
            {
                var categorias= await _produtoService.GetAllCategorias();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> PostProduto(Categoria categoria)
        {
            try
            {
                var categoria_adicionada = await _produtoService.AddCategoria(categoria);
                return Ok(categoria_adicionada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Categoria>> PutCategoria(Categoria categoria)
        {
            try
            {
                var categoria_alterada = await _produtoService.UpdateCategoria(categoria);
                return Ok(categoria_alterada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoria(int id)
        {
            try
            {
                await _produtoService.DeleteCategoria(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
