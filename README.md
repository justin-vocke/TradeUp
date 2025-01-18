# Overview

The Stock Price Notification System is a real-time application designed to track stock price changes and notify users of significant fluctuations. Built with a modern event-driven architecture, this project leverages RabbitMQ for messaging, a .NET Core backend for API and business logic, and a React frontend enhanced with TypeScript for a seamless user experience.

## Features

- **Real-Time Stock Price Notifications**: Get live updates on stock prices.
- **Event-Driven Architecture**: Utilizes RabbitMQ to handle messaging and real-time updates.
- **API Integration**: Stock price data is fetched from external APIs.
- **Web Interface**: Built with React to display stock prices in a user-friendly interface.



## Technologies Used

### Backend

- Language: C#

- Framework: .NET Core

- Messaging: RabbitMQ

- Database: SQL Server

- Authentication: JSON Web Tokens (JWT)

### Frontend

- Language: Javascript/TypeScript

- Framework/Library: React

- Styling: CSS Modules

### DevOps (To Be Added)

- CI/CD: GitHub Actions (or Azure DevOps, if applicable)

- Containerization: Docker

- Infrastructure as Code: Terraform (if applicable)

## Architecture

The Stock Price Notification System follows a modular architecture with clear separation of concerns:

- Publisher Service: Fetches stock prices from external APIs and publishes events to RabbitMQ.

- Notification Service: Listens for stock events from RabbitMQ and sends notifications to subscribed users.

- API Gateway: Provides endpoints for user authentication, watchlist management, and retrieving stock data.

- Frontend: Displays real-time stock data and notification history.

Diagram


Getting Started

Prerequisites

.NET Core SDK

Node.js and npm

RabbitMQ

Docker (optional for containerized deployment)

Setup

Clone the repository:

git clone https://github.com/yourusername/stock-notification-system.git
cd stock-notification-system

Set up the backend:

cd backend
dotnet restore
dotnet run

Set up the frontend:

cd frontend
npm install
npm start

Start RabbitMQ (if not already running):

docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management

Access the application:

Frontend: http://localhost:3000

API: http://localhost:5000

Usage

Sign up and log in to the application.

Add stocks to your watchlist.

Receive real-time notifications about price changes.

Manage your notification preferences.

Testing

Backend tests:

cd backend
dotnet test

Frontend tests:

cd frontend
npm test

Future Improvements

Enhanced Analytics: Visualize stock trends and history.

Multitenancy Support: Allow organizations to manage stocks collaboratively.

Mobile App: Expand the platform to iOS and Android.

Contributing

We welcome contributions to improve the system! Please follow these steps:

Fork the repository.

Create a new branch for your feature/fix.

Commit your changes and push to your fork.

Submit a pull request with a detailed description.

License

This project is licensed under the MIT License. See the LICENSE file for details.
