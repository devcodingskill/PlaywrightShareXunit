using FluentAssertions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightXunitTest.TestAPIs
{
    public class TestAPI1
    {
        [Fact]
        public async Task TestAPIExample()
        {
            //create playwright instance
            var playwright = await Playwright.CreateAsync();
            //create request object
            var requestContext = await playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions()
            {
                BaseURL = "https://jsonplaceholder.typicode.com",
                //IgnoreHTTPSErrors = true, 
            });

            //get the response from use get method
            var response = await requestContext.GetAsync("/todos/1");
            //get value to check the response
            var responseValue = await response.JsonAsync();
            //parse the response value
            var jsonResponse = responseValue.Value;

            var id = jsonResponse.GetProperty("id").GetInt32();

            id.Should().Be(1);
            //check the response value
            //Assert.Equal(1, jsonResponse.GetProperty("id").GetInt32());
            //Assert.Equal(1, jsonResponse.GetProperty("userId").GetInt32());
            //Assert.Equal("delectus aut autem", jsonResponse.GetProperty("title").GetString());
            //Assert.False(jsonResponse.GetProperty("completed").GetBoolean());
        }
        [Fact]
        public async Task TestPostAPI()
        {
            var playwright = await Playwright.CreateAsync();
            //create request object
            var requestContext = await playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions()
            {
                BaseURL = "https://jsonplaceholder.typicode.com",
                //IgnoreHTTPSErrors = true, 
            });
            //
            var response = await requestContext.PostAsync("/posts", new APIRequestContextOptions()
            {
                //Anonymous types are used to create objects with properties
                DataObject = new
                {
                    title = "foo",
                    body = "bar",
                    userId = 1
                }
            });

            var jsonResponse = await response.JsonAsync();
        }
    }
}
