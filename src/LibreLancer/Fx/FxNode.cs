﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package

using System;
using LibreLancer.Utf.Ale;

namespace LibreLancer.Fx
{
	public class FxNode
	{
		public string Name;
		public string NodeName = "LIBRELANCER:UNNAMED_NODE";
		public uint CRC;
		public float NodeLifeSpan = float.PositiveInfinity;
		public AlchemyTransform Transform;

		public FxNode(AlchemyNode ale)
		{
			Name = ale.Name;
			AleParameter temp;
			if (ale.TryGetParameter ("Node_Name", out temp)) {
				NodeName = (string)temp.Value;
				CRC = CrcTool.FLAleCrc(NodeName);
			}
			if (ale.TryGetParameter ("Node_Transform", out temp)) {
				Transform = (AlchemyTransform)temp.Value;
			} else {
				Transform = new AlchemyTransform ();
			}
			if (ale.TryGetParameter ("Node_LifeSpan", out temp)) {
				NodeLifeSpan = (float)temp.Value;
			}
		}

        protected static Matrix4 GetAttachment(NodeReference reference, Matrix4 attachment)
		{
			if (reference.IsAttachmentNode) {
                return attachment;
			}
			else if (reference.Parent == null) {
				return Matrix4.Identity;
			}
			else {
                return GetAttachment(reference.Parent, attachment);
			}
		}

		public virtual void Update(NodeReference reference, ParticleEffectInstance instance, TimeSpan delta, ref Matrix4 transform, float sparam)
		{
		}

		public FxNode(string name, string nodename)
		{
			Name = name;
			NodeName = nodename;
		}
	}
}

