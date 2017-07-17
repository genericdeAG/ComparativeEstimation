using Xunit;

namespace CeDomain.Tests
{
    public class RequestHandlerTests
    {
        [Fact]
        public void Anmelden()
        {
            var state = new RequestHandler.State();
            var sut = new RequestHandler(null, state);

            sut.Register_Estimator_Client("1");
            Assert.Equal(1, state.EstimatorClientRegistrations.Count);

            sut.Register_Estimator_Client("2");
            Assert.Equal(2, state.EstimatorClientRegistrations.Count);
        }

        [Fact]
        public void Anmelden_idempotent()
        {
            var state = new RequestHandler.State();
            var sut = new RequestHandler(null, state);

            sut.Register_Estimator_Client("1");
            Assert.Equal(1, state.EstimatorClientRegistrations.Count);

            sut.Register_Estimator_Client("1");
            Assert.Equal(1, state.EstimatorClientRegistrations.Count);
        }
    }
}
