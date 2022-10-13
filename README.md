ForumAPI

1. SPRENDŽIAMO UŽDAVINIO APRAŠYMAS
	1.1. Sistemos paskirtis
	Projekto Tikslas – sukurti socialinį forumą, kuriame žmonės galėtų susirašinėti apie bendrai dominančias temas ar įvykius.
	Veikimo principas – forumas sudarytas iš trijų dalių: vartotojo sąsajos, kuria naudosis svečiai, registruoti vartotojai, administratoriai,
	programinės sąsajos (angl. trumpai API), bei duomenų bazės.

	Registruoti vartotojai kurtų naujus žinutes („Posts“) naujuose, pačių sukurtuose, arba jau esamuose „Thread“, kurie yra sukurti tam tikruose „Topic”. 
	Administratoriai būtų atsakingi už naujų „Topic“ kūrimą, bei prižiūrėtų vartotojų, kuriamus „Post“ ir „Thread“. Svečiai gali tiktai peržiūrėti
	egzistuojančius „Post“, „Thread“ ir „Topic“.
	1.2. Funkciniai reikalavimai
		Neregistruotas sistemos naudotojas galės:
		1.	Peržiūrėti egzistuojančius „Post“;
		2.	Užsiregistruoti prie internetinės aplikacijos.

		Registruotas sistemos naudotojas
		1.	Atsijungti nuo internetinės aplikacijos;
		2.	Prisijungti prie internetinės aplikacijos;
		3.	Sukurti nauja „Thread“:
		3.1.	Priskisti titulini pavadinimą;
		3.2.	Sukurti aprašymą;
		4.	Sukurti nauja „Post“:
		4.1.	Pridėti nauja tekstą;
		4.2.	Pasirinkti senesnį postą kaip atsakymui, jei norima;
		5.	Reitinguoti kitų vartotojų „Thread“.

		Administratorius galės:
		1.	Šalinti registruotus naudotojus;
		2.	Trinti „Post“ ir „Thread“;
		3.	Perkelti „Thread“ į kitą „Topic“;
		4.	Sukurti naują „Topic“:
		4.1.	Pridėti titulą;
		4.2.	Pridėti aprašymą.

2. SISTEMOS ARCHITEKTŪRA
	Sistemos sudedamosios dalys: 
	•	Kliento pusė (ang. Front-End) – naudojant React.js
	•	Serverio pusė (angl. Back-End) – naudojant C# ASP.NET . Duomenų bazė –SQL server. 

	pav.1 pavaizduota kuriamos sistemos diegimo diagrama. Sistemos talpinimui yra naudojamas Azure serveris. Kiekviena sistemos dalis yra diegiama tame pačiame
	serveryje kaip Docker konteineris. Internetinė aplikacija yra pasiekiama per HTTP protokolą, o po pirmo užkrovimo aplikacija bendrauja su Forum API per HTTP API.
	O Forum API su duomenų baze mainus atlieka naudojant TCP sąsaja.

3. API įgyvendinimas
	
	API ir DataBase yra conteinerizuoti ir juos paleidžiama su docker-compose. API yra pasiekiamas per base URL http://localhost:5084/api/v1
	• Norint pasiekti Topic API reikia pridėti topics arba topics/{id} prie base URl.
	• Norint pasiekti Thread API reikia pridėti threads arba threads/{id} prie topics API URL'o.
	• Norint pasiekti Posts API reikia pridėti posts arba posts/{id} prie threads API URL'o.
	
	Reikalingi parametrai Post ir Put metodams:
	1. Topic atvėju reikia paduoti Title ir Description abu turi buti string tipo.
	2. Thread atvėju reikia paduoti Title ir Description abu turi buti string tipo.
	3. Post atvėju reikia paduoti Description, kuris turi buti string tipo.