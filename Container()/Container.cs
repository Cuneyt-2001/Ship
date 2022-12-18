using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containers
{
 
       public class Container : IComparable
        {
            int totalWeight;
            int maxWeight;
            bool isValueable;
            bool isCold;
            public Container containerAbove;

            public Container(int weight, bool isValuable, bool isCold)
            {
                maxWeight = 30000;
                if (totalWeight + 4000 + weight <= MaxWeight)
                {
                    totalWeight = 4000 + weight;
                }
                else totalWeight = MaxWeight;
                this.IsValueable = isValuable;
                this.isCold = isCold;
                containerAbove = null;
            }
            public int CompareTo(object obj)
            {
                Container other = obj as Container;
                if (this.isValueable && !other.isValueable)
                {
                    return 1;
                }
                else if (!this.isValueable && other.isValueable)
                {
                    return -1;
                }
                if (TotalWeight < other.TotalWeight)
                {
                    return 1;
                }
                else return -1;
            }

            public bool canCarry(Container other,Ship s)
            {
                if (isValueable || (this.isCold != other.isCold))
                {
                    return false;
                }
                int totalW = 0;
                Container temp = other.containerAbove;
                while (temp != null)
                {
                    totalW += temp.TotalWeight;
                    temp = temp.containerAbove;
                }
                return totalW + other.TotalWeight <= s.MAXWEIGHTABOVE;
            }

            public bool canLoad(int weight)
            {
                return totalWeight + weight <= maxWeight;
            }
            public void Print()
            {
                Console.WriteLine("Container Info\nWeight: " + totalWeight + " kgs\nIs Valuable: " + (isValueable ? "Yes" : "No") + "\nIs Cold: " + (isCold ? "Yes" : "No"));
            }

            public int TotalWeight { get => totalWeight; set => totalWeight = value; }
            public int MaxWeight { get => maxWeight; set => maxWeight = value; }
            public bool IsValueable { get => isValueable; set => isValueable = value; }
            public bool IsCold { get => isCold; set => isCold = value; }
        }
    }

