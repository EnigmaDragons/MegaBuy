﻿using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MonoDragons.Core.Engine
{
    public sealed class Metrics : IVisualAutomaton
    {
        private readonly Timer _timer;

        private int _framesThisSecond;
        private int _updatesThisSecond;
        private int _framesPerSecond;
        private int _updatesPerSecond;
        private int _frameRateTroubleCount;

        public Metrics()
        {
            _timer = new Timer(AccumulateMetrics, 500);
#if DEBUG
            AppDomain.MonitoringIsEnabled = true;
#endif
        }

        public void Update(TimeSpan delta)
        {
#if DEBUG  
            _timer.Update(delta);
            _updatesThisSecond++;
#endif
        }

        public void Draw(Transform2 parentTransform)
        {
#if DEBUG  
            UI.DrawText($"FPS: {_framesPerSecond}", new Vector2(0, 0), Color.Yellow);
            UI.DrawText($"UPS: {_updatesPerSecond}", new Vector2(0, 40), Color.Yellow);
            UI.DrawText($"RAM: {AppDomain.MonitoringSurvivedProcessMemorySize}", new Vector2(0, 80), Color.Yellow);
            UI.DrawText($"Sub: {World.CurrentEventSubscriptionCount}", new Vector2(0, 120), Color.Yellow);
            _framesThisSecond++;
#endif
        }

        private void AccumulateMetrics()
        {
            _framesPerSecond = _framesThisSecond * 2;
            _framesThisSecond = 0;
            _updatesPerSecond = _updatesThisSecond * 2;
            _updatesThisSecond = 0;
            CheckForProcessTrouble();
        }

        private void CheckForProcessTrouble()
        {
            if (_framesPerSecond < 12)
                _frameRateTroubleCount++;
            else
                _frameRateTroubleCount = 0;
            if (_frameRateTroubleCount > 4)
                Hack.TheGame.Exit();
        }
    }
}
