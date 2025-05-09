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
        
    }
}
