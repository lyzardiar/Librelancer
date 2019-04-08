﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package

using System;
using System.Collections.Generic;
using System.IO;
namespace LibreLancer.Utf.Ale
{
	public class AlchemyCurveAnimation
	{
		public EasingTypes Type;
		public List<AlchemyCurve> Items;

		public AlchemyCurveAnimation (BinaryReader reader)
		{
			Type = (EasingTypes)reader.ReadByte ();
			int scount = reader.ReadByte ();
			Items = new List<AlchemyCurve> (scount);
			for (int i = 0; i < scount; i++) {
				var cpkf = new AlchemyCurve ();
				cpkf.SParam = reader.ReadSingle ();
				cpkf.Value = reader.ReadSingle ();
				ushort loop = reader.ReadUInt16 ();
				cpkf.Flags = (LoopFlags)loop;
				ushort lcnt = reader.ReadUInt16 ();
				if (loop != 0 || lcnt != 0) {
					var l = new List<CurveKeyframe> (lcnt);
					for (int j = 0; j < lcnt; j++) {
						l.Add (new CurveKeyframe () {
							Time = reader.ReadSingle(),
							Value = reader.ReadSingle(),
							InTangent = reader.ReadSingle(),
							OutTangent = reader.ReadSingle()
						});
					}
					cpkf.Keyframes = l;
				}
				Items.Add (cpkf);
			}
		}

		public float GetValue(float sparam, float time)
		{
			//1 item, 1 value
			if (Items.Count == 1) {
				return Items [0].GetValue (time);
			}
			//Find 2 keyframes to interpolate between
			AlchemyCurve c1 = null, c2 = null;
			for (int i = 0; i < Items.Count - 1; i++) {
				if (sparam >= Items [i].SParam && sparam <= Items [i + 1].SParam) {
					c1 = Items [i];
					c2 = Items [i + 1];
				}
			}
			//We're at the end
			if (c1 == null) {
				return Items [Items.Count - 1].GetValue(time);
			}
			//Interpolate between SParams
			var v1 = c1.GetValue (time);
			var v2 = c2.GetValue (time);
			return AlchemyEasing.Ease (Type, sparam, c1.SParam, c2.SParam, v1, v2);
		}
	}
}

