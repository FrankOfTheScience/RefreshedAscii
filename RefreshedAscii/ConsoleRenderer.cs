using System.Runtime.CompilerServices;

namespace RefreshedAscii;
internal static class ConsoleRenderer
{
    static internal void SetConsoleDefault()
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Clear();
        DisplayLogo();
    }

    internal static void DisplayLogo()
    {
        string logo = @"
            ██████╗ ███████╗███████╗██████╗ ███████╗███████╗██╗  ██╗███████╗██████╗          
            ██╔══██╗██╔════╝██╔════╝██╔══██╗██╔════╝██╔════╝██║  ██║██╔════╝██╔══██╗         
            ██████╔╝█████╗  █████╗  ██████╔╝█████╗  ███████╗███████║█████╗  ██║  ██║         
            ██╔══██╗██╔══╝  ██╔══╝  ██╔══██╗██╔══╝  ╚════██║██╔══██║██╔══╝  ██║  ██║         
            ██║  ██║███████╗██║     ██║  ██║███████╗███████║██║  ██║███████╗██████╔╝         
            ╚═╝  ╚═╝╚══════╝╚═╝     ╚═╝  ╚═╝╚══════╝╚══════╝╚═╝  ╚═╝╚══════╝╚═════╝          
             █████╗ ███████╗ ██████╗██╗██╗     █████╗ ██████╗ ████████╗                      
            ██╔══██╗██╔════╝██╔════╝██║██║    ██╔══██╗██╔══██╗╚══██╔══╝                      
            ███████║███████╗██║     ██║██║    ███████║██████╔╝   ██║                         
            ██╔══██║╚════██║██║     ██║██║    ██╔══██║██╔══██╗   ██║                         
            ██║  ██║███████║╚██████╗██║██║    ██║  ██║██║  ██║   ██║                         
            ╚═╝  ╚═╝╚══════╝ ╚═════╝╚═╝╚═╝    ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝                         
             ██████╗ ██████╗ ███╗   ██╗██╗   ██╗███████╗██████╗ ████████╗███████╗██████╗     
            ██╔════╝██╔═══██╗████╗  ██║██║   ██║██╔════╝██╔══██╗╚══██╔══╝██╔════╝██╔══██╗    
            ██║     ██║   ██║██╔██╗ ██║██║   ██║█████╗  ██████╔╝   ██║   █████╗  ██████╔╝    
            ██║     ██║   ██║██║╚██╗██║╚██╗ ██╔╝██╔══╝  ██╔══██╗   ██║   ██╔══╝  ██╔══██╗    
            ╚██████╗╚██████╔╝██║ ╚████║ ╚████╔╝ ███████╗██║  ██║   ██║   ███████╗██║  ██║    
             ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝  ╚═══╝  ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚══════╝╚═╝  ╚═╝    
            ";

        Console.Clear(); // Pulisci la console
        var consoleWidth = Console.WindowWidth;

        // Calcola la larghezza massima del logo
        var logoLines = logo.Split('\n');
        int maxWidth = logoLines.Max(line => line.Length);

        // Calcola il margine sinistro per centrare il logo
        int leftMargin = (consoleWidth - maxWidth) / 2;

        // Visualizza il logo centrato
        foreach (var line in logoLines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue; // Ignora righe vuote
            Console.SetCursorPosition(leftMargin, Console.CursorTop);
            Console.WriteLine(line);
        }

        Console.WriteLine(); // Aggiungi una riga vuota dopo il logo
        Thread.Sleep(1000);
    }



    static internal void MainMenu()
    {
        bool exit = false;

        while (!exit) 
        {
            SetConsoleDefault();
            Console.WriteLine("\u001b[33m Select an option: \u001b[0m");
            Console.WriteLine("\u001b[33m [1]. Convert an image to ASCII Art \u001b[0m");
            Console.WriteLine("\u001b[33m [2]. Exit \u001b[0m");
            Console.WriteLine("\u001b[32m Choices: \u001b[0m");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    FileManager.SelectJpgFile();
                    SetConsoleDefault();
                    break;
                case "2":
                    Console.WriteLine("\u001b[32m Exiting...\u001b[0m");
                    Thread.Sleep(1000);
                    exit = true;
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("\u001b[31m Operation not valid. Try again.\u001b[0m");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }
}