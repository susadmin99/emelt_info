using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace cegesauto
{
    class autok
    {
        public byte nap;
        public string ido;
        public string rsz;
        public int szazon;
        public int km;
        public bool kibe;
    }

    class cegesauto
    {
        static List<autok> forgalom = new List<autok>();
        static void Beolvas()
        {
            FileStream fs = new FileStream("autok.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            while (!sr.EndOfStream)
            {
                string[] egysor = sr.ReadLine().Split(' ');
                autok egykibe = new autok();
                egykibe.nap = Convert.ToByte(egysor[0]);
                egykibe.ido = egysor[1];
                egykibe.rsz = egysor[2];
                egykibe.szazon = Convert.ToInt32(egysor[3]);
                egykibe.km = Convert.ToInt32(egysor[4]);
                egykibe.kibe = egysor[5] == "1" ? true : false;
                forgalom.Add(egykibe);
            }
            sr.Close();
            fs.Close();
        }
        static autok utolsoautoki()
        {
            int n = forgalom.Count - 1;
            while (forgalom[n].kibe && n > 0) n--;
            return forgalom[n];
        }
        static List<autok> forgalomadottnapon(byte nap)
        {
            List<autok> eredmeny = new List<autok>();
            foreach (var item in forgalom)
            {
                if (item.nap == nap) eredmeny.Add(item);
            }
            return eredmeny;
        }
        static int[] havikmszum()
        {
            int[] aautokki = new int[10];
            int[] aautokbe = new int[10];
            int[] szumkmauto = new int[10];
            for (int i = 0; i < 10; i++) aautokki[i] = 0;
            foreach (var item in forgalom)
            {
                int auto = Convert.ToInt32(item.rsz.Substring(3, 3)) % 10;
                if (!item.kibe && aautokki[auto] == 0)
                    aautokki[auto] = item.km;
                if (item.kibe)
                    aautokbe[auto] = item.km;
            }
            for (int i = 0; i < 10; i++)
            {
                szumkmauto[i] = aautokbe[i] - aautokki[i];
            }
            return szumkmauto;
        }
        static byte kintlevok()
        {
            byte eredmeny = 0;
            bool[] kint = new bool[10];
            for (int i = 0; i < 10; i++) kint[i] = false;
            foreach (var item in forgalom)
            {
                int auto = Convert.ToInt32(item.rsz.Substring(3, 3)) % 10;
                kint[auto] = item.kibe;
            }
            for (int i = 0; i < 10; i++)
                if (!kint[i]) eredmeny++;
            return eredmeny;
        }
        static string maxut()
        {
            string eredmeny = "";
            List<autok> rszrendezett = forgalom.OrderBy(o => o.rsz).ToList();
            int kmmax = 0;
            int szemely = 0;
            for (int i = 1; i < rszrendezett.Count; i++)
            {
                if (rszrendezett[i].rsz == rszrendezett[i-1].rsz && rszrendezett[i].kibe == true)
                {
                    if (kmmax < rszrendezett[i].km - rszrendezett[i - 1].km)
                    {
                        kmmax = rszrendezett[i].km - rszrendezett[i - 1].km;
                        szemely = rszrendezett[i].szazon; 
                    }
                }
            }
            eredmeny = "Leghosszabb út: " + kmmax.ToString() + " km, személy: " + szemely.ToString();
            return eredmeny;
        }

        static void menetlevelkeszit(string rsz)
        {
            FileStream fs = new FileStream(rsz + "_menetlevel.txt",FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            string kiir = "";
            for (int i = 0; i < forgalom.Count; i++)
            {
                if (forgalom[i].rsz == rsz && !forgalom[i].kibe)
                {
                    kiir = forgalom[i].szazon.ToString() + "\t" + forgalom[i].nap.ToString() + ". " + forgalom[i].ido + "\t" + forgalom[i].km.ToString() +" km";
                    sw.Write(kiir);
                    kiir = "";
                }
                if (forgalom[i].rsz == rsz && forgalom[i].kibe)
                {
                    kiir = "\t" + forgalom[i].nap.ToString() + ". " + forgalom[i].ido + "\t" +
                          forgalom[i].km.ToString() + " km" ;
                    sw.WriteLine(kiir);
                    kiir = "";
                }
            }
            sw.Close();
            fs.Close();
        }

        static void Main(string[] args)
        {
            //1. feladat
            Beolvas();
            
            //2. feladat
            autok utolso = utolsoautoki();   
            Console.WriteLine("2. feladat");
            Console.WriteLine("{0}. nap rendszám: {1}", utolso.nap, utolso.rsz);
            
            //3. feladat
            Console.WriteLine("3. feladat");
            Console.Write("Nap: ");
            byte egynap = Convert.ToByte(Console.ReadLine());
            List<autok> lforgalomegynap = new List<autok>();
            lforgalomegynap = forgalomadottnapon(egynap);
            Console.WriteLine("Forgalom a(z) {0}. napon:", egynap);
            foreach (var item in lforgalomegynap)
            {
                Console.WriteLine("{0} {1} {2} {3}", item.ido, item.rsz, item.szazon, item.kibe ? "be" : "ki");
            }
            
            //4. feladat
            Console.WriteLine("4. feladat");
            Console.WriteLine("A hónap végén {0} autót nem hoztak vissza.", kintlevok());
            //5. feladat
            Console.WriteLine("5. feladat");
            int[] havikm = new int[10];
            havikm = havikmszum();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("CEG3{0} {1} km", i.ToString("00"), havikm[i]);
            }
            
            Console.WriteLine("6. feladat");
            Console.WriteLine(maxut());
            Console.WriteLine("7. feladat");
            Console.Write("Rendszám: ");
            string rendszam = Console.ReadLine();
            menetlevelkeszit(rendszam);
            Console.WriteLine("Menetlevél kész.");
            Console.ReadLine();
        }
    }
}
