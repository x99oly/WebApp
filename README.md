# Reuse

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
[LInk para versão mais visual](https://drive.google.com/file/d/1LlTMbVpEzopntEGtJBkSf1s2KD9swfLy/view?usp=sharing)
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
```
---
## 🚀 Como rodar:
** Será necessário ter o MySql instalado na sua máquina.
1. Selecione uma pasta do seu dispositivo.
2. Dentro da pasta clique com o botão direito do mouse e escolha 'abrir no terminal'.
3. No terminal digite os comandos:
   ```
   git clone https://github.com/x99oly/WebApp.git
   ```
   após:
   ```
   cd WebApp.git
   ```
5. Ainda no terminal, dentro do diretório, digite o seguinte comando para restaurar os pacotes instalados no projeto.
   ```
   dotnet restore
   ```
6. Crie um arquivo, na raíz do diretório, chamado 'Credential.json'
7. Configure o arquivo da seguinte forma:
    ```
    {
       "EmailSettings": {
       "DomainEmail": "SEU-EMAIL@SEU-PROVEDOR.com",
       "DomainPassword": "xxxx xxxx xxxx xxxx"
     },
     "MySql": {
    "ConnectionString": "Server=SEU-SERVER;Database=reuse;User=SEU-USUÁRIO;Password=SUA-SENHA"
     }
   }
   ```
** Repare que para este projeto funcionar será necessário configurar seu provedor de email para envio por SMTP
Veja em: [mailmeteor](https://mailmeteor.com/blog/gmail-smtp-settings) ou assista [DesignmpNet](https://www.youtube.com/watch?v=LWYs7QjHC_E)
   
7. Após estes vá no arquivo createcode.sql e rode ele dentro do SGBD (MySql).
8. Dentro do diretório, no terminal, digite
   ```
   dotnet WebApp.cs
   ```

## 🌛 Considerações finais:
Devido aos prazos apertados, este código foi entregue na versão piloto (MVP). Como resultado, ele ainda precisa de refatoração e melhorias para se tornar mais conciso e robusto.
