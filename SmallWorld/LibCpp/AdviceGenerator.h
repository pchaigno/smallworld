#pragma once

#include <map>
#include <vector>
#include "SmallWorld.h"
#include "Point.h"

#define DLL __declspec( dllimport )
#define EXTERNC extern "C"

using namespace std;

class AdviceGenerator {
private:
	Tile** map;
	int size;
	Nation* nations;

	int getMovementScore(Point pos, Nation nation) const;
	bool hasSeaNeighbour(Point pos) const;
	int getCaptureScore(Point pos, Player occupant, Player player) const;

public:
	DLL AdviceGenerator(Tile** map, int size, Nation nationPlayer1, Nation nationPlayer2);
	DLL Point* getAdvice(int x, int y, Player** units, Player player) const;
};