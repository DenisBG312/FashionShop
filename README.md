# Fashion Shop 🛒

## Project deployed on: http://fashionshop.runasp.net/

&#x20;&#x20;

## 🌟 Overview

**Fashion Shop** is a modern and fully functional online shopping platform designed to provide users with a seamless shopping experience. Featuring multilingual support, dynamic product listings, and a secure checkout process, this platform is built with scalability and user engagement in mind.

---

## ✨ Features
- **Stripe Payment Integration:** Secure online payments using Stripe for seamless order processing.
- **User Authentication**: Secure login and registration using Microsoft Authentication.
- **Shopping Cart**: Add, update, and manage items before placing orders.
- **Wishlist**: Add your favourite products that you wait to be on sale.
- **Multilingual Support**: Switch languages dynamically.
- **Product Reviews**: Rate and review products for better customer insights.
- **Admin Panel**: Manage products, stock, and orders efficiently.
- **Order History**: Track past transactions with detailed records.
- **Responsive Design**: Fully optimized for all devices.
- **PDF Exporting:** When you finish an order you can export pdf with all the order information.

---

## 💻 Technologies Used

| Technology       | Purpose                                   |
| ---------------- | ----------------------------------------- |
| ASP.NET Core     | Backend framework for web development     |
| Entity Framework | Database management and ORM               |
| MSSQL Server     | Database storage                          |
| NUnit            | Unit testing for services and controllers |
| Bootstrap        | Styling and responsive design             |
| Stripe API       | Secure online payment processing          |

---

## 🛠️ Database Diagram

![Database Diagram](https://i.ibb.co/KwQCmhN/image.png)

## 🚀 Getting Started

Follow these steps to get the project up and running locally:

### Prerequisites

- .NET SDK 6.0 or higher
- SQL Server
- Node.js (for managing frontend dependencies)
- Visual Studio or VS Code

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/DenisBG312/FashionShop
   ```

2. Navigate to the project directory:

   ```bash
   cd OnlineShop.Web
   ```

3. Configure your database connection:

   - Open `appsettings.json`
   - Add your **MSSQL connection string** under the `ConnectionStrings` section:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=FashionShop;Trusted_Connection=True;TrustServerCertificate=True;"
     }
     ```

4. Configure Stripe API keys in `appsettings.json`:

   ```json
   "Stripe": {
     "PublishableKey": "your_publishable_key",
     "SecretKey": "your_secret_key"
   }
   ```

5. Restore dependencies:

   ```bash
   dotnet restore
   ```

6. Apply migrations to set up the database:

   ```bash
   dotnet ef database update
   ```

7. Run the application:

   ```bash
   dotnet run
   ```

8. Open your browser and go to `http://localhost:5000` to view the app.

---

## 🗂️ Screenshots

### Landing Page

![Landing Page](https://github.com/DenisBG312/FashionShop/blob/master/ApplicationScreenshots/home_index.png)

### Products

![Products](https://github.com/DenisBG312/FashionShop/blob/master/ApplicationScreenshots/product_index.png)

### Shopping Cart

![Shopping Cart](https://github.com/DenisBG312/FashionShop/blob/master/ApplicationScreenshots/shoppingCart_index.png)

### Stripe Checkout Page
![Stripe Checkout Page](https://github.com/DenisBG312/FashionShop/blob/master/ApplicationScreenshots/stripe_page.png)

---

## 🔑 Credentials

Below are the credentials for the seeded admin in the application:

### User Account
- **Username**: john@email.com
- **Password**: John1234

### Admin Account

- **Username**: [admin@onlineshop.com](mailto\:admin@onlineshop.com)
- **Password**: Admin1234

---

## 💜 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

### ⭐ Show Your Support

If you like this project, consider giving it a star ⭐ on GitHub!
