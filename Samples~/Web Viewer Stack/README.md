# Web Viewer Stack sample

Attach `WebViewerStackSample` to a scene object, assign a camera, and optionally
assign an already placed reference model. The sample composes public APIs from
the Suite members; it does not introduce an additional viewer runtime.

The browser host may send `describe_viewer_stack` or `load_reference` using the
canonical Command Routing envelope. `load_reference` accepts a `model_url` and
uses API-backed Object Loading before registering bounds and origin with Viewer
Navigation.

For a complete generic host, selection example, browser harness, and Build
Profiles, use the Deucarian Web Viewer Template package.
