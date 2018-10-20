﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and confiditons defined in
// LICENSE, which is part of this source code package

using System;

namespace LibreLancer
{
	public interface IUIThread
	{
		void EnsureUIThread(Action work);
		void QueueUIThread(Action work);
	}
}

