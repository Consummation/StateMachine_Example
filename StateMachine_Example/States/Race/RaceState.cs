using HellWheels.Car;
using StateMachineRealization;
using System.Collections.Generic;

namespace States.Race
{
    public class RaceState : State
    {
        private readonly List<ICar> _cars;
        private readonly List<Waypoint> _waypoints;

        public RaceState(List<ICar> cars, List<Waypoint> waypoints)
        {
            _cars = cars;
            _waypoints = waypoints;
        }

        public override void Tick(float deltaTime)
        {
            HandleRacersWaypointReaching();
        }

        private void HandleRacersWaypointReaching()
        {
            var carsInfoEnumerator = _cars.GetEnumerator();
            while (carsInfoEnumerator.MoveNext())
            {
                var car = carsInfoEnumerator.Current;
                var info = car.LapInfo;

                var racerPosition = car.Transform.position;

                var nextWaypointIndex = info.CurrentWaypointIndex + 1;
                if (nextWaypointIndex == _waypoints.Count)
                    nextWaypointIndex = 0;

                var pos = racerPosition;
                var left = _waypoints[nextWaypointIndex].leftPositionWithOffset;
                var right = _waypoints[nextWaypointIndex].rightPositionWithOffset;

                var d = (pos.x - left.x) * (right.z - left.z) - (pos.z - left.z) * (right.x - left.x);
                if (d > 0)
                {
                    if (info.CurrentWaypointIndex + 1 < _waypoints.Count)
                    {
                        info.CurrentWaypointIndex++;
                    }
                }
            }

            carsInfoEnumerator.Dispose();
        }

        public void NotifyOnCrossFinishLine(ICar car, int lapsCountToWin)
        {
            var info = car.LapInfo;

            if (info.CurrentWaypointIndex + 1 >= _waypoints.Count && info.Lap < lapsCountToWin)
            {
                info.CurrentWaypointIndex = 0;
                info.Lap++;
            }
        }
    }
}