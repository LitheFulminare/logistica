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
        private int _totalUnusedCapacity = 0;
        private int _totalValue = 0; // reseta pra 0 depois de descarregar

        public Truck(string plate, int capacity)
        {
            this._plate = plate;
            this._capacity = capacity;
            ResetStorage(); // faz 'remainingCapacity' ser igual a 'capacity'
        }

        // reseta a capaciade usada para seu valor original
        // chamado da primeira vez que é construído e quando volta para o final da fila
        public void ResetStorage()
        {
            _remainingCapacity = _capacity;
        }
        public void ResetTotalValue()
        {
            _totalValue = 0;
        }
        public void ResedUnused()
        {
            _totalUnusedCapacity = 0;
        }

        // chamado por Load()
        // se tiver espaço, adciona o produto a lista e retorna 'true' pro 'Program' saber q deu certo
        // se não, retorna false
        public bool Load_old(Product product)
        {
            if (_remainingCapacity > product.Weight)
            {
                // adciona o produto a lista, contabiliza seu peso e valor
                _remainingCapacity -= product.Weight;
                _totalValue += product.Value;
                loadedProducts.Add(product);

                return true;
            }
            else // retorna false para o Program saber que não tem mais espaço
            {
                _totalUnusedCapacity += _remainingCapacity; // vai guardando quantos kgs ficaram livres depois de cada viagem
                return false;
            }

        }

        public bool Load(int weight, int value)
        {
            if (_remainingCapacity > weight)
            {
                // contabiliza seu peso e valor
                _remainingCapacity -= weight;
                _totalValue += value;
                //loadedProducts.Add(product);

                return true;
            }
            else // retorna false para o Program saber que não tem mais espaço
            {
                _totalUnusedCapacity += _remainingCapacity; // vai guardando quantos kgs ficaram livres depois de cada viagem
                return false;
            }
        }


        // propriedades
        public string Plate => _plate;
        public int Capacity => _capacity;
        public int UsedCapacity => _capacity - _remainingCapacity;
        public int RemainingCapacity => _remainingCapacity; // reseta depois de cada viagem
        public int TotalValue => _totalValue;

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
