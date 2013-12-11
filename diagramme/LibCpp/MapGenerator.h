#pragma once

#ifdef WANTDLLEXP
#define DLL __declspec( dllexport )
#define EXTERNC extern "C"
#else
#define DLL __declspec( dllimport )
#define EXTERNC extern "C"
#endif

class MapGenerator
{
public:
	DLL static int** generateMap(int size);
	DLL static int pseudoRandSquare(int** map, int size, int x, int y);
	DLL static int randBounds(int a, int b);
};