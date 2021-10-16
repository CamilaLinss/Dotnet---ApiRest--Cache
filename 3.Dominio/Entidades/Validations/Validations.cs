using System.Collections.Generic;
using FluentValidation.Results;

namespace _3.Dominio.Entidades.Validations
{
    public class Validations
    {

        public List<Validationfailure> AddFalhas(ValidationResult result){

            List<Validationfailure> falhas = new List<Validationfailure>();

            foreach(var failure in result.Errors){

            var falha = new Validationfailure(failure.PropertyName, failure.ErrorMessage);

            falhas.Add(falha);

            }

            return falhas;
        }

        
    }
}