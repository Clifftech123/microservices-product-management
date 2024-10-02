


# Product Management Microservices

This repository contains a simple microservices architecture for managing products, using RabbitMQ for communication and ASP.NET Core Web API.

## Services

### Product Service
- Manages the list of products.
- Publishes product-related events to RabbitMQ.

### Consumer Service
- Interacts with users.
- Retrieves product data from the Product Service.
- Handles user requests.
- Validates tokens issued by the Authentication Service.
- Listens to RabbitMQ for product-related events.

### Authentication Service
- Handles user authentication.
- Issues tokens.

## Getting Started

### Prerequisites
- Docker
- Docker Compose

### Running the Services

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/microservices-product-management.git
   cd microservices-product-management
   ```

2. Build and run the services using Docker Compose:
   ```bash
   docker-compose up --build
   ```

### Endpoints

#### Consumer Service

- **Register**: `POST /api/consumer/register`
- **Login**: `POST /api/consumer/login`
- **Get All Products**: `GET /api/consumer/products`
- **Get Product by ID**: `GET /api/consumer/products/{id}`

## License
This project is licensed under the MIT License.


### Summary

- **GitHub Repository Name**: `microservices-product-management`
- **Solution Name**: `ProductManagementSolution`
- **Project Names**: `ProductService`, `ConsumerService`, `AuthService`
- **Directory Structure**: Organized with separate folders for each service and a `docker-compose.yml` file at the root.
- **README.md**: Provides an overview, setup instructions, and endpoint details.

