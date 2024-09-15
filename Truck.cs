using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistica
{
    internal class Truck
    {
        // dados extraidos do arquivo 'data/caminhoes.txt'
        private string _plate;
        private int _capacity;

        // outros dados

        private List<Product> loadedProducts = new List<Product>(); // produtos que o caminhão está carregando no momento

        private int _remainingCapacity = 0;
        private int _unusedCapacity = 0;
        private int _productCount = 0; // protocolo 4 exige um máximo de 50 produtos no caminhão
        private int _profit = 0; // reseta pra 0 depois de descarregar

        public Truck(string plate, int capacity)
        {
            this._plate = plate;
            this._capacity = capacity;
            ResetCapacity(); // faz 'remainingCapacity' ser igual a 'capacity'
        }

        // reseta a capaciade usada para seu valor original
        // chamado da primeira vez que é construído e quando volta para o final da fila
        public void ResetCapacity()
        {
            _remainingCapacity = _capacity;
        }

        // chamado por Load()
        public bool Load(Product product, int productWeight)
        {
            if (_remainingCapacity > productWeight) 
            {
                // adciona o produto e contabiliza seu peso
                _remainingCapacity -= productWeight;
                loadedProducts.Add(product);

                Console.WriteLine("Product was successfully added");
                Console.WriteLine($"Remaining capacity: {_remainingCapacity}");
                return true;
            }
            else // retorna false para Program saber que não tem mais espaço
            {
                Console.WriteLine("Product wasn't added");
                Console.WriteLine($"Remaining capacity: {_remainingCapacity}");
                return false;               
            }
        }

        // propriedades
        public string Plate => _plate;
        public int Capacity => _capacity;

        // agora é usado pra debug, talvez não tenha uso mais tarde
        public override string ToString()
        {
            return $"Truck Plate: {_plate}, Capacity: {_capacity}";
        }

        public Product GetProduct(int index)
        {
            return loadedProducts[index];
        }
    }
}
