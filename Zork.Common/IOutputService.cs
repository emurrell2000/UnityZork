namespace Zork
{
    public interface IOutputService
    {
        void Clear();

        void Write(string value);
        void Write(object value);

        void WriteLine(string value);
        void WriteLine(object value);
    }
}
