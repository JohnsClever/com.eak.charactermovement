using UnityEngine;

namespace com.eak.charactermovement
{
    public class Jump : IMoveModifier
    {
        MoveByGravity gravityModule;
        public float jumpForce = 10;
        public float jumpVelocity = 0;
        public float jumpDownRate = 10;

        public Jump(MoveByGravity gravityModule = null)
        {
            this.gravityModule = gravityModule;
        }
        public void DoJump()
        {
            jumpVelocity = jumpForce;
            gravityModule?.ResetGravity();
        }
        public Vector3 Modify(Vector3 position)
        {
            if (jumpVelocity > 0)
            {
                position.y += jumpVelocity * Time.deltaTime;
                jumpVelocity -= jumpDownRate * Time.deltaTime;
            }
            return position;
        }

    }
}