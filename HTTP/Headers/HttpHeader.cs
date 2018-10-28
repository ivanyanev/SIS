namespace HTTP.Headers
{
    public class HttpHeader
    {
        public HttpHeader(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public string Key { get; private set; } //без set-er по документация. Public set във видеото.

        public string Value { get; private set; } //без set-er по документация. Public set във видеото.

        public override string ToString()
        {
            return $"{this.Key}: {this.Value}";
        }
    }
}
