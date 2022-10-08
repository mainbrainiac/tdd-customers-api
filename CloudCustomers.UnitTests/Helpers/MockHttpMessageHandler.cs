using Moq.Protected;
using Newtonsoft.Json;

namespace CloudCustomers.UnitTests.Helpers;

internal static class MockHttpMessageHandler<T> 
{
  internal static Mock<HttpMessageHandler> SetupBasicGetResourceList(List<T> expectedResource)
  {
    var mockResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
    {
      Content = new StringContent(JsonConvert.SerializeObject(expectedResource))
    };

    mockResponse.Content.Headers.ContentType = 
      new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

    var handlerMock = new Mock<HttpMessageHandler>();

    handlerMock
      .Protected()
      .Setup<Task<HttpResponseMessage>>(
        "SendAsync",
        ItExpr.IsAny<HttpRequestMessage>(),
        ItExpr.IsAny<CancellationToken>()
      ).ReturnsAsync(mockResponse);
  
    return handlerMock;
  }
}