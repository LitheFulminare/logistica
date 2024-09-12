using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistica
{
    internal class Product
    {
        // dados extraidos do arquivo 'data/produtos.txt'
        private int _cost;
        private int _weight;

        public Product(int cost, int weight)
        {
            this._cost = cost;
            this._weight = weight;
        }

        public int GetCost() { return _cost; }
        public int GetWeight() { return _weight; }

        // agora é usado pra debug, talvez não tenha uso mais tarde
        public override string ToString()
        {
            return $"Product Cost: {_cost}, Weight: {_weight}";
        }
    }
}
