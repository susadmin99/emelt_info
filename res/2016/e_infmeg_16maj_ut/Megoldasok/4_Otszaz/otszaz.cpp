#include <iostream>
#include <fstream>
using namespace std;
struct Tcikk{
	int vasarlo;
	string nev;
};
int ertek(int db)
{
	if(db<=2) { return db*500-(db-1)*50; }
	else { return 1350+(db-3)*400; }
}
int main()
{
	// cout << "1. feladat" << endl;
	Tcikk cikk[1000];
	ifstream be("penztar.txt");
	int db=0;
	while(getline(be, cikk[db].nev))
	{
		db++;
	}
	be.close();

	cout << endl << "2. feladat" << endl;
	int sorszam=1;
	for(int i=0; i<db; i++)
	{
		cikk[i].vasarlo=sorszam;
		if(cikk[i].nev=="F")
		{
			cikk[i].nev="";
			sorszam++;
		}
	}
	cout << "A fizetesek szama: " << sorszam-1 << endl;

	cout << endl << "3. feladat" << endl;
	int elsodb=0;
	for(int i=0; i<db; i++)
	{
		if(cikk[i].vasarlo==1)
		{
			elsodb++;
		}
	}
	cout << "Az elso vasarlo " << elsodb-1 << " darab arucikket vasarolt." << endl;

	cout << endl << "4. feladat" << endl;
	int vasarlo, darabszam;
	string arucikk, sorveg;
	cout << "Adja meg egy vasarlas sorszamat! ";
	cin >> vasarlo;
	getline(cin, sorveg);
	cout << "Adja meg egy arucikk nevet! ";
	getline(cin, arucikk);
	cout << "Adja meg a vasarolt darabszamot! ";
	cin >> darabszam;
	getline(cin, sorveg);

	cout << endl << "5. feladat" << endl;
	int elso=1000, utolso=0, hanyan=0;
	for(int i=0; i<db; i++)
	{
		if(cikk[i].nev==arucikk)
		{
			if(cikk[i].vasarlo>utolso) { hanyan++; }
			elso=min(elso, cikk[i].vasarlo);
			utolso=max(utolso, cikk[i].vasarlo);
		}
	}
	cout << "Az elso vasarlas sorszama: " << elso << endl;
	cout << "A utolso vasarlas sorszama: " << utolso << endl;
	cout << hanyan << " vasarlas soran vettek belole." << endl;

	cout << endl << "6. feladat" << endl;
	cout << darabszam << " darab vetelekor fizetendo: " << ertek(darabszam) << endl;

	cout << endl << "7. feladat" << endl;
	// egyben a 8. feladat megoldása
	for(int i=0; i<db-1; i++)
	{
		for(int j=0; j<db-1; j++)
		{
			if( (cikk[j].vasarlo>cikk[j+1].vasarlo) or (cikk[j].vasarlo==cikk[j+1].vasarlo) and (cikk[j].nev<=cikk[j+1].nev) )
			{
				swap(cikk[j], cikk[j+1]);
			}
		}
	}
	ofstream ki("osszeg.txt");
	int fizet=0;
	int aktdb=1;
	for(int i=1; i<db; i++)
	{
		if(cikk[i].nev==cikk[i-1].nev)
		{
			aktdb++;
		}
		else
		{
			if(cikk[i-1].nev!="")
			{
				fizet+=ertek(aktdb);
				if(cikk[i-1].vasarlo==vasarlo)
				{
					cout << aktdb << " " << cikk[i-1].nev << endl;
				}
			}
			aktdb=1;
		}
		if(cikk[i].nev=="")
		{
			ki << cikk[i].vasarlo << ": " << fizet << endl;
			fizet=0;
		}
	}
	ki.close();
    return 0;
}
