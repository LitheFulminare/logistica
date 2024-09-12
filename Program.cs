using Logistica;
using System;
using System.IO;
using System.Reflection.PortableExecutable;

// paths dos arquivos
string caminhoesPath = "data/caminhoes.txt"; // plate, capacity
string produtosPath = "data/produtos.txt"; // value, weight
string unidadesPath = "data/unidades.txt"; // code, distance, capacity

List<Truck> trucks = new List<Truck>(); // lista de caminhoes -> para poder mover para o final
Queue<Product> products = new Queue<Product>(); // fila de produtos -> para poder remover o produto da fila
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
        if (capacityStr != null) { capacity = int.Parse(capacityStr); }
        Truck truck = new Truck(plate, capacity);
        trucks.Add(truck);
        
    }
}

// lê os dados dos produtos
using (StreamReader reader = new StreamReader(produtosPath))
{
    string? weightStr;

    // lê a linha que contém o valor do produto
    while ((weightStr = reader.ReadLine()) != null)
    {

        int weight = 0;
        if (weightStr != null) { weight = int.Parse(weightStr); }

        // lê a proxima linha, o peso do produto
        string? valueStr = reader.ReadLine();

        // tenta transformar a string weight num 'int'
        int value = 0;
        if (valueStr != null) { value = int.Parse(valueStr); }

        // cria nova intancia do obj 'Product' usando os dados q acabou de ler e adciona a lista
        Product product = new Product(weight, value);
        products.Enqueue(product);
    }
}

// CAMINHÕES

//Console.WriteLine($"First truck on the list: {trucks[0]}");
//Console.WriteLine($"Last truck on the list: {trucks[trucks.Count-1]}");

//// joga o primeiro caminhao para o final
//Truck removedTruck = trucks[0];
//trucks.RemoveAt(0);
//trucks.Add(removedTruck);

//Console.WriteLine($"Removed truck: {removedTruck}");
//Console.WriteLine($"Last truck on the list: {trucks[trucks.Count - 1]}");

//Console.WriteLine("--- Lista de Caminhões ---");
//foreach (var truck in trucks)
//{
//    Console.WriteLine(truck);
//}


// PRODUTOS

//Console.WriteLine($"First product in the queue: {products.First()}");
//Console.WriteLine($"Product count: {products.Count}");
//products.Dequeue();
//Console.WriteLine($"Fist product after dequeue: {products.First()}");
//Console.WriteLine($"Product count: {products.Count}");

Console.WriteLine("\n--- Lista de Produtos ---");
foreach (var product in products)
{
    Console.WriteLine(product);
}

// UNIDADES