using NLog;
string path = Directory.GetCurrentDirectory() + "//nlog.config";

// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

string file = "mario.csv";
// make sure the mario file exists
if (!File.Exists(file))
{
    logger.Error("File does not exist: {File}", file);
}
else
{
    // create list of characters
    // to populate the list with data, read from the data file
    List<Character> characters = [];
    try
    {
        StreamReader sr = new(file);
        // first line contains column headers
        sr.ReadLine();
        while (!sr.EndOfStream)
        {
            string? line = sr.ReadLine();
            if (line is not null)
            {
                Character character = new();
                // character details are separated with comma(,)
                string[] characterDetails = line.Split(',');
                // 1st array element contains id
                character.Id = UInt64.Parse(characterDetails[0]);
                // 2nd array element contains character name
                character.Name = characterDetails[1] ?? string.Empty;
                // 3rd array element contains character description
                character.Description = characterDetails[2] ?? string.Empty;
                // 4th array element contains character species
                character.Species = characterDetails[3] ?? string.Empty;
                // 5th array element contains character first appearance
                character.FirstAppearance = characterDetails[4] ?? string.Empty;
                // 6th array element contains character year created
                character.YearCreated = UInt64.Parse(characterDetails[5]);
                characters.Add(character);
            }
        }
        sr.Close();
    }
    catch (Exception ex)
    {
        logger.Error(ex.Message);
    }
    //menu
    string ? choice;
    do
    {
        // display choices to user
        Console.WriteLine("1) Add Character");
        Console.WriteLine("2) Display All Characters");
        Console.WriteLine("Enter to quit");

        // input selection
        choice = Console.ReadLine();
        logger.Info("User choice: {Choice}", choice);

        if (choice == "1")
        {
            // Add Character
            Character character = new();
            Console.WriteLine("Enter new character name: ");
            character.Name = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrEmpty(character.Name))
            {
                // check for duplicate name
                List<string> LowerCaseNames = characters.ConvertAll(character => character.Name.ToLower());
                if (LowerCaseNames.Contains(character.Name.ToLower()))
                {
                    logger.Info($"Duplicate name {character.Name}");
                }
                else
                {
                    // generate id - use max value in Ids + 1
                    character.Id = characters.Max(character => character.Id) + 1;
                    // input character description
                    Console.WriteLine("Enter description:");
                    character.Description = Console.ReadLine() ?? string.Empty;
                    // input character species
                    Console.WriteLine("Enter species:");
                    character.Species = Console.ReadLine() ?? string.Empty;
                    // input character first appearance
                    Console.WriteLine("Enter first appearance:");
                    character.FirstAppearance = Console.ReadLine() ?? string.Empty;
                    // input character year created
                    Console.WriteLine("Enter year created:");
                    string? yearCreatedStr = Console.ReadLine();
                    if (UInt64.TryParse(yearCreatedStr, out UInt64 yearOfOrgin))
                    {
                        Console.WriteLine($"{character.Id}, {character.Name}, {character.Description}, {character.Species}, {character.FirstAppearance}, {yearOfOrgin}");

                        // Add to lists
                        characters.Add(character);

                        // Append to CSV file
                        try
                        {
                            using StreamWriter sw = new(file, append: true);
                            sw.WriteLine($"{character.Id},{character.Name},{character.Description},{character.Species},{character.FirstAppearance},{yearOfOrgin}");
                            logger.Info("Character added to file: {Name}", character.Name);
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Failed to write to file: {Message}", ex.Message);
                        }
                    }
                   else
                    {
                        logger.Error("Invalid year created");
                    }
                }
            }
            else
            {
                logger.Error("You must enter a name");
            }

        }
        else if (choice == "2")
        {
            // Display All Characters
            // loop thru Lists
            
            foreach(Character character in characters)
            {
                // display character details
                Console.WriteLine(character.Display());
            }
        } 
    } while (choice == "1" || choice == "2");
   
}
 