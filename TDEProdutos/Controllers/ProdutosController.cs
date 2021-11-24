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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace TDEProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProdutosController : Controller
    {
        private readonly ProdutoContext Context;
        public static IWebHostEnvironment _environment;

        public ProdutosController(IWebHostEnvironment environment)
        {
            Context = new ProdutoContext();
            _environment = environment;

        }


        [HttpGet]
        public ActionResult ola()
        {
            return Ok("Ola");
        }

        /// <summary>
        /// Consulta dados de uma pessoa a partir do CPF
        /// Requer uso de token.
        /// </summary>
        /// <param name="Id">CPF</param>
        /// <returns>Objeto contendo os dados de uma pessoa.</returns>
        [Authorize]

        [HttpGet("BuscarPorId/{Id}")]

        public ActionResult BuscarPorId(string Id)
        {

            return Ok(Context._produtos.Find<Produto>(p => p.Id == Id).FirstOrDefault());
        }

        [HttpPost("upload/{Id}")]
        public async Task<ActionResult> EnviaArquivo(string Id,[FromForm] IFormFile arquivo)
        {
            if (arquivo.Length > 0)
            {
                if (arquivo.ContentType != "image/jpeg" &&
                    arquivo.ContentType != "image/jpg" &&
                    arquivo.ContentType != "image/png"
                   )
                {
                    return BadRequest("Formato Inválido de imagens");
                }

                try
                {

                    string contentRootPath = _environment.ContentRootPath;
                    string path = "";
                    path = Path.Combine(contentRootPath, "imagens");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream filestream = System.IO.File.Create(path + "//"+ arquivo.FileName))
                    {
                        await arquivo.CopyToAsync(filestream);
                        filestream.Flush();
                        var pResultado = Context._produtos.Find<Produto>(p => p.Id == Id).FirstOrDefault();
                        if (pResultado == null) return
                        NotFound("Id não encontrado, atualizacao não realizada!");

                        pResultado.Imagem = arquivo.FileName;
                        Context._produtos.ReplaceOne<Produto>(p => p.Id == Id, pResultado);
                        return Ok("Imagem enviada com sucesso " + arquivo.FileName);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }
            else
            {
                return BadRequest("Ocorreu uma falha no envio do arquivo...");
            }
        }

        [HttpPost("Adicionar")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public ActionResult Adicionar(Produto Produto)
        {
            ProdutoValidation produtoValidation = new ProdutoValidation();

            var validacao = produtoValidation.Validate(Produto);

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
            Context._produtos.InsertOne(Produto);
            return Ok("Produto cadastrado");
            
        }



        [HttpPut("Atualizar/{Id}")]
        public ActionResult Atualizar(string Id, [FromBody] Produto Produto)
        {
            var pResultado = Context._produtos.Find<Produto>(p => p.Id == Id).FirstOrDefault();
            if (pResultado == null) return
            NotFound("Id não encontrado, atualizacao não realizada!");

            Produto.Id = Id;
            Context._produtos.ReplaceOne<Produto>(p => p.Id == Id, Produto);

            return Ok("Produto atualizado com sucesso");


        }

        [HttpDelete("Remover/{Id}")]
        public ActionResult Remover(string Id)
        {
            var pResultado = Context._produtos.Find<Produto>(p => p.Id == Id).FirstOrDefault();
            if (pResultado == null) return
                    NotFound("Id não encontrada, atualizacao não realizada!");

            Context._produtos.DeleteOne<Produto>(filter => filter.Id == Id);
            return Ok("Produto removido com sucesso");
        }




    }


}

