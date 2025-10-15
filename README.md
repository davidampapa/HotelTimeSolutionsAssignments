# 🧩 Maze & Fujtajbl Projects (.NET Framework 4.8)

This repository contains two C# console applications demonstrating basic OOP, algorithmic logic, and user interaction via the console.

---

## 🚀 Projects Overview

### 1️⃣ Maze Simulation
A single-threaded console simulation where dwarfs navigate through a maze using different pathfinding strategies and MVC pattern.

#### 📖 Description
The program loads a maze from a text file (`Data/Maze.dat`), spawns dwarfs with various movement algorithms, and animates their progress in the console window.  
Each dwarf class defines its own movement logic (e.g., left-hand rule, right-hand rule, random walk, BFS).

#### 🏗️ Structure
```
Maze/
│
├── Model/
│   ├── MazeMap.cs          # Represents the maze grid
│   ├── Point.cs            # 2D coordinates
│   ├── CellType.cs         # Enum for cell types (Wall, Start, Finish, etc.)
│   ├── DwarfBase.cs        # Abstract base for all dwarfs
│   ├── WallFollowerDwarf.cs# Base for left/right wall-following dwarfs
│   ├── LeftDwarf.cs        # Follows left wall
│   └── RightDwarf.cs       # Follows right wall
│
├── Controller/
│   ├── FileLoader.cs       # Loads maze from Maze.dat
│   ├── GameController.cs   # Game loop and dwarf updates
│   ├── DwarfSpawner.cs     # Spawns dwarfs dynamically
│   └── MazeApplicationController.cs # Entry-level controller
│
├── View/
│   └── ConsoleRenderer.cs  # Handles console drawing
│
├── Data/
│   └── Maze.dat            # Maze layout file
│
└── Program.cs              # Entry point
```

#### ⚙️ How to Run
1. Open in **Visual Studio 2022** (target: .NET Framework 4.8).  
2. Ensure file structure includes `Data/Maze.dat`.
3. Example file content:
   ```
   #######
   #S   F#
   # ### #
   #     #
   #######
   ```
4. Run the program (`Ctrl + F5`).

#### 💡 Features
- Maze loaded from file (`Maze.dat`).
- Multiple dwarf strategies:
  - Left-hand wall follower.
  - Right-hand wall follower.
  - Random or BFS movement (optional extensions).
- MVC-like design (Model–View–Controller separation).
- Animated console rendering.
- Error handling for missing or invalid files.

#### 🧠 Technologies
- C# (.NET Framework 4.8)
- Visual Studio
- Console I/O
- Object-oriented principles (abstraction, inheritance, polymorphism)


### 2️⃣ Fujtajbl – Basic Console Calculator
A simple text-based calculator that reads two numbers, lets the user choose an operation, and displays the result.

#### 📖 Description
The calculator supports basic arithmetic operations (`+`, `-`, `*`, `/`) and runs in a loop until the user chooses to exit.  
The implementation uses dictionaries and delegates (`Func<int,int,int>`) for a clean and extendable design.

#### 🏗️ Structure
```
Fujtajbl/
│
├── BasicCalculator.cs  # Core calculator logic
└── Program.cs          # Entry point
```

#### ⚙️ How to Run
1. Open the Fujtajbl project in **Visual Studio 2022**.
2. Build and run (`Ctrl + F5`).
3. Follow console prompts:
   ```
   Zadejte číslo A:
   Zadejte číslo B:
   Vyberte operaci:
   1: a + b
   2: a - b
   3: a * b
   4: a / b
   ```
4. After each computation, you can choose to perform another one (`a/n`).

#### 💡 Features
- Simple and clear console UI.
- Uses dictionaries for mapping operations to symbols and logic.
- Input validation and error handling (invalid format, division by zero).
- Demonstrates exception handling, enums, and lambda expressions.

#### 🧠 Technologies
- C# (.NET Framework 4.8)
- Console I/O