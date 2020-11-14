using UnityEngine;

namespace Spells {
    public abstract class Spell : MonoBehaviour {
        public abstract void Reset();
        public abstract void KeyPressed( Transform transform );
        public abstract void KeyReleased( Transform transform );
    }

}