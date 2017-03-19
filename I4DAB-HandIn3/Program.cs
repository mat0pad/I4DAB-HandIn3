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

            Console.WriteLine("There are {0} persons in total\n", db.GetNumberOfRecords(DatabaseUtil.TABLE.Person));

            PersonModel person1 = db.GetPerson(7);

            Console.WriteLine(person1 + "\n" + person1.Fornavn + " " + person1.Mellemnavn + " " + person1.Efternavn + " has these phone numbers:");

            var list = db.GetTelefons(7);

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("Nr: {0}",list[i].Nummer);
            }

            Console.WriteLine("Home number is " + db.GetHomeTelefon(7).Nummer);

            
            var personAllData = db.GetAllRecordsForPerson(4);

            Console.WriteLine("\n" + personAllData.person + "\nAdresser");

            foreach (var item in personAllData.adresseList)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine(item.Vejnavn + ", " + item.HusNummer + ", " + item.Bynavn);
                Console.WriteLine("-----------------");
            }

            foreach (var item in personAllData.telefonList)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("Nr: " + item.Nummer + ", type: " + item.Type);
                Console.WriteLine("-----------------");
            }



           db.SetPersonName("Antonine", 2);
           var personOLd = db.GetPerson(2);
           Console.WriteLine("\n* * Insert test * *\nFornavn: " + personOLd.Fornavn + ", personId: " + personOLd.PersonId);

           db.SetPersonName("Anton", 2);
           var personNew = db.GetPerson(2);
           Console.WriteLine("Fornavn: " + personNew.Fornavn + ", personId: " + personNew.PersonId);

            //db.GetAllAdressesForPerson(1);
            // db.DeletePerson(3);
            //db.InsertPerson(11, "hej", "hej","", "test", 2);
        }
    }
}
