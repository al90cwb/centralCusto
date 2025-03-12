# 📘 Documentação da API - Central de Custo

## **Base URL**
```
http://localhost:5114/api
```

---

## **1️⃣ Usuário**

### **1.1 Criar Usuário (POST)**
Cria um novo usuário e automaticamente cria uma **Central de Custo** para ele.

📌 **Endpoint:**  
```
POST /api/usuario
```
📌 **Headers:**  
```http
Content-Type: application/json
```
📌 **Corpo da Requisição:**  
```json
{
    "nome": "João Silva",
    "email": "joao@email.com",
    "senha": "123456",
    "dataNascimento": "1990-05-20",
    "credencial": "User"
}
```
📌 **Resposta (201 Created):**  
```json
{
    "id": 1,
    "nome": "João Silva",
    "email": "joao@email.com",
    "dataNascimento": "1990-05-20T00:00:00",
    "credencial": "User",
    "dataCriacao": "2024-03-12T12:00:00Z",
    "centralCusto": {
        "id": 1,
        "descricao": "Central de Custo de João Silva",
        "usuarioId": 1,
        "dataCriacao": "2024-03-12T12:00:00Z"
    }
}
```

---

### **1.2 Listar Usuários (GET)**
Retorna a lista de todos os usuários cadastrados.

📌 **Endpoint:**  
```
GET /api/usuario
```
📌 **Resposta (200 OK):**  
```json
[
    {
        "id": 1,
        "nome": "João Silva",
        "email": "joao@email.com",
        "dataNascimento": "1990-05-20T00:00:00",
        "credencial": "User",
        "dataCriacao": "2024-03-12T12:00:00Z"
    }
]
```

---

### **1.3 Obter Usuário por ID (GET)**
Retorna um usuário pelo ID informado.

📌 **Endpoint:**  
```
GET /api/usuario/{id}
```
📌 **Exemplo de Requisição:**  
```
GET /api/usuario/1
```
📌 **Resposta (200 OK):**  
```json
{
    "id": 1,
    "nome": "João Silva",
    "email": "joao@email.com",
    "dataNascimento": "1990-05-20T00:00:00",
    "credencial": "User",
    "dataCriacao": "2024-03-12T12:00:00Z"
}
```

---

### **1.4 Atualizar Usuário (PUT)**
Atualiza os dados cadastrais do usuário (exceto a central de custo).

📌 **Endpoint:**  
```
PUT /api/usuario/{id}
```
📌 **Corpo da Requisição:**  
```json
{
    "nome": "João Pedro Silva",
    "email": "joaopedro@email.com",
    "senha": "novaSenha123",
    "dataNascimento": "1990-05-20",
    "credencial": "User"
}
```
📌 **Resposta (200 OK):**  
```json
{
    "id": 1,
    "nome": "João Pedro Silva",
    "email": "joaopedro@email.com",
    "dataNascimento": "1990-05-20T00:00:00",
    "credencial": "User",
    "dataCriacao": "2024-03-12T12:00:00Z"
}
```

---

### **1.5 Excluir Usuário (DELETE)**
Exclui um usuário pelo ID informado. A exclusão também remove automaticamente a **Central de Custo** vinculada a ele.

📌 **Endpoint:**  
```
DELETE /api/usuario/{id}
```
📌 **Exemplo de Requisição:**  
```
DELETE /api/usuario/1
```
📌 **Resposta (204 No Content)**  
Sem conteúdo.

---

## **2️⃣ Central de Custo**

### **2.1 Obter Central de Custo por ID (GET)**
Retorna uma central de custo pelo ID informado.

📌 **Endpoint:**  
```
GET /api/centralcusto/{id}
```
📌 **Exemplo de Requisição:**  
```
GET /api/centralcusto/1
```
📌 **Resposta (200 OK):**  
```json
{
    "id": 1,
    "descricao": "Central de Custo de João Silva",
    "usuarioId": 1,
    "dataCriacao": "2024-03-12T12:00:00Z"
}
```

---

## **3️⃣ Status de Pagamento (ENUM EEstado)**
Os estados de pagamento possíveis são:
- `Pago`
- `Atrasado`
- `Em dia`
- `Pagar Hoje`
- `Pagar Amanhã`

---

## **4️⃣ Credenciais do Usuário (ENUM ECredencial)**
Os tipos de credenciais possíveis são:
- `User`
- `Admin`

---

## **📌 Considerações finais**
- Esta API foi desenvolvida em **C# .NET** usando **Entity Framework Core** e **SQLite**.
- Ao excluir um **Usuário**, sua **Central de Custo** é excluída automaticamente.
- Para testar os endpoints, pode-se usar ferramentas como **Postman** ou **Insomnia**.

---

📌 **Autor:** [Seu Nome]  
📌 **Projeto:** API Central de Custo  
📌 **Data:** 2024

