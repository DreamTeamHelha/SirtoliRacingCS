using Box2DX.Collision;
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

            var textureManager = new TextureManager(Directories.TextureDirectory);
            var background = textureManager.GetTexture("background");

            var physicsWorld = new World(new AABB(), new Vec2(0F, 0F), true);

            var loader = new TilemapLoader();
            loader.GroundColors.Add(new SFML.Graphics.Color(56, 52, 51), GroundType.Asphalt);
            loader.GroundColors.Add(new SFML.Graphics.Color(91, 65, 32), GroundType.Mud);
            var trackSource = new Image(Path.Combine(Directories.TrackDirectory, "Raph_Paradise.png"));
            var tilemap = loader.Load(trackSource);
            var tilemapRenderer = new TilemapRenderer();
            tilemapRenderer.Tiles.Add(GroundType.Grass, new Sprite(textureManager.GetTexture("GrassTile")));
            tilemapRenderer.Tiles.Add(GroundType.Asphalt, new Sprite(textureManager.GetTexture("RoadTile")));
            tilemapRenderer.Tiles.Add(GroundType.Mud, new Sprite(textureManager.GetTexture("MudTile")));

            var rendow = new RenderWindow(new VideoMode(800, 600), "Sirtoli Racing", Styles.Close);
            rendow.SetKeyRepeatEnabled(false);
            rendow.SetFramerateLimit(30);
            IsRunning = true;
            rendow.Closed += OnRendowClosed;
            rendow.KeyPressed += playerInput.OnKeyPressed;
            rendow.KeyReleased += playerInput.OnKeyReleased;

            jukebox.Play();

            do
            {
                tilemapRenderer.Render(rendow, tilemap, RenderStates.Default);
                rendow.Display();

                physicsWorld.Step(1 / 60F, 1, 1);

                playerInput.PreUpdate();
                jukebox.Update();
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

                var view = rendow.GetView();
                var deltaPos = new Vector2f();
                deltaPos.X += playerInput.Turn;
                deltaPos.Y += playerInput.Braking;
                deltaPos.Y -= playerInput.Throttle;
                view.Center = view.Center + deltaPos * 15F;
                rendow.SetView(view);

            } while (IsRunning);
        }

        private static void OnRendowClosed(object sender, EventArgs e)
        {
            IsRunning = false;
        }
    }
}
