﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package

using System;
using System.Collections.Generic;
namespace LibreLancer
{
	public delegate void TextInputHandler (string text);
	public delegate void KeyEventHandler (KeyEventArgs e);
	public class Keyboard
	{
		public event TextInputHandler TextInput;
		public event KeyEventHandler KeyDown;
		public event KeyEventHandler KeyUp;
		Dictionary<Keys, bool> keysDown = new Dictionary<Keys, bool>();

		internal Keyboard ()
		{
		}

		internal void OnTextInput(string text)
		{
			if (TextInput != null)
				TextInput (text);
		}

		internal void OnKeyDown (Keys key, KeyModifiers mod, bool isRepeat)
		{
			if (KeyDown != null)
				KeyDown (new KeyEventArgs (key, mod, isRepeat));
			keysDown [key] = true;
		}

		internal void OnKeyUp (Keys key, KeyModifiers mod)
		{
			if (KeyUp != null)
				KeyUp (new KeyEventArgs (key, mod, false));
			keysDown [key] = false;
		}

		public bool IsKeyDown(Keys key)
		{
			return keysDown.ContainsKey (key) && keysDown [key];
		}

		public bool IsKeyUp (Keys key)
		{
			if (keysDown.ContainsKey (key))
				return !keysDown [key];
			return true;
		}
	}
}

