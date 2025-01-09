using System.Reflection;
using System.Reflection.Unsafe;

namespace BuildingBlocks.ReflectionTest
{
    public class FieldUnsafeTest
    {
        [Fact]
        public void TestClass()
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
            Assert.Equal(1, index.GetValueUnsafe<TestClass, int>(ref test));
            Assert.Equal("1", name.GetValueUnsafe<TestClass, string>(ref test));
            var intsTemp = ints.GetValueUnsafe<TestClass, int[]>(ref test);
            Assert.NotNull(intsTemp);
            Assert.Equal([1, 2, 3], intsTemp);
            var stringsTemp = strings.GetValueUnsafe<TestClass, string[]>(ref test);
            Assert.NotNull(stringsTemp);
            Assert.Equal(["1", "2", "3"], stringsTemp);
            intsTemp[0] = 4;
            stringsTemp[0] = "4";
            var intsTemp2 = ints.GetValueUnsafe<TestClass, int[]>(ref test);
            Assert.NotNull(intsTemp2);
            Assert.Equal(intsTemp, intsTemp2);
            var stringsTemp2 = strings.GetValueUnsafe<TestClass, string[]>(ref test);
            Assert.NotNull(stringsTemp2);
            Assert.Equal(stringsTemp, stringsTemp2);
            Assert.True(index.TrySetValueUnsafe(ref test, 2));
            Assert.True(name.TrySetValueUnsafe(ref test, "2"));
            Assert.True(ints.TrySetValueUnsafe<TestClass, int[]>(ref test, [1, 2, 3]));
            Assert.True(strings.TrySetValueUnsafe<TestClass, string[]>(ref test, ["1", "2", "3"]));
            Assert.Equal(2, index.GetValueUnsafe<TestClass, int>(ref test));
            Assert.Equal("2", name.GetValueUnsafe<TestClass, string>(ref test));
            var intsTemp3 = ints.GetValueUnsafe<TestClass, int[]>(ref test);
            Assert.NotNull(intsTemp3);
            Assert.Equal([1, 2, 3], intsTemp3);
            Assert.NotEqual(intsTemp, intsTemp3);
            var stringsTemp3 = strings.GetValueUnsafe<TestClass, string[]>(ref test);
            Assert.NotNull(stringsTemp3);
            Assert.Equal(["1", "2", "3"], stringsTemp3);
            Assert.NotEqual(stringsTemp, stringsTemp3);
        }

        [Fact]
        public void TestStruct()
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
            Assert.Equal(1, index.GetValueUnsafe<TestStruct, int>(ref test));
            Assert.Equal("1", name.GetValueUnsafe<TestStruct, string>(ref test));
            var intsTemp = ints.GetValueUnsafe<TestStruct, int[]>(ref test);
            Assert.NotNull(intsTemp);
            Assert.Equal([1, 2, 3], intsTemp);
            var stringsTemp = strings.GetValueUnsafe<TestStruct, string[]>(ref test);
            Assert.NotNull(stringsTemp);
            Assert.Equal(["1", "2", "3"], stringsTemp);
            intsTemp[0] = 4;
            stringsTemp[0] = "4";
            var intsTemp2 = ints.GetValueUnsafe<TestStruct, int[]>(ref test);
            Assert.NotNull(intsTemp2);
            Assert.Equal(intsTemp, intsTemp2);
            var stringsTemp2 = strings.GetValueUnsafe<TestStruct, string[]>(ref test);
            Assert.NotNull(stringsTemp2);
            Assert.Equal(stringsTemp, stringsTemp2);
            Assert.True(index.TrySetValueUnsafe(ref test, 2));
            Assert.True(name.TrySetValueUnsafe(ref test, "2"));
            Assert.True(ints.TrySetValueUnsafe<TestStruct, int[]>(ref test, [1, 2, 3]));
            Assert.True(strings.TrySetValueUnsafe<TestStruct, string[]>(ref test, ["1", "2", "3"]));
            Assert.Equal(2, index.GetValueUnsafe<TestStruct, int>(ref test));
            Assert.Equal("2", name.GetValueUnsafe<TestStruct, string>(ref test));
            var intsTemp3 = ints.GetValueUnsafe<TestStruct, int[]>(ref test);
            Assert.NotNull(intsTemp3);
            Assert.Equal([1, 2, 3], intsTemp3);
            Assert.NotEqual(intsTemp, intsTemp3);
            var stringsTemp3 = strings.GetValueUnsafe<TestStruct, string[]>(ref test);
            Assert.NotNull(stringsTemp3);
            Assert.Equal(["1", "2", "3"], stringsTemp3);
            Assert.NotEqual(stringsTemp, stringsTemp3);
        }
    }
}
 