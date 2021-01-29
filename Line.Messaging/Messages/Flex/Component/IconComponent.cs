namespace Line.Messaging
{
    public class IconComponent : IFlexComponent
    {
        public FlexComponentType Type => FlexComponentType.Icon;
        public string Url { get; set; }
        public Spacing? Margin { get; set; }
        public Position Position { get; set; }
        public string OffsetTop { get; set; }
        public string OffsetBottom { get; set; }
        public string OffsetStart { get; set; }
        public string OffsetEnd { get; set; }
        public ComponentSize? Size { get; set; }
        public AspectRatio AspectRatio { get; set; }
        public IconComponent(string url)
        {
            Url = url;
        }
        public IconComponent()
        {

        }
    }
}
