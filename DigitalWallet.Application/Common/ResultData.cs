// <copyright file="ResultData.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DigitalWallet.Application.Common
{
    using System.Net;

    public class ResultData<T> : Result
    {
        public ResultData(T? data, HttpStatusCode statusCode, bool isSuccess = true, List<string>? message = null)
            : base(isSuccess, statusCode, message)
        {
            this.Data = data;
        }

        public T? Data { get; set; }

        public static ResultData<T> Success(T data, HttpStatusCode statusCode) => new (data, statusCode);

        public static ResultData<T> Error(List<string> message, HttpStatusCode statusCode) => new (default, statusCode, false, message);
    }
}
