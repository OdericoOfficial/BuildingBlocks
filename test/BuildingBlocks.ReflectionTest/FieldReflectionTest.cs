using System.Reflection;

namespace BuildingBlocks.ReflectionTest
{
    public class FieldReflectionTest
    {
        internal class TestClass
        {
            private int _index = 1;
            private string _name = "1";
            private int[] _ints = [1, 2, 3];
            private string[] _strings = ["1", "2", "3"];

            public int Index => _index;
            public string Name => _name;
            public int[] Ints => _ints;
            public string[] Strings => _strings;
        }

        internal struct TestStruct
        {
            private int _index = 1;
            private string _name = "1";
            private int[] _ints = [1, 2, 3];
            private string[] _strings = ["1", "2", "3"];

            public TestStruct()
            {
            }

            public int Index => _index;
            public string Name => _name;
            public int[] Ints => _ints;
            public string[] Strings => _strings;
        }

        [Fact]
        public void TestClassReflection()
        {
            var test = new TestClass();
            var fields = typeof(TestClass).GetRuntimeFields();
            var index = fields.FirstOrDefault(item => item.Name == "_index");
            var name = fields.FirstOrDefault(item => item.Name == "_name");
            var ints = fields.FirstOrDefault(item => item.Name == "_ints");
            var strings = fields.FirstOrDefault(item => item.Name == "_strings");
            Assert.NotNull(index);
            Assert.NotNull(name);
            Assert.NotNull(ints);
            Assert.NotNull(strings);
            Assert.Equal(1, index.GetValue<TestClass, int>(ref test));
            Assert.Equal("1", name.GetValue<TestClass, string>(ref test));
            var intsTemp = ints.GetValue<TestClass, int[]>(ref test);
            Assert.NotNull(intsTemp);
            Assert.Equal([1, 2, 3], intsTemp);
            var stringsTemp = strings.GetValue<TestClass, string[]>(ref test);
            Assert.NotNull(stringsTemp);
            Assert.Equal(["1", "2", "3"], stringsTemp);
            intsTemp[0] = 4;
            stringsTemp[0] = "4";
            var intsTemp2 = ints.GetValue<TestClass, int[]>(ref test);
            Assert.NotNull(intsTemp2);
            Assert.Equal(intsTemp, intsTemp2);
            var stringsTemp2 = strings.GetValue<TestClass, string[]>(ref test);
            Assert.NotNull(stringsTemp2);
            Assert.Equal(stringsTemp, stringsTemp2);
            index.TrySetValue(ref test, 2);
            name.TrySetValue(ref test, "2");
            ints.TrySetValue(ref test, new int[] { 1, 2 ,3 });
            strings.TrySetValue(ref test, new string[] { "1", "2", "3" });
            Assert.Equal(2, index.GetValue<TestClass, int>(ref test));
            Assert.Equal("2", name.GetValue<TestClass, string>(ref test));
            var intsTemp3 = ints.GetValue<TestClass, int[]>(ref test);
            Assert.NotNull(intsTemp3);
            Assert.Equal([1, 2, 3], intsTemp3);
            Assert.NotEqual(intsTemp, intsTemp3);
            var stringsTemp3 = strings.GetValue<TestClass, string[]>(ref test);
            Assert.NotNull(stringsTemp3);
            Assert.Equal(["1", "2", "3"], stringsTemp3);
            Assert.NotEqual(stringsTemp, stringsTemp3);
        }

        [Fact]
        public void TestStructReflection()
        {
            var test = new TestStruct();
            var fields = typeof(TestStruct).GetRuntimeFields();
            var index = fields.FirstOrDefault(item => item.Name == "_index");
            var name = fields.FirstOrDefault(item => item.Name == "_name");
            var ints = fields.FirstOrDefault(item => item.Name == "_ints");
            var strings = fields.FirstOrDefault(item => item.Name == "_strings"); 
            Assert.NotNull(index);
            Assert.NotNull(name);
            Assert.NotNull(ints);
            Assert.NotNull(strings);
            Assert.Equal(1, index.GetValue<TestStruct, int>(ref test));
            Assert.Equal("1", name.GetValue<TestStruct, string>(ref test));
            var intsTemp = ints.GetValue<TestStruct, int[]>(ref test);
            Assert.NotNull(intsTemp);
            Assert.Equal([1, 2, 3], intsTemp);
            var stringsTemp = strings.GetValue<TestStruct, string[]>(ref test);
            Assert.NotNull(stringsTemp);
            Assert.Equal(["1", "2", "3"], stringsTemp);
            intsTemp[0] = 4;
            stringsTemp[0] = "4";
            var intsTemp2 = ints.GetValue<TestStruct, int[]>(ref test);
            Assert.NotNull(intsTemp2);
            Assert.Equal(intsTemp, intsTemp2);
            var stringsTemp2 = strings.GetValue<TestStruct, string[]>(ref test);
            Assert.NotNull(stringsTemp2);
            Assert.Equal(stringsTemp, stringsTemp2);
            index.TrySetValue(ref test, 2);
            name.TrySetValue(ref test, "2");
            ints.TrySetValue(ref test, new int[] { 1, 2, 3 });
            strings.TrySetValue(ref test, new string[] { "1", "2", "3" });
            Assert.Equal(2, index.GetValue<TestStruct, int>(ref test));
            Assert.Equal("2", name.GetValue<TestStruct, string>(ref test));
            var intsTemp3 = ints.GetValue<TestStruct, int[]>(ref test);
            Assert.NotNull(intsTemp3);
            Assert.Equal([1, 2, 3], intsTemp3);
            Assert.NotEqual(intsTemp, intsTemp3);
            var stringsTemp3 = strings.GetValue<TestStruct, string[]>(ref test);
            Assert.NotNull(stringsTemp3);
            Assert.Equal(["1", "2", "3"], stringsTemp3);
            Assert.NotEqual(stringsTemp, stringsTemp3);
        }
    }
}
