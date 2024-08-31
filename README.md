# Advanced Todo List Application

This is an simple command-line todo list application built with C# and .NET 7.0. It provides a task management experience with features like categories, due dates, priorities, and more.

## Features

- Add, edit, and remove tasks
- Mark tasks as complete
- Categorize tasks
- Set due dates and priorities
- Filter tasks by various criteria
- Sort tasks by due date, priority, or category
- View task statistics
- Persistent storage using JSON

## Requirements

- .NET 7.0 SDK

## How to Run

1. Clone this repository or download the source code.
2. Navigate to the project directory in your terminal.
3. Run the following command:

   ```
   dotnet run
   ```

4. Follow the on-screen prompts to manage your tasks.

## Project Structure

- `Program.cs`: Contains the main application logic and user interface.
- `Project.csproj`: The project file specifying the .NET SDK and project properties.
- `tasks.json`: (Created after first run) Stores the tasks data.

## Usage

The application presents a menu with the following options:

1. Add Task
2. List Tasks
3. Complete Task
4. Remove Task
5. Edit Task
6. Filter Tasks
7. Sort Tasks
8. View Task Statistics
9. Exit

Choose an option by entering the corresponding number and follow the prompts.