// Learn more about F# at http://fsharp.org

open System
open Objects

let _top = Console.CursorTop
let _left = Console.CursorLeft
let pc = {x=10;y=10;g='@'}
let cursor = {x=0;y=21;g='_'}

let CreateMap =
    let baseTile = {x=0;y=0;g='#'}
    let top = [0..20] |> List.map (fun p -> {baseTile with x=p})
    let left = [0..20] |> List.map (fun p -> {baseTile with y=p})
    let bottom = [0..20] |> List.map (fun p -> {baseTile with x=p; y=20})
    let right = [0..20] |> List.map (fun p -> {baseTile with y=p; x=20})
    List.concat [top ;left ;bottom ;right]

let ShowObject {x = x;y = y;g = g} = 
    let top = _top + x
    let left = _left + y
    System.Console.SetCursorPosition( top, left)
    System.Console.Write g

let MainLoop pc = 
    let rec loop pc=
        ShowObject pc
        ShowObject cursor
        let key_info = Console.ReadKey()
        match key_info.Key with
        | ConsoleKey.UpArrow -> ShowObject {pc with g=' '};loop {pc with y=pc.y-1}
        | ConsoleKey.DownArrow -> ShowObject {pc with g=' '};loop {pc with y=pc.y+1}
        | ConsoleKey.LeftArrow -> ShowObject {pc with g=' '};loop {pc with x=pc.x-1}
        | ConsoleKey.RightArrow -> ShowObject {pc with g=' '};loop {pc with x=pc.x+1}
        | _ -> ignore
    loop pc

[<EntryPoint>]
let main argv =
    //printfn "Hello World from F#!"
    Console.Clear()
    CreateMap |> List.forall (fun x -> ShowObject x; true) |> ignore
    MainLoop pc
    0 // return an integer exit code
