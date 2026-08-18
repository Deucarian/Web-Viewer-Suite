# Web Viewer Stack sample

Attach `WebViewerStackSample` to a scene object, assign a camera, and optionally
assign an already placed reference model. The sample composes public APIs from
the Suite members; it does not introduce an additional viewer runtime. Viewer
Navigation is created through its shared reference composition, so the sample
uses the canonical input blocker, mesh-bounds strategy, and animation policy
as well as the canonical dark Frosted Glass theme/provider even when a scene
supplies custom navigation settings. The custom settings path uses `WithPreset`
so none of those shared policy or theme identities are forked.

The browser host may send `describe_viewer_stack` or `load_reference` using the
canonical Command Routing envelope. `load_reference` accepts a `model_url` and
uses API-backed Object Loading before registering bounds and origin with Viewer
Navigation.

For a complete generic host, selection example, browser harness, and Build
Profiles, use the Deucarian Web Viewer Template package. It composes the Viewer
Rendering and Viewer Shell packages installed by this suite, so generic visual
settings and chrome are shared rather than copied into each application.
