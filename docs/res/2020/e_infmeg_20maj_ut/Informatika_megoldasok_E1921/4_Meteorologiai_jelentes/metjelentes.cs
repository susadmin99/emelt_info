using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace metjelentes
{
    class metjelentes
    {
        static List<string> metadatok = new List<string>();

        static List<string> beolvas(string fajlnev)
        {
            List<string> adatok = new List<string>(File.ReadAllLines(fajlnev));
            return adatok;
            
        }
        
        static string telepulesKeres(string varos)
        {
            string soradat = "";
            int i = metadatok.Count-1;
            while ((i>=0) && (metadatok[i].Split(' ')[0] != varos) )
            {
                i--;
            }
            if (i > 0)
            {
                soradat = metadatok[i];
            }
            return soradat;
        }
        
        static List<string> telepulesKigyujt()
        {
            List<string> varosok = new List<string>();
            for (int i = 0; i < metadatok.Count ; i++)
            {
                if (!varosok.Contains(metadatok[i].Split(' ')[0]))
                {
                    varosok.Add(metadatok[i].Split(' ')[0]);
                }
            }
            return varosok;
        }

        static string homerseklet(string soradat)
        {
            return soradat.Split(' ')[3];
        }
        
        static string idopont(string soradat)
        {
            return soradat.Split(' ')[1].Substring(0, 4).Insert(2, ":");
        }
        
        static List<int> homIdoVaros(string ido, string varos)
        {
            List<int> homAdatok = new List<int>();
            for (int i = 0; i < metadatok.Count-1; i++)
            {
                if ((idopont(metadatok[i]).Substring(0, 2) == ido) && (metadatok[i].Split(' ')[0] == varos))
                {
                    homAdatok.Add(Convert.ToInt32(homerseklet(metadatok[i])));
                }
            }
            return homAdatok; 
        }
        
        static int minHomSor()
        {
            int min = 0;
            for (int i = 1; i < metadatok.Count - 1; i++)
            {
                if (Convert.ToInt32(homerseklet(metadatok[min])) > Convert.ToInt32(homerseklet(metadatok[i])))
                {
                    min = i;
                }
            }
            return min;
        }
        
        static int minHomSor(string varos)
        {
            int min = 0;
            for (int i = 1; i < metadatok.Count - 1; i++)
            {
                if ( varos == metadatok[i].Substring(0,2)  && (Convert.ToInt32(homerseklet(metadatok[min])) > Convert.ToInt32(homerseklet(metadatok[i]))))
                {
                    min = i;
                }
            }
            return min;
        }
        
        static int maxHomSor()
        {
            int max = 0;
            for (int i = 1; i < metadatok.Count - 1; i++)
            {
                if (Convert.ToInt32(homerseklet(metadatok[max])) < Convert.ToInt32(homerseklet(metadatok[i])))
                {
                    max = i;
                }
            }
            return max;
        }
        
        static int maxHomSor(string varos)
        {
            int max = 0;
            for (int i = 1; i < metadatok.Count - 1; i++)
            {
                if (varos == metadatok[i].Substring(0, 2) && (Convert.ToInt32(homerseklet(metadatok[max])) < Convert.ToInt32(homerseklet(metadatok[i]))))
                {
                    max = i;
                }
            }
            return max;
        }
        
        static int kozepHomTelepules(string varos)
        {
            int kozephom = 200;
            List<int> kozephomAdatok = new List<int>();
            if (homIdoVaros("01", varos).Count != 0 &&
                homIdoVaros("07", varos).Count != 0 &&
                homIdoVaros("13", varos).Count != 0 &&
                homIdoVaros("19", varos).Count != 0 )
            {
                kozephomAdatok.AddRange(homIdoVaros("01", varos));
                kozephomAdatok.AddRange(homIdoVaros("07", varos));
                kozephomAdatok.AddRange(homIdoVaros("13", varos));
                kozephomAdatok.AddRange(homIdoVaros("19", varos));
                int szum = 0;
                foreach (var item in kozephomAdatok)
                {
                    szum += item;
                }
                kozephom = Convert.ToInt32(Math.Round(((float)szum / kozephomAdatok.Count), 0));
            }
            return kozephom;
        }

        static int homIngadoz(string varos)
        {
            int min = Convert.ToInt32(homerseklet(metadatok[minHomSor(varos)]));
            int max = Convert.ToInt32(homerseklet(metadatok[maxHomSor(varos)]));
            return max - min;
        }

        static List<string> szelcsend()
        {
            List<string> szelcsendesek = new List<string>();
            foreach (var item in metadatok)
            {
                if (item.Split(' ')[2] == "00000")
                    szelcsendesek.Add(item);
            }
            return szelcsendesek;
        }

        static void Main(string[] args)
        {
            // 1. feladat
            metadatok = beolvas("tavirathu13.txt");

            //2. feladat
            Console.WriteLine("2. feladat");
            Console.Write("Adja meg egy település kódját! Város: ");
            string varos = Console.ReadLine();
            string soradat = telepulesKeres(varos);
            string ido = idopont(soradat);
            Console.WriteLine("Az utolsó mérési adat a megadott településről {0}-kor érkezett.",ido);
            
            //3. feladat
            Console.WriteLine("3. feladat");
            int min = minHomSor();
            int max = maxHomSor();
            Console.WriteLine("A legalacsonyabb hőmérséklet: {0} {1} {2} fok.", metadatok[min].Split(' ')[0], idopont(metadatok[min]), homerseklet(metadatok[min]));
            Console.WriteLine("A legmagasabb hőmérséklet: {0} {1} {2} fok.", metadatok[max].Split(' ')[0], idopont(metadatok[max]), homerseklet(metadatok[max]));
            
            //4. feladat
            Console.WriteLine("4. feladat");
            List<string> szelcsendesek = szelcsend();
            if (szelcsendesek.Count == 0)
            {
                Console.WriteLine("Nem volt szélcsend a mérések idején.");
            }
            else
	        {
                for (int i = 0; i < szelcsendesek.Count; i++)
			    {
			        Console.WriteLine("{0} {1}", szelcsendesek[i].Split(' ')[0],idopont(szelcsendesek[i]));
			    }
	        }

            //5. feladat
            Console.WriteLine("5. feladat");
            List<string> telepulesek = telepulesKigyujt();
            foreach (var item in telepulesek)
            {
                if (kozepHomTelepules(item) == 200)
                {
                    Console.Write("{0} NA; ", item);
                }
                else
                {
                    Console.Write("{0} Középhőmérséklet: {1}; ", item,kozepHomTelepules(item));
                }

                Console.WriteLine("Hőmérséklet-ingadozás: {0}", homIngadoz(item));
            }            

            //6. feladat
            Console.WriteLine("6. feladat");
            foreach (var telepules in telepulesek)
            {
                FileStream fs = new FileStream(telepules+".txt",FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(telepules);
                foreach (var egysor in metadatok)
                {
                    if (egysor.Split(' ')[0] == telepules)
                    {
                        int szelero = Convert.ToInt32(egysor.Split(' ')[2].Substring(3, 2));
                        sw.Write("{0} ", idopont(egysor));
                        for (int i = 1; i <= szelero; i++)
                        {
                            sw.Write("#");
                        }
                        sw.WriteLine();
                    }
                }
                sw.Close();
                fs.Close();
            }
            Console.Write("A fájlok elkészültek.");
            Console.ReadLine();
        }
    }
}
