﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and confiditons defined in
// LICENSE, which is part of this source code package

using System;
using LibreLancer.Thorn;
namespace LibreLancer
{
    [ThnEventRunner(EventTypes.StartFogPropAnim)]
    public class StartFogPropAnimRunner : IThnEventRunner
    {
        class FogPropAnimRoutine : IThnRoutine
        {
            public ThnEvent Event;
            public Vector3? FogColor;
            public float? FogStart;
            public float? FogEnd;
            public float? FogDensity;

            public Color4 OrigFogColor;
            public float OrigFogStart;
            public float OrigFogEnd;
            public float OrigFogDensity;

            double t = 0;
            public bool Run(Cutscene cs, double delta)
            {
                t += delta;
                if (t > Event.Duration)
                    return false;

                return true;
            }
        }

        public void Process(ThnEvent ev, Cutscene cs)
        {
            //fogmode is ignored.
            //fogdensity is ignored.
            var fogprops = (LuaTable)ev.Properties["fogprops"];

            object tmp;
            Vector3 tmp2;

            //Nullable since we are animating
            bool? fogon = null;
            Vector3? fogColor = null;
            float? fogstart = null;
            float? fogend = null;
            float? fogDensity = null;
            FogModes fogMode = FogModes.Linear;
            //Get values
            if (fogprops.TryGetValue("fogon", out tmp))
                fogon = ThnEnum.Check<bool>(tmp);
            if (fogprops.TryGetValue("fogmode", out tmp))
                fogMode = ThnEnum.Check<FogModes>(tmp);
            if (fogprops.TryGetValue("fogdensity", out tmp))
                fogDensity = (float)tmp;
            if (fogprops.TryGetVector3("fogcolor", out tmp2))
                fogColor = tmp2;
            if (fogprops.TryGetValue("fogstart", out tmp))
                fogstart = (float)tmp;
            if (fogprops.TryGetValue("fogend", out tmp))
                fogend = (float)tmp;

            if (fogon.HasValue) //i'm pretty sure this can't be animated
                cs.Renderer.SystemLighting.FogMode = fogon.Value ? fogMode : FogModes.None;

            //Set fog
            if (Math.Abs(ev.Duration) < float.Epsilon) //just set it
            {
                if (fogColor.HasValue)
                {
                    var v = fogColor.Value;
                    v *= (1 / 255f);
                    cs.Renderer.SystemLighting.FogColor = new Color4(v.X, v.Y, v.Z, 1);
                }
                if (fogstart.HasValue)
                    cs.Renderer.SystemLighting.FogRange.X = fogstart.Value;
                if (fogend.HasValue)
                    cs.Renderer.SystemLighting.FogRange.Y = fogend.Value;
                if (fogDensity.HasValue)
                    cs.Renderer.SystemLighting.FogDensity = fogDensity.Value;
            }
            else
                cs.Coroutines.Add(new FogPropAnimRoutine() //animate it!
                {
                    Event = ev,
                    FogDensity = fogDensity,
                    FogColor = fogColor,
                    FogStart = fogstart,
                    FogEnd = fogend,
                    OrigFogColor = cs.Renderer.SystemLighting.FogColor,
                    OrigFogStart = cs.Renderer.SystemLighting.FogRange.X,
                    OrigFogEnd = cs.Renderer.SystemLighting.FogRange.Y,
                    OrigFogDensity = cs.Renderer.SystemLighting.FogDensity
                });
        }
    }
}
