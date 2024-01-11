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
        Sold = false
    },
    new Plant
    {
        Id = 2,
        Species = "Hydrangea",
        LightNeeds = 2,
        AskingPrice = 24.99m,
        City = "Nashville",
        ZIP = 37207,
        Sold = false
    },
    new Plant
    {
        Id = 3,
        Species = "Money Plant",
        LightNeeds = 3,
        AskingPrice = 29.99m,
        City = "Louisville",
        ZIP = 40018,
        Sold = true
    },
    new Plant
    {
        Id = 4,
        Species = "Venus Fly Trap",
        LightNeeds = 2,
        AskingPrice = 42.99m,
        City = "New Jersey",
        ZIP = 07005,
        Sold = false
    },
    new Plant
    {
        Id = 5,
        Species = "Cactus",
        LightNeeds = 4,
        AskingPrice = 17.99m,
        City = "Tuscon",
        ZIP = 85641,
        Sold = false
    }

};

string greeting = "\n\t\t ~~ Welcome To ExtraVert! ~~\n";
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

    }
}

void RandomPlant()
{
    List<Plant> availablePlants = plants.Where(p => !p.Sold).ToList();
    Random random = new();
    int randomPlantIndex = random.Next(1, availablePlants.Count());
    Plant randomPlant = availablePlants[randomPlantIndex];
    Console.WriteLine($"\n\t{randomPlant.Species}   Price: ${randomPlant.AskingPrice}   Location: {randomPlant.City} {randomPlant.ZIP}   Light Needs: {randomPlant.LightNeeds}");
}

void ListPlants()
{
    foreach (Plant plant in plants)
    {
        Console.WriteLine($"\n\t{plant.Id}. A {plant.Species} in {plant.City} {(plant.Sold ? "was sold" : "is available")} for ${plant.AskingPrice}.");
    }
    Console.Write("\n\n\tPress Enter to continue...\n");
    Console.ReadLine();
}

void PostPlant()
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

    Plant newPlant = new()
    {
        Id = plants.Count + 1,
        Species = species,
        LightNeeds = lightNeeds,
        AskingPrice = price,
        City = city,
        ZIP = zip,
        Sold = false

    };

    plants.Add(newPlant);
    Console.WriteLine("\n\tPlant Addition Successful!\n");

}

void AdoptPlant()
{
    string plantChoice = null;

    int index = 1;

    int selectedPlant = 0;

    List<Plant> availablePlants = plants.Where(p => !p.Sold).ToList();

    foreach (Plant plant in availablePlants)
    {
        Console.WriteLine($"\n\t{index++}. {plant.Species}   Price: ${plant.AskingPrice}   Location: {plant.City}");
    }

    Console.Write("\n\tPlease select the number of the plant you wish to adopt:  ");
    plantChoice = Console.ReadLine();

    if (plantChoice != null && plantChoice != "0" && Convert.ToInt32(plantChoice) < availablePlants.Count)
    {
        selectedPlant = Convert.ToInt32(plantChoice) - 1;

        Plant plantBeingAdopted = availablePlants[selectedPlant];

        plantBeingAdopted.Sold = true;

        Console.WriteLine($"\n\tSold! Enjoy your new {plantBeingAdopted.Species}!");
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
        Console.WriteLine($"\n\t{index++}. {plant.Species}   Price: ${plant.AskingPrice}   Location: {plant.City} {plant.ZIP}   Light Needs: {plant.LightNeeds}   {(plant.Sold ? "Isn't" : "Is")} available");
    }

    Console.Write("\n\tPlease select the number of the plant listing to remove: ");

    plantChoice = Console.ReadLine();

    if (plantChoice != null && Convert.ToInt32(plantChoice) < plants.Count && plantChoice != "0")
    {
        plants.RemoveAt(Convert.ToInt32(plantChoice) - 1);
        Console.WriteLine("\n\tThe listing has been removed!");
        index = 1;
        foreach (Plant plant in plants)
        {
            Console.WriteLine($"\n\t{index++}. {plant.Species}   Price: ${plant.AskingPrice}   Location: {plant.City} {plant.ZIP}   Light Needs: {plant.LightNeeds}   {(plant.Sold ? "Isn't" : "Is")} available");
        }
        Console.Write("\n\tPress Enter to continue...");
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

    if (lightChoice >= 1 && lightChoice <= 5)
    {
        foreach (Plant plant in plants)
        {
            if (plant.LightNeeds == lightChoice)
            {
                Console.WriteLine($"\n\t{plant.Species}   Price: {plant.AskingPrice}   Location: {plant.City} {plant.ZIP}   Light Needs: {plant.LightNeeds}   {(plant.Sold ? "Isn't" : "Is")} Available");
            }
        }
    }
    else
    {
        Console.WriteLine("Invalid input, please try again");
        SearchByLight();
    }
}