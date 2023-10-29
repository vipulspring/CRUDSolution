namespace CRUDTest
{
	public class UnitTest1
	{
		[Fact]
		public void Test1()
		{
			//Arrange
			MyMath mm = new MyMath();
			int input1 = 13,input2 = 14;
			int expected = 27;

			//Act
			int actual = mm.Add(input1, input2);

			//Assert
			Assert.Equal(expected, actual);
		}
	}
}