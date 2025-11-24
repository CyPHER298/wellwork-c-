# 📌 WellworkGS — API de Produtividade, Organização e Saúde Mental

WellworkGS é uma **API REST em .NET 9** criada para apoiar trabalhadores com neurodivergência na organização do fluxo de
trabalho, monitoramento de metas, planejamento de tarefas, gerenciamento de lembretes e ativação de alertas de crise.

A API foi desenvolvida utilizando o padrão **Clean Architecture simplificada**, com camadas independentes (Controllers,
Services, Repositories, Domain, DTOs e Infra).  
O banco de dados utilizado é **Oracle**, com suporte total a migrations via Entity Framework Core.

---

# 🧩 1. Visão Geral

O sistema fornece funcionalidades como:

- **Cadastro e gerenciamento de usuários**
- **Cadastro de gestores**
- **Criação e acompanhamento de tarefas**
- **Controle de temporizador (pomodoro)**
- **Metas de produtividade**
- **Lembretes configuráveis**
- **Alertas de crise conectando usuário e gestor**
- **Operações de busca avançada com paginação, ordenação e filtros**
- **HATEOAS incluído nas respostas da rota /search**

A arquitetura foi pensada para:

✔ Manter baixo acoplamento  
✔ Facilitar testes e manutenção  
✔ Garantir integridade entre camadas  
✔ Organizar o fluxo de dados com DTOs  
✔ Controlar transações pelo Repository Pattern

---

# 🏛 2. Decisões Arquiteturais

## 🔹 **Por que .NET 9 Web API?**

- Alto desempenho
- Suporte nativo a Minimal Hosting Model
- APIs modernas e concisas

## 🔹 **Por que Oracle?**

- Suporte da instituição
- Estabilidade para grandes volumes
- Suporte a sequences e identity nativo

## 🔹 **Por que Repository + Service + DTO?**

- **Repository**: isola acesso ao banco
- **Service**: encapsula regras de negócio
- **DTOs**: controlam o que entra e sai da API
- **Mapping**: centraliza configurações do EF Core

## 🔹 **Por que Fluent API (Mappings)?**

- Evita poluir modelos com DataAnnotations
- Facilita trabalhar com banco Oracle (case-sensitive)

## 🔹 **Por que Swagger?**

- Documentação automática
- Testes rápidos sem Postman

---

# 🛠 3. Como Rodar o Projeto

## 🔧 Pré-requisitos

- .NET 9 SDK
- Oracle XE / Oracle 19c / Oracle 21c
- Oracle Data Access instalado
- DBeaver ou SQL Developer
- Visual Studio / Rider

---

## ▶️ 3.1 Clonar o Repositório

```bash
git clone https://github.com/seu-usuario/WellworkGS.git
cd WellworkGS
```

## ▶️ 3.2 Configurar a connection string

*No arquivo appsettings.json:*

```json
"ConnectionStrings": {
"Wellwork": "User Id=RM99742;Password=123456;Data Source=localhost:1521/XEPDB1"
}
```

## ▶️ 3.3 Aplicar migrations

```bash
dotnet ef database update
```

**Se não tiver AppDbContextFactory, crie com:**

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## ▶️ 3.4 Rodar o projeto

```bash
dotnet run
```

---

# 4 Principais Endpoints

| Método     | Endpoint                         | Descrição                                        |
|------------|----------------------------------|--------------------------------------------------|
| **GET**    | `/api/Usuario`                   | Lista todos os usuários                          |
| **GET**    | `/api/Usuario/{id}`              | Busca usuário por ID                             |
| **POST**   | `/api/Usuario`                   | Cria um novo usuário                             |
| **PUT**    | `/api/Usuario/{id}`              | Atualiza um usuário existente                    |
| **DELETE** | `/api/Usuario/{id}`              | Remove um usuário                                |
| **GET**    | `/api/Usuario/search`            | Busca avançada (filtros + ordenação + paginação) |
| **GET**    | `/api/Gestor`                    | Lista todos os gestores                          |
| **GET**    | `/api/Gestor/{id}`               | Busca gestor por ID                              |
| **POST**   | `/api/Gestor`                    | Cria novo gestor                                 |
| **PUT**    | `/api/Gestor/{id}`               | Atualiza gestor                                  |
| **DELETE** | `/api/Gestor/{id}`               | Remove gestor                                    |
| **GET**    | `/api/Tarefa`                    | Lista todas as tarefas                           |
| **GET**    | `/api/Tarefa/{id}`               | Busca tarefa por ID                              |
| **POST**   | `/api/Tarefa`                    | Cria nova tarefa                                 |
| **PUT**    | `/api/Tarefa/{id}`               | Atualiza tarefa                                  |
| **DELETE** | `/api/Tarefa/{id}`               | Remove tarefa                                    |
| **GET**    | `/api/Timer`                     | Lista todos os timers                            |
| **GET**    | `/api/Timer/{id}`                | Busca timer por ID                               |
| **POST**   | `/api/Timer`                     | Cria novo timer                                  |
| **PUT**    | `/api/Timer/{id}`                | Atualiza timer                                   |
| **DELETE** | `/api/Timer/{id}`                | Remove timer                                     |
| **GET**    | `/api/Meta`                      | Lista todas as metas                             |
| **GET**    | `/api/Meta/{id}`                 | Busca meta por ID                                |
| **POST**   | `/api/Meta`                      | Cria nova meta                                   |
| **PUT**    | `/api/Meta/{id}`                 | Atualiza meta                                    |
| **DELETE** | `/api/Meta/{id}`                 | Remove meta                                      |
| **GET**    | `/api/Lembrete`                  | Lista todos os lembretes                         |
| **GET**    | `/api/Lembrete/{id}`             | Busca lembrete por ID                            |
| **POST**   | `/api/Lembrete`                  | Cria novo lembrete                               |
| **PUT**    | `/api/Lembrete/{id}`             | Atualiza lembrete                                |
| **DELETE** | `/api/Lembrete/{id}`             | Remove lembrete                                  |
| **GET**    | `/api/AlertaCrise`               | Lista todos os alertas                           |
| **GET**    | `/api/AlertaCrise/{id}`          | Busca alerta por ID                              |
| **GET**    | `/api/AlertaCrise/ativos`        | Lista apenas alertas com status **ativo**        |
| **POST**   | `/api/AlertaCrise`               | Cria um alerta                                   |
| **PUT**    | `/api/AlertaCrise/resolver/{id}` | Marca alerta como **resolvido**                  |
| **DELETE** | `/api/AlertaCrise/{id}`          | Remove alerta                                    |

---