using System.Collections.Generic;
using _3.Dominio.Entidades.Validations;
using Aplicacao.DTO;
using Aplicacao.DTO.Output;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Apresentacao.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class ClienteController:ControllerBase
    {

      private readonly IClienteService _service;
      private readonly ILogger _logger;
     
      private readonly IMapper _mapper;

      Validations validacao = new Validations();


      public ClienteController(IClienteService service, IMapper mapper, ILogger logger)
      {
          _service = service;
          _mapper =mapper;
          _logger = logger;
      }



        /// <summary>
        ///     Cria o registro dos clientes
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Exemplo de cliente valido para cadastrar.
        /// 
        ///    {
        ///      "nome": "Leandra",
        ///      "cpf": 124234,
        ///      "email": "Lele@gmail.com"
        ///    }
        ///     
        /// </remarks>
        /// <response code="204">Se a requisição for bem sucedida</response>
        /// <response code="400">Se a requisição estiver no formato incorreto</response>
        /// <response code="500">Se ocorrer um erro inesperado</response>

        [HttpPost("cadastra")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult cadastra([FromBody] DTOInputCliente clienteDTO){

           _logger.Information("Iniciando cadastro Cliente");

            Cliente cliente = _mapper.Map<Cliente>(clienteDTO);

             var resultado = _service.cadastra(cliente);


            //FluentValidation
             if(!resultado.IsValid){

                var falhas = validacao.AddFalhas(resultado);

                return BadRequest(falhas);

             }else{

                return NoContent();

             }
        }


              
        [HttpGet("busca")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<DTOOutputCliente>> busca(){

            var clientes = _service.busca();

            List<DTOOutputCliente> clienteDTO2 = _mapper.Map<List<DTOOutputCliente>>(clientes);


            return Ok(clienteDTO2);

        }


        [HttpGet("buscaid/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DTOOutputCliente> buscaId(int id){

            var cliente = _service.buscaId(id);

             DTOOutputCliente clienteDTO = _mapper.Map<DTOOutputCliente>(cliente);

            return clienteDTO;

        }

        [HttpPut("atualiza/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult atualiza([FromBody] DTOInputCliente clienteDTO, int id){

            Cliente cliente = _mapper.Map<Cliente>(clienteDTO);

            _service.atualiza(id ,cliente);

            return NoContent();

        }
    


        
    }
}