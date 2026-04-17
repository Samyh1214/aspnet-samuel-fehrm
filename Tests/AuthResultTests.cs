using Application.Dtos.Results;

namespace GymPortal.Tests;

public class AuthResultTests
{
    [Fact]
    public void Ok_ShouldReturnSucceeded()
    {
        var result = AuthResult.Ok();
        Assert.True(result.Succeeded);
        Assert.Null(result.ErrorMessage);
    }

    [Fact]
    public void Failed_ShouldReturnNotSucceeded()
    {
        var result = AuthResult.Failed("Invalid credentials");
        Assert.False(result.Succeeded);
        Assert.Equal("Invalid credentials", result.ErrorMessage);
    }
}