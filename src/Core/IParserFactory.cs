namespace TRParsers.Core
{
    public interface IParserFactory
    {
        IParser<TIn, TOut> Get<TIn, TOut>();
    }
}