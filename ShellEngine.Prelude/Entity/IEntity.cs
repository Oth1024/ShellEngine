using ShellEngine.Prelude.Components;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngine.Prelude.Entity
{
    public interface IEntity
    {
        #region Properties
        public ConcurrentDictionary<Type, IComponent> Components { get; }
        #endregion

        #region Methods
        public uint GetEntityId();

        public bool TryGetComponent(Type queriedComponentType, out IComponent component);

        public bool TryGetComponent(Type queriedComponentType, Type includeComponentType, Type excludeComponentType, out IComponent component);

        public bool InsertComponent(IComponent component);
        #endregion
    }

    public static class EntityIdGen
    {
        private static uint _currentId = 0;

        public static uint GenId()
        {
            return _currentId++;
        }
    }
}
