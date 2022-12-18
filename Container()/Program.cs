using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


namespace Containers
{

    public class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            List<Container> containers = new List<Container>();
            int containerCount = 50;
            for (int i = 0; i < containerCount; i++)
            {
                containers.Add(new Container(rnd.Next(10000, 26000), (rnd.Next(0, 2) == 1), (rnd.Next(0, 2) == 1)));
            }
            Ship ship = new Ship(1500000, 30, 50);
            ship.loadContainers(containers);
            ship.Print();
            Console.ReadLine();
        }




    }
}
