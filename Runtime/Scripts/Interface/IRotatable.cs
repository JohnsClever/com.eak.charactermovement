using UnityEngine;

namespace com.eak.charactermovement
{
    public interface IRotatable
    {
        Quaternion Rotate(Quaternion currentRotation, Vector3 moveDirection);
    }
}