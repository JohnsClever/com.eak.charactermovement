using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace com.eak.charactermovement
{
    public class MovementMotor : MonoBehaviour
    {
        List<IMoveable> moveableComponents = new();
        List<IRotatable> rotatableComponents = new();
        List<IMoveModifier> moveModifiers = new();
        [SerializeField] private LayerMask floorMask;
        [SerializeField] private CharacterController characterController;
        public MoveByGravity GravityModule { get; private set; }
        void Start()
        {
            GravityModule = new MoveByGravity(transform, floorMask);
            AddMoveModifier(GravityModule);
        }
        void Update()
        {
            ApplyModifiers();
        }
        public void AddMoveModifier(IMoveModifier modifier)
        {
            moveModifiers.Add(modifier);
        }
        public void AddRotatationType(IRotatable rotatableComponent)
        {
            rotatableComponents.Add(rotatableComponent);
        }
        public void AddMovementType(IMoveable moveType)
        {
            moveableComponents.Add(moveType);
        }
        public void Move(Vector3 input)
        {
            foreach (var moveableComponent in moveableComponents)
                MoveWith(moveableComponent, input);
        }
        public void Rotate(Vector3 input)
        {
            foreach (var rotatableComponent in rotatableComponents)
                RotateWith(rotatableComponent, input);
        }
        public void ApplyGravity()
        {
            var moveModifier = moveModifiers.FirstOrDefault(m => m is MoveByGravity);
            if (moveModifier is MoveByGravity gravityModifier)
            {
                gravityModifier.applyGravity = true;
            }
        }
        public void RemoveGravity()
        {
            var moveModifier = moveModifiers.FirstOrDefault(m => m is MoveByGravity);
            if (moveModifier is MoveByGravity gravityModifier)
            {
                gravityModifier.applyGravity = false;
            }
        }
        public void MoveWith(IMoveable moveableComponent, Vector3 input)
        {
            Vector3 result = moveableComponent.Move(transform.position, input);
            if (characterController)
                characterController.Move(result - transform.position);
            else
                transform.position = result;
        }
        public void RotateWith(IRotatable rotatableComponent, Vector3 input)
        {
            transform.rotation = rotatableComponent.Rotate(transform.rotation, input);
        }
        private void ApplyModifiers()
        {
            foreach (var modifier in moveModifiers)
            {
                if (characterController)
                    characterController.Move(modifier.Modify(transform.position) - transform.position);
                else
                    transform.position = modifier.Modify(transform.position);
            }
        }
    }
}