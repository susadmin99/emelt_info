bemenet = open("melyseg.txt")
mélységek = []
for sor in bemenet:
    mélységek.append(int(sor.strip()))

print("1. feladat")
print(f'A fájl adatainak száma: {len(mélységek)}')

print("2. feladat")
hely = int(input("Adjon meg egy távolságértéket! "))
print(f'Ezen a helyen a felszín {mélységek[hely-1]} méter mélyen van.')

print("3. feladat")
érintetlen = 0
for mért in mélységek:
    if mért == 0:
        érintetlen += 1
print("Az érintetlen terület aránya {0:4.2f}%.".format(100*érintetlen/len(mélységek)))

kimenet = open("godrok.txt", "w")
előző = 0
egysor = []
sorok = []
for érték in mélységek:
    if érték > 0:
        egysor.append(str(érték))
    if érték == 0 and előző > 0:
        sorok.append(egysor)
        egysor = []
    előző = érték
for egysor in sorok:
    print(" ".join(egysor), file=kimenet)
kimenet.close()

print("5. feladat")
print(f'A gödrök száma: {len(sorok)}')

print("6. feladat")
if mélységek[hely-1] > 0:
    print("a)")
    poz = hely-1
    while mélységek[poz] > 0:
        poz -= 1
    kezdő = poz+2
    poz = hely
    while mélységek[poz] > 0:
        poz += 1
    záró = poz
    print(f'A gödör kezdete: {kezdő} méter, a gödör vége: {záró} méter.')

    print("b)")
    mélypont = 0
    poz = kezdő
    while mélységek[poz] >= mélységek[poz-1] and poz <= záró:
        poz += 1
    while mélységek[poz] <= mélységek[poz-1] and poz <= záró:
        poz += 1
    if poz > záró:
        print("Folyamatosan mélyül.")
    else:
        print("Nem mélyül folyamatosan.")

    print("c)")
    print(f'A legnagyobb mélysége {max(mélységek[kezdő-1:záró])} méter.')

    print("d)")
    térfogat = 10*sum(mélységek[kezdő-1:záró])
    print(f'A térfogata {térfogat} m^3.')

    print("e)")
    biztonságos = térfogat-10*(záró-kezdő+1)
    print(f'A vízmennyiség {biztonságos} m^3.')
else:
    print("Az adott helyen nincs gödör.")
