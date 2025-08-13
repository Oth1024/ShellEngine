using ShellEngine.Prelude.Components;
using ShellEngine.Prelude.Plugin;
using ShellEngine.Prelude.Protocol;
using ShellEngine.Prelude.Resource;
using ShellEngine.Prelude.World;

namespace ShellEngine.Prelude.App
{
    public class App
    {
        #region Constructor
        public App(AppConfig appConfig)
        {
            _world = new World.World();
        }
        #endregion

        #region Fields
        private bool _started = false;

        private AppConfig _appConfig;

        private Work _work;

        private readonly World.World _world;
        #endregion

        #region Methods
        public static App New(AppConfig appConfig = null)
        {
            if (appConfig == null)
            {
                appConfig = new AppConfig(60);
            }
            return new App(appConfig);
        }

        public void Start()
        {
            if (!_started)
            {
                _work = new Work(() =>
                {
                    _world.ReBuildWorld();
                });
                _work.Start();
                _started = true;
            }
        }

        public App SetConfig(AppConfig config)
        {
            _appConfig = config;
            _work.SetFps(config.FPS);
            return this;
        }

        public App AddSchedules(ScheduleType scheduleType = ScheduleType.Schedule, params (Action<List<IComponent>>, Type, Type, Type)[] schedules)
        {
            _world.AddSchedules(scheduleType, schedules);
            return this;
        }

        public App AddResources(params IResource[] resources)
        {
            _world.AddResources(resources);
            return this;
        }

        public App AddPlugins(params IPlugin[] plugins)
        {
            foreach (var plugin in plugins)
            {
                plugin.LoadPlugin(this);
            }
            return this;
        }

        public App AddComponents(params IComponent[] components)
        {
            _world.AddComponents(components);
            return this;
        }
        #endregion
    }
}
