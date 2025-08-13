using ShellEngine.Prelude.Components;
using ShellEngine.Prelude.Entity;
using ShellEngine.Prelude.Plugin;
using ShellEngine.Prelude.Protocol;
using ShellEngine.Prelude.Resource;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngine.Prelude.World
{
    public class World
    {
        #region Fields
        #region Schedules
        private ConcurrentStack<(Action<List<IComponent>>, Type, Type, Type)> _preSchedules = new();
        private ConcurrentStack<(Action<List<IComponent>>, Type, Type, Type)> _schedules = new();
        private ConcurrentStack<(Action<List<IComponent>>, Type, Type, Type)> _postSchedule = new();

        private ConcurrentDictionary<Type, IResource> _resources = new();
        private ConcurrentDictionary<uint, IEntity> _entities = new();
        #endregion
        #endregion

        #region Methods
        public World()
        {

        }

        public World AddSchedules(ScheduleType scheduleType = ScheduleType.Schedule, params (Action<List<IComponent>>, Type, Type, Type)[] schedules)
        {
            switch (scheduleType)
            {
                case ScheduleType.PreSchedule:
                    foreach (var schedule in schedules)
                    {
                        _preSchedules.Push(schedule);
                    }
                    break;
                case ScheduleType.Schedule:
                    foreach (var schedule in schedules)
                    {
                        _schedules.Push(schedule);
                    }
                    break;
                case ScheduleType.PostSchedule:
                    foreach (var schedule in schedules)
                    {
                        _postSchedule.Push(schedule);
                    }
                    break;
            }
            return this;
        }

        public World AddComponents(params IComponent[] components)
        {
            if (components != null && components.Any())
            {
                var entity = EntityBase.SpawnEntityFromComponents(components.ToList());
                _entities[entity.GetEntityId()] = entity;
            }
            return this;
        }

        public World AddResources(IEnumerable<IResource> resources)
        {
            foreach (var res in resources)
            {
                var resType = res.GetType();
                if (_resources.ContainsKey(resType))
                {
                    throw new Exception($"Resource of type[{resType}] already exists!");
                }
                else
                {
                    _resources[resType] = res;
                }
            }
            return this;
        }

        public bool TryGetEntity(uint id, out IEntity? entity)
        {
            entity =  _entities.FirstOrDefault(x => x.Key == id).Value;
            return entity != null;
        }

        public bool TryGetEntity(Type componentType, IEntity? entity)
        {
            entity = _entities.FirstOrDefault(x => x.Value.TryGetComponent(componentType, out _)).Value;
            return entity != null;
        }

        public void ReBuildWorld()
        {
            foreach (var preSchedule in _preSchedules)
            {
                Execute(preSchedule);
            }
            foreach (var schedule in _schedules)
            {
                Execute(schedule);
            }
            foreach (var postSchedule in _postSchedule)
            {
                Execute(postSchedule);
            }
        }

        private bool Execute((Action<List<IComponent>>, Type, Type, Type) schedule)
        {
            try
            {
                List<IComponent> queries = new List<IComponent>();

                var queriedParamType = schedule.Item2;
                var includeType = schedule.Item2;
                var excludeType = schedule.Item3;
                foreach (var entity in _entities)
                {
                    if (entity.Value.TryGetComponent(queriedParamType, includeType, excludeType, out var component))
                    {
                        queries.Add(component);
                    }
                }
                    schedule.Item1.Invoke(queries);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
