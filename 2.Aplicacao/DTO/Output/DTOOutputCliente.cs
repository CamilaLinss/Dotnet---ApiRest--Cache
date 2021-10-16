using System;
using System.ComponentModel.DataAnnotations;

namespace Aplicacao.DTO.Output
{
    public class DTOOutputCliente
    {


        [Key]
        public int id {get;set;}

        public string nome {get;set;}

        public int CPF {get;set;}

        public string email {get;set;}

        public DateTime horaConsulta {get;set;}
        
        
    }
}