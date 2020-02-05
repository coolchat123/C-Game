using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    // De GameTime class bevat een aantal properties die met tijd te maken hebben.
    public class GameTime
    {
        // deltaTime houdt bij hoeveel tijd er is verlopen sinds de laatste tick
        private float _deltaTime = 0f;
        public float DeltaTime
        {
            get { return _deltaTime * _timeScale; }
            set { _deltaTime = value; }
        }
        public float DeltaTimeUnscaled
        {
            get { return _deltaTime; }
        }

        // timeScale houdt bij hoe snel de tijd verloopt
        private float _timeScale = 1f;
        public float TimeScale
        {
            get { return _timeScale; }
            set { _timeScale = value; }
        }

        // TotalTimeElapsed houdt bij hoeveel tijd er in totaal verlopen is
        public float TotalTimeElapsed
        {
            get;
            private set;
        }

        // de constructor van de GameTime class
        public GameTime()
        {

        }

        // Update updatet alle properties in de GameTime class
        public void Update(float deltaTime, float totalTimeElapsed)
        {
            _deltaTime = deltaTime;
            TotalTimeElapsed = totalTimeElapsed;
        }
    }
}
