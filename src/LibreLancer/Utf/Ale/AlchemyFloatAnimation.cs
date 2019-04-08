﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package

using System;
using System.Collections.Generic;
using System.IO;
namespace LibreLancer.Utf.Ale
{
	public class AlchemyFloatAnimation
	{
		public EasingTypes Type;
		public List<AlchemyFloats> Items = new List<AlchemyFloats> ();
		public AlchemyFloatAnimation (BinaryReader reader)
		{
			Type = (EasingTypes)reader.ReadByte ();
			int itemsCount = reader.ReadByte ();
			for (int fc = 0; fc < itemsCount; fc++) {
				var floats = new AlchemyFloats ();
				floats.SParam = reader.ReadSingle ();
				floats.Type = (EasingTypes)reader.ReadByte ();
				floats.Data = new Tuple<float, float>[reader.ReadByte ()];
				for (int i = 0; i < floats.Data.Length; i++) {
					floats.Data [i] = new Tuple<float, float> (reader.ReadSingle (), reader.ReadSingle ());
				}
				Items.Add (floats);
			}
		}
		public float GetValue(float sparam, float time)
		{
			//1 item, 1 value
			if (Items.Count == 1) {
				return Items [0].GetValue (time);
			}
			//Find 2 keyframes to interpolate between
			AlchemyFloats f1 = null, f2 = null;
			for (int i = 0; i < Items.Count - 1; i++) {
				if (sparam >= Items [i].SParam && sparam <= Items [i + 1].SParam) {
					f1 = Items [i];
					f2 = Items [i + 1];
				}
			}
			//We're at the end
			if (f1 == null) {
				return Items [Items.Count - 1].GetValue(time);
			}
			//Interpolate between SParams
			var v1 = f1.GetValue (time);
			var v2 = f2.GetValue (time);
			return AlchemyEasing.Ease (Type, sparam, f1.SParam, f2.SParam, v1, v2);
		}
		public override string ToString ()
		{
			return string.Format ("<Fanim: Type={0}, Count={1}>",Type,Items.Count);
		}
	}
}

