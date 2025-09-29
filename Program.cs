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
    /*
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
            Console.WriteLine("Enter new character name: ");
            string? Name = Console.ReadLine();
            if (!string.IsNullOrEmpty(Name))
            {
                // check for duplicate name
                List<string> LowerCaseNames = Names.ConvertAll(n => n.ToLower());
                if (LowerCaseNames.Contains(Name.ToLower()))
                {
                    logger.Info($"Duplicate name {Name}");
                }
                else
                {
                    // generate id - use max value in Ids + 1
                    UInt64 Id = Ids.Max() + 1;
                    // input character description
                    Console.WriteLine("Enter description:");
                    string? Description = Console.ReadLine();
                    // input character species
                    Console.WriteLine("Enter species:");
                    string? SpeciesValue = Console.ReadLine();
                    // input character first appearance
                    Console.WriteLine("Enter first appearance:");
                    string? FirstAppearance = Console.ReadLine();
                    // input character year created
                    Console.WriteLine("Enter year created:");
                    string? yearCreatedStr = Console.ReadLine();
                    if (UInt64.TryParse(yearCreatedStr, out UInt64 YearOfOrgin))
                    {
                        Console.WriteLine($"{Id}, {Name}, {Description}, {SpeciesValue}, {FirstAppearance}, {YearOfOrgin}");

                        // Add to lists
                        Ids.Add(Id);
                        Names.Add(Name);
                        Descriptions.Add(Description ?? "");
                        Species.Add(SpeciesValue ?? "");
                        FirstApperance.Add(FirstAppearance ?? "");
                        YearCreated.Add(YearOfOrgin);

                        // Append to CSV file
                        try
                        {
                            using StreamWriter sw = new(file, append: true);
                            sw.WriteLine($"{Id},{Name},{Description},{SpeciesValue},{FirstAppearance},{YearOfOrgin}");
                            logger.Info("Character added to file: {Name}", Name);
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
            for (int i = 0; i < Ids.Count; i++)
            {
                // display character details
                Console.WriteLine($"Id: {Ids[i]}");
                Console.WriteLine($"Name: {Names[i]}");
                Console.WriteLine($"Description: {Descriptions[i]}");
                Console.WriteLine($"Species: {Species[i]}");
                Console.WriteLine($"First Appearance: {FirstApperance[i]}");
                Console.WriteLine($"Year Created: {YearCreated[i]}");
                Console.WriteLine();
            }
        }
    } while (choice == "1" || choice == "2");
    */
}
