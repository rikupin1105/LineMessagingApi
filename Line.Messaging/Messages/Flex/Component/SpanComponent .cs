using System;
using System.Collections.Generic;
using System.Text;

namespace Line.Messaging
{
    public class SpanComponent : IFlexComponent
    {
        public FlexComponentType Type => FlexComponentType.Span;

        public string Text { get; set; }
        public string Color { get; set; }
        public ComponentSize Size { get; set; }
        public Weight? Weight { get; set; }
        public TextStyle Style { get; set; }
        public TextDecoration Decoration { get; set; }

        public SpanComponent(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SpanComponent()
        {

        }
        
    }
    
}
