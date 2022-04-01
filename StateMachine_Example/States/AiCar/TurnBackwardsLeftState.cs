using StateMachineRealization;
using UnityEngine;

namespace States.AiCar
{
    public class TurnBackwardsLeftState : State, IAiState
    {
        private Vector3 _inputVector;

        private readonly CarSensors left;
        private readonly CarSensors right;
        private readonly CarSensors forward;
        private readonly float backwardsLengthMultiplier;

        public TurnBackwardsLeftState(CarSensors left, CarSensors right, CarSensors forward, float backwardsLengthMultiplier)
        {
            this.left = left;
            this.right = right;
            this.forward = forward;
            this.backwardsLengthMultiplier = backwardsLengthMultiplier;
        }

        public override void OnStateEnter()
        {
            left.length *= backwardsLengthMultiplier;
            right.length *= backwardsLengthMultiplier;
            forward.length *= backwardsLengthMultiplier;
        }

        public override void OnStateExit()
        {
            left.length /= backwardsLengthMultiplier;
            right.length /= backwardsLengthMultiplier;
            forward.length /= backwardsLengthMultiplier;
        }

        public override void Tick(float deltaTime)
        {
            _inputVector.y = -1f;
            _inputVector.x = -1f;
        }

        public Vector3 GetInputVector() => _inputVector;
        public bool GetUsingBoost() => false;
    }
}
