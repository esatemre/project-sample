namespace TheProject.Core
{
    using System.Collections.Generic;

    public class Response
    {
        public List<string> Warnings { get; set; }
        public List<string> Errors { get; set; }
        public bool HasError { get; set; }
        public Response()
        {
            HasError = false;
            Errors = new List<string>();
            Warnings = new List<string>();
        }
        
    }

    public class Response<T> : Response
    {
        public T Value { get; set; }

        public Response() : base() { }

        public Response(T val) : base() { Value = val; }

    }
}
