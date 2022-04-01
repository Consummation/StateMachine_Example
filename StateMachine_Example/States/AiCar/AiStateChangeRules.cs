using UnityEngine;

namespace States.AiCar
{
    public class AiStateChangeRules
    {
        private readonly Transform _rootTransform;

        public AiStateChangeRules(Transform rootTransform)
        {
            _rootTransform = rootTransform;
        }

        public bool IsChangeToMoveForward(Vector3 waypointPosition) => IsLookingAt(waypointPosition);

        public bool IsChangeToTurnLeft(Vector3 waypointPosition)
        {
            if (IsLookingAt(waypointPosition) == false)
            {
                if (GetRotationYTo(waypointPosition) < 0)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        public bool IsChangeToTurnRight(Vector3 waypointPosition)
        {
            if (IsLookingAt(waypointPosition) == false)
            {
                if (GetRotationYTo(waypointPosition) > 0)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        public bool IsChangeToUTurn(RoadDirectionDetection directionDetection)
        {
            if (!directionDetection.IsDirectionValid(_rootTransform, _rootTransform.forward))
                return true;
            else
                return false;
        }

        public bool ShouldExitFromUTurn(RoadDirectionDetection directionDetection)
        {
            if (directionDetection.IsDirectionValid(_rootTransform, _rootTransform.forward))
                return true;
            else
                return false;
        }

        private bool IsLookingAt(Vector3 waypointPosition)
        {
            var dirFromObjects = (waypointPosition - _rootTransform.position).normalized;

            var dotProd = Vector3.Dot(dirFromObjects, _rootTransform.forward);
            return dotProd > 0.98f;
        }

        private float GetRotationYTo(Vector3 waypointPosition)
        {
            var dirFromObjects = (waypointPosition - _rootTransform.position).normalized;
            return Vector3.Cross(_rootTransform.forward, dirFromObjects).y;
        }
    }
}