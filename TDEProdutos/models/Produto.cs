using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDEProdutos.models
{
    public class Produto
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Codigo { get; set; }

        public string NomeProduto { get; set; }

        public string DescricaoProduto { get; set; }

        public float PrecoVendas { get; set; }


        public float PrecoCusto { get; set; }


        public DateTime DataCadastro { get; set; }


        public int Estoque { get; set; }


        public string Imagem { get; set; }


        public int Alturacm { get; set; }

        public int Larguracm { get; set; }

        public int Profundidadecm { get; set; }

        public string categoriaProduto { get; set; }


        public bool AtivoInativo { get; set; }

        public int IdCategoria { get;  set; }


        public int EstoqueAtual { get; set; }


        public int EstoqueMinimo { get; set; }

       
        public bool Ativo { get; internal set; }



        public Categoria Categoria { get; set; }
    }
}
