using Logistica;
using System;
using System.IO;

// paths dos arquivos
string caminhoesPath = "data/caminhoes.txt";
string produtosPath = "data/produtos.txt";
string unidadesPath = "data/unidades.txt";

List<Truck> trucks = new List<Truck>();

// Lê os dados dos caminhões
using (StreamReader reader = new StreamReader(caminhoesPath))
{
    string plate = null;

    while ((plate = reader.ReadLine()) != null)
    {
        plate = plate.Trim();

        // Now read the capacity (must be the line right after the plate)
        string capacityStr = reader.ReadLine();

        capacityStr = capacityStr.Trim();

        // Try to parse the capacity into an integer
        if (int.TryParse(capacityStr, out int capacity))
        {
            // Create a new Truck object and add it to the list
            Truck truck = new Truck(plate, capacity);
            trucks.Add(truck);
        }
        else
        {
            Console.WriteLine($"Error: Invalid capacity '{capacityStr}' for truck with plate '{plate}'.");
        }
    }
}

// Output all truck objects
Console.WriteLine("\n--- List of Trucks ---");
foreach (var truck in trucks)
{
    Console.WriteLine(truck);
}
