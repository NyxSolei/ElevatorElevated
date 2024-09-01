using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator
{
    private int elevatorCapacity;
    private bool isDoorClosed = true;
    private int currentFloor;
    private bool isShabbat = false;
    private int currentCapacity;
    private string tooManyError = "Amount of people attempting to get in is above capacity. Cannot preform action.";
    private string tooManyOffError = "Amount of people attempting to get off is below expected. Cannot preform action";
    private int minCapacity = 0;
    public Elevator(int capacity)
    {
        elevatorCapacity = capacity;
        currentCapacity = 0;
        currentFloor = 0;
    }

    public void removePeople(int amountOfPeople)
    {
        if(currentCapacity - amountOfPeople >= minCapacity)
        {
            currentCapacity -= amountOfPeople;
        }
        else
        {
            Debug.Log(tooManyOffError);
        }
    }
    public void addPeople(int amountOfPeople)
    {
        if (currentCapacity + amountOfPeople <= elevatorCapacity)
        {
            currentCapacity += amountOfPeople;
        }
        else
        {
            Debug.Log(tooManyError);
        }
    }
    public int getCurrentCapacity()
    {
        return currentCapacity;
    }

    public int getCapacity()
    {
        // returns capacity of the elevator
        return elevatorCapacity;
    }

    public void openDoors()
    {
        // sets closedDoors to false
        isDoorClosed = false;
    }

    public void closeDoors()
    {
        // sets closedDoors to true
        isDoorClosed = true;
    }

    public bool getDoorsState()
    {
        return isDoorClosed;
    }

    public void moveElevatorDown(int floors)
    {
        // sets the currentFloor minus the floors that it needs to move down
        if (!getDoorsState())
        {
            closeDoors();
        }

        currentFloor -= floors;

        openDoors();
    }

    public void moveElevatorUp(int floors)
    {
        // sets the currentFloor plus the floors that it needs to move up
        if (!getDoorsState())
        {
            closeDoors();
        }
        currentFloor += floors;

        openDoors();
    }

    public int getCurrentFloor()
    {
        // returns the currentFloor
        return currentFloor;
    }

    public void initiateShabbatElevator()
    {
        // sets isShabbat to true
        isShabbat = true;
    }

    public void initiateRegularElevator()
    {
        isShabbat = false;
    }
    public bool isElevatorShabbat()
    {
        // returns the state of the elevator - shabbat true or false
        return isShabbat;
    }

    public void setCurrentFloor(int newCurrentFloor)
    {
        currentFloor = newCurrentFloor;
    }
}
