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
            //double rollingEndTemp_2 = rollingEndTemp * rollingEndTemp;
            double thickness_2 = thickness * thickness;            
            double coolingRate_2 = coolingRate * coolingRate;

            //double deltaAir =   -2.04893277896407 * rollingEndTemp 
            //                    + 88.7134801672352 * thickness 
            //                    + 4.02381247438084 * tempWater
            //                    - 0.110663638373992 * rollingEndTemp * thickness 
            //                    + 0.00236054905582028 * rollingEndTemp_2
            //                    - 0.663734074471114 * thickness_2 
            //                    + 0.00000106266084332336 * rollingEndTemp_2 * thickness_2 
            //                    + 0.0456512660658946 * rollingEndTemp_2 / (thickness * tempWater);

            //tempBeginCooling = rollingEndTemp - deltaAir;
            delta = tempBeginCooling - tempEndCooling;
            
            double delta_2 = delta * delta;
            
            totalWaterFlow =    9.08385161269387 * tempWater
                                - 15.7659533200746 * sectionCount
                                - 30.6124719500918 * thickness                                
                                + 8.30481098755947 * delta
                                + 47.0371235334781 * coolingRate
                                + 0.460835952254791 * thickness_2
                                - 0.0286111986067461 * delta_2
                                - 2.22013181366073 * coolingRate_2
                                + 0.0106746325396193 * thickness * delta * coolingRate
                                - 0.00000000443448754266383 * thickness_2 * delta_2 * coolingRate_2;

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
