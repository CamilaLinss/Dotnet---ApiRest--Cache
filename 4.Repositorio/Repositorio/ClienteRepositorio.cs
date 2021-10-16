using System.Collections.Generic;
using System.Linq;
using _4.Repositorio.Repositorio.Cache;
using Dominio.Entidades;
using Dominio.RepoInterfaces;
using Repositorio.Data;

namespace Repositorio.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {

        private readonly DataContext _context;
        //private readonly IClienteCache _cache;

        public ClienteRepositorio(DataContext context)//, IClienteCache cache)
        {
            _context = context;
            //_cache = cache;
        }



        public void cadastra(Cliente cliente){

            _context.Clientes.Add(cliente);

            _context.SaveChanges();

        }


        public IEnumerable<Cliente> busca(){

           var result = _context.Clientes;

            return result;
        }


        public Cliente buscaId(int id){

            Cliente result = _context.Clientes.Find(id);

          // _cache.cacheSetCliente(result);

            return result;

        }

        public void atualiza(int id, Cliente clienteAtualizado){

            Cliente cliente = _context.Clientes.First(c => c.id == id);

            cliente.CPF = clienteAtualizado.CPF;
            cliente.email = clienteAtualizado.email;
            cliente.nome = clienteAtualizado.nome;

             _context.SaveChanges();

        }


          public void deletar(int id){

            Cliente cliente = buscaId(id);

            _context.Clientes.Remove(cliente);

            _context.SaveChanges();

        }

        
    }
}