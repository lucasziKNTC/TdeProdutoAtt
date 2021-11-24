using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDEProdutos.models
{
    public class Categoria
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string NomeCategoria { get; set; }
        public int IdCategoria { get; set; }
        public bool Ativo { get; set; }

    }
}
