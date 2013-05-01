﻿using GameLibrary.Dependencies.Entities;
using System.Collections.Generic;

namespace SpaceHordes.Entities.Templates
{
    public class StarFieldTemplate : IEntityGroupTemplate
    {
        private List<Entity> stars = new List<Entity>();
        private static int starNum = 400;
        private static int nebNum = 12;

        public Entity[] BuildEntityGroup(EntityWorld world, params object[] args)
        {
            for (int i = 0; i < starNum; ++i)
            {
                Entity e = world.CreateEntity("Star", true);
                e.Refresh();
                stars.Add(e);
            }

            for (int i = 0; i < nebNum; ++i)
            {
                Entity e = world.CreateEntity("Star", false);
                e.Refresh();
                stars.Add(e);
            }

            return stars.ToArray();
        }
    }
}