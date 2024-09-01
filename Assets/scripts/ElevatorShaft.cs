using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorShaft 
{
    private bool[] shaftRange;
    public Elevator shaftElevator;
    private int baseShaftFloor = 0;
    private string requestOutOfBoundsError = "Requested floor is out of bounds.";

    public ElevatorShaft(int elevatorShaftFloorRange, int whereElevatorIs, int elevatorCapacityInShaft)
    {
        // creates the shaft with the respective floor range
        shaftRange = new bool[elevatorShaftFloorRange];
        shaftElevator = new Elevator(elevatorCapacityInShaft);

        // initializes where the elevator is currently     
        for(int index=0; index<shaftRange.Length; index++)
        {
            if (index == whereElevatorIs)
            {
                shaftRange[index] = true;
                shaftElevator.setCurrentFloor(index);
            }
            else
            {
                shaftRange[index] = false;
            }
        }
    }

    public bool isRequestWithinRange(int floors)
    {
        // checks that the requested floor pressed is within the shaft's range
        if(shaftElevator.getCurrentFloor()+floors <= shaftRange.Length && shaftElevator.getCurrentFloor() + floors >= baseShaftFloor)
        {
            return true;
        }

        return false;
    }
    public void moveElevatorDownInShaft(int floors)
    {
        if (isRequestWithinRange(-floors))
        {
            // elevator is leaving the current floor
            shaftRange[shaftElevator.getCurrentFloor()] = false;
            //elevator is moving
            shaftElevator.moveElevatorDown(floors);
            //elevator arriving at new floor
            shaftRange[shaftElevator.getCurrentFloor()] = true;
        }

        else
        {
            Debug.Log(requestOutOfBoundsError);
        }

    }

    public void moveElevatorUpInShaft(int floors)
    {
        if (isRequestWithinRange(floors))
        {
            // elevator is leaving the current floor
            shaftRange[shaftElevator.getCurrentFloor()] = false;
            //elevator is moving
            shaftElevator.moveElevatorUp(floors);
            //elevator arriving at new floor
            shaftRange[shaftElevator.getCurrentFloor()] = true;
        }

        else
        {
            Debug.Log(requestOutOfBoundsError);
        }

    }


}
