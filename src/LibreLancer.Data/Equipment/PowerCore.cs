﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package

using System;
using LibreLancer.Ini;
namespace LibreLancer.Data.Equipment
{
	public class PowerCore : AbstractEquipment
	{
        [Entry("volume")]
        public int Volume;
        [Entry("mass")]
        public int Mass;
        [Entry("capacity")]
        public int Capacity;
        [Entry("charge_rate")]
        public int ChargeRate;
        [Entry("thrust_capacity")]
        public int ThrustCapacity;
        [Entry("thrust_charge_rate")]
        public int ThrustChargeRate;
	}
}
