using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Entities
{
    public abstract class Action : ScriptableObject
    {
        abstract public void Execute(GameObject collector);
    }
}
