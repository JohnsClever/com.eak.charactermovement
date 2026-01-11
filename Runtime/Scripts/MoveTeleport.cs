using UnityEngine;

namespace com.eak.charactermovement
{
    public class MoveTeleport : IMoveable
    {
        public Vector3 Move(Vector3 position, Vector3 input)
        {
            return input;
        }
    }
}