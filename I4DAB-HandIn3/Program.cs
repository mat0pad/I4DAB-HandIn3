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

            Console.WriteLine("They are:");

            var personList = db.GetPersons();

            if (personList != null)
                for (int i = 0; i < personList.Count; i++)
                    Console.WriteLine("\r{0}\t | {1}\t | {2}\t | {3}\t", personList[i].Fornavn, personList[i].Mellemnavn, personList[i].Efternavn, personList[i].Type);
            
        
            var list = db.GetTelefons(7, true);

            if(list != null) { 

                for (int i = 0; i < list.Count; i++)
                    Console.WriteLine("Number is {0}",list[i].Nummer);
            }

            Console.WriteLine("Home number is " + db.GetHomeTelefon(7).Nummer);

        }
    }
}
