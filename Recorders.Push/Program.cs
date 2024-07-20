using Recorders.Push;
using Recorders.Push.AsyncTasks;
using Recorders.Push.EventDispatcher;
using Recorders.Push.EventHandlers;
using Recorders.Push.Window;
using SFML.Window;

GlobalData.Init();

var displayContextManager = new DisplayContextManager(new DisplayContextManagerEventHandler());
var eventDispatcher = new RenderWindowToDisplayContextManager();
var desktop = VideoMode.DesktopMode;
var height = Math.Max(desktop.Height * 3/4, 480);
var width = height * 4 / 3;
var window = new MainWindow(new VideoMode(width, height), new MainWindowEventHandler(), displayContextManager,
    eventDispatcher);
var apiConnector = new ApiConnectionMonitor().Begin();
apiConnector.Task.Start();

window.Run();

apiConnector.Cancel.Cancel();

Console.WriteLine("All done");