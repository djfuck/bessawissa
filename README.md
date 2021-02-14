# Bessawissa

## An F#-based IRC bot for announcing page titles of URLs

Bessawissa is a simple IRC bot which joins to one predefined channel, listens for chat messages with URLs and all of them that are youtube links, will make bessawissa to send the page title to the channel.

The bot is developed with [F#](https://fsharp.org/). Although the bot itself doesn't provide anything new to the world, it's a one-man attempt of trying to learn F# after years of experience in [C#](https://en.wikipedia.org/wiki/C_Sharp_(programming_language)).

### The idea

The application should be a console executable, and receive settings from CLI args. The only settings I've considered as relevant are: nickname, server address and channel name. My only goal is to make it to join into a single channel in [IRCNet](https://en.wikipedia.org/wiki/IRCnet).

The name comes from the German term "besserwisser" ("besser" = better, "Wisser" = knower), which means a know-it-all or wiseguy.
