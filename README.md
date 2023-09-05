# MyDockerApi
CRUD using dotnet 6 and mysql 8 to run in a simple dockerized enviroment.

# Tools
| Tool | Version |
| --- | --- |
| Docker | 24.0.5 |
| Docker Compose | 2.21.0 |
| Dotnet | 6.0.121 |
| Dotnet ef | 7.0.10 |
| MySql | 8.0.34 |
---
# Create and Run containers
Open your terminal in the project root directory (./MyDockerApi) and run the commands:

```
docker-compose build
docker-compose up
```
---
Check if the containers are up and runing with the command ```docker ps```

# Run migration
Open your terminal in the project root directory (./MyDockerApi) and run the commands:
```
dotnet ef database update
```
---
You can access the database via ```MySQL Workbench``` using the host ```localhost:3307``` and see that a table called 'Task' was created in TaskDb.

# Test the API
Now you can open your browser and access ```http://localhost:5000/swagger/index.html``` and test the CRUD operstions.
