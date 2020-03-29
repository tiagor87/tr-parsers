namespace TRParsers.Core
{
    public interface IParser<in TIn, out TOut>
    {
        TOut Parser(TIn value, params object[] args);
    }
}
