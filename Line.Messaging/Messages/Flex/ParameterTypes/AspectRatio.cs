using System;

namespace Line.Messaging
{
    public class AspectRatio
    {
        private readonly int _width;
        private readonly int _height;

        public AspectRatio(int width, int height)
        {
            if (width < 1 || width > 100000) { throw new ArgumentException($"The {nameof(width)} property must be in range from 1 to 100000.", nameof(width)); }
            if (height < 1 || height > 100000) { throw new ArgumentException($"The {nameof(height)} property must be in range from 1 to 100000.", nameof(height)); }
            if(height > width * 3) { throw new ArgumentException($"Cannot set the {nameof(height)} property to a value that is more than three times the value of the {nameof(width)} property."); }

            _width = width;
            _height = height;
        }
        public override string ToString()
        {
            return _width + ":" + _height;
        }
    }
}
