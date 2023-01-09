using System;
namespace ElevatorSys
{
  internal class Program
  {
    static void Main(string[] args)
    {
      // ElevatorSystem elevatorSystem = new ElevatorSystem(4, 4);
      //elevatorSystem.OperateElevators();

      //// add passengers 
      //var passenger1 = new Passenger
      //{
      //  PassengerNo = 1,
      //  ElavatorNo = 0,
      //  OriginFloor = 0,
      //  DestinationFloor = 1,
      //};
      //var passenger2 = new Passenger
      //{
      //  PassengerNo = 2,
      //  ElavatorNo = 1,
      //  OriginFloor = 0,
      //  DestinationFloor = 2,
      //};
      //var passenger3 = new Passenger
      //{
      //  PassengerNo = 3,
      //  ElavatorNo = 2,
      //  OriginFloor = 0,
      //  DestinationFloor = 3,
      //};

      //var passenger4 = new Passenger
      //{
      //  PassengerNo = 4,
      //  ElavatorNo = 3,
      //  OriginFloor = 3,
      //  DestinationFloor = 0,
      //};

      //var passenger5 = new Passenger
      //{
      //  PassengerNo = 5,
      //  ElavatorNo = 3,
      //  OriginFloor = 3,
      //  DestinationFloor = 0,
      //};

      //elevatorSystem.AddPassenger(passenger1);
      //elevatorSystem.AddPassenger(passenger2);
      //elevatorSystem.AddPassenger(passenger3);
      //elevatorSystem.AddPassenger(passenger4);
      //elevatorSystem.AddPassenger(passenger5);

      //elevatorSystem.StopElevators();

      //if random
      ElevatorSystem elevatorSystem = new ElevatorSystem(4, 10);
      elevatorSystem.OperateElevators();
      Console.Clear();
      for (int i = 0; i < 5; i++)
      {
        int originFloor = 0;
        int destinationFloor = 0;
        while (originFloor == destinationFloor)
        {
          originFloor = new Random().Next(9);
          destinationFloor = new Random().Next(9);
        }

        Passenger passenger = new Passenger
        {
          PassengerNo = i,
          ElavatorNo = new Random().Next(3),
          OriginFloor = originFloor,
          DestinationFloor = destinationFloor,
        };
        elevatorSystem.AddPassenger(passenger);
      }

      Console.ReadLine();
    }
  }
}
