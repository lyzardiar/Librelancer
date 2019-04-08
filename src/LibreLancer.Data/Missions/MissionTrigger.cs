﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package
    
using System;
using System.Collections.Generic;
using LibreLancer.Ini;
namespace LibreLancer.Data.Missions
{
    public class MissionTrigger
    {
        [Entry("nickname")]
        public string Nickname;
        [Entry("system")]
        public string System;
        [Entry("repeatable")]
        public bool Repeatable;
        [Entry("InitState")]
        public TriggerInitState InitState;
        public List<MissionAction> Actions = new List<MissionAction>();
        public List<MissionCondition> Conditions = new List<MissionCondition>();

        bool HandleEntry(Entry e)
        {
            TriggerActions t;
            if (Enum.TryParse(e.Name, true, out t))
            {
                Actions.Add(new MissionAction(t, e));
                return true;
            }
            TriggerConditions c;
            if (Enum.TryParse(e.Name, true, out c))
            {
                Conditions.Add(new MissionCondition(c, e));
                return true;
            }

            return false;
        }
    }
    public class MissionAction
    {
        public TriggerActions Type;
        public Entry Entry;
        public MissionAction(TriggerActions act, Entry e)
        {
            Type = act;
            Entry = e;
        }
    }
    public class MissionCondition
    {
        public TriggerConditions Type;
        public Entry Entry;
        public MissionCondition(TriggerConditions cnd, Entry e)
        {
            Type = cnd;
            Entry = e;
        }
    }
}
