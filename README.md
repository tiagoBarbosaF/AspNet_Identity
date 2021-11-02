# ASP.NET Identity - Gerenciando contas de usuários

## Objetivo do Projeto

- Parte 01:
  - Entenda a arquitetura do AspNet Identity
  - Valide senhas seguras
  - Faça o gerenciamento de contas de usuário
  - Envie emails de confirmação de conta
  - Use o Owin para desacoplar sua aplicação do servidor

### Começando com AspNet Identity
- Propósito do AspNet Identity
- Instalando os pacotes necessários
- Pacote obrigatório
- Criando a pagina de registro
- Consolidando seu conhecimento
- O que aprendi?
  - **O que é o AspNet Identity**:
    - Vimos que o AspNet Identity é um framework para o gerenciamento de identidades de usuários e tarefas como verificar senhas, nomes de usuários, e-mails, etc ficam muito mais simples!.
  - **Instalamos o AspNet Identity em um projeto criado do zero:**
    - Vimos que para instalar o AspNet Identity em um novo projeto basta usar o comando `Install-Package` `Microsoft.AspNet.Identity.Core`.

### Arquitetura do AspNet Identity
- IdentityDbContext e IdentityUser
- UserStore e UserManager
- Do UserManager até o banco
- Arquitetura do Identity e assincronicidade
- Arquitetura do framework
- Consolidando seu conhecimento
- O que aprendi?
  - **IdentityDbContext e IdentityUser:**
    - Conhecemos a classe IdentityUser já implementada no AspNet Identity e ela já possui muitas propriedades implementadas! Além disso, usamos o IdentityDbContext para criar as tabelas necessárias para o AspNet Identity de forma muito simples.
  - **UserStore e UserManager:**
    - Vimos a importância da interface IUserStore para a comunicação do UserManager com o banco de dados! Arquitetura do Identity .
  - **Arquitetura do Identity:**
    - Vimos 3 características fundamentais sobre a arquitetura do AspNet Identity: Baseado em interfaces, implementação genérica e assincronicidade!

### Owin
- Instalando o Owin
- Instalação de pacote
- Configurando o Owin
- Sobre o CreatePerOwinContext
- Consolidando seu conhecimento
- O que aprendi?
  - **O que é o Owin:**
    - Vimos o papel do Owin na comunicação entre aplicação e servidor. Além disso, aprendemos como o Owin é muito mais leve em comparação a biblioteca `System.Web`.
  - **Estender o Owin:**
    - Aprendemos sobre a modularização do Owin e instalamos o pacote de extensão do Owin `Microsoft.Owin.Host.SystemWeb`.
  - **Como configurar o Owin:**
    - Para configurar o Owin, criamos o arquivo `Startup.cs` e o atributo de assembly `OwinStartup` usando a classe de inicialização com o método `Configuration`.
  - **Como usar o contexto Owin:**
    - Vimos que é possível usar o padrão de alocação de serviço com o Owin e recuperar instâncias à partir do método `Get` e `GetUserManager`.

### Validação de usuário
- O IdentityResult
- Emails duplicados
- Criando senhas mais seguras
- Validação no AspNet Identity
- Usuários com email único
- Consolidando seu conhecimento
- O que aprendi?
  - **IdentityResult:**
    - Vimos que podemos observar o resultado de uma operação à partir do objeto `IdentityResult` com as propriedades `Succeeded` e `Errors`.
  - **A interface IIdentityValidator:**
    - Para criar nosso validador de senhas, implementamos.
  - **UserValidator:**
    - Para validar emails únicos, poderíamos ter criado uma classe que implemente `IIdentityValidator<UsuarioAplicacao>`, mas, usamos a classe `UserValidator<TUser>` com esta função.

### Serviços de email
- Servico de email
- Serviço de envio de email
- Implementando e integrando nosso servico de email
- Usando o GenerateEmailConfirmationTokenAsync
- Confirmando o email do usuario
- Confirmação de email
- Projeto final do curso
- Consolidando seu conhecimento
- Conclusão
- O que aprendi?
  - **Método UserManager.GenerateEmailConfirmationTokenAsync:**
  - Para a confirmação de email de nosso usuário, foi necessário criar um token de confirmação e conseguimos à partir do método `UserManager.GenerateEmailConfirmationTokenAsync`.
  - **Método UserManager.ConfirmEmailAsync:**
  - Como token de confirmação criado, foi preciso confirmar a ligação entre o token e o usuário. Para tanto, fizemos uso do método `UserManager.ConfirmEmailAsync`.
  - **A interface IIdentityMessageService:**
  - A integração de um serviço de envio de mensagens com o AspNet Identity foi fácil quando implementamos a interface `IIdentityMessageService` - importante não apenas para o envio de email, mas também para mensagens enviadas por outros meios, como vimos na documentação.
