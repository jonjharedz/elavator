using System.Collections.Generic;

namespace ElevatorSys
{
  public class ElevatorSystem
  {
    public static Elevator[] Elevators;
    public List<Passenger> passengers= new List<Passenger>();

    public int NumberOfFloors;

     public ElevatorSystem(int numberOfElevators, int numberOfFloors)
     {
       Elevators = new Elevator[numberOfElevators];
       this.NumberOfFloors = numberOfFloors;
       initElevators();
     }

    private void initElevators()
    {
      for(var i = 0; i < Elevators.Length; i++)
      {
        Elevators[i] = new Elevator(NumberOfFloors, i);
      }
    }

    public void OperateElevators()
    {
      foreach(var e in Elevators)
      {
        e.intervalMs = 1000; //change the interval 
        e.Start();
      }
    }

    public void StopElevators()
    {

    }

    public void AddPassenger(Passenger passenger)
    {
      Elevators[passenger.ElavatorNo].AddPassenger(passenger);
    }

  }

 
}
