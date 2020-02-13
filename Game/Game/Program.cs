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
<<<<<<< Updated upstream
=======
        public static float Scale = 3f;
        public static readonly Color WindowClearColour = Color.Black;
>>>>>>> Stashed changes

        // "Sprites" is a list of sprites that should be drawn.
        // It is updated by the GameLoop class and its child classes.
        public static List<Sprite> Sprites = new List<Sprite> { };

        // "RunningGame" is the GameLoop that is currently running.
        // GameLoop is an abstract class, so RunningGame can only ever be one of its child classes.
        // The child class that occupies RunningGame at any one given point signifies what game is being played.
        // The possible child classes are Menu, Pong, Breakout, Pacman and Snake.
        static GameLoop RunningGame;

        static void Main(string[] args)
        {
            // Initialise the RenderWindow.
            WindowClearColour = Color.Blue;
            Window = new RenderWindow(new VideoMode(800, 600), "Steenboy Color");
            Window.Closed += Window_Closed;

<<<<<<< Updated upstream
            // Create an instance of the Menu class to occupy RunningGame.
            RunningGame = new Menu();

            // Load Menu's content and initialise it.
            RunningGame.LoadContent();
            RunningGame.Initialise();
=======
            LoadNewGame(new Pacman());
>>>>>>> Stashed changes

            // Create an instance of the Clock class provided by SFML.
            Clock clock = new Clock();

            // Create an instance of our GameTime class, which will further help measure time.
            GameTime = new GameTime();
            float totalTimeBeforeUpdate = 0f;
            float previousTimeElapsed = 0f;
            float deltaTime = 0f;
            float totalTimeElapsed = 0f;

            // Start game loop.
            while (Window.IsOpen)
            {
                Window.DispatchEvents();

                totalTimeElapsed = clock.ElapsedTime.AsSeconds();
                deltaTime = totalTimeElapsed - previousTimeElapsed;
                previousTimeElapsed = totalTimeElapsed;

                totalTimeBeforeUpdate += deltaTime;

                if (totalTimeBeforeUpdate >= TIME_UNTIL_UPDATE)
                {
                    // Update game time
                    GameTime.Update(totalTimeBeforeUpdate, clock.ElapsedTime.AsSeconds());
                    totalTimeBeforeUpdate = 0f;

                    // Run program update function.
                    Update(GameTime);

                    // Check for mouse clicks.
                    if (Mouse.IsButtonPressed(Mouse.Button.Left))
                    {
                        Click();
                    }

                    // Run GameLoop update function.
                    RunningGame.Update(GameTime);

                    // Draw
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

        public static void Click()
        {
            foreach (SSprite sprite in Sprites)
            {
                Button button = sprite as Button;

                if(button != null)
                {
                    if (button.MouseOver)
                    {
                        button.PerformClick();
                    }
                }
            }
        }

        public static void Update(GameTime gameTime)
        {
            RunningGame.Update(gameTime);

            Vector2f mousePosition = Window.MapPixelToCoords(Mouse.GetPosition(Window));

            foreach (SSprite sprite in Sprites)
            {
                Button button = sprite as Button;
                
                if(button != null)
                {
                    bool mouseOver = button.GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y);

                    if (mouseOver != button.MouseOver)
                    {
                        if (mouseOver)
                        {
                            button.PerformMouseEnter();
                        }
                        else
                        {
                            button.PerformMouseLeave();
                        }
                        button.MouseOver = mouseOver;
                    }
                }
            }
        }

        public static void Draw(GameTime gameTime)
        {
            foreach (Sprite sprite in Sprites)
            {
                Window.Draw(sprite);
            }
        }
    }
}
