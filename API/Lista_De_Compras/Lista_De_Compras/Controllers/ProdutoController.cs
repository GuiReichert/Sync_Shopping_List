using Microsoft.AspNetCore.Mvc;
using Lista_De_Compras.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Lista_De_Compras.Services;

namespace Lista_De_Compras.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService) 
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> GetAllProdutos()
        {

            try
            {
                var produtos = await _produtoService.GetAllProdutos();
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            try
            {
                var produto_adicionado = await _produtoService.AddProdutos(produto);
                return Ok(produto_adicionado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Produto>> PutProduto(Produto produto)
        {
            try
            {
                var produto_alterado = await _produtoService.UpdateProdutos(produto);
                return Ok(produto_alterado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduto(int id)
        {
            try
            {
                await _produtoService.DeleteProdutos(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }
}
