using HellWheels.Boosts;
using HellWheels.Car;
using HellWheels.Car.Components;
using HellWheels.Race;
using StateMachineRealization;
using UnityEngine;

namespace States.AiCar
{
    public abstract class MoveAheadAiState : State, IAiState
    {
        private const float SAW_USE_DISTANCE = 2.0f;
        private readonly ICar _car;
        private readonly IBoostContainer _boostContainer;
        private readonly CarSensors backwardSensor;
        private readonly ICarsContainer _carsContainer;

        public MoveAheadAiState(ICar car, CarSensors _backwardSensor, ICarsContainer carsContainer)
        {
            _carsContainer = carsContainer;
            _car = car;
            _boostContainer = _car.BoostContainer;
            backwardSensor = _backwardSensor;
        }

        public bool GetUsingBoost()
        {
            if (!_car.IsAlive)
            {
                return false;
            }

            if (_boostContainer.CurrentBoostType == BoostType.SpeedUp)
            {
                return true;
            }
            else if (_boostContainer.CurrentBoostType == BoostType.Rocket)
            {
                if (Physics.Raycast(_car.RocketRaycastTransform.position, _car.RocketRaycastTransform.forward, out RaycastHit hit, 100.0f))
                {
                    var racerTag = hit.collider.GetComponent<RacerTag>();
                    if (racerTag != null && racerTag.Racer != null && racerTag.Racer != _car)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (_boostContainer.CurrentBoostType == BoostType.Oil)
            {
                return backwardSensor.IsSensorHit(1 << 9);
            }
            else if (_boostContainer.CurrentBoostType == BoostType.Saw)
            {
                return ShouldUseSaw();
            }
            else
            {
                return false;
            }
        }

        private bool ShouldUseSaw()
        {
            return Vector3.Distance(_car.Transform.position, GetClosestRacer().Transform.position) < SAW_USE_DISTANCE;
        }

        private ICar GetClosestRacer()
        {
            float minDist = float.PositiveInfinity;
            ICar closestCar = null;
            foreach (ICar car in _carsContainer.GetCars())
            {
                float dist = Vector3.Distance(_car.Transform.position, car.Transform.position);
                if (dist < minDist)
                {
                    closestCar = car;
                    minDist = dist;
                }
            }
            return closestCar;
        }


        public Vector3 GetInputVector() => GetInputVectorInternal();

        protected abstract Vector3 GetInputVectorInternal();
    }
}
