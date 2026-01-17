using com.eak.charactermovement;
using UnityEngine;

namespace com.eak.charactermovement
{
    public class MoveByGravity : IMoveModifier
    {
        public bool applyGravity = true;
        public float radius = 0.4f;
        float gravityVelocity = 0;
        Transform root;
        LayerMask layerMask;
        public float floorOffset = 0.3f;
        public MoveByGravity(Transform entityRoot, LayerMask groundLayer)
        {
            root = entityRoot;
            layerMask = groundLayer;
        }

        public bool IsGrounded(out Vector3 point)
        {
            if (Physics.BoxCast(
                  root.position,
                  new Vector3(radius, 0.05f, radius),
                  Vector3.down,
                  out var hit,
                  root.rotation,
                  floorOffset,
                  layerMask,
                  QueryTriggerInteraction.Ignore
              ))
            {
                point = hit.point;
                return true;
            }
            else
            {
                point = Vector3.zero;
                return false;
            }
        }
        public Vector3 Modify(Vector3 position)
        {
            if (IsGrounded(out var groundPoint))
            {
                float groundY = groundPoint.y + floorOffset;
                ResetGravity();
                position.y = groundY;
            }
            else if (applyGravity)
            {
                gravityVelocity -= 9.81f * Time.deltaTime;
                position.y += gravityVelocity * Time.deltaTime;
            }
            return position;
        }
        public void ResetGravity()
        {
            gravityVelocity = 0;
        }
    }

}