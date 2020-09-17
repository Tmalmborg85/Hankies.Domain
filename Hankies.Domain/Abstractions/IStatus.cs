using System;
using System.Collections.Generic;

namespace Hankies.Domain.Models.Abstractions
{
    public interface IStatus<T>
    {
        /// <summary>
        /// A list of errors 
        /// </summary>
        public IList<string> Errors { get; }

        /// <summary>
        /// If a response is required success will be set to false if a
        /// responce object is never set. 
        /// </summary>
        bool ResponseIsRequired { get; }

        /// <summary>
        /// An optinal object to respond with.
        /// </summary>
        public T ResponseObject { get; }

        /// <summary>
        /// Adds a distinct error to this status
        /// </summary>
        /// <param name="errorMessage">The error message to add</param>
        void AddError(string errorMessage);

        /// <summary>
        /// Adds an expection by an exception's message or by 'Unnamed Error'
        /// </summary>
        /// <param name="exception"></param>
        void AddException(Exception exception);

        /// <summary>
        /// Sets the response object. 
        /// </summary>
        /// <param name="responseObject"></param>
        void RespondWith(T responseObject);

        /// <summary>
        /// Indicates if the status is successfull or not. 
        /// </summary>
        public bool IsSuccess();

        /// <summary>
        /// Concatination of all error messages, if any. 
        /// </summary>
        public string ErrorMessage { get; }
    }
}
