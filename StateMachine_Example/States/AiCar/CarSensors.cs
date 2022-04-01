using HellWheels.Car;
using UnityEngine;

public class CarSensors : MonoBehaviour
{
    [Range(0.1f, 2.0f)]
    public float length = 0.1f;

    public bool IsSensorHit(int layerMask)
    {
        if (Physics.Raycast(transform.position, transform.forward, length, layerMask))
            return true;
        else
            return false;
    }

    public ICar IsSensorHitRacer()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, length, 1 << 12))
        {
            var racerTag = hit.collider.GetComponent<RacerTag>();
            if (racerTag != null && racerTag.Racer != null)
            {
                return racerTag.Racer;
            }
        }

        return null;
    }
}
