using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    /// <summary>
    /// Classe de base pour les objets pouvant être placés dans un niveau.
    /// </summary>
    public class GameObject
    {
        /// <summary>
        /// Position de l'objet dans la map.
        /// </summary>
        public Vector2f Position { get; set; }

        /// <summary>
        /// Rotation de l'objet.
        /// </summary>
        public float Angle { get; set; }
    }
}
