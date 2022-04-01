using StateMachineRealization;
using UnityEngine;

namespace States.AiCar
{
    public class UTurnState : State, IAiState
    {
        private Vector3 _inputVector;

        private readonly CarSensors _leftSensor;
        private readonly CarSensors _rightSensor;
        private readonly CarSensors _forwardSensor;
        private LayerMask _barrierMask;
        private readonly float _backwardsLengthMultiplier;

        private readonly float initialLeftSensorLength;
        private readonly float initialRightSensorLength;
        private readonly float initialForwardSensorLength;

        public UTurnState(CarSensors leftSensor, CarSensors rightSensor, CarSensors forwardSensor, LayerMask barrierMask, float backwardsLengthMultiplier)
        {
            _leftSensor = leftSensor;
            _rightSensor = rightSensor;
            _forwardSensor = forwardSensor;
            _barrierMask = barrierMask;
            _backwardsLengthMultiplier = backwardsLengthMultiplier;
            initialLeftSensorLength = _leftSensor.length;
            initialRightSensorLength = _rightSensor.length;
            initialForwardSensorLength = _forwardSensor.length;
        }

        public override void OnStateExit()
        {
            _leftSensor.length = initialLeftSensorLength;
            _rightSensor.length = initialRightSensorLength;
            _forwardSensor.length = initialForwardSensorLength;
        }

        public override void Tick(float deltaTime)
        {
            if (_leftSensor.IsSensorHit(_barrierMask) || _forwardSensor.IsSensorHit(_barrierMask) || _rightSensor.IsSensorHit(_barrierMask))
            {
                _inputVector.y = -1f;
                _inputVector.x = 1;
                _leftSensor.length = initialLeftSensorLength * _backwardsLengthMultiplier;
                _rightSensor.length = initialRightSensorLength * _backwardsLengthMultiplier;
                _forwardSensor.length = initialForwardSensorLength * _backwardsLengthMultiplier;
            }
            else
            {
                _inputVector.y = 1f;
                _inputVector.x = -1;
                _leftSensor.length = initialLeftSensorLength;
                _rightSensor.length = initialRightSensorLength;
                _forwardSensor.length = initialForwardSensorLength;
            }
        }

        public Vector3 GetInputVector() => _inputVector;
        public bool GetUsingBoost() => false;
    }
}
