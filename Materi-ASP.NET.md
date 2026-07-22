# MATERI LENGKAP HANDOUT ASP.NET

---

## MODUL 1: GIT

### Apa itu GIT?
GIT adalah **Version Control System (VCS)** untuk memantau perubahan code pada file proyek.

### Konfigurasi
```bash
git config --global user.name "Nama"
git config --global user.email "email"
```

### Cara Kerja GIT (3 Tahap)
```
Modified → Staged → Commited
```

| Tahap | Penjelasan | Perintah |
|-------|-----------|----------|
| **Modified** | Perubahan pada code (ditandai warna kuning di VS Code) | - |
| **Staged** | Perubahan ditambahkan ke Staging Area | `git add file` atau `git add .` |
| **Commited** | Perubahan disimpan permanen ke repository lokal | `git commit -m "pesan"` |

### Branch (Percabangan)
```bash
git branch nama_branch     # Buat branch baru
git checkout nama_branch   # Pindah ke branch
git branch                 # Lihat daftar branch
```

### Merge (Menggabungkan)
```bash
git merge nama_branch      # Gabungkan branch ke branch saat ini
```

### Remote Repository
```bash
git remote add origin https://github.com/...   # Hubungkan ke remote
git push origin master                           # Kirim ke remote
git pull origin master                           # Ambil dari remote
git fetch origin                                 # Cek perubahan tanpa merge
```

### Ignore
Buat file `.gitignore` untuk file/folder yang tidak ingin di-push (bin, obj, dll).

---

## MODUL 2: API

### Apa itu API?
**Application Programming Interface** = perantara dua aplikasi untuk berkomunikasi. Seperti pelayan restoran yang menghubungkan pelanggan dan koki.

### RESTful
REST (Representational State Transfer) menggunakan:
- **HTTP Methods**: GET, POST, PUT, DELETE
- **Format Data**: JSON (JavaScript Object Notation)
- **Parameter**: Query parameters, Headers, Body

### Status Code
| Code | Arti |
|------|------|
| **100** | Informasi (server sudah terima header) |
| **200** | Berhasil |
| **300** | Redirect |
| **400** | Client error (URL/body salah) |
| **500** | Server error |

---

## MODUL 3: SOLID

### Apa itu SOLID?
Prinsip OOP agar code berkualitas baik, mudah di-maintain, testing, dan fleksibel.

### 1. Single Responsibility Principle (SRP)
> Satu class = satu tanggung jawab

**Contoh Salah:** Satu method untuk SELECT, INSERT, dan UPDATE
**Contoh Benar:** 3 method terpisah + 1 method koordinator

### 2. Open/Closed Principle (OCP)
> Class terbuka untuk ekstensi, tertutup untuk modifikasi

**Contoh:** Kalkulator luasan → tambah fitur volume TANPA mengubah kode luasan

### 3. Liskov Substitution Principle (LSP)
> Object program harus bisa diganti dengan subkelasnya tanpa mengubah korektivitas

**Contoh:** Penguin adalah Burung tapi TIDAK bisa terbang
**Solusi:** Pisahkan ke interface `IBisaTerbang`

### 4. Interface Segregation Principle (ISP)
> Klien tidak boleh dipaksa depend pada interface yang tidak digunakan

**Contoh Salah:** Interface besar dengan semua method CRUD → class dipaksa implement semua
**Solusi:** Pisah menjadi interface kecil-kecil

### 5. Dependency Inversion Principle (DIP)
> Modul tingkat tinggi TIDAK boleh depend pada modul tingkat rendah. Keduanya depend pada abstraksi.

**Implementasi:** Clean Architecture (Domain → Persistence → Application → Infrastructure)

---

## MODUL 4: ADO.NET

### Apa itu ADO.NET?
Library .NET untuk berinteraksi dengan database.

### File Structure
```
ADO.NET/
├── Program.cs                    → Entry point
├── Controllers/CategoriesController.cs → CRUD operations
├── Models/Categories.cs          → Data model
└── appsettings.json              → Connection string
```

### Anatomi CRUD Operations

#### INSERT (Tambah Data)
```csharp
using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();
    string query = @"INSERT INTO Categories (CategoryID, CategoryName, Description) 
                     VALUES (@CategoryID, @CategoryName, @Description)";
    using (SqlCommand command = new SqlCommand(query, connection))
    {
        command.Parameters.AddWithValue("@CategoryID", categories.CategoryID);
        command.Parameters.AddWithValue("@CategoryName", categories.CategoryName);
        command.Parameters.AddWithValue("@Description", categories.Description);
        command.ExecuteNonQuery();
    }
}
```

#### READ (Ambil Data)
```csharp
using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();
    string query = "SELECT * FROM Categories";
    using (SqlCommand command = new SqlCommand(query, connection))
    {
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Categories category = new Categories();
                category.CategoryID = reader.GetInt32(0);
                category.CategoryName = reader.GetString(1);
                category.Description = reader.GetString(2);
                categoriesList.Add(category);
            }
        }
    }
}
```

#### UPDATE (Ubah Data)
```csharp
string query = "UPDATE Categories SET Description = @Description WHERE CategoryID = @CategoryID";
command.Parameters.AddWithValue("@Description", categories.Description);
command.Parameters.AddWithValue("@CategoryID", categories.CategoryID);
command.ExecuteNonQuery();
```

#### DELETE (Hapus Data)
```csharp
string query = "DELETE FROM Categories WHERE CategoryID = @CategoryID";
command.Parameters.AddWithValue("@CategoryID", id);
command.ExecuteNonQuery();
```

### Alur ADO.NET (5 Langkah)
```
1. SqlConnection (buat koneksi)
2. connection.Open() (buka koneksi)
3. SqlCommand (buat query)
4. ExecuteNonQuery/ExecuteReader (jalankan query)
5. connection.Close() (tutup koneksi)
```

### Konsep Penting ADO.NET
| Konsep | Fungsi |
|--------|--------|
| **SqlConnection** | Membuat koneksi ke database |
| **SqlCommand** | Mengirim query SQL |
| **SqlDataReader** | Membaca hasil query |
| **ExecuteNonQuery()** | Untuk INSERT/UPDATE/DELETE |
| **ExecuteReader()** | Untuk SELECT |
| **Parameterized Query** | Mencegah SQL Injection |
| **Using Block** | Auto close & dispose koneksi |

### Connection String
```
Server=localhost,1433;Database=Northwind;User Id=sa;Password=Password123!;TrustServerCertificate=true;
```
- **Server**: Alamat SQL Server
- **Database**: Nama database
- **User Id/Password**: Credential login

---

## MODUL 5: ORM (Entity Framework Core)

### Apa itu ORM?
**Object-Relational Mapping** = teknik pemrograman yang memberikan abstraksi lebih tinggi dari raw SQL. Query ditulis pakai **LINQ**, bukan SQL mentah.

### Perbedaan ADO.NET vs EF Core

| Aspek | ADO.NET | EF Core |
|-------|---------|---------|
| **Connection** | Manual (Open/Close/Dispose) | Otomatis via DI |
| **Query** | Raw SQL string | LINQ expression |
| **Mapping** | Manual (reader.GetInt32(0)) | Otomatis |
| **Insert** | ~25 baris code | 2 baris (`.Add()` + `.SaveChanges()`) |
| **Read All** | ~20 baris code | 1 baris (`.ToList()`) |
| **SQL Injection** | Harus parameterized sendiri | Otomatis aman |

### File Structure
```
ORM(EFCore)/
├── Program.cs                → Entry point + DI registration
├── DBContext/DataContext.cs   → DbContext (gateway ke database)
├── Models/Categories.cs      → Entity model
└── Controllers/CategoriesController.cs → CRUD operations
```

### Key Components

#### 1. DataContext (DbContext)
```csharp
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<Categories> Categories { get; set; }  // Representasi tabel
}
```

#### 2. Entity Model
```csharp
public class Categories
{
    [Key]
    public int CategoryID { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
}
```

#### 3. DI Registration (Program.cs)
```csharp
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

#### 4. CRUD Operations (SANGAT SIMPEL!)

**INSERT:**
```csharp
_context.Categories.Add(category);
_context.SaveChanges();
```

**READ ALL:**
```csharp
var result = _context.Categories.ToList();
```

**READ ONE:**
```csharp
var result = _context.Categories.Find(id);
```

**UPDATE:**
```csharp
var category = _context.Categories.Find(id);
category.Description = newDescription;
_context.SaveChanges();
```

**DELETE:**
```csharp
var category = _context.Categories.Find(id);
_context.Categories.Remove(category);
_context.SaveChanges();
```

### Konsep Penting EF Core
| Konsep | Fungsi |
|--------|--------|
| **DbContext** | Session dengan database |
| **DbSet<T>** | Representasi tabel sebagai collection |
| **Change Tracking** | Otomatis detect perubahan |
| **SaveChanges()** | Eksekusi SQL ke database |
| **LINQ** | Query pakai C# syntax (Where, Select, etc) |
| **Data Annotations** | `[Key]`, `[Required]` untuk mapping |
| **AddDbContext** | Registrasi DI untuk DbContext |

---

## MODUL 6: CLEAN ARCHITECTURE

### Apa itu Clean Architecture?
Arsitektur software yang menekankan **separation of concerns**, **independence dari external systems**, dan **ease of testing**.

### Layer Structure (seperti bawang berlapis)
```
┌─────────────────────────────────┐
│       Infrastructure            │  ← Outermost (API, Controllers)
├─────────────────────────────────┤
│       Application               │  ← Business Logic
├─────────────────────────────────┤
│       Persistence               │  ← Data Access (EF Core)
├─────────────────────────────────┤
│         Domain                  │  ← Innermost (Entities, Interfaces)
└─────────────────────────────────┘
```

### Dependency Graph
```
Infrastructure → Application → Domain ← Persistence
       │                           ↑
       └───────────────────────────┘
```

**Aturan Utama:** Dependencies flow INWARD. No inner layer references outer layer.

### Layer Details

#### 1. Domain (Innermost - Tidak ada dependensi)
- **Apa:** Entities dan Business Rules
- **Depends on:** Tidak ada (pure .NET class library)
- **Berisi:**
  - `Entities/` → Categories.cs, BaseEntity.cs
  - `Interfaces/Application/` → IBaseApplication, ICategoriesApplication
  - `Interfaces/Persistence/` → IBasePersistence, ICategoriesPersistence

#### 2. Persistence (Data Access Layer)
- **Apa:** Implementasi data access pakai EF Core
- **Depends on:** Domain
- **Berisi:**
  - `Database/DatabaseContext.cs` → DbContext
  - `DAL/CategoriesPersistence.cs` → Implementasi ICategoriesPersistence

#### 3. Application (Business Logic)
- **Apa:** Orchestration logic, validasi, workflow
- **Depends on:** Domain
- **Berisi:**
  - `BusinessLogic/CategoriesApplication.cs` → Implementasi ICategoriesApplication

#### 4. Infrastructure (Outermost - Entry Point)
- **Apa:** ASP.NET Core Web API host
- **Depends on:** Domain, Application, Persistence
- **Berisi:**
  - `Controllers/CategoriesController.cs` → REST API
  - `Program.cs` → Entry point + DI
  - `ScopeRegister/ScopeRegister.cs` → Centralized DI registration

### Request Flow
```
HTTP Request
    ↓
Infrastructure (Controller receives HTTP request)
    ↓
Application (Business Logic processes the request)
    ↓
Persistence (EF Core queries the database)
    ↓
Database (SQL Server returns data)
    ↓
Data flows back up through the same layers
```

### Concrete Example (GET api/Categories/GetRecords)
1. **Infrastructure:** `CategoriesController.GetRecords()` → calls `_application.GetRecords()`
2. **Application:** `CategoriesApplication.GetRecords()` → calls `_persistence.GetRecords()`
3. **Persistence:** `CategoriesPersistence.GetRecords()` → executes `_context.Categories.ToList()`
4. **Result:** Data flows back → HTTP 200 OK response

### DI Registration (ScopeRegister.cs)
```csharp
services.AddScoped<ICategoriesPersistence, CategoriesPersistence>();
services.AddScoped<ICategoriesApplication, CategoriesApplication>();
```

### Key Concepts
| Konsep | Penjelasan |
|--------|-----------|
| **Separation of Concerns** | Setiap layer punya tanggung jawab tunggal |
| **Dependency Inversion** | Domain define interfaces, outer layers implement |
| **Interface Segregation** | Interface terpisah untuk Application & Persistence |
| **Composition Root** | `ScopeRegister.cs` = tempat registrasi DI |
| **Testability** | Setiap layer bisa di-test secara isolated (mock interfaces) |

### Benefits
| Benefit | Penjelasan |
|---------|-----------|
| **Maintainability** | Ganti database? Cuma ubah Persistence layer |
| **Testability** | Mock interfaces untuk unit test |
| **Framework Independence** | Domain tanpa framework dependency |
| **Replaceability** | Ganti EF Core ke Dapper? Cuma rewrite Persistence |
| **Parallel Development** | Tim bisa kerja di layer berbeda secara bersamaan |

---

## MODUL 7: ContohAPI & ChallengeCleanArch

### ContohAPI
Contoh sederhana API dengan ADO.NET - hanya implementasi GET data (Read).

### ChallengeCleanArch
Challenge/praktik Clean Architecture dengan entity `Products`:
- **Domain:** Products entity + IBaseApplication + IProductsApplication + IBasePersistence + IProductsPersistence
- **Application:** ProductsApplication (business logic)
- **Persistence:** ProductsPersistence + AppDbContext
- **Infrastructure:** ProductsController + DI registration

---

## PERBANDINGAN LENGKAP: ADO.NET vs EF Core

| Aspek | ADO.NET | EF Core |
|-------|---------|---------|
| **Tingkat Abstraksi** | Low-level (dekat SQL) | High-level (dekat C#) |
| **Connection** | Manual buka/tutup | Otomatis via DI |
| **Query** | Raw SQL string | LINQ expression |
| **Parameter Binding** | Manual `AddWithValue` | Otomatis |
| **Object Mapping** | Manual (reader.GetInt32) | Otomatis |
| **Change Tracking** | Tidak ada | Built-in |
| **Schema Management** | Manual SQL | Migrations (Code First) |
| **Security** | Harus parameterized | Otomatis aman |
| **Jumlah Code** | Banyak (~25 baris per operasi) | Sedikit (1-2 baris) |
| **Cocok Untuk** | Kontrol penuh, query kompleks | CRUD standard, development cepat |

---

## TIPS UNTUK TES BESOK

### 1. Hafal Alur ADO.NET
```
SqlConnection → Open() → SqlCommand → ExecuteNonQuery/ExecuteReader → Close()
```

### 2. Pahami Perbedaan ADO.NET vs EF Core
- ADO.NET = manual, banyak code, kontrol penuh
- EF Core = otomatis, sedikit code, ORM

### 3. Ingat 5 Prinsip SOLID
- **S**RP = Satu class, satu tanggung jawab
- **O**CP = Terbuka ekstensi, tertutup modifikasi
- **L**SP = Subclass bisa menggantikan parent
- **I**SP = Interface kecil-kecil, jangan paksa implement yang tidak perlu
- **D**IP = Depend pada abstraksi, bukan implementasi

### 4. Pahami Clean Architecture
- Domain = Entities + Interfaces (innermost)
- Persistence = Data Access (EF Core)
- Application = Business Logic
- Infrastructure = API/Controllers (outermost)
- Dependencies flow INWARD

### 5. Ingat HTTP Methods & Status Code
- GET = Read, POST = Create, PUT = Update, DELETE = Delete
- 200 = OK, 400 = Bad Request, 500 = Server Error

### 6. Connection String Format
```
Server=alamat;Database=nama_db;User Id=user;Password=pass;TrustServerCertificate=true;
```

### 7. Key Difference: ADO.NET vs EF Core
```csharp
// ADO.NET Insert (~25 baris)
using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();
    string query = "INSERT INTO Categories ...";
    using (SqlCommand command = new SqlCommand(query, connection))
    {
        command.Parameters.AddWithValue("@param", value);
        command.ExecuteNonQuery();
    }
}

// EF Core Insert (2 baris)
_context.Categories.Add(category);
_context.SaveChanges();
```

---

* Materi ini disusun berdasarkan Handout-ASP.NET untuk persiapan tes.*
