using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TheGame
{
    /// <summary>
    /// Classe permettant d'afficher les tilemaps.
    /// </summary>
    public class TilemapRenderer
    {
        /// <summary>
        /// Retourne les tuiles représentant les différents types de sol.
        /// </summary>
        public Dictionary<GroundType, Sprite> Tiles
        {
            get;
            private set;
        }

        /// <summary>
        /// Retourne ou assigne la taille d'une tuile (en texels).<br/>
        /// Correspond à l'offset donné à chaque 'case' de la tilemap lors de l'affichage,
        /// et est sensé corresponder à la taille des sprite utilisé.
        /// </summary>
        public float TileSize { get; set; }

        /// <summary>
        /// Crée un nouveau renderer.
        /// </summary>
        public TilemapRenderer()
        {
            Tiles = new Dictionary<GroundType, Sprite>();
            TileSize = 32F;
        }

        public void Render(RenderTarget renderTarget, Tilemap tilemap, RenderStates renderStates)
        {
            if (renderTarget == null)
                throw new ArgumentNullException("renderTarget");

            // cas de 'non-affichage'
            if (tilemap == null) return;
            if (Tiles.Count == 0) return;
            if (TileSize == 0F) return;

            // restreint le nombre de tuiles à afficher en fonction du viewport.
            var view = renderTarget.GetView();
            var viewHalfSize = view.Size / 2F;
            var viewPort = new Vector2i(
                (int)(view.Center.X - viewHalfSize.X), 
                (int)(view.Center.Y - viewHalfSize.Y)
            );
            var startColumn = viewPort.X / (int)TileSize;
            var startRow = viewPort.Y / (int)TileSize;
            var endColumn = startColumn + ((int)view.Size.X / (int)TileSize) + 2;
            var endRow = startRow + ((int)view.Size.Y / (int)TileSize) + 2;

            // s'assure que les indices de tuile soient corrects
            startColumn = startColumn < 0 ? 0 : startColumn;
            startRow = startRow < 0 ? 0 : startRow;
            endColumn = endColumn > tilemap.Width ? tilemap.Width : endColumn;
            endRow = endRow > tilemap.Height ? tilemap.Height : endRow;

            // affichage
            var tilePosition = new Vector2f(startColumn * TileSize, startRow * TileSize);
            GroundType previousGround = Tiles.Keys.First();
            Sprite tile;
            Tiles.TryGetValue(previousGround, out tile);

            for (var horizontalIndex = startColumn; horizontalIndex < endColumn; horizontalIndex++)
            {
                for (var verticalIndex = startRow; verticalIndex < endRow; verticalIndex++)
                {
                    var ground = tilemap[horizontalIndex, verticalIndex];
                    if (ground != previousGround)
                    {
                        Tiles.TryGetValue(ground, out tile);
                        previousGround = ground;
                    }

                    if (tile != null)
                    {
                        tile.Position = tilePosition;
                        renderTarget.Draw(tile, renderStates);
                    }

                    tilePosition.Y += TileSize;
                }

                tilePosition.X += TileSize;
                tilePosition.Y = startRow * TileSize;
            }
        }
    }
}
