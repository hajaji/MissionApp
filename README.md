# MissionAPP
## Instalation

Developed with .NET Core 5 using Visual Studio 2019 and MSSQLLocalDB.
MSSQLocalDB shiped with Visual Studio 2019.
To verify that you have it:
- Open Visual Studio.
- Open SQL Server Object Explorer.
- Expand SQL server.

One of the server should be (localdb)\MSSQLLocalDB
If you dont have it, you will need to install SQL Server Express

https://go.microsoft.com/fwlink/?LinkID=866658

In order to install the application

- Clone the repository
- Set **MissionApp.API** as startup project.
- Open the Package Manager Console.
- Make sure that Default project is **MissionApp.Data**.
- Run the commands: ```add-migration init``` and then run ```update-database```.
- Verify that you have a database with name ```MissionDB```.
- In ```MissionApp.Data``` project, you will find folder named **sql_script**, inside the folder there is a file named ```script.sql```. The script populate the database with the input and the settings. Run the script in SQL.
- Build the application.
- At that point you can run the API.
- Swagger - http://localhost:44098/swagger/index.html

To keep things simple I am using trusted connection in the connection string.


