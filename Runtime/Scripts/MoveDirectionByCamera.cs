using UnityEngine;

namespace com.eak.charactermovement
{
    public class MoveDirectionByCamera : IMoveable, IMoveInputModifier
    {
        private Transform cameraTransform;
        private float speed = 2;
        public MoveDirectionByCamera(Transform cameraTransform, float speed = 2)
        {
            this.cameraTransform = cameraTransform;
            this.speed = speed;
        }
        public MoveDirectionByCamera(Camera camera, float speed = 2)
        {
            cameraTransform = camera.transform;
        }
        public Vector3 Move(Vector3 position, Vector3 playerInput)
        {
            Vector3 dir = ModifyInput(playerInput);
            return position + Time.deltaTime * speed * dir;
        }
        public Vector3 ModifyInput(Vector3 input)
        {
            if (cameraTransform == null)
            {
                return input;
            }

            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            Vector3 desiredMoveDirection = forward * input.z + right * input.x;
            return desiredMoveDirection;
        }
    }
}