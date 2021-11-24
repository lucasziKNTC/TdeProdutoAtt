using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TDEProdutos.models;

namespace TDEProdutos.Validations
{
    public class ProdutoValidation: AbstractValidator<Produto>

    {

        public ProdutoValidation()
        {
        

            RuleFor(Produto => Produto.NomeProduto)
                .NotEmpty().WithMessage("campo nome vazio")
                .NotNull().WithMessage("Campo nome obrigatorio ")
                .MaximumLength(250).WithMessage("tamanho maximo excedido!")
                .Must(SomenteLetras).WithMessage("Somente Letras");

            RuleFor(Produto => Produto.PrecoVendas)
                .NotEmpty().WithMessage("campo preço vazio")
                .NotNull().WithMessage("campo preço não informado,Favor inserir !")
                .GreaterThan(0).WithMessage("preço deve ser maior que zero");
           

            RuleFor(Produto => Produto.AtivoInativo)
                .NotEmpty().WithMessage("campo produto ativo ou inativo vazio!")
                .NotNull().WithMessage("campo produto ativo ou inativo não informado,Favor inserir");

            RuleFor(Produto => Produto.DataCadastro)
                .NotEmpty().WithMessage("campo data vazio")
                .NotNull().WithMessage("campo data não informado");

            RuleFor(Produto => Produto.DescricaoProduto)
                .NotEmpty().WithMessage("campo descrição vazio")
                .MaximumLength(500).WithMessage("tamanho maximo excedido!")
                .NotNull().WithMessage("campo descrição obrigatorio");

               

        }

        internal object Validate(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public static bool SomenteNumero(string Numeros)
        {
            //logica da receita
            return  Regex.IsMatch(Numeros, @"^\d$");

        }

        public static bool SomenteLetras(string palavra)
        {
            return Regex.IsMatch(palavra, @"^[a-zA-Z]+$");
        }




    }
}
