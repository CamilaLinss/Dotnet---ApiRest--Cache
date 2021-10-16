using _3.Dominio.Entidades.Validations.Services;
using _3.Dominio.Services.Interface;
using _4.Repositorio.Repositorio.Cache;
using Aplicacao.Profiles;
using AutoMapper;
using Dominio.RepoInterfaces;
using Dominio.Services.Interface;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Repositorio.Data;
using Repositorio.Repositorio;

namespace _1.Apresentacao
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {

            //appsettings.json - Ambiente
            var builder = new ConfigurationBuilder() 
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", false, true)
                .AddEnvironmentVariables();


            Configuration = builder.Build();


            //Serilog - Lendo configurações do appsetting.json
            //Log.Logger = new LoggerConfiguration()
               //.ReadFrom.Configuration(Configuration).CreateLogger();

        }


        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            //ENTITY FRAMEWORK MYSQL.DATA CONTEXT
            services.AddDbContext<DataContext>(opt => opt.UseMySQL(Configuration.
                    GetConnectionString("connection")));

            //DEPENDENCIAS
            services.AddScoped<DataContext>();

            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();

            services.AddScoped<IClienteCache, ClienteCache>();


            //AUTOMAPPER
             var config = new AutoMapper.MapperConfiguration(opt =>
            {
                opt.AddProfile(new ClienteProfile());
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);


            //FluentValidation
            services.AddControllers() 
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());
           
            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    
                    Title = "_1.Apresentacao", Version = "v1.0",
                    Description = "API responsavel por cadastrar clientes",
                    Contact = new OpenApiContact
                    {
                        Name = "Camila",
                        Email = "7070@gmail.com.br"

                    }
                    
                    });

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });



        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //app.UseSerilogRequestLogging();   Diminuir verbosidade das logs no serilog

            if (env.EnvironmentName.Equals("dev"))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "_1.Apresentacao v1"));
            }

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }



    }
}
