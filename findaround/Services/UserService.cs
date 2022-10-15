using System;
using System.Text;
using findaround.Helpers;
using findaround.Utilities;
using findaroundShared.Models;
using findaroundShared.Models.Dtos;
using MonkeyCache.FileStore;
using Newtonsoft.Json;

namespace findaround.Services
{
	public class UserService : IUserService
	{
		readonly HttpClient _client;

        public UserService()
		{
			_client = BackendUtilities.ProduceHttpClient();
		}

        public async Task<bool> RegisterUser(RegisterUserDto dto)
        {
            _client.SetBaseUrl();

            var content = this.GetRequestContent(dto);
            var response = new HttpResponseMessage();

            try
            {
                response = await _client.PostAsync("api/v1/findaround/users/register", content);
            }
            catch (HttpRequestException e)
            {
                return false;
            }

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<bool> LogInUser(LoginUserDto dto)
        {
            _client.SetBaseUrl();

            var content = this.GetRequestContent(dto);
            var response = new HttpResponseMessage();

            try
            {
                response = await _client.PostAsync("api/v1/findaround/users/login", content);
            }
            catch (HttpRequestException e)
            {
                return false;
            }

            if (!response.IsSuccessStatusCode)
                return false;

            var token = await response.Content.ReadAsStringAsync();
            BackendUtilities.SaveToken(token);
            return true;
        }

        public async Task<bool> LogOutUser()
        {
            _client.SetBaseUrl();
            _client.SetAuthenticationToken();

            var userId = UserHelpers.CurrentUser.Id;
            var response = new HttpResponseMessage();

            try
            {
                response = await _client.GetAsync($"api/v1/findaround/users/logout/{userId}");
            }
            catch (HttpRequestException e)
            {
                return false;
            }

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<User> GetUserBasicData(int userId)
        {
            _client.SetBaseUrl();
            _client.SetAuthenticationToken();

            var response = new HttpResponseMessage();

            try
            {
                response = await _client.GetAsync("api/v1/findaround/users/basicinfo");
            }
            catch (HttpRequestException e)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(responseContent);

            return user;
        }

        public async Task<User> GetBasicInfoAboutYourself()
        {
            _client.SetBaseUrl();
            _client.SetAuthenticationToken();

            var response = new HttpResponseMessage();

            try
            {
                response = await _client.GetAsync("api/v1/findaround/users/basicinfo/self");
            }
            catch (HttpRequestException e)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(responseContent);

            return user;
        }

        public async Task<string> GetUserLogin(int userId)
        {
            _client.SetBaseUrl();
            _client.SetAuthenticationToken();

            var response = new HttpResponseMessage();

            try
            {
                response = await _client.GetAsync($"api/v1/findaround/users/getlogin/{userId}");
            }
            catch (HttpRequestException e)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
                return null;

            var userLogin = await response.Content.ReadAsStringAsync();
            return userLogin;
        }
    }
}

