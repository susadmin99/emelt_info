#include <iostream>
#include <fstream>
using namespace std;
struct Tmozgas{
	int id;
	string irany;
	int ido, ora, perc;
};
int main()
{
    Tmozgas mozgas[1000];
    // 1. feladat
    ifstream be("ajto.txt");
    int i=0;
    while(be >> mozgas[i].ora >> mozgas[i].perc >> mozgas[i].id >> mozgas[i].irany)
	{
		mozgas[i].ido=mozgas[i].ora*60+mozgas[i].perc;
		i++;
	}
	int db=i;
	be.close();

    cout << endl << "2. feladat" << endl;
    int utolso=0;
    for(int i=0; i<db; i++)
	{
		if(mozgas[i].irany=="ki")
		{
			utolso=i;
		}
	}
	cout << "Az elso belepo: " << mozgas[0].id << endl;
	cout << "Az utolso kilepo: " << mozgas[utolso].id << endl;

	// 3. feladat
	int athaladt[101]={0};
	for(int i=0; i<db; i++)
	{
		athaladt[mozgas[i].id]++;
	}
	ofstream ki("athaladas.txt");
	for(int i=1; i<=100; i++)
	{
		if(athaladt[i]>0)
		{
			ki << i << " " << athaladt[i] << endl;
		}
	}
	ki.close();

	cout << endl << "4. feladat" << endl;
	cout << "A vegen a tarsalgoban voltak:";
	for(int i=1; i<=100; i++)
	{
		if(athaladt[i]%2==1) cout << " " << i;
	}
	cout << endl;

    cout << endl << "5. feladat" << endl;
    int mikor=0, letszam=0, maxletszam=0;
    for(int i=0; i<db; i++)
	{
		if(mozgas[i].irany=="be") letszam++;
		else letszam--;
		if(letszam>maxletszam)
		{
			maxletszam=letszam;
			mikor=i;
		}
	}
	cout << "Peldaul " << mozgas[mikor].ora << ":" << mozgas[mikor].perc << "-kor voltak a legtobben a tarsalgoban." << endl;

    cout << endl << "6. feladat" << endl;
    int sz;
    cout << "Adja meg a szemely azonositojat! ";
    cin >> sz;

    cout << endl << "7. feladat" << endl;
    int hanyszor=0;
    for(int i=0; i<db; i++)
	{
		if(mozgas[i].id==sz)
		{
			if(mozgas[i].irany=="be") cout << mozgas[i].ora << ":" << mozgas[i].perc << "-";
			else cout << mozgas[i].ora << ":" << mozgas[i].perc << endl;
		}
	}
	cout << endl;

    cout << endl << "8. feladat" << endl;
    int hossz=0;
    string merre;
    for(int i=0; i<db; i++)
	{
		if(mozgas[i].id==sz)
		{
			merre=mozgas[i].irany;
			if(merre=="be") hossz-=mozgas[i].ido;
			else hossz+=mozgas[i].ido;
		}
	}
	if(merre=="be")
	{
		hossz+=15*60;
		cout << "A(z) " << sz << ". szemely osszesen " << hossz << " percet volt bent, a megfigyeles vegen a tarsalgoban volt." << endl;
	}
	else
	{
		cout << "A(z) " << sz << ". szemely osszesen " << hossz << " percet volt bent, a megfigyeles vegen nem volt a tarsalgoban." << endl;
	}

    return 0;
}
