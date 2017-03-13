using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary
{
    class DataModel
    {
    }

    public class PersonModel
    {
    }
    public class TelefonModel
    {
    }
    public class AdresseModel
    {
    }

    public class HarAdresseModel
    {
    }

    class PersonJoin
    {
        private PersonModel person;
        private AdresseModel adresse;
        private TelefonModel telefon;
        void PesonJoin(PersonModel p, AdresseModel a, TelefonModel t)
        {
            this.person = p;
            this.telefon = t;
            this.adresse = a;
        }

    }


    public class KartotekModel
    {
        private List<PersonModel> PersonerFelt;
        private List<TelefonModel> TelefonerFelt;
        private List<AdresseModel> AdresserFelt;
        private List<PersonJoin> SamletFelt;

        internal protected int x; //Access modfiers are "Or'ed"

        public List<PersonModel> MyProperty { get; set; }
        public List<TelefonModel> Telefoner
        {
            get { return TelefonerFelt; }
            set { TelefonerFelt = value; }
        }


        public List<PersonModel> Personer
        {
            get { return PersonerFelt; }
            set { PersonerFelt = value; }
        }


    }

}



