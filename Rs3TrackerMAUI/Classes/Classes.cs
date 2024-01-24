﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rs3TrackerMAUI.Classes {
    public class DisplayClasses {
        public class Keypressed : KeybindClass {
            public double timepressed { get; set; }
        }


        public class BarKeybindClass : BarClass {
            public string modifier { get; set; }
            public string key { get; set; }
            public BarClass bar { get; set; }
        }

        public class BarClass {
            public string name { get; set; }
        }

        public class Ability {
            public string name { get; set; }           
            public string img { get; set; }
        }

        public class Rotation
        {
            public string name { get; set; }
            public List<Ability> abilities { get; set; }
        }

        public class KeybindClass {
            public string modifier { get; set; }
            public string key { get; set; }
            public BarClass bar { get; set; }
            public Ability ability { get; set; }
        }
        public class ResquestInput {
            public int type { get; set; }
            public long time { get; set; }
            public bool altKey { get; set; }
            public bool ctrlKey { get; set; }
            public bool metaKey { get; set; }
            public bool shiftKey { get; set; }
            public int keycode { get; set; }
        }
    }
}