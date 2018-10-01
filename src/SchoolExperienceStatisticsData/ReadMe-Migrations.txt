CREATE NEW MIGRATION
---------------------
add-migration <MigrationName> -project SchoolExperienceStatisticsData -startup SchoolExperienceStatisticsProcessor


UPDATE DATABASE
---------------

Make sure Src\SchoolExperienceStatisticsData is the default project and src/SchoolExperienceStatisticsProcessor is the startup project.

Update local database:
   $> update-database

This uses the connection string specified in SchoolExperienceStatisticsProcessor/appsettings.Development.json

Update remote database - 
   $> update-database -p src\SchoolExperienceData

This requires you to have a user-secret set to something like:
  "ConnectionStrings:DefaultConnection": "Server=tcp:schoolexperiencebeta.database.windows.net,1433;Trusted_Connection=False;Database=SchoolExperienceBetaDev;Persist Security Info=False;User Id=<USERID>;Password=<PASSWORD>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;"
