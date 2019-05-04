using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class _10Mn2VNb : Steel
    {
        public _10Mn2VNb()
        {
            ratio = 2.29;
        }

        public override void Calculate(bool _isWaterFlowDownManual, double _waterFlowDownManual, bool _isWaterFlowUpManual, double _waterFlowUpManual)
        {
            double rollingEndTemp_2 = rollingEndTemp * rollingEndTemp;
            double thickness_2 = thickness * thickness;            
            double coolingRate_2 = coolingRate * coolingRate;

            double deltaAir =   -2.04893277896407 * rollingEndTemp 
                                + 88.7134801672352 * thickness 
                                + 4.02381247438084 * tempWater
                                - 0.110663638373992 * rollingEndTemp * thickness 
                                + 0.00236054905582028 * rollingEndTemp_2
                                - 0.663734074471114 * thickness_2 
                                + 0.00000106266084332336 * rollingEndTemp_2 * thickness_2 
                                + 0.0456512660658946 * rollingEndTemp_2 / (thickness * tempWater);

            tempBeginCooling = rollingEndTemp - deltaAir;
            delta = tempBeginCooling - tempEndCooling;
            
            double delta_2 = delta * delta;
            
            totalWaterFlow =    11.0599490767372 * tempWater
                                - 23.6384639383251 * thickness
                                + 11.105482094709 * sectionCount
                                + 0.206137077664584 * delta
                                + 75.6500782342274 * coolingRate
                                + 0.467673210485158 * thickness_2
                                - 0.29795157286537 * delta_2
                                - 2.18053593426642 * coolingRate_2
                                + 0.00318875009738527 * thickness * delta * coolingRate
                                + 0.00000200817630377281 * thickness_2 * delta_2 * coolingRate_2;

            GetWaterFlowUpDown(_isWaterFlowDownManual, _waterFlowDownManual, _isWaterFlowUpManual, _waterFlowUpManual);

            rollerSpeed =   0.084853 * sectionCount
                            - 0.01277 * thickness
                            - 0.00306 * delta 
                            + 0.000950445594427606 * waterFlowUp 
                            + 0.0000898116871356338 * waterFlowDown 
                            + 0.00227078612464705 * tempWater 
                            + 0.0578715495648475 * coolingRate;
        }
    }
}
