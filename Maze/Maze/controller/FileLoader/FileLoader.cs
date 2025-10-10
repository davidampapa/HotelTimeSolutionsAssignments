using Maze.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Maze.Controller
{
    class FileLoader
    {
        private readonly string path = "Data/Maze.dat";
        public FileLoader() { }
        public MazeMap LoadFromFile()
        {
            var fullPath = Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName,   // např. ...\bin\Debug\
                "Data",
                "Maze.dat");

            if (!File.Exists(fullPath))
                throw new Exception("Soubor nenalezen: " + fullPath);

            // … tvoje načítání …
            var rawLines = File.ReadAllLines(fullPath);

            // odfiltruj prázdné trailing řádky
            var lines = System.Array.FindAll(rawLines, l => !string.IsNullOrWhiteSpace(l));
            if (lines.Length == 0) throw new System.Exception("Soubor je prázdný.");

            int cols = lines[0].Length;
            for (int r = 0; r < lines.Length; r++)
                if (lines[r].Length != cols)
                    throw new System.Exception($"Řádek {r + 1} má jinou délku ({lines[r].Length} vs {cols}).");

            var grid = new CellType[lines.Length, cols];
            Point? start = null, finish = null;

            for (int r = 0; r < lines.Length; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    char ch = lines[r][c];
                    switch (ch)
                    {
                        case '#': grid[r, c] = CellType.Wall; break;
                        case 'S': grid[r, c] = CellType.Start; start = new Point(r, c); break;
                        case 'F': grid[r, c] = CellType.Finish; finish = new Point(r, c); break;
                        case ' ':
                        default: grid[r, c] = CellType.Empty; break;
                    }
                }
            }

            if (start == null) throw new System.Exception("V bludišti chybí 'S' (Start).");
            if (finish == null) throw new System.Exception("V bludišti chybí 'F' (Cíl).");

            return new MazeMap(grid, start.Value, finish.Value);
        }

    }
}
