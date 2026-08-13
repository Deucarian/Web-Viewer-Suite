# Deucarian Web Viewer Suite Agent Notes

Package ID: `com.deucarian.web-viewer-suite`

Follow the canonical architecture and registry rules in Deucarian Package
Registry.

## Ownership

This Suite owns only its curated dependency set and reference sample. It must
remain implementation-free: never add a Runtime or Editor assembly, viewer
services, application DTOs, duplicated helpers, or product-specific behavior.

`package.json` direct Deucarian dependencies, registry dependencies, and
registry `suiteMembers` must remain identical.

## Policies

- Samples may demonstrate composition but must delegate behavior to member packages.
- Do not add direct `UnityEngine.Debug` calls.
- Do not add diagnostics providers: this Suite owns no operational state.
- Do not create editor surfaces.
- Work on `develop`; `main` is the stable distribution channel.

## Validation

Run the Package Registry validator and `git diff --check`.
