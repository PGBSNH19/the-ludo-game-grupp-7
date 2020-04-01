using System;
using LudoGameEngine;


namespace LudoGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game dice = new Game();
            dice.RollDice();
            Console.WriteLine();
        }
    }
}
