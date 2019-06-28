# Starbound Outfit Generator

> <h2>You can now use the <a href="https://silverfeelin.github.io/Starbound-NgOutfitGenerator/">online Outfit Generator</a>!</h2>

A tool that specializes in creating multiplayer compatible custom animated clothing using [Nettle Boy's directives](http://ilovebacons.com/threads/guide-to-re-animating-clothes-with-json.12019/page-5#post-92288) methods.

#### Why pants?

Nettle Boy has found a way to create multiplayer-compatible custom clothing that is fully animated. Hats aren't animated, tops contain only a few frames and back items contain a lot of duplicate frames. All of these options don't work all that well with the method.

To limit the amount of sprites we need to modify, the pants should be used for the main parts of your outfit (chest, pants, back item).
This does come with a limitation; the sprites are rendered between the front arm and body layer of your character. If your outfit comes with sleeves, you'll still need a chest item for this purpose.

#### Why not put the entire outfit on the chest piece, then?

The frames on chest pieces are limited. If you compare any chest sprite sheet with any pants sprite sheet, you'll notice the difference. Putting pants on a chest piece would lead to staggered animations.

#### But I want a custom outfit, not just pants!

Since the frames cover your full character, you can draw an entire outfit and put them on one item (which are, in fact, simply pants). It is highly recommended to use the [Starbound Hatter](https://silverfeelin.github.io/Starbound-Hatter/) for custom hats, to conserve data. This should increase overall performance for you and other players.

For the sleeves of your outfit; these should be drawn on separate sprite sheets. The sleeve generator takes care of this part.

## Installation

This tool requires [.NET Framework 4.5](https://www.microsoft.com/en-US/download/details.aspx?id=30653).

* Download and extract the [latest release](https://github.com/Silverfeelin/Starbound-OutfitGenerator/releases).
  * Make sure both files are in the same folder. The sleeves generator depends on the pants generator to work.

## Usage

* Draw your outfit, or use an existing sprite sheet. Templates can be found in the [templates](https://github.com/Silverfeelin/Starbound-OutfitGenerator/tree/master/templates) folder.
  * The climb frames are not used, so it is highly recommended to keep them out of your sprite sheet.
* Drag your sprite sheet (preferably a 32-bit depth PNG) on top of the `OutfitGenerator` executable.
* Select whether you want to make pants, sleeves or a back item by pressing the according key.
* Use the generated `/spawnitem` command in-game.
  * The command is saved to a file and also copied directly to your clipboard.

For custom sleeves, it is highly recommended to use a provided template. The front and back sleeve sheets have to be combined into one sheet for the tool. You can also sprite the front and back sleeves separately and merge them later by dragging both files on top of the application.
