using FactorioModBuilder.Models.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Data
{
    public class EquipmentData : DataUnit
    {
        public string Name { get; private set; }
        
        public string SpriteIconPath { get; private set; }
        public int SpriteWidth { get; private set; }
        public int SpriteHeight { get; private set; }
        public SpritePriority SpritePriority { get; private set; }

        public int ShapeWidth { get; private set; }
        public int ShapeHeight { get; private set; }
        public ShapeType ShapeType { get; private set; }

        public EnergySourceType EnergyType { get; private set; }
        public int BufferCap { get; private set; }
        public EnergyUnit BufferCapUnit { get; private set; }
        public int InputFlowLimit { get; private set; }
        public EnergyUnit InputFlowLimitUnit { get; private set; }
        public UsagePriority EnergyPiority { get; private set; }

        public EquipmentData(string name, string spIcon, int spWidth, int spHeight, 
            SpritePriority spPriority, int shWidth, int shHeight, ShapeType shType,
            EnergySourceType eSourceType, int buffCap, EnergyUnit buffCapUnit,
            int inFlowLmt, EnergyUnit inFlowLmtUnit, UsagePriority energyPriority)
            : base(Extensions.ExtensionType.PrototypeEquipment)
        {
            this.Name = name;
            this.SpriteIconPath = spIcon;
            this.SpriteWidth = spWidth;
            this.SpriteHeight = spHeight;
            this.SpritePriority = spPriority;

            this.ShapeWidth = shWidth;
            this.ShapeHeight = shHeight;
            this.ShapeType = shType;

            this.EnergyType = eSourceType;
            this.BufferCap = buffCap;
            this.BufferCapUnit = buffCapUnit;
            this.InputFlowLimit = inFlowLmt;
            this.InputFlowLimitUnit = inFlowLmtUnit;
            this.EnergyPiority = energyPriority;
        }
    }
}
