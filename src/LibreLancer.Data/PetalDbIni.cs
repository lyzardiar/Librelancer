// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package

using System;
using System.Collections.Generic;
using LibreLancer.Ini;
namespace LibreLancer.Data
{
	public class PetalDbIni : IniFile
	{
		public Dictionary<string, string> Rooms = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		public Dictionary<string, string> Props = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		public Dictionary<string, string> Carts = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		public void AddFile(string path)
		{
			foreach (var section in ParseFile(path))
			{
				if (!section.Name.Equals("objecttable", StringComparison.OrdinalIgnoreCase))
					throw new Exception("Unexpected section in PetalDB " + section);
				foreach (var e in section)
				{
					switch (e.Name.ToLowerInvariant())
					{
						case "room":
							if (!Rooms.ContainsKey(e[0].ToString()))
							{
								Rooms.Add(e[0].ToString(), e[1].ToString());
							}
							else
							{
								FLLog.Error("PetalDB", "Room " + e[0].ToString() + " duplicate entry");
							}
							break;
						case "prop":
							if (!Props.ContainsKey(e[0].ToString()))
							{
								Props.Add(e[0].ToString(), e[1].ToString());
							}
							else
							{
								FLLog.Error("PetalDB", "Prop " + e[0].ToString() + " duplicate entry");
							}
							break;
						case "cart":
							if (!Carts.ContainsKey(e[0].ToString()))
							{
								Carts.Add(e[0].ToString(), e[1].ToString());
							}
							else
							{
								FLLog.Error("PetalDB", "Cart " + e[0].ToString() + " duplicate entry");
							}
							break;
					}
				}
			}
		}
	}
}
