SELECT indulasi.vonalid, ???.nev, ???.nev
FROM 
	(
		SELECT nev, vonalid 
			FROM allomas, hely 
			WHERE allomas.id=allomasid 
			AND ???
	) AS indulasi,
	(
		SELECT nev, vonalid, tav 
			FROM allomas, hely 
			WHERE allomas.id=allomasid
	) AS veg,
	(
		SELECT vonalid, Max(tav) ??? 
			FROM hely 
			GROUP BY vonalid
	) AS tulso
WHERE indulasi.vonalid=veg.vonalid
	AND veg.vonalid=tulso.vonalid
	AND veg.??? =tulso.maxtav;
