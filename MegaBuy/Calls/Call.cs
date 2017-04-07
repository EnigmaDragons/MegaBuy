﻿using System;
using System.Collections.Generic;
using MegaBuy.Calls.Rules;
using MegaBuy.Money;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Calls
{
    public sealed class Call : IAutomaton
    {
        public List<ICallOption> Options { get; }
        public Caller Caller { get; }
        public Script Script { get; }
        
        private readonly CallResolution _correctResolution;

        public Call(Caller caller, Script script, CallResolution correctResolution, List<ICallOption> options)
        {
            Caller = caller;
            Script = script;
            _correctResolution = correctResolution;
            Options = options;
            World.Subscribe(new EventSubscription<CallResolved>(ResolveCall, this));
        }

        public void Update(TimeSpan delta)
        {
            Caller.Update(delta);
        }

        private void ResolveCall(CallResolved callResolved)
        {
            if (callResolved.Resolution == _correctResolution)
                World.Publish(new CallSucceeded(GetHashCode()));
            else
                World.Publish(new CallFailed(new Fee(2)));
            World.Publish(new CallRated(GetHashCode(), CallerPatienceCallRatings.Get(Caller.Patience)));
            World.Unsubscribe(this);
        }
    }
}
