﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Tour : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartingArea { get; set; }
        public string TargetArea { get; set; }
        public float Distance { get; set; } // in km?
    }
}
