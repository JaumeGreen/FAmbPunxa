module Objects

type WorldObject = {x: int; y: int; c: char}

type Direction = North = 0 | East = 1 | South = 3 | West = 4

type World = {screen: WorldObject[,]; actors: WorldObject list}


