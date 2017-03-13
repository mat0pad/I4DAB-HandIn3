using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary
{

    public class PersonModel
    {
        public int PersonId { get; set; }
        public int AdresseId { get; set; }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public string Mellemnavn { get; set; }
        public string Type { get; set; }

        public PersonModel(int personId, string fornavn, string mellemnavn, string efternavn, string type, int adresseId)
        {
            AdresseId = adresseId;
            PersonId = personId;
            Fornavn = fornavn;
            Mellemnavn = mellemnavn;
            Type = type;
            Efternavn = efternavn;
        }
}
    public class TelefonModel
    {
        public int TelefonId { get; set; }
        public int PersonnId { get; set; }
        public string Nummer { get; set; }
        public string Type { get; set; }

        public TelefonModel(int telefonId, string type, string nr, int personId)
        {
            TelefonId = telefonId;
            PersonnId = personId;
            Nummer = nr;
            Type = type;
        }
    }
    public class AdresseModel
    {
        public int AdresseId { get; set; }
        public int HusNummer { get; set; }
        public int PostNummer { get; set; }
        public string Vejnavn { get; set; }
        public string Bynavn { get; set; }
        public string Type { get; set; }

        public AdresseModel(int adresseId, string vej, string by, int husnr, int postnr, string type)
        {
            AdresseId = adresseId;
            Vejnavn = vej;
            Bynavn = by;
            HusNummer = husnr;
            Type = type;
            PostNummer = postnr;
        }

    }

    public class HarAdresseModel
    {
        public int PersonId { get; set; }
        public int AdresseId { get; set; }

        public HarAdresseModel(int personId, int adresseid)
        {
            PersonId = personId;
            AdresseId = adresseid;
        }
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



