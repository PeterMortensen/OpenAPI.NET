﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. 

using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Interfaces;

namespace Microsoft.OpenApi.Services
{
    /// <summary>
    /// The walker to visit multiple Open API elements.
    /// </summary>
    public class OpenApiWalker
    {
        private readonly OpenApiVisitorBase _visitor;

        /// <summary>
        /// Initializes the <see cref="OpenApiWalker"/> class.
        /// </summary>
        public OpenApiWalker(OpenApiVisitorBase visitor)
        {
            _visitor = visitor;
        }

        /// <summary>
        /// Visits list of <see cref="OpenApiDocument"/> and child objects
        /// </summary>
        /// <param name="doc">OpenApiDocument to be walked</param>
        public void Walk(OpenApiDocument doc)
        {
            _visitor.Visit(doc);

            Walk(doc.Info);
            Walk(doc.Servers);
            Walk(doc.Paths);
            Walk(doc.Components);
            Walk(doc.ExternalDocs);
            Walk(doc.Tags);
            Walk(doc as IOpenApiExtensible);
        }

        /// <summary>
        /// Visits list of <see cref="OpenApiTag"/> and child objects
        /// </summary>
        /// <param name="tags"></param>
        internal void Walk(IList<OpenApiTag> tags)
        {
            _visitor.Visit(tags);

            foreach (var tag in tags)
            {
                Walk(tag);
            }
        }

        /// <summary>
        /// Visits <see cref="OpenApiExternalDocs"/> and child objects
        /// </summary>
        /// <param name="externalDocs"></param>
        internal void Walk(OpenApiExternalDocs externalDocs)
        {
            _visitor.Visit(externalDocs);
        }

        /// <summary>
        /// Visits <see cref="OpenApiComponents"/> and child objects
        /// </summary>
        /// <param name="components"></param>
        internal void Walk(OpenApiComponents components)
        {
            _visitor.Visit(components);
        }

        /// <summary>
        /// Visits <see cref="OpenApiPaths"/> and child objects
        /// </summary>
        /// <param name="paths"></param>
        internal void Walk(OpenApiPaths paths)
        {
            _visitor.Visit(paths);
            foreach (var pathItem in paths.Values)
            {
                Walk(pathItem);
            }
        }

        /// <summary>
        /// Visits list of  <see cref="OpenApiServer"/> and child objects
        /// </summary>
        /// <param name="servers"></param>
        internal void Walk(IList<OpenApiServer> servers)
        {
            _visitor.Visit(servers);

            // Visit Servers
            if (servers != null)
            {
                foreach (var server in servers)
                {
                    Walk(server);
                }
            }
        }

        /// <summary>
        /// Visits <see cref="OpenApiInfo"/> and child objects
        /// </summary>
        /// <param name="info"></param>
        internal void Walk(OpenApiInfo info)
        {
            _visitor.Visit(info);
            Walk(info.Contact);
            Walk(info.License);
            Walk(info as IOpenApiExtensible);
        }

        /// <summary>
        /// Visits dictionary of extensions
        /// </summary>
        /// <param name="openApiExtensible"></param>
        internal void Walk(IOpenApiExtensible openApiExtensible)
        {
            _visitor.Visit(openApiExtensible);
        }

        /// <summary>
        /// Visits <see cref="OpenApiLicense"/> and child objects
        /// </summary>
        /// <param name="license"></param>
        internal void Walk(OpenApiLicense license)
        {
            _visitor.Visit(license);
        }

        /// <summary>
        /// Visits <see cref="OpenApiContact"/> and child objects
        /// </summary>
        /// <param name="contact"></param>
        internal void Walk(OpenApiContact contact)
        {
            _visitor.Visit(contact);
        }

        /// <summary>
        /// Visits <see cref="OpenApiTag"/> and child objects
        /// </summary>
        /// <param name="tag"></param>
        internal void Walk(OpenApiTag tag)
        {
            _visitor.Visit(tag);
            _visitor.Visit(tag.ExternalDocs);
            _visitor.Visit(tag as IOpenApiExtensible);
        }

        /// <summary>
        /// Visits <see cref="OpenApiServer"/> and child objects
        /// </summary>
        /// <param name="server"></param>
        internal void Walk(OpenApiServer server)
        {
            _visitor.Visit(server);
            foreach (var variable in server.Variables.Values)
            {
                Walk(variable);
            }
            _visitor.Visit(server as IOpenApiExtensible);
        }

        /// <summary>
        /// Visits <see cref="OpenApiServerVariable"/> and child objects
        /// </summary>
        /// <param name="serverVariable"></param>
        internal void Walk(OpenApiServerVariable serverVariable)
        {
            _visitor.Visit(serverVariable);
            _visitor.Visit(serverVariable as IOpenApiExtensible);
        }

        /// <summary>
        /// Visits <see cref="OpenApiPathItem"/> and child objects
        /// </summary>
        /// <param name="pathItem"></param>
        internal void Walk(OpenApiPathItem pathItem)
        {
            _visitor.Visit(pathItem);

            Walk(pathItem.Operations);
            _visitor.Visit(pathItem as IOpenApiExtensible);
        }

        /// <summary>
        /// Visits dictionary of <see cref="OpenApiOperation"/>
        /// </summary>
        /// <param name="operations"></param>
        internal void Walk(IDictionary<OperationType, OpenApiOperation> operations)
        {
            _visitor.Visit(operations);
            foreach (var operation in operations.Values)
            {
                Walk(operation);
            }
        }

        /// <summary>
        /// Visits <see cref="OpenApiOperation"/> and child objects
        /// </summary>
        /// <param name="operation"></param>
        internal void Walk(OpenApiOperation operation)
        {
            _visitor.Visit(operation);

            Walk(operation.Parameters);
            Walk(operation.RequestBody);
            Walk(operation.Responses);
            Walk(operation as IOpenApiExtensible);
        }

        /// <summary>
        /// Visits list of <see cref="OpenApiParameter"/>
        /// </summary>
        /// <param name="parameters"></param>
        internal void Walk(IList<OpenApiParameter> parameters)
        {
            if (parameters != null)
            {
                _visitor.Visit(parameters);
                foreach (var parameter in parameters)
                {
                    Walk(parameter);
                }
            }
        }

        /// <summary>
        /// Visits <see cref="OpenApiParameter"/> and child objects
        /// </summary>
        /// <param name="parameter"></param>
        internal void Walk(OpenApiParameter parameter)
        {
            _visitor.Visit(parameter);
            Walk(parameter.Schema);
            Walk(parameter.Content);
            Walk(parameter as IOpenApiExtensible);
        }

        /// <summary>
        /// Visits <see cref="OpenApiResponses"/> and child objects
        /// </summary>
        /// <param name="responses"></param>
        internal void Walk(OpenApiResponses responses)
        {
            if (responses != null)
            {
                _visitor.Visit(responses);

                foreach (var response in responses.Values)
                {
                    Walk(response);
                }

                Walk(responses as IOpenApiExtensible);
            }
        }

        /// <summary>
        /// Visits <see cref="OpenApiResponse"/> and child objects
        /// </summary>
        /// <param name="response"></param>
        internal void Walk(OpenApiResponse response)
        {
            _visitor.Visit(response);
            Walk(response.Content);

            if (response.Links != null)
            {
                _visitor.Visit(response.Links);
                foreach (var link in response.Links.Values)
                {
                    _visitor.Visit(link);
                }
            }

            _visitor.Visit(response as IOpenApiExtensible);
        }

        /// <summary>
        /// Visits <see cref="OpenApiRequestBody"/> and child objects
        /// </summary>
        /// <param name="requestBody"></param>
        internal void Walk(OpenApiRequestBody requestBody)
        {
            if (requestBody != null)
            {
                _visitor.Visit(requestBody);

                if (requestBody.Content != null)
                {
                    Walk(requestBody.Content);
                }

                Walk(requestBody as IOpenApiExtensible);
            }
        }

        /// <summary>
        /// Visits dictionary of <see cref="OpenApiMediaType"/>
        /// </summary>
        /// <param name="content"></param>
        internal void Walk(IDictionary<string, OpenApiMediaType> content)
        {
            if (content == null)
            {
                return;
            }

            _visitor.Visit(content);
            foreach (var mediaType in content.Values)
            {
                Walk(mediaType);
            }
        }

        /// <summary>
        /// Visits <see cref="OpenApiMediaType"/> and child objects
        /// </summary>
        /// <param name="mediaType"></param>
        internal void Walk(OpenApiMediaType mediaType)
        {
            _visitor.Visit(mediaType);
            
            Walk(mediaType.Examples);
            Walk(mediaType.Schema);
            Walk(mediaType.Encoding);
            Walk(mediaType as IOpenApiExtensible);
        }

        /// <summary>
        /// Visits dictionary of <see cref="OpenApiEncoding"/>
        /// </summary>
        /// <param name="encoding"></param>
        internal void Walk(IDictionary<string, OpenApiEncoding> encoding)
        {
            foreach (var item in encoding.Values)
            {
                _visitor.Visit(item);
            }
        }

        /// <summary>
        /// Visits <see cref="OpenApiEncoding"/> and child objects
        /// </summary>
        /// <param name="encoding"></param>
        internal void Walk(OpenApiEncoding encoding)
        {
            _visitor.Visit(encoding);
            Walk(encoding as IOpenApiExtensible);
        }

        /// <summary>
        /// Visits <see cref="OpenApiSchema"/> and child objects
        /// </summary>
        /// <param name="schema"></param>
        internal void Walk(OpenApiSchema schema)
        {
            _visitor.Visit(schema);
            Walk(schema.ExternalDocs);
            Walk(schema as IOpenApiExtensible);
        }

        /// <summary>
        /// Visits dictionary of <see cref="OpenApiExample"/>
        /// </summary>
        /// <param name="examples"></param>
        internal void Walk(IDictionary<string,OpenApiExample> examples)
        {
            _visitor.Visit(examples);
            foreach (var example in examples.Values)
            {
                Walk(example);
            }
        }

        /// <summary>
        /// Visits <see cref="OpenApiExample"/> and child objects
        /// </summary>
        /// <param name="example"></param>
        internal void Walk(OpenApiExample example)
        {
            _visitor.Visit(example);
            Walk(example as IOpenApiExtensible);
        }

        /// <summary>
        /// Visits the list of <see cref="OpenApiExample"/> and child objects
        /// </summary>
        /// <param name="examples"></param>
        internal void Walk(IList<OpenApiExample> examples)
        {
            foreach (var item in examples)
            {
                _visitor.Visit(item);
            }
        }

        /// <summary>
        /// Visits <see cref="OpenApiOAuthFlows"/> and child objects
        /// </summary>
        /// <param name="flows"></param>
        internal void Walk(OpenApiOAuthFlows flows)
        {
            _visitor.Visit(flows);
            Walk(flows as IOpenApiExtensible);
        }

        /// <summary>
        /// Visits <see cref="OpenApiOAuthFlow"/> and child objects
        /// </summary>
        /// <param name="oAuthFlow"></param>
        internal void Walk(OpenApiOAuthFlow oAuthFlow)
        {
            _visitor.Visit(oAuthFlow);
            Walk(oAuthFlow as IOpenApiExtensible);
        }

        /// <summary>
        /// Visits dictionary of <see cref="OpenApiLink"/> and child objects
        /// </summary>
        /// <param name="links"></param>
        internal void Walk(IDictionary<string,OpenApiLink> links)
        {
            foreach (var item in links)
            {
                _visitor.Visit(item.Value);
            }
        }

        /// <summary>
        /// Visits <see cref="OpenApiLink"/> and child objects
        /// </summary>
        /// <param name="link"></param>
        internal void Walk(OpenApiLink link)
        {
            _visitor.Visit(link);
            Walk(link.Server);
            Walk(link as IOpenApiExtensible);
        }

        /// <summary>
        /// Visits <see cref="OpenApiHeader"/> and child objects
        /// </summary>
        /// <param name="header"></param>
        internal void Walk(OpenApiHeader header)
        {
            _visitor.Visit(header);
            Walk(header.Content);
            Walk(header.Example);
            Walk(header.Examples);
            Walk(header.Schema);
            Walk(header as IOpenApiExtensible);
        }

        /// <summary>
        /// Visits <see cref="OpenApiSecurityRequirement"/> and child objects
        /// </summary>
        /// <param name="securityRequirement"></param>
        internal void Walk(OpenApiSecurityRequirement securityRequirement)
        {
            _visitor.Visit(securityRequirement);
            Walk(securityRequirement as IOpenApiExtensible);
        }

        /// <summary>
        /// Visits <see cref="OpenApiSecurityScheme"/> and child objects
        /// </summary>
        /// <param name="securityScheme"></param>
        internal void Walk(OpenApiSecurityScheme securityScheme)
        {
            _visitor.Visit(securityScheme);
            Walk(securityScheme as IOpenApiExtensible);
        }

        /// <summary>
        /// Walk IOpenApiElement
        /// </summary>
        /// <param name="element"></param>
        internal void Walk(IOpenApiElement element)
        {
            switch(element)
            {
                case OpenApiDocument e: Walk(e); break;
                case OpenApiLicense e: Walk(e); break;
                case OpenApiInfo e: Walk(e); break;
                case OpenApiComponents e: Walk(e); break;
                case OpenApiContact e: Walk(e); break;
                case OpenApiCallback e: Walk(e); break;
                case OpenApiEncoding e: Walk(e); break;
                case OpenApiExample e: Walk(e); break;
                case IDictionary<string, OpenApiExample> e: Walk(e); break;
                case OpenApiExternalDocs e: Walk(e); break;
                case OpenApiHeader e: Walk(e); break;
                case OpenApiLink e: Walk(e); break;
                case IDictionary<string, OpenApiLink> e: Walk(e); break;
                case OpenApiMediaType e: Walk(e); break;
                case OpenApiOAuthFlows e: Walk(e); break;
                case OpenApiOAuthFlow e: Walk(e); break;
                case OpenApiOperation e: Walk(e); break;
                case OpenApiParameter e: Walk(e); break;
                case OpenApiRequestBody e: Walk(e); break;
                case OpenApiResponse e: Walk(e); break;
                case OpenApiSchema e: Walk(e); break;
                case OpenApiSecurityRequirement e: Walk(e); break;
                case OpenApiSecurityScheme e: Walk(e); break;
                case OpenApiServer e: Walk(e); break;
                case OpenApiServerVariable e: Walk(e); break;
                case OpenApiTag e: Walk(e); break;
                case IList<OpenApiTag> e: Walk(e); break;
                case OpenApiXml e: Walk(e); break;
                case IOpenApiExtensible e: Walk(e); break;
            }
        }

    }
}