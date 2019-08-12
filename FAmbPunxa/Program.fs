// Learn more about F# at http://fsharp.org
open System
open Objects

let _top = Console.CursorTop
let _left = Console.CursorLeft

let CreateTile size x y =
    let wall = {x=x ; y=y; c='#'}
    let floor = {x=x ; y=y; c=' '}
    match x, y with
    | 0, _  | _, 0 -> wall
    | x, y when x=size-1 || y=size-1  -> wall
    | _, _ -> floor

let CreateMap size =
    Array2D.init size size (fun x y -> CreateTile size x y)

let CreateWorld size = 
    {screen = CreateMap size; actors = {x=10; y=10; c='@'}}

let ShowObject {x = x;y = y;c = c} = 
    let top = _top + x
    let left = _left + y
    System.Console.SetCursorPosition( top, left)
    System.Console.Write c

let DirectionalMovement (x,y) direction = 
    match direction with
    | Direction.North -> x, y-1
    | Direction.East  -> x+1, y
    | Direction.South -> x, y+1
    | Direction.West  -> x-1, y
    | _ -> x,y

let CollisionDetector {x=_;y=_;c=what} where oldposition =
    match what with
    | '#' -> oldposition
    | _ -> where

//[{x=x;y=y;c=c}::rest]
let Move {screen=screen;actors={x=x;y=y;c=c}} direction = 
    let newx,newy = DirectionalMovement (x,y) direction
    let what = screen.[newx, newy]
    let updatedx, updatedy = CollisionDetector what (newx,newy) (x,y)
    {screen=screen; actors= {x=updatedx;y=updatedy;c=c}}

let MainLoop world = 
    let rec loop world=
        world.screen |> Array2D.iter (fun x -> ShowObject x)
        ShowObject world.actors
        let Mover = Move world
        let key_info = Console.ReadKey()
        match key_info.Key with
        | ConsoleKey.UpArrow -> Mover Direction.North |> loop
        | ConsoleKey.DownArrow -> Mover Direction.South |> loop
        | ConsoleKey.LeftArrow -> Mover Direction.West |> loop
        | ConsoleKey.RightArrow -> Mover Direction.East |> loop
        | _ -> 0
    loop world

[<EntryPoint>]
let main argv =
    Console.Clear()
    Console.CursorVisible <- false;
    CreateWorld 20 |> MainLoop 
