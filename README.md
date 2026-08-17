# Deucarian Web Viewer Suite

`com.deucarian.web-viewer-suite` installs the proven reusable stack used by
Deucarian browser-hosted 3D viewers. It is deliberately implementation-free:
there is no Runtime or Editor assembly and no second viewer framework.

The suite members are the direct package dependencies and are intentionally
identical to the `suiteMembers` list in Package Registry. They provide:

- API-backed AssetBundle loading through API, Object Loading, and their integration;
- canonical command dispatch and the secure WebGL browser transport;
- Camera Navigation, Input System integration, Pointer Capture, UI, Theming,
  and Viewer Navigation with its toolbar and optional six-face view cube (off by default);
- sanitized operational diagnostics; and
- project-owned WebGL Build Profiles through Build Pipeline.

## Install

Stable:

```json
"com.deucarian.web-viewer-suite": "https://github.com/Deucarian/Web-Viewer-Suite.git#main"
```

Development:

```json
"com.deucarian.web-viewer-suite": "https://github.com/Deucarian/Web-Viewer-Suite.git#develop"
```

Import the **Web Viewer Stack** sample to inspect a small explicit composition
root. Application commands, DTOs, model-version resolution, selection policy,
and persistence remain application responsibilities.

For a runnable generic starting point, install
`com.deucarian.template.viewer.web` rather than adding implementation here.

## Boundary

The suite owns dependency composition and its reference sample only. It does
not own camera math, raw input, pointer-lock lifecycle, command routing, model
loading, a generic viewer core, or any Report/Activity business behavior.

## Validation

Run the shared Package Registry validator against this repository and run
`git diff --check`. The suite has no runtime tests because it ships no runtime
implementation.

## License

See [LICENSE.md](LICENSE.md).
