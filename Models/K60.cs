using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Model
{
    public class K60 : Steel
    {
        public K60()
        {
            ratio = 2.4;
        }

        public override void Calculate(bool _isWaterFlowDownManual, double _waterFlowDownManual, bool _isWaterFlowUpManual, double _waterFlowUpManual)
        {
            delta = tempBeginCooling - tempEndCooling;
            totalWaterFlow = 37.556 * tempWater + 36.217 * thickness - 60.56 * sectionCount + 0.0198 * delta + 23.109 * coolingRate;
            GetWaterFlowUpDown(_isWaterFlowDownManual, _waterFlowDownManual, _isWaterFlowUpManual, _waterFlowUpManual);
            //waterFlowDown = totalWaterFlow / (ratio + 1);
            //waterFlowUp = waterFlowDown * ratio;
            //if (_isWaterFlowDownManual)
            //{
            //    waterFlowDown = _waterFlowDownManual;
            //}
            //else
            //{
            //    waterFlowDown = totalWaterFlow / (ratio + 1);
            //}
            //if (_isWaterFlowUpManual)
            //{
            //    waterFlowUp = _waterFlowUpManual;
            //}
            //else
            //{
            //    waterFlowUp = waterFlowDown * ratio;
            //}
            rollerSpeed = 0.1609 * sectionCount - 0.01448 * thickness - 0.00747 * delta - 0.00045 * waterFlowUp + 0.000719 * waterFlowDown - 0.01243 * tempWater + 0.06658 * coolingRate;
        }
    }
}
