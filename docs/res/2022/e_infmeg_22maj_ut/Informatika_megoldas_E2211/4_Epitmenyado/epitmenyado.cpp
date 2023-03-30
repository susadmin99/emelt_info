#include <iostream>
#include <fstream>
#include <vector>

using namespace std;

struct Tta
{
    int adoszam;
    string utcanev;
    string haszszam;
    string sav;
    int alapter;
 };

 vector <Tta> ta(0);
 int sA=0;
 int sB=0;
 int sC=0;

 struct Tfiz
 {
     int adoszam;
     int osszeg;
 };

 vector <Tfiz> fiz(0);

 void f1();
 void f2();
 void f3();
 int ado(string s, int t);
 void f5();
 void f6();
 void f7();

int main()
{
    f1();
    f2();
    f3();
    f5();
    f6();
    f7();

    return 0;
}

void f1()
{
    cout << "1. feladat. Adatok beolvasasa" << endl;
    ifstream be("utca.txt");
    be >> sA >> sB >> sC;
    Tta x;
    while (be >> x.adoszam >> x.utcanev >> x.haszszam >> x.sav >> x.alapter)
            ta.push_back(x);
    be.close();
}

void f2()
{
    cout << "2. feladat. A mintaban " << ta.size() << " telek szerepel." << endl;
}

void f3()
{
    cout << "3. feladat. Egy tulajdonos adoszama:";
    int asz=0;
    cin >> asz;
    bool volt=false;
    for (Tta x:ta)
        if (x.adoszam==asz)
        {
            cout << x.utcanev << " utca " << x.haszszam << endl;
            volt=true;
        }
    if (!volt)
        cout << "Nem szerepel az adatallomanyban." << endl;
}

int ado(string s, int t)
{
    int x=0;
    if (s=="A") x=sA*t;
    if (s=="B") x=sB*t;
    if (s=="C") x=sC*t;
    if (x<10000) x=0;
    return x;
}

void f5()
{
    int dbA=0;
    int dbB=0;
    int dbC=0;
    int adoA=0;
    int adoB=0;
    int adoC=0;
    for (Tta x:ta)
    {
        if (x.sav=="A")
        {
            dbA++;
            adoA+=ado(x.sav, x.alapter);
        }
        if (x.sav=="B")
        {
            dbB++;
            adoB+=ado(x.sav, x.alapter);
        }
        if (x.sav=="C")
        {
            dbC++;
            adoC+=ado(x.sav, x.alapter);
        }
    }
    cout << "5. feladat" << endl;
    cout << "A savba " << dbA << " telek esik, az ado " << adoA << " Ft."<< endl;
    cout << "B savba " << dbB << " telek esik, az ado " << adoB << " Ft."<< endl;
    cout << "C savba " << dbC << " telek esik, az ado " << adoC << " Ft."<< endl;
}

void f6()
{
    cout << "6. feladat. A tobb savba sorolt utcak:" << endl;
    string vutca=ta[0].utcanev;
    string vsav=ta[0].sav;
    bool tobbsav=false;
    for (int i=1; i<ta.size(); i++)
        if (ta[i].utcanev != vutca)
        {
            if (tobbsav)
                cout << vutca << endl;
            vutca=ta[i].utcanev;
            vsav=ta[i].sav;
            tobbsav=false;
        }
        else
        {
            if (vsav!=ta[i].sav)
                tobbsav=true;
        }
}

void f7()
{
    cout << "7. feladat. A fizetendo.txt allomany letrehozasa";
    for (int i=0; i<ta.size(); i++)
    {
        bool volt=false;
        for (int j=0; j<fiz.size(); j++)
            if (ta[i].adoszam==fiz[j].adoszam)
            {
                volt=true;
                fiz[j].osszeg+=ado(ta[i].sav,ta[i].alapter);
            }

        if (volt==false)
        {
            Tfiz y;
            y.adoszam=ta[i].adoszam;
            y.osszeg=ado(ta[i].sav,ta[i].alapter);
            fiz.push_back(y);
        }
    }

    ofstream ki("fizetendo.txt");

    for (int i=0; i<fiz.size(); i++)
        ki << fiz[i].adoszam << " " << fiz[i].osszeg << endl;

    ki.close();

}
