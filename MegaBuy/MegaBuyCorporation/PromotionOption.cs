using System;
using MegaBuy.Calls.Rules;

namespace MegaBuy.MegaBuyCorporation
{
    public struct PromotionOption
    {
        public Action AcceptResponse;
        public Action DeclineResponse;
        public JobRole Role;
        public string Message;

        public PromotionOption(string message, JobRole role, Action acceptResponse, Action declindResponse)
        {
            Message = message;
            Role = role;
            AcceptResponse = acceptResponse;
            DeclineResponse = declindResponse;
        }
    }
}