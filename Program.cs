namespace ToDoListTracker;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Welcome. Your To-Do Lists: ");
        DisplayToDoLists();

        bool shouldExit = false;

        while (!shouldExit)
        {
            DisplayOptions();
            ChooseOption();
        }
    }

    static void DisplayToDoLists()
    {
        try
        {
            //* Get and display files under todo-lists folder
            string toDoListLocation = @"C:\github\projects\to-do-list-tracker\todo-lists";
            string[] files = Directory.GetFiles(toDoListLocation);
            Console.WriteLine(string.Join(Environment.NewLine, files));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void OpenToDoList()
    {
        //* Open and display a to-do list
        string? toDoListName;
        string toDoListPath = "";

        try
        {
            Console.WriteLine("Please enter the name of the To-Do List you want to open: ");
            toDoListName = Console.ReadLine();

            if (toDoListPath == null)
                throw new IOException("You entered an empty text. Please enter your input.");

            toDoListPath = $"./todo-lists/{toDoListName}";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        try
        {
            string[] lines = File.ReadAllLines(toDoListPath);
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found.");
        }
        catch (IOException)
        {
            Console.WriteLine("An error occurred.");
        }
    }

    static void DisplayOptions()
    {
        //* Display the options for the app
        Console.WriteLine();
        Console.WriteLine("1 - Display To-Do Lists");
        Console.WriteLine("2 - Open a To-Do List");
        Console.WriteLine("3 - Create a New To-Do List");
        Console.WriteLine("4 - Remove a To-Do List");
        Console.WriteLine("! - Type exit to exit.");
        Console.WriteLine();
    }

    static void ChooseOption()
    {
        //* Read the user input and direct the app according to it
        try
        {
            string? readResult = Console.ReadLine();
            if (readResult == null)
                throw new IOException("You entered an empty text. Please enter your input.");

            if (readResult.ToLower().Trim().Equals("exit"))
                TerminateProgram();

            switch (readResult)
            {
                case "1":
                    DisplayToDoLists();
                    break;

                case "2":
                    OpenToDoList();
                    break;

                case "3":
                    break;

                case "4":
                    RemoveToDoList();
                    break;

                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void RemoveToDoList()
    {
        //* Remove a to-do list
        try
        {
            string? toDoListName;

            Console.WriteLine("Please enter the To-Do List's name which you want to remove: ");
            toDoListName = Console.ReadLine();
            string toDoListPath = $"./todo-lists/{toDoListName}";

            if (File.Exists(toDoListPath))
            {
                File.Delete(toDoListPath);
                Console.WriteLine($"File deleted: {toDoListPath}");
            }
            else
            {
                throw new IOException("The specified file does not exist.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void TerminateProgram()
    {
        //* Exit program
        Console.WriteLine("Exiting.");
        Environment.Exit(1);
    }
}