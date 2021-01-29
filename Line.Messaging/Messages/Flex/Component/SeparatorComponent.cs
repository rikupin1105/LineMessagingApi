using System;
using System.Collections.Generic;
using System.Text;

namespace Line.Messaging
{
    public class SeparatorComponent : IFlexComponent
    {
        public FlexComponentType Type => FlexComponentType.Separator;
        public Spacing? Margin { get; set; }
        public string Color { get; set; }
    }
}
