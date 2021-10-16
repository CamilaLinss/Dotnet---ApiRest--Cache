using System.Collections.Generic;
using Aplicacao.DTO;
using Aplicacao.DTO.Output;
using AutoMapper;
using Dominio.Entidades;

namespace Aplicacao.Profiles
{
    //Auto Mapper

    public class ClienteProfile:Profile
    {

        public ClienteProfile()
        {

            CreateMap<DTOInputCliente, Cliente>();
            CreateMap<Cliente, DTOOutputCliente>();
            CreateMap<DTOOutputCliente, Cliente>();
            
        }
        
    }
}