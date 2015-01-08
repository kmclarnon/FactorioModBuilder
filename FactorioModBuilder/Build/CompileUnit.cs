using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build
{
    public class CompileUnit
    {
        public enum UnitType
        {
            Value,
            List,
            Struct
        }

        public enum ValueType
        {
            Number,
            Text,
            None
        }

        public UnitType UType { get; private set; }

        public ValueType VType { get; private set; }
        public string SValue { get; private set; }
        public int IValue { get; private set; }

        public List<CompileUnit> ListValues { get; private set; }
        public Dictionary<string, CompileUnit> StructValues { get; private set; }
        public string KeyValueSeparator { get; private set; }
        public bool SingleLine { get; private set; }

        public CompileUnit(string val)
        {
            this.UType = UnitType.Value;
            this.VType = ValueType.Text;
            this.SValue = val;
            this.SingleLine = true;
        }

        public CompileUnit(int val)
        {
            this.UType = UnitType.Value;
            this.VType = ValueType.Number;
            this.IValue = val;
            this.SingleLine = true;
        }

        public CompileUnit(IEnumerable<CompileUnit> units)
        {
            this.UType = UnitType.List;
            this.VType = ValueType.None;
            this.ListValues = units.ToList();
            this.SingleLine = units.Select(o => o.SingleLine).Aggregate((a, b) => a && b);
        }

        public CompileUnit(IDictionary<string, CompileUnit> members)
            : this(members, " = ")
        {
        }

        public CompileUnit(IDictionary<string, CompileUnit> members, string keyValueSeparator)
        {
            this.UType = UnitType.Struct;
            this.VType = ValueType.None;
            this.StructValues = new Dictionary<string, CompileUnit>(members);
            this.KeyValueSeparator = keyValueSeparator;
            this.SingleLine = false;
        }

        public override string ToString()
        {
            switch (this.UType)
            {
                case UnitType.Value:
                    switch (this.VType)
                    {
                        case ValueType.Number: return this.IValue.ToString();
                        case ValueType.Text: return "\"" + this.SValue + "\"";
                    }
                    break;
                case UnitType.List: return "Count = " + this.ListValues.Count;
                case UnitType.Struct: return "Count = " + this.StructValues.Count;
            }

            throw new Exception("Internal consistency error");
        }
    }
}
