using System;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Game
{
    class Program
    {
        public enum GameName
        {
            None = 0,
            Pong = 1,
            Snake = 2,
            Pacman = 3,
            Breakout = 4
        }

        public const int TARGET_FPS = 60;
        public const float TIME_UNTIL_UPDATE = 1f / TARGET_FPS;
        public static readonly Color TextureClearColour = Color.Blue;
        public static readonly Color WindowClearColour = Color.Black;

        public static RenderTexture Texture;
        public static Vector2f TexturePosition;
        public static RenderWindow Window;
        public static int CurrentResolution = 0;
        public static List<Vector2u> Resolutions = new List<Vector2u>
        {
            new Vector2u(798, 600), // 3x
            new Vector2u(1064, 800), // 4x
            new Vector2u(1330, 1000), // 5x
            new Vector2u(1596, 1200), // 6x
            new Vector2u(1862, 1400), // 7x
            new Vector2u(1920, 1600) // 8x
        };
        public static float Scale = 3f;

        public static Font MyFont;

        public static bool LeftPressed = false;

        public static GameName ChangeGame = GameName.None;

        // "Sprites" is a list of sprites that should be drawn.
        // It is updated by the GameLoop class and its child classes.
        public static List<Sprite> Sprites = new List<Sprite> { };

        public static List<Text> Strings = new List<Text> { };

        // "RunningGame" is the GameLoop that is currently running.
        // GameLoop is an abstract class, so RunningGame can only ever be one of its child classes.
        // The child class that occupies RunningGame at any one given point signifies what game is being played.
        // The possible child classes are Menu, Pong, Breakout, Pacman and Snake.
        static GameLoop RunningGame;

        static void Main(string[] args)
        {
            // Initialise the RenderWindow and RenderTexture.
            Texture = new RenderTexture(266, 200);
            Window = new RenderWindow(new VideoMode(800, 600), "Steenboy Color", Styles.Close);
            ResizeWindow();
            Window.Closed += Window_Closed;
            
            // Create an instance of the Menu class to occupy RunningGame.
            RunningGame = new Menu();

            // Load Menu's content and initialise it.
            RunningGame.LoadContent();
            RunningGame.Initialise();

            MyFont = new Font("Content/arialbd.ttf");

            LoadNewGame(new Menu());

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
                    if(ChangeGame != GameName.None)
                    {
                        switch (ChangeGame)
                        {
                            case GameName.Pong:
                                LoadNewGame(new Pong());
                                break;
                            case GameName.Pacman:
                                LoadNewGame(new Pacman());
                                break;
                            case GameName.Snake:
                                LoadNewGame(new Snake());
                                break;
                            case GameName.Breakout:
                                LoadNewGame(new Breakout());
                                break;
                        }

                        ChangeGame = GameName.None;
                    }

                    // Update game time
                    GameTime.Update(totalTimeBeforeUpdate, clock.ElapsedTime.AsSeconds());
                    totalTimeBeforeUpdate = 0f;

                    // Run program update function.
                    Update(GameTime);

                    // Draw
                    Draw(GameTime);
                }
            }
        }

        private static void Window_Closed(object sender, EventArgs a)
        {
            Window.Close();
        }

        public static GameTime GameTime;

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
                        return;
                    }
                }
            }
        }

        public static void WASD(Keyboard.Key key)
        {
            foreach (SSprite sprite in Sprites)
            {
                PacmCharacter pacmCharacter = sprite as PacmCharacter;

                if (pacmCharacter != null)
                {
                    if(key == Keyboard.Key.W)
                    {
                        pacmCharacter.ChangeDirection(3);
                        Console.WriteLine("test3");
                    }
                    if (key == Keyboard.Key.A)
                    {
                        pacmCharacter.ChangeDirection(1);
                        Console.WriteLine("test1");
                    }
                    if (key == Keyboard.Key.S)
                    {
                        pacmCharacter.ChangeDirection(4);
                        Console.WriteLine("test4");
                    }
                    if (key == Keyboard.Key.D)
                    {
                        pacmCharacter.ChangeDirection(2);
                        Console.WriteLine("test2");
                    }

                }
            }
        }


        public static void Update(GameTime gameTime)
        {
            // Run GameLoop update function.
            RunningGame.Update(gameTime);

            // Check whether buttons are moused over.
            Vector2f mousePosition = Texture.MapPixelToCoords(Mouse.GetPosition(Window));
            foreach (SSprite sprite in Sprites)
            {
                Button button = sprite as Button;
                
                if(button != null)
                {
                    bool mouseOver = button.GetGlobalBounds().Contains((mousePosition.X - TexturePosition.X) / Scale, (mousePosition.Y - TexturePosition.Y) / Scale);

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

            // Check for mouse clicks.
            if (!LeftPressed && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                LeftPressed = true;
                Click();
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                WASD(Keyboard.Key.W);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                WASD(Keyboard.Key.D);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                WASD(Keyboard.Key.A);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                WASD(Keyboard.Key.S);
            }
        }

        public static void Draw(GameTime gameTime)
        {
            Texture.Clear(TextureClearColour);

            Window.Clear(WindowClearColour);

            foreach (SSprite sprite in Sprites)
            {
                Texture.Draw(sprite);
            }

            foreach(Text text in Strings)
            {
                Texture.Draw(text);
            }

            Texture.Display();

            Sprite textureSprite = new Sprite(Texture.Texture);
            textureSprite.Scale = new Vector2f(Scale, Scale);
            textureSprite.Position = new Vector2f(TexturePosition.X, TexturePosition.Y);

            Window.Draw(textureSprite);

            Window.Display();
        }

        public static void LoadNewGame(GameLoop gameLoop)
        {
            // Clear list of sprites.
            Sprites.Clear();
            
            // Set our new GameLoop to run.
            RunningGame = gameLoop;

            // Load the GameLoop's content and initialise it.
            RunningGame.LoadContent();
            RunningGame.Initialise();

            // Initialise SSprite.RealPosition.
            foreach(SSprite sprite in Sprites)
            {
                sprite.RealPosition = sprite.Position;
            }
        }

        public static void ResizeWindow()
        {
            Vector2u oldSize = Window.Size;
            Window.Size = Resolutions[CurrentResolution];
            Window.Position = new Vector2i(Window.Position.X - ((int)Window.Size.X - (int)oldSize.X) / 2, Window.Position.Y - ((int)Window.Size.Y - (int)oldSize.Y) / 2);
            Window.SetView(new View(new Vector2f(Window.Size.X / 2, Window.Size.Y / 2), new Vector2f(Window.Size.X, Window.Size.Y)));
            Scale = Math.Min((int)Window.Size.X / Texture.Size.X, (int)Window.Size.Y / Texture.Size.Y);
            TexturePosition = new Vector2f(Window.Size.X / 2 - Texture.Size.X * Scale / 2, Window.Size.Y / 2 - Texture.Size.Y * Scale / 2);

            Console.WriteLine("Window size: " + Window.Size);
            Console.WriteLine("Texture size: " + Texture.Size);
            Console.WriteLine("Scale: " + Scale);
        }
    }
}
