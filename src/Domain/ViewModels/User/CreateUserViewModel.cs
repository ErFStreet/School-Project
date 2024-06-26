﻿namespace Domain.ViewModels.User;

public class CreateUserViewModel : object
{
    public CreateUserViewModel() : base()
    {
    }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string UserName { get; set; }

    public required string RoleName { get; set; }

    public int ClassId { get; set; }

    public required string Password { get; set; }
}
