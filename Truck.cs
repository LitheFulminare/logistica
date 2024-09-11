using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistica
{
    internal class Truck
    {
        private string _plate;
        private int _capacity;

        public Truck(string plate, int capacity)
        {
            this._plate = plate;
            this._capacity = capacity;
        }

        public string GetPlate() { return _plate; }
        public int GetCapacity() { return _capacity; }

        public override string ToString()
        {
            return $"Truck Plate: {_plate}, Capacity: {_capacity}";
        }
    }
}
