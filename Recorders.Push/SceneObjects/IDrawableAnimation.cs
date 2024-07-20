using SFML.Graphics;
using SFML.System;

namespace Recorders.Push.SceneObjects;

interface IDrawableAnimation: IAnimatable, Drawable
{
    Vector2f Position { get; set; }
}