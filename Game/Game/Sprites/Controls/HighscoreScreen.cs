using System;
using System.Collections.Generic;
using System.Text;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Data.SQLite;

namespace Game
{
    public class HighscoreScreen : SSprite
    {
        SText[] Initials;
        SText Header;
        Button[,] CharButtons;
        Button DiscardButton;
        Button SubmitButton;
        string GameName;
        int Score;
        int Level;

        public HighscoreScreen() { }

        public HighscoreScreen(string game, int score, int level = 0, string group1 = null, string group2 = null) : base(new Texture("Content/HighscoreScreen/Background.png"), group1, group2)
        {
            this.GameName = game;
            this.Score = score;
            this.Level = level;
            if (Position.X == 0 && Position.Y == 0)
            {
                Position = new Vector2f(Program.Texture.Size.X / 2 - Texture.Size.X / 2, Program.Texture.Size.Y / 2 - Texture.Size.Y / 2);
            }

            this.Discard += CloseScreen;
            this.Submit += SubmitScore;
            this.Submit += CloseScreen;

            Initials = new SText[3];
            CharButtons = new Button[3, 2];

            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    CharButtons[i, j] = new Button(new Texture("Content/HighscoreScreen/Option" + (j == 0 ? "Up" : "Down") + ".png"));

                    CharButtons[i, j].SetPosition(this.Position.X + 20 + 33 * i, this.Position.Y + 22 + 42 * j);
                }
                Initials[i] = new SText("A", (float)CharButtons[i, 0].Position.X + 5, (float)(CharButtons[i, 0].Position.Y + CharButtons[i, 0].Texture.Size.Y), 20);
            }

            Header = new SText("Submit Highscore " + score, 11);
            Header.SetPosition(this.Position.X + 3, this.Position.Y + 3);

            DiscardButton = new Button(new Texture("Content/HighscoreScreen/Discard.png"));
            DiscardButton.SetPosition(this.Position.X + 2, this.Position.Y + 79);
            SubmitButton = new Button(new Texture("Content/HighscoreScreen/Submit.png"));
            SubmitButton.SetPosition(this.Position.X + 68, this.Position.Y + 79);

            CharButtons[0, 0].Click += Button0Click;
            CharButtons[0, 0].MouseEnter += Button0Enter;
            CharButtons[0, 0].MouseLeave += Button0Leave;
            CharButtons[0, 1].Click += Button1Click;
            CharButtons[0, 1].MouseEnter += Button1Enter;
            CharButtons[0, 1].MouseLeave += Button1Leave;
            CharButtons[1, 0].Click += Button2Click;
            CharButtons[1, 0].MouseEnter += Button2Enter;
            CharButtons[1, 0].MouseLeave += Button2Leave;
            CharButtons[1, 1].Click += Button3Click;
            CharButtons[1, 1].MouseEnter += Button3Enter;
            CharButtons[1, 1].MouseLeave += Button3Leave;
            CharButtons[2, 0].Click += Button4Click;
            CharButtons[2, 0].MouseEnter += Button4Enter;
            CharButtons[2, 0].MouseLeave += Button4Leave;
            CharButtons[2, 1].Click += Button5Click;
            CharButtons[2, 1].MouseEnter += Button5Enter;
            CharButtons[2, 1].MouseLeave += Button5Leave;
            DiscardButton.Click += Discard;
            SubmitButton.Click += Submit;
            DiscardButton.MouseEnter += DiscardEnter;
            DiscardButton.MouseLeave += DiscardLeave;
            SubmitButton.MouseEnter += SubmitEnter;
            SubmitButton.MouseLeave += SubmitLeave;

            Program.HighscoreScreenUp = true;
        }

        // The Submit button.
        public event EventHandler Submit;
        public void PerformSubmit()
        {
            Submit(this, EventArgs.Empty);
        }

        // The Discard button.
        public event EventHandler Discard;
        public void PerformDiscard()
        {
            Discard(this, EventArgs.Empty);
        }

        // SubmitScore submits the highscore to the database.
        // It is called when the submit button is clicked.
        public void SubmitScore(object sender, EventArgs e)
        {
            var command = new SQLiteCommand(Program.SQLiteConn);
            bool level = GameName == "breakout";
            command.CommandText = "INSERT INTO " + GameName + "(time, name, score" + (level ? ", level" : "") + ") VALUES('" + DateTime.Now + "', '"+ Initials[0].DisplayedString + Initials[1].DisplayedString + Initials[2].DisplayedString + "', " + Score + (level ? ", " + Level : " ") + ")";
            command.ExecuteNonQuery();
        }

        // CloseScreen closes this HighscoreScreen by removing it from Program's sprite list.
        // It is called when either the submit or the discard button is clicked.
        public void CloseScreen(object sender, EventArgs e)
        {
            foreach (Button button in CharButtons)
            {
                Program.Sprites.Remove(button);
            }

            foreach(SText sText in Initials)
            {
                Program.Strings.Remove(sText);
            }

            Program.Sprites.Remove(SubmitButton);

            Program.Sprites.Remove(DiscardButton);

            Program.Strings.Remove(Header);

            Program.Sprites.Remove(this);

            Program.HighscoreScreenUp = false;
        }

        // Increase first character button
        public void Button0Click(object sender, EventArgs e)
        {
            if(this.Initials[0].DisplayedString[0] < 'Z')
            {
                this.Initials[0].DisplayedString = this.Initials[0].DisplayedString.Replace(this.Initials[0].DisplayedString[0], (char)(this.Initials[0].DisplayedString[0] + 1));
            }
            else
            {
                this.Initials[0].DisplayedString = this.Initials[0].DisplayedString.Replace(this.Initials[0].DisplayedString[0], 'A');
            }
        }
        public void Button0Enter(object sender, EventArgs e)
        {
            CharButtons[0, 0].SetScale(1.1f, Pin.Middle);
        }
        public void Button0Leave(object sender, EventArgs e)
        {
            CharButtons[0, 0].SetScale(1f, Pin.Middle);
        }

        // Decrease first character button
        public void Button1Click(object sender, EventArgs e)
        {
            if (this.Initials[0].DisplayedString[0] > 'A')
            {
                this.Initials[0].DisplayedString = this.Initials[0].DisplayedString.Replace(this.Initials[0].DisplayedString[0], (char)(this.Initials[0].DisplayedString[0] - 1));
            }
            else
            {
                this.Initials[0].DisplayedString = this.Initials[0].DisplayedString.Replace(this.Initials[0].DisplayedString[0], 'Z');
            }
        }
        public void Button1Enter(object sender, EventArgs e)
        {
            CharButtons[0, 1].SetScale(1.1f, Pin.Middle);
        }
        public void Button1Leave(object sender, EventArgs e)
        {
            CharButtons[0, 1].SetScale(1f, Pin.Middle);
        }


        // Increase second character button
        public void Button2Click(object sender, EventArgs e)
        {
            if (this.Initials[1].DisplayedString[0] < 'Z')
            {
                this.Initials[1].DisplayedString = this.Initials[1].DisplayedString.Replace(this.Initials[1].DisplayedString[0], (char)(this.Initials[1].DisplayedString[0] + 1));
            }
            else
            {
                this.Initials[1].DisplayedString = this.Initials[1].DisplayedString.Replace(this.Initials[1].DisplayedString[0], 'A');
            }
        }
        public void Button2Enter(object sender, EventArgs e)
        {
            CharButtons[1, 0].SetScale(1.1f, Pin.Middle);
        }
        public void Button2Leave(object sender, EventArgs e)
        {
            CharButtons[1, 0].SetScale(1f, Pin.Middle);
        }

        // Decrease second character button
        public void Button3Click(object sender, EventArgs e)
        {
            if (this.Initials[1].DisplayedString[0] > 'A')
            {
                this.Initials[1].DisplayedString = this.Initials[1].DisplayedString.Replace(this.Initials[1].DisplayedString[0], (char)(this.Initials[1].DisplayedString[0] - 1));
            }
            else
            {
                this.Initials[1].DisplayedString = this.Initials[1].DisplayedString.Replace(this.Initials[1].DisplayedString[0], 'Z');
            }
        }
        public void Button3Enter(object sender, EventArgs e)
        {
            CharButtons[1, 1].SetScale(1.1f, Pin.Middle);
        }
        public void Button3Leave(object sender, EventArgs e)
        {
            CharButtons[1, 1].SetScale(1f, Pin.Middle);
        }


        // Increase third character button
        public void Button4Click(object sender, EventArgs e)
        {
            if (this.Initials[2].DisplayedString[0] < 'Z')
            {
                this.Initials[2].DisplayedString = this.Initials[2].DisplayedString.Replace(this.Initials[2].DisplayedString[0], (char)(this.Initials[2].DisplayedString[0] + 1));
            }
            else
            {
                this.Initials[2].DisplayedString = this.Initials[2].DisplayedString.Replace(this.Initials[2].DisplayedString[0], 'A');
            }
        }
        public void Button4Enter(object sender, EventArgs e)
        {
            CharButtons[2, 0].SetScale(1.1f, Pin.Middle);
        }
        public void Button4Leave(object sender, EventArgs e)
        {
            CharButtons[2, 0].SetScale(1f, Pin.Middle);
        }

        // Decrease third character button
        public void Button5Click(object sender, EventArgs e)
        {
            if (this.Initials[2].DisplayedString[0] > 'A')
            {
                this.Initials[2].DisplayedString = this.Initials[2].DisplayedString.Replace(this.Initials[2].DisplayedString[0], (char)(this.Initials[2].DisplayedString[0] - 1));
            }
            else
            {
                this.Initials[2].DisplayedString = this.Initials[2].DisplayedString.Replace(this.Initials[2].DisplayedString[0], 'Z');
            }
        }
        public void Button5Enter(object sender, EventArgs e)
        {
            CharButtons[2, 1].SetScale(1.1f, Pin.Middle);
        }
        public void Button5Leave(object sender, EventArgs e)
        {
            CharButtons[2, 1].SetScale(1f, Pin.Middle);
        }

        // Discard Button
        public void DiscardEnter(object sender, EventArgs e)
        {
            DiscardButton.SetScale(1.1f, Pin.Middle);
        }
        public void DiscardLeave(object sender, EventArgs e)
        {
            DiscardButton.SetScale(1f, Pin.Middle);
        }

        // Submit Button
        public void SubmitEnter(object sender, EventArgs e)
        {
            SubmitButton.SetScale(1.1f, Pin.Middle);
        }
        public void SubmitLeave(object sender, EventArgs e)
        {
            SubmitButton.SetScale(1f, Pin.Middle);
        }
    }
}
