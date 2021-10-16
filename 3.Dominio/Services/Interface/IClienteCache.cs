using System.Collections.Generic;
using _3.Dominio.Entidades.Abstract;
using Dominio.Entidades;

namespace _3.Dominio.Services.Interface
{
    public interface IClienteCache
    {
        void GravaCacheGet(IEnumerable<Cliente> clientes);
        BaseResponse BuscaCacheGet();
        void GravaCacheGet(Cliente clientes);
        BaseResponse BuscaCacheGet(int id);
        void deleteCacheAll();
        void deleteCacheId(int id);



    }
}