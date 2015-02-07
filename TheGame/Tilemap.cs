using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    /// <summary>
    /// Représente le terrain, constituée de tuiles.
    /// </summary>
    public class Tilemap
    {
        /// <summary>
        /// Retourne le nombre de tuiles en largeur
        /// </summary>
        public int Width
        {
            get
            {
                return _width;
            }
        }

        /// <summary>
        /// Retourne le nombre de tuiles en hauteur.
        /// </summary>
        public int Height
        {
            get
            {
                return _height;
            }
        }

        /// <summary>
        /// Récupère ou assigne une tuile à la position donnée.
        /// </summary>
        /// <param name="horizontalIndex">Position horizontale de la tuile dans la matrice.</param>
        /// <param name="verticalIndex">Position verticale de la tuile dans la matrice.</param>
        /// <returns></returns>
        public GroundType this[int horizontalIndex, int verticalIndex]
        {
            get
            {
                return _tiles[horizontalIndex, verticalIndex];
            }
            set
            {
                _tiles[horizontalIndex, verticalIndex] = value;
            }
        }

        /// <summary>
        /// Crée un nouvel ensemble de tuiles, avec les dimentions spécifiées.
        /// </summary>
        /// <param name="width">Nombre de tuiles en largeur.</param>
        /// <param name="height">Nombre de tuiles en hauteur.</param>
        public Tilemap(int width, int height)
        {
            _tiles = new GroundType[width, height];
            _width = width;
            _height = height;
        }

        #region Interne

        /// <summary>
        /// Le nombre de tuiles en largeur.
        /// </summary>
        private int _width;

        /// <summary>
        /// Le nombre de tuiles en hauteur.
        /// </summary>
        private int _height;

        /// <summary>
        /// Les tuiles.
        /// </summary>
        private GroundType[,] _tiles;

        #endregion
    }
}
