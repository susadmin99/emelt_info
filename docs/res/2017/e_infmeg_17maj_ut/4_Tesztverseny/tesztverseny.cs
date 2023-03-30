using System;
using System.IO;

namespace tesztverseny
{
    class Program
    {
        static string megoldas = "";
        struct Egyteszt
        {
            public string azonosito;
            public string valasz;
            public int pont;
        }
        static Egyteszt[] teszt = new Egyteszt[1000];
        static int[] pontok = new int[] { 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 5, 5, 5, 6 };
        static int n = 0;
        static int valaki = 0;

        static void Main(string[] args)
        {
            Feladat1();
            Feladat2();
            Feladat3();
            Feladat4();
            Feladat5();
            Feladat6();
            Feladat7();

            Console.ReadKey();
        }


        static void Feladat7()
        {
            Console.WriteLine("7. feladat: A verseny legjobbjai:");
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n - i - 1; j++)
                {
                    Egyteszt b = new Egyteszt();
                    if (teszt[j].pont < teszt[j + 1].pont)
                    {
                        b = teszt[j];
                        teszt[j] = teszt[j + 1];
                        teszt[j + 1] = b;
                    }
                }
            int dij = 1;
            Console.WriteLine("{0}. díj ({1} pont): {2}", dij, teszt[0].pont, teszt[0].azonosito);

            for (int k = 1; k < n; k++)
            {
                if (teszt[k].pont < teszt[k - 1].pont)
                    dij++;
                if (dij <= 3)
                    Console.WriteLine("{0}. díj ({1} pont): {2}", dij, teszt[k].pont, teszt[k].azonosito);
            }
        }

        static void Feladat6()
        {
            Console.WriteLine("6. feladat: A versenyzők pontszámának meghatározása");
            for (int i = 0; i < n; i++)
                for (int j = 0; j < 14; j++)
                    if (teszt[i].valasz[j] == megoldas[j])
                        teszt[i].pont += pontok[j];
            StreamWriter sw = new StreamWriter("../pontok.txt");
            for (int i = 0; i < n; i++)
                sw.WriteLine("{0} {1}", teszt[i].azonosito, teszt[i].pont);
            sw.Close();
        }

        static void Feladat5()
        {
            Console.Write("5. feladat: A feladat sorszáma = ");
            int x = Convert.ToInt32(Console.ReadLine()) - 1;
            int db = 0;
            for (int i = 0; i < n; i++)
                if (teszt[i].valasz[x] == megoldas[x])
                    db++;
            double sz = 100 * Convert.ToDouble(db) / Convert.ToDouble(n);
            Console.WriteLine("A feladatra {0} fő, a versenyzők {1}%-a adott helyes választ.", db, sz.ToString("F2"));
        }

        static void Feladat4()
        {
            Console.WriteLine("4. feladat:");
            Console.WriteLine(megoldas + " (a helyes megoldás)");
            for (int i = 0; i < 14; i++)
                if (teszt[valaki].valasz[i] == megoldas[i])
                    Console.Write("+");
                else
                    Console.Write(" ");
            Console.WriteLine(" (a versenyző helyes válaszai)");
        }

        static void Feladat3()
        {
            Console.Write("3. feladat: A versenyző azonosítója = ");
            string s = Console.ReadLine();
            while (valaki < n && s != teszt[valaki].azonosito)
                valaki++;
            Console.WriteLine(teszt[valaki].valasz + " (a versenyző válasza)");
        }

        static void Feladat2()
        {
            Console.WriteLine("2. feladat: A vetélkedőn {0} versenyző indult.", n);
        }

        static void Feladat1()
        {
            Console.WriteLine("1. feladat: Az adatok beolvasása");
            StreamReader sr = new StreamReader("../valaszok.txt");
            string[] sor = new string[2];

            megoldas = sr.ReadLine();
            while (sr.Peek() > -1)
            {
                sor = sr.ReadLine().Split(' ');
                teszt[n].azonosito = sor[0];
                teszt[n].valasz = sor[1];
                n++;
            }
        }
    }
}
