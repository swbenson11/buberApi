using BuberDinner.api.Controllers;
using BuberDinner.application.Services.Authentication;
using BuberDinner.contracts.Authenticiation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;

namespace BuberDinner.api.test;

public class AuthenticationControllerTest
{
    private readonly Mock<IAuthenticationService> serviceMock = new();
    private readonly AuthenticationResult authResult = new AuthenticationResult(
            System.Guid.NewGuid(),
            "FirstName",
            "LastName",
            "Email@email.com",
            "Token"
        );
    private readonly string password = "P@ssword";
    private AuthenticationController controller = null!;

    public AuthenticationControllerTest(){
        controller = new AuthenticationController(serviceMock.Object);
    }


    [Fact]
    public async void Register_ValidCall()
    {
        var request = new RegisterRequest(
            authResult.FirstName, authResult.LastName, authResult.Email, password
        );

        serviceMock.Setup(x => x.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        )).Returns(Task.FromResult(authResult));
        
        
        var result = await controller.Register(request)  as OkObjectResult;
        
        Assert.Equal(200, result?.StatusCode);
        var expectedResult = new AuthenticationResponse(
         authResult.Id,
         authResult.FirstName,
         authResult.LastName,
         authResult.Email,
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
        var request = new LoginRequest(authResult.Email, password);
        serviceMock.Setup(x => x.Login(request.Email, request.Password))
                .Returns(Task.FromResult(authResult));
        
        var result = await controller.Login(request)  as OkObjectResult;

        Assert.Equal(200, result?.StatusCode);
        var expectedResult = new AuthenticationResponse(
         authResult.Id,
         authResult.FirstName,
         authResult.LastName,
         authResult.Email,
         authResult.Token
        );
        Assert.Equal(JsonConvert.SerializeObject(expectedResult), JsonConvert.SerializeObject(result?.Value));
        serviceMock.Verify(x => 
            x.Login(request.Email, request.Password), 
            Times.Once
        );
    }
}