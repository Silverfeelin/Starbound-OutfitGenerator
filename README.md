# Starbound Pants Generator

A tool that specializes in creating custom animated clothing using [Nettle Boy's directives](http://ilovebacons.com/threads/guide-to-re-animating-clothes-with-json.12019/page-5#post-92288) method.

#### Why pants?

Nettle Boy has found a way to create multiplayer-compatible custom clothing that is fully animated. Hats aren't animated, tops contain only a few frames and back items contain a lot of duplicate frames. All of these options don't work all that well with the method.

#### But I want a custom outfit, not just pants!

Since the frames cover your full character, you can draw an entire outfit and put them on one item (which are, in fact, simply pants). It is highly recommended to use the [Starbound Hatter](https://silverfeelin.github.io/Starbound-Hatter/) for custom hats, to conserve data which will increase overall performance.

The main issue is that sleeves aren't supported as of right now; the outfit renders in front of your body but behind your front arm. An option for sleeve sprite sheets is planned, but no promises can be made so far.

## Installation

This tool requires [.NET Framework 4.5](https://www.microsoft.com/en-US/download/details.aspx?id=30653).

* Download and extract the [latest release](https://github.com/Silverfeelin/Starbound-PantsGenerator/releases).

## Usage

* Sprite your outfit, or use an existing spritesheet. An empty template is included in the release.
 * The climb frames are not used, so it is highly recommended to keep them out of your spritesheet.
* Drag your spritesheet (preferably a 32-bit depth PNG) on top of the executable.
* Use the generated `/spawnitem` command in-game. This requires `/admin` mode to be active.
 * The command is saved to a file and also copied directly to your clipboard.
