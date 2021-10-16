using System.Collections.Generic;
using _3.Dominio.Entidades.Validations;
using Dominio.Entidades;
using FluentValidation.Results;

namespace Dominio.Services.Interface
{
    public interface IClienteService
    {
         
            ValidationResult cadastra(Cliente cliente);

            IEnumerable<Cliente> busca();

            Cliente buscaId(int id);

            bool atualiza (int id, Cliente clienteUpdate);

            bool delete(int id);

    }
}