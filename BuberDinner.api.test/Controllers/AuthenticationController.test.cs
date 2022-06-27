using BuberDinner.api.Controllers;
using BuberDinner.application.Services.Authenticiation;
using BuberDinner.contracts.Authenticiation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;

namespace BuberDinner.api.test;

public class AuthenticationControllerTest
{
    public readonly Mock<IAuthenticationService> serviceMock = new();
    public void Setup(){
        // controller = new AuthenticationController(serviceMock);
    }


    [Fact]
    public async void Register_ValidCall()
    {
        // mock out IAuthenticationService.Register
        var request = new RegisterRequest(
            "FirstName",
            "LastName",
            "Email@email.com",
            "Password"
        );
        var mockResponse = new AuthenicationResult(
            System.Guid.NewGuid(),
            request.FirstName,
            request.LastName,
            request.Email,
            "Token"
        );
        serviceMock.Setup(x => x.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        )).Returns(Task.FromResult(mockResponse));
        
        var controller = new AuthenticationController(serviceMock.Object);
        var result = await controller.Register(request)  as OkObjectResult;
        Assert.Equal(200, result?.StatusCode);
        var expectedResult = new AuthenticationResponse(
         mockResponse.Id,
         mockResponse.FirstName,
         mockResponse.LastName,
         mockResponse.Email,
         mockResponse.Token
        );
        Assert.Equal(JsonConvert.SerializeObject(expectedResult), JsonConvert.SerializeObject(result?.Value));
        //Test mock is called with areguments
        // Test results match mock results.
        //   _dbServiceMock.Verify(x => x.SaveShoppingCartItem(It.IsAny<Product>()), Times.Never); 
    }
}