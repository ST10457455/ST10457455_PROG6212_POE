# 💼 Claim System Web Application

## 📖 Overview
The **Claim System Web Application** is a cloud-enabled ASP.NET Core MVC project designed for managing lecturer claims efficiently and securely. It streamlines the process of submitting, reviewing, and approving claims, while integrating with **Azure services** for storage and scalability.

This project forms part of **PROG6212 – Portfolio of Evidence (POE) Part 2**.

---

## 🌟 Features
✅ Lecturer Registration and Claim Submission  
✅ Claim Review and Approval Workflow  
✅ File Uploads to **Azure Blob Storage**  
✅ Messaging with **Azure Queue Storage**  
✅ Secure Data Storage using **Entity Framework Core**  
✅ Modern MVC architecture with Razor Pages  
✅ Cloud Deployment on **Microsoft Azure**

---

## 🏗️ Technologies Used
| Category | Technology |
|-----------|-------------|
| **Framework** | ASP.NET Core MVC (.NET 8.0) |
| **Database** | Entity Framework Core |
| **Cloud Services** | Azure Blob Storage, Azure Queue Storage |
| **Frontend** | Razor Views, HTML, CSS, Bootstrap |
| **IDE** | Visual Studio / VS Code |
| **Version Control** | Git & GitHub |
| **Language** | C# |

---

## 🧩 Project Structure
ClaimSystem.Web/
│
├── Controllers/
│ └── ClaimController.cs
│
├── Models/
│ ├── Lecturer.cs
│ ├── Claim.cs
│ ├── SupportingDocument.cs
│
├── Data/
│ └── ApplicationDbContext.cs
│
├── Views/
│ ├── Claims/
│ │ ├── Create.cshtml
│ │ ├── Details.cshtml
│ │ ├── Pending.cshtml
│ │ └── Review.cshtml
│
├── wwwroot/
│ ├── css/
│ ├── js/
│ └── images/
│
├── appsettings.json
└── Program.cs

yaml
Copy code

---

## ⚙️ Setup Instructions

### 1. Clone the Repository
```bash
git clone https://github.com/ST10457455/ST10457455_PROG6212_POE.git
cd ClaimSystem.Web
2. Install Dependencies
bash
Copy code
dotnet restore
3. Create and Apply Database Migrations
bash
Copy code
dotnet ef migrations add InitialCreate
dotnet ef database update
4. Run the Application
bash
Copy code
dotnet run
The app will be available at:
👉 http://localhost:5000

☁️ Azure Integration
Blob Storage
Used for uploading and storing supporting documents (e.g., receipts or PDFs).

Queue Storage
Used for handling background claim processing messages asynchronously.

👨‍💻 Developer Information
Student Name: Heloise Byrne

Student Number: ST10457455

Module Code: PROG6212

Institution: Varsity College Newlands Cape Town

Project: Portfolio of Evidence – Part 2

🧠 Future Enhancements
Integration with Azure Table Storage for claim audit trails

Implement user authentication via Azure AD B2C

Add email notifications using Azure Logic Apps

Create an Admin Dashboard for statistics and reports

📝 License
This project is created for educational purposes as part of the PROG6212 Portfolio of Evidence and is not licensed for commercial use.
