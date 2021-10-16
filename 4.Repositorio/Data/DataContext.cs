using _3.Dominio.Entidades.Abstract;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> opt):base (opt)
        {

        }


        public DbSet<Cliente> Clientes {get;set;}


        protected override void OnModelCreating(ModelBuilder builder)
        {
    
            builder.Entity<Cliente>().ToTable("cliente");

            builder.Entity<Cliente>().Ignore(c => c.validationResult);
            builder.Entity<Cliente>().Ignore(c => c.CascadeMode);

        }

        
    }
}