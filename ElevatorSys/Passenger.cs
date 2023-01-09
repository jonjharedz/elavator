using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSys
{
  public class Passenger
  {
    public int OriginFloor { get; set; }
    public int DestinationFloor { get; set; }

    public Direction Direction { get; set; }

    public bool isDelivered { get; set; }
    public bool isIn { get; set; }

    public int ElavatorNo { get; set; }
    public int PassengerNo { get; set; }

  }

}
