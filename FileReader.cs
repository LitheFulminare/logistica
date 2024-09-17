using Logistica;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Logistica
{
    internal class FileReader
    {
        public static List<Truck> GenerateTrucks(string filepath)
        {
            List<Truck> trucks = new List<Truck>();

            using (StreamReader reader = new StreamReader(filepath))
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
            return trucks;
        }

        public static List<Product> GenerateProducts(string filepath)
        {
            List<Product> products = new List<Product>();

            using (StreamReader reader = new StreamReader(filepath))
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
                    products.Add(product);
                }
            }
            return products;
        }

        public static List<Unit> GenerateUnits(string filepath)
        {
            List<Unit> units = new List<Unit>();

            using (StreamReader reader = new StreamReader(filepath))
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
            }
            return units;
        }
    }
}
