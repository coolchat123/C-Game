using System;

namespace Game
{
    // Program is een class.
    // Van een class kunnen we "instances" maken.
    // Dit doen we op dezelfde manier als we bijvoorbeeld een nieuwe int aan zouden maken:
    // type     | naam       = | value         
    //----------+------------------------------
    //  int       newInteger =   0;            
    //  Program   newProgram =   new Program();
    // Een class kan variabelen, methods, en nog veel meer bevatten.
    // Al deze dingen heeft een instance van die class dan ook.
    class Program
    {
        // De Main method is de ingang van onze code.
        // Hier begint ons programma.
        static void Main(string[] args)
        {
            // Hier maken we een instance van de Game class.
            // Een instance 
            Game Game = new Game();
            // Daarna roepen we de Run method van de
            Game.Run();
        }
    }
}
