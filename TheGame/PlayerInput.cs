using SFML.Window;

namespace TheGame
{
    /// <summary>
    /// Encapsule la gestion des commandes du joueur.
    /// </summary>
    public class PlayerInput
    {
        /// <summary>
        /// Direction.
        /// </summary>
        public float Turn
        {
            get;
            private set;
        }

        /// <summary>
        /// Accélérateur.
        /// </summary>
        public float Throttle
        {
            get;
            private set;
        }

        /// <summary>
        /// Freins.
        /// </summary>
        public float Braking
        {
            get;
            private set;
        }

        /// <summary>
        /// Indique s'il faut jouer/éteindre la musique.
        /// </summary>
        public bool ToggleRadio
        {
            get;
            private set;
        }

        /// <summary>
        /// Indique s'il faut jouer la musique suivante.
        /// </summary>
        public bool NextSong
        {
            get;
            private set;
        }

        /// <summary>
        /// Indique s'il faut jouer la musique précédente.
        /// </summary>
        public bool PreviousSong
        {
            get;
            private set;
        }

        /// <summary>
        /// À apeller avant que la RenderWindow ne dispatche ses événements.
        /// </summary>
        public void PreUpdate()
        {
            NextSong = false;
            PreviousSong = false;
            ToggleRadio = false;
        }

        /// <summary>
        /// À connecter à l'événemnet KeyPressed de la RenderWindow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnKeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.Up:
                    Throttle += 1F;
                    break;

                case Keyboard.Key.Down:
                    Throttle -= 1F;
                    break;

                case Keyboard.Key.Left:
                    Turn -= 1F;
                    break;

                case Keyboard.Key.Right:
                    Turn += 1F;
                    break;

                default:
                    // rien à faire, pas géré.
                    break;
            }
        }

        /// <summary>
        /// À connecter à l'événemnet KeyReleased de la RenderWindow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnKeyReleased(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.Up:
                    Throttle -= 1F;
                    break;

                case Keyboard.Key.Down:
                    Throttle += 1F;
                    break;

                case Keyboard.Key.Left:
                    Turn += 1F;
                    break;

                case Keyboard.Key.Right:
                    Turn -= 1F;
                    break;

                case Keyboard.Key.I:
                    PreviousSong = true;
                    break;

                case Keyboard.Key.O:
                    NextSong = true;
                    break;

                case Keyboard.Key.P:
                    ToggleRadio = true;
                    break;

                default:
                    // rien à faire, pas géré.
                    break;
            }
        }
    }
}
