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
        private int _weight;
        private int _value;

        public Product(int weight, int value)
        {
            this._weight = weight;
            this._value = value;
        }

        public int GetWeight() { return _weight; }
        public int GetValue() { return _value; }


        // agora é usado pra debug, talvez não tenha uso mais tarde
        public override string ToString()
        {
            return $"Product Weight: {_weight}, Value: {_value}";
        }
    }
}
