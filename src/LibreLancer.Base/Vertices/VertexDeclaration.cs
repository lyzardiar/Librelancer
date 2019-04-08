﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package

using System;

namespace LibreLancer.Vertices
{
	public class VertexDeclaration
	{
		public int Stride;
		public VertexElement[] Elements;

		public VertexDeclaration (int stride, params VertexElement[] elements)
		{
			Stride = stride;
			Elements = elements;
		}
		internal void SetPointers()
		{
			foreach (var e in Elements) {
				GL.EnableVertexAttribArray (e.Slot);
				GL.VertexAttribPointer ((uint)e.Slot, e.Elements, (int)e.Type, e.Normalized, Stride, (IntPtr)(e.Offset));
			}
		}

	}
}

