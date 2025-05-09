using UnityEngine;

namespace Kurao
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(PlayerController player, PlayerStateMachine stateMachine, InputHandler inputHandler, CharacterData characterData) : base(player, stateMachine, inputHandler, characterData)
        {
        }
    }
}
