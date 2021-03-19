# Store Order Manager

## Project Description

This is an online webservice to allow product managers to easily create and manage orders. Through an easy to use web interface, the application enables said managers to place orders with support for multiple type of products at once, create new customers, and filter customers, locations and orders by various parameters. Most impoartantly though, all of the data is hosted and managed through an external SQL database, ensuring that the data is secure and reliable.

## Technologies Used

* C# 9 / .NET 5
* ASP.NET Core - version 5.6.3
* Entity Framework Core - version 5.0.3
* SQL Server - version 12.0.2000.8

## Features

List of features ready and TODOs for future development
* Create and place product orders
* Add additional customers to the database
* Search for orders based on the customer, location 

To-do list:
* Add user authentication to allow different access levels per user
* Implement deeper analysis of orders to easily show trends on graphs (e.g quantity purchased of specific product over time)

## Getting Started

> The program must use SQL server, other SQL variations may not work.

- Run the command `git clone https://github.com/2102-feb08-net/bryson-project1` to clone the repository.
- Create a new SQL database in Azure
- In database settings, set the firewall settings to "Allow Azure services and resources to access this server"
- Create a new AppService in Azure
- In the App Service settings under Configuration, create a new connection string with Name being "DigitalStoreDb" and the Value being the connection string to the database created earlier.
- Open DigitalStore_Tables.sql in SQL Server 2019
- Run the script on the new database that was just made
- Open the Solution file in Visual Studio
- Add the
- Finally, right-click on the Web project and then select Publish to begin publishing the project.
- Follow the steps show to then publish the application to your Azure App Service.
- Finally, open the URL to your App Service in your web browser of choice to run the application.

## Usage

- To add a customer:
  - Go to the Customer tab
  - Enter in the first and last name
  - Press the Submit button
- To search for a customer or location:
  - Go to the respective tab
  - Enter your search query in the search bar
  - Press the Search button
- To view orders:
  - Go to the View Order dropdown under the Orders tab
  - Click on the order to recieve more details about it
  > Tip: Orders can be filter to customer or location by clicking on the respective field on the Customer and Location pages
- To place an order:
  - Go to the Place an Order dropdown under the Orders tab
  - Select a customer to order for
  - Select a location to order from
  - Select a product you wish to add to your
  - Enter the quantity of the product you wish to order
  - Press the Add Product button to the the product to your cart
  - Keep adding products until satisfied with your order
  - > Tip: Add a quantity of 0 to remove the product from the cart
  - Press the Submit button to submit your order
  - 
## License

This project uses the following license: [<license_name>](<link>).
