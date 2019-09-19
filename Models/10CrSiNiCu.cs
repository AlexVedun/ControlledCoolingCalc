using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Model
{
    public class _10CrSiNiCu : Steel
    {
        public _10CrSiNiCu()
        {
            ratio = 2.337;
        }

        public override void Calculate(bool _isWaterFlowDownManual, double _waterFlowDownManual, bool _isWaterFlowUpManual, double _waterFlowUpManual)
        {
            //double deltaAir =   0.079861 * rollingEndTemp 
            //                    - 0.86541 * thickness 
            //                    - 0.14331 * tempWater;
            //tempBeginCooling = rollingEndTemp - deltaAir;
            delta = tempBeginCooling - tempEndCooling;
            //totalWaterFlow = -37.392 * tempWater + 30.2604 * thickness + 39.79 * sectionCount - 1.7979 * delta + 29.861 * coolingRate + 30;
            totalWaterFlow =    56.66324 * tempWater 
                                + 34.29533 * thickness 
                                - 92.774 * sectionCount 
                                + 0.38690506 * delta 
                                + 25.34494 * coolingRate;
            GetWaterFlowUpDown(_isWaterFlowDownManual, _waterFlowDownManual, _isWaterFlowUpManual, _waterFlowUpManual);
            //rollerSpeed = 0.09968 * sectionCount - 0.01422 * thickness - 0.00295 * delta + 0.00038 * waterFlowUp + 0.0000378 * waterFlowDown + 0.00593 * tempWater + 0.05506 * coolingRate + 0.1;
            rollerSpeed =   0.099682 * sectionCount 
                            - 0.01422 * thickness 
                            - 0.00295 * delta 
                            + 0.000383 * waterFlowUp 
                            + 0.0000378331 * waterFlowDown 
                            + 0.005926 * tempWater 
                            + 0.055057 * coolingRate;
        }
    }
}
