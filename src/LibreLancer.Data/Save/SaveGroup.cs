﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package

using System;
using System.Collections.Generic;
using LibreLancer.Ini;
namespace LibreLancer.Data.Save
{
    public class SaveGroup
    {
        [Entry("nickname")]
        public string Nickname;

        public List<SaveRep> Rep = new List<SaveRep>();

        bool HandleEntry(Entry e)
        {
            if(e.Name.Equals("rep", StringComparison.OrdinalIgnoreCase))
            {
                Rep.Add(new SaveRep(e));
                return true;
            }
            return false;
        }
    }
}
