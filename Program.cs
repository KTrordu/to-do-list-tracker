namespace ToDoListTracker;

class Program
{
    static void Main(string[] args)
    {
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
        string? toDoListPath = "";

        try
        {
            Console.WriteLine("Please enter the name of the To-Do List you want to open: ");
            toDoListPath = Console.ReadLine();

            if (toDoListPath == null)
                throw new IOException("You entered an empty text. Please enter your input.");

            toDoListPath = $"./todo-lists/{toDoListPath}";
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
}