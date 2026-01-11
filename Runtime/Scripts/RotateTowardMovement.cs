using System.Collections.Generic;
using UnityEngine;

namespace com.eak.charactermovement
{
    public class RotateTowardMovement : IRotatable, IMoveInputModifier
    {
        public float rotationSpeed = 10f;
        Transform camera;
        public RotateTowardMovement(Transform camera, float rotationSpeed)
        {
            this.camera = camera;
            this.rotationSpeed = rotationSpeed;
        }

        public Vector3 ModifyInput(Vector3 input)
        {
            if (camera == null)
            {
                return input;
            }

            Vector3 forward = camera.forward;
            Vector3 right = camera.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            Vector3 desiredMoveDirection = forward * input.z + right * input.x;
            return desiredMoveDirection;
        }

        public Quaternion Rotate(Quaternion currentRotation, Vector3 moveDirection)
        {
            if (moveDirection == Vector3.zero)
            {
                return currentRotation;
            }
            moveDirection = ModifyInput(moveDirection);
            var targetRot = Quaternion.LookRotation(moveDirection);
            return Quaternion.Slerp(currentRotation, targetRot, Time.deltaTime * rotationSpeed);
        }
    }
}