using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

class Task
{
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public string Category { get; set; }
    public DateTime? DueDate { get; set; }
    public int Priority { get; set; } // 1 (Low) to 5 (High)
}

class Program
{
    private static List<Task> tasks = new List<Task>();
    private const string FILE_PATH = "tasks.json";

    static void Main(string[] args)
    {
        LoadTasks();

        while (true)
        {
            Console.WriteLine("\n--- Advanced Todo List ---");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. List Tasks");
            Console.WriteLine("3. Complete Task");
            Console.WriteLine("4. Remove Task");
            Console.WriteLine("5. Edit Task");
            Console.WriteLine("6. Filter Tasks");
            Console.WriteLine("7. Sort Tasks");
            Console.WriteLine("8. View Task Statistics");
            Console.WriteLine("9. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ListTasks(tasks);
                    break;
                case "3":
                    CompleteTask();
                    break;
                case "4":
                    RemoveTask();
                    break;
                case "5":
                    EditTask();
                    break;
                case "6":
                    FilterTasks();
                    break;
                case "7":
                    SortTasks();
                    break;
                case "8":
                    ViewTaskStatistics();
                    break;
                case "9":
                    SaveTasks();
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void AddTask()
    {
        Console.Write("Enter task description: ");
        string description = Console.ReadLine();

        Console.Write("Enter category: ");
        string category = Console.ReadLine();

        Console.Write("Enter due date (YYYY-MM-DD) or leave blank: ");
        string dueDateInput = Console.ReadLine();
        DateTime? dueDate = string.IsNullOrEmpty(dueDateInput) ? null : DateTime.Parse(dueDateInput);

        Console.Write("Enter priority (1-5): ");
        int priority = int.Parse(Console.ReadLine());

        tasks.Add(new Task
        {
            Description = description,
            IsCompleted = false,
            Category = category,
            DueDate = dueDate,
            Priority = priority
        });

        Console.WriteLine("Task added successfully.");
    }

    static void ListTasks(List<Task> tasksToList)
    {
        if (tasksToList.Count == 0)
        {
            Console.WriteLine("No tasks found.");
            return;
        }

        for (int i = 0; i < tasksToList.Count; i++)
        {
            var task = tasksToList[i];
            Console.WriteLine($"{i + 1}. [{(task.IsCompleted ? "X" : " ")}] {task.Description} " +
                              $"(Category: {task.Category}, Due: {task.DueDate?.ToString("yyyy-MM-dd") ?? "N/A"}, " +
                              $"Priority: {task.Priority})");
        }
    }

    static void CompleteTask()
    {
        ListTasks(tasks);
        Console.Write("Enter the number of the task to mark as complete: ");
        if (int.TryParse(Console.ReadLine(), out int taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count)
        {
            tasks[taskNumber - 1].IsCompleted = true;
            Console.WriteLine("Task marked as complete.");
        }
        else
        {
            Console.WriteLine("Invalid task number.");
        }
    }

    static void RemoveTask()
    {
        ListTasks(tasks);
        Console.Write("Enter the number of the task to remove: ");
        if (int.TryParse(Console.ReadLine(), out int taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count)
        {
            tasks.RemoveAt(taskNumber - 1);
            Console.WriteLine("Task removed successfully.");
        }
        else
        {
            Console.WriteLine("Invalid task number.");
        }
    }

    static void EditTask()
    {
        ListTasks(tasks);
        Console.Write("Enter the number of the task to edit: ");
        if (int.TryParse(Console.ReadLine(), out int taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count)
        {
            var task = tasks[taskNumber - 1];

            Console.Write($"Enter new description (current: {task.Description}): ");
            string newDescription = Console.ReadLine();
            if (!string.IsNullOrEmpty(newDescription))
                task.Description = newDescription;

            Console.Write($"Enter new category (current: {task.Category}): ");
            string newCategory = Console.ReadLine();
            if (!string.IsNullOrEmpty(newCategory))
                task.Category = newCategory;

            Console.Write($"Enter new due date (YYYY-MM-DD) (current: {task.DueDate?.ToString("yyyy-MM-dd") ?? "N/A"}): ");
            string newDueDateInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(newDueDateInput))
                task.DueDate = DateTime.Parse(newDueDateInput);

            Console.Write($"Enter new priority (1-5) (current: {task.Priority}): ");
            if (int.TryParse(Console.ReadLine(), out int newPriority))
                task.Priority = newPriority;

            Console.WriteLine("Task updated successfully.");
        }
        else
        {
            Console.WriteLine("Invalid task number.");
        }
    }

    static void FilterTasks()
    {
        Console.WriteLine("Filter by:");
        Console.WriteLine("1. Category");
        Console.WriteLine("2. Completion Status");
        Console.WriteLine("3. Due Date");
        Console.WriteLine("4. Priority");
        Console.Write("Choose a filter option: ");

        string filterChoice = Console.ReadLine();
        List<Task> filteredTasks = new List<Task>();

        switch (filterChoice)
        {
            case "1":
                Console.Write("Enter category to filter by: ");
                string category = Console.ReadLine();
                filteredTasks = tasks.Where(t => t.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
                break;
            case "2":
                Console.Write("Filter completed tasks? (y/n): ");
                bool isCompleted = Console.ReadLine().ToLower() == "y";
                filteredTasks = tasks.Where(t => t.IsCompleted == isCompleted).ToList();
                break;
            case "3":
                Console.Write("Enter due date to filter by (YYYY-MM-DD): ");
                DateTime dueDate = DateTime.Parse(Console.ReadLine());
                filteredTasks = tasks.Where(t => t.DueDate?.Date == dueDate.Date).ToList();
                break;
            case "4":
                Console.Write("Enter priority to filter by (1-5): ");
                int priority = int.Parse(Console.ReadLine());
                filteredTasks = tasks.Where(t => t.Priority == priority).ToList();
                break;
            default:
                Console.WriteLine("Invalid filter option.");
                return;
        }

        ListTasks(filteredTasks);
    }

    static void SortTasks()
    {
        Console.WriteLine("Sort by:");
        Console.WriteLine("1. Due Date");
        Console.WriteLine("2. Priority");
        Console.WriteLine("3. Category");
        Console.Write("Choose a sort option: ");

        string sortChoice = Console.ReadLine();
        List<Task> sortedTasks = new List<Task>();

        switch (sortChoice)
        {
            case "1":
                sortedTasks = tasks.OrderBy(t => t.DueDate ?? DateTime.MaxValue).ToList();
                break;
            case "2":
                sortedTasks = tasks.OrderByDescending(t => t.Priority).ToList();
                break;
            case "3":
                sortedTasks = tasks.OrderBy(t => t.Category).ToList();
                break;
            default:
                Console.WriteLine("Invalid sort option.");
                return;
        }

        ListTasks(sortedTasks);
    }

    static void ViewTaskStatistics()
    {
        int totalTasks = tasks.Count;
        int completedTasks = tasks.Count(t => t.IsCompleted);
        int pendingTasks = totalTasks - completedTasks;
        var categories = tasks.GroupBy(t => t.Category).Select(g => new { Category = g.Key, Count = g.Count() });
        var overdueTasks = tasks.Count(t => t.DueDate < DateTime.Today && !t.IsCompleted);

        Console.WriteLine("\n--- Task Statistics ---");
        Console.WriteLine($"Total Tasks: {totalTasks}");
        Console.WriteLine($"Completed Tasks: {completedTasks}");
        Console.WriteLine($"Pending Tasks: {pendingTasks}");
        Console.WriteLine($"Overdue Tasks: {overdueTasks}");
        Console.WriteLine("Tasks by Category:");
        foreach (var category in categories)
        {
            Console.WriteLine($"  {category.Category}: {category.Count}");
        }
    }

    static void LoadTasks()
    {
        if (File.Exists(FILE_PATH))
        {
            string json = File.ReadAllText(FILE_PATH);
            tasks = JsonSerializer.Deserialize<List<Task>>(json);
        }
    }

    static void SaveTasks()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(tasks, options);
        File.WriteAllText(FILE_PATH, json);
    }
}
