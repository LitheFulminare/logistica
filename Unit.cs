using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistica
{
    internal class Unit
    {
        // dados extraidos do arquivo 'data/unidades.txt' 
        private string _code;
        private int _distance;
        private int _capacity;

        public Unit(string code, int _distance, int capacity)
        {
            this._code = code;
            this._distance = _distance;
            this._capacity = capacity;
        }

        // propriedades
        public string Code => _code;
        public int Distance => _distance;
        public int Capacity => _capacity;

        // agora é usado pra debug, talvez não tenha uso mais tarde
        public override string ToString()
        {
            return $"Unit code: {_code}, Distance: {_distance}, Capacity: {_capacity}";
        }
    }
}
