using System;
using LudoGameEngine;


namespace LudoGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            while(true)
            {
                menu.MenuHeader();
                menu.MenuOptions();
                Console.Clear();
            }
        }
    }
}
