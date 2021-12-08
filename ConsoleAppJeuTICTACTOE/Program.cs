using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAppJeuTICTACTOE
{
    class Program
    {
        //Variables
        public static bool quitGame = false;
        public static bool playerTurn = true;
        public static char[,] board;
        static void Main(string[] args)
        {
            //boucle de jeu / game Loop
            while (!quitGame) //tant que le joueur n'a pas décidé de quitter le jeu
            {
                //Plateau de 3 lignes - 3 colonnes

                board = new char[3, 3]
                {
                    {' ',' ',' ' },
                    {' ',' ',' ' },
                    {' ',' ',' ' }
                };

                while (!quitGame)
                {
                    if (playerTurn)
                    {
                        PlayerTurn();
                        if (CheckLines('X'))
                        {
                            EndGame("You Win !! - Congratulation !!");
                            break;
                        }
                    }
                    else
                    {
                        ComputerTurn();
                        if (CheckLines('O'))
                        {
                            EndGame("You Loose");
                            break;
                        }
                    }

                    //Changement du joueur
                    playerTurn = !playerTurn;

                    //Vérifier si match nul
                    if (CheckDraw())
                    {
                        EndGame("Draw - Match Nul");
                        break;
                    }
                }

                if (!quitGame)
                {
                    //Instruction
                    Console.WriteLine("Appuyer sur [escape] pou quitter ou [Enter] pour continuer");
                    //Recuperation touche du clavier
                    GetKey:
                    switch (Console.ReadKey().Key)
                    {
                        //Rejouer
                        case ConsoleKey.Enter:
                            break;
                        case ConsoleKey.Escape:
                            quitGame = true;
                            Console.Clear();
                            break;
                        default:
                            goto GetKey;
                            
                    }
                }
            }

            // fin du Main
        }

        public static void PlayerTurn()
        {
            //où se touve le joueur 
            //curseur sur la ligne et la colonne

            var (row, column) = (0, 0);
            //Le curseur à t'il été bougé
            bool moved = false;
            //Boucle pour deplacer le curseur
            while (!quitGame && !moved)
            {
                Console.Clear();
                //Fonction pour generer le plateau de jeu
                RenderBoard();
                Console.WriteLine();
                //Afficher les instuctions
                Console.WriteLine("Choiser une case valide et appuyer sur [Enter]");
                //Afficher le curseur
                Console.SetCursorPosition(column * 6 + 1, row * 4 + 1);

                //Attendre que l'utilisateur realise une action
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                        quitGame = true;
                        Console.Clear();
                        break;
                    //Gérer les flèches du clavier
                    //Pour deplacer le curseur à l'écran
                    case ConsoleKey.RightArrow:
                        if (column >= 2)
                        {
                            column = 0;
                        }else
                        column = column + 1;
                        break;
                    case ConsoleKey.LeftArrow:
                        if(column <= 0)
                        {
                            column = 2;
                        }
                        else
                        {
                            column = column - 1;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (row >= 2)
                        {
                            row = 0;
                        }
                        else
                        {
                            row = row + 1;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if(row <= 0)
                        {
                            row = 2;
                        }
                        else
                        {
                            row = row - 1;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if(board[row,column] is ' ')
                        {
                            board[row, column] = 'X';
                            moved = true;
                        }
                        break;

                }
            }
        }

        //Au tour de l'ordinateur
        public static void ComputerTurn()
        {
            var emptyBox = new List<(int x, int y)>();

            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if (board[i,j]==' ')
                    {
                        emptyBox.Add((i, j));
                    }
                }
            }
            var (x, y) = emptyBox[new Random().Next(0, emptyBox.Count())];
            board[x, y] = 'O';
        }

        //Afficher le plateau à l'écran
        public static void RenderBoard()
        {
            Console.WriteLine();
            Console.WriteLine($" {board[0, 0]}  |  {board[0, 1]}  |  {board[0, 2]}");
            Console.WriteLine("    |     |");
            Console.WriteLine("----+-----+----");
            Console.WriteLine("    |     |");
            Console.WriteLine($" {board[1, 0]}  |  {board[1, 1]}  |  {board[1, 2]}");
            Console.WriteLine("    |     |");
            Console.WriteLine("----+-----+----");
            Console.WriteLine("    |     |");
            Console.WriteLine($" {board[2, 0]}  |  {board[2, 1]}  |  {board[2, 2]}");
        }

        //Vérifier si un joueur à gagner
        public static bool CheckLines(char c) =>
           board[0, 0] == c && board[1, 0] == c && board[2, 0] == c ||
           board[0, 1] == c && board[1, 1] == c && board[2, 1] == c ||
           board[0, 2] == c && board[1, 2] == c && board[2, 2] == c ||
           board[0, 0] == c && board[0, 1] == c && board[0, 2] == c ||
           board[1, 0] == c && board[1, 1] == c && board[1, 2] == c ||
           board[2, 0] == c && board[2, 1] == c && board[2, 2] == c ||
           board[0, 0] == c && board[1, 1] == c && board[2, 2] == c ||
           board[2, 0] == c && board[1, 1] == c && board[0, 2] == c;

        //Vérifier si match null
            public static bool CheckDraw() =>
            board[0, 0] != ' ' && board[1, 0] != ' ' && board[2, 0] != ' ' &&
            board[0, 1] != ' ' && board[1, 1] != ' ' && board[2, 1] != ' ' &&
            board[0, 2] != ' ' && board[1, 2] != ' ' && board[2, 2] != ' ';

        //fin de Partie
        public static void EndGame(string msg)
        {
            Console.Clear();
            RenderBoard();
            Console.WriteLine(msg);
        }
    }
    
}
