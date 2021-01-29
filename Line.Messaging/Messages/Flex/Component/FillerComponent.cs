namespace Line.Messaging
{
    public class FillerComponent : IFlexComponent
    {
        public FlexComponentType Type => FlexComponentType.Filler;
        public int? Flex { get; set; }
    }
}
