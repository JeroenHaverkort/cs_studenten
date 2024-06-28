

namespace EmmerOpdrachtTest
{
    public class BucketTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Fill_CorrectValues()
        {
            // Arrange
            Bucket bucket = new Bucket();

            // Act
            bucket.Fill(8);
            bucket.Fill(2);

            // Assert
            Assert.That(bucket.Content, Is.EqualTo(10));
        }

        [Test]
        public void Fill_IncorrectValues()
        {
            // Arrange
            Bucket bucket = new Bucket();

            // Act
            bucket.Fill(8);

            // Assert argumentoutofrangeexception
            Assert.Catch<ArgumentException>(() => bucket.Fill(-2));
        }

        [Test]
        public void Empty_CorrectValues()
        {
            // Arrange
            Bucket bucket = new Bucket();

            // Act
            bucket.Fill(8);
            bucket.Empty(2);

            // Assert
            Assert.That(bucket.Content, Is.EqualTo(6));
        }

        [Test]
        public void Empty_IncorrectValues()
        {
            // Arrange
            Bucket bucket = new Bucket();

            // Act
            bucket.Fill(8);

            // Assert
            Assert.That(() => bucket.Empty(-2), Throws.ArgumentException);
        }

        [Test]
        public void FillBucketWithBucket_CorrectValues()
        {
            // Arrange
            Bucket bucket = new Bucket();
            Bucket bucket2 = new Bucket();

            // Act
            bucket.Fill(8);
            bucket2.Fill(2);
            bucket.Fill(bucket2);

            // Assert
            Assert.That(bucket.Content, Is.EqualTo(10));
            Assert.That(bucket2.Content, Is.EqualTo(0));
        }

        [Test]
        public void FillBucketWithBucket_IncorrectValues()
        {
            // Arrange
            Bucket bucket = new Bucket();
            Bucket bucket2 = new Bucket();

            // Act
            bucket.Fill(8);
            bucket2.Fill(5);

            bucket.Fill(bucket2);
            // Assert

            Assert.That(bucket.Content.Equals(bucket.Capacity));
            Assert.That(bucket2.Content.Equals(0));
        }

        [Test]
        public void InitializeBucket_CorrectValues()
        {
            // Arrange
            Bucket bucket = new Bucket();

            // Act

            // Assert
            Assert.That(bucket.Content, Is.EqualTo(0));
            Assert.That(bucket.Capacity, Is.EqualTo(12));
        }

        [Test]
        public void InitializeBucket_IncorrectValues()
        {
            // Arrange

            // Act

            // Assert
            Assert.Catch<FalseCapacityException>(() => new Bucket(5));
        }

        [Test]
        public void OverflowBucket()
        {

           // Arrange
            Bucket bucket = new Bucket();

            // Act
            var eventRaised = false;
            bucket.Overflowing += (sender, e) =>
            {
                eventRaised = true;
            };

            bucket.Fill(2500);

            // Assert
            Assert.That(eventRaised, Is.True);
            Assert.That(bucket.Content, Is.EqualTo(bucket.Capacity));
        }

        [Test]
        public void FullBucket()
        {
            // Arrange
            Bucket bucket = new Bucket();

            var eventRaised = false;
            bucket.Full += (sender, e) =>
            {
                eventRaised = true;
            };

            // Act
            bucket.Fill(12);

            // Assert
            Assert.That(eventRaised, Is.True);
            Assert.That(bucket.Content, Is.EqualTo(bucket.Capacity));
        }

        [Test]
        public void CancelOverflow()
        {
            // Arrange
            Bucket bucket = new Bucket();
            var eventRaised = false;

            bucket.Overflowing += (sender, e) =>
            {
                e.Cancel = true;
                eventRaised = true;
            };

            // Act
            bucket.Fill(2500);

            // Assert
            Assert.That(eventRaised, Is.True);
            Assert.That(bucket.Content, Is.EqualTo(0));

        }
    }
}