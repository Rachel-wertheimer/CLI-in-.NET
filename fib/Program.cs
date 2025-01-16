// See https://aka.ms/new-console-template for more information
using System.CommandLine;
using System.Reflection.Emit;



var rootCommand = new RootCommand("root file");

var bundleCommad = new Command("bundle", "bundle file");

var bundleOption = new Option<FileInfo>("--output", "file pathand and name");
bundleOption.AddAlias("-o");//כינוי אליאס

var language = new Option<string>("--language", "file with this language");

language.AddAlias("-l");

var note = new Option<bool>("--note", "file note");
note.AddAlias("-n");

var sort = new Option<bool>("--sort", () => false, "file sort");
sort.AddAlias("-s");

var author = new Option<string>("--author", "registering the name of the file creator");
author.AddAlias("-a");

var remove_empty_line = new Option<bool>("--remove_empty_line", "file with remove empty line");
remove_empty_line.AddAlias("-r");

rootCommand.AddCommand(bundleCommad);

bundleCommad.AddOption(bundleOption);
bundleCommad.AddOption(language);
bundleCommad.AddOption(note);
bundleCommad.AddOption(sort);
bundleCommad.AddOption(author);
bundleCommad.AddOption(remove_empty_line);

bundleCommad.SetHandler((output, note, language, sort, author, remove_empty_line) =>
{
    try
    {
        // בדיקה אם output הוא null
        if (output == null)
        {
            Console.WriteLine("Error: Missing required option --output | -o");
            return;
        }

        // בדיקה אם language הוא null או ריק
        if (string.IsNullOrEmpty(language))
        {
            Console.WriteLine("Error: Missing required option --language | -l");
            return;
        }

        else
        {
            //שליפת הניתוב  מאיפה הריצו לי את הפקודה 
            string currentDirectory = Directory.GetCurrentDirectory();
            string searchPattern = "";
            string[] sortedFilePaths;
            //מה שלחו לי באופציה לבחירת שפה 

            if (language == "all")
                searchPattern = "";
            else
                searchPattern = "*" + language;
            //שליפת כל הקבצים מהניתוב ומתקיות משנה
            string[] files = Directory.GetFiles(currentDirectory, searchPattern, SearchOption.AllDirectories);
            //בדיקה האם באמת קיים קבצים בניתוב
            if (files.Length == 0)
                Console.WriteLine("file is not exists");
            else
            {
                //?האם השתמשו באופציה של מיון
                if (sort)
                {
                    //מיון הניתוב לפי שמות בסדר ה-א,ב 
                    files = files.OrderBy(files => Path.GetFileNameWithoutExtension(files)).ToArray();
                }
                foreach (string file in files)
                {
                    try
                    {
                        using StreamReader reader = new StreamReader(file);
                        using StreamWriter writer = new StreamWriter(output.FullName, true);
                        string line;
                        // note האם השתמשו באופציה של   
                        if (note)
                        {
                            //bundle שליפת השם של הקובץ המועתק כרגע לקובץ ה 
                            writer.WriteLine("//the name file: " + Path.GetFileNameWithoutExtension(file));
                            //שליפת הניתוב ביחסי בין קובץ החדש לקובץ הנוכחי
                            string relativePath = Path.GetRelativePath(output.FullName, file);
                            writer.WriteLine("//the relative Path is: " + relativePath);
                        }
                        if (author != null)
                        {
                            writer.WriteLine("//the name of the file creator: " + author);
                        }
                        if (remove_empty_line)
                        {
                            while ((line = reader.ReadLine()) != null)
                            {
                                if (!string.IsNullOrEmpty(line))
                                    writer.WriteLine(line);
                            }
                            Console.WriteLine("The file was copied successfully.");
                        }
                        else
                        {
                            while ((line = reader.ReadLine()) != null)
                            {
                                writer.WriteLine(line);
                            }
                            Console.WriteLine("The file was copied successfully.");
                        }
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine("An error occurred while copying the file.");
                    }
                }
            }
        }
    }
    catch (DirectoryNotFoundException ex)
    {
        Console.WriteLine("file path is invalid");
    }
}, bundleOption, note, language, sort, author, remove_empty_line);

var creat_rsp = new Command("creat-rsp", "creat_rsp file");

rootCommand.AddCommand(creat_rsp);

creat_rsp.SetHandler(() =>
{
    try
    {
        string rspFilePath = "MyRespons.rsp";
        using (StreamWriter sw = new StreamWriter(rspFilePath))
        {
            Console.WriteLine("enter file path *if you want* and file name");
            string output_rsp = Console.ReadLine();
            sw.WriteLine("--output " + output_rsp);

            Console.WriteLine("enter language that you want in your file * yoo can enter 'all'*");
            string language_rsp = Console.ReadLine();
            sw.WriteLine("--language " + language_rsp);

            Console.WriteLine("enter 'true' if you want remove_empty_lines");
            string remove_empty_lines_rsp = Console.ReadLine();
            if (bool.Parse(remove_empty_lines_rsp))
                sw.WriteLine("--remove_empty_lines");

            Console.WriteLine("enter 'true' if you want sort file / false");
            string sort_rsp = Console.ReadLine();
            if (bool.Parse(sort_rsp))
                sw.WriteLine("--sort");

            Console.WriteLine("enter 'true' if you want write name file and path fole in the new file / false");
            string note_rsp = Console.ReadLine();
            if (bool.Parse(note_rsp))
                sw.WriteLine("--note");

            Console.WriteLine("enter registering the name of the file creator * if yoy dont want enter false");
            string author_rsp = Console.ReadLine();
            if (author_rsp != "false")
                sw.WriteLine("--author " + author_rsp);
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine("Exception 404");
    }
});

rootCommand.InvokeAsync(args);

