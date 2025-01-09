using System.Reflection;
using System.Reflection.Unsafe;

namespace BuildingBlocks.ReflectionTest
{
    public class PropertyUnsafeTest
    {
        [Fact]
        public void TestClass()
        {
            var test = new TestClass();
            var fields = typeof(TestClass).GetRuntimeProperties();
            var index = fields.FirstOrDefault(item => item.Name == "Index");
            var name = fields.FirstOrDefault(item => item.Name == "Name");
            var ints = fields.FirstOrDefault(item => item.Name == "Ints");
            var strings = fields.FirstOrDefault(item => item.Name == "Strings");
            Assert.NotNull(index);
            Assert.NotNull(name);
            Assert.NotNull(ints);
            Assert.NotNull(strings);
            Assert.True(index.TryGetValueUnsafe<TestClass, int>(ref test, out var indexTemp));
            Assert.Equal(1, indexTemp);
            Assert.True(name.TryGetValueUnsafe<TestClass, string>(ref test, out var nameTemp));
            Assert.Equal("1", nameTemp);
            Assert.True(ints.TryGetValueUnsafe<TestClass, int[]>(ref test, out var intsTemp));
            Assert.NotNull(intsTemp);
            Assert.Equal([1, 2, 3], intsTemp);
            Assert.True(strings.TryGetValueUnsafe<TestClass, string[]>(ref test, out var stringsTemp));
            Assert.NotNull(stringsTemp);
            Assert.Equal(["1", "2", "3"], stringsTemp);
            intsTemp[0] = 4;
            stringsTemp[0] = "4";
            Assert.True(ints.TryGetValueUnsafe<TestClass, int[]>(ref test, out var intsTemp2));
            Assert.NotNull(intsTemp2);
            Assert.Equal(intsTemp, intsTemp2);
            Assert.True(strings.TryGetValueUnsafe<TestClass, string[]>(ref test, out var stringsTemp2));
            Assert.NotNull(stringsTemp2);
            Assert.Equal(stringsTemp, stringsTemp2);
            Assert.True(index.TrySetValueUnsafe(ref test, 2));
            Assert.True(name.TrySetValueUnsafe(ref test, "2"));
            Assert.True(ints.TrySetValueUnsafe<TestClass, int[]>(ref test, [1, 2, 3]));
            Assert.True(strings.TrySetValueUnsafe<TestClass, string[]>(ref test, ["1", "2", "3"]));
            Assert.True(index.TryGetValueUnsafe<TestClass, int>(ref test, out var indexTemp2));
            Assert.Equal(2, indexTemp2);
            Assert.True(name.TryGetValueUnsafe<TestClass, string>(ref test, out var nameTemp2));
            Assert.Equal("2", nameTemp2);
            Assert.True(ints.TryGetValueUnsafe<TestClass, int[]>(ref test, out var intsTemp3));
            Assert.NotNull(intsTemp3);
            Assert.Equal([1, 2, 3], intsTemp3);
            Assert.NotEqual(intsTemp, intsTemp3);
            Assert.True(strings.TryGetValueUnsafe<TestClass, string[]>(ref test, out var stringsTemp3));
            Assert.NotNull(stringsTemp3);
            Assert.Equal(["1", "2", "3"], stringsTemp3);
            Assert.NotEqual(stringsTemp, stringsTemp3);
        }

        [Fact]
        public void TestStruct()
        {
            var test = new TestStruct();
            var fields = typeof(TestStruct).GetRuntimeProperties();
            var index = fields.FirstOrDefault(item => item.Name == "Index");
            var name = fields.FirstOrDefault(item => item.Name == "Name");
            var ints = fields.FirstOrDefault(item => item.Name == "Ints");
            var strings = fields.FirstOrDefault(item => item.Name == "Strings");
            Assert.NotNull(index);
            Assert.NotNull(name);
            Assert.NotNull(ints);
            Assert.NotNull(strings);
            Assert.True(index.TryGetValueUnsafe<TestStruct, int>(ref test, out var indexTemp));
            Assert.Equal(1, indexTemp);
            Assert.True(name.TryGetValueUnsafe<TestStruct, string>(ref test, out var nameTemp));
            Assert.Equal("1", nameTemp);
            Assert.True(ints.TryGetValueUnsafe<TestStruct, int[]>(ref test, out var intsTemp));
            Assert.NotNull(intsTemp);
            Assert.Equal([1, 2, 3], intsTemp);
            Assert.True(strings.TryGetValueUnsafe<TestStruct, string[]>(ref test, out var stringsTemp));
            Assert.NotNull(stringsTemp);
            Assert.Equal(["1", "2", "3"], stringsTemp);
            intsTemp[0] = 4;
            stringsTemp[0] = "4";
            Assert.True(ints.TryGetValueUnsafe<TestStruct, int[]>(ref test, out var intsTemp2));
            Assert.NotNull(intsTemp2);
            Assert.Equal(intsTemp, intsTemp2);
            Assert.True(strings.TryGetValueUnsafe<TestStruct, string[]>(ref test, out var stringsTemp2));
            Assert.NotNull(stringsTemp2);
            Assert.Equal(stringsTemp, stringsTemp2);
            Assert.True(index.TrySetValueUnsafe(ref test, 2));
            Assert.True(name.TrySetValueUnsafe(ref test, "2"));
            Assert.True(ints.TrySetValueUnsafe<TestStruct, int[]>(ref test, [1, 2, 3]));
            Assert.True(strings.TrySetValueUnsafe<TestStruct, string[]>(ref test, ["1", "2", "3"]));
            Assert.True(index.TryGetValueUnsafe<TestStruct, int>(ref test, out var indexTemp2));
            Assert.Equal(2, indexTemp2);
            Assert.True(name.TryGetValueUnsafe<TestStruct, string>(ref test, out var nameTemp2));
            Assert.Equal("2", nameTemp2);
            Assert.True(ints.TryGetValueUnsafe<TestStruct, int[]>(ref test, out var intsTemp3));
            Assert.NotNull(intsTemp3);
            Assert.Equal([1, 2, 3], intsTemp3);
            Assert.NotEqual(intsTemp, intsTemp3);
            Assert.True(strings.TryGetValueUnsafe<TestStruct, string[]>(ref test, out var stringsTemp3));
            Assert.NotNull(stringsTemp3);
            Assert.Equal(["1", "2", "3"], stringsTemp3);
            Assert.NotEqual(stringsTemp, stringsTemp3);
        }
    }
}
