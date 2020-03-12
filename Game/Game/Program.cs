using System;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;

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
            Breakout = 4,
            Menu = 5
        }

        public const int TARGET_FPS = 60;
        public const float TIME_UNTIL_UPDATE = 1f / TARGET_FPS;
        public static readonly Color TextureClearColour = Color.Black;
        public static readonly Color WindowClearColour = Color.White;

        public static RenderTexture Texture;
        public static Vector2f TexturePosition;
        public static RenderWindow Window;
        public static int CurrentResolution = 0;
        public static List<Vector2u> Resolutions = new List<Vector2u>
        {
            new Vector2u(804, 600), // 3x
            new Vector2u(1072, 800), // 4x
            new Vector2u(1340, 1000), // 5x
            new Vector2u(1608, 1200), // 6x
            new Vector2u(1876, 1400), // 7x
            new Vector2u(2144, 1600) // 8x
        };
        public static float Scale = 3f;
        public static bool Fullscreen;

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
            Texture = new RenderTexture(268, 200);
            Window = new RenderWindow(new VideoMode(Resolutions[0].X, Resolutions[0].Y), "Steenboy Color", Styles.Close);
            ResizeWindow();
            Window.Closed += Window_Closed;

            MyFont = new Font("Content/arialbd.ttf");

            LoadNewGame(new Snake());

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
                            case GameName.Menu:
                                LoadNewGame(new Menu());
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

        // Click is called by Update every time LMB is pressed.
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

        //to play a sound.
        public static void PlaySound(string filename)
        {
            var sound = new Sound(new SoundBuffer(filename));
            sound.Loop = true;
            sound.Volume = 50;
            sound.Play();
        }

        // Update is called once every tick from the game loop.
        public static void Update(GameTime gameTime)
        {
            // Check whether buttons are moused over.
            Vector2f mousePosition = Texture.MapPixelToCoords(Mouse.GetPosition(Window));
            foreach (SSprite sprite in Sprites)
            {
                Button button = sprite as Button;
                
                if(button != null)
                {
                    if(button.JustClicked > 0)
                    {
                        button.JustClicked -= 1;
                    }

                    bool mouseOver = (button.GetGlobalBounds().Contains((mousePosition.X - TexturePosition.X) / Scale, (mousePosition.Y - TexturePosition.Y) / Scale) && button.JustClicked == 0);

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

            // Run GameLoop update function.
            RunningGame.Update(gameTime);

            // Check for mouse clicks.
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (!LeftPressed)
                {
                    LeftPressed = true;
                    Click();
                }
            }
            else
            {
                LeftPressed = false;
            }



            // Check for keyboard input.
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                RunningGame.KeyInput(Keyboard.Key.A);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                RunningGame.KeyInput(Keyboard.Key.W);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                RunningGame.KeyInput(Keyboard.Key.D);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                RunningGame.KeyInput(Keyboard.Key.S);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                RunningGame.KeyInput(Keyboard.Key.Return);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                RunningGame.KeyInput(Keyboard.Key.Space);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Tab))
            {
                RunningGame.KeyInput(Keyboard.Key.Tab);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                RunningGame.KeyInput(Keyboard.Key.Up);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                RunningGame.KeyInput(Keyboard.Key.Down);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                RunningGame.KeyInput(Keyboard.Key.Left);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                RunningGame.KeyInput(Keyboard.Key.Right);
            }
        }

        // Draw is called once every tick by the game loop.
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
            Strings.Clear();
            
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
            foreach(SText text in Strings)
            {
                text.RealPosition = text.Position;
            }
        }

        public static void ResizeWindow(bool fullscreen = false)
        {
            Vector2u oldSize = Window.Size;

            if (fullscreen)
            {
                Window.Close();
                Window = new RenderWindow(new VideoMode(Resolutions[0].X, Resolutions[0].Y), "Steenboy Color", Styles.Fullscreen);
                Window.Closed += Window_Closed;

                Fullscreen = true;
            }
            else
            {
                if(Fullscreen)
                {
                    Window.Close();
                    Window = new RenderWindow(new VideoMode(Resolutions[0].X, Resolutions[0].Y), "Steenboy Color", Styles.Close);
                    Window.Closed += Window_Closed;

                    Fullscreen = false;
                }

                Window.Size = Resolutions[CurrentResolution];
            }

            Window.Position = new Vector2i(Math.Max((int)VideoMode.DesktopMode.Width / 2 - (int)Window.Size.X / 2, 0), Math.Max((int)VideoMode.DesktopMode.Height / 2 - (int)Window.Size.Y / 2, 0));
            Window.SetView(new View(new Vector2f(Window.Size.X / 2, Window.Size.Y / 2), new Vector2f(Window.Size.X, Window.Size.Y)));
            Scale = Math.Min((int)Window.Size.X / Texture.Size.X, (int)Window.Size.Y / Texture.Size.Y);
            TexturePosition = new Vector2f(Window.Size.X / 2 - Texture.Size.X * Scale / 2, Window.Size.Y / 2 - Texture.Size.Y * Scale / 2);
        }
    }
}
