using System;
using System.IO;

namespace valasztas
{
    class valasztas
    {
        struct EgyKepviselo
        {
            public string vnev, unev, korzet, part;
            public int szavazat;
        }
        static EgyKepviselo[] kepviselo = new EgyKepviselo[100];
        static int n;
        static float osszesszavazat = 0;
        const float valasztokszama = 12345;

        static void Main(string[] args)
        {
            Feladat1();
            Console.WriteLine();
            Feladat2();
            Console.WriteLine();
            Feladat3();
            Console.WriteLine();
            Feladat4();
            Console.WriteLine();
            Feladat5();
            Console.WriteLine();
            Feladat6();
            Console.WriteLine();
            Feladat7();
            Console.ReadKey();
        }

        static void Feladat1()
        {
            StreamReader olvaso = new StreamReader("szavazatok.txt");
            string[] egysor = new string[5];

            Console.WriteLine("1. feladat. Az adatok beolvasása");
            n = 0;
            while (olvaso.Peek() > -1)
            {
                egysor = olvaso.ReadLine().Split(' ');
                kepviselo[n].korzet = egysor[0];
                kepviselo[n].szavazat = int.Parse(egysor[1]);
                kepviselo[n].vnev = egysor[2];
                kepviselo[n].unev = egysor[3];
                kepviselo[n].part = egysor[4];
                n++;
            }
        }

        static void Feladat2()
        {
            Console.WriteLine("2. feladat. A helyhatósági választáson {0} képviselőjelölt indult.", n);
        }

        static void Feladat3()
        {
            Console.WriteLine("3. feladat. Egy képviselő");
            Console.Write("vezetékneve=");
            string veznev = Console.ReadLine();
            Console.Write("utóneve=");
            string utonev = Console.ReadLine();
            bool szerepel = false;

            for (int i = 0; i < n; i++)
            {
                if (kepviselo[i].vnev == veznev && kepviselo[i].unev == utonev)
                {
                    Console.WriteLine("{0} {1} képviselőjelölt {2} szavazatot kapott.", veznev, utonev, kepviselo[i].szavazat);
                    szerepel = true;
                }
            }
            if (!szerepel) Console.WriteLine("Ilyen nevű képviselőjelölt nem szerepel a nyilvántartásban!"); 
        }

        static void Feladat4()
        {
            for (int i = 0; i < n; i++)
                osszesszavazat += kepviselo[i].szavazat;
            float arany = osszesszavazat / valasztokszama * 100;
            Console.WriteLine("4. feladat. A választáson {0} állampolgár, a jogosultak {1}%-a vett részt.", osszesszavazat, arany.ToString("F2"));
        }

        static void Feladat5()
        {
            float gyep = 0;
            float hep = 0;
            float tisz = 0;
            float zep = 0;
            float flen = 0;

            for (int i = 0; i < n; i++)
            {
                if (kepviselo[i].part == "GYEP") gyep += kepviselo[i].szavazat;
                if (kepviselo[i].part == "HEP") hep += kepviselo[i].szavazat;
                if (kepviselo[i].part == "TISZ") tisz += kepviselo[i].szavazat;
                if (kepviselo[i].part == "ZEP") zep += kepviselo[i].szavazat;
                if (kepviselo[i].part == "-") flen += kepviselo[i].szavazat;
            }

            Console.WriteLine("5. feladat. Az egyes pártokra leadott szavazatok aránya:");
            Console.WriteLine("Gyümölcsevők Pártja = {0}%", (100 * gyep / osszesszavazat).ToString("F2"));
            Console.WriteLine("Húsevők Pártja = {0}%", (100 * hep / osszesszavazat).ToString("F2"));
            Console.WriteLine("Tejivók Szövetsége = {0}%", (100 * tisz / osszesszavazat).ToString("F2"));
            Console.WriteLine("Zöldségevők Pártja = {0}%", (100 * zep / osszesszavazat).ToString("F2"));
            Console.WriteLine("Független jelöltek = {0}%", (100 * flen / osszesszavazat).ToString("F2"));
        }

        static void Feladat6()
        {
            int max = kepviselo[0].szavazat;
            for (int i = 1; i < n; i++)
                if (max < kepviselo[i].szavazat)
                {
                    max = kepviselo[i].szavazat;
                }

            Console.WriteLine("6. feladat. A legtöbb szavazatot kapott képviselő(k):");
            for (int i = 0; i < n; i++)
                if (kepviselo[i].szavazat == max)
                {
                    Console.Write(kepviselo[i].vnev + " " + kepviselo[i].unev + " ");
                    if (kepviselo[i].part == "-")
                    {
                        Console.WriteLine("független");
                    }
                    else
                    {
                        Console.WriteLine(kepviselo[i].part);
                    }
                }
        }

        static void Feladat7()
        {
            StreamWriter iro = new StreamWriter("kepviselok.txt");
            Console.WriteLine("7. feladat. A választás eredményének kiírása");

            for (int i = 1; i <= 8; i++)
            {
                bool elso = true;
                int max = 0;
                int maxh = 0;
                for (int j = 0; j < n; j++)
                    if (kepviselo[j].korzet == i.ToString())
                    {
                        if (elso)
                        {
                            maxh = j;
                            max = kepviselo[j].szavazat;
                            elso = false;
                        }
                        else
                        {
                            if (kepviselo[j].szavazat > max)
                            {
                                maxh = j;
                                max = kepviselo[j].szavazat;
                            }
                        }
                    }
                iro.Write(i.ToString() + " " + kepviselo[maxh].vnev + " " + kepviselo[maxh].unev + " ");
                if (kepviselo[maxh].part == "-")
                {
                    iro.WriteLine("független");
                }
                else
                {
                    iro.WriteLine(kepviselo[maxh].part);
                }
            }
            iro.Close();
        }
    }
}
