using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlledCoolingCalc.Model
{
    public class _10CrSiNiCu : Steel
    {
        public _10CrSiNiCu()
        {
            ratio = 2.337;
        }

        public override void Calculate()
        {
            delta = tempBeginCooling - tempEndCooling;            
            totalWaterFlow = -37.392 * tempWater + 30.26 * thickness + 39.79 * sectionCount - 1.7979 * delta + 29.861 * coolingRate;
            waterFlowDown = totalWaterFlow / (ratio + 1);
            waterFlowUp = waterFlowDown * ratio;
            rollerSpeed = 0.09968 * sectionCount + (-0.01422) * thickness + (-0.00295) * delta + 0.00038 * waterFlowUp + 3.78E-5 * waterFlowDown + 0.00593 * tempWater + 0.05506 * coolingRate;
        }
    }
}
