using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseModel
{
    public class RequestResult
    {
        public RequestResult()
        {
            Errors = new List<string>();
            Messages = new List<string>();
            Exceptions = new List<Exception>();
        }

        public bool Succeeded => !Errors.Any() && !Exceptions.Any();

        public List<string> Errors { get; set; }
        public List<string> Messages { get; set; }
        public List<Exception> Exceptions { get; set; }

        public void Add(RequestResult requestResult)
        {
            Errors.AddRange(requestResult.Errors);
            Messages.AddRange(requestResult.Messages);
            Exceptions.AddRange(requestResult.Exceptions);
        }

        public void Add(List<string> errors, List<string> messages, List<Exception> exceptions)
        {
            Errors.AddRange(errors);
            Messages.AddRange(messages);
            Exceptions.AddRange(exceptions);
        }
    }
    public class RequestResult<T> : RequestResult
    {
        public RequestResult()
        {
            Errors = new List<string>();
            Messages = new List<string>();
            Exceptions = new List<Exception>();
        }
        public T Model { get; set; }
        public void Add(RequestResult<T> requestResult)
        {
            Errors.AddRange(requestResult.Errors);
            Messages.AddRange(requestResult.Messages);
            Exceptions.AddRange(requestResult.Exceptions);
        }
    }
}
