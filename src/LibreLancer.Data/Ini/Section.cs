﻿// MIT License - Copyright (c) Malte Rupprecht
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibreLancer.Ini
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class Section : ICollection<Entry>
	{
		public string Name { get; private set; }

        public string File;
        public int Line;

		private List<Entry> entries;

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
		public Section(BinaryReader reader, string stringBlock)
		{
			if (reader == null) throw new ArgumentNullException("reader");
			if (stringBlock == null) throw new ArgumentNullException("stringBlock");

			short nameOffset = reader.ReadInt16();
			Name = stringBlock.Substring(nameOffset, stringBlock.IndexOf('\0', nameOffset) - nameOffset);

			short count = reader.ReadInt16();
			entries = new List<Entry>(count);

			for (int i = 0; i < count; i++)
				entries.Add(new Entry(reader, stringBlock));
		}

		public Section(string name)
		{
			if (name == null) throw new ArgumentNullException("name");

			entries = new List<Entry>();
			this.Name = name;
		}

		public Entry this[int index]
		{
			get { return entries[index]; }
			set { entries[index] = value; }
		}

		public Entry this[string name]
		{
			get
			{
				IEnumerable<Entry> candidates = from Entry e in entries where e.Name == name select e;
				int count = candidates.Count<Entry>();
				/*if (count == 1)
                    return candidates.First<Entry>();
                else if (count == 0)
                    return null;
                else
                    throw new FileContentsException(IniFile.INI, count + " entries with the name " + name);*/
				if (count == 0) return null;
				else return candidates.First<Entry>();
			}
		}

		public void Add(Entry item)
		{
			entries.Add(item);
		}

		public void Clear()
		{
			entries.Clear();
		}

		public bool Contains(Entry item)
		{
			return entries.Contains(item);
		}

		public void CopyTo(Entry[] array, int arrayIndex)
		{
			entries.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return entries.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(Entry item)
		{
			return entries.Remove(item);
		}

		public IEnumerator<Entry> GetEnumerator()
		{
			return entries.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return entries.GetEnumerator();
		}

		/*public static bool operator ==(Section operand1, Section operand2)
        {
            return operand1.Equals(operand2);
        }

        public static bool operator !=(Section operand1, Section operand2)
        {
            return !(operand1 == operand2);
        }

        public override bool Equals(object obj)
        {
            if (obj is Section)
            {
                Section s = (Section)obj;
                return namePointer == s.namePointer && entries == s.entries;
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return namePointer.GetHashCode() ^ entries.GetHashCode();
        }*/

		public override string ToString()
		{
			//string result = "[" + Name + "]\r\n";
			//foreach (Entry e in entries) result += e + "\r\n";
			//return result;
			return Name;
		}
	}
}