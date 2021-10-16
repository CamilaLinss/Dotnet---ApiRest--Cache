namespace _3.Dominio.Entidades.Validations
{
    public class Validationfailure
    {

        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }


        public Validationfailure(string propertyName, string errorMessage){

            PropertyName = propertyName;
            ErrorMessage = errorMessage;

        }
        
    }
}