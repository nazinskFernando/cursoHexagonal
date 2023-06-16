using NUnit.Framework;
using Domain.Entities;
using Domain.Enums;
using Action = Domain.Enums.Action;

namespace DomainTests.Bookings
{
    public class StateMachineTests
    {
        [SetUp]
        public void Setup() { }

        [Test] public void ShouldAlwaysStartWithCreatedStatus() 
        { 
            var booking = new Booking();
            Assert.AreEqual(Status.Created, booking.Status);
        
        }
        [Test] public void ShoudSetStatusToPaidWhenPayingForBookingWithCreatedStatus() {

            var booking = new Booking();
            booking.ChangeState(Action.Pay);

            Assert.AreEqual(booking.CurrentStatus, Status.Paid);

        }

        [Test]
        public void ShoudSetStatusToCancelWhenCancelingForBookingWithCreatedStatus()
        {

            var booking = new Booking();
            booking.ChangeState(Action.Cancel);

            Assert.AreEqual(booking.CurrentStatus, Status.Canceled);

        }

        [Test]
        public void ShoudSetStatusToFinishedWhenFinishingAPaidBooking()
        {

            var booking = new Booking();
            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Finish);

            Assert.AreEqual(booking.CurrentStatus, Status.Finished);

        }

        [Test]
        public void ShoudSetStatusToRefoundedWhenRefoundingAPaidBooking()
        {

            var booking = new Booking();
            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Refound);

            Assert.AreEqual(booking.CurrentStatus, Status.Refounded);

        }

        [Test]
        public void ShoudSetStatusToCreateddWhenReopeningACancelBooking()
        {

            var booking = new Booking();
            booking.ChangeState(Action.Cancel);
            booking.ChangeState(Action.Reopen);

            Assert.AreEqual(booking.CurrentStatus, Status.Created);

        }

        [Test]
        public void ShouldNotChangeStatusWhenRefoundingABookingWithCreatedStatus()
        {

            var booking = new Booking();
            booking.ChangeState(Action.Refound);

            Assert.AreEqual(booking.CurrentStatus, Status.Created);

        }

        [Test]
        public void ShouldNotChangeStatusWhenRefoundingABookingWithFinishedStatus()
        {

            var booking = new Booking();
            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Finish);
            booking.ChangeState(Action.Refound);

            Assert.AreEqual(booking.CurrentStatus, Status.Finished);

        }
    }
}
