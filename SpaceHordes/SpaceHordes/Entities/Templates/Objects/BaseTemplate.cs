﻿using GameLibrary;
using GameLibrary.Dependencies.Entities;
using GameLibrary.Dependencies.Physics.Factories;
using GameLibrary.Entities.Components;
using GameLibrary.Entities.Components.Physics;
using GameLibrary.Helpers;
using Microsoft.Xna.Framework;
using SpaceHordes.Entities.Components;

namespace SpaceHordes.Entities.Templates
{
    internal class BaseTemplate : IEntityTemplate
    {
        private World world;
        private SpriteSheet spriteSheet;
        private Entity junkRock;

        public BaseTemplate(World world, SpriteSheet spriteSheet)
        {
            this.world = world;
            this.spriteSheet = spriteSheet;
        }

        public Entity BuildEntity(Entity e, params object[] args)
        {
            e.Group = "Base";
            e.Tag = "Base";

            #region Body

            Body Body = e.AddComponent<Body>(new Body(world, e));
            {
                FixtureFactory.AttachEllipse(//Add a basic bounding box (rectangle status)
                    ConvertUnits.ToSimUnits(spriteSheet.Animations["base"][0].Width / 2f),
                    ConvertUnits.ToSimUnits(spriteSheet.Animations["base"][0].Height / 2f),
                    20,
                    1,
                    Body);
                Body.Position = ConvertUnits.ToSimUnits(new Vector2(0, 0));
                Body.BodyType = GameLibrary.Dependencies.Physics.Dynamics.BodyType.Static;
                Body.CollisionCategories = GameLibrary.Dependencies.Physics.Dynamics.Category.Cat1;
                Body.CollidesWith = GameLibrary.Dependencies.Physics.Dynamics.Category.Cat2;

                Body.SleepingAllowed = false;
            }

            #endregion Body

            #region Sprite

            Sprite Sprite = e.AddComponent<Sprite>(
                new Sprite(spriteSheet, "base",
                    Body, 1, Color.White, 0.1f));

            #endregion Sprite

            e.AddComponent<Score>(new Score());

            int health = 10;
#if DEBUG
            health = 10000;
#endif

            e.AddComponent<Health>(new Health(health)).OnDeath +=
            ent =>
            {
                Vector2 poss = e.GetComponent<ITransform>().Position;
                world.CreateEntityGroup("BigExplosion", "Explosions", poss, 25, e);

                SoundManager.Play("Explosion1");
            };

            return e;
        }
    }
}