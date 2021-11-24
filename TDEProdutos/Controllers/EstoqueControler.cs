using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDEProdutos.Context;
using TDEProdutos.models;
using TDEProdutos.Models;

namespace TDEProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueController : Controller
    {

        private readonly ProdutoContext Context;

        public EstoqueController()
        {
            Context = new ProdutoContext();

        }
        [HttpPost("DebitarEstoque/{Id}")]
        public ActionResult DebitarEstoque(string id, [FromBody] DebitoProduto produtoEstoque)
        {
            var resultado = Context._produtos.Find<Produto>(p => p.Id == id).FirstOrDefault();

            if (resultado == null)
            {
                return NotFound("O produto não existe na base de dados");
            }

            if (produtoEstoque.Qtde > resultado.EstoqueAtual)
            {
                return BadRequest("O produto não tem estoque suficiente");
            }

            resultado.EstoqueAtual = resultado.EstoqueAtual - produtoEstoque.Qtde;

            Context._produtos.ReplaceOne<Produto>(p => p.Id == id, resultado);

            return Ok("Produto debitado do estoque com sucesso!");



        }
    }

}
