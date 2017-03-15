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

            var list = db.GetTelefons(7);

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("Number is {0}",list[i].Nummer);
            }

            Console.WriteLine("Home number is " + db.GetHomeTelefon(7).Nummer);

            PersonModel person = db.GetPerson(1);
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Person information for person with Id = 1:");
            Console.WriteLine("AdresseId: " + person.AdresseId + ". Efternavn: " + person.Efternavn + ". Fornavn: " + person.Fornavn + ". Mellemnavn: " + person.Mellemnavn + ". PersonId: " + person.PersonId + ". Type: " + person.Type);
            Console.WriteLine("----------------------------------------");


            
            var listad = db.GetAdresses();

            
            
            foreach (var item in listad)
            {
                Console.WriteLine(item.ToString());
            }
            
        }
    }
}
