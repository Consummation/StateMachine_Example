﻿using HellWheels.Car;
using HellWheels.Race;
using UnityEngine;

namespace States.AiCar
{
    public class MoveForwardAiState : MoveAheadAiState
    {
        readonly Vector3 INPUT_VECTOR = new Vector3(0f, 1f);

        public MoveForwardAiState(ICar car, CarSensors _backwardSensor, ICarsContainer carsContainer)
            : base(car, _backwardSensor, carsContainer)
        {
        }

        protected override Vector3 GetInputVectorInternal()
        {
            return INPUT_VECTOR;
        }
    }
}
