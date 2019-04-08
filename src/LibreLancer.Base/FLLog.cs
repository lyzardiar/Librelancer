﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package

using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Threading;

//High performance console output - nonblocking + colour coded
namespace LibreLancer
{
	public enum LogSeverity {
		Debug,
		Info,
		Warning,
		Error
	}
	public static class FLLog
	{
		#if DEBUG
		public static LogSeverity MinimumSeverity = LogSeverity.Debug;
#else
		public static LogSeverity MinimumSeverity = LogSeverity.Info;
#endif

        public static IUIThread UIThread;
        public static Action<string,LogSeverity> AppendLine;
		[DllImport("libc")]
		static extern bool isatty(int desc);

		public static void Write(string component, string message, LogSeverity severity)
		{
			if ((int)severity < (int)MinimumSeverity)
				return;
			var newC = ConsoleColor.White;
			switch (severity) {
			case LogSeverity.Debug:
				newC = ConsoleColor.DarkGray;
				break;
			case LogSeverity.Error:
				newC = ConsoleColor.Red;
				break;
			case LogSeverity.Warning:
				newC = ConsoleColor.Yellow;
				break;
			}
			NonblockWrite(newC, string.Format("[{0}] {1}: {2}", severity, component, message),severity);
		}

		struct NonblockingWrite
		{
            public LogSeverity Severity;
			public ConsoleColor Color;
			public string Value;
		}

		static BlockingCollection<NonblockingWrite> m_Queue = new BlockingCollection<NonblockingWrite>();
		static FLLog()
		{
			Thread thread = new Thread(
	 		() =>
	 		{
				 while (true)
				 {
					 var q = m_Queue.Take();
                     if (UIThread != null && AppendLine != null)
                         UIThread.QueueUIThread(() => AppendLine(q.Value,q.Severity));
					 if (Platform.RunningOS == OS.Windows)
					 {
						 var c = Console.ForegroundColor;
						 Console.ForegroundColor = q.Color;
						 Console.WriteLine(q.Value);
						 Console.ForegroundColor = c;
					 }
					 else if (q.Color != ConsoleColor.White && isatty(1))
					 {
						 string cc = "";
						 if (q.Color == ConsoleColor.DarkGray) cc = "\x1b[90m";
						 if (q.Color == ConsoleColor.Yellow) cc = "\x1b[33m";
						 if (q.Color == ConsoleColor.Red) cc = "\x1b[91m";
						 Console.WriteLine("{0}{1}\x1b[0m", cc, q.Value);
					 }
					 else
						 Console.WriteLine(q.Value);
				 }
	 		});
			thread.IsBackground = true;
            thread.Name = "Log";

            thread.Start();
		}
        static void NonblockWrite(ConsoleColor color, string message,LogSeverity severity)
		{
			m_Queue.Add(new NonblockingWrite() { Color = color, Value = message, Severity = severity });
		}

		public static void Info(string component, string message)
		{
			Write (component, message, LogSeverity.Info);
		}
		public static void Debug(string component, string message)
		{
			Write (component, message, LogSeverity.Debug);
		}
		public static void Warning(string component, string message)
		{
			Write (component, message, LogSeverity.Warning);
		}
		public static void Error(string component, string message)
		{
			Write (component, message, LogSeverity.Error);
		}
	}
}

