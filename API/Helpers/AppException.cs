﻿using System;
using System.Globalization;
namespace API.Helpers
{
    public class AppException
    {
        public AppException(int statusCode, string message = null, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }

    }
}