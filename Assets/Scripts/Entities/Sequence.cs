using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(menuName = "Sequence")]
    public class Sequence : ScriptableObject
    {
        public List<Action> Actions;
    }
}
