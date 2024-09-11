using Logistica;
using System;
using System.IO;

// paths dos arquivos
string caminhoesPath = "data/caminhoes.txt";
string produtosPath = "data/produtos.txt";
string unidadesPath = "data/unidades.txt";

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
        // cria novo obj 'Truck' usando os dados q acabou de ler e adciona a lista
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
