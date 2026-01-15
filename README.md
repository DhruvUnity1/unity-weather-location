# Native Toast Plugin for Unity

This repository contains a small Unity plugin that shows native toast-style
messages on mobile platforms. It was built as part of the CleverTap Unity SDK
assignment and is intended to demonstrate platform-specific integration,
clean API design, and proper Android permission handling.

---

## What this plugin does

- Shows a native **Android Toast** on Android devices
- Uses a simple fallback on iOS (alert-style message)
- Logs to the Unity Console when running in the Editor

The goal was to expose a **very small and clean public API** that can be used
from any Unity script without worrying about platform details.

---

## Installation (Unity Package Manager)

The plugin is installed using Unity’s Package Manager via a Git URL.

Steps:
1. Open Unity
2. Go to **Window → Package Manager**
3. Click the **+** button (top-left)
4. Select **Add package from git URL**
5. Paste the following URL:
https://github.com/DhruvUnity1/unity-weather-sdk.git


After Unity finishes importing, the package will appear under **In Project**
in the Package Manager.

---

## Basic usage

Using the plugin is intentionally simple:

```csharp
using CleverTap.NativeToast;

public class Example : MonoBehaviour
{
    void Start()
    {
        NativeToast.Show("Hello from native toast!");
    }
}