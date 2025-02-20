# 📚 Book Management API

The **Book Management API** is a RESTful web service built with **ASP.NET Core Web API** that allows managing books. It provides CRUD operations, pagination, and popularity scoring for books.

## 🚀 Features

- 📌 **CRUD Operations**: Add (single and bulk), update, retrieve, and delete books (single and bulk).
- 📄 **Pagination**: Retrieve books with **paginated results**.
- 📊 **Popularity Scoring**: Books are ranked based on views and age.
- 📖 **Validation**: Prevent adding duplicate book titles and handle invalid data.
- 📜 **API Documentation**: Uses **Swagger** for interactive API documentation.

## 📌 API Endpoints

### 📖 Books
| Method | Endpoint | Description |
|--------|---------|-------------|
| `GET` | `/api/books?pageNumber=1&pageSize=10` | Get paginated books list |
| `GET` | `/api/books/{id}` | Get book details by ID |
| `POST` | `/api/books` | Add a new book |
| `POST` | `/api/books/bulk` | Add multiple books |
| `PUT` | `/api/books/{id}` | Update a book |
| `DELETE` | `/api/books/{id}` | Soft delete a book |
| `DELETE` | `/api/books/bulk` | Soft delete multiple books |

## 🏗️ Technologies Used

- **.NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQL Server**
- **Swagger (API Documentation)**
- **3-layered architecture**

## 📜 Installation & Setup

1. Clone the repository:
   ```sh
   git clone https://github.com/Ogabek-Kholmirzaev/BookManagementAPI.git
   cd BookManagementAPI
   ```

2. Install dependencies:
   ```sh
   dotnet restore
   ```

3. It is optional to update the **appsettings.json** for database connection.

4. Run database migrations:
   ```sh
   dotnet ef database update
   ```

5. Run the API:
   ```sh
   dotnet run
   ```

6. Open `https://localhost:7003/swagger` to explore the API.

## 📌 Popularity Score Formula

```csharp
Popularity Score = (BookViews * 0.5) + (YearsSincePublished * 2)
```
- **BookViews**: Number of times book details are retrieved.
- **YearsSincePublished**: The older the book, the lower the bonus.

## ✅ Validation Rules

- A book **cannot be added** if another book with the same **title** already exists.
- Pagination is **limited to 20 items per request**.
- **Required fields**: Title, Author Name, and Publication Year.