To run the app

1. Pull the sql server docker image
	docker pull mcr.microsoft.com/mssql/server
2. Start sql server container
	docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Passw0rd!" -p 1433:1433 --name sql_server_container -d mcr.microsoft.com/mssql/server
3. Open the solution
4. Run
