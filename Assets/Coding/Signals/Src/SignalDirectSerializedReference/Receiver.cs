﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Signals.SerializedReference
{
    public class Receiver : MonoBehaviour
    {
        public void LaunchEvent()
        {
            Debug.Log("Receiver Received EVENT!");
        }
    }
}