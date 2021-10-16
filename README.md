# Dotnet---APIREST

# Cache - Redis

1- A cada get vai ser guardado em cache o resultado

    - Validar se ja existe atualização em cache, caso não exista criar

2- A cada modificação (POST, PUT, DELETE), o cache anterior deve ser apagado (Será recriado versão 
atualizada quando for solicitado o get novamente) 



Features:

Validação - FluentValidation

Data Transfer Object - AutoMapper

Logs - Serilog

ORM - Entity Framework (Mysql)
