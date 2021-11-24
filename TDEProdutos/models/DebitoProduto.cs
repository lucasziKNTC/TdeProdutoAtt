using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDEProdutos.Models
{
    public class DebitoProduto
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Codigo { get; set; }

        public int Qtde { get; set; }

        public string Id { get; set; }
    }
}

