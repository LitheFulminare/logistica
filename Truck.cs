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

        private int _usedCapacity = 0;
        private int _unusedCapacity = 0;
        private int _productCount = 0; // protocolo 4 exige um máximo de 50 produtos no caminhão
        private int _profit = 0; // reseta pra 0 depois de descarregar

        public Truck(string plate, int capacity)
        {
            this._plate = plate;
            this._capacity = capacity;
        }

        // propriedades
        public string Plate => _plate;
        public int Capacity => _capacity;

        // agora é usado pra debug, talvez não tenha uso mais tarde
        public override string ToString()
        {
            return $"Truck Plate: {_plate}, Capacity: {_capacity}";
        }

        public void Load(Product product)
        {
            loadedProducts.Add(product);
        }
    }
}
