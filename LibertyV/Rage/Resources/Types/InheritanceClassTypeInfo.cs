﻿/*
 
    LibertyV - Viewer/Editor for RAGE Package File version 7
    Copyright (C) 2013  koolk <koolkdev at gmail.com>
   
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
  
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
   
    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibertyV.Rage.Resources.Types
{
    class InheritanceClassTypeInfo : ClassTypeInfo
    {
        // TODO: Support multiplie class inheritances.
        protected InheritanceClassTypeInfo(string name, ClassTypeInfo parent)
            : base(name)
        {
            base.AddMember("base", parent);
        }
        protected InheritanceClassTypeInfo(string name, string parentName)
            : base(name)
        {
            base.AddMember("base", parentName);
        }

        public override ResourceObject CreateClass(ResourceReader reader)
        {
            Dictionary<string, ResourceObject> values = new Dictionary<string, ResourceObject>();
            foreach (var member in this.Members)
            {
                values[member.Item1] =  (member.Item1 == "base") ? (member.Item2 as ClassTypeInfo).CreateClass(reader) : member.Item2.Create(reader);
            }
            return new ClassObject(this, values);
        }
    }
}