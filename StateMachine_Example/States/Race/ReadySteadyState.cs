using HellWheels.Car;
using HellWheels.UI;
using StateMachineRealization;
using System.Collections;
using UnityEngine;

namespace States.Race
{
    public class ReadySteadyState : State
    {
        private readonly MonoBehaviour _countdownCoroutineMono;
        private readonly IRacingCamera _racingCamera;
        private readonly ICar _playerCar;
        private float _countdownTime;
        private readonly float _countdownDuration;
        private readonly IRacingUI _racingUI;

        public ReadySteadyState(MonoBehaviour countdownCoroutineMono, float countdownDuration, IRacingCamera racingCamera, ICar playerCar,
            IRacingUI racingUi)
        {
            _countdownCoroutineMono = countdownCoroutineMono;
            _countdownDuration = countdownDuration;
            _racingCamera = racingCamera;
            _playerCar = playerCar;
            _racingUI = racingUi;
        }

        public override void OnStateEnter()
        {
            _racingUI.Show();
            _countdownCoroutineMono.StartCoroutine(Countdown());
            _racingCamera.SetTargetCar(_playerCar);
            _racingCamera.LookUp = true;
        }

        public override void OnStateExit()
        {
            _racingCamera.LookUp = false;
        }

        public bool IsCountdownFinished() => _countdownTime <= 0;

        public int GetCurrentCountdownTime() => (int)_countdownTime;

        private IEnumerator Countdown()
        {
            _countdownTime = _countdownDuration;

            while (_countdownTime > 0)
            {
                _countdownTime -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}