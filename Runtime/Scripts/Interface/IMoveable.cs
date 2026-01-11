using UnityEngine;

namespace com.eak.charactermovement
{
    public interface IMoveable
    {
        Vector3 Move(Vector3 position, Vector3 input);
    }
}