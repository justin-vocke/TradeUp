﻿using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Buffers;

namespace TradeUp.Api.OpenApi
{
    public sealed class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var openApiInfo = new OpenApiInfo
            {
                Title = $"TradeUp.Api v{description.ApiVersion}",
                Version = description.ApiVersion.ToString()
            };

            if (description.IsDeprecated)
            {
                openApiInfo.Description += " This API version has been deprecated.";
            }

            return openApiInfo;
        }
    }
}
