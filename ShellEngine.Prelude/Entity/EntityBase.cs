using ShellEngine.Prelude.Components;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngine.Prelude.Entity
{
    public class EntityBase : IEntity
    {
        #region Fields
        private ConcurrentDictionary<Type, IComponent> _components = new();
        private readonly uint _entityId;
        #endregion

        #region Properties
        public ConcurrentDictionary<Type, IComponent> Components { get; private set; }
        #endregion

        #region Constructor
        public EntityBase(IEnumerable<IComponent> components)
        {
            foreach (var  component in components)
            {
                var type = component.GetType();
                _components[type] = component;
            }
            _entityId = EntityIdGen.GenId();
        }
        #endregion

        #region Methods
        public static IEntity SpawnEntityFromComponents(IEnumerable<IComponent> components)
        {
            return new EntityBase(components);
        }

        public bool TryGetComponent(Type type, out IComponent component)
        {
            if (_components.TryGetValue(type, out component))
            {
                return true;
            }
            component = null;
            return false;
        }

        public bool TryGetComponent(Type queriedComponentType, Type includeComponentType, Type excludeComponentType, out IComponent component)
        {
            if ((excludeComponentType == null || _components.ContainsKey(excludeComponentType))
                && _components.TryGetValue(queriedComponentType, out component)
                && (includeComponentType == null || _components.ContainsKey(includeComponentType)))
            {
                return true;
            }
            component = null;
            return false;
        }

        public bool InsertComponent(IComponent component)
        {
            try
            {
                var type = component.GetType();
                _components[type] = component;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public uint GetEntityId()
        {
            return _entityId;
        }
        #endregion
    }
}
