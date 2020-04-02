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