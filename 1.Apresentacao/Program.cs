using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace _1.Apresentacao
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args){

            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog((builderContext, config ) =>
                {
                   config.MinimumLevel.Debug()
                         .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                         .MinimumLevel.Override("System", LogEventLevel.Error)
                         .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                         .Enrich.FromLogContext()
                         .Enrich.WithProperty("System", "API - Cadastro de Clientes ")
                         .WriteTo
                         .Console(
                                  outputTemplate: "{{" +
                                                  " System: {System}," +
                                                  " Timestamp:{Timestamp:HH:mm:ss}," +
                                                  " TraceId: {TraceId}," +
                                                  " SpanId: {SpanId}," +
                                                  " RequestId: {RequestId}," +
                                                  " CorrelationId: {CorrelationId}," +
                                                  " Level: {Level}," +
                                                  " Context: {SourceContext}," +
                                                  " Exception: {Exception}," +
                                                  " Message: {Message:lj}," +
                                                  " Enviroment: {Enviroment}," +
                                                  "" +
                                                  "}," +
                                                  "{NewLine}"
                                 );

                AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();

                });
        }
    }
}
