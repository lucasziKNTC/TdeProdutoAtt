using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDEProdutos.models;


namespace TDEProdutos.Validations
{
    public class CategoriaValidation : AbstractValidator<Categoria>

    {
        public CategoriaValidation()
        {
            RuleFor(Categoria => Categoria.IdCategoria)
              .NotEmpty().WithMessage("Campo codigo vazio,tente novamente ")
              .NotNull().WithMessage("Campo codigo obrigatorio, tente novamente !");


            RuleFor(Categoria => Categoria.NomeCategoria)
                .NotEmpty().WithMessage("Campo nome vazio,tente novamente ")
                .NotEmpty().WithMessage("Campo nome não informado ,tente novamente ")
                .NotEmpty().WithMessage("Tamanho minimo não atingido ")
                .NotEmpty().WithMessage("Tamanho maximo exedido de 250 caracteres ");


        }

    }
}