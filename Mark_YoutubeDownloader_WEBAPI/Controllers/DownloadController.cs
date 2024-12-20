using MediaToolkit;
using MediaToolkit.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoLibrary;


namespace Mark_YoutubeDownloader_WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Convert(string url)
        {
            var youtube = YouTube.Default;
            var video = youtube.GetVideo(url);
            System.IO.File.WriteAllBytes(@"D:\Downloads\" + video.FullName, video.GetBytes());
            var inputFile = new MediaFile { Filename = @"D:\Downloads\" + video.FullName };
            var outputFile = new MediaFile { Filename = @"D:\Downloads\output.mp3" };
            try
            {
                using (var engine = new Engine())
                {
                    engine.GetMetadata(inputFile);
                    engine.Convert(inputFile, outputFile);
                }
                return Ok(new { file = "success" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
