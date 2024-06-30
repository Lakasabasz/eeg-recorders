using Recorders.Push.EventHandlers;
using Recorders.Push.Window;
using SFML.Window;

var displayContextManager = new DisplayContextManager();
var desktop = VideoMode.DesktopMode;
var height = Math.Max(desktop.Height * 3/4, 480);
var width = height * 4 / 3;
var window = new MainWindow(new VideoMode(width, height), new MainWindowEventHandler(), displayContextManager);
window.Run();

Console.WriteLine("All done");