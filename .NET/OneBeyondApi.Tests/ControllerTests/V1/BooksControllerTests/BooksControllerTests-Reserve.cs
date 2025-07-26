using FluentAssertions;
using OneBeyondApi.Dto.V1.Books;
using OneBeyondApi.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneBeyondApi.Tests.ControllerTests.V1.BooksControllerTests
{
    public partial class BooksControllerTests : _BooksControllerTestsBase
    {
        private readonly static string _urlReserve = Constants.Urls.Books.Reserve;

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Reserve_Bad_Request(string title)
        {
            var client = CreateReserveHttpClient();
            var requestDto = new BookReservationRequestDto { Title = title };
            using var response = await client.PostAsync(_urlReserve, requestDto);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Reserve_Ok()
        {
            var client = CreateReserveHttpClient();
            var requestDto = new BookReservationRequestDto { Title = OneBeyondApi.Constants.Books.Titles.AgileProjectManagementAPrimer };
            var responseDto = await client.PostAsync<BookReservationResponseDto>(_urlReserve, requestDto);
            responseDto.Should().NotBeNull();
            responseDto.Result.Should().NotBeNullOrEmpty();
            responseDto.Result.Should().Contain("successfully");
        }

        [Fact]
        public async Task Reserve_Not_Ok_Available()
        {
            var client = CreateReserveHttpClient();
            var requestDto01 = new BookReservationRequestDto { Title = OneBeyondApi.Constants.Books.Titles.TheImportanceOfClay };
            var responseDto01 = await client.PostAsync<BookReservationResponseDto>(_urlReserve, requestDto01);
            responseDto01.Should().NotBeNull();
            responseDto01.Result.Should().NotBeNullOrEmpty();
            responseDto01.Result.Should().Contain("available");

            var requestDto02 = new BookReservationRequestDto { Title = OneBeyondApi.Constants.Books.Titles.RustDevelopmentCookbook };
            var responseDto02 = await client.PostAsync<BookReservationResponseDto>(_urlReserve, requestDto02);
            responseDto02.Should().NotBeNull();
            responseDto02.Result.Should().NotBeNullOrEmpty();
            responseDto02.Result.Should().Contain("available");
        }

        // Helpers

        private HttpClient CreateReserveHttpClient()
        {
            var client = CreateHttpClient();
            //client.
            return client;
        }
    }
}
