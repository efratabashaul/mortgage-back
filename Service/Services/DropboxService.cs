using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

    namespace Service.Services
    {
        public class DropboxService
        {
            private readonly string _appKey = "w8epehv4xr0nthm";
            private readonly string _appSecret = "u8blsxef2f61x4a";
            private string _accessToken = "sl.B4s-DoWRkYADhy2RmfcXL7Ku6oNzugEaJFmvskKl9B_Qu2121bp5hP618d1gfplfD8fdOqJwn02qVFU0UALVipeIKjTAlhHthpLfYvfROF4JzEUAbXRfzn_3x_bkKEevW3zJQf4OJ2bLSyku9Q";
            private string _refreshToken = "sl.B4s-DoWRkYADhy2RmfcXL7Ku6oNzugEaJFmvskKl9B_Qu2121bp5hP618d1gfplfD8fdOqJwn02qVFU0UALVipeIKjTAlhHthpLfYvfROF4JzEUAbXRfzn_3x_bkKEevW3zJQf4OJ2bLSyku9Q";
            private DateTime _accessTokenExpiration;


        int count = 0;
            public async Task<FileMetadata> UploadFileToDropbox(IFormFile file)
            {
            count++;
                try
                {
                if(count>1)
                    await CheckAndRefreshAccessToken();
                    Console.WriteLine( "in try in service");
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;

                        using (var dropboxClient = new DropboxClient(_accessToken))
                        {
                            var fileMetadata = await dropboxClient.Files.UploadAsync(
                                "/" + file.FileName,
                                WriteMode.Overwrite.Instance,
                                body: memoryStream);

                            return fileMetadata;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error uploading file: {ex.Message}");
                    throw;
                }
            }

            private async Task CheckAndRefreshAccessToken()
            {
                if (_accessToken == null || DateTime.UtcNow >= _accessTokenExpiration)
                {
                    await RefreshAccessToken();
                }
            }

            private async Task RefreshAccessToken()
            {
                var oauth2Token = await OAuth2TokenExchangeAsync();
                _accessToken = oauth2Token.AccessToken;
                _refreshToken = oauth2Token.RefreshToken;
                _accessTokenExpiration = DateTime.UtcNow.AddSeconds(oauth2Token.ExpiresIn);
            }

            private async Task<OAuth2Response> OAuth2TokenExchangeAsync()
            {
                Console.WriteLine("in OAuth2TokenExchangeAsync");
                var url = "https://api.dropbox.com/oauth2/token";
                var client = new HttpClient();
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("refresh_token", _refreshToken),
                new KeyValuePair<string, string>("client_id", _appKey),
                new KeyValuePair<string, string>("client_secret", _appSecret)
            });

                var response = await client.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response from Dropbox API: {responseBody}");
                }
                //Console.WriteLine("after post to oauth2, res="+response);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("after EnsureSuccessStatusCode");
                var responseString = await response.Content.ReadAsStringAsync();
                var oauth2Token = Newtonsoft.Json.JsonConvert.DeserializeObject<OAuth2Response>(responseString);

                return oauth2Token;
            }

            public class OAuth2Response
            {
                public string AccessToken { get; set; }
                public string TokenType { get; set; }
                public string RefreshToken { get; set; }
                public int ExpiresIn { get; set; }
            }
        }
    }





//{
//    public class DropboxService
//    {

//        private string _appKey = "w8epehv4xr0nthm";
//        private string _appSecret = "u8blsxef2f61x4a";



//        public async Task<FileMetadata> UploadFileToDropbox(IFormFile file)
//        {
//            using (var memoryStream = new MemoryStream())
//            {
//                await file.CopyToAsync(memoryStream);

//                using (var dropboxClient = new DropboxClient(_appKey, _appSecret))
//                {
//                    Console.WriteLine("in UploadFileToDropbox in service");
//                    // UploadAsync returns FileMetadata
//                    var fileMetadata = await dropboxClient.Files.UploadAsync(
//                        "/" + file.FileName,
//                        WriteMode.Overwrite.Instance,
//                        body: memoryStream);
//                    Console.WriteLine(fileMetadata.Name);

//                    return fileMetadata;
//                }
//            }
//        }


//        private readonly string _accessToken = "sl.B4vSFlDeBOXEwK3574OBmAACo-eSaJNBrjOTOfiYmvG57is9hsB2_viFnYSfa13vujzrkK3fcQYh1k16UwBe6M9nkD-wPczDm6EkQWuP-Vn5nxAqlGlR78J517qg-PSKnXTKg9Wjm2sSv2Zb2ie0F_Q";
//        public async Task<FileMetadata> UploadFileToDropbox2(IFormFile file)
//        {
//            using (var memoryStream = new MemoryStream())
//            {
//                await file.CopyToAsync(memoryStream);

//                using (var dropboxClient = new DropboxClient(_accessToken))
//                {
//                    // UploadAsync returns FileMetadata
//                    var fileMetadata = await dropboxClient.Files.UploadAsync(
//                        path: "/" + file.FileName,
//                        mode: WriteMode.Overwrite.Instance,
//                        body: memoryStream);

//                    return fileMetadata;
//                }
//            }
//        }
//        //public async Task<byte[]> DownloadFileFromDropbox(string fileName)
//        //{
//        //    using (var dropboxClient = new DropboxClient(_accessToken))
//        //    {
//        //        var response = await dropboxClient.Files.DownloadAsync("/" + fileName);
//        //        return await response.GetContentAsByteArrayAsync();
//        //    }
//        //}
//    }



//}