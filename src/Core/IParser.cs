namespace TRParsers.Core
{
    public interface IParser<in TIn, out TOut>
    {
        TOut Parse(TIn value, params object[] args);
    }
}
