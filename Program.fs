open HtmlAgilityPack
open System.Text.RegularExpressions
open System.Web
open SimpleIRCLib

let IsYouTubeWatchLink urlString = 
    let ismatch = Regex.IsMatch(urlString, @"^((?:https?:)?\/\/)?((?:www|m)\.)?((?:youtube\.com|youtu.be))(\/(?:[\w\-]+\?v=|embed\/|v\/)?)([\w\-]+)(\S+)?$")
    ismatch

let GetTitleFromBody (htmlDoc: HtmlDocument) =
    let node = htmlDoc.DocumentNode.SelectSingleNode("/html/body/title")
    if node = null then "error" else HttpUtility.HtmlDecode(node.InnerText)

[<EntryPoint>]
let main argv =

    if argv.Length <> 3 then
        printfn "%s" "Invalid amount of arguments given. Correct syntax:\n[SERVER] [NICKNAME] [CHANNEL]"
        1
    else

    let irc = new SimpleIRC()
    irc.SetupIrc(argv.[0], argv.[1], argv.[2]) |> ignore

    let ReceivedEventHandler (rcvArgs : IrcReceivedEventArgs) =
        printfn "%s" rcvArgs.Message
        if (rcvArgs.Channel = argv.[2] && IsYouTubeWatchLink(rcvArgs.Message) = true) then
            let htmlDoc = (new HtmlWeb()).Load(rcvArgs.Message)
            irc.SendMessageToChannel(GetTitleFromBody(htmlDoc), argv.[2]) |> ignore

    irc.IrcClient.OnMessageReceived.Add(ReceivedEventHandler)

    if (irc.StartClient() = false) then
        printfn "%s" "Couldn't start IRC client."
        1
    else

    while (irc.IsClientRunning()) do
        if System.Console.ReadLine() = "quit" then
            printfn "%s" "Stopping IRC client..."
            irc.StopClient() |> ignore
            printfn "%s" "Stopped."
    0
