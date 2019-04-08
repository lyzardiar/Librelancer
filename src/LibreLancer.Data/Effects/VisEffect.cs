﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package

using System;
using System.Collections.Generic;
using LibreLancer.Ini;
namespace LibreLancer.Data.Effects
{
	public class VisEffect
	{
        [Entry("nickname")]
		public string Nickname;
        [Entry("effect_crc")]
		public int EffectCrc;
        [Entry("alchemy")]
		public string AlchemyPath;
        [Entry("textures", Multiline = true)]
		public List<string> Textures = new List<string>();
	}
}
