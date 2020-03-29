using System;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TRParsers.Core;
using TRParsers.Core.Implementations;
using TRParsers.DependencyInjection.Extensions;
using Xunit;

namespace TRParsers.DependencyInjection.Extentions.Tests
{
    public class ParsersExtensionsTests
    {
        [Fact]
        public void GivenExtensionsWhenAddParsersShouldRegisterFactory()
        {
            var servicesMock = new Mock<IServiceCollection>();
            servicesMock
                .Setup(x => x.Add(It.Is<ServiceDescriptor>(sd =>
                    sd.Lifetime == ServiceLifetime.Singleton
                    && sd.ServiceType == typeof(IParserFactory)
                    && sd.ImplementationType == typeof(ParserFactory))))
                .Verifiable();

            servicesMock.Object.AddParsers(typeof(ParsersExtensionsTests).Assembly);
            
            servicesMock.VerifyAll();
        }
        
        [Fact]
        public void GivenExtensionsWhenAddParsersShouldScanAssembliesForParsers()
        {
            var servicesMock = new Mock<IServiceCollection>();
            servicesMock
                .Setup(x => x.Add(It.Is<ServiceDescriptor>(sd =>
                    sd.Lifetime == ServiceLifetime.Singleton
                    && sd.ServiceType == typeof(IParser<string, int>)
                    && sd.ImplementationType == typeof(TestParserStub))))
                .Verifiable();
            servicesMock
                .Setup(x => x.Add(It.Is<ServiceDescriptor>(sd =>
                    sd.Lifetime == ServiceLifetime.Singleton
                    && sd.ServiceType == typeof(IParser<string, double>)
                    && sd.ImplementationType == typeof(TestParserStub))))
                .Verifiable();

            servicesMock.Object.AddParsers(typeof(ParsersExtensionsTests).Assembly);
            
            servicesMock.VerifyAll();
        }
        
        private class TestParserStub : IParser<string, int>, IParser<string, double>
        {
            public int Parser(string value, params object[] args)
            {
                throw new NotImplementedException();
            }

            double IParser<string, double>.Parser(string value, params object[] args)
            {
                throw new NotImplementedException();
            }
        }
    }
}
