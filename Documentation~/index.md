# Web Viewer Suite architecture

The package is a Suite, not a runtime framework. The reference sample wires
public APIs from its members and intentionally owns no reusable production
behavior. Concrete viewers keep one application state owner and a thin
composition root, then add application-specific model resolution and commands.

The suite installs Viewer Rendering and Viewer Shell as the shared visual and
chrome baseline. The runnable Web Viewer Template composes those packages into
one complete generic viewer; this suite's intentionally small sample remains a
low-level composition example.

The minimum low-level lifecycle is:

1. create the API/Object Loading pipeline and Viewer Navigation installer;
2. start the WebGL `ICommandTransport` bridge;
3. load and place a model;
4. build the application's identifier index;
5. register reference bounds and capture the camera origin;
6. publish the application-level `viewer_ready` event.

Browser transport readiness is intentionally distinct from application
readiness. Visibility changes never call navigation framing APIs.

Viewer Rendering owns the reference camera, lighting, environment, URP, and
display policy. Viewer Shell owns generic status, diagnostics,
display-settings chrome, responsive placement, menu coordination, and the
authoritative theme provider passed into Viewer Navigation. Product-specific
selection, commands, media, markers, and backend adapters stay in the
application.
