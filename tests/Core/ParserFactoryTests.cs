using System;
using FluentAssertions;
using Moq;
using TRParsers.Core.Implementations;
using Xunit;

namespace TRParsers.Core.Tests
{
    public class ParserFactoryTests
    {
        [Fact]
        public void GivenParserFactoryWhenCreateAndServiceProviderIsNullShouldThrow()
        {
            Func<IParserFactory> action = () => new ParserFactory(null);
            
            action.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void GivenParserFactoryWhenGetAndResultIsNullShouldThrow()
        {
            var providerMock = new Mock<IServiceProvider>();
            providerMock.Setup(x => x.GetService(typeof(IParser<object, object>)))
                .Returns(null)
                .Verifiable();
            
            var factory = new ParserFactory(providerMock.Object);
            Func<IParser<object, object>> action = () => factory.Get<object, object>();
            
            action.Should().Throw<NotSupportedException>();
            providerMock.VerifyAll();
        }
        
        [Fact]
        public void GivenParserFactoryWhenGetShouldReturnsParser()
        {
            var objectParserMock = new Mock<IParser<object, object>>();
            var providerMock = new Mock<IServiceProvider>();
            providerMock.Setup(x => x.GetService(typeof(IParser<object, object>)))
                .Returns(objectParserMock.Object)
                .Verifiable();
            
            var factory = new ParserFactory(providerMock.Object);
            var objectParser = factory.Get<object, object>();

            objectParser.Should().BeAssignableTo<IParser<object, object>>();
            providerMock.VerifyAll();
        }
    }
}
