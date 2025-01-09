namespace BuildingBlocks.ReflectionTest
{
    internal struct TestStruct
    {
        private int _index = 1;
        private string _name = "1";
        private int[] _ints = [1, 2, 3];
        private string[] _strings = ["1", "2", "3"];

        public TestStruct()
        {
        }

        public int Index
        {
            get => _index;
            set => _index = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public int[] Ints
        { 
            get => _ints;
            set => _ints = value;
        }

        public string[] Strings
        {
            get => _strings;
            set => _strings = value;
        }
    }
}
