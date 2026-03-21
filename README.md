<img src="https://github.com/WaitingForNeko/AstraLingua/blob/main/Assets/Icon10X.png?raw=true" width=180>

# Astra Lingua

Astra Lingua is a universal alien language invented for [Konekomi Castle](https://store.steampowered.com/app/3812300),
a game by studio [Waiting For Neko](https://waitingforneko.com).

## Repository Contents

- [`AstraLingua`](https://github.com/WaitingForNeko/AstraLingua/tree/main/AstraLingua) - A C# library for converting to/from Astra Lingua.
- [`AstraLinguaCheatSheet`](https://github.com/WaitingForNeko/AstraLingua/tree/main/AstraLinguaCheatSheet) - A PDF explaining how Astra Lingua works.
- [`AstraLinguaDictionary`](https://github.com/WaitingForNeko/AstraLingua/tree/main/AstraLinguaDictionary) - A JSONH file containing the standard words used in Astra Lingua.

## Overview

Astra Lingua is designed as a lingua franca for different alien species to communicate with each other.
As such, there are only three sounds.

Since the word for "hello" ("⊢⊢⊩⊩⊪⊪") contains the three sounds in order, it can be used 
to calibrate between species.

Astra Lingua is based around balanced ternary. There are three digits:
- `⊢` (1)
- `⊩` (0)
- `⊪` (-1 / T)

Each word is encoded as an integer in balanced ternary.
For example:
- English: `Hello`
- Conversion: `Hello` -> `8` -> `10T` -> `110 0TT` -> `⊢⊢⊩⊩⊪⊪`
- Astra Lingua: `⊢⊢⊩⊩⊪⊪`

There are two additional symbols for embedding literal numbers:
- `[` (used for writing literal numbers, e.g. `[⊢⊩⊪[` is 8)
- `>` (used for writing literal fractions, e.g. `[⊢⊩⊪>⊢⊢[` is 8/4 or 2)

The Konekomi, an alien species in the world of [Konekomi Castle](https://store.steampowered.com/app/3812300)
who helped to create Astra Lingua, have a dialect that includes feeler vibrations to indicate tone:
- `–` (? / uncertain / question) 
- `=` (. / neutral / expressionless) 
- `≡` (! / urgent / emphasis)

The number codes in Astra Lingua can technically be interpreted with any meaning,
but standard meanings can be found in the Astra Lingua Dictionary. For example:
- `2` - life (moving object)
- `18` - me (speaker / self)
- `149` - storm (atmospheric disturbance / hazard)

To learn more about Astra Lingua, you can [read the cheat sheet](https://github.com/WaitingForNeko/AstraLingua/blob/main/AstraLinguaCheatSheet/AstraLinguaCheatSheet.pdf) or [watch the video](https://youtu.be/EdC5M-olroU).

## Examples

Some examples attempting to translate English to Astra Lingua:

#### "Welcome"
- Number Codes: `8 (hello)`
- Astra Lingua: `⊢⊢⊩⊩⊪⊪`

#### "Astra Lingua"
- Number Codes: `713 (Astra Lingua)`
- Astra Lingua: `⊢⊢⊩⊢⊩⊪⊢⊢⊢⊩⊪⊪`

#### "Who are you?" (Konekomi Dialect)
- Number Codes: `19 (you) 1 (something) 22 (question) ? (uncertain) ? (uncertain)`
- Astra Lingua: `⊢⊢⊪⊪⊩⊢⊩⊪⊢⊢⊢⊪⊪⊢⊢––`

#### "Why is the sky blue?"
- Number Codes: `17 (confuse) 627 (daylight) 508 (blue) 22 (question)`
- Astra Lingua: `⊢⊢⊪⊪⊩⊪⊢⊢⊩⊢⊪⊪⊢⊢⊪⊩⊪⊩⊢⊢⊪⊢⊩⊢⊢⊪⊢⊩⊪⊢⊢⊢⊪⊪⊢⊢`

#### "3.5"
- Fraction: `7 / 2`
- Balanced Ternary Fraction: `1T1 / 1T`
- Astra Lingua: `[⊢⊪⊢>⊢⊪[`

#### "Three Point Five"
- Number Codes: `26 (one) 28 (minus-one) 26 (one) 13 (division) 26 (one) 28 (minus-one)`
- Astra Lingua: `⊢⊢⊩⊪⊩⊪⊢⊢⊩⊪⊩⊢⊢⊢⊩⊪⊩⊪⊢⊢⊢⊩⊪⊢⊢⊢⊩⊪⊩⊪⊢⊢⊩⊪⊩⊢`

## License

This repository is licensed to you under the MIT license.
You can use it freely, but must give attribution.
