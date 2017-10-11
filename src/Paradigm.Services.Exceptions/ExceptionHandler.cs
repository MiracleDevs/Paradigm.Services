/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System;
using System.Collections.Generic;
using System.Resources;

namespace Paradigm.Services.Exceptions
{
    public class ExceptionHandler : IExceptionHandler
    {
        private List<IExceptionMatcher> Matchers { get; }

        private ResourceManager ResourceManager { get; }

        public ExceptionHandler(Type resourceType)
        {
            this.ResourceManager = new ResourceManager(resourceType);
            this.Matchers = new List<IExceptionMatcher>();
        }

        public void AddMatcher(IExceptionMatcher matcher)
        {
            this.Matchers.Add(matcher);
        }

        public Exception Handle(Exception ex)
        {
            var collectionException = ex as CollectionException;

            if (collectionException == null)
                return this.ProcessException(ex) ?? ex;

            foreach(var exception in collectionException.Exceptions)
            {
                var result = this.ProcessException(exception);
                
                if (result != null)
                {
                    return result;
                }
            }

            return ex;
        }

        private Exception ProcessException(Exception ex)
        {
            foreach (var matcher in this.Matchers)
            {
                var e = ex;

                while (e != null)
                {
                    if (!matcher.Match(e))
                    {
                        e = e.InnerException;
                        continue;
                    }

                    var message = matcher.GetNewMessage(this.ResourceManager, e);
                    return message != null ? new Exception(message, e) : e;
                }
            }

            return null;
        }
    }
}