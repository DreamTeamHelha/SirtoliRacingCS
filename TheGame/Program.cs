﻿using Box2DX.Collision;
using Box2DX.Common;
using Box2DX.Dynamics;
using SFML.Graphics;
using SFML.Window;
using System;
using System.IO;

namespace TheGame
{
    public static class Program
    {
        public static bool IsRunning { get; private set; }

        public static void Main()
        {
            if (!Directory.Exists(Directories.DataDirectory))
                return;

            var playerInput = new PlayerInput();

            var jukebox = new Jukebox(Directories.MusicDirectory);
            jukebox.Play();

            var textureManager = new TextureManager(Directories.TextureDirectory);
            var background = textureManager.GetTexture("background");

            var physicsWorld = new World(new AABB(), new Vec2(0F, 0F), true);

            var rendow = new RenderWindow(new VideoMode(800, 600), "Sirtoli Racing", Styles.Close);
            IsRunning = true;
            rendow.Closed += OnRendowClosed;
            rendow.KeyPressed += playerInput.OnKeyPressed;
            rendow.KeyReleased += playerInput.OnKeyReleased;

            do
            {
                rendow.Draw(new Sprite(background));
                rendow.Display();

                physicsWorld.Step(1 / 60F, 1, 1);

                playerInput.PreUpdate();
                rendow.DispatchEvents();

                if (playerInput.PreviousSong)
                {
                    jukebox.PlayPrevious();
                }

                if (playerInput.NextSong)
                {
                    jukebox.PlayNext();
                }

                if (playerInput.ToggleRadio)
                {
                    if (jukebox.IsPlaying)
                    {
                        jukebox.Stop();
                    }
                    else
                    {
                        jukebox.Play();
                    }
                }

            } while (IsRunning);
        }

        private static void OnRendowClosed(object sender, EventArgs e)
        {
            IsRunning = false;
        }
    }
}
