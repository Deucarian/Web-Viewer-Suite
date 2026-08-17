# Changelog

## [0.1.7] - 2026-08-17

- Updated to Viewer Navigation 0.1.7 so the suite composes the canonical
  package-owned reference toolbar assets, presentation, and input behavior.
- Updated to Theming 1.0.5 so the reference toolbar resolves its runtime UI
  Toolkit styling and typography through the reusable Theming adapter.

## [0.1.6] - 2026-08-17

- Updated to Viewer Navigation 0.1.6 so the shared reference composition also
  owns the defensive WebGL `prefers-reduced-motion` gate. Every suite consumer
  now receives the same runtime navigation accessibility behavior by default.

## [0.1.5] - 2026-08-17

- Updated the suite to Viewer Navigation 0.1.5 so composed viewers can use the
  shared reference preset together with its canonical input blocker, mesh-bounds
  strategy, runtime-only animation policy, and reference theme composition.
- Updated to Theming 1.0.4 so the same composition installs the canonical dark
  Frosted Glass theme family and provider for navigation chrome.
- Updated the compiled reference sample to delegate navigation setup to that
  shared composition for both default and explicitly supplied settings. Custom
  settings now use `WithPreset`, preserving every shared policy and theme object.

## [0.1.4] - 2026-08-17

- Updated the suite to Viewer Navigation 0.1.4, restoring the polished
  Report Viewer icon toolbar, interactions, theming, and runtime tooltips for
  every composed viewer while keeping the optional view cube disabled.

## [0.1.3] - 2026-08-17

- Updated the implementation-free suite to Viewer Navigation 0.1.3, where the
  optional six-face view cube is disabled unless a consumer explicitly enables it.

## [0.1.2] - 2026-08-14

- Updated the implementation-free suite to the canonical Viewer Navigation reference
  preset used by Report Viewer, Activity Viewer, and the Web Viewer Template.

## [0.1.1] - 2026-08-14

- Aligned the Suite with the two-consumer-proven navigation and command-routing package versions.
- Declared Newtonsoft JSON directly for the compiled composition sample.

## [0.1.0] - 2026-08-13

- Added the implementation-free Web Viewer Suite dependency bundle.
- Added a compiled reference composition sample.
