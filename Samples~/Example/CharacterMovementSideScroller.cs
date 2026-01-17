using UnityEngine;
namespace com.eak.charactermovement
{
    public class CharacterMovementSideScroller : CharacterMovementController
    {
        protected override void Start()
        {
            base.Start();
            movementMotor.AddMoveModifier(new MoveLockZPosition());
        }
    }
}