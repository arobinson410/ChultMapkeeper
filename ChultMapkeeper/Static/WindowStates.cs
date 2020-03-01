using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChultMapkeeper
{
    public enum InteractMode
    {
        RevealMode,
        MoveMapMode,
        MoveParty
    };
}

namespace ChultMapkeeper.Static
{
    public static class WindowStates
    {
        public static event EventHandler WindowStateChanged;

        private static InteractMode mapMode = InteractMode.MoveMapMode;

        public static InteractMode MapMode
        {
            set
            {
                mapMode = value;
                WindowStateChanged?.Invoke(value, new EventArgs());
            }
            get
            {
                return mapMode;
            }
        }
    }
}
