﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace LibreLancer.Vertices
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPosition : IVertexType
    {
        public Vector3 Position;

        public VertexPosition(BinaryReader reader)
            : this()
        {
            this.Position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        public VertexPosition(Vector3 position)
            : this()
        {
            this.Position = position;
        }

		public VertexDeclaration GetVertexDeclaration()
		{
			return new VertexDeclaration(
				sizeof(float) * 3,
				new VertexElement(VertexSlots.Position, 3, VertexElementType.Float, false, 0)
			);
		}
    }
}
