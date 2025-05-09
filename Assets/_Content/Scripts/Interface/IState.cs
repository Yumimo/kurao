using UnityEngine;

namespace Kurao
{
    public interface IState
    {
        void Enter();
        void Exit();
        void FixedUpdate();
        void Update();
    }
}
