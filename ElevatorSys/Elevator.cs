using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace ElevatorSys
{
  public class Elevator
  {
    public int _numberOfFloors;
    public Direction _direction = Direction.UP;
    public int _currentFloor = 0;
    public int _elevatorNo = 0;
    public  bool _started;
    public int intervalMs = 0;
    public  List<Passenger> passengers = new List<Passenger>();
    private Thread _thread;

    public Elevator(int numberOfFloors, int elavatorNo)
    {
      _elevatorNo = elavatorNo;
      _numberOfFloors = numberOfFloors;
      _started = false;
    }

    public void Start()
    {
      _thread = new Thread(Operate);
      _thread.Start();
      _started = true;
    }

    public List<Passenger> GetPassengers()
    {
      return passengers;
    }

    public void Operate()
    {
      while (_started)
      {      
        if (passengers.Count > 0)
        {
          checkPassengers();
          if (_direction == Direction.UP)
          {
            checkHasPassengersInRemainingFloors();
            if (checkHasPassengers())
            {
              _direction = Direction.UP;
            }
            MoveUp();
          }
          else
          {
            MoveDown();
          }
          if (_started)
            Console.WriteLine($"car#: {_elevatorNo}  floor#: {_currentFloor}, DateTime: {DateTime.Now} Moving " + (_direction == Direction.UP ? "UP" : "DOWN"));
        }

      }
    }

    public void StopOperation()
    {
      _thread.Abort();
    }

    public void AddPassenger(Passenger passenger)
    {
      passengers.Add(passenger);
      _started = true;
    }

    public void Stop()
    {
      _started = false;
      Console.WriteLine($"car#: {_elevatorNo}  floor#: {_currentFloor}, DateTime: {DateTime.Now} Stopped");
    }

    private bool checkHasPassengers()
    {
      var deliveredCount = passengers.Where(w => w.isDelivered).Count();

      if (deliveredCount == passengers.Count && passengers.Count > 0)
      {
        _direction = Direction.DOWN;
        return false;
      }  

      return true;
    }

    private void checkHasPassengersInRemainingFloors()
    {
      var hasPassenges = passengers.Where(w => (w.OriginFloor >= _currentFloor || w.DestinationFloor >= _currentFloor)  && (!w.isIn || (w.isIn && !w.isDelivered))).ToList().Count > 0;
      if (!hasPassenges)
      {
          _direction = Direction.DOWN;
      }
    }

    private void checkPassengers()
    {
      var inPassengers = passengers.Where(w => w.OriginFloor == _currentFloor && !w.isIn).ToList();
      var outPassenges = passengers.Where(w => w.DestinationFloor == _currentFloor && w.isIn  && !w.isDelivered).ToList();

      if (inPassengers.Any() || outPassenges.Any())
      {
        foreach (var pIn in inPassengers)
        {
          updatePassenger(pIn, true, false);
        }

        foreach (var p in outPassenges)
        {
          updatePassenger(p, false, true);
        }
        wait();

      }
    }

    private void updatePassenger(Passenger passenger, bool isIn, bool isOut)
    {
      for(var i=0; i<passengers.Count; i++)
      {
        if (passengers[i].PassengerNo == passenger.PassengerNo)
        {
          if (isIn)
          {
            passengers[i].isIn = true;
          }
          if(isOut)
          {
            passengers[i].isDelivered = true;
          }
          Console.WriteLine($"car#: {_elevatorNo}  floor#: {_currentFloor}, DateTime: {DateTime.Now}, passenger#: {passenger.PassengerNo}, action: " + (isIn ? "In" : "out"));
        }
      }
    }

    private void wait()
    {
      Thread.Sleep(intervalMs);
    }

    private void MoveUp()
    {
      if (!checkHasPassengers() && _currentFloor ==0)
      {
      //  Stop();
        return;
      }
      wait();
      _currentFloor++;
      if(_currentFloor>=_numberOfFloors-1)
      {
        _currentFloor = _numberOfFloors - 1;
        _direction = Direction.DOWN;
      }
       
    }

    private void MoveDown()
    {
      wait();
      _currentFloor--;
      if (_currentFloor <= 0)
      {
        _currentFloor = 0;
        _direction = Direction.UP;
        if(!checkHasPassengers())
        {
          Stop();
        }
      }
    }
  }
}
