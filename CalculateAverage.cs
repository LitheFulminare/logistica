using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistica
{
    internal class CalculateAverage
    {
        // realiza loops e calculos simples de media aritmética

        public static float Capacity(List<Truck> trucks)
        {
            int totalCapacity = 0;
            foreach (var truck in trucks) { totalCapacity += truck.Capacity; }
            return totalCapacity / trucks.Count();
        }

        public static float Distance(List<Unit> units)
        {
            int totalDistance = 0;
            foreach (var unit in units) { totalDistance += unit.Distance; }
            return totalDistance / units.Count();
        }
    }
}
