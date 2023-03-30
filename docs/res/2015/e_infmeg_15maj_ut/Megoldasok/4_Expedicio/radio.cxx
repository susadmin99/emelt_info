#include <iostream>
#include <fstream>
#include <sstream>
using namespace std;

struct uzenet{
	int nap, segito;
	string szoveg;
};
void f(int feladat)
{
	cout << endl << feladat << ". feladat: " << endl;
}
bool szame(string szo)
{
	bool valasz=true;
	for(unsigned int i=0; i<szo.size(); i++)
	{
		if((szo[i]<'0') or (szo[i]>'9')) { valasz=false; };
	}
	return valasz;
}
int main(int argc, char **argv)
{
	// 1. feladat
	f(1);
	ifstream be;
	uzenet u[250];
	int db=0;
	string sorveg;
	be.open("veetel.txt");
	while(be >> u[db].nap >> u[db].segito)
	{
		getline(be, sorveg);
		getline(be, u[db].szoveg);
		db++;
	}
	be.close();
	// 2. feladat
	f(2);
	cout << "Az elso uzenet rogzitoje: " << u[0].segito << endl;
	cout << "Az utolso uzenet rogzitoje: " << u[db-1].segito << endl;
	// 3. feladat
	f(3);
	for(int i=0; i<db; i++)
	{
		if(u[i].szoveg.find("farkas")!=string::npos)
		{
			cout << u[i].nap << ". nap " << u[i].segito << ". radioamator" << endl;
		}
	}
	// 4. feladat
	f(4);
	int nap[12]={0};
	for(int i=0; i<db; i++)
	{
		nap[u[i].nap]++;
	}
	for(int n=1; n<=11; n++)
	{
		cout << n << ". nap: "<< " " << nap[n] << " radioamator"<< endl;
	}
	// 5. feladat
	f(5);
	ofstream ki;
	ki.open("adaas.txt");
	string joszoveg[12];
	for(int n=1; n<=11; n++)
	{
		for(int j=0; j<90; j++)
		{
			joszoveg[n]=joszoveg[n]+'#';
		}
	}
	for(int i=0; i<db; i++)
	{
		for(unsigned int j=0; j<u[i].szoveg.size(); j++)
		{
			if(u[i].szoveg[j]!='#')
			{
				joszoveg[u[i].nap][j]=u[i].szoveg[j];
			}
		}
	}
	for(int n=1; n<=11; n++)
	{
		ki << joszoveg[n] << endl;
	}
	ki.close();
	// 7 feladat
	f(7);
	int melynap, melyfigyelo;
	cout << "Adja meg a nap sorszamat! ";
	cin >> melynap;
	cout << "Adja meg a radioamator sorszamat! ";
	cin >> melyfigyelo;
	stringstream puffer, puffer2;
	string szoveg="";
	for(int i=0; i<db; i++)
	{
		if( (u[i].nap==melynap) and (u[i].segito==melyfigyelo) )
		{
			szoveg=u[i].szoveg;
			puffer << u[i].szoveg;
		}
	}
	if(szoveg.size()>0)
	{
		string szo1, szo2;
		getline(puffer, szo1, '/');
		getline(puffer, szo2, ' ');
		int szam1, szam2;
		if(szame(szo1) and szame(szo2))
		{
			puffer2 << szo1 <<" " << szo2;
			puffer2 >> szam1 >> szam2;
			cout << "A megfigyelt egyedek szama: " << szam1+szam2 << endl;
		}
		else
		{
			cout << "Nincs informacio." << endl;
		}
	}
	else
	{
		cout << "Nincs ilyen feljegyzes." << endl;
	}
	return 0;
}

