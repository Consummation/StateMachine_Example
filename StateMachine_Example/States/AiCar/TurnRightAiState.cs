using HellWheels.Car;
using HellWheels.Race;
using UnityEngine;

namespace States.AiCar
{
    public class TurnRightAiState : MoveAheadAiState
    {
        readonly Vector3 INPUT_VECTOR = new Vector3(1f, 1f);

        public TurnRightAiState(ICar car, CarSensors _backwardSensor, ICarsContainer carsContainer)
            : base(car, _backwardSensor, carsContainer)
        {
        }

        protected override Vector3 GetInputVectorInternal()
        {
            return INPUT_VECTOR;
        }
    }
}
