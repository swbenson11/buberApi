using BuberDinner.api.Controllers;
using BuberDinner.application.Common.Errors;
using BuberDinner.application.Services.Authentication;
using BuberDinner.contracts.Authentication;
using BuberDinner.domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using OneOf;

namespace BuberDinner.api.test;

public class AuthenticationControllerTest
{
    private readonly Mock<IAuthenticationService> serviceMock = new();
    private static readonly User user = new User{
            Id = System.Guid.NewGuid(),
            FirstName ="FirstName",
            LastName = "LastName",
            Email ="Email@email.com"
        };
    private readonly AuthenticationResult authResult = new AuthenticationResult(user, "Token");
    private readonly string password = "P@ssword";
    private AuthenticationController controller = null!;

    public AuthenticationControllerTest(){
        controller = new AuthenticationController(serviceMock.Object);
    }


    [Fact]
    public async void Register_ValidCall()
    {
        var request = new RegisterRequest(
            user.FirstName, user.LastName, user.Email, password
        );

        serviceMock.Setup(x => x.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        )).Returns(Task.FromResult((OneOf<AuthenticationResult, IProcessedError>)authResult));
        
        
        var result = await controller.Register(request)  as OkObjectResult;
        
        Assert.Equal(200, result?.StatusCode);
        var expectedResult = new AuthenticationResponse(
         authResult.User.Id,
         authResult.User.FirstName,
         authResult.User.LastName,
         authResult.User.Email,
         authResult.Token
        );
        Assert.Equal(JsonConvert.SerializeObject(expectedResult), JsonConvert.SerializeObject(result?.Value));
        serviceMock.Verify(x => 
            x.Register(            
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password), 
            Times.Once
        );
        //   _dbServiceMock.Verify(x => x.SaveShoppingCartItem(It.IsAny<Product>()), Times.Never); 
    }


    [Fact]
    public async void Login_ValidCall()
    {
        var request = new LoginRequest(authResult.User.Email, password);
        serviceMock.Setup(x => x.Login(request.Email, request.Password))
                .Returns(Task.FromResult(authResult));
        
        var result = await controller.Login(request)  as OkObjectResult;

        Assert.Equal(200, result?.StatusCode);
        var expectedResult = new AuthenticationResponse(
         authResult.User.Id,
         authResult.User.FirstName,
         authResult.User.LastName,
         authResult.User.Email,
         authResult.Token
        );
        Assert.Equal(JsonConvert.SerializeObject(expectedResult), JsonConvert.SerializeObject(result?.Value));
        serviceMock.Verify(x => 
            x.Login(request.Email, request.Password), 
            Times.Once
        );
    }
}