﻿using GameLibrary.Dependencies.Entities;
using GameLibrary.Entities.Components;
using GameLibrary.Entities.Components.Physics;
using GameLibrary.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceHordes.Entities.Components;

namespace SpaceHordes.Entities.Systems
{
    internal class HealthRenderSystem : EntityProcessingSystem
    {
        private SpriteBatch _SpriteBatch;
        private SpriteFont _SpriteFont;
        private Texture2D _BarTexture;

        private ComponentMapper<Health> healthMapper;
        private ComponentMapper<Body> bodyMapper;

        public HealthRenderSystem(SpriteBatch spriteBatch)
            : base(typeof(Health), typeof(Body))
        {
            this._SpriteBatch = spriteBatch;
            _BarTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            _BarTexture.SetData(CreateTextureData());
        }

        private Color[] CreateTextureData()
        {
            Color[] Colors = new Color[1 * 1];

            for (int x = 0; x < 1; x++)
            {
                for (int y = 0; y < 1; y++)
                {
                    Colors[x + y * 1] = Color.White;
                }
            }

            return Colors;
        }

        public void LoadContent(SpriteFont spriteFont)
        {
            this._SpriteFont = spriteFont;
        }

        public override void Initialize()
        {
            healthMapper = new ComponentMapper<Health>(world);
            bodyMapper = new ComponentMapper<Body>(world);
        }

        public override void Process(Entity e)
        {
            Health health = healthMapper.Get(e);
            Body body = bodyMapper.Get(e);

            int X = (int)ScreenHelper.Center.X;
            int Y = (int)ScreenHelper.Center.Y;
            if (e.HasComponent<Sprite>() && e.GetComponent<Sprite>().Source != null && health.MaxHealth > 1 && e.Tag != "SmasherBall")
            {
                float Width = e.GetComponent<Sprite>().CurrentRectangle.Width;
                float Height = e.GetComponent<Sprite>().CurrentRectangle.Height + 10;

                //Draw backing
                _SpriteBatch.Draw(_BarTexture,
                    new Rectangle(
                        X + (int)ConvertUnits.ToDisplayUnits(body.Position.X) - (int)Width / 2,
                        Y + (int)ConvertUnits.ToDisplayUnits(body.Position).Y - (int)Height / 2,
                        (int)Width, 2), Color.DarkRed);

                _SpriteBatch.Draw(_BarTexture,
                    new Rectangle(
                        X + (int)ConvertUnits.ToDisplayUnits(body.Position.X) - (int)Width / 2,
                        Y + (int)ConvertUnits.ToDisplayUnits(body.Position).Y - (int)Height / 2,
                        (int)((health.CurrentHealth / health.MaxHealth) * Width),
                            2), Color.Red);
            }
            else
            {
            }
        }

        public override void Process()
        {
            _SpriteBatch.Begin();
            base.Process();
            _SpriteBatch.End();
        }
    }
}