﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.BaseCellInterfaces
{
    public interface IClicked
    {
        void OnLeftMouseClick();

        void OnRightMouseClick();
    }
}