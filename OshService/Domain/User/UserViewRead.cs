﻿namespace OshService.Domain.User;

public class UserViewRead
{
    public long Id { get; set; }

    public required string Login { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }
}
