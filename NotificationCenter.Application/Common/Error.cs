using System.Net;

namespace NotificationCenter.Application.Common;

public sealed record Error(string Code, string Message, HttpStatusCode Status, object? Details = null)
{
    public static readonly Error None = new("", "", 0);
    public bool IsNone => string.IsNullOrEmpty(Code);
}