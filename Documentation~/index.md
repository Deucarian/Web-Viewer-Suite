# Web Viewer Suite architecture

The package is a Suite, not a runtime framework. The reference sample wires
public APIs from its members and intentionally owns no reusable production
behavior. Concrete viewers keep one application state owner and a thin
composition root, then add application-specific model resolution and commands.

The minimum lifecycle is:

1. create the API/Object Loading pipeline and Viewer Navigation installer;
2. start the WebGL `ICommandTransport` bridge;
3. load and place a model;
4. build the application's identifier index;
5. register reference bounds and capture the camera origin;
6. publish the application-level `viewer_ready` event.

Browser transport readiness is intentionally distinct from application
readiness. Visibility changes never call navigation framing APIs.
