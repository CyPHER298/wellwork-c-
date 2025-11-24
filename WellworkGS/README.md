# 📌 WellworkGS — API de Produtividade, Organização e Saúde Mental

WellworkGS é uma **API REST em .NET 9** criada para apoiar trabalhadores com neurodivergência na organização do fluxo de trabalho, monitoramento de metas, planejamento de tarefas, gerenciamento de lembretes e ativação de alertas de crise.

A API foi desenvolvida utilizando o padrão **Clean Architecture simplificada**, com camadas independentes (Controllers, Services, Repositories, Domain, DTOs e Infra).  
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
