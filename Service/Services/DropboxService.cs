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
        private readonly int _accessTokenLifetimeSeconds = 14400; 

        public DropboxService(string accessToken, string refreshToken, string appKey, string appSecret)
        {
            _accessToken = accessToken;
            _refreshToken = refreshToken;
            _appKey = appKey;
            _appSecret = appSecret;
            _accessTokenCreationTime = DateTime.MinValue;
        }
        public async Task<FileMetadata> UploadFileToDropbox(IFormFile file)
        {
            await RefreshAccessTokenIfNeeded();
            Console.WriteLine("after refresh");
            try
            {
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
                    var responseContent = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Error occurred:"+ responseContent);
                        return;
                    }
                    response.EnsureSuccessStatusCode();

                    var tokenResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);

                    if (tokenResponse != null && tokenResponse.ContainsKey("access_token"))
                    {
                        _accessToken = tokenResponse["access_token"];
                        _accessTokenCreationTime = DateTime.UtcNow; 
                    }
                   _accessToken = tokenResponse["access_token"];
                }
            }
        }


        public async Task<List<FileMetadata>> UploadFilesToDropbox(List<IFormFile> files)
        {
            await RefreshAccessTokenIfNeeded();
            List<FileMetadata> uploadedFiles = new List<FileMetadata>();
            try
            {
                foreach (var file in files)
                {
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



        public class FileDownloadResult
        {
            public byte[] Content { get; set; }
            public string FileName { get; set; }
        }
        public async Task<FileDownloadResult> DownloadFileById(string id)
        {
            await RefreshAccessTokenIfNeeded();

            using (var dbx = new DropboxClient(_accessToken))
            {
                var list = await dbx.Files.ListFolderAsync(string.Empty);
                        var fileMetadata = list.Entries
                    .Where(i => i.IsFile && i.Name.StartsWith($"{id}_"))
                    .FirstOrDefault();
                if (fileMetadata != null)
                {
                    using (var response = await dbx.Files.DownloadAsync(fileMetadata.PathLower))
                    {
                        var content = await response.GetContentAsByteArrayAsync();
                        return new FileDownloadResult
                        {
                            Content = content,
                            FileName = fileMetadata.Name,
                        };
                    }
                }
            }

            return null;
        }

    }
}
