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

        public Truck(string plate, int capacity)
        {
            this._plate = plate;
            this._capacity = capacity;
        }

        public string GetPlate() { return _plate; }
        public int GetCapacity() { return _capacity; }

        // agora é usado pra debug, talvez não tenha uso mais tarde
        public override string ToString()
        {
            return $"Truck Plate: {_plate}, Capacity: {_capacity}";
        }
    }
}
