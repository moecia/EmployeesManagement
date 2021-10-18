# EmployeesManagement
An employee management demo that users can view/add/update/delete employees.

In the employee panel, the user can edit the employee's first name, last name, hired date, and assigned task.
## Projects
- EmployeeManagement.Endpoints: API implementation.
- EmployeeManagement.Data: Model, data repositories, and helper method to read/write to json file.
- EmployeeManagement.Testing: Unit testing project to test data read/write.
- EmployeeManagement.Web: Frontend code by using React/Ant Design.
## Development Environment
- Backend: .NET Core 3.1
- Frontend: React + JavaScript
- Node.js: v14.17.0
- Visual Studio: version 2019
## Run locally
### Backend
1. Open EmployeeManagement.sln in Visual Studio 2019.
2. Set <b>EmployeeManagement.Endpoints</b> as Startup project
3. Run EmployeeManagement.Endpoints. The apis will be running in http://localhost:5100. Make sure the port is available.
### Frontend
1. Open EmployeeManagement.Web in Visual Studio Code or command line prompt.
2. Run ```npm install``` to retrieve node.js
3. Run ```npn start``` to start the frontend application.
## Run Unit Testing
1. Build the  testing project.
2. Copy ```Employees.json``` and ```Tasks.json``` from /EmployeeManagement.Endpoints to /EmployeeManagement.Testing/bin/Debug/netcoreapp3.1.
3. Run test cases.