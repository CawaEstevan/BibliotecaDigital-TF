

## Como Executar

### Clonar o Repositório

git clone https://github.com/SEU_USUARIO/BibliotecaDigital-TF.git
cd BibliotecaDigital-TF


### Restaurar Pacotes

dotnet restore


### Aplicar Migrations

dotnet ef database update --project BibliotecaDigital.Infrastructure --startup-project BibliotecaDigital.Web


### Executar o Projeto

dotnet run --project BibliotecaDigital.Web


