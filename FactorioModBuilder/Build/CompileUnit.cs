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

        public UnitType Type { get; private set; }
        public string Value { get; private set; }
        public List<CompileUnit> ListValues { get; private set; }
        public Dictionary<string, CompileUnit> StructValues { get; private set; }

        public CompileUnit(string val)
        {
            this.Type = UnitType.Value;
            this.Value = val;
        }

        public CompileUnit(IEnumerable<CompileUnit> units)
        {
            this.Type = UnitType.List;
            this.ListValues = units.ToList();
        }

        public CompileUnit(IDictionary<string, CompileUnit> members)
        {
            this.Type = UnitType.Struct;
            this.StructValues = new Dictionary<string, CompileUnit>(members);
        }

        public implicit operator CompileUnit(string value)
        {
            return new CompileUnit(value);
        }

        public implicit operator CompileUnit(IEnumerable<CompileUnit> values)
        {
            return new CompileUnit(values);
        }

        public implicit operator CompileUnit(IDictionary<string, CompileUnit> values)
        {
            return new CompileUnit(values);
        }
    }
}
