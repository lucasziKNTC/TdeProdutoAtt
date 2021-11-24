using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDEProdutos.Context;
using TDEProdutos.models;
using TDEProdutos.Validations;

namespace TDEProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaContext Context;
        public CategoriaController()
        {
            Context = new CategoriaContext();
        }


        [HttpGet]
        public ActionResult Ola()
        {
            return Ok("olaaaaaaa");
        }




        [HttpGet("BuscarCategoria/{Id}")]
        public ActionResult BuscarCategoria(string id)
        {
            return Ok(Context._categorias.Find<Categoria>(p => p.Id == id).FirstOrDefault());

        }



        [HttpPost("AdicionarCategoria")]

        public ActionResult AdicionarCategoria(Categoria categoria)

        {
            CategoriaValidation produtoValidation = new CategoriaValidation();
            var validacao = produtoValidation.Validate(categoria);

            if (!validacao.IsValid)
            {
                List<string> erros = new List<string>();
                foreach (var failure in validacao.Errors)
                {
                    erros.Add("Property " + failure.PropertyName +
                        " failed validation. Error Was: "
                        + failure.ErrorMessage);
                }

                return BadRequest(erros);
            }




            Context._categorias.InsertOne(categoria);
            return Ok("Categoria cadastrada");


        }





        [HttpPut("Atualizar/{Id}")]
        public ActionResult Atualizar(string id, [FromBody] Categoria categoria)
        {
            var pResultado = Context._categorias.Find<Categoria>(p => p.Id == id).FirstOrDefault();
            if (pResultado == null) return
            NotFound("Id não encontrado, atualizacao não realizada!");

            categoria.Id = id;
            Context._categorias.ReplaceOne<Categoria>(p => p.Id == id, categoria);

            return Ok("Categoria atualizada com sucesso");



        }



        [HttpPut("Desativar/{Id}")]
        public ActionResult Desativar(string id)
        {
            var ProdutoDesativado = Context._categorias.Find<Categoria>(P => P.Id == id).FirstOrDefault();
            if (ProdutoDesativado == null)
                return NotFound("Categoria não pode ser desativado, pois id não existe");


            if (ProdutoDesativado != null &&
                ProdutoDesativado.Ativo == false)
                return BadRequest("Categoria já está desativado, operação não realizada");



            ProdutoDesativado.Ativo = false;

            return Ok("Categoria desativada");


        }



        [HttpDelete("Deletar/{Id}")]
        public ActionResult Deletar(string id)

        {
            var pResultado = Context._categorias.Find<Categoria>(p => p.Id == id).FirstOrDefault();
            if (pResultado == null) return
                    NotFound("Id não encontrada, atualizacao não realizada!");

            Context._categorias.DeleteOne<Categoria>(filter => filter.Id == id);
            return Ok("Categoria removida com sucesso");


        }



    }
}

