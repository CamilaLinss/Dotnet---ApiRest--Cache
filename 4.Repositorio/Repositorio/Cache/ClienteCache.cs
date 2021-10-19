using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using _3.Dominio.Entidades.Abstract;
using _3.Dominio.Services.Interface;
using Aplicacao.DTO.Output;
using AutoMapper;
using Dominio.Entidades;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using ServiceStack.Redis;

namespace _4.Repositorio.Repositorio.Cache
{
    public class ClienteCache : IClienteCache
    {

        
        //DICAS
        //*Voce pode colocar como CHAVE para guardar consultas de lista, a query SQL que trouxe aquele resultado
        //*Tb existe no redis, parametros para expirar os dados de acordo com uma DATA


        private RedisClient redisClient;
        private readonly IMapper _mapper;
        private string serverHost;
        private string chaveGetAll;
        

        public ClienteCache(IConfiguration config, IMapper mapper)
        {
            _mapper = mapper;

            serverHost = config.GetSection("conexaoRedis:server").Value;
            redisClient = new RedisClient(serverHost);

            chaveGetAll = "GetAllClientesParametros";
        }



        public void GravaCacheGet(IEnumerable<Cliente> clientes){

        var DTOClientes = _mapper.Map<IEnumerable<DTOOutputCliente>>(clientes);

        redisClient.Set<IEnumerable<DTOOutputCliente>>(chaveGetAll, DTOClientes, new TimeSpan(0,10,0));

        }


        public void GravaCacheGet(Cliente cliente){

        var chave = "cliente" + cliente.id.ToString();

        var DTOCliente = _mapper.Map<DTOOutputCliente>(cliente);

        redisClient.Set<DTOOutputCliente>(chave, DTOCliente, new TimeSpan(0,10,0));

        }


        public BaseResponse BuscaCacheGet(){

        BaseResponse baseResponse = new BaseResponse();

        var DTOclientes = redisClient.Get<IEnumerable<DTOOutputCliente>>(chaveGetAll);

        if(DTOclientes == null){

            baseResponse.ValidationResult.Errors.Add(new ValidationFailure("Redis","Sem cache"));

            return baseResponse;
        }

        var clientes = _mapper.Map<IEnumerable<Cliente>>(DTOclientes);

        baseResponse.Entidade = clientes;

        return baseResponse;

        }


       public BaseResponse BuscaCacheGet(int id){

        var chave = "cliente" + id.ToString();

        BaseResponse baseResponse = new BaseResponse();

        var DTOcliente = redisClient.Get<DTOOutputCliente>(chave);

        if(DTOcliente == null){

            baseResponse.ValidationResult.Errors.Add(new ValidationFailure("Redis","Sem cache"));

            return baseResponse;
        }

        var cliente = _mapper.Map<Cliente>(DTOcliente);

        baseResponse.Entidade = cliente;

        return baseResponse;

        }


        public void deleteCacheAll(){

            //Alterações sempre baseada em um id
            redisClient.Remove(chaveGetAll);

        }

        public void deleteCacheId(int id){

            //É preciso fazer um tratamento anterior para verificar se o id existe no cache

            var chave = "cliente" + id.ToString();

            //Alterações sempre baseada em um id
            redisClient.Remove(chave);

        }


        
    }
}