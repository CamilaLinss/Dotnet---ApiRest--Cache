using System.Collections.Generic;
using Dominio.Entidades;

namespace Dominio.RepoInterfaces
{
    public interface IClienteRepositorio
    {
         
            void cadastra(Cliente cliente);

            IEnumerable<Cliente> busca();

            Cliente buscaId(int id);

            void atualiza(int id, Cliente clienteAtualizado);

            void deletar(int id);

    }
}