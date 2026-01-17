using UnityEngine;
namespace com.eak.charactermovement
{
    public class MoveLockZPosition : IMoveModifier
    {
        float zPosition = 0;
        public Vector3 Modify(Vector3 position)
        {
            position.z = zPosition;
            return position;
        }
    }
}