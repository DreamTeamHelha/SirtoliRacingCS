using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    /// <summary>
    /// Permet de charger une tilemap.
    /// </summary>
    public class TilemapLoader
    {
        /// <summary>
        /// Retourne les associations type de sol - couleur de sol à utiliser lors du chargement.
        /// </summary>
        public Dictionary<Color, GroundType> GroundColors
        {
            get;
            private set;
        }

        /// <summary>
        /// Crée un nouveau loader.
        /// </summary>
        public TilemapLoader()
        {
            GroundColors = new Dictionary<Color, GroundType>();
        }

        /// <summary>
        /// Charge une tilemap, construite à partir de l'image passée en paramètre. <br/>
        /// Chaque pixel de l'image sera converti en GroundTime en fonction des associations de 
        /// couleur définies par GroundColor.
        /// </summary>
        /// <param name="sourceImage">Image source.</param>
        /// <returns>La tilemap.</returns>
        public Tilemap Load(Image sourceImage)
        {
            if (sourceImage == null)
                throw new ArgumentNullException("'sourceImage'");

            var mapWidth = sourceImage.Size.X;
            var mapHeight = sourceImage.Size.Y;

            var tilemap = new Tilemap((int)mapWidth, (int)mapHeight);

            for (var horizontalIndex = 0U; horizontalIndex < mapWidth; horizontalIndex++)
            {
                for (var verticalIndex = 0U; verticalIndex < mapHeight; verticalIndex++)
                {
                    var color = sourceImage.GetPixel(horizontalIndex, verticalIndex);
                    var ground = GetGroundTypeFor(color);
                    tilemap[(int)horizontalIndex, (int)verticalIndex] = ground;
                }
            }

            return tilemap;
        }

        /// <summary>
        /// Retourne le type de sol correspondant à la couleur passée en paramètre.
        /// </summary>
        /// <param name="color">La couleur.</param>
        /// <returns>Le type de sol (Grass par défaut)</returns>
        private GroundType GetGroundTypeFor(Color color)
        {
            GroundType ground;
            GroundColors.TryGetValue(color, out ground);
            return ground;
        }
    }
}
