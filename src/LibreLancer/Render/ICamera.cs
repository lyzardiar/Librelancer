﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package

using System;
namespace LibreLancer
{
	public interface ICamera
	{
		Matrix4 ViewProjection { get; }
		Matrix4 Projection { get; }
		Matrix4 View { get; }
		Vector3 Position { get; }
		BoundingFrustum Frustum { get; }
        long FrameNumber { get; }
	}
}

