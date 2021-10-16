using FluentValidation.Results;

namespace _3.Dominio.Entidades.Abstract
{
    public class BaseResponse
    {

        public BaseResponse()
        {
            ValidationResult = new ValidationResult();
        }
        public BaseResponse(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }

        public ValidationResult ValidationResult { get;set;}

        public dynamic Entidade { get; set; }


    }
}