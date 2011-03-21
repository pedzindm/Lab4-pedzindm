using System;
using NUnit.Framework;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestFixture()]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[SetUp()]
		public void SetUp()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[Test()]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}
        [Test()]
        public void TestLocationOfCarFromTheDatabase()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            String car = "Whale Rider";
            String anotherCar = "Raptor Wrangler";
            using (mocks.Record())
            {
                mockDatabase.getCarLocation(24);
                LastCall.Return(car);
                mockDatabase.getCarLocation(25);
                LastCall.Return(anotherCar);

            }
            var target = new Car(10);
            target.Database = mockDatabase;
            String result;
            result = target.getCarLocation(25);
            Assert.AreEqual(result, anotherCar);
            result = target.getCarLocation(24);
            Assert.AreEqual(result, car);


        }
        [Test()]
        public void TestThatCarGetsDatabaseMileage()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            int Miles = 3000;
            mockDatabase.Miles = Miles;
            var target = new Car(10);
            target.Database = mockDatabase;
            int mileCount = target.Mileage;
            Assert.AreEqual(mileCount, Miles);
        }
        [Test()]
        public void TestMotherObjects()
        {
            Assert.AreEqual("Saab 9-5 Sports Sedan",ObjectMother.Saab().Name); 
                       
         
        }

	}
}
