using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Deucarian.API.Configuration;
using Deucarian.API.Core;
using Deucarian.API.Models;
using Deucarian.CommandRouting;
using Deucarian.CommandRouting.WebGLIntegration;
using Deucarian.ObjectLoading;
using Deucarian.ObjectLoading.APIIntegration;
using Deucarian.ViewerAuthentication;
using Deucarian.ViewerNavigation;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Deucarian.WebViewerSuite.Samples.Stack
{
    public sealed class WebViewerStackSample :
        MonoBehaviour,
        IViewerAuthenticationHost
    {
        [SerializeField] private Camera viewerCamera;
        [SerializeField] private GameObject referenceModel;
        [SerializeField] private Transform modelParent;
        [SerializeField] private ViewerNavigationSettings navigationSettings;
        [SerializeField] private ApiClientConfig apiClientConfig;
        [SerializeField] private bool iframeMode;
        [SerializeField] private string parentOrigin = "http://localhost:8080";

        private ViewerNavigationInstaller navigation;
        private ObjectLoadingPipeline loadingPipeline;
        private WebGlCommandRoutingHost<WebViewerStackSample> commandHost;
        private ViewerAuthenticationSession authenticationSession;
        private IDisposable authenticationTargetRegistration;

        public IViewerAuthenticationSession AuthenticationSession =>
            authenticationSession;

        private void Start()
        {
            if (viewerCamera == null)
            {
                viewerCamera = Camera.main;
            }

            if (viewerCamera == null)
            {
                return;
            }

            authenticationSession = new ViewerAuthenticationSession();
            authenticationTargetRegistration =
                ViewerAuthenticationTargetRegistry.Register(
                    "web-viewer-suite-sample-" + GetInstanceID(),
                    "Web Viewer Suite Sample",
                    authenticationSession);
            IApiClient apiClient = ApiClientFactory.Create(
                apiClientConfig,
                authenticationSession.ApiAuthProvider);
            loadingPipeline = ApiObjectLoadingPipelineFactory.Create(
                apiClient,
                ApiAuthenticationRequirement.Disabled);
            ViewerNavigationReferenceCompositionProfile referenceComposition =
                ViewerNavigationReferenceComposition.Resolve();
            ViewerNavigationReferenceCompositionProfile effectiveComposition =
                navigationSettings == null
                    ? referenceComposition
                    : referenceComposition.WithPreset(navigationSettings);
            navigation = effectiveComposition.Compose(transform, viewerCamera);

            navigation.BeginReferenceLoad();
            if (referenceModel != null)
            {
                navigation.RegisterReference(referenceModel, true, true);
            }

            WebGlCommandTransportMode mode = iframeMode
                ? WebGlCommandTransportMode.ParentIframe
                : WebGlCommandTransportMode.DirectPage;
            string[] origins = iframeMode
                ? new[] { parentOrigin }
                : Array.Empty<string>();
            var transportOptions = new WebGlCommandTransportOptions(
                "web-viewer-suite-sample",
                mode,
                origins,
                iframeMode ? parentOrigin : null);
            commandHost = new WebGlCommandRoutingHost<WebViewerStackSample>(
                this,
                new ICommandHandler<WebViewerStackSample>[]
                {
                    new DescribeStackHandler(),
                    new LoadReferenceHandler(),
                    new ViewerAuthenticationCommandHandler<
                        WebViewerStackSample>()
                },
                transportOptions,
                gameObject);
            commandHost.Start();
        }

        private void OnDestroy()
        {
            authenticationTargetRegistration?.Dispose();
            authenticationTargetRegistration = null;
            commandHost?.Dispose();
            commandHost = null;
            loadingPipeline?.UnloadLast();
            loadingPipeline = null;
        }

        internal Task<CommandResult> LoadReferenceAsync(
            JObject payload,
            CancellationToken cancellationToken)
        {
            string sourceUrl = payload?.Value<string>("model_url");
            if (string.IsNullOrWhiteSpace(sourceUrl))
            {
                return Task.FromResult(CommandResult.Failure(
                    "invalid_model_url",
                    "model_url is required."));
            }

            var completion = new TaskCompletionSource<CommandResult>();
            ObjectLoadRequest request = ObjectLoadRequest.FromUrl(sourceUrl.Trim());
            request.Parent = modelParent != null ? modelParent : transform;
            request.CancellationToken = cancellationToken;
            request.DisplayName = "Web viewer suite sample model";
            StartCoroutine(loadingPipeline.LoadAsync(request, result =>
            {
                if (result == null || !result.Succeeded || result.Handle == null)
                {
                    completion.TrySetResult(CommandResult.Failure(
                        "model_load_failed",
                        result?.Message ?? "The model did not load."));
                    return;
                }

                GameObject root = result.Handle.InstantiatedObjects.Count > 0
                    ? result.Handle.InstantiatedObjects[0]
                    : null;
                if (root == null || !navigation.RegisterReference(root, true, true))
                {
                    completion.TrySetResult(CommandResult.Failure(
                        "model_has_no_bounds",
                        "The loaded model has no renderable bounds."));
                    return;
                }

                completion.TrySetResult(CommandResult.Success(
                    new JObject { ["loaded"] = true }));
            }));
            return completion.Task;
        }

        private sealed class DescribeStackHandler :
            ICommandHandler<WebViewerStackSample>
        {
            private static readonly IReadOnlyList<string> Names =
                new[] { "describe_viewer_stack" };

            public IReadOnlyList<string> CommandNames => Names;

            public Task<CommandResult> HandleAsync(
                CommandExecutionContext<WebViewerStackSample> context,
                CancellationToken cancellationToken)
            {
                return Task.FromResult(CommandResult.Success(new JObject
                {
                    ["transport"] = "webgl",
                    ["navigation"] = "viewer-navigation",
                    ["model_loading"] = "object-loading-api-integration",
                    ["authentication"] =
                        context.Application.AuthenticationSession.Status.Status
                            .ToString()
                }));
            }
        }

        private sealed class LoadReferenceHandler :
            ICommandHandler<WebViewerStackSample>
        {
            private static readonly IReadOnlyList<string> Names =
                new[] { "load_reference" };

            public IReadOnlyList<string> CommandNames => Names;

            public Task<CommandResult> HandleAsync(
                CommandExecutionContext<WebViewerStackSample> context,
                CancellationToken cancellationToken)
            {
                return context.Application.LoadReferenceAsync(
                    context.Command.Payload,
                    cancellationToken);
            }
        }
    }
}
