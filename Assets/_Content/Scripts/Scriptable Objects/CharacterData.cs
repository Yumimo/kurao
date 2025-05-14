using UnityEngine;

namespace Kurao
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        [Header("Movement Settings")]
        [Tooltip("Maximum movement speed of the character.")]
        public float movementSpeed = 5f;

        [Tooltip("How fast the character accelerates and decelerates.")]
        public float speedChangeRate = 10f;

        [Range(0f, 1f)]
        [Tooltip("Speed of rotation relative to input. 1 = instant rotation.")]
        public float rotationSpeed = 0.5f;

        [Header("Gravity Settings")]
        [Tooltip("Gravity force applied to the character.")]
        public float gravity = -9.81f;

        [Tooltip("Maximum falling speed.")]
        public float terminalVelocity = 53.0f;

        [Header("Dash Settings")] 
        public float dashDelay;
        public float dashLength;
        
        [Header("Battle Settings")] 
        
        [Tooltip("Health of the character.")]
        public float naxHealth = 100f;
        [Tooltip("Stamina of the character.")]
        public float maxStamina = 50;
        [Tooltip("Attack damage of the character.")]
        public float baseDamage = 0.3f;
        [Tooltip("Attack speed of the character.")]
        public float baseAttackSpeed = 1f;
        [Tooltip("Defence of the character.")]
        public float baseDefence = 1f;

        
        public float ComputeDamageTaken(float incomingDamage)
        {
            return incomingDamage / (1f + baseDefence);
        }
        
        float ComputeDamageToEnemy(float baseAttackDamage, float enemyDefense)
        {
            return baseAttackDamage / (1f + enemyDefense);
        }
    }
}
