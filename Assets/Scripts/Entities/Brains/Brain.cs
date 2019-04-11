using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public abstract class Brain : ScriptableObject
    {
        abstract public void Controll(GameObject collector);
    }
}
