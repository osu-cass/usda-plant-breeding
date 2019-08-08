# Welcome to the USDA ARS Plant Breeding Program
.Net MVC Web Application for tracking plant breeding. 

## Getting Started
The majority of this project was developed using Windows 10 and Visual Studio Enterprise 2017. 
This development environment is not required to run or contribute to the application.

### To begin, open the `Usda.PlanBreeding.sln` file with Visual Studio. 

### 1. Setting Up the Database
By default, this application is configured to use a local SQL database named `PlantBreeding`. The `Usda.PlantBreeding.Data` 
project contains a publish profile which will create this database, along with the default values necessary for the program to run. 
The following steps should be taken to create the database:
1. Double click on the `Usda.PlantBreeding.Database.publish.xml` file within the `Usda.PlantBreeding.Database` project.
2. Click the `Load Values` button on the lower left of the menu that appears.
	* If you do not wish to include the default values, switch the value of the `defaultValues` variable from a 1 to a 0.
	* <span style="background-color: #f2c36b">NOTE: failing to include default values will cause portions of the application to crash. If you do not include the default values, those values must be provided manually.<span>
3. Click the `Publish` button on the lower right.
4. Open Microsoft SQL Management Studio, and ensure that the `PlantBreeding` database was created on your local SQL server.

### 2. Setting Machine Key Values
This application requires that machine key values be set. The following steps should be taken to set these values:
1. Open the `Web.config` file located within the `Usda.PlantBreeding.Site` project.
2. Locate the `machineKey` section within the file. This section is located around one third of the way into the file.
3. Set both the `decryptionKey` and `validationKey` values, instructions for obtaining these values can be found [here](https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-4.0/w8h3skw9(v=vs.100)).

### 3. Run the Application
By default, this application should have the `Usda.PlantBreeding.Site` project configured as the startup project. 
To run the application, click the green button on the top of the Visual Studio interface, or press f5. 

If an error occurs that reads `A project with Output Type of...`, right click on the `Usda.PlantBreeding.Site` project and select the `Set as Startup Project` option.
Then, try running the application again. 

## Further Steps

### Using Reports
This application makes use of reports to display information. To use the reports in this application, the follow steps should be taken:
1. Ensure that a report server is setup and avaliable for the application to use. 
2. Locate the `Usda.PlantBreeding.Reports` project in Visual Studio. 
3. Right click on the project, and select `properties` at the bottom of the menu. 
4. Note the value located under the `Configuration` section in the top left of the menu. Compare this to the value specified in the top left of the
Visual Studio user interface. <span style="background-color: #f2c36b">Changes made to the configuration will only be applied when the value in the top left of Visual Studio matches the `Configuration` value.<span>
5. Within the configuration menu, fill in the values for the `TargetDataset`, `TargetDataSourceFolder`, `TargetReportFolder`, and `TargetServerUrl` with values from your report server. 
6. Exit the menu, and again right click on the project. 
7. Select the `Deploy` option, and confirm that you wish to deploy the reports to your server. 
8. Locate the `Usda.PlantBreeding.Site` project, and open the `Web.config` file. Locate the `ReportUrl` and `ReportPath` application settings. These setting are located near the bottom of the file.
9. Fill in the `ReportUrl` setting with your report server url, and fill in the `ReportPath` setting with the path to your report files (this value should be the same as the value for the `TargetReportFolder`). 
10. Run the application.

### User Authentication
This application includes built in user authentication, based on security groups. By default, this feature is disabled. To enable this feature, the following steps should be taken:
1. Locate the `Usda.PlantBreeding.Data` project.
2. Within this project, open the `Models` folder, and locate the `UserRole.cs` file.
3. Within the file, specify the groups that you wish to have access to the application by filling in the `Admin` and `Employee` values.
	* <span style="background-color: #f2c36b">NOTE: Currently, the application does not provide different privledges to employee and admin users.<span>

### Web.config Transformations
This application includes files which will transform the `Web.config` values based on the configuration specified in Visual Studio. To edit these files, 
locate the `Usda.PlantBreeding.Site` project. Next, find the `Web.config` file and expand it. The `Web.[ConfigurationName].config` files can be used to replace values in the default `Web.config`. 
When using these files, consider specifying different connection strings and report server variable values for your configurations. These files can also be used when publishing the application, allowing for
easier configuration management.

## Stack
- SSRS
- SSIS
- .Net
- TypeScript
- React
- MVVM
- Entity Framework
- Database

## Contributions
Currently, this project is not accepting contributions. However, if you are intested in this application, please email us at info@CASS.oregonstate.edu. 