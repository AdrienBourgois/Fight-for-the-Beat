using Level;
using UnityEngine;

namespace Entities
{
    public abstract class Entity : MonoBehaviour
    {
        public int Life { get; private set; }

        public int BeginSpaceIndex { get; protected set; }
        public int EndSpaceIndex { get; protected set; }
        public bool IsOnSpace(int _index) => _index >= BeginSpaceIndex && _index <= EndSpaceIndex;

        public void SetSpaceIndex(int _index, int _size = 1)
        {
            BeginSpaceIndex = _index;
            EndSpaceIndex = _index + _size;
            gameObject.transform.position = Vector3.right * BeginSpaceIndex;
        }

        protected virtual void Start()
        {
            SpaceManager.Instance.AddEntity(this);
        }

        protected virtual void OnDestroy()
        {
            SpaceManager.Instance.RemoveEntity(this);
        }

        protected Entity GetNextSpaceEntity() => SpaceManager.Instance.GetEntityOnSpace(EndSpaceIndex + 1);
        protected Entity GetPreviousSpaceEntity() => SpaceManager.Instance.GetEntityOnSpace(BeginSpaceIndex - 1);

        protected Entity GetNextRelativeSpaceEntity(int _delta) => SpaceManager.Instance.GetEntityOnSpace(EndSpaceIndex + _delta);
        protected Entity GetPreviousRelativeSpaceEntity(int _delta) => SpaceManager.Instance.GetEntityOnSpace(BeginSpaceIndex - _delta);

        public void Move(int _spaces)
        {
            BeginSpaceIndex += _spaces;
            EndSpaceIndex += _spaces;
            OnMove(_spaces);
        }

        protected virtual void OnMove(int _spaces) {}

        public void Hit(int _damages)
        {
            Life -= _damages;

            if (Life > 0)
                OnHit(_damages);
            else
                Die();
        }

        protected virtual void OnHit(int _damages) {}

        public void Die()
        {
            SpaceManager.Instance.RemoveEntity(this);
        }

        protected virtual void OnDie() {}
    }
}
