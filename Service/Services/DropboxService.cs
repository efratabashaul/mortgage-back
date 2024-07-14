using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

    namespace Service.Services
    {
        public class DropboxService
        {
        private readonly string _refreshToken;
        private readonly string _appKey;
        private readonly string _appSecret;
        private string _accessToken;
        private DateTime _accessTokenCreationTime;
        private readonly int _accessTokenLifetimeSeconds = 14400; // זמן חיי ה-token ב-שניות (4 שעות)

        public DropboxService(string accessToken, string refreshToken, string appKey, string appSecret)
        {
            _accessToken = accessToken;
            _refreshToken = refreshToken;
            _appKey = appKey;
            _appSecret = appSecret;
            _accessTokenCreationTime = DateTime.UtcNow;
        }
        public async Task<FileMetadata> UploadFileToDropbox(IFormFile file)
        {
            await RefreshAccessTokenIfNeeded();
            Console.WriteLine("after refresh");
            try
            {
                Console.WriteLine("in try in service");
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    using (var dropboxClient = new DropboxClient(_accessToken))
                    {
                        var fileMetadata = await dropboxClient.Files.UploadAsync(
                            "/mortgages-files/" + file.FileName,
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

        public async Task RefreshAccessTokenIfNeeded()
        {
            if ((DateTime.UtcNow - _accessTokenCreationTime).TotalSeconds >= _accessTokenLifetimeSeconds)
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.dropboxapi.com/oauth2/token");

                    var keyValues = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "refresh_token"),
                    new KeyValuePair<string, string>("refresh_token", _refreshToken),
                    new KeyValuePair<string, string>("client_id", _appKey),
                    new KeyValuePair<string, string>("client_secret", _appSecret),
                };

                    request.Content = new FormUrlEncodedContent(keyValues);

                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                    if (tokenResponse != null && tokenResponse.ContainsKey("access_token"))
                    {
                        _accessToken = tokenResponse["access_token"];
                        _accessTokenCreationTime = DateTime.UtcNow; // נעדכן את זמן יצירת ה-token
                    }
                }
            }
        }
    public async Task<List<FileMetadata>> UploadFilesToDropbox(List<IFormFile> files)
        {
            await RefreshAccessTokenIfNeeded();
            Console.WriteLine("in UploadFilesToDropboxin service");
            List<FileMetadata> uploadedFiles = new List<FileMetadata>();
            try
            {
                foreach (var file in files)
                {
                    //await CheckAndRefreshAccessToken(); // אמור לבצע רענון רק אם זו הפעם השנייה
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;

                        using (var dropboxClient = new DropboxClient(_accessToken))
                        {
                            var fileMetadata = await dropboxClient.Files.UploadAsync(
                                "/mortgages-files/" + file.FileName,
                                WriteMode.Overwrite.Instance,
                                body: memoryStream);

                            uploadedFiles.Add(fileMetadata);
                        }
                    }
                }
                return uploadedFiles;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading files: {ex.Message}");
                throw;
            }
        }
    }
}
