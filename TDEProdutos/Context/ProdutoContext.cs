using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDEProdutos.models;

namespace TDEProdutos.Context
{
    public class ProdutoContext
    {
        public MongoDatabase Database;
        public String DataBaseName = "aula";
        string conexaoMongoDB = "mongodb+srv://Lucas:123@cluster0.ajsl8.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";

        public IMongoCollection<Produto> _produtos;
        internal object _usuarios;

        public ProdutoContext()
        {
            var cliente = new MongoClient(conexaoMongoDB);
            var server = cliente.GetDatabase(DataBaseName);
            _produtos = server.GetCollection<Produto>("Produtos");
 

        }
    }
        public class CategoriaContext
        {

            public MongoDatabase Database;
            public String DataBaseName = "aula";
            string conexaoMongoDB = "mongodb+srv://Lucas:123@cluster0.ajsl8.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";


            public IMongoCollection<Categoria> _categorias;
            public CategoriaContext()
            {
                var cliente = new MongoClient(conexaoMongoDB);
                var server = cliente.GetDatabase(DataBaseName);
                _categorias = server.GetCollection<Categoria>("categorias");
            }

           
        }
    public class UsuarioContext
    {

        public MongoDatabase Database;
        public String DataBaseName = "aula";
        string conexaoMongoDB = "mongodb+srv://Lucas:123@cluster0.ajsl8.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";


        public IMongoCollection<Usuario> _usuarios;
        public UsuarioContext()
        {
            var cliente = new MongoClient(conexaoMongoDB);
            var server = cliente.GetDatabase(DataBaseName);
            _usuarios = server.GetCollection<Usuario>("Usuarios");
        }


    }

}

