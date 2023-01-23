using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace megoldasok
{
    class Program
    {

        List<Diak> li = new List<Diak>();

        void felolvas()
        {
            string[] t = File.ReadAllLines("diákok.csv");
            foreach (string s in t.Skip(1))
            {
                //Console.WriteLine(s);
                li.Add(new Diak(s));
            }
        }

        #region A_sorozat

        void a_1()
        {
            Console.WriteLine("a1: Akik több, mint 3 tárgyból buktak");
            foreach (Diak di in li.Where(a => a.bukasokSzama > 3).OrderBy(a => a.nev))
                Console.WriteLine($"\t{di.nev}, {di.osztaly}");
        }

        void a_2()
        {
            Console.WriteLine("a2: Éneket vagy rajzot kedvelő fiúk");
            var okAzok = li
                .Where(a => a.nem == 'f')
                .Where(a => a.kedvencTargy == "ének" || a.kedvencTargy == "rajz")
                .OrderBy(a => a.nev);
            foreach (Diak di in okAzok)
                Console.WriteLine($"\t{di.nev}, {di.osztaly}, {di.csoport} ({di.kedvencTargy})");
        }

        void a_3()
        {
            Console.WriteLine("a3: Lányok, akik több, mint 8000 Ft zsebpénzt kapnak");
            var okAzok = li
                .Where(a => a.nem == 'l')
                .Where(a => a.zsebpenz > 8000)
                .OrderBy(a => a.nev);
            foreach (Diak di in okAzok)
                Console.WriteLine($"\t{di.nev}, {di.osztaly}, {di.zsebpenz:c0}");
        }

        void a_4()
        {
            Console.WriteLine("a4: A vidékiek");
            var okAzok = li
                .Where(a => a.videki)
                .OrderBy(a => a.nev);
            foreach (Diak di in okAzok)
                Console.WriteLine($"\t{di.nev}: {di.lakcim}");
        }

        void a_5()
        {
            Console.WriteLine("a5: Az átlagnál több zsebpénzt kapó fiúk");
            double fiuAtlagPenz = li.Where(a => a.nem == 'f').Average(a => a.zsebpenz);
            var okAzok = li
                .Where(a => a.nem == 'f')
                .Where(a => a.zsebpenz > fiuAtlagPenz)
                .OrderBy(a => a.nev);
            Console.WriteLine($"a fiú átlag zsebpénz: {fiuAtlagPenz:c0}");
            foreach (Diak di in okAzok)
                Console.WriteLine($"\t{di.nev}: {di.osztaly}, {di.zsebpenz:c0}");
        }

        void a_6()
        {
            Console.WriteLine("a6: Legalább 4-es átlagú, rajzot kedvelő lányok");
            var okAzok = li
                .Where(a => a.nem == 'l' && a.atlag >= 4 && a.kedvencTargy == "rajz")
                .OrderBy(a => a.nev);
            foreach (Diak di in okAzok)
                Console.WriteLine($"\t{di.nev}: {di.osztaly}, {di.atlag}");
        }

        void a_7()
        {
            Console.WriteLine("a7: A bukott informatikusok");
            var okAzok = li
                .Where(a => a.informatikus)
                .Where(a => a.bukasokSzama > 0)
                .OrderBy(a => a.nev);
            foreach (Diak di in okAzok)
                Console.WriteLine($"\t{di.nev}, {di.osztaly} ({di.csoport}) - {di.bukasokSzama} bukása volt");
        }

        void a_8()
        {
            Console.WriteLine("a8: Fiúk-lányok tanulmányi átlaga");
            double fiuAtlag = li.Where(a => a.nem == 'f').Average(a => a.atlag);
            double lanyAtlag = li.Where(a => a.nem == 'l').Average(a => a.atlag);
            Console.WriteLine($"lányok átlaga: {lanyAtlag:n2}; fiúk átlaga: {fiuAtlag:n2}");
        }

        void a_9()
        {
            Console.WriteLine("a8: A legnagyobb zsebpénz, amit fiú kap");
            int maxZsebpenz = li.Where(a => a.nem == 'f').Max(a => a.zsebpenz);
            var okAzok = li
                .Where(a => a.nem == 'f' && a.zsebpenz == maxZsebpenz)
                .OrderBy(a => a.nev);
            Console.WriteLine($"a legnagyobb zsebpénz: {maxZsebpenz}");
            foreach (Diak di in okAzok)
                Console.WriteLine($"\t{di.nev}, {di.osztaly}");
        }

        void a_10()
        {
            Console.WriteLine("a10: Az egyes évfolyamok létszáma");
            var evfolyamok = li
                .GroupBy(a => a.evfolyam)
                .Select(a => new
                {
                    evfolyam = a.Key,
                    letszam = a.Count()
                })
                .OrderBy(a => a.evfolyam);
            foreach (var evf in evfolyamok)
                Console.WriteLine($"\t{evf.evfolyam,2}: {evf.letszam} fő");
        }

        void a_11()
        {
            int videkiekSzama = li.Count(a => a.videki);
            double arany = 100.0 * videkiekSzama / li.Count;
            Console.WriteLine($"a11: A diákok {arany:n2}%-a vidéki");
        }

        void a_12()
        {
            Console.WriteLine("a12: 10.C-sek, akik az osztályátlagnál kevesebb zsebpénzt kapnak");
            double atl10c = li.Where(a => a.osztaly == "10.C").Average(a => a.zsebpenz);
            Console.WriteLine($"A 10.c-sek átlagos zsebpénze: {atl10c:c0}");
            var okAzok = li
                .Where(a => a.osztaly == "10.C" && a.zsebpenz < atl10c)
                .OrderBy(a => a.nev);
            foreach (Diak di in okAzok)
                Console.WriteLine($"\t{di.nev}, {di.zsebpenz:c0}");
        }

        void a_13()
        {
            string oszt = li.Find(a => a.nev.StartsWith("Agnes")).osztaly;
            int db = li.Count(a => a.osztaly == oszt) - 1;
            Console.WriteLine($"a13: A svéd Agnes osztálya: {oszt}; {db} osztálytársa van");
        }

        void a_14()
        {
            Console.WriteLine("a14: A legjobb-legrosszabb átlagú lányok közötti eltérés");
            double minAtlag = li
                .Where(a => a.nem == 'l' && a.bukasokSzama == 0)
                .Min(a => a.atlag);
            double maxAtlag = li
                .Where(a => a.nem == 'l' && a.bukasokSzama == 0)
                .Max(a => a.atlag);
            Console.WriteLine($"a legjobb lányátlag: {maxAtlag}");
            Console.WriteLine($"a legrosszabb lányátlag: {minAtlag}");
            Console.WriteLine($"a különbség: {maxAtlag - minAtlag}");
        }

        void a_15()
        {
            Console.WriteLine("a15: A legjobb informatikus fiúk kedvenc tárgya");
            var infosFiuk = li
                .Where(a => a.informatikus && a.nem == 'f')
                .OrderByDescending(a => a.atlag);
            double max3Atlag = infosFiuk
                .Skip(2).First().atlag;
            foreach (Diak di in infosFiuk.Where(a => a.atlag >= max3Atlag))
                Console.WriteLine($"\t{di.nev}, {di.atlag} - {di.kedvencTargy}");
        }

        void a_16()
        {
            Console.WriteLine("a16: Zalaegerszegi átlag vs vidéki átlag");
            double zegiAtlag = li.Where(a => !a.videki).Average(a => a.atlag);
            double videkiAtlag = li.Where(a => a.videki).Average(a => a.atlag);
            Console.WriteLine($"zalaegerszegiek átlaga: {zegiAtlag:n2}; vidékiek átlaga: {videkiAtlag:n2}");
            if (zegiAtlag > videkiAtlag) Console.WriteLine("az egerszegiek a jobbak");
            else if (videkiAtlag > zegiAtlag) Console.WriteLine("a vidékiek a jobbak");
            else Console.WriteLine("egyformák");
        }

        void a_17()
        {
            Console.WriteLine("a17: Az egyes településeken lakók átlagai");
            var telepek = li
                .GroupBy(a => a.lakohely)
                .Select(a => new
                {
                    telepules = a.Key,
                    atlag = a.Average(b => b.atlag)
                })
                .OrderByDescending(a => a.atlag);
            foreach (var te in telepek)
                Console.WriteLine($"\t{te.telepules}: {te.atlag:n2}");
        }

        void a_18()
        {
            Console.WriteLine("a18: Alsóbbévesek-felsőbbévesek-felnőttek");
            var korok = li
                .OrderBy(a => a.evfolyam)
                .GroupBy(a => a.korosztaly)
                .Select(a => new
                {
                    kor = a.Key,
                    zsebpenz = a.Average(b => b.zsebpenz),
                    atlag = a.Average(b => b.atlag)
                });
            foreach (var ko in korok)
                Console.WriteLine($"\t{ko.kor}: átlagos zsebpénz {ko.zsebpenz:c0}, átlag: {ko.atlag:n2}");
            string maxPenz = korok.OrderByDescending(a => a.zsebpenz).First().kor;
            Console.WriteLine($"legtöbb zsebpénz: {maxPenz}");
            string maxAtlag = korok.OrderByDescending(a => a.atlag).First().kor;
            Console.WriteLine($"legjobb átlag: {maxAtlag}");
        }

        void a_19()
        {
            Console.WriteLine("a19: Az egyes tárgyak hány diák kedvencei");
            var targyak = li
                .GroupBy(a => a.kedvencTargy)
                .Select(a => new
                {
                    targy = a.Key,
                    fo = a.Count()
                })
                .OrderBy(a => a.targy);
            foreach (var ta in targyak)
                Console.WriteLine($"\t{ta.targy}: {ta.fo} diák kedvence");
        }

        void a_20()
        {
            Console.WriteLine("a20: Egynél több keresztnév");
            var okAzok = li
                .Where(a => a.nev.Split(' ').Length > 2)
                .OrderBy(a => a.nev);
            foreach (Diak di in okAzok)
                Console.WriteLine($"\t{di.nev} {di.osztaly}");
        }

        void a_21()
        {
            Console.WriteLine("a21: Akik nem \"utcá\"-ban laknak");
            var okAzok = li
                .Where(a => !a.lakcim.Contains("utca"))
                .OrderBy(a => a.nev);
            foreach (Diak di in okAzok)
                Console.WriteLine($"\t{di.nev}, {di.osztaly}: {di.lakcim}");
        }

        void a_22()
        {
            Console.WriteLine("a22: Csoportonként a bukott diákok");
            var csoportok = li
                .Where(a => a.bukasokSzama > 0)
                .GroupBy(a => a.csoport)
                .Select(a => new
                {
                    csoport = a.Key,
                    fo = a.Count()
                });
            foreach (var cs in csoportok)
                Console.WriteLine($"\t{cs.csoport}: {cs.fo} bukott diák");
        }

        void a_23()
        {
            int nagyCsalad = li.Count(a => a.testverekSzama >= 2);
            int bukottNagyCsalad = li
                .Where(a => a.testverekSzama >= 2)
                .Where(a => a.bukasokSzama > 3)
                .Count();
            double arany = 100.0 * bukottNagyCsalad / nagyCsalad;
            Console.WriteLine($"a23: A nagycsaládosok {arany:n2}%-a bukott több, mint 3 tárgyból");
        }

        void a_24()
        {
            Console.WriteLine("a24: Legtöbb bukás");
            var osztalyok = li
                .Where(a => a.bukasokSzama > 0)
                .OrderBy(a => a.evfolyam)
                .ThenBy(a => a.osztaly)
                .GroupBy(a => a.osztaly)
                .Select(a => new
                {
                    osztaly = a.Key,
                    fo = a.Count()
                });
            foreach (var osz in osztalyok)
                Console.WriteLine($"\t{osz.osztaly,4}: {osz.fo} bukott");
            int legtobb = osztalyok.Max(a => a.fo);
            string s = string.Join(", ", osztalyok
                .Where(a => a.fo == legtobb)
                .Select(a => a.osztaly)
                );
            Console.WriteLine($"itt van a legtöbb: {s}\n");

            var csoportok = li
                .Where(a => a.bukasokSzama > 0)
                .GroupBy(a => a.csoport)
                .Select(a => new
                {
                    csop = a.Key,
                    fo = a.Count()
                });
            foreach (var cs in csoportok)
                Console.WriteLine($"\t{cs.csop,4}: {cs.fo} bukott");
            legtobb = csoportok.Max(a => a.fo);
            s = string.Join(", ", csoportok
                .Where(a => a.fo == legtobb)
                .Select(a => a.csop)
                );
            Console.WriteLine($"itt van a legtöbb: {s}");
        }

        void a_25()
        {
            int kitunok = li.Count(a => a.atlag == 5);
            int kitunoEgykek = li.Count(a => a.atlag == 5 && a.testverekSzama == 0);
            double arany = 100.0 * kitunoEgykek / kitunok;
            Console.WriteLine($"a25: A kitűnő tanulók {arany:n2}%-a egyke");
            Console.WriteLine($"{kitunok} kitűnő tanuló van, ebből {kitunoEgykek} egyke");
        }

        void a_26()
        {
            Console.WriteLine("a26: Minősítés szövegesen");
            foreach (Diak di in li.OrderBy(a => a.nev))
                Console.WriteLine($"{di.nev} {di.osztaly}: átlaga {di.atlag}, {di.minosites}");
        }

        void a_27()
        {
            Console.WriteLine("a27: Monogramok");
            var monogramok = li
                .GroupBy(a => a.monogram)
                .Select(a => new
                {
                    mono = a.Key,
                    nevsor = a
                })
                .Where(a => a.nevsor.Count() > 1)
                .OrderBy(a => a.mono);
            foreach (var mon in monogramok)
            {
                Console.WriteLine($"{mon.mono}:");
                foreach (Diak di in mon.nevsor)
                    Console.WriteLine($"\t{di.nev} {di.osztaly}");
            }
        }

        void a_28()
        {
            Console.WriteLine("a28: Osztályonként, nemenként");
            var osztalyok = li
                .OrderBy(a => a.evfolyam)
                .ThenBy(a => a.osztaly)
                .GroupBy(a => a.osztaly)
                .Select(a => new
                {
                    osztaly = a.Key,
                    atlag = a.Average(b => b.atlag),
                    letszam = a.Count(),
                    lanyok = a.Where(b => b.nem == 'l'),
                    fiuk = a.Where(b => b.nem == 'f')
                });
            foreach (var oszt in osztalyok)
            {
                Console.WriteLine($"{oszt.osztaly}: {oszt.letszam} fő, átlag: {oszt.atlag:n2}");
                Console.WriteLine("lányok:");
                foreach (Diak di in oszt.lanyok.OrderBy(a => a.nev))
                    Console.WriteLine($"\t{di.nev}, átlaga: {di.atlag}");
                Console.WriteLine("fiúk:");
                foreach (Diak di in oszt.fiuk.OrderBy(a => a.nev))
                    Console.WriteLine($"\t{di.nev}, átlaga: {di.atlag}");
                Console.WriteLine();
            }
        }

        void a_29()
        {
            Console.WriteLine("a29: Reálosok és humánosok");
            var csoportok = li.GroupBy(a => a.csoport)
                .Select(a => new
                {
                    csop = a.Key,
                    realArany = 100.0* a.Count(b => b.realos) / a.Count(),
                    humanArany = 100.0 * a.Count(b => !b.realos) / a.Count()
                });
            foreach (var cs in csoportok)
                Console.WriteLine($"{cs.csop}: {cs.realArany:n2}% reálos, {cs.humanArany:n2}% humános");
        }

        void a_30()
        {
            Console.WriteLine("a30: A leghosszabb nevűek");
            int leg = li.Max(a => a.nev.Length);
            foreach (Diak di in li.Where(a => a.nev.Length == leg))
                Console.WriteLine($"{di.nev} {di.osztaly}, {di.eletkor} éves, lakik: {di.lakcim}");
        }

        void a_31()
        {
            Console.WriteLine("a31: Településenként hányan születtek");
            var telepulesek = li
                .GroupBy(a => a.szulHely)
                .Select(a => new
                {
                    telep = a.Key,
                    fo = a.Count()
                })
                .OrderBy(a => a.telep);
            foreach (var te in telepulesek)
                Console.WriteLine($"\t{te.telep}: {te.fo} fő született itt");
        }

        Random rnd = new Random();

        void a_32()
        {
            Diak randomDiak = li
                .Where(a => a.allat1 != "")
                .OrderBy(a => rnd.NextDouble())
                .First();
            string randomAllat;
            if (randomDiak.allat2 == "") randomAllat = randomDiak.allat1;
            else
            {
                int a = rnd.Next(1, 3);
                if (a == 1) randomAllat = randomDiak.allat1;
                else randomAllat = randomDiak.allat2;
            }
            Console.WriteLine($"a32: Random állat: {randomAllat}");
            var osztalyok = li
                .OrderBy(a => a.evfolyam)
                .ThenBy(a => a.osztaly)
                .GroupBy(a => a.osztaly)
                .Select(a => new
                {
                    oszt = a.Key,
                    nekik = a.Where(b => b.allat1 == randomAllat || b.allat2 == randomAllat)
                });
            foreach (var osz in osztalyok)
            {
                int db = 0;
                foreach (Diak di in osz.nekik)
                {
                    if (di.allat1 == randomAllat) db++;
                    if (di.allat2 == randomAllat) db++;
                }
                Console.WriteLine($"{osz.oszt}; {db} {randomAllat} van; nekik:");
                foreach (Diak di in osz.nekik)
                    Console.WriteLine($"\t{di.nev}");
            }
        }

        void a_33()
        {
            Console.WriteLine("a33: Állatos");
            var nincs = li.Where(a => a.allat1 == "").OrderBy(a => a.nev);
            Console.WriteLine("nincs állatuk:");
            foreach (Diak di in nincs)
                Console.WriteLine($"\t{di.nev} {di.osztaly}");
            var egy = li.Where(a => a.allat2 == "").OrderBy(a => a.nev);
            Console.WriteLine("\negy állatuk van:");
            foreach (Diak di in egy)
                Console.WriteLine($"\t{di.nev} {di.osztaly}: {di.allat1}");
            var ketto = li.Where(a => a.allat2 != "").OrderBy(a => a.nev);
            Console.WriteLine("\nkét állatuk van:");
            foreach (Diak di in ketto)
                Console.WriteLine($"\t{di.nev} {di.osztaly}: {di.allat1} és {di.allat2}");
            var ketAzonos = li.Where(a => a.allat2 != "" && a.allat1 == a.allat2).OrderBy(a => a.nev);
            Console.WriteLine("\nkét azonos állatuk van:");
            foreach (Diak di in ketAzonos)
                Console.WriteLine($"\t{di.nev} {di.osztaly}: két {di.allat1}");
        }

        void a_34()
        {
            int zegi = li.Count(a => !a.videki && a.allat1 == "");
            int videki = li.Count(a => a.videki && a.allat1 == "");
            Console.WriteLine($"a34: {zegi} zalaegerszegi és {videki} vidéki diáknak nincs állata");
        }

        void a_35()
        {
            Diak randomDiak = li
                .Where(a => a.allat1 != "")
                .OrderBy(a => rnd.NextDouble())
                .First();
            string randomAllat;
            if (randomDiak.allat2 == "") randomAllat = randomDiak.allat1;
            else
            {
                int a = rnd.Next(1, 3);
                if (a == 1) randomAllat = randomDiak.allat1;
                else randomAllat = randomDiak.allat2;
            }
            Console.WriteLine($"a35: Random állat: {randomAllat}");
            var honapok = li
                .OrderBy(a => a.szulHonapSzam)
                .GroupBy(a => a.szulHonapNev)
                .Select(a => new
                {
                    honap = a.Key,
                    ennyi = a.Sum(b => b.hanyIlyenAllatVan(randomAllat))
                });
            Console.WriteLine("az egyes hónapoban születettek között ennyi ilyen van:");
            foreach (var ho in honapok)
                Console.WriteLine($"\t{ho.honap}: {ho.ennyi}");
            int leg2 = honapok.OrderByDescending(a => a.ennyi).Skip(1).First().ennyi;
            var ezek = string.Join(", ",
                honapok
                    .Where(a => a.ennyi >= leg2)
                    .Select(a => a.honap)
                );
            Console.WriteLine($"legtöbb: {ezek}");
        }

        void a_36()
        {
            Console.WriteLine("a36: Évfolyamonként melyik állatból van legtöbb");
            var evfolyamok = li
                .OrderBy(a => a.evfolyam)
                .GroupBy(a => a.evfolyam)
                .Select(a => new
                {
                    evf = a.Key,
                    nevsor = a
                });
            foreach (var ev in evfolyamok)
            {
                Dictionary<string, int> allatok = new Dictionary<string, int>();
                foreach (Diak di in ev.nevsor)
                {
                    if (di.allat1 != "")
                    {
                        if (allatok.ContainsKey(di.allat1)) allatok[di.allat1]++;
                        else allatok.Add(di.allat1, 1);
                        if (di.allat2 != "")
                            if (allatok.ContainsKey(di.allat2)) allatok[di.allat2]++;
                            else allatok.Add(di.allat2, 1);
                    }
                }
                int leg = allatok.Max(a => a.Value);
                string ezek = string.Join(", ",
                    allatok
                        .Where(a => a.Value == leg)
                        .Select(a => a.Key)
                    );
                Console.WriteLine($"{ev.evf,2}. évfolyam: {ezek} ({leg} példány)");
            }
        }

        void a_37()
        {
            Console.WriteLine("a37: Nincs állat");
            int buk = li.Count(a => a.bukasokSzama > 0 && a.allat1 == "");
            int jo = li.Count(a => a.atlag >= 4 && a.allat1 == "");
            Console.WriteLine($"{buk} bukottnak és {jo} jó tanulónak nincs állata");
            string valasz = "";
            if (buk > jo) valasz = "a bukottak között";
            else if (jo > buk) valasz = "a jó tanulók között";
            else valasz = "egyenlő arányban";
            Console.WriteLine($"{valasz} fordul elő többször, hogy nincs állat");
        }

        void a_38()
        {
            Console.WriteLine("a38: Állatonként és településenként");
            List<string> allatok = new List<string>();
            foreach (Diak di in li)
            {
                if (di.allat1 != "" && !allatok.Contains(di.allat1))
                    allatok.Add(di.allat1);
                if (di.allat1 != "" && !allatok.Contains(di.allat1))
                    allatok.Add(di.allat1);
            }
            allatok.Sort();
            foreach (string allat in allatok)
            {
                Console.WriteLine($"{allat}:");
                var telepek = li
                    .Where(a => a.allat1 == allat || a.allat2 == allat)
                    .GroupBy(a => a.lakohely)
                    .Select(a => new
                    {
                        helyseg = a.Key,
                        db = a.Sum(b => b.hanyIlyenAllatVan(allat))
                    })
                    .OrderBy(a => a.helyseg);
                foreach (var te in telepek)
                    Console.WriteLine($"\t{te.helyseg}: {te.db} példány");
                Console.WriteLine();
            }
        }

        void a_39()
        {
            Console.WriteLine("a39: Kopaszok száma");
            var osztalyok = li
                .Where(a => a.hajszin == "kopasz")
                .OrderBy(a => a.evfolyam)
                .ThenBy(a => a.osztaly)
                .GroupBy(a => a.osztaly)
                .Select(a => new
                {
                    oszt = a.Key,
                    fiuk = a.Count(b => b.nem == 'f'),
                    lanyok = a.Count(b => b.nem == 'l')
                });
            foreach (var osz in osztalyok)
                Console.WriteLine($"\t{osz.oszt,4}: {osz.lanyok} lány és {osz.fiuk} fiú");
            int legF = osztalyok.Max(a => a.fiuk);
            string sF = string.Join(", ",
                    osztalyok
                    .Where(a => a.fiuk == legF)
                    .Select(a => a.oszt)
                );
            int legL = osztalyok.Max(a => a.lanyok);
            string sL = string.Join(", ",
                    osztalyok
                    .Where(a => a.lanyok == legL)
                    .Select(a => a.oszt)
                );
            Console.WriteLine($"a legtöbb kopasz fiú itt van: {sF}");
            Console.WriteLine($"a legtöbb kopasz lány itt van: {sL}");
        }

        void a_40()
        {
            Console.WriteLine("a40: Extrém hajak");
            int bukott = li.Count(a => a.atlag == 1 && a.extremHaj);
            int jeles = li.Count(a => a.atlag >= 4.5 && a.extremHaj);
            Console.WriteLine($"{bukott} bukott és {jeles} jeles tanulónak van extrém haja");
            if (bukott > jeles) Console.WriteLine("a bukottak vannak többen");
            else if (jeles > bukott) Console.WriteLine("a jeles rendűek vannak többen");
            else Console.WriteLine("azonos számban vannak");
        }

        void a_41()
        {
            string randomHaj = li
                .OrderBy(a => rnd.NextDouble())
                .First().hajszin;
            int randomEvfolyam = li
                .OrderBy(a => rnd.NextDouble())
                .First().evfolyam;
            Console.WriteLine($"41: {randomHaj} haj és {randomEvfolyam}. évfolyam");
            var okAzok = li
                .Where(a => a.hajszin == randomHaj && a.evfolyam == randomEvfolyam)
                .OrderBy(a => a.nev);
            foreach (Diak di in okAzok)
                Console.WriteLine($"\t{di.nev} {di.osztaly}");
        }

        void a_42()
        {
            Console.WriteLine("a42: Hiányzó hajszínek");
            var hajak = li.Select(a => a.hajszin).Distinct();
            var osztalyok = li
                .OrderBy(a => a.evfolyam)
                .ThenBy(a => a.osztaly)
                .GroupBy(a => a.osztaly)
                .Select(a => new
                {
                    oszt = a.Key,
                    nevsor = a
                });
            foreach (var osz in osztalyok)
            {
                var ezek = hajak.ToList();
                foreach (Diak di in osz.nevsor)
                    ezek.Remove(di.hajszin);
                if (ezek.Count() > 0)
                {
                    string s = string.Join(", ", ezek);
                    Console.WriteLine($"{osz.oszt,4}: {s} haj nincs");
                }
            }
        }

        void a_sorozat()
        {
            a_42();
            //a_41();
            //a_40();
            //a_39();
            //a_38();
            //a_37();
            //a_36();
            //a_35();
            //a_34();
            //a_33();
            //a_32();
            //a_31();
            //a_30();
            //a_29();
            //a_28();
            //a_27();
            //a_26();
            //a_25();
            //a_24();
            //a_23();
            //a_22();
            //a_21();
            //a_20();
            //a_19();
            //a_18();
            //a_17();
            //a_16();
            //a_15();
            //a_14();
            //a_13();
            //a_12();
            //a_11();
            //a_10();
            //a_9();
            //a_8();
            //a_7();
            //a_6();
            //a_5();
            //a_4();
            //a_3();
            //a_2();
            //a_1();
        }

        #endregion

        #region B_sorozat

        void b_sorozat()
        {

        }

        #endregion

        Program()
        {
            felolvas();
            //a_sorozat();
            b_sorozat();
        }

        static void Main(string[] args)
        {
            new Program();
            Console.WriteLine("\nBillentyű...");
            Console.ReadKey();
        }
    }
}
