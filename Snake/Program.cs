using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace Snake
{

	struct Position
	{
		public int row;
		public int col;
		public Position(int row, int col)
		{
			this.row = row;
			this.col = col;
		}
	}

	class MainClass
	{
		static void Main(string[] args)
		{
			Position[] directions = new Position[]
			{
				new Position(0, 1), // right
				new Position(0, -1), // left
				new Position(1, 0), // down
				new Position(-1, 0), // up
			};

			byte right = 0;
			byte left = 1;
			byte down = 2;
			byte up = 3;

			Console.WriteLine("Choose difficulty: ");
			Console.WriteLine("Easy");
			Console.WriteLine("Medium");
			Console.WriteLine("Hard");

			string difficulty = Console.ReadLine();
			Console.Clear();

			double sleepTime = 100;

			int direction = right;

			Console.BufferHeight = Console.WindowHeight;

			Random randomNumbersGenerator = new Random();
			Position food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight), randomNumbersGenerator.Next
										 (0, Console.WindowWidth));

			Console.SetCursorPosition(food.col, food.row);
			Console.Write("@");

			Queue<Position> snakeElements = new Queue<Position>();

			for (int i = 0; i < 6; ++i)
			{
				snakeElements.Enqueue(new Position(0, i));
			}

			foreach (Position position in snakeElements)
			{
				Console.SetCursorPosition(position.col, position.row);
				Console.Write('*');
			}


			while (true)
			{
				if (Console.KeyAvailable)
				{
					ConsoleKeyInfo userInput = Console.ReadKey();

					if (userInput.Key == ConsoleKey.LeftArrow)
					{
						if (direction != right) direction = left;

					}
					else if (userInput.Key == ConsoleKey.RightArrow)
					{
						if (direction != left) direction = right;
					}
					else if (userInput.Key == ConsoleKey.DownArrow)
					{
						if (direction != up) direction = down;
					}
					else if (userInput.Key == ConsoleKey.UpArrow)
					{
						if (direction != down) direction = up;
					}

				}

				Position snakeHead = snakeElements.Last();
				Position nextDirection = directions[direction];

				Position snakeNewHead = new Position(snakeHead.row + nextDirection.row, snakeHead.col + nextDirection.col);


				if (snakeNewHead.row < 0 ||
					snakeNewHead.col < 0 ||
					snakeNewHead.row >= Console.WindowHeight ||
				    snakeNewHead.col >= Console.WindowWidth ||
					snakeElements.Contains(snakeNewHead))
				{
					Console.SetCursorPosition(0, 0);
					Console.WriteLine("Game Over!");
					Console.WriteLine("Highscore: {0}", (snakeElements.Count - 6) * 100);
					return;
				}
				
				snakeElements.Enqueue(snakeNewHead);
				Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
				Console.Write('*');

				if (snakeNewHead.col == food.col && snakeNewHead.row == food.row)
				{
					do
					{
						food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight), randomNumbersGenerator.Next
										 (0, Console.WindowWidth));
					}
					while (snakeElements.Contains(food));
					
					Console.SetCursorPosition(food.col, food.row);
					Console.Write("@");

					sleepTime--;
					//snake eats
				}
				else
				{
					Position last = snakeElements.Dequeue();
					Console.SetCursorPosition(last.col, last.row);
					Console.Write(' ');
				}



				//Console.Clear();

				if (difficulty == "Easy")
				{
					sleepTime -= 0.01;
				}
				else if (difficulty == "Medium")
				{
					sleepTime -= 0.03;
				}
				else if (difficulty == "Hard")
				{
					sleepTime -= 0.07;
				}
				else
				{
					Console.WriteLine("Congrats, idiot!");
					sleepTime -= 20;
				}


				sleepTime -= 0.01;
				Thread.Sleep((int)sleepTime);


			}
		}
	}
}
