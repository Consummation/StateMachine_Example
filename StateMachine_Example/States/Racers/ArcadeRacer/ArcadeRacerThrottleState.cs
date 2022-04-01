using ArcadeCar;
using HellWheels.Car;
using ScriptableObjects;
using StateMachineRealization;
using UnityEngine;

namespace States.Racers.ArcadeRacer.CarControls
{
    public class ArcadeRacerThrottleState : State
    {
        private readonly ICar _car;
        private readonly Transform _rootTransform;
        private readonly Rigidbody _carRigidbody;
        private readonly CarSettings _settings;
        private readonly CarGroundCheck _groundCheck;

        private float _speedInput = 0;
        private const float ParametersMultiplier = 1000f;

        private int _prevDirection = 1;

        public ArcadeRacerThrottleState(ICar car, Transform rootTransform)
        {
            _car = car;
            _rootTransform = rootTransform;

            _carRigidbody = _car.CarRigidbody;
            _settings = _car.CarSettings;
            _groundCheck = _car.CarGroundCheck;
        }

        public override void Tick(float deltaTime)
        {
            bool isGrounded = _groundCheck.IsGrounded();
            if (isGrounded)
            {
                CalculateSpeed();
            }

            Rotate();

            _car.AudioPlayer.PlaySound(AudioTypes.CarEngine);
            UpdateEnginePitch(AudioTypes.CarEngine);
        }

        public override void OnStateExit()
        {
            _car.AudioPlayer.StopSound(AudioTypes.CarEngine);
        }

        private void UpdateEnginePitch(AudioTypes audioType)
        {
            var pitch = 1.0f + _carRigidbody.velocity.magnitude / 10.0f;
            _car.AudioPlayer.SetPitch(audioType, pitch);
        }

        public override void FixedTick(float deltaTime)
        {
            base.FixedTick(deltaTime);
            if (_groundCheck.IsGrounded())
            {
                Move(deltaTime);
            }
        }

        private void Rotate()
        {
            int direction = _prevDirection;

            if (_car.CarInput.InputVector.y > 0)
            {
                direction = 1;
                _prevDirection = direction;
            }
            else
            {
                if (_car.CarInput.InputVector.y < 0)
                {
                    direction = -1;
                    _prevDirection = direction;
                }
            }

            var newRotationY = _car.CarInput.InputVector.x * _settings.TurnStrength * _carRigidbody.velocity.magnitude * direction * Time.deltaTime;
            var newRotation = Quaternion.Euler(_rootTransform.rotation.eulerAngles + new Vector3(0, newRotationY, 0));
            _rootTransform.rotation = newRotation;
        }

        private void CalculateSpeed()
        {
            _speedInput = 0;
            if (_car.CarInput.InputVector.y > 0)
            {
                _speedInput = _car.CarInput.InputVector.y * _settings.ForwardAcceleration * ParametersMultiplier;
            }
            else if (_car.CarInput.InputVector.y < 0)
            {
                _speedInput = _car.CarInput.InputVector.y * _settings.ReverseAcceleration * ParametersMultiplier;
            }
        }

        private void Move(float deltaTime)
        {
            if (_carRigidbody.velocity.magnitude > _settings.CurrentStats.maxSpeed)
            {
                _carRigidbody.velocity = Vector3.ClampMagnitude(_carRigidbody.velocity, _settings.CurrentStats.maxSpeed);
            }
            if (Mathf.Abs(_car.CarInput.InputVector.y) > 0)
            {
                _carRigidbody.AddForce(_speedInput * deltaTime * _rootTransform.forward, ForceMode.Acceleration);
            }
            else
            {
                if (_carRigidbody.velocity.magnitude < 0.2f)
                {
                    _carRigidbody.velocity = Vector3.zero;
                    _carRigidbody.angularVelocity = Vector3.zero;
                }
            }

            if (Mathf.Abs(Vector3.Dot(_carRigidbody.velocity.normalized, _rootTransform.forward)) < 0.9f)
            {
                _carRigidbody.AddForce(-_carRigidbody.velocity * _settings.Brake);
            }
        }
    }
}