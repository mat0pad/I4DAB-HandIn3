using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4DAB_HandIn3
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseUtil db = new DatabaseUtil();

            db.Setup();

            Console.WriteLine("There are {0} persons in db", db.GetNumberOfRecords());
            Console.WriteLine("{0}", db.GetPersons());
        }
    }
}
