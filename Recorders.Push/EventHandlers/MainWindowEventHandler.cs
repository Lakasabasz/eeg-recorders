﻿using SFML.Graphics;

namespace Recorders.Push.EventHandlers;

class MainWindowEventHandler: IEventHandler<RenderWindow>
{
    public void Register(RenderWindow window)
    {
        window.Closed += WindowOnClosed;
    }

    private void WindowOnClosed(object? sender, EventArgs e) => Tools.RequireNotNull<SFML.Window.Window>(sender).Close();
}