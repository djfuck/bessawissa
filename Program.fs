open HtmlAgilityPack
open System.Text.RegularExpressions
open System.Web

let IsYouTubeWatchLink urlString = 
    Regex.IsMatch(urlString, @"^((?:https?:)?\/\/)?((?:www|m)\.)?((?:youtube\.com|youtu.be))(\/(?:[\w\-]+\?v=|embed\/|v\/)?)([\w\-]+)(\S+)?$")

let GetTitleFromBody (htmlDoc: HtmlDocument) =
    let node = htmlDoc.DocumentNode.SelectSingleNode("/html/body/title")
    if node = null then "error" else HttpUtility.HtmlDecode(node.InnerText)

[<EntryPoint>]
let main argv =
    let testUrl = "https://www.youtube.com/watch?v=6-IRnDofg6Y&list=RD6-IRnDofg6Y&start_radio=1"

    if IsYouTubeWatchLink(testUrl) then
        let htmlDoc = (new HtmlWeb()).Load(testUrl)
        System.Console.WriteLine(GetTitleFromBody(htmlDoc))

    0