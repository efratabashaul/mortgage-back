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
            _accessTokenCreationTime = DateTime.MinValue; // מאתחל לזמן מאוד מוקדם כדי שהטוקן יתרענן תמיד עם יצירת האובייקט
        }
        public async Task<FileMetadata> UploadFileToDropbox(IFormFile file)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------");

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
           // Console.WriteLine("in");
            //if ((DateTime.UtcNow - _accessTokenCreationTime).TotalSeconds >= _accessTokenLifetimeSeconds)
            {
                Console.WriteLine("in if RefreshAccessTokenIfNeeded");
                //Console.WriteLine($"Refresh Token: {_refreshToken}");

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
                    Console.WriteLine("1******************************");

                    var response = await client.SendAsync(request);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    Console.WriteLine($"Response: {response}");
                    Console.WriteLine($"Response Content: {responseContent}");

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Error occurred:");
                        Console.WriteLine(responseContent);
                        return;
                    }
                    Console.WriteLine("2******************************");

                    response.EnsureSuccessStatusCode();

                    var tokenResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);

                    if (tokenResponse != null && tokenResponse.ContainsKey("access_token"))
                    {
                        _accessToken = tokenResponse["access_token"];
                        _accessTokenCreationTime = DateTime.UtcNow; // נעדכן את זמן יצירת ה-token
                    }
                    Console.WriteLine("_accessToken before:" + _accessToken);
                    _accessToken = tokenResponse["access_token"];
                    Console.WriteLine("_accessToken after:" + _accessToken);


                }
            }
        }


        public async Task<List<FileMetadata>> UploadFilesToDropbox(List<IFormFile> files)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
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




        //    public async Task<byte[]> DownloadFileById(string id)
        //    {
        //        //await RefreshAccessTokenIfNeeded();

        //        using (var dbx = new DropboxClient(_accessToken))
        //        {
        //            var list = await dbx.Files.ListFolderAsync(string.Empty);
        //            foreach (var item in list.Entries)
        //            {
        //                Console.WriteLine(item.Name);
        //            }
        //            var fileMetadata = list.Entries
        //                .Where(i => i.IsFile && i.Name.StartsWith(""+id + "_"))
        //                .FirstOrDefault();
        //            Console.WriteLine("in download service filemetadata="+fileMetadata.Name);
        //            if (fileMetadata != null)
        //            {
        //                Console.WriteLine("in if in %%%%%%%%%%%%%%%%%%%%%%%%%%");
        //                try
        //                {
        //                    using (var response = await dbx.Files.DownloadAsync(fileMetadata.PathLower))
        //                    {
        //                        Console.WriteLine("in if in using%%%%%%%%%%%%%%%%%%%%%%%%%%");
        //                        return  await response.GetContentAsByteArrayAsync();
        //                    }
        //                }
        //                catch (Exception x)
        //                {
        //                  string err=  x.Message;
        //                    throw;
        //                }

        //            }
        //        }

        //        return null;
        //    }
        //}
        public class FileDownloadResult
        {
            public byte[] Content { get; set; }
            public string FileName { get; set; }
        }
        public async Task<FileDownloadResult> DownloadFileById(string id)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------");

            await RefreshAccessTokenIfNeeded();

            using (var dbx = new DropboxClient(_accessToken))
            {
                var list = await dbx.Files.ListFolderAsync(string.Empty);
                foreach (var item in list.Entries)
                {
                    Console.WriteLine(item.Name);
                }

                        var fileMetadata = list.Entries
                    .Where(i => i.IsFile && i.Name.StartsWith($"{id}_"))
                    .FirstOrDefault();
                Console.WriteLine("in service, ");
                if (fileMetadata != null)
                {
                    using (var response = await dbx.Files.DownloadAsync(fileMetadata.PathLower))
                    {
                        Console.WriteLine("\n=====================================in\n");
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
