program cimek;
uses crt;

Type csopSTR = string[4];
     sorSTR  = string[39];

Type ipREC = record
   sor      : sorSTR;
   csoport  : array [1..8] of csopSTR;
   rcsoport : array [1..8] of csopSTR;
   nulla    : byte;
   R        : array [1..8] of byte;
end; {ip}

Var N : integer;
    ip : array [1..500] of ipREC;
    legkisebb : sorStr;

procedure f(sz:byte);
begin {f}
  WriteLn; WriteLn (sz, '. feladat:');
end; {f}

procedure Levag (a: csopSTR; var b: csopSTR);
var i: byte;
begin
  i := 1;
  b := '';
  While (i<4) AND (a[i]='0') do inc(i);
  While (i<=4) do
    begin
      b:= b + a[i];
      inc(i);
    end;
end; {Levag}

procedure Darabol (a: integer);
var i : byte;
begin
  With ip[N] do
    for i := 1 to 8 do
      begin
        csoport[i]  := copy(sor, i*5-4, i*5-1);
        Levag (csoport[i], rcsoport[i]);
      end; {for i}
end; {Darabol}

procedure Megszamol (a: integer);
var i : byte;
begin
  With ip[a] do
    begin
      nulla := 0;
      for i := 1 to 39 do
        if sor[i] = '0' then inc (nulla);
    end; {with ip[a]}
end; {Megszamol}

procedure f1;
var fv : textfile;
    s  : sorSTR;
begin {f1}
  N :=0;
  legkisebb := 'ff';
  Assign (fv, 'ip.txt');
  Reset (fv);
  While not eof(fv) do
    begin
      inc(N);
      ReadLn (fv, ip[N].sor);
      Darabol (N);
      if legkisebb>ip[N].sor then legkisebb := ip[N].sor;
      Megszamol (N);
    end; {not eof}
  Close (fv);
end; {f1}

procedure f2;
begin {f2}
  WriteLn ('Az  llom nyban ', N, ' darab adatsor van.');
end; {f2}

procedure f3;
begin {f3}
  WriteLn ('A legalacsonyabb t rolt IP-c¡m: ', legkisebb);
end; {f3}

procedure f4;
var d, g, l : integer;
    i : Integer;
begin {f4}
  d := 0; g := 0; l := 0;
  for i := 1 to N do
    with ip[i] do
      begin
        if copy(sor, 1, 9) = '2001:0db8' then inc(d);
        if copy(sor, 1, 7) = '2001:0e' then inc(g);
        if (copy(sor, 1, 2) = 'fc') OR (copy(sor, 1, 2) = 'fd')then inc(l);
      end; {with ip[i]; for i}
  WriteLn('Dokument ci¢s c¡m: ', d, ' darab');
  WriteLn('Glob lis egyedi c¡m: ', g, ' darab');
  WriteLn('Helyi egyedi c¡m: ', l, ' darab');
end; {f4}

procedure f5;
var fv : textfile;
    i  : integer;
begin {f5}
  Assign (fv, 'sok.txt');
  ReWrite (fv);
    for i:= 1 to N do
      With ip[i] do
        if nulla>=18 then WriteLn (fv, i, '  ', sor);
  Close(fv);
end; {f5}

var SSZ : integer;

procedure f6;
var i : integer;
begin {f6}
  Write ('K‚rek egy sorsz mot: ');
  ReadLn (SSZ);
  WriteLn (ip[SSZ].sor);
  for i := 1 to 7 do Write (ip[SSZ].rcsoport[i],':');
  WriteLn (ip[SSZ].rcsoport[8]);
end; {f6}

procedure f7;
var i   : integer;
    van : boolean;
    max : integer;
begin {f7}
  van := false;
  With ip[SSZ] do
    begin
      R[1] := 0;
      For i := 2 to 8 do
        if rcsoport[i]='0'
          then
            begin R[i] := R[i-1]+1; van := true; end
          else
            R[i] := 0;
      if van
        then
          begin
            max := 1;
            For i := 2 to 8 do
              if R[i]>R[max] then max := i;
            For i := 1 to max-R[max] do Write (rcsoport[i], ':');
            For i := max+1 to 8 do Write (':', rcsoport[i]);
            WriteLn;
          end
        else
          WriteLn ('Nem r”vid¡thet‹ tov bb');
    end; {with ip[SSZ]}
end; {f7}


BEGIN
  ClrScr;
  f1;
  f(2);
  f2;
  f(3);
  f3;
  f(4);
  f4;
  f5;
  f(6);
  f6;
  f(7);
  f7;
  ReadKey;
END.