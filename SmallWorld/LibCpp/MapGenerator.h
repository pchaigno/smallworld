#pragma once

#include "Graph.h"
#include <map>
#include <vector>
#include <stdlib.h>
#include <stdio.h>
#include <time.h>

#define DLL __declspec( dllimport )
#define EXTERNC extern "C"

using namespace std;

class MapGenerator {

public:
	DLL static Tile** generateMap(int size);
	DLL static Point* placeUnits(Tile** map, int size);

private:
	// TODO Is DLL keyword mandatory?
	DLL static int randBounds(int a, int b);
	DLL static void initiateRand();
};