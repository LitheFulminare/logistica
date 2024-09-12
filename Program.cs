using Logistica;
using System;
using System.IO;

// paths dos arquivos
string caminhoesPath = "data/caminhoes.txt"; // plate, capacity
string produtosPath = "data/produtos.txt"; // cost, weight
string unidadesPath = "data/unidades.txt"; // code, distance, capacity

List<Truck> trucks = new List<Truck>();

// lê os dados dos caminhões
using (StreamReader reader = new StreamReader(caminhoesPath))
{
    string? plate;

    // lê a linha que contém a placa do caminhão (eu espero)
    while ((plate = reader.ReadLine()) != null)
    {

        // lê a proxima linha, q na teoria deve ser a capacidade do caminhao
        string? capacityStr = reader.ReadLine();

        // tenta transformar a string capacidade num 'int'
        // cria nova intancia do obj 'Truck' usando os dados q acabou de ler e adciona a lista
        if (int.TryParse(capacityStr, out int capacity))
        {
            
            Truck truck = new Truck(plate, capacity);
            trucks.Add(truck);
        }
        else
        {
            Console.WriteLine($"Erro: Capacidade '{capacityStr}' é invalida para o caminhão de placa '{plate}'.");
        }
    }
}

// printa os dados de todos os caminhoes
Console.WriteLine("\n--- List of Trucks ---");
foreach (var truck in trucks)
{
    Console.WriteLine(truck);
}
