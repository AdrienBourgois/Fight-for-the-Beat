using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Level
{
    public class SpaceManager : MonoBehaviour
    {
        public static SpaceManager Instance { get; private set; }

        private readonly List<Entity> entities = new List<Entity>();

        private void Awake()
        {
            Instance = this;
        }

        public void AddEntity(Entity _entity)
        {
            entities.Add(_entity);
        }

        public void RemoveEntity(Entity _entity)
        {
            entities.Remove(_entity);
        }

        public Entity GetEntityOnSpace(int _index)
        {
            foreach (Entity entity in entities)
            {
                if (entity.IsOnSpace(_index))
                    return entity;
            }

            return null;
        }

        private void OnDrawGizmos()
        {
            for (int i = 0; i < 200; i++)
            {
                Gizmos.color = i % 2 == 1 ? Color.red : Color.blue;
                Gizmos.DrawLine(Vector3.right * (i - .5f), Vector3.right * ((i + 1) - .5f));
            }
        }
    }
}
