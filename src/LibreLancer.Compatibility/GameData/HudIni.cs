﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and confiditons defined in
// LICENSE, which is part of this source code package

using System;
using System.Collections.Generic;
using System.Linq;
using LibreLancer.Ini;
using LibreLancer.Compatibility.GameData.Interface;
namespace LibreLancer.Compatibility.GameData
{
	public class HudIni : IniFile
	{
		public List<HudManeuver> Maneuvers = new List<HudManeuver>();
		public HudIni()
		{
		}
		public void AddIni(string path)
		{
			foreach (var section in ParseFile(path))
			{
				switch (section.Name.ToLowerInvariant())
				{
					case "maneuvers":
						foreach (var e in section)
							Maneuvers.Add(new HudManeuver(e));
						break;
				}
			}
		}
	}
}