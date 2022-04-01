using StateMachineRealization;
using UnityEngine;

namespace States.AiCar
{
    public interface IAiState : IState
    {
        Vector3 GetInputVector();
        bool GetUsingBoost();
    }
}