# Material Replacer

<a href="https://kurotu.booth.pm/items/4023240">
  <img alt="Booth" src="https://asset.booth.pm/static-images/banner/200x40_01.png">
</a>

<a href="https://github.com/kurotu/MaterialReplacer/releases/latest">
  <img alt="Release" src="https://img.shields.io/github/v/release/kurotu/MaterialReplacer">
</a>

[ English | [日本語](./README_JP.md) | [Demo (YouTube)](https://youtu.be/cPbJyPUZaqo) ]

## Overview

Unity editor extension to replace materials at once by defining replacement rules.

This save the hassle of manually changing materials one by one.
It would be useful when applying your own materials to dressed avatars.

This extension is designed for services such as VRChat, but it's usable for any purpose.

## Support

Verified Unity versions
- Unity 2019.4.31f1

## Setup

There are two options to import the package into your project.

- Download the latest `.unitypackage` from [the release page](https://github.com/kurotu/MaterialReplacer/releases/latest) or [Booth]().
- Add `https://github.com/kurotu/MaterialReplacer.git` from UPM.

## Usage

### Create a MaterialReplacerRule asset

1. Right click in project pane, then select *Create* -> *Material Replacer* -> *Material Replacer Rule*.
2. Select the created `MaterialReplacerRule.asset`.
3. Add `Original` and `Replaced` materials in an inspector.
    - Original: Materials which will be replaced.
    - Replaced: Materials which will be applied instead of `Original` (When it's None, `Original` will be kept).
    > ℹ️ You can set `Reference Object` then press `Add to Original Materials`. It's easier than adding original materials one by one.

### Apply materials to a GameObject.

1. Select the menu, *Window* -> *Material Replacer* to show the Material Replacer window.
   > ℹ️ You can also select the menu, *Material Replacer* from the right click menu on a GameObject in the scene.
2. Set `Game Object` and `Material Replacer Rule` in the window.
3. Press `Apply`.
4. All renderers' materials of `Game Object` and its descendants will be replaced by `Material Replacer Rule`.

## License

The MIT License.

## Contact

- VRCID: kurotu
- Twitter: [@kurotu](https://twitter.com/kurotu)
- GitHub: [kurotu/VRCQuestTools](https://github.com/kurotu/VRCQuestTools)
