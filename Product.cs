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
        private int _value;
        private int _weight;

        public Product(int value, int weight)
        {
            this._value = value;
            this._weight = weight;
        }

        public int GetValue() { return _value; }
        public int GetWeight() { return _weight; }

        // agora é usado pra debug, talvez não tenha uso mais tarde
        public override string ToString()
        {
            return $"Product Value: {_value}, Weight: {_weight}";
        }
    }
}
