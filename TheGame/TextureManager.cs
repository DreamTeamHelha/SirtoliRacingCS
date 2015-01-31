using SFML;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    /// <summary>
    /// Gère l'accès aux textures utilisées dans le jeu.
    /// </summary>
    public class TextureManager
    {
        public TextureManager(string textureDir)
        {
            LoadFromDirectory(textureDir, true);
        }

        /// <summary>
        /// Retourne la texture de nom spécifié, ou null si elle n'existe pas.
        /// </summary>
        /// <param name="textureName">Nom de la texture.</param>
        /// <returns>La texture; null si elle n'exite pas.</returns>
        public Texture GetTexture(string textureName)
        {
            Texture texture;
            if (!_textures.TryGetValue(textureName.ToLowerInvariant(), out texture))
            {
                texture = null;
            }

            return texture;
        }

        #region Interne

        /// <summary>
        /// Contient l'ensemble des textures, ordonnées par nom.
        /// </summary>
        private Dictionary<string, Texture> _textures = new Dictionary<string, Texture>();

        /// <summary>
        /// Charge toutes les textures présentes dans le dossier, y compris ses sous-dossiers.<br/>
        /// Chaque image doit avoir un nom unique, peu importe son arborescence et son type.
        /// </summary>
        /// <param name="directoryName">Nom du dossier à scanner.</param>
        /// <param name="recursive">Indique si les sous-dossiers doivent être également scannés.</param>
        /// <exception cref="ArgumentException"/>
        private void LoadFromDirectory(string directoryName, bool recursive = true)
        {
            if (recursive)
            {
                foreach (var subDirectoryName in Directory.EnumerateDirectories(directoryName))
                {
                    LoadFromDirectory(subDirectoryName, true);
                }
            }

            foreach (var filename in Directory.EnumerateFiles(directoryName))
            {
                var texture = LoadTexture(filename);
                if (texture == null) continue;

                var textureName = Path.GetFileNameWithoutExtension(filename).ToLowerInvariant();
                CheckTextureNameAvailable(textureName);
                _textures[textureName] = texture;
            }
        }

        /// <summary>
        /// Charge la texture de nom spécifié, et la retourne.
        /// </summary>
        /// <param name="filename">Chemin vers la texture.</param>
        /// <returns>La texture; null si le chargement a échoué.</returns>
        private Texture LoadTexture(string filename)
        {
            Texture texture;
            try
            {
                texture = new Texture(filename);
            }
            catch (LoadingFailedException)
            {
                texture = null;
            }

            return texture;
        }

        /// <summary>
        /// Vérifie qu'un nom de texture soit disponible.<br/>
        /// Lance une ArgumentException si cela n'est pas le cas.
        /// </summary>
        /// <param name="textureName">Nom de la texture à vérifier.</param>
        /// <exception cref="ArgumentException"/>
        private void CheckTextureNameAvailable(string textureName)
        {
            if (_textures.ContainsKey(textureName))
                throw new ArgumentException(String.Format("A texture called '{0}' already exists!"));
        }

        #endregion
    }
}
