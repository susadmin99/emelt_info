SELECT nev, ev, elhunyt
FROM tag, tagsag
WHERE tag.id=tagid
AND ev<=( ... )
AND (elhunyt>=( ... ) OR ... )
AND tipus='t'; 
