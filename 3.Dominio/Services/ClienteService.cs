using System.Collections.Generic;
using System.Linq;
using _3.Dominio.Services.Interface;
using Dominio.Entidades;
using Dominio.RepoInterfaces;
using Dominio.Services.Interface;
using FluentValidation.Results;

namespace _3.Dominio.Entidades.Validations.Services
{
    public class ClienteService:IClienteService
    {

        private readonly IClienteRepositorio _repo;
        private readonly IClienteCache _cache;


        public ClienteService( IClienteRepositorio repo, IClienteCache cache)
        {
            _repo = repo;
            _cache = cache;
        }


        public ValidationResult cadastra(Cliente cliente){

            cliente.validationResult = cliente.Validate(cliente);

            if(!cliente.validationResult.IsValid){return cliente.validationResult;}


            //Regras de negocio - Nenhum cliente pode ter e-mail igual
            IEnumerable<Cliente> emails = _repo.busca().Where(e => e.email == cliente.email);

            if(emails.Any()){
                cliente.AddError("Cliente", "Esse e-mail já está cadastrado. Recupere a sua senha");
                return cliente.validationResult;
            }

            _repo.cadastra(cliente);


            //apaga cache do GetAll cache
            _cache.deleteCacheAll();


            return cliente.validationResult;

        }


        public IEnumerable<Cliente> busca(){

            IEnumerable<Cliente> result;

            var cacheResult = _cache.BuscaCacheGet();

            if(cacheResult.ValidationResult.IsValid){

            return cacheResult.Entidade;

            }

            //logger.Information(cacheResult.ValidationResult.Errors[0].ErrorMessage;);

            //busca no banco padrão
            result = _repo.busca();

            //grava novo retorno em cache
            _cache.GravaCacheGet(result);

            return result;
          
        }


        public Cliente buscaId(int id){
 
            Cliente result;

            var cacheResult = _cache.BuscaCacheGet(id);

            if(cacheResult.ValidationResult.IsValid){

            return cacheResult.Entidade;

            }

            //logger.Information(cacheResult.ValidationResult.Errors[0].ErrorMessage;);
 
            result = _repo.buscaId(id);

            //grava novo retorno em cache
            _cache.GravaCacheGet(result);

            return result;
        }


        public bool atualiza (int id, Cliente clienteUpdate){

            _repo.atualiza(id, clienteUpdate);

            //apaga cache do GetAll cache
            _cache.deleteCacheAll();

            //apaga cache do GetId cache
            _cache.deleteCacheId(id);


            return true;

        }


        public bool delete(int id){

            var clienteexiste = _repo.buscaId(id);

            if(clienteexiste == null){

                return false;

            }

            _repo.deletar(id);

            //apaga cache do GetAll cache
            _cache.deleteCacheAll();

            //apaga cache do GetId cache
            _cache.deleteCacheId(id);

            return true;

        }

        
    }
}