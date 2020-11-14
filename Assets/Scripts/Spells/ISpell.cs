using UnityEngine;

namespace Spells
{
    public interface ISpell
    {
        void Reset();
        void KeyPressed( Transform transform );
        void KeyReleased( Transform transform );
    }

}


