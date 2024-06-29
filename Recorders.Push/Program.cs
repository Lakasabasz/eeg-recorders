using push;
using SFML.Window;

var displayContextManager = new DisplayContextManager();
var window = new MainWindow(new VideoMode(640, 480), new MainWindowEventHandler(), displayContextManager);
window.Run();

Console.WriteLine("All done");