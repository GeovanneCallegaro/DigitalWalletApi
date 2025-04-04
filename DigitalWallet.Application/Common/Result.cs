// <copyright file="Result.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DigitalWallet.Application.Common
{
    using System.Net;

    public abstract class Result
    {
        protected Result(bool isSuccess, HttpStatusCode statusCode, List<string>? message = null)
        {
            this.IsSuccess = isSuccess;
            this.Message = message ??[];
            this.HttpStatusCode = statusCode;
        }

        public bool IsSuccess { get; private set; }

        public List<string> Message { get; private set; }

        public HttpStatusCode HttpStatusCode { get; private set; }
    }
}
