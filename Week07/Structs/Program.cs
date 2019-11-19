using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structs
{
    public abstract class Shape
    {
        protected string Name { get; set; }
        protected Shape(string name)
        {
            Name = name;
            Console.WriteLine("Shape");
        }
    }

    public class Circle : Shape
    {
        public Circle(string name) : base(name)
        {
            Console.WriteLine("Circle");
        }
    }

    public class SuperCircle : Circle
    {
        public SuperCircle(string name) : base(name)
        {
            Console.WriteLine("SuperCircle");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var circle = new SuperCircle("circle 1");
        }
    }
}
