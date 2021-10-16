using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using _3.Dominio.Entidades.Validations;
using FluentValidation;
using FluentValidation.Results;

namespace _3.Dominio.Entidades.Abstract
{

    
    public abstract class Entity<T> : AbstractValidator<T>
    {

        public ValidationResult validationResult { get; set; }
  
        protected Entity()
        {
            validationResult = new ValidationResult();

        }

        public void AddError(string propertyName, string errorMessage){

            validationResult.Errors.Add(new ValidationFailure(propertyName, errorMessage));
        }

        public void AddError(string errorMessage){

            validationResult.Errors.Add(new ValidationFailure("", errorMessage));

        }


        
    }
}