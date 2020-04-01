Menu

| Responsibility | Collaborators |
| -------------- | ------------- |
| MenuHeader     | Program       |
| MenuOptions    |               |
|                |               |

MenuOptions

| Responsibility       | Collaborators |
| -------------------- | ------------- |
| CheckGameHistory     | Menu          |
| StartNewGame         | Game          |
| ContinueExistingGame |               |

GameEngine

| Responsibility       | Collaborators |
| -------------------- | ------------- |
| PlayerScore          |               |
| Dice                 |               |
| MoveSelectedPiece    |               |
| ChangeTurn           |               |
| ChooseStartingPlayer |               |

- Samlingsklass som binder ihop spelets logik. 

  

Game

| Responsibility | Collaborators |
| -------------- | ------------- |
| IsFinished     | GameEngine    |
| GameID         | Position      |
| Winner         |               |
| TimeOfGame     |               |

Position

| Responsibility | Collaborators |
| -------------- | ------------- |
| PositionType   |               |
| GamePieceID    |               |
| PositionID     |               |
| Color          |               |

StartingZone

| Responsibility | Collaborators |
| -------------- | ------------- |
| StartingZoneID | GameBoard     |
| Color          |               |
|                |               |

GamePiece

| Responsibility | Collaborators |
| -------------- | ------------- |
| PositionID     | GameEngine    |
| GamePieceID    | StartingZone  |
| StartingZoneID | GameBoard     |

Player

| Responsibility | Collaborators |
| -------------- | ------------- |
| color          | GameEngine    |
| Score          |               |
|                |               |





