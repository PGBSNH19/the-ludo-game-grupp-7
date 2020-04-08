**2020-03-30**

·     Skrivit CRC-cards

·     Skrivit User-stories

·     Skapat en enkel flowchart

Spenderade eftermiddagen med att planera hur vi ska lägga upp projektet.

 

**2020-04-01**

Börjat med att vi har skapat metoden för att rulla tärningen, och även skapat ett test för metoden.

Skrivit ut de klasser vi behövde för att enklare kunna visualisera hur vi skulle flytta på pjäserna.

Skapat props för player, och en lista för att varje spelare ska kunna hålla fyra pjäser.

Skapat en metod för att generera 4 pjäser till varje spelare.

 

Skapat en visualisering för vårat positions-system/spelbräda.



**2020-04-02**

Gjorde ändringar i flera klasser, bl.a. GamePiece, player, GameEngine. Refaktorering av onödig kod och vi har tänkt om gällande hur vårt spelbräde ser ut. 

Positioner kommer hållas koll på genom GamePieces.

Skrivit en metod för att kunna flytta på pjäser. Vi valde att ha ett simpelt positionssystem där en vi enkelt bara
kan slå tärningen och sedan ta det output lägga på GamePiece.position.

Vi lade till en StepCounter i GamePiece. 
Anledningen är att att de olika färgerna börjar på olika positioner, samt att vi ska kunna räkna ut ifall
en pjäs har gått i mål.

Börjat skriva en metod för att kolla var pjäsernas position är, och ifall en pjäs går i mål.
Vi behöver hitta ett bättre sätt för att kunna lägga till poäng till spelaren ur just denna metoden.

[Vi funderar på hur testbar denna koden är, kan komma att ändras i framtiden.]
Jobbat med klassen som kommer att visa menyn, skapat en lobbymetod för att kunna välja hur många spelare som ska vara med att spela.
I lobbymetoden skapar vi även en en ny instans av spelet. 



**2020-04-03**

- Skapat ett test för att kolla ordningen på spelare. 
- Skrivit en metod som låter spelaren välja en av sina pjäser att flytta.
- Lade till ett ID i GamePiece klassen för att göra koden lite mer hållbar och lättare kunna hålla koll på GamePieces.
- Skapat en metod för att sätta startingposition för varje spelpjäs, och även skapat ett test för denna metoden.
- Fixat till metoden MoveGamePiece, så att spelbrädan inte är oändlig. Alltså kommer en GamePiece till position 40 och slår 4 så fortsätter den inte till 44 utan hamnar på position 4.
- Skapade test för "knuff"-funktionen och sedan själva funktionen. Alltså hamnar GamePiece på en position där det redan står en gamepiece, så knuffas denne tillbakat till dennes bo/ startposition. 



**2020-04-06** 

- Arbetat med metoder för att kunna flytta ut pjäser och att man inte får flytta ut pjäser om man inte slagit 1 eller 6.
- Psuedo-kod:  dice roll if dice roll mellan 1 & 6 {du kan flytta alla pjäser position.boardposition.start && position.boardposition.outerpath && position.boardposition.innerpath} if dice mellan 2 & 5 {du kan flytta på pjäser som är på spelplan position.boardposition.outerpath && position.boardposition.innerpath}  

 **2020-04-07**

- Vi har refaktureat en hel del av koden som låg i GameEngine, eftersom en hel del inputs och outputs låg där i. 
- Vi löste även problemet med att en spelare ska kunna välja vilken pjäs som helst när tärningen slår 1 eller 6.
- Ändrat om så att spelplanen börjar på 0 istället för 1 för att slippa vissa onödiga uträkningar. Jobbat på metoden som håller koll på ifall en spelare går i mål eller inte, och var koden hör hemma.
- Lagt till logik som håller koll på hur många poäng en spelare har.
- Börjat med felhantering för user input som ser till att input är korrekt, men vi använder förtillfället samma kod på flera ställen. Eventuellt bör det vara en metod som kollar user-input. 
- Skapat ett test som testar ifall pjäserna kan flytta in på InnerPath. 
- Skapade en metod som ser till att det användaren matar in är korrekt (CheckUserInput).
- Vi har ändrat på metoden som räknar ut ifall en spelpjäs går i mål eller inte, och skapat ett test för ifall en pjäs måste gå förbi mål.


 **2020-04-08**

- Fixade så att en pjäs inte blir tillänglig för att flyttas efter att den har kommit till finishposition och lade till test för detta.
- Skapat en CheckWin-metod i GameEngine istället för att ha den i menyn, och även skapat ett test för den.
-  Snyggat till lite i utskrifterna. 
- Börjat lägga grund för databas, en DbContext klass.