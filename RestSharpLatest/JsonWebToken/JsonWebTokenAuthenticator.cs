using FluentAssertions;
using RestSharp;
using RestSharp.Authenticators;
using RestSharpLatest.JsonWebToken.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.JsonWebToken
{
    public class JsonWebTokenAuthenticator : AuthenticatorBase
    {
        // Base URL of the application
        private readonly string _baseUrl;
        // Username and password of the User
        private readonly User _userData;

        public JsonWebTokenAuthenticator(string baseUrl, User userData) : base("")
        {
            _baseUrl = baseUrl;
            _userData = userData;
        }

        protected override ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            Token = string.IsNullOrEmpty(Token) ? GetToken() : Token;
            return new ValueTask<Parameter>(new HeaderParameter(KnownHeaders.Authorization, Token));
        }

        private string GetToken()
        {
            using(var client = new RestClient(_baseUrl))
            {
                //Create a Request for the User Registration /users/sign-up
                var regResponse = client.PostJson<User>("users/sign-up", _userData);
                // 1. Create a RestRequest of Type POSt
                // 2. Ser the given object into JSON rep
                // 3. Send the Post request
                regResponse.Should().Be(System.Net.HttpStatusCode.OK);

                //Create a Second Request for authenticating the created user /users/authenticate

                var authResponse = client.PostJson<User, JwtToken>("users/authenticate", _userData);
                // 1. Create a RestRequest of Type POSt
                // 2. Ser the given object into JSON rep
                // 3. Send the Post request
                // 4. De-ser the response to a given type
                authResponse.Token.Should().NotBeNullOrEmpty();
                return $"Bearer {authResponse.Token}";
            }
        }
    }
}
