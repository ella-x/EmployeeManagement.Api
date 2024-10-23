#EmployeeManagement
EmployeeManagement is a .NET Core application built using the Model First approach. It consists of three projects: a backend REST API service, a frontend ASP.NET Core web application, and a shared model library. The application provides employee registration, company management, and email invitation features.
Project Structure

    EmployeeManagement.Models: Contains the shared data models for the application.
    EmployeeManagement.Api: The backend REST API service, handling data operations and business logic.
    EmployeeManagement.Web: The frontend ASP.NET Core application for user interaction.

Features

    User Registration:
        Collects first name, last name, and email.
        Sends an email verification upon registration.

    Company Management:
        Allows employees to add company details (name and VAT number).

    Email Invitations:
        Enables employees to send email invitations to team members.
        Automatically registers invited employees as valid users in the system.
