using Logistica;
using System;
using System.IO;
using System.Reflection.PortableExecutable;

// paths dos arquivos
string caminhoesPath = "data/caminhoes.txt"; // plate, capacity
string produtosPath = "data/produtos.txt"; // value, weight
string unidadesPath = "data/unidades.txt"; // code, distance, capacity

// listas geradas a partir dos arquivos
List<Truck> trucks = new List<Truck>(); // lista de caminhoes -> mover o caminhão para o final
Queue<Product> products = new Queue<Product>(); // fila de produtos -> para poder remover o produto da fila
List<Unit> units = new List<Unit>(); // lista de unidades -> não vai ser necessário mexer na ordem delas ou retirar unidades da lista

// outras listas
Queue<Product> remainingProducts = new Queue<Product>();
List<Unit> availableUnits = new List<Unit>();

// dados medidos para responder as perguntas de 1 a 4
int travelledDistance = 0;

Truck mostValuableTruck = null; // caminhão que fez a carga de maior valor (será usado a placa dele depois)
int mostValuableLoad = 0; // usado pra definir o caminhão mais valioso (o de cima)

Unit heaviestUnit = null; // unidade que recebeu maior tanto em kg no total
int highestTotalWeight = 0; // usado pra definir o de cima

int totalUnusedCapacity = 0; // guarda quanto de espaço não foi usado depois de cada viagem (apenas a ida é contabilizada, a volta é ignorada)

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
    availableUnits.AddRange(units); // sets all units as available
}


bool truckLoaded = false; // debug -> provavelmente não vai ser usado assim no cod. final

//if (remainingProducts.Count() > 0)
//{
//    Load();
//}

while (!truckLoaded) // debug -> provavelmente não vai ser usado assim no cod. final
{
    if (remainingProducts.Count() == 0)
    {
        truckLoaded = true; // faz o while parar
        Console.WriteLine("\nNo products left");
    }
    else
    {
        Load();
    }
}

// função usada para carregar os produtos nos caminhões
// depois chama SendToUnit(), que manda esses caminhões para as unidades
void Load() 
{
    // se o caminhão tiver espaço, ele carrega o item
    if (trucks.First().Load(remainingProducts.First(), remainingProducts.First().Weight))
    {
        //Console.WriteLine("Dequeue was called");
        remainingProducts.Dequeue();
        if (remainingProducts.Count() == 0)
        {
            Console.WriteLine("\nTruck loaded");
            Console.WriteLine("No products left");
            Console.WriteLine($"Used capacity: {trucks.First().UsedCapacity}");
            Console.WriteLine($"Remaining capacity: {trucks.First().UnusedCapacity}");
            //truckLoaded = true;
            CheckAvailableUnits();
        }
    }
    // se não tiver espaço ele é mandado para a unidade
    else // protocolo 2 vai mudar as coisas aqui
    {
        Console.WriteLine("\nTruck fully loaded");
        Console.WriteLine($"Used capacity: {trucks.First().UsedCapacity}");
        Console.WriteLine($"Remaining capacity: {trucks.First().UnusedCapacity}");
        //truckLoaded = true;
        CheckAvailableUnits();
    }
}

// chamada por Load() depois do caminhão ficar cheio
// procura por unidades disponíveis
void CheckAvailableUnits()
{
    bool hasEnoughCapacity = false;
    int i = 0; // usado no loop foreach e tb depois quando for chamar 'SendToUnit(i)'

    // checa as capacidades das unidades para ver o que é suficiente
    foreach (var unit in availableUnits)
    {
        Console.WriteLine($"Unit capacity: {availableUnits[i].Capacity}");

        if (trucks.First().UsedCapacity <= availableUnits[i].Capacity)
        {
            hasEnoughCapacity = true;
            Console.WriteLine($"Unit has enough capacity");
            break;
        }

        Console.WriteLine($"Unit {availableUnits[i].Code} doesn't have enough capacity");
        i++;        
    }

    if (hasEnoughCapacity)
    {
        SendToUnit(i);
    }

    else
    {
        Console.WriteLine("Couldn't find an available unit");
        Console.WriteLine($"Available units left: {availableUnits.Count()}");

        // reseta a lista de unidades disponíveis e roda a função CheckAvailableUnits() mais uma vez
        availableUnits.Clear(); // garante que a lista realmente vai estar vazia
        availableUnits.AddRange(units);
        CheckAvailableUnits();
    }
}

// chamado por CheckAvailableUnits() se a unidade tiver capacidade suficiente pra descarregar
void SendToUnit(int unitIndex)
{  
    Console.WriteLine($"Truck of plate {trucks.First().Plate} will be sent to unit of code {availableUnits[unitIndex].Code}");

    checkLoadValue(); // checa se esse descarregamento foi o mais caro

    units[unitIndex].addLoad(trucks.First().UsedCapacity); // adciona a unidade o tanto que ela tá recebendo em kg

    checkTotalWeight(units[unitIndex]); // checa de essa mesma unidade é a que mais recebeu

    totalUnusedCapacity += trucks.First().UnusedCapacity;

    travelledDistance += availableUnits[unitIndex].Distance * 2; // x2 porque deve contar distancia de ida e volta

    Console.WriteLine($"Travelled distance: {travelledDistance}");

    availableUnits.RemoveAt(unitIndex);
    SendTruckToLast(); 
}

// manda o primeiro caminhao para o final da fila
void SendTruckToLast()
{
    Truck removedtruck = trucks[0];
    removedtruck.ResetStorage();
    removedtruck.ResetValue();
    trucks.RemoveAt(0);
    trucks.Add(removedtruck);
    if (remainingProducts.Count > 0)
    {
        Console.WriteLine($"There still are {remainingProducts.Count()} products left");
        // isso vai voltar a fazer o while lá de cima voltar a loopar e vai repetir todo esse processo até ficar sem productos
        truckLoaded = false;
    }
}

// chamado por 'SendToUnit(int unitIndex)', guarda o caminhão mais valioso
void checkLoadValue()
{
    if (trucks.First().TotalValue > mostValuableLoad)
    {
        mostValuableLoad = trucks.First().TotalValue;
        mostValuableTruck = trucks.First();
    }
}

// chamado por 'SendToUnit(int unitIndex)', guarda a unidade que mais recebeu em kg
void checkTotalWeight(Unit unit)
{
    if (unit.TotalLoadReceived > highestTotalWeight)
    {
        heaviestUnit = unit;
    }
}

Console.WriteLine($"\n1 - Placa do caminhão que fez a carga de maior valor: {mostValuableTruck.Plate}");
Console.WriteLine($"2 - Unidade que recebeu maior qtd em kg: {heaviestUnit.Code}");
Console.WriteLine($"3 - Quilometros percorridos de ida e volta: {travelledDistance}");
Console.WriteLine($"4 - Quilos de capacidade não utilizados: {totalUnusedCapacity}");