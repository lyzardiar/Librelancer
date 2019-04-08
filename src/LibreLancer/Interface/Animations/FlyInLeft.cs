﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package

using System;

namespace LibreLancer
{
	public class FlyInLeft : UIAnimation
	{
		Vector2 finalPos;
        public float From = -2;
		public FlyInLeft(Vector2 final, double start, double time) : base(start, time)
		{
			finalPos = final;
			CurrentPosition.Y = finalPos.Y;
		}

		protected override void Run (double currentTime)
		{
			CurrentPosition.X = (float)Easings.Circular.EaseOut (
				currentTime, 
				From,
				Math.Abs (finalPos.X - (From)),
				Duration
			);
		}
	}
}

