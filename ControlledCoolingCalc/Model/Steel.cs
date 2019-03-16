using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlledCoolingCalc.Model
{
    public abstract class Steel
    {
        public double tempBeginCooling { get; set; }
        public double tempEndCooling { get; set; }
        public double tempWater { get; set; }
        public double thickness { get; set; }
        protected double totalWaterFlow { get; set; }
        public double waterFlowUp { get; set; }
        public double waterFlowDown { get; set; }
        public int sectionCount { get; set; }
        public double coolingRate { get; set; }
        protected double delta { get; set; }
        public double rollerSpeed { get; set; }
        public double ratio { get; set; }

        public abstract void Calculate();
    }
}
