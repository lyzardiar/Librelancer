﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and confiditons defined in
// LICENSE, which is part of this source code package

using System;

namespace LibreLancer
{
	static class GLBind
	{
		static uint programBound = 0;
		public static void UseProgram(uint prg)
		{
			if (programBound != prg) {
				GL.UseProgram (prg);
				programBound = prg;
			}
		}

		public static void Trash()
		{
			programBound = 0;
			bound_vao = 0;
           
            active_unit = -1;
        }
        static int active_unit = -1;
        public static void BindTexture(int unit, int target, uint texture)
        {
			var uval = GL.GL_TEXTURE0 + unit;
			if (uval != active_unit)
			{
				GL.ActiveTexture(uval);
				active_unit = uval;
			}
			GL.BindTexture(target, texture);
        }

		static uint bound_vao = 0;

		public static void VertexArray(uint vao)
		{
			if (bound_vao != vao) {
				bound_vao = vao;
				GL.BindVertexArray (vao);
			}
		}
	}
}

