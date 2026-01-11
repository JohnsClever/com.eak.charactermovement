using Mono.Cecil;
using UnityEngine;

namespace com.eak.charactermovement
{
    public interface IMoveModifier
    {
        Vector3 Modify(Vector3 position);
    }
}