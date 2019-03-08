# Teste BeBlue Cashback

## Dependência
SQL Server

Para rodar InMemory e remover a dependência do SQL Server, com o projeto aberto vá em Services -> Cashback.API -> Startup.cs e
na linha 38 comente o trecho

``` csharp
options.UseSqlServer("Server=localhost;Database=CashbackTests;Trusted_Connection=True;");
```

e descomente o trecho

``` csharp
options.UseInMemoryDatabase("cashback");
```



## Build
Colocar os dois projetos para executar ao mesmo tempo conforme a imagem abaixo:
(https://drive.google.com/open?id=13gJEqU39inaYOWHsh4cgBRxNNvCeavsg)

É só abrir o Visual Studio e executar.

Ao abrir a página inicial, clique em Inicializar Catálogo e aguarde a finalização

(https://drive.google.com/open?id=1Vx2WoiAh33aa2ceFNxrDf4wA3K1aYgzT)
