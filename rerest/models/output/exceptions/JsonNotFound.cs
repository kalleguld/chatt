namespace rerest.models.output.exceptions
{
    class JsonNotFound : JsonException
    {
        public JsonNotFound() : this("Specified Resource not found"){ }

        public JsonNotFound(string userMessage) : base(404, 2, userMessage, userMessage) { }
    }
}
