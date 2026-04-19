using Application.Dtos.Results;

namespace GymPortal.Tests;

public class AccountResultTests
{
    [Fact]
    public void Ok_ShouldReturnSucceeded()
    {
        var result = AccountResult.Ok();
        Assert.True(result.Succeeded);
        Assert.Null(result.ErrorMessage);
        Assert.Null(result.Details);
    }

    [Fact]
    public void Failed_ShouldReturnNotSucceeded()
    {
        var result = AccountResult.Failed("Unable to save changes");
        Assert.False(result.Succeeded);
        Assert.Equal("Unable to save changes", result.ErrorMessage);
    }

    [Fact]
    public void NotFound_ShouldReturnDefaultMessage()
    {
        var result = AccountResult.NotFound();
        Assert.False(result.Succeeded);
        Assert.Equal("User not found", result.ErrorMessage);
    }
}