﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Entities
{
    public interface IEntityTemplate
    {
        Entity BuildEntity(Entity e, params object[] args);
    }
}