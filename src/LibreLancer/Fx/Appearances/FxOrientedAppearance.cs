﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package

using System;
using LibreLancer.Utf.Ale;
namespace LibreLancer.Fx
{
	public class FxOrientedAppearance : FxBasicAppearance
	{
		public AlchemyFloatAnimation Height;
		public AlchemyFloatAnimation Width;

		public FxOrientedAppearance(AlchemyNode ale) : base(ale) 
		{
			AleParameter temp;
			if (ale.TryGetParameter("OrientedApp_Height", out temp))
			{
				Height = (AlchemyFloatAnimation)temp.Value;
			}
			if (ale.TryGetParameter("OrientedApp_Width", out temp))
			{
				Width = (AlchemyFloatAnimation)temp.Value;
			}
		}

		public override void Draw(ref Particle particle, float lasttime, float globaltime, NodeReference reference, ResourceManager res, Billboards billboards, ref Matrix4 transform, float sparam)
		{
			var time = particle.TimeAlive / particle.LifeSpan;
            var node_tr = GetAttachment(reference, transform);

			var p = node_tr.Transform(particle.Position);
			Texture2D tex;
			Vector2 tl, tr, bl, br;
			HandleTexture(res, globaltime, sparam, ref particle, out tex, out tl, out tr, out bl, out br);
			var c = Color.GetValue(sparam, time);
			var a = Alpha.GetValue(sparam, time);

			var p2 = node_tr.Transform(particle.Position + particle.Normal);
			//var n = (p - p2).Normalized();
			var n = Vector3.UnitZ;

			/*billboards.DrawPerspective(
				tex,
				p,
				new Vector2(Width.GetValue(sparam, time), Height.GetValue(sparam, time)),
				new Color4(c, a),
				tl,
				tr,
				bl,
				br,
				n,
                Rotate == null ? 0f : MathHelper.DegreesToRadians(Rotate.GetValue(sparam, time)),
				SortLayers.OBJECT,
				BlendInfo
			);*/
		}
	}
}

