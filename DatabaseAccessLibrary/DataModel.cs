﻿using System;
using System.Collections.Generic;
using System.Data;
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

        public override string ToString()
        {
            return "AdresseId: " + AdresseId + ". Efternavn: " + Efternavn + ". Fornavn: " + Fornavn + ". Mellemnavn: " + Mellemnavn + ". PersonId: " + PersonId + ". Type: " + Type;
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

        public override string ToString()
        {
            return "TelefonId: " + TelefonId + ". Nummer: " + Nummer + ". Type: " + Type + ". PersonId: " + PersonnId;
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

        public override string ToString()
        {
            return "adresseId : " + AdresseId + " Vej : " + Vejnavn + " husnr : " + HusNummer + " postnr : " +
                   PostNummer + " by : " + Bynavn + " Type : " + Type;
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
        public PersonModel person { get; set; }
        public List<AdresseModel> adresseList { get; set; }
        public List<TelefonModel> telefonList { get; set; }
        public PersonJoin(PersonModel p, List<AdresseModel> a, List<TelefonModel> t)
        {
            this.person = p;
            this.telefonList = t;
            this.adresseList = a;
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



