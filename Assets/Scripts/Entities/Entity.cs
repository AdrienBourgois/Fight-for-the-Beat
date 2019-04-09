using Level;
using UnityEngine;

namespace Entities
{
    public abstract class Entity : MonoBehaviour
    {
        public int BeginSpaceIndex { get; protected set; }
        public int EndSpaceIndex { get; protected set; }
        public bool IsOnSpace(int _index) => _index >= BeginSpaceIndex && _index <= EndSpaceIndex;

        protected virtual void Awake()
        {
            SpaceManager.Instance.AddEntity(this);
        }

        protected virtual void OnDestroy()
        {
            SpaceManager.Instance.RemoveEntity(this);
        }

        protected Entity GetNextSpaceEntity() => SpaceManager.Instance.GetEntityOnSpace(EndSpaceIndex + 1);
        protected Entity GetPreviousSpaceEntity() => SpaceManager.Instance.GetEntityOnSpace(BeginSpaceIndex - 1);
    }
}
