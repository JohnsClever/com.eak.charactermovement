using System.Collections.Generic;
using UnityEngine;

namespace com.eak.charactermovement
{
    public class MoveByForce : IMoveModifier
    {
        public List<ForceData> activeForces = new List<ForceData>();
        public float linearDamping = 0.95f; // Damping coefficient (0-1), closer to 1 = less damping
        
        public void AddForce(Vector3 force, float duration = 0.1f)
        {
            activeForces.Add(new ForceData(force, duration));
        }
        
        public Vector3 Modify(Vector3 position)
        {
            Vector3 totalForce = Vector3.zero;
            float deltaTime = Time.deltaTime;
            
            // Apply all active forces and update them
            for (int i = activeForces.Count - 1; i >= 0; i--)
            {
                ForceData forceData = activeForces[i];
                
                // Add current force to total
                totalForce += forceData.force;
                
                // Update force with damping and duration
                forceData.UpdateForce(deltaTime, linearDamping);
                
                // Remove expired forces
                if (forceData.IsExpired())
                {
                    activeForces.RemoveAt(i);
                }
            }
            
            return position + totalForce * deltaTime;
        }
        
        /// <summary>
        /// Calculate the force deduction due to linear damping
        /// </summary>
        /// <param name="currentForce">The current force vector</param>
        /// <param name="dampingCoefficient">Damping coefficient (0-1)</param>
        /// <param name="deltaTime">Time step</param>
        /// <returns>The reduced force after damping</returns>
        public static Vector3 CalculateLinearDamping(Vector3 currentForce, float dampingCoefficient, float deltaTime)
        {
            // Linear damping formula: F_new = F_old * damping^deltaTime
            float dampingFactor = Mathf.Pow(dampingCoefficient, deltaTime);
            return currentForce * dampingFactor;
        }
        
        /// <summary>
        /// Get the total force deduction amount from damping
        /// </summary>
        /// <param name="originalForce">Original force before damping</param>
        /// <param name="dampingCoefficient">Damping coefficient (0-1)</param>
        /// <param name="deltaTime">Time step</param>
        /// <returns>The amount of force removed by damping</returns>
        public static Vector3 GetForceDampingDeduction(Vector3 originalForce, float dampingCoefficient, float deltaTime)
        {
            Vector3 dampedForce = CalculateLinearDamping(originalForce, dampingCoefficient, deltaTime);
            return originalForce - dampedForce;
        }
    }
    
    public class ForceData
    {
        public Vector3 force;
        public float duration;
        public float remainingTime;
        
        public ForceData(Vector3 force, float duration)
        {
            this.force = force;
            this.duration = duration;
            this.remainingTime = duration;
        }
        
        public void UpdateForce(float deltaTime, float dampingCoefficient)
        {
            // Apply linear damping to reduce force over time
            force = MoveByForce.CalculateLinearDamping(force, dampingCoefficient, deltaTime);
            
            // Reduce remaining time
            remainingTime -= deltaTime;
        }
        
        public bool IsExpired()
        {
            return remainingTime <= 0 || force.magnitude < 0.001f;
        }
        
        public Vector3 CombineForce(Vector3 additionalForce)
        {
            return force + additionalForce;
        }
        
        /// <summary>
        /// Get the percentage of force lost due to damping since creation
        /// </summary>
        /// <param name="originalMagnitude">The original force magnitude</param>
        /// <returns>Percentage of force lost (0-1)</returns>
        public float GetDampingLossPercentage(float originalMagnitude)
        {
            if (originalMagnitude == 0) return 0;
            return 1f - (force.magnitude / originalMagnitude);
        }
    }
}