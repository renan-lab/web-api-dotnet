# Web API REST
Esta API tem a função de disponibilizar as funcionalidades de criar, ler, editar e deletar médicos e pacientes. A API se comunica com o SGBD **Microsoft SQL Server**.

## 📖 Documentação da API

### 🧑‍⚕️ Médicos

#### 1. Retorna todos os médicos

```http
GET /api/medicos
```

#### 2. Retorna um médico

```http
GET /api/medicos/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID do médico que você quer |

#### 3. Cadastra um médico

```http
POST /api/medicos
```
É enviado um JSON ao endpoint, exemplo:

_( * = dado obrigatório)_
```json
{
    * "Nome": "Renan",
    "DataNasc": "2003-07-15",
    * "CRM": "654321/SP"
}
```
#### 4. Editar um médico

```http
PUT /api/medicos/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID do médico que você quer editar|

É enviado um JSON ao endpoint, exemplo:

_(deve-se informar o código do médico a ser alterado tanto no endpoint quanto no JSON)_
```json
{
    "Codigo": 9,
    "Nome": "Renan-Editado",
    "DataNasc": "2008-12-15",
    "CRM": "987765/SP"
}
```

#### 5. Deletar um médico

```http
DELETE /api/medicos/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID do médico que você quer deletar|

### 👤 Pacientes

#### 1. Retorna todos os pacientes

```http
GET /api/pacientes
```

#### 2. Retorna um paciente

```http
GET /api/pacientes/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID do paciente que você quer |

#### 3. Cadastra um paciente

```http
POST /api/pacientes
```
É enviado um JSON ao endpoint, exemplo:

_( * = dado obrigatório)_
```json
{
    * "Nome": "paciente-teste",
    * "Email": "paciente-teste@email.com"
}
```

#### 4. Edita um paciente

```http
PUT /api/pacientes/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID do paciente que você quer editar|

É enviado um JSON ao endpoint, exemplo:

_(deve-se informar o código do paciente a ser alterado tanto no endpoint quanto no JSON)_
```json
{
    "Codigo": 2,
    "Nome": "paciente-teste-editado",
    "Email": "paciente-teste-editado@gmail.com"
}
```

#### 5. Deletar um paciente

```http
DELETE /api/pacientes/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID do paciente que você quer deletar|

### ℹ️ Informações adicionais
Esta API utiliza **cache** no repositório de médico, após fazer um `GET /api/medicos` o sistema cria um cache para médicos, fazendo com que não seja mais necessário consultar o SGBD e esperar a resposta dele, tudo fica armazenado junto ao repositório do médico, com isso o tempo de consulta é diminuído drasticamente, provendo mais eficiência ao sistema. Porém, quando forem utilizados os seguintes métodos em médicos: `POST, PUT e DELETE`, o cache é **zerado** e somente irá ser refeito após uma nova consulta `GET /api/medicos`.
