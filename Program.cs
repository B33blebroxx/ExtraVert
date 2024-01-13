using System.Linq.Expressions;
using System.Numerics;

List<Plant> plants = new()
{
    new Plant
    {
        Id = 1,
        Species = "Bamboo plant",
        LightNeeds = 5,
        AskingPrice = 55.00m,
        City = "Seattle",
        ZIP = 98101,
        Sold = false,
        AvailableUntil = new DateTime(2024, 04, 27)
    },
    new Plant
    {
        Id = 2,
        Species = "Hydrangea",
        LightNeeds = 2,
        AskingPrice = 24.99m,
        City = "Nashville",
        ZIP = 37207,
        Sold = false,
        AvailableUntil = new DateTime(2023, 06, 25)
    },
    new Plant
    {
        Id = 3,
        Species = "Money Plant",
        LightNeeds = 3,
        AskingPrice = 29.99m,
        City = "Louisville",
        ZIP = 40018,
        Sold = true,
        AvailableUntil = new DateTime(2024, 09, 22)
    },
    new Plant
    {
        Id = 4,
        Species = "Venus Fly Trap",
        LightNeeds = 2,
        AskingPrice = 42.99m,
        City = "New Jersey",
        ZIP = 07005,
        Sold = false,
        AvailableUntil = new DateTime(2024, 08, 13)
    },
    new Plant
    {
        Id = 5,
        Species = "Cactus",
        LightNeeds = 4,
        AskingPrice = 17.99m,
        City = "Tuscon",
        ZIP = 85641,
        Sold = false,
        AvailableUntil = new DateTime(2022, 01, 10)
    }

};

string greeting = "\n\t\t\t\t ~~ Welcome To ExtraVert! ~~\n";
string? choice = null;

while (choice != "0")
{
    Console.WriteLine(greeting);
    Console.WriteLine("\n\t0. Exit");
    Console.WriteLine("\n\t1. Display All Plants");
    Console.WriteLine("\n\t2. Post a Plant for Adoption");
    Console.WriteLine("\n\t3. Adopt a Plant");
    Console.WriteLine("\n\t4. Delist a Plant");
    Console.WriteLine("\n\t5. Plant of the Day");
    Console.WriteLine("\n\t6. Search by Light Needs");
    Console.WriteLine("\n\t7. Show Plant Stats");
    Console.Write("\n\n\t\tPlease choose an option:  ");
    choice = Console.ReadLine();

    switch (choice)
    {
        case "0":
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);
            break;

        case "1":
            ListPlants();
            break;

        case "2":
            PostPlant();
            break;

        case "3":
            AdoptPlant();
            break;

        case "4":
            DelistPlant();
            break;

        case "5":
            RandomPlant();
            break;

        case "6":
            SearchByLight();
            break;

        case "7":
            ShowStats();
            break;

    }
}

void RandomPlant()
{
    List<Plant> availablePlants = plants.Where(p => !p.Sold).ToList();

    Random random = new();

    int randomPlantIndex = random.Next(1, availablePlants.Count());

    Plant randomPlant = availablePlants[randomPlantIndex];

    Console.WriteLine($"\n\t{PlantDetails(randomPlant)}  Price: ${randomPlant.AskingPrice}  Location: {randomPlant.City} {randomPlant.ZIP}  Post Expires: {randomPlant.AvailableUntil}  Light Needs: {randomPlant.LightNeeds}");
    Console.WriteLine("\n\t\t Press Enter to continue...");
    Console.ReadLine();
}

void ListPlants()
{
    foreach (Plant plant in plants)
    {
        Console.WriteLine($"\n\t{plant.Id}. A {plant.Species} in {plant.City} {(plant.Sold ? "was sold" : "is available")} for ${plant.AskingPrice} until {plant.AvailableUntil}.");
    }
    Console.Write("\n\n\tPress Enter to continue...\n");
    Console.ReadLine();
}

void PostPlant()
{
    try
    {
        Console.Write("\n\tEnter the plant's species: ");
        string species = Console.ReadLine();

        Console.Write("\n\tEnter the new plant's light needs on a scale of 1-5 (1 = total shade 5 = total light): ");
        int lightNeeds = Convert.ToInt32(Console.ReadLine());

        Console.Write("\n\tEnter the price of your plant (ex. = 25.00): ");
        decimal price = Convert.ToDecimal(Console.ReadLine());

        Console.Write("\n\tEnter the name of the city in which your plant is located: ");
        string city = Console.ReadLine();

        Console.Write("\n\tEnter the city's ZIP code: ");
        int zip = Convert.ToInt32(Console.ReadLine());

        Console.Write("\n\tEnter the year this listing expires (YYYY): ");
        int year = Convert.ToInt32(Console.ReadLine());

        Console.Write("\n\tEnter the month this listing expires (MM): ");
        int month = Convert.ToInt32(Console.ReadLine());

        Console.Write("\n\tEnter the numerical day this listing will expire (DD): ");
        int day = Convert.ToInt32(Console.ReadLine());


        Plant newPlant = new()
        {
            Id = plants.Count + 1,
            Species = species,
            LightNeeds = lightNeeds,
            AskingPrice = price,
            City = city,
            ZIP = zip,
            Sold = false,
            AvailableUntil = new DateTime(year, month, day)

        };

        plants.Add(newPlant);
        Console.WriteLine("\n\t\tPlant listing successfully added! Press Enter to continue...");
        Console.ReadLine();
    }
    catch
    {
        Console.WriteLine("Input not recognized, returning to main menu.");
    }

}
Console.WriteLine("\n\tPlant Addition Successful!\n");
Console.Write("\n\n\tPress Enter to continue...");
Console.ReadLine();

void AdoptPlant()
{
    string plantChoice = null;

    int index = 1;

    int selectedPlant = 0;

    List<Plant> unsoldPlants = plants.Where(p => !p.Sold).ToList();
    List<Plant> availablePlants = unsoldPlants.Where(p => p.AvailableUntil >= DateTime.Now).ToList();

    foreach (Plant plant in availablePlants)
    {
        Console.WriteLine($"\n\t{index++}. {plant.Species}   Price: ${plant.AskingPrice}   Available Until: {plant.AvailableUntil}    Location: {plant.City}");
    }
    Console.WriteLine($"\n\t\t\t\t\t~~ Plants available: {availablePlants.Count()} ~~");
    Console.Write("\n\tPlease select the number of the plant you wish to adopt:  ");
    plantChoice = Console.ReadLine();

    if (plantChoice != null && plantChoice != "0" && Convert.ToInt32(plantChoice) <= availablePlants.Count)
    {
        selectedPlant = Convert.ToInt32(plantChoice) - 1;

        Plant plantBeingAdopted = availablePlants[selectedPlant];

        plantBeingAdopted.Sold = true;

        Console.WriteLine($"\n\tSold! Enjoy your new {plantBeingAdopted.Species}!");
        Console.Write("\n\n\tPress Enter to continue...");
        Console.ReadLine();
    }
    else
    {
        Console.WriteLine($"\n\t{plantChoice} is not a valid option, please try again.");
        AdoptPlant();
    }

}

void DelistPlant()
{
    string plantChoice = null;

    int index = 1;

    foreach (Plant plant in plants)
    {
        Console.WriteLine($"\n\t{index++}. {plant.Species}  Price: ${plant.AskingPrice}  Location: {plant.City} {plant.ZIP} ");
    }

    Console.Write("\n\tPlease select the number of the plant listing to remove: ");

    plantChoice = Console.ReadLine();

    if (plantChoice != null && Convert.ToInt32(plantChoice) <= plants.Count && plantChoice != "0")
    {
        plants.RemoveAt(Convert.ToInt32(plantChoice) - 1);

        Console.WriteLine("\n\tThe listing has been removed!");

        index = 1;

        foreach (Plant plant in plants)
        {
            Console.WriteLine($"\n\t{index++}. {plant.Species}  Price: ${plant.AskingPrice}  Location: {plant.City} {plant.ZIP}");
        }
        Console.Write("\n\n\tPress Enter to continue...");
        Console.ReadLine();
    }
    else
    {
        Console.WriteLine($"\n\t{plantChoice} is not a valid selection, please try again.");
        DelistPlant();
    }
}

void SearchByLight()
{
    Console.Write("\n\tEnter desired maximum light value (1-5): ");

    int lightChoice = Convert.ToInt32(Console.ReadLine());

    List<Plant> searchedPlants = plants.Where(p => p.LightNeeds == lightChoice).ToList();

    try
    {
        foreach (Plant plant in searchedPlants)
        {
            Console.WriteLine($"\n\t{plant.Species}  Price: {plant.AskingPrice}  Location: {plant.City} {plant.ZIP}  Available Until: {plant.AvailableUntil}  {(plant.Sold ? "Isn't" : "")} Available");
        }
            Console.WriteLine("\n\t\tPress Enter to continue...");
            Console.ReadLine();

        if (searchedPlants.Count == 0 && lightChoice <= 5)
        {
            Console.WriteLine("\n\tNo plants found matching your search. Press Enter to continue...");
            Console.ReadLine();
        }

        if (lightChoice > 5)
        {
            Console.WriteLine("\n\tSearch parameter selection must be between 1 - 5. Please try again...");
            SearchByLight();
        }
    }
    catch
    {
        Console.WriteLine("\n\tNot a valid input, please try again!");
        SearchByLight();
    }

}

void ShowCheapestPlant()
{
    List<Decimal> prices = plants.Select(p => p.AskingPrice).ToList();

    decimal lowestPrice = prices.Min();

    Plant cheapestPlant = plants.FirstOrDefault(p => p.AskingPrice == lowestPrice);

    List<Plant> unSoldPlants = plants.Where(p => !p.Sold).ToList();

    List<Plant> availablePlants = unSoldPlants.Where(a => a.AvailableUntil >= DateTime.Now).ToList();

    Console.WriteLine($"\n\t\tThe plant with the lowest cost of ${cheapestPlant.AskingPrice} is the {cheapestPlant.Species}.");
}

void AvailablePlantCount()
{
    List<Plant> unsoldPlants = plants.Where(p => !p.Sold).ToList();

    List<Plant> availablePlants = unsoldPlants.Where(p => p.AvailableUntil >= DateTime.Now).ToList();

    Console.WriteLine($"\n\t\tThere are {availablePlants.Count} plants available for purchase.");
}

void AverageLightNeeds()
{
    List<int> lightNeeds = plants.Select(p => p.LightNeeds).ToList();

    double averageLightNeeds = lightNeeds.Average();

    Console.WriteLine($"\n\t\tThe average light needs of all plants is {averageLightNeeds}");
}

void HighestLightNeeds()
{
    List<int> lightNeeds = plants.Select(p => p.LightNeeds).ToList();

    int highestLightNeeds = lightNeeds.Max();

    List<Plant> highestLightNeedsPlants = plants.Where(p => p.LightNeeds == highestLightNeeds).ToList();

    foreach (Plant plant in highestLightNeedsPlants)
    {
        Console.WriteLine($"\n\t\tThe {plant.Species} needs the most light with a light need rating of {plant.LightNeeds}.");
    }

}

void PercentOfAdoptedPlants()
{
    List<Plant> adoptedPlants = plants.Where(p => p.Sold).ToList();

    int numberAdopted = adoptedPlants.Count();

    double calculatingAdopted = (double)numberAdopted / plants.Count;

    double percentAdopted = calculatingAdopted * 100;

    Console.WriteLine($"\n\t\t{percentAdopted}% of the plants have been sold");
}

void ShowStats()
{
    Console.WriteLine("\n\n\t\t\t\t~~ Plant Stats ~~");
    ShowCheapestPlant();
    AvailablePlantCount();
    AverageLightNeeds();
    HighestLightNeeds();
    PercentOfAdoptedPlants();

    Console.Write("\n\n\tPress Enter to continue...");
    Console.ReadLine();
}

string PlantDetails(Plant plant)
{
    string plantString = plant.Species;

    return plantString;
}