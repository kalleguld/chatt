namespace rerest.models.output.exceptions
{
    class JsonAccessDenied : JsonException
    {
        public JsonAccessDenied() : base(403, 8, 
            "Resource is not accessible to that token, or the resource doesn't exist") 
        { }
    }
}
