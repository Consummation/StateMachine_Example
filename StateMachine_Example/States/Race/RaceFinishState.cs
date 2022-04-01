using HellWheels.Car;
using StateMachineRealization;
using UnityEngine;

namespace States.Race
{
    public class RaceFinishState : State
    {
        private readonly ICar _playerCar;

        public RaceFinishState(ICar playerCar)
        {
            _playerCar = playerCar;
        }

        public override void OnStateEnter()
        {
            Debug.Log("Player " + _playerCar.Name + " finished the race!");

            _playerCar.AudioPlayer.PlaySound(AudioTypes.Finish);
        }
    }
}