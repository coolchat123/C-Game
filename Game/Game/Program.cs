﻿using System;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Game
{
    class Program
    {
        public const int TARGET_FPS = 60;
        public const float TIME_UNTIL_UPDATE = 1f / TARGET_FPS;

        // "Sprites" is a list of sprites that should be drawn.
        // It is updated by the GameLoop class and its child classes.
        public static List<Sprite> Sprites = new List<Sprite> { };

        // "RunningGame" is the GameLoop that is currently running.
        // GameLoop is an abstract class, so RunningGame can only ever be one of its child classes.
        // The child class that occupies RunningGame at any one given point signifies what game is being played.
        // The possible child classes are Menu, Pong, Breakout, Pacman and Game4.
        static GameLoop RunningGame;

        static void Main(string[] args)
        {
            // Initialise the RenderWindow.
            WindowClearColour = Color.Blue;
            Window = new RenderWindow(new VideoMode(800, 600), "Steenboy Color");
            Window.Closed += Window_Closed;

            // Create an instance of the Menu class to occupy RunningGame.
            RunningGame = new Menu();

            // Load Menu's content and initialise it.
            RunningGame.LoadContent();
            RunningGame.Initialise();

            // Create an instance of the Clock class provided by SFML.
            Clock clock = new Clock();

            // Create an instance of our GameTime class, which will further help measure time.
            GameTime = new GameTime();
            float totalTimeBeforeUpdate = 0f;
            float previousTimeElapsed = 0f;
            float deltaTime = 0f;
            float totalTimeElapsed = 0f;

            // Start game loop
            while (Window.IsOpen)
            {
                Window.DispatchEvents();

                totalTimeElapsed = clock.ElapsedTime.AsSeconds();
                deltaTime = totalTimeElapsed - previousTimeElapsed;
                previousTimeElapsed = totalTimeElapsed;

                totalTimeBeforeUpdate += deltaTime;

                if (totalTimeBeforeUpdate >= TIME_UNTIL_UPDATE)
                {
                    GameTime.Update(totalTimeBeforeUpdate, clock.ElapsedTime.AsSeconds());
                    totalTimeBeforeUpdate = 0f;

                    RunningGame.Update(GameTime);

                    Window.Clear(WindowClearColour);

                    Draw(GameTime);

                    Window.Display();
                }
            }
        }

        public static RenderWindow Window;

        private static void Window_Closed(object sender, EventArgs a)
        {
            Window.Close();
        }

        public static GameTime GameTime;

        public static Color WindowClearColour;

        public static void Draw(GameTime gameTime)
        {
            foreach(Sprite sprite in Sprites)
            {
                Window.Draw(sprite);
            }
        }
    }
}
