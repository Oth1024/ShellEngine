using ShellEngine.Prelude.App;
using ShellEngine.Prelude.Components;
using ShellEngine.Prelude.Plugin;

namespace ShellEngine.Demo;

public class Demo
{
    public static void Main(string[] args)
    {
        App.New()
            .AddPlugins(new TestPlugin())
            .Start();
    }

    public class TestComponent : IComponent
    {
        public TestComponent() { }
    }

    public class TestPlugin : IPlugin
    {
        public TestPlugin()
        {

        }

        public void LoadPlugin(App app)
        {
            app
                .AddComponents(new TestComponent())
                .AddSchedules(Prelude.Protocol.ScheduleType.Schedule, ((q) =>
            {
                TestSchedule(q);
            }, typeof(TestComponent), null, null));
        }

        public void TestSchedule(List<IComponent> testComponents)
        {
            Console.WriteLine("Test");
        }
    }
}