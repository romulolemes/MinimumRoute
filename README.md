# MinimumRoute 

Esse projeto possui o intuito de solucionar o problema de caminho e tempo de entrega das encomendas postadas no estado San Andreas.

San Andreas possui as seguintes cidades e condados:
- Los Santos (LS)
- San Fierro (SF)
- Las Venturas (LV)
- Red County (RC)
- Whetstone (WS)
- Bone County (BC)

Os trechos disponíveis entre as cidades e condados são atualizados mensalmente, sendo definidos através de um arquivo que além do trecho, informa o tempo em dias que cada
encomenda leva ao passar por ele. Cada trecho é unidirecional, podendo haver diferenças entre o trecho de ida e volta, ou mesmo estar indisponível.

## Pré-requisito

- SDK do .NET Core.
Link para instação https://docs.microsoft.com/pt-br/dotnet/core/install/sdk

Para a correta execução do projeto é necessário os arquivos *'trechos.txt'* e *'encomendas.txt'* localizado na pasta raiz da aplicação.

Exemplo arquivo *trechos.txt*
```
LS SF 1
SF LS 2
LS LV 1
LV LS 1
SF LV 2
LV SF 2
LS RC 1
RC LS 2
SF WS 1
WS SF 2
LV BC 1
BC LV 1
```
Exemplo arquivo *encomendas.txt*
```
SF WS
LS BC
WS BC
```

Tais arquivos já existem no corrente projeto e precisam serem setados para o path raiz da aplicação seguindo o procedimento da seção Build(tentar linkar)

## Build
Na pasta raiz do projeto é necessário rodar no PowerShell o seguinte comando:

```
dotnet build '.\MinimumRoute\MinimumRoute.csproj' --output '.\Build\'
``` 
 
## Execução
Ainda na pasta raiz executar o comando abaixo para executar o projeto

```
.\Build\MinimumRoute.exe
```
A aplicação irá gerar um arquivo de saída *'rotas.txt'* contendo a solução para do problema do arquivo *'encomendas.txt'*

## Testes
Executando teste unitários

```
 dotnet test .\MinimumRoute.UnitTests\MinimumRoute.UnitTests.csproj
```

## Autor
Rômulo Rocha Lemes
