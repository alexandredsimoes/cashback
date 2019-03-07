# Teste BeBlue Cashback

## Dependência
SQL Server

Para rodar InMemory e remover a dependência do SQL Server, com o projeto aberto vá em Services -> Cashback.API -> Startup.cs e
na linha 38 comente o trecho

```
options.UseSqlServer("Server=localhost;Database=CashbackTests;Trusted_Connection=True;");
```

e descomente o trecho

```
options.UseInMemoryDatabase("cashback");
```

## Build
É só abrir o Visual Studio e executar.

