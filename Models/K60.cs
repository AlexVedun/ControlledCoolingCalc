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
            //double deltaAir =   0.088928 * rollingEndTemp 
            //                    - 1.40144 * thickness 
            //                    - 0.15846 * tempWater;
            //tempBeginCooling = rollingEndTemp - deltaAir;
            delta = tempBeginCooling - tempEndCooling;
            totalWaterFlow =    37.55652 * tempWater 
                                + 36.21689 * thickness 
                                - 60.5608 * sectionCount 
                                + 0.019801 * delta 
                                + 23.10946 * coolingRate;
            GetWaterFlowUpDown(_isWaterFlowDownManual, _waterFlowDownManual, _isWaterFlowUpManual, _waterFlowUpManual);
            //rollerSpeed = 0.1609 * sectionCount - 0.01448 * thickness - 0.00747 * delta - 0.00045 * waterFlowUp + 0.000719 * waterFlowDown - 0.01243 * tempWater + 0.06658 * coolingRate;
            rollerSpeed =   0.160851 * sectionCount 
                            - 0.01448 * thickness 
                            - 0.00747 * delta 
                            - 0.00045 * waterFlowUp 
                            + 0.000719 * waterFlowDown 
                            - 0.01243 * tempWater 
                            + 0.066583 * coolingRate;
        }
    }
}
