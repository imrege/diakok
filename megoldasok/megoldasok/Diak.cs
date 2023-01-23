using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace megoldasok
{
    class Diak
    {
        public string nev;
        public string osztaly;
        public string csoport;
        public char nem;
        public string hajszin;
        public int zsebpenz;
        public string kedvencTargy;
        public int bukasokSzama;
        public double atlag;
        public int testverekSzama;
        public string szulHely;
        public DateTime szulDatumIdo;
        public string lakcim;
        public DateTime anyjaSzul;
        public DateTime apjaSzul;
        public string allat1;
        public string allat2;

        public Diak(string s)
        {
            string[] t = s.Split(';');
            nev = t[0].Trim(); osztaly = t[1]; csoport = t[2];
            nem = t[3][0];
            hajszin = t[4];
            zsebpenz = int.Parse(t[5]);
            kedvencTargy = t[6];
            bukasokSzama = int.Parse(t[7]);
            atlag = double.Parse(t[8]);
            testverekSzama = int.Parse(t[9]);
            szulHely = t[10];
            int[] szD = t[11].Split('.').Select(a => int.Parse(a)).ToArray();
            int[] szI = t[12].Split(':').Select(a => int.Parse(a)).ToArray();
            szulDatumIdo = new DateTime(szD[0], szD[1], szD[2], szI[0], szI[1], szI[2]);
            lakcim = t[13];
            anyjaSzul = DateTime.Parse(t[14]);
            apjaSzul = DateTime.Parse(t[15]);
            allat1 = t[16]; allat2 = t[17];
        }

        public bool videki => lakcim.Substring(0, 4) != "8900";

        public int evfolyam => int.Parse(osztaly.Split('.')[0]);

        public bool informatikus => csoport == "rendszergazda" || csoport == "szoftverfejlesztő";

        public string lakohely
        {
            get
            {
                string[] t1 = lakcim.Split(',');
                string[] t2 = t1[0].Split(' ');
                return t2[1];
            }
        }

        public string korosztaly
        {
            get
            {
                if (evfolyam <= 10) return "alsóbbéves";
                if (evfolyam <= 12) return "felsőbbéves";
                return "felnőtt";
            }
        }

        public string minosites
        {
            get
            {
                if (atlag == 1) return "elégtelen";
                if (atlag < 2.75) return "elégséges";
                if (atlag < 3.75) return "közepes";
                if (atlag < 4.75) return "jó";
                if (atlag < 5) return "jeles";
                return "kitűnő";
            }
        }

        public string monogram
        {
            get
            {
                string[] t = nev.Split(' ');
                string mon = "";
                foreach (string s in t) mon += s[0] + ".";
                return mon;
            }
        }

        public bool realos
            => "matematika,fizika,informatika,kémia,földrajz,biológia".Split(',').Contains(kedvencTargy);

        public int eletkor
        {
            get
            {
                DateTime ma = DateTime.Today;
                int evek = ma.Year - szulDatumIdo.Year;
                DateTime ideiSzulinap;
                if (szulDatumIdo.Month == 2 && szulDatumIdo.Day == 29 && !DateTime.IsLeapYear(ma.Year))
                    ideiSzulinap = new DateTime(ma.Year, 2, 28);
                else ideiSzulinap = new DateTime(ma.Year, szulDatumIdo.Month, szulDatumIdo.Day);
                if (ideiSzulinap > ma) evek--;
                return evek;
            }
        }

        public int anyjaKoraSzulkor
        {
            get
            {
                DateTime ma = szulDatumIdo;
                int evek = ma.Year - anyjaSzul.Year;
                DateTime ideiSzulinap;
                if (anyjaSzul.Month == 2 && anyjaSzul.Day == 29 && !DateTime.IsLeapYear(ma.Year))
                    ideiSzulinap = new DateTime(ma.Year, 2, 28);
                else ideiSzulinap = new DateTime(ma.Year, anyjaSzul.Month, anyjaSzul.Day);
                if (ideiSzulinap > ma) evek--;
                return evek;
            }
        }

        public int apjaKoraSzulkor
        {
            get
            {
                DateTime ma = szulDatumIdo;
                int evek = ma.Year - apjaSzul.Year;
                DateTime ideiSzulinap;
                if (apjaSzul.Month == 2 && apjaSzul.Day == 29 && !DateTime.IsLeapYear(ma.Year))
                    ideiSzulinap = new DateTime(ma.Year, 2, 28);
                else ideiSzulinap = new DateTime(ma.Year, apjaSzul.Month, apjaSzul.Day);
                if (ideiSzulinap > ma) evek--;
                return evek;
            }
        }

        public int apaKora
        {
            get
            {
                DateTime ma = DateTime.Today;
                int evek = ma.Year - apjaSzul.Year;
                DateTime ideiSzulinap;
                if (apjaSzul.Month == 2 && apjaSzul.Day == 29 && !DateTime.IsLeapYear(ma.Year))
                    ideiSzulinap = new DateTime(ma.Year, 2, 28);
                else ideiSzulinap = new DateTime(ma.Year, apjaSzul.Month, apjaSzul.Day);
                if (ideiSzulinap > ma) evek--;
                return evek;
            }
        }

        public int anyaKora
        {
            get
            {
                DateTime ma = DateTime.Today;
                int evek = ma.Year - anyjaSzul.Year;
                DateTime ideiSzulinap;
                if (anyjaSzul.Month == 2 && anyjaSzul.Day == 29 && !DateTime.IsLeapYear(ma.Year))
                    ideiSzulinap = new DateTime(ma.Year, 2, 28);
                else ideiSzulinap = new DateTime(ma.Year, anyjaSzul.Month, anyjaSzul.Day);
                if (ideiSzulinap > ma) evek--;
                return evek;
            }
        }

        public string apjaKorosztaly
        {
            get
            {
                int a = apaKora / 5 * 5;
                int b = a + 4;
                return $"{a} - {b}";
            }
        }

        public int szulHonapSzam => szulDatumIdo.Month;

        public string szulHonapNev => szulDatumIdo.ToString("MMMM");

        public string szulNapNev => szulDatumIdo.ToString("dddd");

        public int hanyIlyenAllatVan(string allat)
        {
            int db = 0;
            if (allat1 == allat) db++;
            if (allat2 == allat) db++;
            return db;
        }

        public bool extremHaj => hajszin == "kék" || hajszin == "lila" || hajszin == "kopasz";

        public string hosszuSzulDatum => szulDatumIdo.ToString("yyyy. MMMM dd. dddd");

        public string szul => szulDatumIdo.ToString("yyyy.MM.dd.");

        public string szulHoNap => szulDatumIdo.ToString("MMMM dd.");
        public string szulHoNapSzam => szulDatumIdo.ToString("MM.dd");

        public string anySzul => anyjaSzul.ToString("yyyy.MM.dd.");

        public string apSzul => apjaSzul.ToString("yyyy.MM.dd.");

        public bool hetvegenSzul => szulNapNev == "szombat" || szulNapNev == "vasárnap";

        public bool teliUnnepek
        {
            get
            {
                if (szulDatumIdo.Month == 1 && szulDatumIdo.Day == 1) return true;
                if (szulDatumIdo.Month == 12)
                {
                    if (new List<int>(){ 24, 25, 26, 31}.Contains(szulDatumIdo.Day)) return true;
                }
                return false;
            }
        }

        public DateTime szulDatum
            => new DateTime(szulDatumIdo.Year, szulDatumIdo.Month, szulDatumIdo.Day);
    }
}
