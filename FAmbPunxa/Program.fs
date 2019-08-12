// Learn more about F# at http://fsharp.org

open System
open Objects

let _top = Console.CursorTop
let _left = Console.CursorLeft
let pc = {x=10;y=10;g='@'}

let CreateTile size x y =
    let wall = {x=x ; y=y; g='#'}
    let floor = {x=x ; y=y; g=' '}
    match x, y with
    | 0, _  | _, 0 -> wall
    | x, y when x=size-1 || y=size-1  -> wall
    | _, _ -> floor

let CreateMap size =
    Array2D.init size size (fun x y -> CreateTile size x y)

let ShowObject {x = x;y = y;g = g} = 
    let top = _top + x
    let left = _left + y
    System.Console.SetCursorPosition( top, left)
    System.Console.Write g

let MainLoop pc = 
    let rec loop pc=
        ShowObject pc
        let key_info = Console.ReadKey()
        match key_info.Key with
        | ConsoleKey.UpArrow -> ShowObject {pc with g=' '};loop {pc with y=pc.y-1}
        | ConsoleKey.DownArrow -> ShowObject {pc with g=' '};loop {pc with y=pc.y+1}
        | ConsoleKey.LeftArrow -> ShowObject {pc with g=' '};loop {pc with x=pc.x-1}
        | ConsoleKey.RightArrow -> ShowObject {pc with g=' '};loop {pc with x=pc.x+1}
        | _ -> 0
    loop pc

[<EntryPoint>]
let main argv =
    Console.Clear()
    Console.CursorVisible <- false;
    CreateMap 20 |> Array2D.iter (fun x -> ShowObject x)
    MainLoop pc
