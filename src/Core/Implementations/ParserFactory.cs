using System;

namespace TRParsers.Core.Implementations
{
    public class ParserFactory : IParserFactory
    {
        private readonly IServiceProvider _provider;

        public ParserFactory(IServiceProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }
        
        public IParser<TIn, TOut> Get<TIn, TOut>()
        {
            var parser = (IParser<TIn, TOut>) _provider.GetService(typeof(IParser<TIn, TOut>));
            if (parser == null)
            {
                throw new NotSupportedException($"The parser IParser<{typeof(TIn).Name}, {typeof(TOut).Name}> is not registered.");
            }

            return parser;
        }
    }
}