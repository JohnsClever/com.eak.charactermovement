using UnityEngine;
using com.eak.charactermovement;
namespace com.eak
{
    public class CharacterMovementController : MonoBehaviour
    {
        [SerializeField] protected Camera mainCamera;
        [SerializeField] protected MovementMotor movementMotor;
        protected MoveDirectionByCamera moveDirectionByCamera;
        protected MoveTeleport teleportModule = new MoveTeleport();
        protected RotateTowardMovement rotateTowardMovement;
        protected Jump jumpModule;
        protected MoveByForce forceModule;
        protected virtual void Start()
        {
            // init varaibles
            if (mainCamera == null)
                mainCamera = Camera.main;
            moveDirectionByCamera = new MoveDirectionByCamera(mainCamera.transform, 5f);
            rotateTowardMovement = new RotateTowardMovement(mainCamera.transform, 10f);
            jumpModule = new Jump(movementMotor.GravityModule);
            forceModule = new MoveByForce();

            // add movement components to motor
            movementMotor.AddMovementType(moveDirectionByCamera);
            movementMotor.AddRotatationType(rotateTowardMovement);
            movementMotor.AddMoveModifier(jumpModule);
            movementMotor.AddMoveModifier(forceModule);
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
        public void AddForce(Vector3 force, float duration = 0.1f)
        {
            forceModule.AddForce(force, duration);
        }
    }
}