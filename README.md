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

		Administratorius galės:
			1.	Trinti „Post“ ir „Thread“;
			2.	Perkelti „Thread“ į kitą „Topic“;
			3.	Sukurti naują „Topic“:
				3.1.	Pridėti titulą;
				3.2.	Pridėti aprašymą.

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

4. Authorizacija
	api naudoja JWT tokenus vartotojų authentifikacijai ir authorizacijai. Sistemoje yra 3 roles (AnonGuest, RegisteredUser, Admin), AnonGuest vartotojai gauna tokenus 30 min, o RegisteredUser ir admin 1h.

5. Front endas
	Forumo vaizdavimo padarytas su React.js pasitelkinat Bootstrap paprastiasniai vizualizacijai ir funkcianalumui įgyvendinti. WEB puslapis yra hostinas ant Azure ir pasiekiamas adresu: https://reactfront12.azurewebsites.net. WEB Puslapį sudaro 6 skirtingi puslapiai pasiekiami nurodant tinkamus URL-us arba naudojantis teikiama komponentų navigacija. Pateikus netinkma URL lankitojas bus nukreiptas į prisijungimo langą.  

6. Api Specifikacija
6.1. Login api
Api URL: 
api/v1/login

Api Response Codes and messages:
200 obejct,
401 "User name or password is invalid.",

Api Example:
	Method: Post,
	Body:
	{
		„UserName“:“Testas1“,
		„Password“:“Testas!2!“
}
Api Example resut:
{
		"accessToken": "generated access token "
}


6.2. Register api
Api URL: POST api/v1/register

Api Response Codes:
	401 "Request invalid.",
	401 "Could not create a user.",
	201 returns an object
Api Example:
	Method: Post,
	Body:
	{
    "UserName":"Testas2",
    "Email":"testas@test.com",
    "Password":"Testas!2!"
}
Api Example resut:
{
    "id": "4edf1bb8-0d7b-4502-8270-89faca9d8a40",
    "userName": "Testas2",
    "email": testas@test.com
}
6.3. Anon browse api
Api URL: POST api/v1/anonBrowse

Api Response Codes:
	200 returns an object.
Api Example:
Method: Post
Api Example resut:
	{
		"accessToken": "generated access token "
}

6.4. Topic GET all api
Api URL: api/v1/topics

Api Response Codes:
	200
401
Api Example:
Method: GET,
BearerToken: accessToken

Api Example resut:
[
    {
        "id": 1,
        "title": "Science",
        "description": "Topic that's focused on newest science's achievements",
        "creationDate": "2022-11-10T12:57:45.6839781"
    },
    {
        "id": 2,
        "title": "Funny",
        "description": "Topic that's focused on jokes, memes and funny videossssa",
        "creationDate": "2022-11-10T12:57:45.6840175"
    },
    {
        "id": 3,
        "title": "Animal",
        "description": "Topic that's focused on anything animal related",
        "creationDate": "2022-11-10T12:57:45.6840179"
    }
]


6.5. Topic GET single api
Api URL: api/v1/topics/1

Api Response Codes:
	200
401
404
Api Example:
Method: GET,
BearerToken: accessToken

Api Example resut:
{
    "id": 1,
    "title": "Science",
    "description": "Topic that's focused on newest science's achievements",
    "creationDate": "2022-11-10T12:57:45.6839781"
}

6.6. Topic POST api
Api URL: api/v1/topics

Api Response Codes:
	201
	401
     	403
Api Example:
Method: POST,
BearerToken: accessToken,
Body {
    "Title":"Tester Topic 99",
    "Description":"Tester topic description"
 }

Api Example resut:
{
    "id": 22,
    "title": "Tester Topic 99",
    "description": "Tester topic description",
    "creationDate": "2022-12-14T20:32:48.2271224+00:00"
}

6.7. Topic PUT api
Api URL: api/v1/topics/1

Api Response Codes:
	200,
	404,
401
Api Example:
Method: PUT,
BearerToken: accessToken
	Body :{
    "Title":"Tester topic doesn't matter 123",
    "Description":"Newer text for tester topic 256525"
}

Api Example resut:
{
    "id": 22,
    "title": "Tester Topic 99",
    "description": "Newer text for tester topic 256525",
    "creationDate": "2022-12-14T20:32:48.2271224"
}
6.8. Topic Delete api
Api URL: api/v1/topics/1

Api Response Codes:
	204
	401
     	403
Api Example:
Method: DELETE,
BearerToken: accessToken

Api Example resut:


6.9. Thread GET all api
Api URL: api/v1/topics/1/threads

Api Response Codes:
	200
401
Api Example:
Method: GET,
BearerToken: accessToken

Api Example resut:
[
    {
        "id": 1,
        "title": "Newest nuclear reactor in France",
        "description": "A discussion thread about the newest nuclear reactor built in France",
        "creationDate": "2022-11-10T12:57:45.9945512"
    },
    {
        "id": 2,
        "title": "Mars discoveries",
        "description": "A discussion thread about the latest dicoveries on and about Mars",
        "creationDate": "2022-11-10T12:57:46.0130572"
    },
    {
        "id": 7,
        "title": "111111",
        "description": "322222",
        "creationDate": "2022-12-09T11:55:20.9585543"
    }
]

6.10. Thread GET single api
Api URL: api/v1/topics/1/threads/1

Api Response Codes:
	200
	401
	404
Api Example:
Method: GET,
BearerToken: accessToken

Api Example resut:
{
    "id": 1,
    "title": "Newest nuclear reactor in France",
    "description": "A discussion thread about the newest nuclear reactor built in France",
    "creationDate": "2022-11-10T12:57:45.9945512"
}

6.11. Thread POST api
Api URL: api/v1/topics/1/threads

Api Response Codes:
	201
401
     	403

Api Example:
Method: POST,
BearerToken: accessToken
	Body:{
    "Title": "Tester thread",
    "Description": "Description for tester thread 11"
}


Api Example resut:
{
    "id": 8,
    "title": "Tester thread",
    "description": "Description for tester thread 11",
    "creationDate": "2022-12-14T20:40:04.3129535+00:00"
}

6.12. Thread PUT api
Api URL: api/v1/topics/1/threads/1

Api Response Codes:
	200,
	401
     	403
	404 

Api Example:
Method: PUT,
BearerToken: accessToken
	Body: {
    "Title":"Tester thread new Title",
    "Description":"Testing Newer updated thread discription"
}


Api Example resut:
{
    "id": 8,
    "title": "Tester thread",
    "description": "Testing Newer updated thread discription",
    "creationDate": "2022-12-14T20:40:04.3129535"
}
6.13. Thread Delete api
Api URL: api/v1/topics/1/threads/1

Api Response Codes:
	204
	401
	403
	404
Api Example:
Method: DELETE,
BearerToken: accessToken

Api Example resut:


6.14. Post GET all api
Api URL: api/v1/topics/1/threads/1/posts

Api Response Codes:
	200
401
Api Example:
Method: GET,
BearerToken: accessToken

Api Example resut:
[
    {
        "id": 1,
        "description": "Is it capabilities as good as the one in the Germany?",
        "creationDate": "2022-11-10T12:57:46.1384868"
    },
    {
        "id": 2,
        "description": "Can it blow up from an earthquake?",
        "creationDate": "2022-11-10T12:57:46.138582"
    },
    {
        "id": 4,
        "description": "we are testing thread post 333",
        "creationDate": "2022-11-10T13:11:27.0237943"
    },
    {
        "id": 10,
        "description": "asd",
        "creationDate": "2022-12-07T19:33:29.1111294"
    }
]

6.15. Post GET single api
Api URL: api/v1/topics/1/threads/1/posts/1

Api Response Codes:
	200
	401
Api Example:
Method: GET,
BearerToken: accessToken

Api Example resut:
{
    "id": 1,
    "description": "Is it capabilities as good as the one in the Germany?",
    "creationDate": "2022-11-10T12:57:46.1384868"
}

4.16. Post POST api
Api URL: api/v1/topics/1/threads/1/posts

Api Response Codes:
	201
401
403
Api Example:
Method: POST,
BearerToken: accessToken
	Body:{
    "Description": "we are testing Posts post "
}
Api Example resut:
	{
    "id": 16,
    "description": "we are testing Posts post ",
    "creationDate": "2022-12-14T20:42:41.4676886+00:00"
}

6.17. Post UPDATE api
Api URL: api/v1/topics/1/threads/1/posts/1
 
Api Response Codes:
	200
	401
     	403
	404
Api Example:
Method: PUT,
BearerToken: accessToken
	Body:{
        "Description": "we are testing pdated Posts put "
}
Api Example resut:
{
    "id": 16,
    "description": "we are testing pdated Posts put ",
    "creationDate": "2022-12-14T20:42:41.4676886"
}

6.18. Post Delete api
Api URL: api/v1/topics/1/threads/1/posts/1

Api Response Codes:
	204
	403
	404
Api Example:
Method: DELETE,
BearerToken: accessToken

Api Example resut:

