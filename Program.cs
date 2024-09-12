using Logistica;
using System;
using System.IO;

// paths dos arquivos
string caminhoesPath = "data/caminhoes.txt"; // plate, capacity
string produtosPath = "data/produtos.txt"; // value, weight
string unidadesPath = "data/unidades.txt"; // code, distance, capacity

// os dois sao fila porque o primeiro a ser lido vai ser o primeiro da fila
Queue<Truck> trucks = new Queue<Truck>(); // fila de caminhoes
Queue<Product> products = new Queue<Product>(); // fila de produtos
List<Unit> units = new List<Unit>(); // lista de unidades

// lê os dados dos caminhões
using (StreamReader trucksReader = new StreamReader(caminhoesPath))
{
    string? plate;

    // lê a linha que contém a placa do caminhão (eu espero)
    while ((plate = trucksReader.ReadLine()) != null)
    {

        // lê a proxima linha, q na teoria deve ser a capacidade do caminhao
        string? capacityStr = trucksReader.ReadLine();

        // transforma a string capacidade num 'int'
        // cria nova intancia do obj 'Truck' usando os dados q acabou de ler e adciona a lista
        int capacity = 0;
        if (capacityStr != null) { capacity = int.Parse(capacityStr); }
        Truck truck = new Truck(plate, capacity);
        trucks.Enqueue(truck);
        
    }
}

// lê os dados dos produtos
using (StreamReader productsReader = new StreamReader(produtosPath))
{
    string? weightStr;

    // lê a linha que contém o valor do produto
    while ((weightStr = productsReader.ReadLine()) != null)
    {

        int weight = 0;
        if (weightStr != null) { weight = int.Parse(weightStr); }

        // lê a proxima linha, o peso do produto
        string? valueStr = productsReader.ReadLine();

        // tenta transformar a string weight num 'int'
        int value = 0;
        if (valueStr != null) { value = int.Parse(valueStr); }

        // cria nova intancia do obj 'Product' usando os dados q acabou de ler e adciona a lista
        Product product = new Product(weight, value);
        products.Enqueue(product);
    }
}

// printa os dados de todos os caminhoes
//Console.WriteLine("--- Lista de Caminhões ---");
//foreach (var truck in trucks)
//{
//    Console.WriteLine(truck);
//}

// printa dos dados de todos os produtos

Console.WriteLine($"Product count: {products.Count}");
Console.WriteLine($"First product in the queue: {products.First()}");
Console.WriteLine($"Last product in the queue: {products.Last()}");

//Console.WriteLine("\n--- Lista de Produtos ---");
//foreach (var product in products)
//{
//    Console.WriteLine(product);
//}