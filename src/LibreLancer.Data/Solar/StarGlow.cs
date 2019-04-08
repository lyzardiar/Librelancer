﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package

using System;
using LibreLancer.Ini;
namespace LibreLancer.Data.Solar
{
	public class StarGlow
	{
        [Entry("nickname")]
		public string Nickname;
        [Entry("shape")]
		public string Shape;
        [Entry("scale")]
		public int Scale;
        [Entry("inner_color")]
		public Color3f InnerColor;
        [Entry("outer_color")]
		public Color3f OuterColor;
	}
}

