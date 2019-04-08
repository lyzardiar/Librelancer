﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package

using System;
using System.Linq;
namespace LibreLancer
{
	public class WeightedRandomCollection<T>
	{
		Random random;
		T[] items;
		float[] weights;
		float max;
		public WeightedRandomCollection(T[] items, int[] weights)
		{
			if (items.Length != weights.Length)
			{
				throw new InvalidOperationException();
			}
			random = new Random();
			max = weights.Sum();
			float current = 0;
			this.weights = new float[weights.Length];
			for (int i = 0; i < weights.Length; i++)
			{
				this.weights[i] = current + weights[i];
				current += weights[i];
			}
			this.items = items;
		}
		public T GetNext()
		{
			var val = (float)(random.NextDouble() * max);
			for (int i = 0; i < weights.Length; i++)
			{
				if (val < weights[i])
					return items[i];
			}
			return items[items.Length - 1];
		}
	}
}

