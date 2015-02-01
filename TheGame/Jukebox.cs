using SFML.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TheGame
{
    /// <summary>
    /// Gère la diffusion de la liste de lecture.
    /// </summary>
    public class Jukebox
    {
        /// <summary>
        /// Retourne une valeur indiquant si une musique est en train d'être jouée ou non.
        /// </summary>
        public bool IsPlaying
        {
            get
            {
                return _song != null;
            }
        }

        /// <summary>
        /// Retourne le nom des musiques connues par le jukebox.
        /// </summary>
        public IReadOnlyList<string> Collection
        {
            get
            {
                return (IReadOnlyList<string>)_collection.AsReadOnly();
            }
        }

        /// <summary>
        /// Crée un nouveau Jukebox.
        /// </summary>
        /// <param name="songDir">Le dossier où trouver les musiques.</param>
        public Jukebox(string songDir)
        {
            _collection = (from file in Directory.EnumerateFiles(songDir)
                           where Path.GetExtension(file).Equals(".wav", StringComparison.InvariantCultureIgnoreCase)
                           select file).ToList();
        }

        /// <summary>
        /// Joue la musique actuelle dans la file d'attente.
        /// </summary>
        public void Play()
        {
            if (IsPlaying) return;
            if (_collection.Count == 0) return;

            var songIndex = _currentSongIndex;
            if (songIndex == -1) // aucune musique n'a encore été jouée.
            {
                songIndex = PeekNextSongIndex();
            }

            Play(songIndex);
        }

        /// <summary>
        /// Joue la musique du nom spécifié.<br/>
        /// Le nom doit être présent dans la collection, sinon l'appel est ignoré.
        /// </summary>
        /// <param name="songName">Nom de la musique.</param>
        public void Play(string songName)
        {
            var songIndex = _collection.FindIndex(s => s.Equals(songName));
            if (songIndex == -1) return;

            Play(songIndex);
        }

        /// <summary>
        /// Joue la musique correspondant à l'indice spécifié.
        /// </summary>
        /// <param name="songIndex">L'indice de la musique.</param>
        /// <exception cref="IndexOutOfRangeException">Indice invalide.</exception>
        public void Play(int songIndex)
        {
            CheckSongIndexValid(songIndex);

            if (_song != null)
            {
                _song.Stop();
            }

            _song = new Music(_collection[songIndex]);
            _song.Play();
            _currentSongIndex = songIndex;
        }

        /// <summary>
        /// Joue la musique suivante dans la liste, ou la première si l'on est à la fin.
        /// </summary>
        public void PlayNext()
        {
            int songIndex = PeekNextSongIndex();
            if (songIndex == -1) return;

            Play(songIndex);
        }

        /// <summary>
        /// Joue la musique précédente dans la liste, ou la dernière si l'on est au début.
        /// </summary>
        public void PlayPrevious()
        {
            int songIndex = PeekNextSongIndex(true);
            if (songIndex == -1) return;

            Play(songIndex);
        }

        /// <summary>
        /// Arrête la musique actuellement jouée.
        /// </summary>
        public void Stop()
        {
            if (!IsPlaying) return;

            _song.Stop();
            _song = null;
        }

        /// <summary>
        /// À appeller régulièrement pour que le jukebox joue tout seul.
        /// </summary>
        public void Update()
        {
            if (_song == null) return;

            if (_song.Status == SoundStatus.Stopped)
            {
                PlayNext();
            }
        }

        #region Interne

        /// <summary>
        /// Liste des musiques connues par le jukebox.
        /// </summary>
        private List<string> _collection;

        /// <summary>
        /// Musique actuellement jouée par le jukebox.
        /// </summary>
        private Music _song;

        /// <summary>
        /// Indice dans la collection de la musique actuellement jouée.
        /// </summary>
        private int _currentSongIndex = -1;

        /// <summary>
        /// Vérifie la validité de l'indice de musique donnée.<br/>
        /// Lance une IndexOutOfRangeException si ce n'est pas le cas.
        /// </summary>
        /// <param name="index">Indice de la musique.</param>
        ///<exception cref="IndexOutOfRangeException"/>
        private void CheckSongIndexValid(int index)
        {
            if (index < 0 || index >= _collection.Count)
                throw new IndexOutOfRangeException("'index'");
        }

        /// <summary>
        /// Vérifie que la référence vers la musique actuellement jouée soit non nulle.<br/>
        /// Lance une InvalidOperationException si la référence manque.
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        private void CheckSongNotNull()
        {
            if (_song == null)
                throw new InvalidOperationException("null song!");
        }

        /// <summary>
        /// Retourne l'indice de la musique à jouer ensuite.
        /// </summary>
        /// <param name="reverse">True s'il faut parcourir la liste à l'envers.</param>
        /// <returns>L'indice de la musique suivante; -1 s'il n'y en a pas.</returns>
        private int PeekNextSongIndex(bool reverse = false)
        {
            if (_collection.Count == 0) return -1;

            int nextIndex = _currentSongIndex;
            if (reverse)
            {
                nextIndex--;
                if (nextIndex < 0)
                {
                    nextIndex = _collection.Count - 1;
                }
            }
            else
            {
                nextIndex++;
                if (nextIndex >= _collection.Count)
                {
                    nextIndex = 0;
                }
            }

            return nextIndex;
        }

        #endregion
    }
}
