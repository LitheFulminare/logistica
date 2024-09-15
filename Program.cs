using Logistica;
using System;
using System.IO;
using System.Reflection.PortableExecutable;

// paths dos arquivos
string caminhoesPath = "data/caminhoes.txt"; // plate, capacity
string produtosPath = "data/produtos.txt"; // value, weight
string unidadesPath = "data/unidades.txt"; // code, distance, capacity

// listas geradas a partir dos arquivos
List<Truck> trucks = new List<Truck>(); // lista de caminhoes -> mover o caminhão para o final para o final
Queue<Product> products = new Queue<Product>(); // fila de produtos -> para poder remover o produto da fila
List<Unit> units = new List<Unit>(); // lista de unidades -> não vai ser necessário mexer na ordem delas ou retirar unidades da lista

// outras listas
Queue<Product> remainingProducts = new Queue<Product>();
List<Unit> availableUnits = new List<Unit>();

// dados medidos para responder as perguntas de 1 a 4
int travelledDistance = 0;

// lê os dados dos caminhões
using (StreamReader reader = new StreamReader(caminhoesPath))
{
    string? plate;

    // lê a linha que contém a placa do caminhão (eu espero)
    while ((plate = reader.ReadLine()) != null)
    {

        // lê a proxima linha, q na teoria deve ser a capacidade do caminhao
        string? capacityStr = reader.ReadLine();

        // transforma a string 'capacidadeStr' num 'int'
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

        // tenta transformar a string 'weightStr' num 'int'
        int value = 0;
        if (valueStr != null) { value = int.Parse(valueStr); }

        // cria nova intancia do obj 'Product' usando os dados q acabou de ler e adciona a lista
        Product product = new Product(weight, value);
        products.Enqueue(product);
    }

    // this is called when the reades has finished reading the file
    remainingProducts = products;
}

// lê os dados das unidades
using (StreamReader reader = new StreamReader(unidadesPath))
{
    string? code;

    // lê a linha que contém o código da unidade
    while ((code = reader.ReadLine()) != null)
    {

        // lê a proxima linha, a distância
        string? distanceStr = reader.ReadLine();

        // tenta transformar a string 'distance' num 'int'
        int distance = 0;
        if (distanceStr != null) { distance = int.Parse(distanceStr); }

        // lê a próxima linha, a capacidade de descarregamento
        string? capacityStr = reader.ReadLine();

        // tenta transformar a string 'capacityStr' num 'int'
        int capacity = 0;
        if (capacityStr != null) { capacity = int.Parse(capacityStr); }

        // cria nova intancia do obj 'Product' usando os dados q acabou de ler e adciona a lista
        Unit unit = new Unit(code, distance, capacity);
        units.Add(unit);
    }

    // this is called when the reades has finished reading the file
    availableUnits = units; // sets all units as available
}

bool truckLoaded = false; // debug -> provavelmente não vai ser usado assim no cod. final

//while (remainingProducts.Count() > 0)
//{
//    Load();
//}

while (!truckLoaded) // debug -> provavelmente não vai ser usado assim no cod. final
{
    Load();
}

// função usada para carregar os produtos nos caminhões
// depois chama SendToUnit(), que manda esses caminhões para as unidades
void Load() 
{
    // se o caminhão tiver espaço, ele carrega o item
    if (trucks.First().Load(remainingProducts.First(), remainingProducts.First().Weight))
    {       
        remainingProducts.Dequeue();
    }
    // se não tiver espaço ele é mandado para a unidade
    else
    {
        Console.WriteLine("\nTruck fully loaded");
        Console.WriteLine($"Used capacity: {trucks.First().UsedCapacity}");
        Console.WriteLine($"Remaining capacity: {trucks.First().UnusedCapacity}");
        truckLoaded = true;
        CheckAvailableUnits();
    }
}

// chamada por Load() depois do caminhão ficar cheio
// procura por unidades disponíveis
void CheckAvailableUnits()
{
    bool hasEnoughCapacity = false;
    int i = 0; // usado no loop foreach

    // checa as capacidades das unidades para ver o que é suficiente
    foreach (var unit in availableUnits)
    {
        Console.WriteLine($"\nUnit capacity: {availableUnits[i].Capacity}");

        if (trucks.First().UsedCapacity <= availableUnits[i].Capacity)
        {
            hasEnoughCapacity = true;
            Console.WriteLine($"Unit has enough capacity");
            break;
        }

        i++;
        Console.WriteLine($"Unit doesn't have enough capacity");
    }

    if (hasEnoughCapacity)
    {
        SendToUnit();
    }

    else
    {
        Console.WriteLine("Couldn't find an available unit");

        // reseta a lista de unidades disponíveis e roda a função CheckAvailableUnits() mais uma vez
        availableUnits.Clear();
        availableUnits = units;
        CheckAvailableUnits();
    }
}

// chamado por CheckAvailableUnits() se a unidade tiver capacidade suficiente pra descarregar
void SendToUnit()
{
    Console.WriteLine($"\nTruck of plate {trucks.First().Plate} will be sent to unit of code {availableUnits[0].Code}");
}

// manda o primeiro caminhao para o final da fila
void SendTruckToLast()
{
    Truck removedtruck = trucks[0];
    trucks.RemoveAt(0);
    trucks.Add(removedtruck);
}



// ---------------------------------------------------
// --------------------- DEBUG -----------------------
// ---------------------------------------------------

// CAMINHÕES

//Console.WriteLine($"First truck on the list: {trucks[0]}");
//Console.WriteLine($"Truck count: {trucks.Count}");
//Console.WriteLine($"Last truck on the list: {trucks[trucks.Count - 1]}");

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

//Console.WriteLine("\n--- Lista de Produtos ---");
//foreach (var product in products)
//{
//    Console.WriteLine(product);
//}



// UNIDADES
//units.RemoveAt(0);
//Console.WriteLine("\n--- Lista de Unidades ---");
//foreach (var unit in units)
//{
//    Console.WriteLine(unit);
//}