﻿using Logistica;
using System.Collections.Generic;

// paths dos arquivos
string caminhoesPath = "data/caminhoes.txt"; // plate, capacity
string produtosPath = "data/produtos.txt"; // value, weight
string unidadesPath = "data/unidades.txt"; // code, distance, capacity

// listas geradas a partir dos arquivos
List<Truck> trucks = new List<Truck>(); // lista de caminhoes -> mover o caminhão para o final
List<Product> products = new List<Product>(); // fila de produtos -> para poder remover o produto da fila
List<Unit> units = new List<Unit>(); // lista de unidades -> não vai ser necessário mexer na ordem delas ou retirar unidades da lista

// outras listas
List<Product> remainingProducts = new List<Product>();
List<Unit> availableUnits = new List<Unit>();

// dados medidos para responder as perguntas de 1 a 4
int travelledDistance = 0;

Truck mostValuableTruck = null; // caminhão que fez a carga de maior valor (será usado a placa dele depois)
int mostValuableLoad = 0; // usado pra definir o caminhão mais valioso (o de cima)

Unit heaviestUnit = null; // unidade que recebeu maior tanto em kg no total
int highestTotalWeight = 0; // usado pra definir o de cima

int totalUnusedCapacity = 0; // guarda quanto de espaço não foi usado depois de cada viagem (apenas a ida é contabilizada, a volta é ignorada)

float averageUnitDistance = 0f; // média das distancias das unidades (protocolo 3)
float averageTruckCapacity = 0f; // média das capacidades dos caminhoes (protocolo 3)

// lê dados dos arquivos de texto, cria objetos, adiciona da lista
trucks.AddRange(FileReader.GenerateTrucks(caminhoesPath));
products.AddRange(FileReader.GenerateProducts(produtosPath));
units.AddRange(FileReader.GenerateUnits(unidadesPath));

// leito genérico de arquivos (não deu certo)
//FileReader.ReadAndCreateObjects(produtosPath, 2, typeof(Product));

// cria cópia das listas pra não precisar mexer nas originais
// ex.: a lista de unidades precisa retornar ao seu "estado original"
remainingProducts.AddRange(products);
availableUnits.AddRange(units);

// chama as funcoes responsaveis por caulcular a media de capacidade e distancia
// esses 2 parametros sao usados no protocolo 3
averageTruckCapacity = CalculateAverage.Capacity(trucks);
averageUnitDistance = CalculateAverage.Distance(units);

bool isProductListEmpty = false;
while (!isProductListEmpty) // responvel pelo loop de colocar os produtos nos caminhões
{
    if (remainingProducts.Count() != 0)
    {
        Load();     
    }
    else
    {
        isProductListEmpty = true; // faz o while parar
        Console.WriteLine("\nNo products left");
    }
}

// função usada para carregar os produtos nos caminhões
// depois chama SendToUnit(), que manda esses caminhões para as unidades
void Load() // essa função muda um pouco no protocolo 2
{
    bool productLoaded = false;
    int productIndex = 0;

    while (!productLoaded)
    {
        // se o caminhão tiver espaço, ele carrega o item
        if (trucks.First().Load(remainingProducts.ElementAt(productIndex)))
        {
            remainingProducts.RemoveAt(productIndex); // tira o produto da lista

            if (remainingProducts.Count() == 0)
            {
                productLoaded = true; // faz o while parar de rodar
                Console.WriteLine("No products left");
            }
        }
        // se não tiver espaço ele checa o proximo
        else
        {
            if (productIndex < remainingProducts.Count() - 1) // se não for o último produto
            {
                productIndex++; // vai tentar pegar o próximo da lista quando loopar de volta
            }
            else // se for o ultimo
            {
                productLoaded = true; // faz o while parar de rodar
                Console.WriteLine("\nTruck loaded");
            }
        }
    }

    // só é executado quando o while acabar
    // printa algumas infos importantes
    // chama a função que procura por unidades
    Console.WriteLine("\nTruck loaded");
    Console.WriteLine($"Used capacity: {trucks.First().UsedCapacity}");
    Console.WriteLine($"Remaining capacity: {trucks.First().RemainingCapacity}");
    CheckAvailableUnits();
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

        if (trucks.First().UsedCapacity <= availableUnits[i].Capacity) // checar aqui as medias
        {
            //  checa media de capacidade e de distancia
            if (trucks.First().Capacity >= averageTruckCapacity && availableUnits[i].Distance >= averageUnitDistance)
            {
                Console.WriteLine($"Both are above average: {trucks.First().Capacity} - {availableUnits[i].Distance}");
                hasEnoughCapacity = true;
                break;
            }
            else if (trucks.First().Capacity < averageTruckCapacity && availableUnits[i].Distance < averageUnitDistance)
            {
                Console.WriteLine($"Both are below average: {trucks.First().Capacity} - {availableUnits[i].Distance}");
                hasEnoughCapacity = true;
                break;
            }
            else
            {
                Console.WriteLine($"Wrong values: {trucks.First().Capacity} - {availableUnits[i].Distance}");
            }
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

    totalUnusedCapacity += trucks.First().RemainingCapacity;

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
        isProductListEmpty = false;
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