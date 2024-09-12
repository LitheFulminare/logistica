using Logistica;
using System;
using System.IO;

// paths dos arquivos
string caminhoesPath = "data/caminhoes.txt"; // plate, capacity
string produtosPath = "data/produtos.txt"; // value, weight
string unidadesPath = "data/unidades.txt"; // code, distance, capacity

Queue<Truck> trucks = new Queue<Truck>(); // fila de caminhoes
Stack<Product> products = new Stack<Product>(); // pilha de produtos
List<Unit> units = new List<Unit>(); // lista de unidades

// lê os dados dos caminhões
using (StreamReader reader = new StreamReader(caminhoesPath))
{
    string? plate;

    // lê a linha que contém a placa do caminhão (eu espero)
    while ((plate = reader.ReadLine()) != null)
    {

        // lê a proxima linha, q na teoria deve ser a capacidade do caminhao
        string? capacityStr = reader.ReadLine();

        // transforma a string capacidade num 'int'
        // cria nova intancia do obj 'Truck' usando os dados q acabou de ler e adciona a lista
        int capacity = 0;
        if (capacityStr != null) { int.Parse(capacityStr); }
        Truck truck = new Truck(plate, capacity);
        trucks.Enqueue(truck);
        
    }
}

using (StreamReader reader = new StreamReader(produtosPath))
{
    string? valueStr;

    // lê a linha que contém o valor do produto
    while ((valueStr = reader.ReadLine()) != null)
    {
        int value = 0;
        if (valueStr != null) { value = int.Parse(valueStr); }

        // lê a proxima linha, o peso do produto
        string? weightStr = reader.ReadLine();

        // tenta transformar a string capacidade num 'int'
        // cria nova intancia do obj 'Truck' usando os dados q acabou de ler e adciona a lista

        if (int.TryParse(weightStr, out int weight))
        {

            Product product = new Product(value, weight);
            products.Append(product);
        }
        else
        {
            Console.WriteLine($"Erro: Custo '{value}' é invalido");
        }
    }
}

// printa os dados de todos os caminhoes
Console.WriteLine("--- Lista de Caminhões ---");
foreach (var truck in trucks)
{
    Console.WriteLine(truck);
}
