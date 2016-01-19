﻿using System;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using LibreLancer.Primitives;
using LibreLancer.Vertices;
namespace LibreLancer
{
	public class DemoSystemView : GameState
	{
		private const float ROTATION_SPEED = 1f;
		GameData.StarSystem sys;
		Camera camera;
		SystemRenderer sysrender;
		bool wireframe = false;

		public DemoSystemView (FreelancerGame g) : base(g)
		{
			FLLog.Info ("Game", "Starting System Viewer Demo");
			sys = g.GameData.GetSystem ("Li01");
			camera = new Camera (g.Viewport);
			camera.Zoom = 5000;
			sysrender = new SystemRenderer (camera, g.GameData, g.ResourceManager);
			sysrender.StarSystem = sys;
			camera.UpdateProjection ();
			Game.KeyPress += (object sender, OpenTK.KeyPressEventArgs e) => {
				if(e.KeyChar == 'p') {
					wireframe = !wireframe;
					if(wireframe) {
						GL.PolygonMode (MaterialFace.FrontAndBack, PolygonMode.Line);
					} else {
						GL.PolygonMode (MaterialFace.FrontAndBack, PolygonMode.Fill);
					}
				}
			};
		}

		public override void Update (TimeSpan delta)
		{
			if (Game.Keyboard [Key.Right]) {
				camera.Rotation = new Vector2 (camera.Rotation.X - (ROTATION_SPEED * (float)delta.TotalSeconds),
					camera.Rotation.Y);
			}
			if (Game.Keyboard [Key.Left]) {
				camera.Rotation = new Vector2 (camera.Rotation.X + (ROTATION_SPEED * (float)delta.TotalSeconds),
					camera.Rotation.Y);
			}
			if (Game.Keyboard [Key.Up]) {
				camera.Rotation = new Vector2 (camera.Rotation.X,
					camera.Rotation.Y  + (ROTATION_SPEED * (float)delta.TotalSeconds));
			}
			if (Game.Keyboard [Key.Down]) {
				camera.Rotation = new Vector2 (camera.Rotation.X,
					camera.Rotation.Y  - (ROTATION_SPEED * (float)delta.TotalSeconds));
			}
			if (Game.Keyboard [Key.W]) {
				camera.MoveVector = VectorMath.Forward;
			}
			if (Game.Keyboard [Key.S]) {
				camera.MoveVector = VectorMath.Backward;
			}
			if (Game.Keyboard [Key.A]) {
				camera.MoveVector = VectorMath.Left;
			}
			if (Game.Keyboard [Key.D]) {
				camera.MoveVector = VectorMath.Right;
			}
			camera.Update (delta);
			camera.Free = true;
			sysrender.Update (delta);
		}
		public override void Draw (TimeSpan delta)
		{
			sysrender.Draw ();
		}
	}
}
