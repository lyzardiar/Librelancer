﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package
using System;
using System.Collections.Generic;
using LibreLancer.Ini;
namespace LibreLancer.Data.Missions
{
    public class ObjList
    {
        [Entry("nickname")]
        public string Nickname;
        [Entry("system")]
        public string System;
        public List<ObjCmd> Commands = new List<ObjCmd>();
        bool HandleEntry(Entry e)
        {
            ObjListCommands c;
            if(Enum.TryParse<ObjListCommands>(e.Name, true, out c))
            {
                Commands.Add(new ObjCmd() { Command = c, Entry = e });
                return true;
            }
            return false;
        }
    }
    public class ObjCmd
    {
        public ObjListCommands Command;
        public Entry Entry;
    }
}
