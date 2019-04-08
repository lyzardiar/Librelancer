﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package

using System;
namespace LibreLancer.Physics
{
    public interface IDebugRenderer
    {
        void DrawLine(Vector3 start, Vector3 end, Color4 color);
    }
}
