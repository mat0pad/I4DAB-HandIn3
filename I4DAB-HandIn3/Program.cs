using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseAccessLibrary;

namespace I4DAB_HandIn3
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseUtil db = new DatabaseUtil();
            db.Setup();

            Console.WriteLine("There are {0} persons in db", db.GetNumberOfRecords());

            var list = db.GetTelefons(7, true);

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("Number is {0}",list[i].Nummer);
            }

            Console.WriteLine("Home number is " + db.GetHomeTelefon(7).Nummer);

        }
    }
}
