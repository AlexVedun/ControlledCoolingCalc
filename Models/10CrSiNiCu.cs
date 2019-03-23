﻿using System;
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
            delta = tempBeginCooling - tempEndCooling;
            //totalWaterFlow = -37.392 * tempWater + 30.26 * thickness + 39.79 * sectionCount - 1.7979 * delta + 29.861 * coolingRate + 30;
            totalWaterFlow = -37.392 * tempWater + 30.2604 * thickness + 39.79 * sectionCount - 1.7979 * delta + 29.861 * coolingRate + 30;
            GetWaterFlowUpDown(_isWaterFlowDownManual, _waterFlowDownManual, _isWaterFlowUpManual, _waterFlowUpManual);
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
            rollerSpeed = 0.09968 * sectionCount - 0.01422 * thickness - 0.00295 * delta + 0.00038 * waterFlowUp + 0.0000378 * waterFlowDown + 0.00593 * tempWater + 0.05506 * coolingRate + 0.1;
        }
    }
}
