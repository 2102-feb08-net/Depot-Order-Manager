# Store Order Manager

## Project Description

This is an online webservice to allow product managers to easily create and manage orders. Through an easy to use web interface, the application enables said managers to add

## Technologies Used

* ASP.NET Core - version 5.6.3
* Entity Framework Core - version 5.0.3
* SQL Server - version 3.0

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

> Here, you instruct other people on how to use your project after they’ve installed it. This would also be a good place to include screenshots of your project in action.

## Contributors

> Here list the people who have contributed to this project. (ignore this section, if its a solo project)

## License

This project uses the following license: [<license_name>](<link>).
