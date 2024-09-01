using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorStartUpdate : MonoBehaviour
{
    ElevatorControl operationalElevator = new ElevatorControl(10, 0, 22);
    // Start is called before the first frame update
    void Start()
    {
        operationalElevator.requestElevator(9);
        operationalElevator.moveToNextLocation();
        operationalElevator.arriveAtFloor(0, 10);

        operationalElevator.requestElevator(2);
        operationalElevator.requestElevator(8);
        operationalElevator.requestElevator(3);
        operationalElevator.moveToNextLocation();
    }


}
