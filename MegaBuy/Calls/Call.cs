using System;
using MegaBuy.Calls.Rules;
using MegaBuy.Money;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Calls
{
    public sealed class Call : IAutomaton
    {
        private readonly Caller _caller;
        private readonly Script _script;
        private readonly CallResolution _correctResolution;
        private readonly ICallOption _option;

        public Call(Caller caller, Script script, CallResolution correctResolution, ICallOption option)
        {
            _caller = caller;
            _script = script;
            _correctResolution = correctResolution;
            _option = option;
            World.Subscribe(new EventSubscription<CallResolved>(ResolveCall, this));
        }

        private void ResolveCall(CallResolved callResolved)
        {
            if (callResolved.Resolution == _correctResolution)
                World.Publish(new CallSucceeded());
            else
                World.Publish(new CallFailed(new Fee(2)));
        }

        public void Update(TimeSpan delta)
        {
            _caller.Update(delta);
        }
    }
}
