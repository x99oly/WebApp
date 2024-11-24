# Donation Management System

Este projeto é uma aplicação para gerenciamento de doações que conecta **donors** (doadores), **PCs** (Pontos de Coleta), e **CERSAM** (entidade que recebe os lotes de doações). O sistema foi desenvolvido em **.NET Framework** com **Razor Pages**, seguindo os princípios de **Domain-Driven Design (DDD)** e utilizando **MySQL** como banco de dados relacional.

---

## 🛠️ Funcionalidades

1. **Gestão de Doadores (DONOR):**  
   Permite que doadores façam doações para pontos de coleta específicos.

2. **Registro de Doações (DONATION):**  
   Cada doação é associada a um ponto de coleta (PC) gerido por um usuário específico.

3. **Gerenciamento de Disponibilidade (AVAILABILITY):**  
   Cada ponto de coleta mantém o registro de sua disponibilidade para receber doações.

4. **Fechamento de Lotes de Doações (DONATION_LOT):**  
   Um usuário pode agrupar várias doações em um lote e transferi-lo para o **CERSAM**.

---

## 🔧 Tecnologias Utilizadas

- **Backend:** .NET Framework com Razor Pages.
- **Frontend:** Bootstrap para design responsivo.
- **Banco de Dados:** MySQL.
- **ORM:** Implementação manual via métodos genéricos com **Reflection**:
  - Os métodos constroem dinamicamente as classes com base na premissa de que os atributos das classes correspondem aos nomes das colunas no banco de dados.

---

## 📂 Estrutura de Pastas

```plaintext
WebApp
├── Properties
├── wwwroot
├── Aid
│   └── Códigos auxiliares (ex.: hash, strings aleatórias, etc.)
├── Domain
│   ├── Serviços que intermediam entidades com a camada de persistência
│   ├── DTOs, enums, interfaces
├── Pages
│   └── Páginas Razor (csHTML)
├── Persistence
│   └── Código responsável pela comunicação com o banco de dados
├── Services
│   └── Serviços específicos do domínio
├── appsettings.json
├── Credentials.json
│   └── Contém as credenciais usadas para acessar o banco de dados
└── Program.cs
