
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using _3.Dominio.Entidades.Abstract;
using FluentValidation;
using FluentValidation.Results;

namespace Dominio.Entidades
{

    [Table("cliente")]
    public class Cliente : Entity<Cliente>
    {


        [Key]
        public int id {get;set;}

        public string nome {get;set;}

        public int CPF {get;set;}

        public string email {get;set;}


        public Cliente()
        {

             RuleFor(x => x.nome)
                .NotNull().WithMessage("O Nome é necessário")
                .NotEmpty().WithMessage("O campo Nome não pode ficar vazio")
                .MaximumLength(10).WithMessage("O campo Nome so aceita até 10 caracteres");

            RuleFor(x => x.email)
                .NotNull().WithMessage("O email é necessário")
                .NotEmpty().WithMessage("O campo email não pode ficar vazio")
                .EmailAddress().WithMessage("Formato de e-mail Inválido");
        }



    }
}