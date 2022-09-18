<p align="center">
  <img width="128" align="center" src="https://user-images.githubusercontent.com/67554762/171267199-45341351-5968-4f68-802d-2e80136ea4ab.png">
</p>
<h1 align="center">Gera Chess Library</h1>
<div>
	<p align="center">
	  Chess logic made with C# and &hearts; by Geras1mleo
	</p>
</div>


<b>Browse in <a href="https://www.nuget.org/packages/Gera.Chess/"><img height="15px" src="https://www.nuget.org/favicon.ico"> NuGet</a></b>

## Chess lib includes:

- **ChessBoard** with 2-dimentional array of **Pieces**
- Generation, Validation and Execution of **Moves** on chess board
- **Parse**:
  - **Move object** into [SAN](https://www.chessprogramming.org/Algebraic_Chess_Notation#Standard_Algebraic_Notation_.28SAN.29) and back into Move object
- **Load** and play further:
  - **Chess _Board_** from [FEN](https://www.chessprogramming.org/Forsyth-Edwards_Notation) and back to *FEN-string*
  - **Chess _Game_** from [PGN](https://en.wikipedia.org/wiki/Portable_Game_Notation) and back to *PGN-string*
- **Event handlers** are raised:
  -  *OnInvalidMoveKingChecked* - when trying to execute move that places own king in **check**
  -  *On(White/Black)KingCheckedChanged* - with **state** (checked or not) and its **position**
  -  *OnPromotePawn* - with *PromotionEventArgs.PromotionResult* (default: **PromotionToQueen**)
  -  *OnEndGame* - with according end game info (**Won Side**/**Draw**)
  -  *OnCaptured* - with captured piece and all recently captured pieces (**White**/**Black**)
- **End Game** Declaration: **Draw** or **Resign** of one of sides
- **Navigation** between executed moves:
  - **First**/**Last**
  - **Next**/**Previous**
  - Also: **board.MoveIndex** property to navigate directly to specific move
- **Cancelation** of last executed move

# Usage!

Example simple **console** chess game:

```csharp
using Chess;

var board = new ChessBoard();

while (!board.IsEndGame)
{
    Console.WriteLine(board.ToAscii());
    board.Move(Console.ReadLine());
}

Console.WriteLine(board.ToAscii());
Console.WriteLine(board.ToPgn());

// Outcome after last move:
// Qh5
//   ┌────────────────────────┐
// 8 │ r  n  b  q  k  b  n  r │
// 7 │ p  p  p  p  p  .  .  p │
// 6 │ .  .  .  .  .  .  .  . │
// 5 │ .  .  .  .  .  p  p  Q │
// 4 │ .  .  .  .  P  P  .  . │
// 3 │ .  .  .  .  .  .  .  . │
// 2 │ P  P  P  P  .  .  P  P │
// 1 │ R  N  B  .  K  B  N  R │
//   └────────────────────────┘
//     a  b  c  d  e  f  g  h  
//
// 1. e4 f5 2. f4 g5 3. Qh5# 1-0
```

Example **random** chess game:

```csharp
while (!board.IsEndGame)
{
    var moves = board.Moves();
    board.Move(moves[Random.Shared.Next(moves.Length)]);
}

Console.WriteLine(board.ToAscii());
Console.WriteLine(board.ToPgn());

// Todo: End game by Insufficient Material
```

## Track Pieces

Track pieces on position using **indexers**:

```csharp
board["c2"]... 		  	// => White Pawn
board['g', 8]... 		// => Black Bishop

// Counting from 0
board[0, 0]...			// => White Rook
board[new Position(4, 7)]... 	// => Black King
```

Track **captured pieces**:

```csharp
board.CapturedWhite... // => White pieces that has been captured by black player
board.CapturedBlack... // => Black pieces that has been captured by white player
// Properties above work just fine when board has been loaded from FEN
```

Track **kings** and their state (Checked/Unchecked):

```csharp
board.WhiteKing... // => White king position on chess board (Calculated property)
board.BlackKing... // => Black king position on chess board (Calculated property)

board.WhiteKingChecked... // => State of White king (Referred property)
board.BlackKingChecked... // => State of Black king (Referred property)
```

## Move Pieces

Move pieces using **SAN/LAN**:

```csharp
board.Move("e4");	// => Good
board.Move("N-f6");	// => Good
board.Move("NXf6");	// => Good
board.Move("dxc3 e.p.");// => Good
board.Move("Pe4");	// => Good 
board.Move("Pe5xd6");	// => Good
board.Move("O-O-O+");	// => Good

board.Move("ne5");	// => Bad
board.Move("e8=K");	// => Bad
board.Move("0-0");	// => Bad
```

Move pieces using **Move object** and corresponding positions:

```csharp
board.Move(new Move("b1", "c3"));
```

**Ambiguity**:

```csharp
if(ChessBoard.TryLoadFromPgn("1. e4 e5 2. Ne2 f6", out var board))
{
  board.ToAscii();
  //   ┌────────────────────────┐
  // 8 │ r  n  b  q  k  b  n  r │
  // 7 │ p  p  p  p  .  .  p  p │
  // 6 │ .  .  .  .  .  p  .  . │
  // 5 │ .  .  .  .  p  .  .  . │
  // 4 │ .  .  .  .  P  .  .  . │
  // 3 │ .  .  .  .  .  .  .  . │
  // 2 │ P  P  P  P  N  P  P  P │
  // 1 │ R  N  B  Q  K  B  .  R │
  //   └────────────────────────┘
  //     a  b  c  d  e  f  g  h  

  board.Move("Nc3"); 	// => Throws exception: ChessSanTooAmbiguousException. Both knights can move to c3
  board.Move("Nc4"); 	// => Throws exception: ChessSanNotFoundException. None of knights can move to c3
}
```

## Load Chess game/board

Load chess board Variant: **From Position** (using FEN):

```csharp
board = ChessBoard.LoadFromFen("1nbqkb1r/pppp1ppp/2N5/4p3/3P4/8/PPP1PPPP/RN2KB1R w KQk - 0 1");
board.ToAscii();
//   ┌────────────────────────┐
// 8 │ .  n  b  q  k  b  .  r │
// 7 │ p  p  p  p  .  p  p  p │
// 6 │ .  .  N  .  .  .  .  . │
// 5 │ .  .  .  .  p  .  .  . │
// 4 │ .  .  .  P  .  .  .  . │
// 3 │ .  .  .  .  .  .  .  . │
// 2 │ P  P  P  .  P  P  P  P │
// 1 │ R  N  .  .  K  B  .  R │
//   └────────────────────────┘
//     a  b  c  d  e  f  g  h  

board.CapturedWhite... // => { White Bishop, White Queen }
board.CapturedBlack... // => { Black Rook, Black Knight }

// Stalemate
board.LoadFen("rnb1kbnr/pppppppp/8/8/8/8/5q2/7K w kq - 0 1");
board.EndGame... // => { EndgameType = Stalemate, WonSide = null }
```

Load full chess game from **PGN**:

```csharp
board = ChessBoard.LoadFromPgn(
@"[Variant ""From Position""]
[FEN ""rnbqkbnr/ppp1pppp/8/3p4/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 1""]
            
1.exd5 e6 2.dxe6 fxe6 3.d4(3.f4 g5 4.fxg5) 3... c5 4.b4");

board.ToAscii();
//   ┌────────────────────────┐
// 8 │ r  n  b  q  k  b  n  r │
// 7 │ p  p  .  .  .  .  p  p │
// 6 │ .  .  .  .  p  .  .  . │
// 5 │ .  .  p  .  .  .  .  . │
// 4 │ .  P  .  P  .  .  .  . │
// 3 │ .  .  .  .  .  .  .  . │
// 2 │ P  .  P  .  .  P  P  P │
// 1 │ R  N  B  Q  K  B  N  R │
//   └────────────────────────┘
//     a  b  c  d  e  f  g  h  
```

Alternative moves and comments are (temporarily) skipped<br/>
In further versions:<br/>
Navigate between alternative branches, also load branches from PGN<br/>
Adding comments to each move<br/>

## End Game

Declare **Draw/Resign**:

```csharp
board.Draw();
board.EndGame... // => { EndgameType = DrawDeclared, WonSide = null }

board.Clear();

board.Resign(PieceColor.Black);
board.EndGame... // => { EndgameType = Resigned, WonSide = White }
```

## [Unit Tests](/ChessUnitTests/UnitTests.cs)
Here you can see all the tests that have been used to test and improve chess library

## [Benchmarks](/ChessBenchmarks/Benchmarks.cs)
Here you can see the evolution of performance of chess library

## Like the project?

Give it a :star: Star!

## Found a bug?

Drop to <a href="https://github.com/Geras1mleo/Chess/issues">Issues</a><br/>
Or: sviatoslav.harasymchuk@gmail.com<br/>
<br/>
Thanks in advance!
