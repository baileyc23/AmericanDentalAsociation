# Lab 9

## Turning the app into an Enterprise Application with multiple projects and layers

Take your time to refactor the application into a more enterprise-like structure. You can use the following structure as a guide:

- Data Access Layer
- Models (Entities) layer
- Utility Layer
- Web Application Layer

### Instructions

- move the `Data` folder into a new project  `DataAccessLayer` including the migrations
- move the `Models` folder into a new project `Models`
- Create a Utility static class in a new project `Utility` that will have all the constants used in the applications
- Spend time fixing all the namespaces and understanding how the application is now structured

