using System.IO;

namespace TheGame
{
    /// <summary>
    /// Encapsule l'ensemble des dossiers du jeu.
    /// </summary>
    public static class Directories
    {
        /// <summary>
        /// Retourne le chemin racine des données.
        /// </summary>
        public static string DataDirectory
        {
            get
            {
                return "Data";
            }
        }

        /// <summary>
        /// Retourne le chemin vers le dossier contenant les musiques du jeu.
        /// </summary>
        public static string MusicDirectory
        {
            get
            {
                return Path.Combine(DataDirectory, "Sounds");
            }
        }

        /// <summary>
        /// Retourne le chemin vers le dossier contenant toutes les textures du jeu.
        /// </summary>
        public static string TextureDirectory
        {
            get
            {
                return Path.Combine(DataDirectory, "Pictures");
            }
        }

        /// <summary>
        /// Retourne le chemin vers le dossier contenant les circuits du jeu.
        /// </summary>
        public static string TrackDirectory
        {
            get
            {
                return Path.Combine(DataDirectory, "Tracks");
            }
        }
    }
}
