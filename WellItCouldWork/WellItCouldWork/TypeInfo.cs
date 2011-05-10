namespace WellItCouldWork
{
    public class TypeInfo
    {
        public string Name { get; private set; }

        public TypeInfo(string className)
        {
            Name = className;
        }

        public static implicit operator TypeInfo(string className)
        {
            return new TypeInfo(className);
        }

    }
}