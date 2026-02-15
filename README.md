Zomra - AI-Powered Blood Inventory Management
Zomra is an innovative AI-driven platform designed to optimize blood bank inventories for SMEs and hospitals. It bridges the gap between healthcare providers and donors through a gamified, real-time response system.

 Key Features Implemented
 Admin & System Management
Hospital Onboarding: Full CRUD operations to manage hospital profiles, locations, and administration.

Gamification Setup: Ability to add and manage rewards to incentivize donors.

🏥 Hospital & Inventory Intelligence
Smart Stock Management: Real-time tracking and manual updates (Add/Subtract) for various blood types.

Predictive Transaction Logs: Every movement is logged to feed the AI predictive engine for future demand forecasting.

Emergency Dispatch: Hospitals can raise urgent "Emergency Calls" with specific blood types and urgency levels.

Donation Confirmation: A unique workflow where hospital staff verify a donor's ID to automatically update stock, award points, and close active emergency requests.

 Donor (User) Journey
Personalized Dashboard: Displays real-time points, total successful donations, and blood type.

Smart Donation Timer: Automatically calculates and displays the date for the next eligible donation (Last Donation + 3 Months).

Live Emergency Feed: Shows active blood requests from nearby hospitals.

Rewards Exchange: A store where donors can browse and redeem their earned points for rewards.

Technical Architecture
The project follows Clean Architecture principles to ensure scalability and maintainability:

Domain: Contains Entities (Hospital, BloodInventory, Reward), Interfaces (IUnitOfWork, IBaseRepository), and DTOs.

Application: Core logic including Services (Admin, Inventory, Donor), AutoMapper Profiles, and Result Helpers.

Infrastructure: Database Context (ApplicationDbContext), Repository implementations, and Identity/JWT configuration.

Web API: Controllers handling HTTP requests and providing a fully documented Swagger UI.

 Tech Stack
Framework: .NET 8.0 / ASP.NET Core Web API.

Database: MS SQL Server with Entity Framework Core.

Security: ASP.NET Core Identity with JWT Bearer Authentication.

Tools: AutoMapper, LINQ, Repository Pattern, and Unit of Work.
