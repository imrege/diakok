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
            Console.WriteLine($"a41: {randomHaj} haj és {randomEvfolyam}. évfolyam");
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

                if (ezek.Count() > 1)
                {
                    string s = string.Join(", ", ezek);
                    Console.WriteLine($"{osz.oszt,4}: {s} haj nincs");
                }
                else Console.WriteLine($"{osz.oszt,4}: minden hajszín van");
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

        void b_1()
        {
            Console.WriteLine("b2: Akik a mostani hónapban születtek");
            int maiHonap = DateTime.Today.Month;
            var okAzok = li
                .Where(a => a.szulDatumIdo.Month == maiHonap)
                .OrderBy(a => a.nev);
            foreach (Diak di in okAzok)
                Console.WriteLine($"{di.nev}, szül.: {di.hosszuSzulDatum}");
        }

        void b_2()
        {
            Console.WriteLine("b2: Éjfél +-30 perces környezete");
            var okAzok = li
                .Where(a => (a.szulDatumIdo.Hour == 23 && a.szulDatumIdo.Minute >= 30)
                    || (a.szulDatumIdo.Hour == 0 && a.szulDatumIdo.Minute <= 30))
                .OrderBy(a => a.nev);
            foreach (Diak di in okAzok)
                Console.WriteLine($"{di.nev}: {di.szulDatumIdo}");
        }

        void b_3()
        {
            Console.WriteLine("b3: A decemberi egykék");
            var okAzok = li
                .Where(a => a.szulDatumIdo.Month == 12)
                .Where(a => a.testverekSzama == 0)
                .OrderBy(a => a.nev);
            foreach (Diak di in okAzok)
                Console.WriteLine($"{di.nev}: szül. {di.szulHely}, {di.szul}");
        }

        void b_4()
        {
            Console.WriteLine("b4: Számtani sorozat");
            var okAzok = li
                .Where(a => a.szulDatumIdo.Hour - a.szulDatumIdo.Minute == a.szulDatumIdo.Minute - a.szulDatumIdo.Second)
                .OrderBy(a => a.nev);
            foreach (Diak di in okAzok)
                Console.WriteLine($"{di.nev} szül. {di.szulDatumIdo}");
        }

        void b_5()
        {
            Console.WriteLine("b5: Anya-apa életkora közötti legkisebb különbség");
            long min = li.Min(a => Math.Abs(a.anyjaSzul.Ticks - a.apjaSzul.Ticks));
            Diak oAz = li
                .Find(a => Math.Abs(a.anyjaSzul.Ticks - a.apjaSzul.Ticks) == min);
            Console.WriteLine($"{oAz.nev}: anya {oAz.anySzul}, apa {oAz.apSzul}");
        }

        void b_6()
        {
            Console.WriteLine("b6: Akik elmúltak 19 évesek");
            var okAzok = li
                .Where(a => a.eletkor > 19)
                .OrderBy(a => a.nev);
            foreach (Diak di in okAzok)
                Console.WriteLine($"{di.nev}, {di.osztaly}: {di.eletkor} éves");
        }

        void b_7()
        {
            Console.WriteLine("b7: Akiknek az anyja kiskorú volt, amikor születtek");
            var okAzok = li
                .Where(a => a.anyjaKoraSzulkor < 18)
                .OrderBy(a => a.nev);
            foreach (Diak di in okAzok)
                Console.WriteLine($"{di.nev}; anyja {di.anyjaKoraSzulkor} évesen szülte (ő: {di.szul}, anyja: {di.anySzul})");
        }

        void b_8()
        {
            Console.WriteLine("b8: Fiatalabb apa");
            var okAzok = li
                .Where(a => a.anyjaSzul < a.apjaSzul)
                .OrderBy(a => a.nev);
            foreach (Diak di in okAzok)
                Console.WriteLine($"{di.nev} - anya {di.anySzul}, apa {di.apSzul}");
        }

        void b_9()
        {
            Console.WriteLine($"b9: Legfiatalabb szülők a gyerek születésekor");
            Diak oAzAnya = li
                .OrderBy(a => a.szulDatumIdo.Ticks - a.anyjaSzul.Ticks)
                .First();
            Console.WriteLine("a legfiatalabb anya:");
            Console.WriteLine($"\t{oAzAnya.nev}; az anyja {oAzAnya.anyjaKoraSzulkor} éves volt (ő {oAzAnya.szul}, anyja {oAzAnya.anySzul})");
            Diak oAzApa = li
                .OrderBy(a => a.szulDatumIdo.Ticks - a.apjaSzul.Ticks)
                .First();
            Console.WriteLine("a legfiatalabb apa:");
            Console.WriteLine($"\t{oAzApa.nev}; az apja {oAzApa.apjaKoraSzulkor} éves volt (ő {oAzApa.szul}, apja {oAzApa.apSzul})");
        }

        void b_10()
        {
            Console.WriteLine("b10: Születésnap valamelyik szülővel egy napon");
            var okAzokAnya = li
                .Where(a => a.szulDatumIdo.Month == a.anyjaSzul.Month && a.szulDatumIdo.Day == a.anyjaSzul.Day)
                .OrderBy(a => a.nev);
            Console.WriteLine("az anyával egy napon:");
            foreach (Diak di in okAzokAnya)
                Console.WriteLine($"\t{di.nev}: ő {di.szul}, anyja {di.anySzul}");
            var okAzokApa = li
                .Where(a => a.szulDatumIdo.Month == a.apjaSzul.Month && a.szulDatumIdo.Day == a.apjaSzul.Day)
                .OrderBy(a => a.nev);
            Console.WriteLine("az apával egy napon:");
            foreach (Diak di in okAzokApa)
                Console.WriteLine($"\t{di.nev}: ő {di.szul}, apja {di.apSzul}");
        }

        void b_11()
        {
            Console.WriteLine("b11: Életkor szerinti statisztika");
            var korok = li
                .GroupBy(a => a.eletkor)
                .Select(a => new
                {
                    kor = a.Key,
                    ossz = a.Count(),
                    lanyok = a.Count(b => b.nem == 'l'),
                    fiuk = a.Count(b => b.nem == 'f')
                })
                .OrderBy(a => a.kor);
            foreach (var kor in korok)
                Console.WriteLine($"{kor.kor} évesek: {kor.lanyok,2} lány, {kor.fiuk,2} fiú, összesen {kor.ossz,3} diák");
        }

        void b_12()
        {
            Console.WriteLine("b12: Péntek 13-a");
            var okAzok = li
                .Where(a => a.szulNapNev == "péntek" && a.szulDatumIdo.Day == 13)
                .OrderBy(a => a.nev);
            foreach (Diak di in okAzok)
                Console.WriteLine($"{di.nev}; szül. {di.hosszuSzulDatum}");
        }

        void b_13()
        {
            Console.WriteLine("b13: Legalább 5-en születtek azonos napon");
            var napok = li
                .OrderBy(a => a.szulHoNapSzam)
                .GroupBy(a => a.szulHoNap)
                .Select(a => new
                {
                    hoNap = a.Key,
                    nevsor = a.OrderBy(b => b.nev)
                })
                .Where(a => a.nevsor.Count() >= 5);
            foreach (var na in napok)
            {
                Console.WriteLine($"{na.hoNap} ({na.nevsor.Count()} diák):");
                foreach (Diak di in na.nevsor)
                    Console.WriteLine($"\t{di.nev}, {di.osztaly}; szül. {di.szul}");
            }
        }

        void b_14()
        {
            Console.WriteLine("b14: Pontosan azonos napon születtek");
            var napok = li
                .OrderBy(a => a.szul)
                .GroupBy(a => a.szul)
                .Select(a => new
                {
                    nap = a.Key,
                    nevsor = a.OrderBy(b => b.nev)
                })
                .Where(a => a.nevsor.Count() > 1);
            foreach (var na in napok)
            {
                Console.WriteLine($"{na.nap}:");
                foreach (Diak di in na.nevsor)
                    Console.WriteLine($"\t{di.nev}, {di.osztaly}");
            }
        }

        void b_15()
        {
            Console.WriteLine("b15: A legfiatalabb/legidősebb diák");
            var sorrend = li.OrderBy(a => a.szulDatumIdo);
            Console.WriteLine($"a legfiatalabb:\n\t{sorrend.Last().nev} {sorrend.Last().osztaly}, {sorrend.Last().szulDatumIdo}");
            Console.WriteLine($"a legidősebb:\n\t{sorrend.First().nev} {sorrend.First().osztaly}, {sorrend.First().szulDatumIdo}");
        }

        void b_16()
        {
            Console.WriteLine("b16: Az apák életkora szerint");
            var apak = li
                .OrderBy(a => a.apjaKorosztaly)
                .GroupBy(a => a.apjaKorosztaly)
                .Select(a => new
                {
                    kor = a.Key,
                    lista = a.OrderBy(b => b.nev)
                });
            foreach (var ap in apak)
            {
                Console.WriteLine($"{ap.kor} éves apjuk nekik van ({ap.lista.Count()} fő):");
                foreach (Diak di in ap.lista)
                    Console.WriteLine($"\t{di.nev} {di.osztaly}, apja szül.: {di.apSzul}");
            }
        }

        void b_17()
        {
            Console.WriteLine("b17: Anya-apa kora közötti legnagyobb különbség");
            Diak ezADiak = li
                .OrderByDescending(a => Math.Abs(a.apjaSzul.Ticks - a.anyjaSzul.Ticks))
                .First();
            int korKul = Math.Abs(ezADiak.apaKora - ezADiak.anyaKora);
            Console.WriteLine($"{ezADiak.nev} {ezADiak.osztaly}: anyja szül. {ezADiak.anySzul}, apja szül. {ezADiak.apSzul}; korkülönbség: {korKul} év");
        }

        void b_18()
        {
            Console.WriteLine("b18: Azonos hónapban a születésnapok");
            var okAzok = li
                .Where(a => a.szulDatumIdo.Month == a.anyjaSzul.Month)
                .Where(a => a.szulDatumIdo.Month == a.apjaSzul.Month)
                .OrderBy(a => a.nev);
            foreach (Diak di in okAzok)
                Console.WriteLine($"{di.nev} {di.osztaly}: ő szül. {di.szul}, anyja szül. {di.anySzul}, apja szül. {di.apSzul}");
        }

        void b_19()
        {
            Console.WriteLine("b19: Hónap, amikor a legtöbb fiú-lány született");
            var honapok = li
                .OrderBy(a => a.szulHonapSzam)
                .GroupBy(a => a.szulHonapNev)
                .Select(a => new
                {
                    honap = a.Key,
                    lanyok = a.Count(b => b.nem == 'l'),
                    fiuk = a.Count(b => b.nem == 'f')
                });
            int maxL = honapok.Max(a => a.lanyok);
            int maxF = honapok.Max(a => a.fiuk);
            string hoL = string.Join(", ",
                    honapok
                        .Where(a => a.lanyok == maxL)
                        .Select(a => a.honap)
                );
            foreach (var ho in honapok)
                Console.WriteLine($"\t{ho.honap}: {ho.lanyok} lány és {ho.fiuk} fiú");
            string hoF = string.Join(", ",
                honapok
                    .Where(a => a.fiuk == maxF)
                    .Select(a => a.honap)
                );
            Console.WriteLine($"a legtöbb lány ({maxL}) ekkor született: {hoL}");
            Console.WriteLine($"a legtöbb fiú ({maxF}) ekkor született: {hoF}");
        }

        void b_20()
        {
            Console.WriteLine("b20: Osztályonként a legfiatalabb-legidősebb");
            var osztalyok = li
                .OrderBy(a => a.evfolyam)
                .ThenBy(a => a.osztaly)
                .GroupBy(a => a.osztaly)
                .Select(a => new
                {
                    oszt = a.Key,
                    nevsor = a.ToList(),
                    minSzul = a.Min(b => b.szulDatumIdo),
                    maxSzul = a.Max(b => b.szulDatumIdo)
                });
            foreach (var osz in osztalyok)
            {
                Console.WriteLine($"{osz.oszt}:");
                Diak legidosebb = osz.nevsor.Find(a => a.szulDatumIdo == osz.minSzul);
                Console.WriteLine($"\tlegidősebb: {legidosebb.nev}; szül. {legidosebb.szul}");
                Diak legfiatalabb = osz.nevsor.Find(a => a.szulDatumIdo == osz.maxSzul);
                Console.WriteLine($"\tlegfiatalabb: {legfiatalabb.nev}; szül. {legfiatalabb.szul}");
            }
        }

        void b_21()
        {
            Console.WriteLine("b21: A hétköznap vagy hétvégén születettek tanulnak-e jobban");
            double hetkoznapiAtlag = li
                .Where(a => !a.hetvegenSzul)
                .Average(a => a.atlag);
            double hetvegiAtlag = li
                .Where(a => a.hetvegenSzul)
                .Average(a => a.atlag);
            Console.WriteLine($"hétvégén születettek átlaga: {hetvegiAtlag:n2}");
            Console.WriteLine($"hétköznap születettek átlaga: {hetkoznapiAtlag:n2}");
            string s = "";
            if (hetkoznapiAtlag > hetvegiAtlag) s = "A hétköznap születettek jobban tanulnak.";
            else if (hetvegiAtlag > hetkoznapiAtlag) s = "A hétvégén születettek jobban tanulnak.";
            else s = "Egyformán tanulnak.";
            Console.WriteLine(s);
        }

        void b_22()
        {
            Console.WriteLine("b22: Random diák születési éve");
            Diak randomDiak = li
                .OrderBy(a => rnd.NextDouble())
                .First();
            Console.WriteLine($"a random diák: {randomDiak.nev} {randomDiak.osztaly}, szül. {randomDiak.szulDatumIdo}");
            var okAzok = li
                .Where(a => a.szulDatumIdo.Year == randomDiak.szulDatumIdo.Year)
                .OrderBy(a => a.szulDatumIdo);
            Console.WriteLine($"ugyanebben az évben először született: {okAzok.First().nev} {okAzok.First().osztaly}; szül. {okAzok.First().szulDatumIdo}");
            Console.WriteLine($"ugyanebben az évben utoljára született: {okAzok.Last().nev} {okAzok.Last().osztaly}; szül. {okAzok.Last().szulDatumIdo}");
        }

        void b_23()
        {
            Console.WriteLine("b23: Nyugdíjas szülő");
            var okAzok = li
                .Where(a => a.apaKora >= 65 || a.anyaKora >= 65)
                .OrderBy(a => a.nev);
            foreach (Diak di in okAzok)
            {
                string s = "";
                if (di.anyaKora >= 65 && di.apaKora < 65)
                    s = $"anya {di.anyaKora} éves";
                else if (di.apaKora >= 65 && di.anyaKora < 65)
                    s = $"apa {di.apaKora} éves";
                else s = $"anya {di.anyaKora} éves és apa {di.apaKora} éves";
                Console.WriteLine($"{di.nev} {di.osztaly}: {s}");
            }
        }

        void b_24()
        {
            Console.WriteLine("b24: Szülők átlagéletkora osztályonként");
            var osztalyok = li
                .GroupBy(a => a.osztaly)
                .Select(a => new
                {
                    oszt = a.Key,
                    atlag = (a.Average(b => b.anyaKora) + a.Average(b => b.apaKora)) / 2
                })
                .OrderByDescending(a => a.atlag);
            foreach (var osz in osztalyok)
                Console.WriteLine($"\t{osz.oszt,4}: {osz.atlag:n2} év");
        }

        void b_25()
        {
            Console.WriteLine("b25: Lány esetén melyik szülő az idősebb");
            int idosebbAnya = li
                .Count(a => a.nem == 'l' && a.anyjaSzul < a.apjaSzul);
            int idosebbApa = li
                .Count(a => a.nem == 'l' && a.apjaSzul < a.anyjaSzul);
            Console.WriteLine($"Lány születése esetén {idosebbAnya} alkalommal az anya, {idosebbApa} alkalommal az apa volt az idősebb.");
            if (idosebbAnya > idosebbApa) Console.WriteLine("Az idősebb anyák voltak többen.");
            else if (idosebbApa > idosebbAnya) Console.WriteLine("Az idősebb apák voltak többen.");
            else Console.WriteLine("Azonos a számuk.");
        }

        void b_26()
        {
            Console.WriteLine("b26: A legnagyobb korkülönbségű 2 tanuló");
            var lista = li.OrderBy(a => a.szulDatumIdo);
            Diak egyik = lista.Last();
            Diak masik = lista.First();
            Console.WriteLine($"egyik diák: {egyik.nev} {egyik.osztaly}, szül. {egyik.szulDatumIdo}");
            Console.WriteLine($"masik diák: {masik.nev} {masik.osztaly}, szül. {masik.szulDatumIdo}");
        }

        void b_27()
        {
            Console.WriteLine("b27: Nappal és éjjel születettek");
            double nappaliak = li
                .Where(a => a.szulDatumIdo.Hour >= 8 && a.szulDatumIdo.Hour < 20)
                .Average(a => a.atlag);
            double ejjeliek = li
                .Where(a => a.szulDatumIdo.Hour < 8 || a.szulDatumIdo.Hour >= 20)
                .Average(a => a.atlag);
            Console.WriteLine($"a nappaliak átlaga: {nappaliak:n2}");
            Console.WriteLine($"az éjjeliek átlaga: {ejjeliek:n2}");
            if (nappaliak > ejjeliek) Console.WriteLine("A nappaliak jobbak.");
            else if (ejjeliek > nappaliak) Console.WriteLine("Az éjjeliek jobbak.");
            else Console.WriteLine("Egyformán tanulnak.");
        }

        void b_28()
        {
            Console.WriteLine("b28: Random diák születésnapja");
            Diak randomDiak = li
                .OrderBy(a => rnd.NextDouble())
                .First();
            Console.WriteLine($"{randomDiak.nev} {randomDiak.osztaly}, szül. {randomDiak.szulDatumIdo}");
            Diak oAz = li
                .Where(a => a.szul != randomDiak.szul)
                .OrderBy(a => Math.Abs(a.szulDatumIdo.Ticks - randomDiak.szulDatumIdo.Ticks))
                .First();
            Diak oAz2 = li
                .Where(a => a.szul != randomDiak.szul)
                .OrderBy(a => Math.Abs(a.szulDatumIdo.Ticks - randomDiak.szulDatumIdo.Ticks))
                .Skip(1)
                .First();
            Console.WriteLine($"hozzá legközelebb: {oAz.nev} {oAz.osztaly}, szül. {oAz.szulDatumIdo}");
            Console.WriteLine($"hozzá második legközelebb: {oAz2.nev} {oAz2.osztaly}, szül. {oAz2.szulDatumIdo}");
        }

        void b_29()
        {
            var li2 = li.OrderBy(a => a.eletkor).ToList();
            double medianKor = 0;
            if (li2.Count % 2 == 1)
                medianKor = li2[li2.Count / 2].eletkor;
            else medianKor = (li2[li2.Count / 2].eletkor + li2[li2.Count / 2 - 1].eletkor) / 2;
            Console.WriteLine($"b29: Az iskola medián életkora {medianKor} év.");
        }

        void b_30()
        {
            string maiNap = DateTime.Today.ToString("dddd");
            Console.WriteLine($"b30: Melyik osztályban születtek legtöbben ezen a napon: {maiNap}");
            var osztalyok = li
                .Where(a => a.szulNapNev == maiNap)
                .OrderBy(a => a.evfolyam)
                .ThenBy(a => a.osztaly)
                .GroupBy(a => a.osztaly)
                .Select(a => new
                {
                    oszt = a.Key,
                    fo = a.Count()
                });
            foreach (var osz in osztalyok)
                Console.WriteLine($"\t{osz.oszt,4}: {osz.fo} diák");
            int leg = osztalyok.Max(a => a.fo);
            string s = string.Join(", ", osztalyok
                    .Where(a => a.fo == leg)
                    .Select(a => a.oszt)
                );
            Console.WriteLine($"Legtöbb: {s}");

        }

        void b_31()
        {
            Console.WriteLine("b31: Hónapok indián neve");
            var honapok = li
                .OrderBy(a => a.szulHonapSzam)
                .GroupBy(a => a.szulHonapNev)
                .Select(a => new
                {
                    honap = a.Key,
                    nevsor = a
                });
            foreach (var ho in honapok)
            {
                string haj = ho.nevsor
                    .GroupBy(a => a.hajszin)
                    .Select(a => new
                    {
                        haj = a.Key,
                        fo = a.Count()
                    })
                    .OrderByDescending(a => a.fo)
                    .First().haj;
                Dictionary<string, int> allatok = new Dictionary<string, int>();
                foreach (Diak di in ho.nevsor)
                {
                    if (di.allat1 != "")
                        if (!allatok.ContainsKey(di.allat1))
                            allatok.Add(di.allat1, 1);
                        else allatok[di.allat1]++;
                    if (di.allat2 != "")
                        if (!allatok.ContainsKey(di.allat2))
                            allatok.Add(di.allat2, 1);
                        else allatok[di.allat2]++;
                }
                string allat = allatok
                    .OrderByDescending(a => a.Value)
                    .First().Key;
                Console.WriteLine($"\t{ho.honap} indián neve: {haj} {allat}");
            }
        }

        void b_32()
        {
            Console.WriteLine("b32: Születésnapok a téli ünnepkör idején");
            var okAzok = li
                .Where(a => a.teliUnnepek)
                .OrderBy(a => a.nev);
            foreach (Diak di in okAzok)
                Console.WriteLine($"{di.nev} {di.osztaly}, szül. {di.szul}");
        }

        void b_33()
        {
            Console.WriteLine("b33: Az állattalanok vagy a két azonos állatot tartók idősebbek");
            double ketAzonosAllat = li
                .Where(a => a.allat1 != "" && a.allat1 == a.allat2)
                .Average(a => a.eletkor);
            double nincsAllat = li
                .Where(a => a.allat1 == "")
                .Average(a => a.eletkor);
            Console.WriteLine($"akiknek két azonos állata van, az átlagéletkor: {ketAzonosAllat:n2} év");
            Console.WriteLine($"akiknek nincs állata, az átlagéletkor: {nincsAllat} év");
            if (ketAzonosAllat > nincsAllat) Console.WriteLine("A két azonos állatot tartók idősebbek.");
            else if (nincsAllat > ketAzonosAllat) Console.WriteLine("Az állattalanok idősebbek.");
            else Console.WriteLine("Egyidősek.");
        }

        void b_34()
        {
            Console.WriteLine("b34: Az osztályokban a legidősebb szülő");
            var osztalyok = li
                .OrderBy(a => a.evfolyam)
                .ThenBy(a => a.osztaly)
                .GroupBy(a => a.osztaly)
                .Select(a => new
                {
                    oszt = a.Key,
                    minSzuloSzul = Math.Min(a.Min(b => b.anyjaSzul.Ticks), a.Min(b => b.apjaSzul.Ticks)),
                    nevsor = a
                });
            foreach (var osz in osztalyok)
            {
                Console.Write($"{osz.oszt, 4}: ");
                foreach (Diak di in osz.nevsor)
                {
                    if (di.anyjaSzul.Ticks == osz.minSzuloSzul)
                        Console.WriteLine($"{di.nev}, anyja szül. {di.anySzul}");
                    else if (di.apjaSzul.Ticks == osz.minSzuloSzul)
                        Console.WriteLine($"{di.nev}, apja szül. {di.apSzul}");
                }
            }
        }

        void b_35()
        {
            Console.WriteLine("b35: Szülők átlagéletkora osztályonként");
            var osztalyok = li
                .OrderBy(a => a.evfolyam)
                .ThenBy(a => a.osztaly)
                .GroupBy(a => a.osztaly)
                .Select(a => new
                {
                    osz = a.Key,
                    atlagKor = (a.Average(b => b.anyaKora) + a.Average(b => b.apaKora)) / 2.0
                });
            foreach (var osz in osztalyok)
                Console.WriteLine($"{osz.osz, 4}: szülők átlagéletkora {osz.atlagKor:n1} év");
            double leg = osztalyok.Max(a => a.atlagKor);
            var legek = string.Join(", ", osztalyok
                    .Where(a => a.atlagKor == leg)
                    .Select(a => a.osz)
                );
            Console.WriteLine($"Itt a legidősebbek: {legek}");
        }

        void b_36()
        {
            Console.WriteLine("b36: Zóka Dániel osztálya");
            Diak zoka = li.Find(a => a.nev == "Zóka Dániel");
            int ora = zoka.szulDatumIdo.Hour;
            string oszt = zoka.osztaly;
            var okAzok = li
                .Where(a => a.osztaly == oszt)
                .Where(a => a.szulDatumIdo.Hour == ora);
            Console.WriteLine($"Zóka Dániel osztálya: {zoka.osztaly}, szül. {zoka.szulDatumIdo}");
            Console.WriteLine("ugyanebben az órában ebben az osztályban:");
            foreach (Diak di in okAzok)
                Console.WriteLine($"\t{di.nev}, szül. {di.szulDatumIdo}");
        }

        void b_37()
        {
            Console.WriteLine("b37: Random diákhoz legközelebb született");
            Diak randomDiak = li
                .OrderBy(a => rnd.NextDouble())
                .First();
            Console.WriteLine($"diák: {randomDiak.nev} {randomDiak.osztaly}, szül. {randomDiak.szulDatumIdo}");
            long legK = li
                .Where(a => !(a.nev == randomDiak.nev && a.szulDatumIdo == randomDiak.szulDatumIdo))
                .OrderBy(a => Math.Abs(randomDiak.szulDatum.Ticks - a.szulDatum.Ticks))
                .First()
                .szulDatum.Ticks;
            legK = Math.Abs(legK - randomDiak.szulDatum.Ticks);
            var okAzok = li
                .Where(a => !(a.nev == randomDiak.nev && a.szulDatumIdo == randomDiak.szulDatumIdo))
                .Where(a => Math.Abs(a.szulDatum.Ticks - randomDiak.szulDatum.Ticks) == legK);
            Console.WriteLine("napra legközelebb vannak:");
            foreach (Diak di in okAzok)
                Console.WriteLine($"\t{di.nev} {di.osztaly}, szül. {di.szulDatumIdo}");
            if (okAzok.Count() > 1)
            {
                Console.WriteLine("ezek közül legközelebb:");
                Diak oAz = okAzok
                    .OrderBy(a => Math.Abs(a.szulDatumIdo.Ticks - randomDiak.szulDatumIdo.Ticks))
                    .First();
                Console.WriteLine($"\t{oAz.nev} {oAz.osztaly}, szül. {oAz.szulDatumIdo}");
            }
        }
        void b_38()
        {
            Console.WriteLine("b38: Apa vagy anya öregebb, ahol több a gyerek");
            double apaOregebb = li
                .Where(a => a.apjaSzul < a.anyjaSzul)
                .Average(a => a.testverekSzama + 1);
            double anyaOregebb = li
                .Where(a => a.anyjaSzul < a.apjaSzul)
                .Average(a => a.testverekSzama + 1);
            Console.WriteLine($"ahol az anya idősebb, ott átlag {anyaOregebb:n1} gyerek van");
            Console.WriteLine($"ahol az apa idősebb, ott átlag {apaOregebb:n1} gyerek van");
            if (apaOregebb > anyaOregebb) Console.WriteLine("Ahol az apa idősebb, azok nagyobb családok.");
            else if (anyaOregebb > apaOregebb) Console.WriteLine("Ahol az anya idősebb, azok nagyobb családok.");
            else Console.WriteLine("Egyformák.");
        }

        void b_39()
        {
            Console.WriteLine("b39: A májusiak milyen állatot nem tartanak");
            var allatok1 = li
                .Where(a => a.allat1 != "")
                .Select(a => a.allat1)
                .Distinct();
            var allatok2 = li
                .Where(a => a.allat2 != "")
                .Select(a => a.allat2)
                .Distinct();
            allatok1.ToList().AddRange(allatok2.ToList());
            var allatok = allatok1.Distinct().ToList();
            foreach (Diak di in li.Where(a => a.szulHonapSzam == 5))
            {
                allatok.Remove(di.allat1);
                allatok.Remove(di.allat2);
            }
            if (allatok.Count == 0) Console.WriteLine("A májusiak mindenféle állatot tartanak.");
            else
            {
                string s = string.Join(", ", allatok);
                Console.WriteLine($"A májusiak nem tartanak: {s}");
            }
        }

        void b_40()
        {
            Console.WriteLine("b40: Melyik osztályban legjobban azonos az apák-anyák kora");
            var osztalyok = li
                .OrderBy(a => a.evfolyam)
                .ThenBy(a => a.osztaly)
                .GroupBy(a => a.osztaly)
                .Select(a => new
                {
                    oszt = a.Key,
                    anyaKor = a.Average(b => b.anyaKora),
                    apaKor = a.Average(b => b.apaKora)
                });
            foreach (var osz in osztalyok)
                Console.WriteLine($"{osz.oszt,4}: anyák átlagéletkora {osz.anyaKor:n2} év, apák átlagéletkora {osz.apaKor:n2} év; különbség: {Math.Abs(osz.anyaKor - osz.apaKor),5:n2} év");
            string ezaz = osztalyok
                .OrderBy(a => Math.Abs(a.anyaKor - a.apaKor))
                .First()
                .oszt;
            Console.WriteLine($"Legkisebb a különbség itt: {ezaz}");
        }

        void b_41()
        {
            Console.WriteLine("b41: A 3 legfiatalabb anyuka");
            DateTime fiatal3 = li
                .OrderByDescending(a => a.anyjaSzul)
                .Skip(2).First()
                .anyjaSzul;
            foreach (Diak di in li.Where(a => a.anyjaSzul >= fiatal3).OrderByDescending(a => a.anyjaSzul))
                Console.WriteLine($"{di.nev} {di.osztaly}: anyja szül. {di.anySzul}, {di.anyaKora} éves");
        }

        void b_42()
        {
            int lanyok = li
                .Where(a => a.nem == 'l')
                .Where(a => a.anyaKora == a.apaKora)
                .Count();
            int fiuk = li
                .Where(a => a.nem == 'f')
                .Where(a => a.anyaKora == a.apaKora)
                .Count();
            Console.WriteLine($"b42: {lanyok} lánynak és {fiuk} fiúnak egyidősek a szülei");
            Console.WriteLine("lányok:");
            var la = li
                .Where(a => a.nem == 'l')
                .Where(a => a.anyaKora == a.apaKora);
            foreach (Diak di in la)
                Console.WriteLine($"\t{di.nev} {di.osztaly}; szülők kora: {di.apaKora} év (anya szül. {di.anySzul}, apa szül. {di.apSzul})");
            Console.WriteLine("fiúk:");
            var fi = li
                .Where(a => a.nem == 'f')
                .Where(a => a.anyaKora == a.apaKora);
            foreach (Diak di in fi)
                Console.WriteLine($"\t{di.nev} {di.osztaly}; szülők kora: {di.apaKora} év (anya szül. {di.anySzul}, apa szül. {di.apSzul})");
        }

        void b_sorozat()
        {
            b_42();
            //b_41();
            //b_40();
            //b_39();
            //b_38();
            //b_37();
            //b_36();
            //b_35();
            //b_34();
            //b_33();
            //b_32();
            //b_31();
            //b_30();
            //b_29();
            //b_28();
            //b_27();
            //b_26();
            //b_25();
            //b_24();
            //b_23();
            //b_22();
            //b_21();
            //b_20();
            //b_19();
            //b_18();
            //b_17();
            //b_16();
            //b_15();
            //b_14();
            //b_13();
            //b_12();
            //b_11();
            //b_10();
            //b_9();
            //b_8();
            //b_7();
            //b_6();
            //b_5();
            //b_4();
            //b_3();
            //b_2();
            //b_1();
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
