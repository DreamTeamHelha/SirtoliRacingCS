using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using Box2DX;
using Box2DX.Dynamics;
using Box2DX.Collision;
using Box2DX.Common;
using System.Reflection;
using System.IO;

namespace TheGame
{
    public static class Program
    {
        public static bool IsRunning { get; private set; }

        public static Jukebox Jukebox { get; private set; }

        public static void Main()
        {
            if (!Directory.Exists(Directories.DataDirectory))
                return;

            Jukebox = new Jukebox(Directories.MusicDirectory);
            Jukebox.Play();

            var textureManager = new TextureManager(Directories.TextureDirectory);
            var background = textureManager.GetTexture("background");

            var physicsWorld = new World(new AABB(), new Vec2(0F, 0F), true);

            var rendow = new RenderWindow(new VideoMode(800, 600), "Sirtoli Racing", Styles.Close);
            IsRunning = true;
            rendow.Closed += OnRendowClosed;
            rendow.KeyReleased += OnKeyReleased;

            do
            {
                rendow.Draw(new Sprite(background));
                rendow.Display();

                physicsWorld.Step(1 / 60F, 1, 1);
                rendow.DispatchEvents();

            } while (IsRunning);
        }

        static void OnKeyReleased(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.I:
                    Jukebox.PlayPrevious();
                    break;

                case Keyboard.Key.O:
                    Jukebox.PlayNext();
                    break;

                case Keyboard.Key.P:
                    if (Jukebox.IsPlaying)
                    {
                        Jukebox.Stop();
                    }
                    else
                    {
                        Jukebox.Play();
                    }
                    break;
            }
        }

        private static void OnRendowClosed(object sender, EventArgs e)
        {
            IsRunning = false;
        }
    }
}
