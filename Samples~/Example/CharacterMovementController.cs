using UnityEngine;
using com.eak.charactermovement;
namespace com.eak
{
    public class CharacterMovementController : MonoBehaviour
    {
        [SerializeField] Camera mainCamera;
        [SerializeField] MovementMotor movementMotor;
        MoveDirectionByCamera moveDirectionByCamera;
        MoveTeleport teleportModule = new MoveTeleport();
        RotateTowardMovement rotateTowardMovement;
        Jump jumpModule;
        void Start()
        {
            moveDirectionByCamera = new MoveDirectionByCamera(mainCamera.transform, 5f);
            rotateTowardMovement = new RotateTowardMovement(mainCamera.transform, 10f);
            jumpModule = new Jump(movementMotor.GravityModule);
            
            movementMotor.AddMovementType(moveDirectionByCamera);
            movementMotor.AddRotatationType(rotateTowardMovement);
            movementMotor.AddMoveModifier(jumpModule);
        }
        void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            MovementHandler(new Vector3(x, 0, z));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (movementMotor.GravityModule.IsGrounded(out var groundPoint))
                    jumpModule.DoJump();
                // TeleportToPosition(new Vector3(0, 1, 0));
            }
        }
        public void MovementHandler(Vector3 input)
        {
            movementMotor.Move(input);
            movementMotor.Rotate(input);
        }
        public void TeleportToPosition(Vector3 targetPosition)
        {
            movementMotor.MoveWith(teleportModule, targetPosition);
        }
    }
}