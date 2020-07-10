using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrdmDB;

namespace ConsoleTestPrdmDB
{
    class Program
    {
        static void Main(string[] args)
        {
            PrdmDB.Users User = new PrdmDB.Users();
            User.FindByLogin("1866");

            Console.WriteLine( User.Name );
            Console.ReadLine();
        }

    }
}
