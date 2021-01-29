using System;
using System.Collections.Generic;
using System.Text;

namespace Line.Messaging
{
    public class CarouselContainer : IFlexContainer
    {
        public FlexContainerType Type => FlexContainerType.Carousel;
        public IList<BubbleContainer> Contents { get; set; } = new List<BubbleContainer>();
        public CarouselContainer()
        {
        }
    }
}
