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
        private string _player;

        public CallGenerater(CallCenterPosition position, string player = "Player")
        {
            _player = player;
            _position = position;
        }

        public void PositionChanged(CallCenterPosition postion)
        {
            _position = postion;
        }

        public Call GenerateCall()
        {
            if (_position == CallCenterPosition.Referrer)
                return Referrer.NewCall(_player);
            throw new Exception();
        }
    }
}
