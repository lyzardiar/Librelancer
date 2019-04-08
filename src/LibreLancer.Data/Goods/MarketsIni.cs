﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package

using System;
using System.Collections.Generic;
using LibreLancer.Ini;
    
namespace LibreLancer.Data.Goods
{
    public class MarketsIni : IniFile
    {
        [Section("basegood")]
        public List<BaseGood> BaseGoods = new List<BaseGood>();

        public void AddMarketsIni(string filename) => ParseAndFill(filename);
    }
}
