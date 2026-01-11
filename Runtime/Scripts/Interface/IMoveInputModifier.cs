using UnityEngine;

namespace com.eak.charactermovement
{
    public interface IMoveInputModifier
    {
        Vector3 ModifyInput(Vector3 input);
    }
}