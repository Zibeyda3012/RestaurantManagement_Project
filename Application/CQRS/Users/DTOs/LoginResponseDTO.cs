﻿namespace Application.CQRS.Users.DTOs;

public class LoginResponseDTO
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
