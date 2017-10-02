using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Options;

namespace CoWorker.Net.FileUpload
{
    public class FileUploadHandler
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly HttpRequest _request;
        private readonly ILogger<FileUploadHandler> _logger;
        private readonly FormOptions _defaultFormOptions;

        public FileUploadHandler(
            IHttpContextAccessor accessor,
            ILogger<FileUploadHandler> logger,
            IOptions<FormOptions> options)
        {
            _accessor = accessor;
            this._request = _accessor.HttpContext.Request;
            _logger = logger;
            _defaultFormOptions = options.Value;
        }

        public async Task<string> Upload(string targetFilePath)
        {
            if (!MultipartRequestHelper.IsMultipartContentType(_request.ContentType))
            {
                await _request.HttpContext.SetBadRequest($"Expected a multipart request, but got {_request.ContentType}");
                return string.Empty;
            }
            var formAccumulator = new KeyValueAccumulator();

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(_request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, _request.Body);

            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                ContentDispositionHeaderValue contentDisposition;
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

                if (hasContentDispositionHeader)
                {
                    if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {

                        targetFilePath = Path.GetTempFileName();

                        using (var targetStream = File.Create(targetFilePath))
                        {
                            await section.Body.CopyToAsync(targetStream);

                            _logger.LogInformation($"Copied the uploaded file '{targetFilePath}'");
                        }
                    }
                    else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
                    {
                        var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                        var encoding = GetEncoding(section);
                        using (var streamReader = new StreamReader(section.Body,encoding,true,1024,true))
                        {
                            var value = await streamReader.ReadToEndAsync();
                            if (String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                            {
                                value = String.Empty;
                            }
                            formAccumulator.Append(key.Value, value);

                            if (formAccumulator.ValueCount > _defaultFormOptions.ValueCountLimit)
                            {
                                throw new InvalidDataException($"Form key count limit {_defaultFormOptions.ValueCountLimit} exceeded.");
                            }

                        }
                    }
                }
                section = await reader.ReadNextSectionAsync();
            }

            return targetFilePath;
        }

        private static Encoding GetEncoding(MultipartSection section)
        {
            MediaTypeHeaderValue mediaType;
            var hasMediaTypeHeader = MediaTypeHeaderValue.TryParse(section.ContentType, out mediaType);
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }
            return mediaType.Encoding;
        }
    }
}
