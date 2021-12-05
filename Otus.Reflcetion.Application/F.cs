namespace Otus.Reflcetion.Application
{
    public class F
    {
        private int field1, field2, field3, field4, field5;

        public int Prop1 => field1;
        public int Prop2 => field2;
        public int Prop3 => field3;
        public int Prop4 => field4;
        public int Prop5 => field5;

        public static F Get() => new()
        {
            field1 = 1,
            field2 = 2,
            field3 = 3,
            field4 = 4,
            field5 = 5
        };
    }
}