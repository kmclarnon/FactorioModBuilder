﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public class Entity : ProjectItem<Entity>
    {
        public Entity() : base("Entities")
        {
        }
    }
}
