class Character
{
    public UInt64 Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Species { get; set; }
    public string FirstAppearance { get; set; }
    public UInt64 YearCreated { get; set; }

    public string Display()
    {
        return $"Id: {Id}\nName: {Name}\nDescription: {Description}\nSpecies: {Species}\nFirst Appearance: {FirstAppearance}\nYear Created: {YearCreated}";
   }
}