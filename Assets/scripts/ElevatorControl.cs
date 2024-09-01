using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ElevatorControl 
{
    private ElevatorShaft controlShaft;
    private int controlPanelMaxFloor;
    private int controlPanelMinFloor;
    private List<int> elevatorQueue = new List<int>();
    private List<int> temporaryElevatorQueue = new List<int>();
    private List<int> shabbatQueue = new List<int>();
    private int firstIndex = 0;

    // outputs
    private string requestedFloorIs = "Requested floor is: ";
    private string movingToNextFloor = "Moving to next floor: ";
    private string elevatorMovingUp = "Elevator moving up...";
    private string elevatorMovingDown = "Elevator moving down...";
    private string elevatorArrivedAt = "Elevator arrived at location! Floor ";
    private string doorsAreOpening = "Doors are opening!";
    private string peopleLeft = "The amount of people that are left in the elevator: ";
    private string doorsAreClosing = "Doors are closing!";
    private string elevatorIsShabbat = "Elevator now operates in Shabbat mode.";
    private string elevatorIsRegular = "Elevator now operates in regular mode.";
    private string peopleAreLeaving = "People are leaving...";
    private string peopleAreEntering = "People are entering...";

    public ElevatorControl(int elevatorShaftFloorRange, int whereElevatorIs, int elevatorCapacityInShaft)
    {
        controlShaft = new ElevatorShaft(elevatorShaftFloorRange, whereElevatorIs, elevatorCapacityInShaft);
        controlPanelMaxFloor = elevatorShaftFloorRange;
        controlPanelMinFloor = 0;
    }

    private void sortQueue()
    {
        int shortestStep = controlPanelMaxFloor;
        int insertQueueFloor=0;
        int step;

        while (elevatorQueue.Count > 0)
        {
            shortestStep = controlPanelMaxFloor;
            // checks smallest step from current floor
            foreach (int queuedFloor in elevatorQueue)
            {
                step = controlShaft.shaftElevator.getCurrentFloor() - queuedFloor;

                if (step < controlPanelMinFloor)
                {
                    // ensures that the step is a positive natural number
                    step *= -1;
                }

                if (step < shortestStep)
                {
                    insertQueueFloor = queuedFloor;
                }
            }

            temporaryElevatorQueue.Add(insertQueueFloor);
            elevatorQueue.Remove(insertQueueFloor);
        }

        foreach(int floor in temporaryElevatorQueue)
        {
            elevatorQueue.Add(floor);
        }
    }

    private void addShabbatFloor()
    {
        for(int floor=controlPanelMinFloor; floor<shabbatQueue.Count; floor++)
        {
            if (!shabbatQueue.Contains(floor))
            {
                shabbatQueue.Add(floor);
            }
        }
    }
    public void requestElevator(int calledFloor)
    {
        // adds floor to the queue
        Debug.Log(requestedFloorIs + calledFloor.ToString());

        if (!controlShaft.shaftElevator.isElevatorShabbat())
        {
            elevatorQueue.Add(calledFloor);
            // SORT
            sortQueue();
        }
        else
        {
            addShabbatFloor();
        }
        


    }

    public void moveToNextLocation()
    {
        int nextLocation=0;
        if (!controlShaft.shaftElevator.isElevatorShabbat())
        {
            nextLocation = elevatorQueue[firstIndex];
            elevatorQueue.RemoveAt(firstIndex);
        }
        else
        {
            nextLocation = shabbatQueue[firstIndex];
            shabbatQueue.RemoveAt(firstIndex);
        }
  

        Debug.Log(movingToNextFloor + nextLocation.ToString());

        if(nextLocation< controlShaft.shaftElevator.getCurrentFloor())
        {
            Debug.Log(elevatorMovingDown);
            controlShaft.moveElevatorDownInShaft(nextLocation);
        }
        else
        {
            Debug.Log(elevatorMovingUp);
            controlShaft.moveElevatorUpInShaft(nextLocation);
        }

        Debug.Log(elevatorArrivedAt + nextLocation);
    }

    public void arriveAtFloor(int peopleLeaving, int peopleEntering)
    {


        controlShaft.shaftElevator.openDoors();
        Debug.Log(doorsAreOpening);
        Debug.Log(peopleAreLeaving);
        controlShaft.shaftElevator.removePeople(peopleLeaving);
        Debug.Log(peopleLeft + controlShaft.shaftElevator.getCurrentCapacity());
        Debug.Log(peopleAreEntering);
        controlShaft.shaftElevator.addPeople(peopleEntering);
        Debug.Log(peopleLeft + controlShaft.shaftElevator.getCurrentCapacity());
        Debug.Log(doorsAreClosing);

    }

    public void configureShabbatElevator()
    {
        controlShaft.shaftElevator.initiateShabbatElevator();
        Debug.Log(elevatorIsShabbat);
        shabbatQueue = Enumerable.Range(controlPanelMinFloor, controlPanelMaxFloor).ToList();
    }

    public void stopShabbatElevator()
    {
        controlShaft.shaftElevator.initiateRegularElevator();
        Debug.Log(elevatorIsRegular);
    }
}
