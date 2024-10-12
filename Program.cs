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
            Console.Clear();

            //* Get and display files under todo-lists folder
            // TODO: Update the locations so that only the name of the files would be displayed
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
        bool shouldExit = false;

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
            DisplayToDoList(toDoListPath);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found.");
            shouldExit = true;
        }
        catch (IOException)
        {
            Console.WriteLine("An error occurred.");
            shouldExit = true;
        }

        while (!shouldExit)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Do you want to edit the list? (y/n)");

                string? readResult = Console.ReadLine();
                if (readResult == null)
                    throw new IOException("You entered an empty text. Please enter your input.");
                else if (readResult.ToLower().Trim().Equals("y"))
                {
                    shouldExit = true;
                    EditList(toDoListPath);
                }
                else if (readResult.ToLower().Trim().Equals("n"))
                    shouldExit = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

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
                    CreateNewToDoList();
                    break;

                case "4":
                    RemoveToDoList();
                    break;

                default:
                    Console.WriteLine("Please enter a valid value.");
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

    static void RemoveToDoList(string toDoListPath)
    {
        //* Remove a to-do list with a pre-specified name
        try
        {
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

    static void EditList(string toDoListPath)
    {
        Console.Clear();

        bool shouldExit = false;

        //* First read and display all lines from the specified to-do list
        DisplayToDoList(toDoListPath);

        while (!shouldExit)
        {
            Console.WriteLine();
            Console.WriteLine("1 - Add a new line");
            Console.WriteLine("2 - Remove existing line");
            Console.WriteLine("3 - Remove to-do list");
            Console.WriteLine("4 - Display the list");
            Console.WriteLine("! - Type \"exit\" to exit.");
            Console.WriteLine();

            //* Read the user input and direct the app according to it
            try
            {
                string? readResult = Console.ReadLine();
                if (readResult == null)
                    throw new IOException("You entered an empty text. Please enter your input.");

                if (readResult.ToLower().Trim().Equals("exit"))
                    shouldExit = true;

                switch (readResult)
                {
                    case "1":
                        AddNewLineToList(toDoListPath);
                        break;

                    case "2":
                        RemoveExistingLine(toDoListPath);
                        break;

                    case "3":
                        RemoveToDoList(toDoListPath);
                        break;

                    case "4":
                        DisplayToDoList(toDoListPath);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    static void AddNewLineToList(string toDoListPath)
    {
        //* Enter a new line to the to-do list with user input
        string? readResult;
        bool shouldExit = false;

        while (!shouldExit)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Please enter the item you want to add to the list. (If you want to exit, type \"exit\".)");
                Console.WriteLine();

                readResult = Console.ReadLine();
                if (readResult == null)
                    throw new IOException("You entered an empty text. Please enter your input.");

                else if (readResult.ToLower().Trim().Equals("exit"))
                    break;

                File.AppendAllText(toDoListPath, $"\n{readResult}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    static void DisplayToDoList(string toDoListPath)
    {
        //* Display a specified to-do list
        string[] toDoListContent = File.ReadAllLines(toDoListPath);

        Console.WriteLine();
        foreach (string line in toDoListContent)
        {
            Console.WriteLine($"- {line}");
        }
        Console.WriteLine();
    }

    static void RemoveExistingLine(string toDoListPath)
    {
        //* Remove a specified line
        try
        {
            string[] toDoListContent = File.ReadAllLines(toDoListPath);

            string? readResult;
            int readValue;

            Console.WriteLine("Please enter the line number you want to delete.");
            readResult = Console.ReadLine();

            if (readResult == null)
                throw new IOException("You entered an empty text. Please enter your input.");

            else if (!int.TryParse(readResult, out readValue))
                throw new IOException("Please enter a valid numeric value.");

            else if (readValue > toDoListContent.Length)
                throw new IndexOutOfRangeException("Please enter a value that exists.");

            readValue--;
            for (int i = readValue + 1; i < toDoListContent.Length; i++)
            {
                toDoListContent[i - 1] = toDoListContent[i];
            }

            string[] newToDoList = new string[toDoListContent.Length - 1];
            for (int i = 0; i < newToDoList.Length; i++)
            {
                newToDoList[i] = toDoListContent[i];
            }

            File.WriteAllLines(toDoListPath, newToDoList);
            DisplayToDoList(toDoListPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void CreateNewToDoList()
    {
        //* Create a new to-do list with a title and open the editing view
        try
        {
            string? readResult;
            string toDoListPath;

            Console.WriteLine("Please enter the name for your list:");

            readResult = Console.ReadLine();
            if (readResult == null)
                throw new IOException("You entered an empty text. Please enter your input.");

            toDoListPath = $"./todo-lists/{readResult}.txt";
            StreamWriter streamWriter = new(toDoListPath);
            streamWriter.Write($"***{readResult}***");
            streamWriter.Close();
            EditList(toDoListPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void TerminateProgram()
    {
        //* Clear the console and exit the program
        Console.Clear();
        Console.WriteLine("Exiting.");
        Environment.Exit(1);
    }
}