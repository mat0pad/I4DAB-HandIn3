﻿using System;
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

        public AdresseModel(int adresseId,string vej, int husnr, int postnr, string by, string type)
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

    public class PersonJoin
    {
        private PersonModel person;
        private AdresseModel adresse;
        private TelefonModel telefon;
        public PersonJoin(PersonModel p, AdresseModel a, TelefonModel t)
        {
            this.person = p;
            this.telefon = t;
            this.adresse = a;
        }

    }

    public class KartotekModel
    {
        public List<PersonJoin> SamletFelt { get; set; }

        public KartotekModel(List<PersonJoin> all)
        {
            SamletFelt = all;
        }

    }

}



