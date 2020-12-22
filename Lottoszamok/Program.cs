using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottoszamok
{
    class Program
    {
        private static List<Sorsolas> FajlOlvas(String filePath, String fileName)
        {
            List<Sorsolas> lista = new List<Sorsolas>();
            if (!File.Exists(filePath + fileName))
                throw new IOException($"{fileName} fájl nem található!");
            using (StreamReader sr = new StreamReader(filePath + fileName))
            {
                sr.ReadLine();      // skip header

                while (!sr.EndOfStream)
                {
                    string[] lines = sr.ReadLine().Split(';');
                    Sorsolas ujSorsolas = new Sorsolas(Convert.ToDateTime(lines[0]));
                    if (!lista.Any(sorsolas => sorsolas.sorsolasNapja.Equals(ujSorsolas.sorsolasNapja)))
                        lista.Add(ujSorsolas);
                }
                Console.WriteLine($"{fileName} fájl beolvasva.");
            }
            return lista;
        }

        public static Random rng = new Random();
        private static List<int> Sorsol(int min, int max, int db)
        {

            List<int> sorsoltSzamok = new List<int>();
            for (int i = 1; i <= db; i++)
            {
                int sorsoltSzam = min + rng.Next(max - min + 1);
                if (sorsoltSzamok.Contains(sorsoltSzam))
                {
                    i--;
                    continue;
                }
                sorsoltSzamok.Add(sorsoltSzam);
            }
            return sorsoltSzamok;
        }

        static void Main(string[] args)
        {
            #region 1.feladat
            Console.Write("1. feladat: ");
            String fileName = "sorsolas.csv";
            String filePath = @"..\..\src\";
            List<Sorsolas> sorsolasLista = new List<Sorsolas>();
            try
            {
                sorsolasLista = FajlOlvas(filePath, fileName);
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }

            #endregion

            #region 2.feladat
            Console.WriteLine("2. feladat: Sorsolások eredménye:");
            sorsolasLista.ForEach(sorsolas => sorsolas.sorsoltSzamok = Sorsol(1, 9000, 5));
            List<int> mindenKihuzottSzam = new List<int>();
            var sorsolasokEredmenye = sorsolasLista
                .GroupBy(sorsolas => sorsolas.sorsolasNapja)
                .ToList();
            foreach (var sorsolasiNap in sorsolasokEredmenye)
            {
                Console.Write("\t" + sorsolasiNap.Key.ToShortDateString() + ": ");
                foreach (var eredmeny in sorsolasiNap)
                {
                    var rendezetteredmeny = eredmeny.sorsoltSzamok.OrderBy(szam => szam).ToList();
                    rendezetteredmeny.ForEach(szam => Console.Write($"{szam} "));
                    Console.WriteLine();
                    rendezetteredmeny.ForEach(szam => mindenKihuzottSzam.Add(szam));
                }

            }
            #endregion

            #region 3.feladat
            Console.Write("3. feladat: ");

            Console.WriteLine($"{mindenKihuzottSzam.Count} db számot húztak ki.");
            #endregion

            #region 4.feladat
            Console.WriteLine("4. feladat:");
            var parosak = mindenKihuzottSzam.Where(szam => szam % 2 == 0);
            var paratlanok = mindenKihuzottSzam.Where(szam => szam % 2 == 1);
            Console.WriteLine($"\tpáros: {parosak.Count()} db");
            Console.WriteLine($"\tpáratlan: {paratlanok.Count()} db");
            #endregion

            #region 5.feladat
            Console.Write("5. feladat: ");
            bool nincsTalalat = true;
            string negyvenotAlattiakEredmeny = "Eze(ke)n a napo(ko)n volt(ak) a legtöbb 45 alatti szám(ok):";
            foreach (var sorsolasiNap in sorsolasokEredmenye)
            {
                foreach (var eredmeny in sorsolasiNap)
                {
                    var negyvenotAlattiakSzama = eredmeny.sorsoltSzamok.Where(szam => szam < 45).Count();
                    if (negyvenotAlattiakSzama >= 3)
                    {
                        nincsTalalat = false;
                        negyvenotAlattiakEredmeny += "\n\t" + sorsolasiNap.Key.ToShortDateString() + ": " + negyvenotAlattiakSzama + " db";
                    }
                }
            }
            Console.WriteLine(nincsTalalat ? "Nem volt olyan nap, ami a feltételnek megfelel." : negyvenotAlattiakEredmeny);

            #endregion

            #region 6.feladat
            Console.Write("6. feladat: ");
            var szamokGyakorisaga = mindenKihuzottSzam
                .GroupBy(szam => szam)
                .Where(szam => szam.Count() > 1)
                .OrderByDescending(szam => szam.Count()).ToList();
            if (szamokGyakorisaga.Count == 0)
                Console.WriteLine("Nem volt olyan szám, amit egynél többször húztak ki.");
            else
            {
                Console.WriteLine();
                foreach (var kihuzottSzam in szamokGyakorisaga)
                {
                    Console.WriteLine($"\t{kihuzottSzam.Key}: {kihuzottSzam.Count()} db");
                }

            }
            #endregion

            #region 7.feladat
            Console.WriteLine("7. feladat: Jövő évi sorsolások:");

            #endregion
            Console.ReadKey();
        }
    }
}
