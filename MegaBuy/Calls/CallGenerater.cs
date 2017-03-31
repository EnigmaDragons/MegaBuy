using MegaBuy.Calls.PositionBasedCall;
using MegaBuy.Calls.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaBuy.Calls
{
    public class CallGenerater
    {
        private CallCenterPosition _position;

        public CallGenerater(CallCenterPosition position)
        {
            _position = position;
        }

        public void PositionChanged(CallCenterPosition postion)
        {
            _position = postion;
        }

        public Call GenerateCall()
        {
            if (_position == CallCenterPosition.Referrer)
                return Referrer.NewCall();
            throw new Exception();
        }
    }
}
