using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containers
{
    public class Ship
    {
       public  readonly int MAXWEIGHTABOVE = 120000;
        public  readonly int C_WIDTH = 10, C_HEIGHT = 10;
        int maxWeight;
        int wAmount, hAmount;
        List<Container>[,] leftSide;
        List<Container>[,] rightSide;

        public Ship(int maxWeight, int sideWidth, int sideHeight)
        {
            this.maxWeight = maxWeight;
            wAmount = (sideWidth / C_WIDTH);
            hAmount = (sideHeight / C_HEIGHT);
            leftSide = new List<Container>[wAmount, hAmount];
            rightSide = new List<Container>[wAmount, hAmount];
            for (int i = 0; i < wAmount; i++)
            {
                for (int j = 0; j < hAmount; j++)
                {
                    leftSide[i, j] = new List<Container>();
                    rightSide[i, j] = new List<Container>();
                }
            }
        }
        public int sideWeight(Side side)
        {
            int sum = 0;
            for (int i = 0; i < wAmount; i++)
            {
                for (int j = 0; j < hAmount; j++)
                {
                    if (side == Side.RIGHT)
                    {
                        sum += rightSide[i, j].Sum(x => x.TotalWeight);
                    }
                    else
                    {
                        sum += leftSide[i, j].Sum(x => x.TotalWeight);
                    }
                }
            }
            return sum;
        }

        public bool isSafe()
        {
            return (totalWeight() >= maxWeight / 2);
        }
        Side getBestSideToLoad(Container c)
        {
            if ((sideWeight(Side.RIGHT) + c.TotalWeight) - sideWeight(Side.LEFT) < totalWeight() * 0.2)
            {
                return Side.RIGHT;
            }
            else return Side.LEFT;

        }
        public void loadContainers(List<Container> containers)
        {
            Console.WriteLine("Loading process started.");
            List<Container> loaded = new List<Container>();
            containers.Sort();
            for (int i = 0; i < wAmount; i++)
            {
                for (int j = 0; j < hAmount; j++)
                {
                    foreach (Container container in containers)
                    {
                        Side loadSide = getBestSideToLoad(container);
                        if (canLoad(container, loadSide, i, j))
                        {
                            if (loadSide == Side.RIGHT)
                            {
                                if (rightSide[i, j].Count > 0)
                                {
                                    rightSide[i, j][rightSide[i, j].Count - 1].containerAbove = container;
                                }
                                if (container.IsCold)
                                {

                                    rightSide[0, j].Add(container);
                                }
                                else
                                {
                                    rightSide[i, j].Add(container);
                                }
                            }
                            else
                            {
                                if (leftSide[i, j].Count > 0)
                                {
                                    leftSide[i, j][leftSide[i, j].Count - 1].containerAbove = container;
                                }
                                if (container.IsCold)
                                {

                                    leftSide[0, j].Add(container);
                                }
                                else
                                {
                                    leftSide[i, j].Add(container);
                                }
                            }
                            loaded.Add(container);
                        }
                    }
                    foreach (Container rContainer in loaded)
                    {
                        containers.Remove(rContainer);
                    }
                }
            }

            Console.WriteLine("Loading process completed. " + (loaded.Count) + " Containers loaded.");
            if (containers.Count > 0)
            {
                Console.WriteLine("Containers below could not be loaded because of some reasos.");
                foreach (Container container in containers)
                {
                    container.Print();
                    Console.WriteLine("---------------");
                }
            }
        }

        public bool canLoad(Container container, Side side, int i, int j)
        {
            if (totalWeight() + container.TotalWeight > maxWeight)
            {
                return false;
            }
            if (side == Side.RIGHT)
            {
                foreach (Container c in rightSide[i, j])
                {
                    if (c.canCarry(container,this) == false)
                    {
                        return false;
                    }
                }
            }
            else
            {
                foreach (Container c in rightSide[i, j])
                {
                    if (c.canCarry(container,this) == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public int totalWeight()
        {
            return sideWeight(Side.LEFT) + sideWeight(Side.RIGHT);
        }
        public void Print()
        {
            Console.WriteLine("--SHIP--");
            Console.WriteLine("Max Carryable Weight: " + maxWeight + " kgs");
            Console.WriteLine("Current Weight(Carrying): " + totalWeight() + " kgs");
            Console.WriteLine("Size of the ship: {0} X {1} Meters Each Side", wAmount * C_WIDTH, hAmount * C_HEIGHT);
            Console.WriteLine("Size of each container: {0} X {1} Meters Each", C_WIDTH, C_HEIGHT);
            Console.WriteLine("Left Side of the ship:\n-------------------");
            for (int i = 0; i < wAmount; i++)
            {
                for (int j = 0; j < hAmount; j++)
                {
                    Console.WriteLine("Position: {0} X {1}", i, j);
                    if (leftSide[i, j].Count > 0)
                    {
                        foreach (Container container in leftSide[i, j])
                        {
                            container.Print();
                            Console.WriteLine("-------------------");
                        }
                    }
                    else Console.Write("---EMPTY---");
                }
            }
            Console.WriteLine("Right Side of the ship:\n-------------------");
            for (int i = 0; i < wAmount; i++)
            {
                for (int j = 0; j < hAmount; j++)
                {
                    Console.WriteLine("Position: {0} X {1}", i, j);
                    if (rightSide[i, j].Count > 0)
                    {
                        foreach (Container container in rightSide[i, j])
                        {
                            container.Print();
                            Console.WriteLine("-------------------");
                        }
                    }
                    else Console.Write("---EMPTY---");
                }
            }
            Console.WriteLine("\nShip is " + (isSafe() ? "safe." : "not safe! Load or unload containers!") + "\n");
        }
    }
}

